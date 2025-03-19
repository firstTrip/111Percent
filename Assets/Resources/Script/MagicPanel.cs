using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Canvas")]
    Canvas uiCanvas;

    private void Awake()
    {
        BTN_Chat.onClick.AddListener(Indicator);
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

    void Indicator()
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
}
