using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour{
    public Health health;
    [SerializeField] protected Transform body;
    [SerializeField] protected float moveSpeed;
    protected Vector3 direction;

    private void Start() {
        health.OnDeath += Die;
    }

    public abstract void Attack();

    protected virtual void Move(Vector3 dir) {
        if (dir.x >= 0) body.transform.localScale = Vector3.one;
        else body.transform.localScale = new Vector3(-1, 1, 1);
    }

    public abstract void Die();

}
