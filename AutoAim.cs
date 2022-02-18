using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private Transform playerT;
    private ObjectBreaking ob;
    private GameObject origAutoAim;
    private GameObject objectBreaker;

    private void Start()
    {
        playerT = GameObject.Find("Player").transform;
        ob = GameObject.Find("ObjectBreaker").GetComponent<ObjectBreaking>();
        origAutoAim = GameObject.Find("AutoAim(Orig)");
        objectBreaker = GameObject.Find("ObjectBreaker");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" && this.name == "AutoAim")
        {
            Destroy(this.gameObject);
            ob.BlockSelect(collision.gameObject, false);
        }        
    }
    private void Update()
    {        
        if (this.name == "AutoAim")
        {
            if (this.transform.position.x < playerT.position.x - 5 || this.transform.position.x > playerT.position.x + 5 || this.transform.position.y < playerT.position.y - 5 || this.transform.position.y > playerT.position.y + 5)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            origAutoAim.transform.position = playerT.position;
        }
    }
    public void Aiming()
    {
        GameObject go = Instantiate(origAutoAim, playerT.position, new Quaternion());
        go.GetComponent<Rigidbody2D>().AddForce(objectBreaker.transform.up * 50, ForceMode2D.Impulse);
        go.name = "AutoAim";
    }
}