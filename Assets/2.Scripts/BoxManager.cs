using UnityEngine;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
    public List<GameObject> boxes = new List<GameObject>();  // 박스 리스트

    void Start()
    {
        UpdateBoxList();
    }

    // 박스 리스트를 새로 정렬하는 함수(미완성)
    void UpdateBoxList()
    {
        boxes.Clear();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Hero")) // Hero 태그가 붙은 박스들 추가
            {
                boxes.Add(child.gameObject);
            }
        }
        boxes.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y)); // Y축 기준 정렬
    }

    // 박스가 파괴되었을 때 호출(미완성)
    public void HandleBoxDestroyed(GameObject destroyedBox)
    {
        int destroyedIndex = boxes.IndexOf(destroyedBox);
        if (destroyedIndex == -1) return;

        Destroy(destroyedBox); // 박스 삭제
        boxes.RemoveAt(destroyedIndex);

        // 위에 있는 오브젝트들 한 칸씩 내리기
        for (int i = destroyedIndex; i < boxes.Count; i++)
        {
            boxes[i].transform.position += Vector3.down * 1.0f;
        }

        // 플레이어도 내려야 하는 경우
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero != null && hero.transform.position.y > destroyedBox.transform.position.y)
        {
            hero.transform.position += Vector3.down * 1.0f;
        }
    }
}
