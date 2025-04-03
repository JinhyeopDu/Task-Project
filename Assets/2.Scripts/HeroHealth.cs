using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public int maxHealth = 200;     // �÷��̾� �ִ� ü��
    private int currentHealth;      // �÷��̾� ���� ü��
    private Animator animator;      // �ִϸ����� 
    private bool isDead = false;    // ���� üũ

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // �޴� ������
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �÷��̾� ����
    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 2f); // 2�� �� ����
    }
}
