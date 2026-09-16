using System.Collections;
using UnityEngine;
public class TakeProduct : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private DeliveryLine deliveryLine;

    private Transform heldObj = null;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(PickupRoutine());
    }

    IEnumerator PickupRoutine()
    {
        while (true)
        {

            if (Input.GetMouseButton(0))
            {
                if (heldObj == null)
                {
                    if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, rayDistance, layer))
                    {
                        Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            Product product = hit.transform.GetComponent<Product>();

                            if (product != null)
                            {
                                Order order = NPCManager.instance.orderManager.GetOrderForProduct(product.productType);
                                if (order != null)
                                    deliveryLine.SetTarget(order.customer.transform);
                            }
                            heldObj = hit.transform;
                            heldObj.SetParent(mainCamera.transform);
                            rb.isKinematic = true;
                        }
                    }
                }
            }
            else
            {
                if (heldObj != null)
                {
                    Rigidbody rb = heldObj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                    }

                    heldObj.SetParent(null);
                    heldObj = null;
                }
            }

            yield return null;
        }
    }
}
