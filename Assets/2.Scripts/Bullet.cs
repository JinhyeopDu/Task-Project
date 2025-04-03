using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   // ���ǵ� 
    public int damage = 20;     // ������
    public float lifetime = 3f; // 3�� �� ����

    private void Start()
    {
        Destroy(gameObject, lifetime); // ���� �ð��� ������ ����
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // �Ѿ� �̵�
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
