using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public int maxHealth = 200;     // 플레이어 최대 체력
    private int currentHealth;      // 플레이어 현재 체력
    private Animator animator;      // 애니메이터 
    private bool isDead = false;    // 죽음 체크

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // 받는 데미지
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 플레이어 죽음
    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 2f); // 2초 후 제거
    }
}
