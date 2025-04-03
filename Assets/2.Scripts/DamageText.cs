using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour
{
    // �������� �Ű澲���� �ð��й踦 �� �ؼ� ��� ���� ��ũ��Ʈ(�̿ϼ�)

    public float moveSpeed = 1f;  // ���� �̵� �ӵ�
    public float fadeSpeed = 1f;  // ������ ������� �ӵ�
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
