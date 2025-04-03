using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public int health = 50; // 박스 체력
    public bool IsDestroyed => health <= 0; // 체력이 0 이하이면 true

    // 받는 데미지
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyBox();
        }
    }

    // 오브젝트 파괴
    void DestroyBox()
    {
        Destroy(gameObject); // 박스 삭제
    }
}
