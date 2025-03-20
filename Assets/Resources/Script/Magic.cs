using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class Magic : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer sr;

    public int MagicDamage;
    public Sprite MagicImage;
    public float MagicCoolTime;
    public float MagicSpeed;
    public float MagicRange;
    public bool isSoloAttck;

    public MagicType magicType;
    [SerializeField]
    Rigidbody2D rb;
    public void Init(SOMagic magic)
    {

        magicType = magic.magicType;
        MagicDamage = magic.MagicDamage + (magic.MagicDamage * GameManager.Instance.GetMagicUpgrade(magicType));
        sr.sprite = magic.MagicImage;
        MagicCoolTime = magic.MagicCoolTime;
        MagicSpeed = magic.MagicSpeed;
        MagicRange = magic.MagicRange;
        isSoloAttck = magic.isSoloAttck;

    }

    private void FixedUpdate()
    {
        if (transform.position.y <= (transform.position + new Vector3(0, -MagicRange, 0)).y)
        {
            PoolingManager.ReturnObj("Magic", this.gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0,-MagicRange,0), MagicSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if(isSoloAttck)
                PoolingManager.ReturnObj("Magic", this.gameObject);

            Debug.Log("Monster Get DMG");
            collision.GetComponent<Monster>().GetDamage(MagicDamage);
        }
    }

}
