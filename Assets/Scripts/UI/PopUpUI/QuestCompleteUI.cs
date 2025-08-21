using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompleteUI : MonoBehaviour
{
    Image panelImage;
    RectTransform rectTransform;
    Vector2 panelVector;
    float openTime = 0.5f;
    float closeTime = 0.3f;
    [SerializeField] TextMeshProUGUI completetext;
    private void OnEnable()
    {
        panelImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        completetext.gameObject.SetActive(false);
        StartCoroutine(OpenEffect());
    }

    
    IEnumerator OpenEffect()
    {
        panelVector = rectTransform.sizeDelta;
        float passedTime = 0f;
        float lerpNumber = 0f;
        while(passedTime<openTime)
        {
            passedTime += Time.deltaTime;
            lerpNumber = Mathf.Clamp01(passedTime/openTime);
            panelVector.x = Mathf.Lerp(0f, 800f, lerpNumber);
            rectTransform.sizeDelta = panelVector;
            yield return null;
            
        }
        completetext.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        completetext.gameObject.SetActive(false);
        passedTime = 0f;
        lerpNumber = 0f;
        while(passedTime<closeTime)
        {
            passedTime += Time.deltaTime;
            lerpNumber = Mathf.Clamp01(passedTime / closeTime);
            panelVector.x = Mathf.Lerp(800f, 0f, lerpNumber);
            rectTransform.sizeDelta = panelVector;
        }
        this.gameObject.SetActive(false);
        
    }

}
