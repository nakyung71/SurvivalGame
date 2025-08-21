using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;
    private AttackSound attackSound;

    // ===== ▼▼▼ 추가: 도구 종류 정의 ▼▼▼ =====
    public enum ToolKind { Axe, Hammer }                // ★추가
    [Header("Tool")]                                    // ★추가
    public ToolKind toolKind = ToolKind.Axe;            // ★추가
    // ===== ▲▲▲ 추가: 도구 종류 정의 ▲▲▲ =====

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
        attackSound = GetComponentInParent<AttackSound>();
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate);
            if (attackSound != null) attackSound.Swing();   // ★변경: NRE 방지 가드 추가
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        if (camera == null) camera = Camera.main;

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * attackDistance, Color.red, 0.5f);

        if (Physics.Raycast(ray, out hit, attackDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            Debug.Log($"[공격 판정] {hit.collider.name} 에 맞음 (좌표: {hit.point})");

            if (doesDealDamage)
            {
                // 자식 콜라이더를 맞아도 부모의 Enemy를 찾도록 InParent 사용
                var damageable = hit.collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakePhysicalDamage(damage);
                    attackSound.AnimalHit();
                    Debug.Log($"[전투] {hit.collider.name} 에 {damage} 피해");
                    
                    return; // 한 번의 스윙에 자원채집까지 함께하지 않으려면 바로 종료
                }
            }

            if (doesGatherResources)
            {
                var resource = hit.collider.GetComponentInParent<Resource>();
                if (resource != null)
                {
                    // ===== ▼▼▼ 추가: 도구-자원 호환 체크 ▼▼▼ =====
                    if (IsCompatible(toolKind, resource.resourceKind))  // ★추가
                    {                                                   // ★추가
                        Debug.Log($"[공격 판정] {resource.name} 에서 자원 채집 시도"); // (원문 유지)
                        resource.Gather(hit.point, hit.normal);         // (원문 유지)
                        if (attackSound != null) attackSound.WoodHit();     // ★변경: NRE 가드 + 호환될 때만 Hit
                    }                                                   // ★추가
                    else                                                // ★추가
                    {                                                   // ★추가
                        Debug.LogWarning($"[채집] {toolKind} 로는 {resource.resourceKind} 를 캘 수 없음"); // ★추가
                    }                                                   // ★추가
                    // ===== ▲▲▲ 추가: 도구-자원 호환 체크 ▲▲▲ =====
                }
                else
                {
                    Debug.LogWarning($"[공격 판정] {hit.collider.name} 오브젝트에서 Resource 스크립트를 찾을 수 없음");
                }
            }
        }
        else
        {
            Debug.Log("[공격 판정] 레이캐스트가 아무것도 맞추지 못함 (범위 부족 또는 콜라이더 없음)");
        }
    }

    // ===== ▼▼▼ 추가: 호환 규칙 함수 ▼▼▼ =====
    private bool IsCompatible(ToolKind tool, Resource.ResourceKind kind) // ★추가
    {                                                                    // ★추가
        return (tool == ToolKind.Axe && kind == Resource.ResourceKind.Tree)   // ★추가
            || (tool == ToolKind.Hammer && kind == Resource.ResourceKind.Rock); // ★추가
    }                                                                    // ★추가
    // ===== ▲▲▲ 추가: 호환 규칙 함수 ▲▲▲ =====
}
