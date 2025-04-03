using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 3f;        // 이동속도
    public float jumpForce = 5f;        // 점프하기 위한 힘
    public float jumpForwardSpeed = 2f; // 점프 시 앞으로 나아가는 속도
    public LayerMask zombieLayer;       // 좀비레이어 체크용
    public LayerMask groundLayer;       // 바닥레이어 체크용

    private Rigidbody2D rb;             // 리지디바디2D
    private Animator animator;          // 애니메이터
    private bool isJumping = false;     // 점프 체크
    private bool isAttacking = false;   // 공격 체크
    private bool isClimbing = false;    // 등산 체크
    private bool isFalling = false;     // 낙하 체크

    private static int attackingZombies = 0;    // 공격중인 좀비 체크용

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
        CheckZombiesInFront();  // 앞에 좀비 확인
        CheckIfFalling();       // 낙하 확인
    }

    // 좀비 이동 관련 함수
    private void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        animator?.SetBool("IsIdle", false);
        animator?.SetBool("IsAttacking", false);
    }

    // 앞에 좀비 수를 체크하고, 일정 수량 이상이면 점프시키기 위한 함수
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

        if (count >= 3 && highestZombie != null) // 일정 개수 이상이면 점프
        {
            StartCoroutine(TryClimb(highestZombie));
        }
    }

    // 일정 갯수 이상의 좀비가 있으면 위로 올라가게 하려는 함수
    private IEnumerator TryClimb(Transform targetZombie)
    {
        isClimbing = true;
        rb.gravityScale = 1.2f; // 점프할 때 중력 활성화
        rb.velocity = new Vector2(0, jumpForce);
        yield return new WaitForSeconds(0.5f);

        if (targetZombie == null) // 혹시 삭제된 경우 방어 코드
        {
            isClimbing = false;
            yield break;
        }

        float newYPosition = targetZombie.position.y + 1.0f;
        rb.gravityScale = 0; // 착지 후 중력 제거
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        isClimbing = false;
    }

    // 좀비가 내려가도록 설정 및 완전히 떨어지지 않게 막기 위한 용도
    private void CheckIfFalling()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, zombieLayer);
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        if (hit.collider == null && !isGrounded) // 밑에 좀비도 없고, 땅도 아니면
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        if (isFalling)
        {
            transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0); // 천천히 내려감
        }
    }

    // 플레이어 및 박스 충돌시 공격수행 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero; // 공격 중 멈추기
                animator.SetBool("IsAttacking", true);
                attackingZombies++;
            }
        }
    }
    // 공격 애니메이션 이벤트 호출용
    void OnAttack()
    {
        Debug.Log($"[OnAttack] {gameObject.name} 공격 애니메이션 이벤트 실행됨");
    }



    //// 시간 내에 구현하고 싶었는데, 시간에 맞춰서 진행하기 어려워 보류하게 된 함수(미완성)
    //// 각 레인별로 좀비들을 출현 점프해서 좀비를 쌓게 해 좀더 풍성하게 하고 싶었던 코드입니다.
    ////
    //// private int laneIndex;         // 현재 좀비의 레인 인덱스
    //// private float laneYPosition;   // 레인의 Y 위치

    //// 좀비 스택을 관리할 리스트 (모든 좀비가 공유)
    //   private static List<GameObject> stackedZombies = new List<GameObject>();
    // 
    //// 앞에 있는 좀비 수를 확인하고, 일정 수 이상이면 점프하도록 하는 함수
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
    //                frontZombie = otherZombie; // 가장 앞에 있는 좀비 찾기
    //            }
    //        }
    //    }

    //    if (zombieCount >= 2 && frontZombie != null)
    //    {
    //        JumpOntoZombie(frontZombie); // 앞 좀비 머리 위로 점프
    //    }
    //}


    //// 앞에 있는 좀비 위로 점프하는 함수
    //private void JumpOntoZombie(ZombieMovement frontZombie)
    //{
    //    isJumping = true;
    //    Debug.Log("앞 좀비 머리 위로 점프!");

    //    float jumpHeight = 0.8f; // 좀비 머리 위로 착지할 높이
    //    Vector3 jumpTarget = new Vector3(frontZombie.transform.position.x, frontZombie.transform.position.y + jumpHeight, transform.position.z);

    //    rb.velocity = new Vector2(0, jumpForce);
    //    Invoke("LandOnTarget", 0.5f);
    //}

    //// 착지 후 위치 조정하는 함수
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

    //            // 점프 후 좀비 스택에 추가
    //            StackZombie();
    //        }
    //    }

    //    isJumping = false;
    //    MoveLeft();
    //}

    //// 좀비를 스택 리스트에 추가하는 함수
    //private void StackZombie()
    //{
    //    if (!stackedZombies.Contains(this.gameObject))
    //    {
    //        stackedZombies.Add(this.gameObject);
    //    }

    //    int index = stackedZombies.IndexOf(this.gameObject);

    //    // 앞에 있는 좀비들 위에 쌓이도록 설정
    //    if (index > 0)
    //    {
    //        Vector3 belowZombiePos = stackedZombies[index - 1].transform.position;
    //        transform.position = new Vector3(belowZombiePos.x, belowZombiePos.y + 1.0f, belowZombiePos.z);
    //    }
    //}

    //// 좀비의 레인을 설정하기 위한 함수용
    //public void SetLane(int index, float yPos)
    //{
    //    laneIndex = index;
    //    laneYPosition = yPos;
    //    transform.position = new Vector3(transform.position.x, laneYPosition, transform.position.z);
    //    // 좀비 레이어 자동 설정
    //    zombieLayer = 1 << gameObject.layer;
    //}

    //public bool IsMoving()
    //{
    //    return rb.velocity.x < 0;
    //}

}
