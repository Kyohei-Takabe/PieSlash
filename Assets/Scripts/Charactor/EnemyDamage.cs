using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    const float DAMAGE_TIME = 0.5f; //ダメージで赤くなる時間
    
    public UnityEngine.UI.Slider sliderHP;
    public float HP;
    public float maxHP = 100.0f;
    public float kaisuu;
    float damageTime = 0.0f;
    public GameObject Player;

    void Start()
    {
        //体力をMAXにする
        this.HP = maxHP;
        //メーターを満タンにする
        if (sliderHP != null)
        {
            sliderHP.value = 1.0f;
        }
    }

    void Update()
    {
        this.transform.LookAt(Player.transform);
        //ダメージ時間があるとき赤ー＞白に変化させる
        if (damageTime > 0.0f)
        {
            damageTime -= Time.deltaTime;
            if (damageTime < 0.0f) damageTime = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float Ondamage = maxHP / kaisuu;
        //"Pie"タグがついているものが当たったら
        if (collision.gameObject.tag == "Pie")
        { 
            //ダメージ計算
            this.HP -= Ondamage;
            if (this.HP <= 0.0f)
            {
                //ゼロになったら自己削除
                this.HP = 0.0f;
                Destroy(this.gameObject);
            }

            //メーターに反映
            if (sliderHP != null)
            {
                sliderHP.value = this.HP / maxHP;
            }            
        }
    }
}