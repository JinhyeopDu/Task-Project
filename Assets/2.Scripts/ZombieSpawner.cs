using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint; // �ϳ��� ���� ��ġ�� ���
    public float spawnInterval = 3f;
    private float timer = 0f;
    private float fixedYPosition = -3.0f; // ������ Y ��ǥ

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnZombie();
            timer = 0f;
        }
    }

    // ���� ������ �Լ�
    void SpawnZombie()
    {
        GameObject spawnedZombie = Instantiate(zombiePrefab, new Vector3(spawnPoint.position.x, fixedYPosition, 0f), Quaternion.identity);
        Debug.Log($"���� ������: {spawnedZombie.name}");
    }


    //// Ÿ�� ����Ƽ�� �����̺� ó�� ��, �߰�, �Ʒ� 3���� ���ο��� ���������� ���� �ڵ�(���� ���� ���� ���� �� ������ �־ ����)
    //// �� ���ο� �����Ǵ� ����� �� ������ ���̾ �ڵ����� ���� �Ѵ�.

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

    //        // ������ Layer ����
    //        int zombieLayer = LayerMask.NameToLayer("ZombieLane" + (laneIndex + 1));
    //        spawnedZombie.layer = zombieLayer;

    //        // ZombieMovement���� Layer�� �ڵ� ������ �� �ֵ��� SetLane ȣ�� �� �ٽ� ����
    //        zombie.zombieLayer = 1 << zombieLayer;
    //    }
    //}
}
