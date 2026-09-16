using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<Order> orders = new List<Order>();
    public static OrderManager instance;

    private void Awake()
    {
        instance = this;
    }

    public int CreateOrder(NPC customer)
    {
        if (customer == null)
        {
            return -1;
        }

        if (customer.product == null)
        {
            return -1;
        }

        Order order = new Order();

        order.customer = customer;
        order.time = 60f;

        order.productList = new List<ProductType>();

        foreach (ProductType product in customer.product)
        {
            order.productList.Add(product);
        }

        orders.Add(order);
        return orders.Count;
    }

    public void RemoveOrder(int index)
    {
        orders.RemoveAt(index);
    }

    private void Update()
    {
        foreach (Order order in orders)
        {

            if (order.isDelivered)
                continue;

            if (order.time > 0)
            {
                order.time -= Time.deltaTime;
            }

            if (order.time <= 0)
            {
                order.time = 0;
            }
        }

    }

    public Order GetOrderForProduct(ProductType product)
    {
        foreach (Order order in orders)
        {
            if (order.isDelivered)
                continue;

            if (order.productList.Contains(product))
            {
                return order;
            }
        }

        return null;
    }
}