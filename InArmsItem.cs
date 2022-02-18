using UnityEngine;

public class InArmsItem : MonoBehaviour
{
    public GameObject itemInArms;
    private Vector2 MousePos;
    private Vector2 vector2;
    private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    private Animator armAnimator;
    private GameObject usingItem;
    private GameObject player;
    private Moving moving;
    private float respite;
    private float rateOfFire;
    private GameObject rotationAxis;

    private void Start()
    {
        player = GameObject.Find("Player");
        moving = GameObject.Find("Player").GetComponent<Moving>();
        usingItem = GameObject.Find("UsingItem");
        armAnimator = GameObject.Find("Arms").GetComponent<Animator>();
        rotationAxis = GameObject.Find("RotationAxis");
        rb = rotationAxis.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (itemInArms != null && itemInArms.tag != "Pickaxe" && itemInArms.tag != "Axe" && itemInArms.tag != "Hammer")
        {
            if (respite < 0.5)
            {
                respite += Time.deltaTime;
            }
            else
            {
                usingItem.GetComponent<SpriteRenderer>().sprite = null;
                moving.weaponUsing = false;
                armAnimator.SetBool("isShooting", moving.weaponUsing);
            }
        }
        else if(itemInArms != null && rateOfFire != 0) 
        {
            if (itemInArms.tag == "Pickaxe" || itemInArms.tag == "Axe" || itemInArms.tag == "Hammer" || itemInArms.tag == "Infighting")
            {
                if (rb.rotation >= -115 && moving.facingRight)
                {
                    rb.rotation -= rateOfFire;
                    armAnimator.SetFloat("WeaponRotation", rb.rotation);
                }
                else if (rb.rotation <= 115 && !moving.facingRight)
                {
                    rb.rotation += rateOfFire;
                    armAnimator.SetFloat("WeaponRotation", rb.rotation * -1);
                }
                else
                {
                    usingItem.GetComponent<SpriteRenderer>().sprite = null;
                    rateOfFire = 0;
                    moving.weaponUsing = false;
                    armAnimator.SetBool("isBeating", moving.weaponUsing);
                }
            }
        }
    }
    public void OnDistantWeaponUsing(Vector3 scale, Vector3 position, Vector3 rotation)
    {
        usingItem.GetComponent<SpriteRenderer>().sprite = itemInArms.GetComponent<SpriteRenderer>().sprite;
        usingItem.transform.localScale = scale;
        usingItem.transform.localPosition = position;
        usingItem.transform.localRotation = Quaternion.AngleAxis(rotation.z, rotation);
        moving.weaponUsing = true;
        armAnimator.SetFloat("WeaponRotation", Mathf.Abs(rb.rotation));
        armAnimator.SetBool("isShooting", moving.weaponUsing);
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        respite = 0;
        if (MousePos.x < player.transform.position.x && moving.facingRight)
        {
            Vector3 theScale = player.transform.localScale;
            theScale.x *= -1;
            player.transform.localScale = theScale;
            moving.facingRight = !moving.facingRight;
        }
        if (MousePos.x > player.transform.position.x && !moving.facingRight)
        {
            Vector3 theScale = player.transform.localScale;
            theScale.x *= -1;
            player.transform.localScale = theScale;
            moving.facingRight = !moving.facingRight;
        }
    }
    public void OnInfightingWeaponUsing(Vector3 scale, Vector3 position, Vector3 rotation, float rateOfFire)
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(moving.weaponUsing == false)
        {
            if (MousePos.x < player.transform.position.x && moving.facingRight)
            {
                Vector3 theScale = player.transform.localScale;
                theScale.x *= -1;
                player.transform.localScale = theScale;
                moving.facingRight = !moving.facingRight;
            }
            if (MousePos.x > player.transform.position.x && !moving.facingRight)
            {
                Vector3 theScale = player.transform.localScale;
                theScale.x *= -1;
                player.transform.localScale = theScale;
                moving.facingRight = !moving.facingRight;
            }
        }
        if (moving.weaponUsing == false && moving.facingRight)
        {
            rb.rotation = 50;
        }
        else if (moving.weaponUsing == false && !moving.facingRight)
        {
            rb.rotation = -50;
        }
        armAnimator.SetBool("isBeating", moving.weaponUsing);
        usingItem.GetComponent<SpriteRenderer>().sprite = itemInArms.GetComponent<SpriteRenderer>().sprite;
        usingItem.transform.localScale = scale;
        usingItem.transform.localPosition = position;
        usingItem.transform.localRotation = Quaternion.AngleAxis(rotation.z, rotation);
        moving.weaponUsing = true;
        this.rateOfFire = rateOfFire;
    }
}
