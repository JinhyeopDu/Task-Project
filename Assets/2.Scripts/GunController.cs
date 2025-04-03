using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint;         // �ѱ� ��ġ
    public GameObject bulletPrefab;     // �Ѿ� ������
    public float bulletSpeed = 10f;     // �Ѿ� ������ �ӵ�
    public float fireRate = 1f;         // 1�� ����
    private float nextFireTime;         // ���� �߻�

    void Update()
    {
        RotateGun(); // ���� ���� �������� ȸ��
        TryShoot();  // �ڵ� �߻�
    }

    // �ѱ� ȸ�� �Լ�
    private void RotateGun()
    {
        GameObject closestZombie = FindClosestZombie();
        if (closestZombie != null)
        {
            Vector3 direction = closestZombie.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // �� ȸ��
        }
    }

    //�ڵ� �߻�� �Լ�
    private void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 1�� �� �ٽ� �߻�
        }
    }

    // �߻� ������ �Լ�
    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;    // �ѱ� �������� �߻�
        Destroy(bullet, 3f);                            // 3�� �� �Ѿ� ����
    }

    // ����� ���� �ܳ��ϱ� ���� �Լ�
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
