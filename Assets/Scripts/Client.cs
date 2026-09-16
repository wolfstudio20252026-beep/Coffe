using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textproduct;
    [SerializeField] private TMP_Text textTable;
    [SerializeField] private TMP_Text textTime;

    private Order order;

    public void SetClient(string name, List<ProductType> products, int table, Order newOrder)
    {
        order = newOrder;
        textName.text = name;

        foreach (ProductType product in products)
        {
            textproduct.text = product.ToString();
        }

        textTable.text = table.ToString();
    }

    private void Update()
    {
        if (order == null)
            return;

        textTime.text = Mathf.CeilToInt(order.time).ToString();
    }
}