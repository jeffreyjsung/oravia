using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindAttack : Attack
{
    public override void UseAttack(Vector3 spawnPos, Vector3 direction, string target)
    {
        RaycastHit hit;
        attackEffect.Play();
        if (Physics.SphereCast(spawnPos, 0.5f, direction, out hit, AttackRange))
        {
            if (hit.collider.CompareTag(target))
            {
                switch (target)
                {
                    case "Player":
                        hit.collider.GetComponent<Player>().TakeDamage(Damage);
                        break;
                    case "Enemy":
                        hit.collider.GetComponent<Enemy>().TakeDamage(Damage);
                        break;
                }
            }
        }
    }
}
