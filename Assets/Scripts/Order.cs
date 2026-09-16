using System.Collections.Generic;
using System;
[Serializable]
public class Order
{
    public NPC customer;
    public List<ProductType> productList;
    public int table;
    public bool isCompleted;
    public bool isPaid;
    public float time;
    public int reputation;
    public bool isDelivered;

}
