using UnityEngine;

public class LyingItem : MonoBehaviour
{
    private float respite;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag != "Bullet" && collision.gameObject.tag == "Player" && this.name != "Shooten Object")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (this.tag == "Bullet" && collision.gameObject.tag == "Player" && this.name != "Shooten Object")
        {
            this.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" && this.name != "Shooten Object")
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<Rigidbody2D>().position += new Vector2(0, 0.1f);
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.tag != "Bullet" && collision.gameObject.tag == "Player" && this.name != "Shooten Object")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if(this.tag == "Bullet" && collision.gameObject.tag == "Player" && this.name != "Shooten Object")
        {
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (this.tag != "Bullet" && this.name == "Shooten Object" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "GameController")
        {
            Destroy(this.gameObject);
        }
        else if (this.tag == "Bullet" && this.name == "Shooten Object" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "GameController")
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(this.name == "Shooten Object")
        {
            if(respite < 5)
            {
                respite += Time.deltaTime;
            }
            else
            {
                respite = 0;
                Destroy(this.gameObject);
            }
            if(this.tag == "Bullet")
            {
                this.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
    }
}
