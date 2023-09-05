using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField][Range(1f, 100f)] float _bulletSpeed = 10f;

    [SerializeField] Transform _crosshair;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    void Shoot(){

        Vector3 dir = _crosshair.position - transform.position;
        dir.Normalize();
        dir.z = 0;

        Vector3 pos = transform.position + (Vector3)dir;

        GameObject bullet = Instantiate(_bulletPrefab, pos, Quaternion.identity);


        bullet.GetComponent<Rigidbody2D>().velocity = dir * _bulletSpeed;
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Input.mousePosition);
    }
}
