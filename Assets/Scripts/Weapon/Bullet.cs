using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] Rigidbody2D rb;

    int damage;

    public void Shoot(Vector3 direction, int bulletDamage) {
        rb.velocity = direction * 30;
        damage = bulletDamage;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Health>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
