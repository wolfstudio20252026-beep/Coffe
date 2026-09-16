using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    public List<ProductType> products;
    public int index;
    NPC client;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("NPC"))
        {
            client = other.GetComponent<NPC>();
            if (client != null)
            {
                foreach(ProductType product in client.product)
                {
                    products.Add(product);
                }
            }
        }
        else if (other.CompareTag("Products") && client != null)
        {
            Product product = other.GetComponent<Product>();

            if (product != null)
            {
                Debug.Log(product.productType);
                Destroy(other.gameObject);
                products.Remove(product.productType);
            }

            if (products.Count <= 0)
            {
                OrderManager.instance.RemoveOrder(index);
            }
        }

    }

}
