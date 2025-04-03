using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint; // 하나의 스폰 위치만 사용
    public float spawnInterval = 3f;
    private float timer = 0f;
    private float fixedYPosition = -3.0f; // 고정된 Y 좌표

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnZombie();
            timer = 0f;
        }
    }

    // 좀비 스폰용 함수
    void SpawnZombie()
    {
        GameObject spawnedZombie = Instantiate(zombiePrefab, new Vector3(spawnPoint.position.x, fixedYPosition, 0f), Quaternion.identity);
        Debug.Log($"좀비 생성됨: {spawnedZombie.name}");
    }


    //// 타워 데스티니 서바이브 처럼 위, 중간, 아래 3개의 레인에서 좀비출현을 위한 코드(좀비 점프 조건 구현 중 막힘이 있어서 보류)
    //// 각 레인에 생성되는 좀비는 그 레인의 레이어를 자동으로 갖게 한다.

    //private float[] laneYPositions = { -2.58f, -3.08f, -3.58f };

    //void Start()
    //{
    //    for (int i = 0; i < laneYPositions.Length; i++)
    //    {
    //        for (int j = i + 1; j < laneYPositions.Length; j++)
    //        {
    //            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ZombieLane" + (i + 1)),
    //                                           LayerMask.NameToLayer("ZombieLane" + (j + 1)), true);
    //        }
    //    }
    //}

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= spawnInterval)
    //    {
    //        SpawnZombie();
    //        timer = 0f;
    //    }
    //}

    //void SpawnZombie()
    //{
    //    int laneIndex = Random.Range(0, laneYPositions.Length);
    //    Transform spawnPoint = spawnPoints[laneIndex];
    //    GameObject spawnedZombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
    //    ZombieMovement zombie = spawnedZombie.GetComponent<ZombieMovement>();

    //    if (zombie != null)
    //    {
    //        zombie.SetLane(laneIndex, laneYPositions[laneIndex]);

    //        // 좀비의 Layer 설정
    //        int zombieLayer = LayerMask.NameToLayer("ZombieLane" + (laneIndex + 1));
    //        spawnedZombie.layer = zombieLayer;

    //        // ZombieMovement에서 Layer를 자동 설정할 수 있도록 SetLane 호출 후 다시 지정
    //        zombie.zombieLayer = 1 << zombieLayer;
    //    }
    //}
}
