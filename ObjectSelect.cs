using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    private Transform trans;
    [SerializeField] private Camera cam;
    private Transform playerT;
    private ObjectBreaking ob;

    void Start()
    {
        trans = GameObject.Find("ObjectSelect").transform;
        playerT = GameObject.Find("Player").transform;
        ob = GameObject.Find("ObjectBreaker").GetComponent<ObjectBreaking>();
    }
    void Update()
    {
        trans.position = Vector3.MoveTowards(trans.position, cam.ScreenToWorldPoint(Input.mousePosition), 500 * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            if (trans.transform.position.x < playerT.position.x - 5 || trans.transform.position.x > playerT.position.x + 5 || trans.transform.position.y < playerT.position.y - 5 || trans.transform.position.y > playerT.position.y + 5)
            {
            }
            else
            {
                Debug.Log("gg");
                ob.BlockSelect(collision.gameObject, true);
            }
        }
    }
}
