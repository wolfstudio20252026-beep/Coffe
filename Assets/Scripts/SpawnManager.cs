using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] NPCPrefabs;
    [SerializeField] private NPCQueueManager npcQueue;
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnNPCRoutine());
    }

    private IEnumerator SpawnNPCRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(5f, 15f);

            yield return new WaitForSeconds(waitTime);

            NPC npc = NPCPrefabs[Random.Range(0, NPCPrefabs.Length)].GetComponent<NPC>();

            GameObject newNpc = Instantiate(
                npc.gameObject,
                spawnPoint.position,
                Quaternion.identity
            );
        }

    }
}
