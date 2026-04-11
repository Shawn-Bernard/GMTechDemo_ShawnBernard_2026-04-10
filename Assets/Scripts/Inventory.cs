using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int maxAmmo;
    private int currentAmmoCount;

    private List<GameObject> bulletPool = new List<GameObject>();
    private int bulletIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillList();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        if (bulletPool == null) return null;

        for (int i = 0; i < maxAmmo; i++)
        {
            if (bulletPool[i] != null && !bulletPool[i].activeSelf)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void AddBullet()
    {
        currentAmmoCount++;
    }

    void FillList()
    {

        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);   
            bullet.SetActive(false);
            bullet.name = $"{bulletPrefab.name } {i}";
            bulletPool.Add(bullet);
        }

        currentAmmoCount = maxAmmo;
    }
}
