using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint;         // 총구 위치
    public GameObject bulletPrefab;     // 총알 프리팹
    public float bulletSpeed = 10f;     // 총알 나가는 속도
    public float fireRate = 1f;         // 1초 간격
    private float nextFireTime;         // 다음 발사

    void Update()
    {
        RotateGun(); // 총을 좀비 방향으로 회전
        TryShoot();  // 자동 발사
    }

    // 총구 회전 함수
    private void RotateGun()
    {
        GameObject closestZombie = FindClosestZombie();
        if (closestZombie != null)
        {
            Vector3 direction = closestZombie.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // 총 회전
        }
    }

    //자동 발사용 함수
    private void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 1초 후 다시 발사
        }
    }

    // 발사 관리용 함수
    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;    // 총구 방향으로 발사
        Destroy(bullet, 3f);                            // 3초 후 총알 삭제
    }

    // 가까운 좀비를 겨냥하기 위한 함수
    private GameObject FindClosestZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject zombie in zombies)
        {
            float distance = Vector2.Distance(transform.position, zombie.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = zombie;
            }
        }

        return closest;
    }
}
