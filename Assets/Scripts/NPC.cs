using UnityEngine;
using System.Collections.Generic;

public enum ProductType
{
    Coffee,
    Donut,
    Lemonade,
    Cake
}

public class NPC : MonoBehaviour
{
    public string name;
    public int age;

    public float speed = 5f;

    public int tableNumber { get; private set; }
    public List<ProductType> product = new List<ProductType>();

    private bool isBuyTable = false;
    private bool isGoingToTable = false;

    private Transform targetTable;
    private Transform queuePoint;

    NPCManager manager;
    private NPCQueueManager npcQueue;

    private void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<NPCManager>();
        npcQueue = FindObjectOfType<NPCQueueManager>();
    }

    private void Start()
    {
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            product.Add((ProductType)Random.Range(0, 4));
        }
        npcQueue.AddNPC(this);
    }

    public void SetQueuePoint(Transform point)
    {
        queuePoint = point;
    }

    private void Update()
    {
        if (isGoingToTable)
        {
            MoveToTable();
            return;
        }

        if (!isBuyTable)
        {
            if (npcQueue != null && npcQueue.IsFirst(this))
            {
                MoveToBuyZone();
            }
            else
            {
                MoveToQueuePoint();
            }
        }
    }

    private void MoveToQueuePoint()
    {
        if (queuePoint == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            queuePoint.position,
            speed * Time.deltaTime
        );
    }

    private void MoveToBuyZone()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            manager.buyZone.position,
            speed * Time.deltaTime
        );
    }

    private void MoveToTable()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetTable.position,
            speed * Time.deltaTime
        );


        if (Vector3.Distance(transform.position, targetTable.position) < 0.1f) 
        {
            TimeManager.instance.AddClient(this);
            isGoingToTable = false;
        }
    }

    public void GoToTable(Transform table)
    {
        targetTable = table;
        isGoingToTable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuyZone"))
        {
                isBuyTable = true;

                NPCManager.instance.panelTable.SetActive(false);
                NPCManager.instance.SetNPC(this);

                NPCManager.instance.ShowPanel(product);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuyZone"))
            NPCManager.instance.SetNPC(null);
    }
}