using UnityEngine;
using System.Collections.Generic;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<Vector3> weaponPosition = new List<Vector3>();
    [SerializeField] private List<Vector3> weaponRotation = new List<Vector3>();
    [SerializeField] private List<Vector3> weaponScale = new List<Vector3>();
    [SerializeField] private List<float> weaponRateOfFire = new List<float>();
    [SerializeField] private List<float> weaponDamage = new List<float>();
    [SerializeField] private List<bool> weaponAutoAttack = new List<bool>();
    [SerializeField] private List<GameObject> ammo = new List<GameObject>();
    [SerializeField] private List<float> ammoDamage = new List<float>();
    [SerializeField] private List<float> ammoSpeed = new List<float>();
    private Inventory inventory;
    public GameObject weapon;
    private InArmsItem inArms;
    private ObjectBreaking ob;
    public bool isWeaponActive;
    private float rateOfFire;
    private bool autoAttack;
    private Vector3 scale;
    private Vector3 position;
    private Vector3 rotation;
    private float respite;
    private GameObject selectedArrow;
    private GameObject selectedBullet;
    private GameObject rotationAxis;
    private GameObject firePoint;
    private Vector2 MousePos;
    private Vector2 vector2;
    private Rigidbody2D rb;
    [SerializeField] private Camera cam;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        rotationAxis = GameObject.Find("RotationAxis");
        firePoint = GameObject.Find("FirePoint");
        inArms = GameObject.Find("Player").GetComponent<InArmsItem>();
        ob = GameObject.Find("ObjectBreaker").GetComponent<ObjectBreaking>();
    }
    private void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapon == weapons[i])
            {
                rateOfFire = weaponRateOfFire[i];
                autoAttack = weaponAutoAttack[i];
                scale = weaponScale[i];
                position = weaponPosition[i];
                rotation = weaponRotation[i];
                break;
            }
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapon == weapons[i] && weapon.tag == "Bow")
            {
                if(selectedArrow == null)
                {
                    isWeaponActive = false;
                }
                else
                {
                    isWeaponActive = true;
                }
                break;
            }
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapon == weapons[i] && weapon.tag == "Gun")
            {
                if (selectedBullet == null)
                {
                    isWeaponActive = false;
                }
                else
                {
                    isWeaponActive = true;
                }
                break;
            }
        }
        if(weapon != null)
        {
            if (weapon.tag == "Pickaxe" || weapon.tag == "Axe" || weapon.tag == "Hammer")
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    if (weapons[i] == weapon)
                    {
                        ob.toolRateOfFire = weaponRateOfFire[i];
                        ob.toolDamage = weaponDamage[i];
                    }
                }
            }
        }
        if (autoAttack && isWeaponActive && weapon.tag != "Pickaxe" && weapon.tag != "Axe" && weapon.tag != "Hammer" && weapon.tag != "Infighting" && Input.GetButton("Fire1") && respite >= rateOfFire)
        {
            inArms.OnDistantWeaponUsing(scale, position, rotation);
            Shoot();
        }
        else if (!autoAttack && isWeaponActive && Input.GetButtonDown("Fire1") && respite >= rateOfFire)
        {
            inArms.OnDistantWeaponUsing(scale, position, rotation);
            Shoot();
        }
        if (Input.GetButton("Fire1") && isWeaponActive)
        {
            if (weapon.tag == "Pickaxe" || weapon.tag == "Axe" || weapon.tag == "Hammer" || weapon.tag == "Infighting")
            {
                inArms.OnInfightingWeaponUsing(scale, position, rotation, rateOfFire);
            }
        }
        if (respite < rateOfFire)
        {
            respite += Time.deltaTime;
        }
        if (respite >= rateOfFire && weapon != null && weapon.tag == "Bow" || respite >= rateOfFire && weapon != null && weapon.tag == "Gun" || respite >= rateOfFire && weapon != null && weapon.tag == "Magical")
        {
            rb = rotationAxis.GetComponent<Rigidbody2D>();
            MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            vector2 = MousePos - rb.position;
            rb.rotation = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg - 90f;
        }
        rotationAxis.transform.position = GameObject.Find("Player").GetComponent<Transform>().position;
        for (int j = 0; j < inventory.inventory.Count; j++)
        {
            if (inventory.isFull[j] && inventory.inventory[j].tag == "Arrow")
            {
                selectedArrow = inventory.inventory[j];
                break;
            }
        }
        for (int j = 0; j < inventory.inventory.Count; j++)
        {
            if (inventory.isFull[j] && inventory.inventory[j].tag == "Bullet")
            {
                selectedBullet = inventory.inventory[j];
                break;
            }
        }
    }
    public void Shoot()
    {
        if (weapon.tag == "Bow" && selectedArrow != null)
        {
            for (int i = 0; i < ammo.Count; i++)
            {
                if (weapon.tag == "Bow" && ammo[i] == selectedArrow)
                {
                    GameObject go = Instantiate(selectedArrow, firePoint.transform.position, new Quaternion());
                    go.GetComponent<BoxCollider2D>().isTrigger = true;
                    go.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * ammoSpeed[i], ForceMode2D.Impulse);
                    go.name = "Shooten Object";
                    rb = go.GetComponent<Rigidbody2D>();
                    MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    vector2 = MousePos - rb.position;
                    rb.rotation = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg - 270f;
                    selectedArrow = null;
                    inventory.AmmoSpend("Arrow");
                    break;
                }
            }
        }
        if(weapon.tag == "Gun" && selectedBullet != null)
        {
            for (int i = 0; i < ammo.Count; i++)
            {
                if (weapon.tag == "Gun" && ammo[i] == selectedBullet)
                {
                    GameObject go = Instantiate(selectedBullet, firePoint.transform.position, new Quaternion());
                    go.GetComponent<CircleCollider2D>().isTrigger = true;
                    go.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * ammoSpeed[i], ForceMode2D.Impulse);
                    go.name = "Shooten Object";
                    selectedBullet = null;
                    inventory.AmmoSpend("Bullet");
                    break;
                }
            }
        }
        respite = 0;
    }
}