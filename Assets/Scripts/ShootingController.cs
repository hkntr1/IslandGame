using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class ShootingController : MonoBehaviour
{
    public GameObject bulletprefab;
    public Transform Barrel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletCoroutine());
    }
 
    public IEnumerator BulletCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject bullet = LeanPool.Spawn(bulletprefab, Barrel.position, Quaternion.identity);
            bullet.transform.forward = Barrel.forward;
        }
    }
  
}

        
    

