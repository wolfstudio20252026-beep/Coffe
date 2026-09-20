using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[Serializable]
public class ProductEntry
{
    public ProductType productType;
    public GameObject productsPrefab;

}
public class ManagerAllProducts : MonoBehaviour
{
    [SerializeField] Button productButtonPrefab;
    [SerializeField] Transform allProductPanel;
    [SerializeField] Transform SpawnProduct;
    [SerializeField] List<ProductEntry> products;
    Dictionary<ProductType, GameObject> productsPrefabs;

    private void Awake()
    {
        productsPrefabs = new Dictionary<ProductType, GameObject>();
        foreach (ProductEntry product in products)
        {
            productsPrefabs.Add(product.productType, product.productsPrefab);
        }
    }

    void Start()
    {
        for (int i = 0; i < Enum.GetValues(typeof(ProductType)).Length; i++)
        {
            Button productButton = Instantiate(productButtonPrefab, allProductPanel);
            //Debug.Log(productButton.GetComponentInChildren<TMP_Text>());
            productButton.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{(ProductType)i}";
            int numberProduct = i;

            productButton.onClick.AddListener(() => Cooke(numberProduct));
        }
    }

    private void Cooke(int i)
    {
        ProductType product = (ProductType)i;
        TimeManager.instance.countProducts++;
        Instantiate(productsPrefabs[product], SpawnProduct.position, Quaternion.identity);
    }
}
