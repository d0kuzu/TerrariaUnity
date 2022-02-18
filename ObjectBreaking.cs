using UnityEngine;
using System.Collections.Generic;

public class ObjectBreaking : MonoBehaviour
{
    [SerializeField] List<GameObject> Blocks = new List<GameObject>();
    [SerializeField] List<float> BlocksHealth = new List<float>();
    [SerializeField] List<GameObject> BrokenBlock = new List<GameObject>();
    [SerializeField] List<float> BrokenBlockHealth = new List<float>();
    [SerializeField] List<float> BlockHeal = new List<float>();
    private bool autoAiming;
    private Transform playerT;
    [SerializeField] private Camera cam;
    private float respite;
    private float respite1;
    [SerializeField] private GameObject selectedBlock = null;
    private AutoAim aim;
    private Vector2 MousePos;
    private Vector2 vector2;
    private Rigidbody2D rb;
    public GameObject tool;
    public float toolRateOfFire;
    public float toolDamage;
    private bool isPassed;

    private void Start()
    {
        playerT = GameObject.Find("Player").transform;
        aim = GameObject.Find("AutoAim(Orig)").GetComponent<AutoAim>();
    }
    private void Update()
    {
        this.transform.position = playerT.position;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            autoAiming = !autoAiming;
        } 
        if (tool == null || !autoAiming)
        {            
            BlockSelect(null, false);
        }
        else if(autoAiming)
        {
            respite += Time.deltaTime;
            if (respite >= 0.25 && tool != null)
            {
                respite = 0;
                aim.Aiming();
            }
            rb = this.GetComponent<Rigidbody2D>();
            MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            vector2 = MousePos - rb.position;
            rb.rotation = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg - 90f;
        }
        if(tool != null && respite1 < toolRateOfFire)
        {
            respite1 += Time.deltaTime;
        }
        if (Input.GetMouseButton(0))
        {
            if(tool != null && respite1 >= toolRateOfFire && selectedBlock != null)
            {
                BlockBreaking(selectedBlock);
                respite1 = 0;
            }
        }
        if (selectedBlock != null)
        {
            if (selectedBlock.transform.position.x < playerT.position.x - 5 || selectedBlock.transform.position.x > playerT.position.x + 5 || selectedBlock.transform.position.y < playerT.position.y - 5 || selectedBlock.transform.position.y > playerT.position.y + 5)
            {
                selectedBlock.GetComponent<SpriteRenderer>().color = Color.white;
                selectedBlock = null;
            }
        }        
    }
    public void BlockSelect(GameObject go, bool isCS)
    {
        if (isCS)
        {
            Debug.Log("CS");
            selectedBlock = go;
            Debug.Log(selectedBlock);
        }
        else
        {
            if (!autoAiming && selectedBlock != null || autoAiming && go == null)
            {
                selectedBlock.GetComponent<SpriteRenderer>().color = Color.white;
                selectedBlock = go;
            }
            else if (autoAiming && selectedBlock == null && go != null)
            {
                selectedBlock = go;
                selectedBlock.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (autoAiming && selectedBlock != null && go != null)
            {
                selectedBlock.GetComponent<SpriteRenderer>().color = Color.white;
                selectedBlock = go;
                selectedBlock.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }
    private void BlockBreaking(GameObject sb)
    {
        for (int i = 0; i < BrokenBlock.Count; i++)
        {
            if(sb == BrokenBlock[i])
            {
                isPassed = true;
                BrokenBlockHealth[i] -= toolDamage;
                if(BrokenBlockHealth[i] <= 0)
                {
                    Destroy(sb);
                    BlockSelect(null, false);
                    BrokenBlock.RemoveAt(i);
                    BrokenBlockHealth.RemoveAt(i);
                    BlockHeal.RemoveAt(i);
                }
                break;
            }
        }
        if (!isPassed)
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (sb.name == Blocks[i].name)
                {
                    if (BlocksHealth[i] - toolDamage <= 0)
                    {
                        Destroy(sb);
                        BlockSelect(null, false);
                    }
                    else
                    {
                        BrokenBlock.Add(sb);
                        BrokenBlockHealth.Add(BlocksHealth[i] - toolDamage);
                        BlockHeal.Add(0);
                    }
                    break;
                }
            }
        }
        isPassed = false;
    }
}