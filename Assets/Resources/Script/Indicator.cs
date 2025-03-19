using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;

    [SerializeField]
    Text TXT_Desc;

    [SerializeField]
    float speed = 2f;

    private void Update()
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, rect.anchoredPosition + new Vector2(0,20f), Time.deltaTime * speed);
    }

    public void SetDesc(string _desc,float _remainTime)
    {
        TXT_Desc.text = _desc;

        StartCoroutine(CO_ReturnIndicator(_remainTime));
    }

    IEnumerator CO_ReturnIndicator(float _remainTime)
    {
        yield return new WaitForSeconds(_remainTime);
        PoolingManager.ReturnObj("Indicator", this.gameObject);

    }

}
