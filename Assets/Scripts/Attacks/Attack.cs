using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    #region Attack Variables
    [SerializeField]
    [Tooltip("How much damage this attack deals.")]
    protected float Damage;
    public float damage
    {
        get
        {
            return Damage;
        }
    }

    [SerializeField]
    [Tooltip("The range of this attack.")]
    protected float AttackRange;
    public float attackRange
    {
        get
        {
            return AttackRange;
        }
    }

    [SerializeField]
    [Tooltip("The cooldown time of this attack.")]
    protected float CooldownTime;
    public float cooldownTime
    {
        get
        {
            return CooldownTime;
        }
    }

    [SerializeField]
    [Tooltip("Where to spawn this attack with respect to the object.")]
    protected Vector3 AttackOffset;
    public Vector3 attackOffset
    {
        get
        {
            return AttackOffset;
        }
    }

    protected ParticleSystem attackEffect;
    #endregion

    #region Initialization
    private void Awake()
    {
       attackEffect = GetComponent<ParticleSystem>();
    }
    #endregion

    #region Use Method
    public abstract void UseAttack(Vector3 spawPos, Vector3 direction, string target);
    #endregion
}
