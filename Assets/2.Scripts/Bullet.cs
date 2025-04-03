using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   // 스피드 
    public int damage = 20;     // 데미지
    public float lifetime = 3f; // 3초 후 삭제

    private void Start()
    {
        Destroy(gameObject, lifetime); // 일정 시간이 지나면 삭제
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // 총알 이동
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            ZombieHealth zombieHealth = collision.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damage);
            }
        }
    }
}
