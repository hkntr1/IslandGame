using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class Bullet : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        StartCoroutine(BulletDestroy());
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    public IEnumerator BulletDestroy()
    {  
            yield return new WaitForSeconds(2f);
        if (this.gameObject.activeSelf)
        {
            LeanPool.Despawn(this.gameObject,0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.activeSelf)
        {
            LeanPool.Despawn(this.gameObject);
        }
    }
}
