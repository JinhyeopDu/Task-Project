using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    private int currentHealth;  // 현재 체력

    // 체력 바(막바지에 넣어보려고 했다가 시간이 부족해서 에디터에 추가를 못 했습니다.
    public Slider healthBar;   

    void Start()
    {
        currentHealth = maxHealth;

        // 체력 바 초기화
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // 체력이 0 이하가 되면 사망
        if (currentHealth <= 0)
        {
            Die();
        }

        // 체력 바 업데이트
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} 좀비가 사망했습니다.");
        Destroy(gameObject);
    }
}
