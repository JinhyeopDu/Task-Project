using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 10;             // ������
    public float attackRate = 1f;       // ���� ����
    private float nextAttackTime = 0f;  // ���� �ð�

    private BoxManager boxManager;      // �ڽ� ���� ��ũ��Ʈ
    private Animator animator;          // �ִϸ��̼� 

    void Start()
    {
        boxManager = FindObjectOfType<BoxManager>();
        animator = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        InvokeRepeating(nameof(AttackTower), 0f, attackRate);
    }

    void AttackTower()
    {
        if (Time.time < nextAttackTime) return;

        GameObject lowestBox = boxManager.boxes.Count > 0 ? boxManager.boxes[0] : null;

        if (lowestBox != null)
        {
            lowestBox.GetComponent<BoxHealth>().TakeDamage(damage);
        }

        nextAttackTime = Time.time + attackRate;
    }
}
