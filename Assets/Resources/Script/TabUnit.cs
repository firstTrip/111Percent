using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabUnit : MonoBehaviour
{

    [SerializeField]
    Image IMG_TabPanel;

    [SerializeField]
    Image IMG_RedDot;

    [SerializeField]
    Text TXT_Cnt;


    [SerializeField]
    public enum ECellType
    {
        NONE,
        PRIZE,
        PET,
        UPGRADE,
        MINE

    }

    public ECellType eCellType;
    public void ActivePanel(Action action1 ,Action action2)
    {
        if (!IMG_TabPanel.IsActive())
        {
            action1?.Invoke();
            IMG_TabPanel.gameObject.SetActive(true);
            IMG_TabPanel.transform.position += new Vector3(0, 200, 0);
        }
        else
        {
            IMG_TabPanel.gameObject.SetActive(false);
            IMG_TabPanel.transform.position -= new Vector3(0, 200, 0);
            action2?.Invoke();
        }
    }

    public void SetCnt(int cnt)
    {
        TXT_Cnt.text = cnt.ToString();
    }
}
