using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour
{
    // 좀비스폰만 신경쓰느라 시간분배를 못 해서 사용 못한 스크립트(미완성)

    public float moveSpeed = 1f;  // 위로 이동 속도
    public float fadeSpeed = 1f;  // 서서히 사라지는 속도
    private Text damageText;
    private Color textColor;

    private void Awake()
    {
        damageText = GetComponent<Text>();
        textColor = damageText.color;
    }

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            textColor.a = Mathf.Lerp(1, 0, t);
            damageText.color = textColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}
