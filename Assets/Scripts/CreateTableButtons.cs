using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTableButtons : MonoBehaviour
{
    [SerializeField] Transform PanelTable;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject buttonAddList;
    [SerializeField] Transform panelClients;
    [SerializeField] GameObject clientPrefab;

    public List<Transform> tablePoints;
    bool isOpenClientsPanel;

    private void Start()
    {
        for (int i = 0; i < tablePoints.Count; i++)
        {
            GameObject buttonGameObject = Instantiate(buttonPrefab, PanelTable);
            Button button = buttonGameObject.GetComponent<Button>();
            Transform point = tablePoints[i];

            button.onClick.AddListener( ()=> ButtonTable(point));
        }
    }

    public void OpenClientPanel()
    {
        isOpenClientsPanel = !isOpenClientsPanel;
        panelClients.gameObject.SetActive(isOpenClientsPanel);
    }

    private void ButtonTable(Transform point)
    {
        NPC client = NPCManager.instance.CurrentNpc;

        buttonAddList.SetActive(true);

        Button button = buttonAddList.GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => AddInList(point));
    }

    private void AddInList(Transform point)
    {
        NPC client = NPCManager.instance.CurrentNpc;
        client.GoToTable(point);
        NPCQueueManager queue = FindObjectOfType<NPCQueueManager>();
        queue.RemoveNPC(client);
        point.GetComponent<Table>().index = NPCManager.instance.CreateOrder() - 1;

        Order order = NPCManager.instance.orderManager.GetOrderForProduct(client.product[0]);
        GameObject clientBlock = Instantiate(clientPrefab, panelClients);

        clientBlock.GetComponent<Client>().SetClient(client.name, client.product, client.tableNumber, order);

        NPCManager.instance.ClosePanel();
    }
}
