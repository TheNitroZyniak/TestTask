using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase{
    [SerializeField] private Joystick movement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform cam;
    public Weapon currentWeapon;


    private void Update() {
        direction = GetInputVector();
        Move(direction);
    }
    protected override void Move(Vector3 dir) {
        base.Move(dir);

        if (Input.GetKeyDown(KeyCode.Space)) 
            Attack();

        RotateWeaponTowardsDirection(direction);
        ApplyMovement(direction);

    }

    private Vector3 GetInputVector() {
        Vector2 inputDir = new Vector2(movement.Horizontal, movement.Vertical) * moveSpeed;
        return inputDir;
    }

    private void RotateWeaponTowardsDirection(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(direction.x >= 0) currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        else currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 + angle));
    }

    private void ApplyMovement(Vector2 inputVector) {
        rb.velocity = inputVector;
    }


    public override void Attack() {
        currentWeapon.Fire(direction.normalized);
    }


    private void LateUpdate() {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

    public override void Die() {
        GameManager.Instance.PlayerDied();
    }
}
