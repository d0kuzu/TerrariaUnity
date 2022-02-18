using UnityEngine.UI;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image image;
    [SerializeField] private Image a;
    [SerializeField] private Sprite selectBar;
    [SerializeField] private GameObject slot;
    private Weapon weapon;
    private InArmsItem inArms;
    private ObjectBreaking ob;
    private Sprite defoultSprite;
    private Sprite empty;
    private GameObject onMouseObject;
    [SerializeField] private GameObject Slots;
    private int activeBar = 1;
    private bool isInventoryOpen;
    private string b;
    private int c;
    private bool d;

    private void Start()
    {
        defoultSprite = slot.GetComponent<Image>().sprite;
        empty = image.sprite;
        inventory.SpawnStartItems();
        weapon = GameObject.Find("Player").GetComponent<Weapon>();
        inArms = GameObject.Find("Player").GetComponent<InArmsItem>();
        ob = GameObject.Find("ObjectBreaker").GetComponent<ObjectBreaking>();
    }
    public void Obmen()
    {
        if (isInventoryOpen)
        {
            inventory.OnMouse(image, a, onMouseObject, b, c, d);
        }
    }
    private void Update()
    {
        if (!isInventoryOpen)
        {
            for (int i = 0; i < 10; i++)
            {
                int x = i + 1;
                if (x == 10)
                {
                    x = 0;
                }
                if (activeBar == x && inventory.slots[i].tag == image.tag)
                {
                    slot.GetComponent<Image>().sprite = selectBar;
                    slot.transform.localScale = new Vector3(1.3f, 1.3f);
                    image.GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                }
                if (activeBar != x && inventory.slots[i].tag == image.tag)
                {
                    slot.GetComponent<Image>().sprite = defoultSprite;
                    slot.transform.localScale = new Vector3(1, 1);
                    image.GetComponentInChildren<Text>().color = new Color(255, 255, 255);
                }
            }
        }
        else
        {
            slot.GetComponent<Image>().sprite = defoultSprite;
            slot.transform.localScale = new Vector3(1, 1);
            image.GetComponentInChildren<Text>().color = new Color(255, 255, 255);
        }
        if (!isInventoryOpen && inventory.onMouseImage.sprite != empty)
        {
            for (int i = 0; i < 10; i++)
            {
                int x = i + 1;
                if (x == 10)
                {
                    x = 0;
                }
                if (activeBar == x && inventory.slots[i].tag == image.tag)
                {
                    inventory.Throwing(true, -1, isInventoryOpen);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            inventory.Throwing(false, activeBar, isInventoryOpen);
        }
        InArmCheck();
        if (isInventoryOpen)
        {
            Slots.transform.localPosition= new Vector3(-261, 142.25f);
        }
        else
        {
            Slots.transform.localPosition = new Vector3(-665, 142.25f);
        }
    }
    public void InArmCheck()
    {
        for (int i = 0; i < 10; i++)
        {
            int x = i + 1;
            if (x == 10)
            {
                x = 0;
            }
            if (activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Bow" || activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Gun" || activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Magical" || activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Infighting" || activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Pickaxe" || activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Axe" || activeBar == x && inventory.isFull[i] && inventory.inventory[i].tag == "Hammer")
            {
                weapon.weapon = inventory.inventory[i];
                inArms.itemInArms = inventory.inventory[i];
                weapon.isWeaponActive = true;
                if(inventory.inventory[i].tag == "Pickaxe" || inventory.inventory[i].tag == "Axe" || inventory.inventory[i].tag == "Hammer")
                {
                    ob.tool = inventory.inventory[i];
                }
                else
                {
                    ob.tool = null;
                }
                break;
            }
            else
            {
                weapon.weapon = null;
                weapon.isWeaponActive = false;
                inArms.itemInArms = inventory.inventory[i];
                ob.tool = null;
            }
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            activeBar = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            activeBar = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            activeBar = 3;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            activeBar = 4;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            activeBar = 5;
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            activeBar = 6;
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            activeBar = 7;
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            activeBar = 8;
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            activeBar = 9;
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            activeBar = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInventoryOpen = !isInventoryOpen;
        }
    }
}