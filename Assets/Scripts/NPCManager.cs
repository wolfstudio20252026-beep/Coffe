using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public PlayerMove player { get; private set; }
    [HideInInspector] public List<int> tables;
    public NPC CurrentNpc { get; private set; }
    public List<NPC> clients;
    public static NPCManager instance;
    public OrderManager orderManager;

    [Header("Panel")]
    public Transform buyZone;
    public GameObject panelTable;
    [SerializeField] GameObject panel;
    public Transform takeClientZone;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text ageText;
    [SerializeField] TMP_Text productText;


    private void Awake()
    {
        instance = this;

    }

    private void ShowPanelUI()
    {
        if (player != null && CurrentNpc != null)
        {
            panel.SetActive(true);
        }
        else
            panel.SetActive(false);
    }
    public void SetNPC(NPC npc)
    {
        CurrentNpc = npc;
        ShowPanelUI();
    }

    public void ShowPanel(List<ProductType> products)
    {
        nameText.text = CurrentNpc.name;
        ageText.text = CurrentNpc.age.ToString();
        string stringproduct = "";
        foreach (ProductType product in products)
            stringproduct = "Product: " + product;

        productText.text = stringproduct;
    }

    public void PlayerCass(PlayerMove player)
    {
        this.player = player;
        ShowPanelUI();
    }

    public void ClosePanel() => panel.SetActive(false);

    public int CreateOrder()
    {
        return orderManager.CreateOrder(CurrentNpc); 
    }
}
