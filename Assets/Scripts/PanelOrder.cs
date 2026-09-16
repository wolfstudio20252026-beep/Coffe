using UnityEngine;
using UnityEngine.UI;
public class PanelOrder : MonoBehaviour
{
    public Button addInListButton;

    void Start()
    {
        addInListButton.onClick.AddListener(() => NPCManager.instance.CreateOrder());
    }
}
