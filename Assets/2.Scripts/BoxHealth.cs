using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public int health = 50; // �ڽ� ü��
    public bool IsDestroyed => health <= 0; // ü���� 0 �����̸� true

    // �޴� ������
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyBox();
        }
    }

    // ������Ʈ �ı�
    void DestroyBox()
    {
        Destroy(gameObject); // �ڽ� ����
    }
}
