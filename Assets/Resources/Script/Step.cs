using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class Step : MonoBehaviour
{

    [SerializeField]
    SOMagic magicData;

    [SerializeField]
    Button BTN_Step;

    [SerializeField]
    Image IMG_RedDot;

    [SerializeField]
    Image IMG_ActiveImage;

    [SerializeField]
    Image IMG_Icon;

    [SerializeField]
    Image IMG_CoolTime;

    [SerializeField]
    Text TXT_Cnt;


    [SerializeField]
    int Cnt;

    [SerializeField]
    public GameManager.MagicType magicType;
    [SerializeField]
    public GameManager.GradeType gradeType;

    private float timer;

    private void Awake()
    {
        timer = magicData.MagicCoolTime;
        BTN_Step.onClick.AddListener(Updrade);
    }
    private void Update()
    {
        if(Cnt != 0)
        {
            if (Cnt >= 3)
                IMG_RedDot.gameObject.SetActive(true);
            else
                IMG_RedDot.gameObject.SetActive(false);


            timer -= Time.deltaTime;
            float width = (timer / magicData.MagicCoolTime) * IMG_ActiveImage.rectTransform.rect.width;
            IMG_CoolTime.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            if(timer <= 0)
            {
                timer = magicData.MagicCoolTime;
                IMG_CoolTime.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
                // 공격신호

                GameManager.Instance.player.Attack(magicData);
            }
        }
        else
        {
            IMG_ActiveImage.gameObject.SetActive(false);
            IMG_RedDot.gameObject.SetActive(false);

        }
    }

    void Updrade()
    {
        if(Cnt>=3)
        {
            var TypePicker = new MagicRandomMangager<MagicType>();

            TypePicker.AddEntry(MagicType.GROUND, 33);
            TypePicker.AddEntry(MagicType.FIRE, 33);
            TypePicker.AddEntry(MagicType.WATER, 33);

            GameManager.Instance.AddMagic(TypePicker.PickRandom(), gradeType+1);
            GameManager.Instance.RemoveMagic(magicType, gradeType);
            UIManager.Instance.GetMagicPanel().SetMagic();
            UIManager.Instance.GetMagicPanel().SetMagicCntText();
        }
    }
    public void ActiveMagic(int _cnt)
    {
        IMG_ActiveImage.gameObject.SetActive(true);
        Cnt = _cnt;
        TXT_Cnt.text = Cnt.ToString();
    }
}
