using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwordHitBox : MonoBehaviour
{
    LayerMask layer;
    [SerializeField] BossBaseFSM boss;
    [SerializeField] FinalBoss finalBoss;
    [SerializeField] Collider[] colliders;
    [SerializeField] float swordDamage;
    [SerializeField] PlayerBase player;
    //This function is called when the object becomes enabled and active.
    
    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        boss = GameObject.FindWithTag("Boss").GetComponent<BossBaseFSM>();
    }
    private void Start()
    {
        //boss = GameObject.FindWithTag("Boss").GetComponent<BossBaseFSM>();
        //Debug.Log("Į ���ݷ� : " + swordDamage);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        layer = 1 << 7;
        //Debug.Log("�浹ü: " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<MonsterBase>().GetHit(5);
            colliders = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2, Quaternion.identity, layer);

            foreach (Collider _collider in colliders)
            {
                CheckCritical();
                //Debug.Log("�����ϱ� : " + _collider.tag);
                Debug.Log("������" + swordDamage);
                _collider.SendMessage("GetDamage", swordDamage);
                if (player.HasPoison)
                    _collider.SendMessage("GetDotDamage");
                player.HpCur += 5f;
                if (player.HpCur > 100)
                    player.HpCur = 100f;
            }
        }
        else if (other.CompareTag("Boss"))
        {
            if (player.BossBonus)
            {
                swordDamage += 10;
            }
            CheckCritical();
            boss.GetHit(swordDamage);
        }
        //else if (other.CompareTag("FinalBoss"))
        //{
        //    if (player.BossBonus)
        //    {
        //        swordDamage += 10;
        //        CheckCritical();
        //        finalBoss.GetHit(swordDamage);
        //    }
        //}

    }

    private void CheckCritical()
    {
        if (this.gameObject.name == "SwordHitBox") swordDamage = player.AttackPower;
        else if (this.gameObject.name == "Skill1HitBox") swordDamage = player.AttackPower * 3;
        //else if (this.gameObject.name == "Skill2HitBox") swordDamage = player.AttackPower * 2;
        if (Random.Range(0, 10) < 3 && player.Amplify)
            swordDamage *= 2f;
        Debug.Log("Į ������" + swordDamage);
    }

    
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
