using UnityEngine;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
    public List<GameObject> boxes = new List<GameObject>();  // �ڽ� ����Ʈ

    void Start()
    {
        UpdateBoxList();
    }

    // �ڽ� ����Ʈ�� ���� �����ϴ� �Լ�(�̿ϼ�)
    void UpdateBoxList()
    {
        boxes.Clear();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Hero")) // Hero �±װ� ���� �ڽ��� �߰�
            {
                boxes.Add(child.gameObject);
            }
        }
        boxes.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y)); // Y�� ���� ����
    }

    // �ڽ��� �ı��Ǿ��� �� ȣ��(�̿ϼ�)
    public void HandleBoxDestroyed(GameObject destroyedBox)
    {
        int destroyedIndex = boxes.IndexOf(destroyedBox);
        if (destroyedIndex == -1) return;

        Destroy(destroyedBox); // �ڽ� ����
        boxes.RemoveAt(destroyedIndex);

        // ���� �ִ� ������Ʈ�� �� ĭ�� ������
        for (int i = destroyedIndex; i < boxes.Count; i++)
        {
            boxes[i].transform.position += Vector3.down * 1.0f;
        }

        // �÷��̾ ������ �ϴ� ���
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero != null && hero.transform.position.y > destroyedBox.transform.position.y)
        {
            hero.transform.position += Vector3.down * 1.0f;
        }
    }
}
