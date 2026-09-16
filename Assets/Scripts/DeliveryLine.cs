using UnityEngine;

public class DeliveryLine : MonoBehaviour
{
    [SerializeField] private Transform player;
    LineRenderer line;

    private Transform target;

    private void Start() => line = GetComponent<LineRenderer>();

    private void Update()
    {
        if (target == null)
        {
            line.enabled = false;
            return;
        }

        line.enabled = true;

        line.SetPosition(0, player.position + Vector3.up * 0.1f);
        line.SetPosition(1, target.position + Vector3.up * 0.1f);
    }

    public void SetTarget(Transform targetNPC)
    {
        target = targetNPC;
    }

    public void ClearTarget()
    {
        target = null;
        line.enabled = false;
    }
}