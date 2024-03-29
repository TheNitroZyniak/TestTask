using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

    public override void Fire(Vector2 direction) {
        Bullet bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
        bullet.Shoot(direction, damage);

    }
}
