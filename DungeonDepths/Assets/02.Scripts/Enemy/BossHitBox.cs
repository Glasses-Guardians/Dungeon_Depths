using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    public BossGolem boss;
    public PlayerBase player;
    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }
    private void Start()
    {
        boss = transform.GetComponentInParent<BossGolem>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
    }
    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�浹ü ���� : " + other.name);
        if(other.CompareTag("Player"))
        {
            float damage = boss.AttackDamage;
            Debug.Log("���� ���� ���� : " + other.name);
            player.SetTakedDamage(damage);
        }
    }
}
