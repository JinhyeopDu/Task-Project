using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 10;             // 데미지
    public float attackRate = 1f;       // 공격 간격
    private float nextAttackTime = 0f;  // 공격 시간

    private BoxManager boxManager;      // 박스 관리 스크립트
    private Animator animator;          // 애니메이션 

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
