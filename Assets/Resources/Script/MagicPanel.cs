using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class MagicPanel : MonoBehaviour
{
    [Header("Button Group")]
    [SerializeField]
    Button BTN_Chat;

    [SerializeField]
    Button BTN_Speed;

    [Space]
    [Header("Text Group")]

    [SerializeField]
    Text TXT_Speed;

    [SerializeField]
    Text TXT_Coin;

    [SerializeField]
    Text TXT_Sprit;

    [SerializeField]
    Text TXT_Magic;

    [SerializeField]
    Text TXT_Wave;

    [SerializeField]
    Text TXT_Timer;

    [Space]
    [Header("Rect Group")]
    [SerializeField]
    MagicGroup Ground;
    [SerializeField]
    MagicGroup Fire; 
    [SerializeField]
    MagicGroup Water;

    [Header("Canvas")]
    Canvas uiCanvas;

    private void Awake()
    {
        BTN_Chat.onClick.AddListener(ChatIndicator);
        BTN_Speed.onClick.AddListener(SpeedControl);

        uiCanvas = UIManager.Instance.GetUICanvas();

        TXT_Coin.text = GameManager.Instance.GetCoin().ToString();
    }
    public Text GetTimer()
    { 
        return TXT_Timer; 
    }

    public Text GetTimerTitle()
    {
        return TXT_Wave;
    }

    public void SetCoinText()
    {
        TXT_Coin.text = GameManager.Instance.GetCoin().ToString();
    }

    public void SetSpritText()
    {
        TXT_Sprit.text = GameManager.Instance.GetSprit().ToString();
    }

    public void SetMagicCntText()
    {
        TXT_Magic.text = GameManager.Instance.GetMagicCnt().ToString() + "/" + GameManager.Instance.GetMaxMagicCnt().ToString();
    }

    void ChatIndicator()
    {
       var obj = PoolingManager.GetObj("Indicator");
       obj.transform.SetParent(uiCanvas.transform);
       obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
       obj.GetComponent<Indicator>().SetDesc("싱글 플레이중 사용이 불가능 합니다.",1.5f);
    }

    void SpeedControl()
    {
        GameManager.Instance.SetGameSpeed();
        TXT_Speed.text = GameManager.Instance.GetGameSpeed().ToString("n1");
    }

    public void SetMagic()
    {
        Dictionary<MagicType, Dictionary<GradeType, int>> dict = GameManager.Instance.GetMagicDict();

       foreach(var data in dict)
       {
            foreach(var item in data.Value)
            {
                switch(data.Key)
                {
                    case MagicType.GROUND:

                        if (item.Value > 0)
                        {

                            Ground.stepList[((int)item.Key - 1)].GetComponent<Step>().ActiveMagic(item.Value);

                        }
                        else
                        {
                            Ground.stepList[((int)item.Key - 1)].GetComponent<Step>().ActiveMagic(0);
                        }
                        break;

                    case MagicType.FIRE:

                        if (item.Value > 0)
                        {
                            Fire.stepList[((int)item.Key - 1)].GetComponent<Step>().ActiveMagic(item.Value);
                        }
                        else
                        {
                            Fire.stepList[((int)item.Key - 1)].GetComponent<Step>().ActiveMagic(0);
                        }
                        break;

                    case MagicType.WATER:

                        if (item.Value > 0)
                        {
                            Water.stepList[((int)item.Key - 1)].GetComponent<Step>().ActiveMagic(item.Value);
                        }
                        else
                        {
                            Water.stepList[((int)item.Key - 1)].GetComponent<Step>().ActiveMagic(0);
                        }
                        break;
                }

                
            }
       }

    }
}
