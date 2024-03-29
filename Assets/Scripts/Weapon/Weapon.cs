using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] protected Bullet bulletPref;

    [SerializeField] protected int damage;

    
    public abstract void Fire(Vector2 direction);

}