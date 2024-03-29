using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterBase{

    PlayerCharacter player;
    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackRate = 1f;
    [SerializeField] int damage;
    private float attackTimer;

    private void OnEnable() {
        GameManager.Instance.RegisterEnemy();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }


    private void Update() {
        Move(direction);
    }

    protected override void Move(Vector3 dir) {
        base.Move(dir);
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < chaseRange) {
            direction = (player.transform.position - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * dir;
        }

        attackTimer += Time.deltaTime;
        if (distanceToPlayer < attackRange && attackTimer >= attackRate) {
            Attack();
            attackTimer = 0f;
        }
    }

    public override void Attack() {
        player.health.GetDamage(damage);
    }
    
    public override void Die() {
        GameManager.Instance.EnemyKilled();
        SpawnItem();
    }


    private void SpawnItem() {
        if (itemPrefabs.Length > 0) {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }

    
}
