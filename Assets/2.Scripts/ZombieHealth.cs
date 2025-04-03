using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100; // �ִ� ü��
    private int currentHealth;  // ���� ü��

    // ü�� ��(�������� �־���� �ߴٰ� �ð��� �����ؼ� �����Ϳ� �߰��� �� �߽��ϴ�.
    public Slider healthBar;   

    void Start()
    {
        currentHealth = maxHealth;

        // ü�� �� �ʱ�ȭ
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // ü���� 0 ���ϰ� �Ǹ� ���
        if (currentHealth <= 0)
        {
            Die();
        }

        // ü�� �� ������Ʈ
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ���� ����߽��ϴ�.");
        Destroy(gameObject);
    }
}
