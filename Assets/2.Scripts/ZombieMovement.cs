using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 3f;        // �̵��ӵ�
    public float jumpForce = 5f;        // �����ϱ� ���� ��
    public float jumpForwardSpeed = 2f; // ���� �� ������ ���ư��� �ӵ�
    public LayerMask zombieLayer;       // �����̾� üũ��
    public LayerMask groundLayer;       // �ٴڷ��̾� üũ��

    private Rigidbody2D rb;             // ������ٵ�2D
    private Animator animator;          // �ִϸ�����
    private bool isJumping = false;     // ���� üũ
    private bool isAttacking = false;   // ���� üũ
    private bool isClimbing = false;    // ��� üũ
    private bool isFalling = false;     // ���� üũ

    private static int attackingZombies = 0;    // �������� ���� üũ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.isKinematic = false;
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (!isJumping && !isAttacking)
        {
            MoveLeft();
        }
        CheckZombiesInFront();  // �տ� ���� Ȯ��
        CheckIfFalling();       // ���� Ȯ��
    }

    // ���� �̵� ���� �Լ�
    private void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        animator?.SetBool("IsIdle", false);
        animator?.SetBool("IsAttacking", false);
    }

    // �տ� ���� ���� üũ�ϰ�, ���� ���� �̻��̸� ������Ű�� ���� �Լ�
    private void CheckZombiesInFront()
    {
        Vector2 rayOrigin = transform.position + new Vector3(0f, 0.5f, 0f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, Vector2.left, 1.5f, zombieLayer);
        int count = 0;
        Transform highestZombie = null;
        float highestY = float.MinValue;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                count++;
                if (hit.transform.position.y > highestY)
                {
                    highestY = hit.transform.position.y;
                    highestZombie = hit.transform;
                }
            }
        }
        Debug.DrawRay(rayOrigin, Vector2.left * 1.5f, Color.red);

        if (count >= 3 && highestZombie != null) // ���� ���� �̻��̸� ����
        {
            StartCoroutine(TryClimb(highestZombie));
        }
    }

    // ���� ���� �̻��� ���� ������ ���� �ö󰡰� �Ϸ��� �Լ�
    private IEnumerator TryClimb(Transform targetZombie)
    {
        isClimbing = true;
        rb.gravityScale = 1.2f; // ������ �� �߷� Ȱ��ȭ
        rb.velocity = new Vector2(0, jumpForce);
        yield return new WaitForSeconds(0.5f);

        if (targetZombie == null) // Ȥ�� ������ ��� ��� �ڵ�
        {
            isClimbing = false;
            yield break;
        }

        float newYPosition = targetZombie.position.y + 1.0f;
        rb.gravityScale = 0; // ���� �� �߷� ����
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        isClimbing = false;
    }

    // ���� ���������� ���� �� ������ �������� �ʰ� ���� ���� �뵵
    private void CheckIfFalling()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, zombieLayer);
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        if (hit.collider == null && !isGrounded) // �ؿ� ���� ����, ���� �ƴϸ�
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        if (isFalling)
        {
            transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0); // õõ�� ������
        }
    }

    // �÷��̾� �� �ڽ� �浹�� ���ݼ��� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero; // ���� �� ���߱�
                animator.SetBool("IsAttacking", true);
                attackingZombies++;
            }
        }
    }
    // ���� �ִϸ��̼� �̺�Ʈ ȣ���
    void OnAttack()
    {
        Debug.Log($"[OnAttack] {gameObject.name} ���� �ִϸ��̼� �̺�Ʈ �����");
    }



    //// �ð� ���� �����ϰ� �;��µ�, �ð��� ���缭 �����ϱ� ����� �����ϰ� �� �Լ�(�̿ϼ�)
    //// �� ���κ��� ������� ���� �����ؼ� ���� �װ� �� ���� ǳ���ϰ� �ϰ� �;��� �ڵ��Դϴ�.
    ////
    //// private int laneIndex;         // ���� ������ ���� �ε���
    //// private float laneYPosition;   // ������ Y ��ġ

    //// ���� ������ ������ ����Ʈ (��� ���� ����)
    //   private static List<GameObject> stackedZombies = new List<GameObject>();
    // 
    //// �տ� �ִ� ���� ���� Ȯ���ϰ�, ���� �� �̻��̸� �����ϵ��� �ϴ� �Լ�
    //private void DetectFrontZombies()
    //{
    //    Vector2 origin = transform.position + Vector3.up * 0.5f;
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(origin, 1.5f, zombieLayer);

    //    int zombieCount = 0;
    //    ZombieMovement frontZombie = null;

    //    foreach (Collider2D collider in hitColliders)
    //    {
    //        ZombieMovement otherZombie = collider.GetComponent<ZombieMovement>();

    //        if (otherZombie != null && otherZombie.laneIndex == this.laneIndex)
    //        {
    //            zombieCount++;

    //            if (otherZombie.IsMoving() && frontZombie == null)
    //            {
    //                frontZombie = otherZombie; // ���� �տ� �ִ� ���� ã��
    //            }
    //        }
    //    }

    //    if (zombieCount >= 2 && frontZombie != null)
    //    {
    //        JumpOntoZombie(frontZombie); // �� ���� �Ӹ� ���� ����
    //    }
    //}


    //// �տ� �ִ� ���� ���� �����ϴ� �Լ�
    //private void JumpOntoZombie(ZombieMovement frontZombie)
    //{
    //    isJumping = true;
    //    Debug.Log("�� ���� �Ӹ� ���� ����!");

    //    float jumpHeight = 0.8f; // ���� �Ӹ� ���� ������ ����
    //    Vector3 jumpTarget = new Vector3(frontZombie.transform.position.x, frontZombie.transform.position.y + jumpHeight, transform.position.z);

    //    rb.velocity = new Vector2(0, jumpForce);
    //    Invoke("LandOnTarget", 0.5f);
    //}

    //// ���� �� ��ġ �����ϴ� �Լ�
    //private void LandOnTarget()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, zombieLayer);

    //    if (hit.collider != null)
    //    {
    //        ZombieMovement belowZombie = hit.collider.GetComponent<ZombieMovement>();

    //        if (belowZombie != null && belowZombie.laneIndex == this.laneIndex)
    //        {
    //            float stackedHeight = 0.5f;
    //            transform.position = new Vector3(transform.position.x, belowZombie.transform.position.y + stackedHeight, transform.position.z);

    //            // ���� �� ���� ���ÿ� �߰�
    //            StackZombie();
    //        }
    //    }

    //    isJumping = false;
    //    MoveLeft();
    //}

    //// ���� ���� ����Ʈ�� �߰��ϴ� �Լ�
    //private void StackZombie()
    //{
    //    if (!stackedZombies.Contains(this.gameObject))
    //    {
    //        stackedZombies.Add(this.gameObject);
    //    }

    //    int index = stackedZombies.IndexOf(this.gameObject);

    //    // �տ� �ִ� ����� ���� ���̵��� ����
    //    if (index > 0)
    //    {
    //        Vector3 belowZombiePos = stackedZombies[index - 1].transform.position;
    //        transform.position = new Vector3(belowZombiePos.x, belowZombiePos.y + 1.0f, belowZombiePos.z);
    //    }
    //}

    //// ������ ������ �����ϱ� ���� �Լ���
    //public void SetLane(int index, float yPos)
    //{
    //    laneIndex = index;
    //    laneYPosition = yPos;
    //    transform.position = new Vector3(transform.position.x, laneYPosition, transform.position.z);
    //    // ���� ���̾� �ڵ� ����
    //    zombieLayer = 1 << gameObject.layer;
    //}

    //public bool IsMoving()
    //{
    //    return rb.velocity.x < 0;
    //}

}
