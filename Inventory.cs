using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] public List<bool> isFull = new List<bool>();
    [SerializeField] private List<bool> isStackFull999 = new List<bool>();
    [SerializeField] private List<bool> isStackFull99 = new List<bool>();
    [SerializeField] public List<Image> slots = new List<Image>();
    [SerializeField] private List<int> count = new List<int>();
    [SerializeField] public Image onMouseImage;
    [SerializeField] private Hotbar hotbar;
    private string onMouseName;
    private int onMouseCount;
    private bool onMouse999;
    private bool onMouse99;
    private bool onMouseIsFull;
    private GameObject onMouseObject;
    private Sprite empty;
    private List<GameObject> thrownItemsObjects = new List<GameObject>();
    private List<int> thrownItemsCount = new List<int>();
    private float throwDirection = 0;

    private void Start()
    {
        empty = onMouseImage.sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Infighting" || collision.gameObject.tag == "Bow" || collision.gameObject.tag == "Gun" || collision.gameObject.tag == "Magical" || collision.gameObject.tag == "Pickaxe" || collision.gameObject.tag == "Axe" || collision.gameObject.tag == "Hammer")
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!isFull[i])
                {
                    bool thrown = false;
                    slots[i].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                    slots[i].name = collision.gameObject.name;
                    for (int j = 0; j < thrownItemsObjects.Count; j++)
                    {
                        if (thrownItemsObjects[j] == collision.gameObject)
                        {
                            count[i] = thrownItemsCount[j];
                            thrownItemsObjects.Remove(thrownItemsObjects[j]);
                            thrownItemsCount.Remove(thrownItemsCount[j]);
                            thrown = true;
                            break;
                        }
                    }
                    if (!thrown)
                    {
                        count[i] += 1;
                    }
                    isFull[i] = !isFull[i];
                    for (int j = 0; j < items.Count; j++)
                    {
                        if (items[j].name == collision.gameObject.name)
                        {
                            inventory[i] = items[j];
                            break;
                        }
                    }
                    Destroy(collision.gameObject);
                    break;
                }
            }
        }
        if (collision.gameObject.tag == "999Stack" || collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "Bullet")
        {
            bool canStack = false;
            bool thrown = false;
            for (int i = 0; i < slots.Count; i++)
            {
                if (isFull[i] && inventory[i].name == collision.gameObject.name && !isStackFull999[i])
                {
                    canStack = true;
                    if (count[i] != 998)
                    {
                        count[i] += 1;
                        slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        count[i] += 1;
                        slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                        isStackFull999[i] = true;
                        Destroy(collision.gameObject);
                    }                    
                    break;
                }
            }            
            if (canStack == false)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (!isFull[i])
                    {
                        for (int j = 0; j < thrownItemsObjects.Count; j++)
                        {
                            if (thrownItemsObjects[j] == collision.gameObject)
                            {
                                count[i] = thrownItemsCount[j];
                                thrownItemsObjects.Remove(thrownItemsObjects[j]);
                                thrownItemsCount.Remove(thrownItemsCount[j]);
                                thrown = true;
                                break;
                            }
                        }
                        slots[i].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                        slots[i].name = collision.gameObject.name;
                        if (!thrown)
                        {
                            count[i] += 1;
                        }
                        slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                        isFull[i] = !isFull[i];
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[j].name == collision.gameObject.name)
                            {
                                inventory[i] = items[j];
                                break;
                            }
                        }
                        Destroy(collision.gameObject);
                        break;
                    }
                }
            }
        }
        if (collision.gameObject.tag == "99Stack")
        {
            bool canStack = false;
            bool thrown = false;
            for (int i = 0; i < slots.Count; i++)
            {
                if (isFull[i] && inventory[i].name == collision.gameObject.name && !isStackFull99[i])
                {
                    canStack = true;
                    if (count[i] != 98)
                    {
                        count[i] += 1;
                        slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        count[i] += 1;
                        slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                        isStackFull999[i] = true;
                        Destroy(collision.gameObject);
                    }
                    break;
                }
            }
            if (canStack == false)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (!isFull[i])
                    {
                        for (int j = 0; j < thrownItemsObjects.Count; j++)
                        {
                            if (thrownItemsObjects[j] == collision.gameObject)
                            {
                                count[i] = thrownItemsCount[j];
                                thrownItemsObjects.Remove(thrownItemsObjects[j]);
                                thrownItemsCount.Remove(thrownItemsCount[j]);
                                thrown = true;
                                break;
                            }
                        }
                        slots[i].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                        slots[i].name = collision.gameObject.name;
                        if (!thrown)
                        {
                            count[i] += 1;
                        }
                        slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                        isFull[i] = !isFull[i];
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[j].name == collision.gameObject.name)
                            {
                                inventory[i] = items[j];
                                break;
                            }
                        }
                        Destroy(collision.gameObject);
                        break;
                    }
                }
            }
        }
    }
    public void OnMouse(Image image, Image a, GameObject gameObject, string b, int c, bool d)
    {       
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name == image.name)
            {
                a.sprite = onMouseImage.sprite;
                onMouseImage.sprite = image.sprite;
                image.sprite = a.sprite;
                d = onMouseIsFull;
                onMouseIsFull = isFull[i];
                isFull[i] = d;
                gameObject = onMouseObject;
                onMouseObject = inventory[i];
                inventory[i] = gameObject;
                c = onMouseCount;
                onMouseCount = count[i];
                count[i] = c;
                for (int j = 0; j < items.Count; j++)
                {
                    if (items[j].name == slots[i].name && items[j].tag == "999Stack" && isStackFull999[i] || items[j].name == slots[i].name && items[j].tag == "999Stack" && onMouse999)
                    {
                        isStackFull999[i] = !isStackFull999[i];
                        onMouse999 = !onMouse999;
                        break;
                    }
                }
                for (int j = 0; j < items.Count; j++)
                {
                    if (items[j].name == slots[i].name && items[j].tag == "99Stack" && isStackFull99[i] || items[j].name == slots[i].name && items[j].tag == "99Stack" && onMouse99)
                    {
                        isStackFull99[i] = !isStackFull99[i];
                        onMouse99 = !onMouse99;
                        break;
                    }
                }
                a.GetComponentInChildren<Text>().text = onMouseImage.GetComponentInChildren<Text>().text;
                onMouseImage.GetComponentInChildren<Text>().text = slots[i].GetComponentInChildren<Text>().text;
                slots[i].GetComponentInChildren<Text>().text = a.GetComponentInChildren<Text>().text;
                b = onMouseName;
                onMouseName = slots[i].name;
                for (int j = 0; j < 10; j++)
                {
                    if (slots[i].name == j.ToString())
                    {
                        onMouseName = null;
                    }
                }
                if (b == null)
                {
                    int x = i + 1;
                    if (x != 10)
                    {
                        slots[i].name = x.ToString();
                    }
                    else
                    {
                        slots[i].name = i.ToString();
                    }
                }
                else
                {
                    slots[i].name = b;
                }
                break;
            }
        }
    }
    public void Throwing(bool onMouse, int activeBar, bool isInventoryOpen)
    {
        if (onMouse)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == onMouseName)
                {
                    GameObject go = Instantiate(items[i], GameObject.FindGameObjectWithTag("Player").transform.position, new Quaternion());
                    go.name = items[i].name;
                    thrownItemsObjects.Add(go);
                    thrownItemsCount.Add(onMouseCount);
                    onMouseImage.GetComponentInChildren<Text>().text = null;
                    onMouse99 = false;
                    onMouse999 = false;
                    onMouseCount = 0;
                    onMouseImage.sprite = empty;
                    onMouseName = null;
                    onMouseObject = null;
                    onMouseIsFull = false;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                int x = i + 1;
                bool break_ = false; 
                if (x == 10)
                {
                    x = 0;
                }
                for (int j = 0; j < items.Count; j++)
                {
                    if (items[j].name == slots[i].name && activeBar == x && !isInventoryOpen)
                    {
                        GameObject go = Instantiate(items[j], GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(1 * throwDirection, 0), new Quaternion());
                        go.name = items[j].name;
                        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * throwDirection, -40));
                        thrownItemsObjects.Add(go);
                        thrownItemsCount.Add(count[i]);
                        count[i] = 0;
                        inventory[i] = null;
                        isFull[i] = false;
                        isStackFull99[i] = false;
                        isStackFull999[i] = false;
                        slots[i].name = x.ToString();
                        slots[i].sprite = empty;
                        break_ = !break_;
                        slots[i].GetComponentInChildren<Text>().text = "";
                        break;
                    }
                }
                if (break_)
                {
                    break;
                }
            }
        }
    }
    private void Update()
    {
        onMouseImage.transform.position = Input.mousePosition - new Vector3(20, 35, 0);
        throwDirection = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
    }
    public void AmmoSpend(string tag)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(isFull[i] && inventory[i].tag == tag)
            {
                if (count[i] == 999)
                {
                    isStackFull999[i] = false;
                    count[i]--;
                    slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                }
                else if (count[i] > 1)
                {
                    count[i]--;
                    slots[i].GetComponentInChildren<Text>().text = count[i].ToString();
                }
                else
                {
                    count[i] = 0;
                    inventory[i] = null;
                    isFull[i] = false;
                    int x = i + 1;
                    if (x == 10)
                    {
                        x = 0;
                    }
                    slots[i].name = x.ToString();
                    slots[i].sprite = empty;
                    slots[i].GetComponentInChildren<Text>().text = "";
                }
                break;
            }
        }
    }
    public void SpawnStartItems()
    { 
        
    }
}