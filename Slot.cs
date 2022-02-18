using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image a;
    private Inventory inventory;
    private bool isInventoryOpen;
    private string b;
    private int c;
    private bool d;
    private GameObject onMouseObject;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public void Obmen()
    {
        if (isInventoryOpen)
        {
            inventory.OnMouse(image, a, onMouseObject, b, c, d);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInventoryOpen = !isInventoryOpen;
        }
    }
}
