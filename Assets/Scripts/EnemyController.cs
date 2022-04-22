using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Material bulletMaterial;
    [SerializeField] float lookRadius=20f;
    Transform target;
    NavMeshAgent agent;
    public float distance;
    public Animator animator;
    [SerializeField] Image colorBar;
    public PlayerManager playerManager;
    public Rigidbody rb; 
    public LevelController levelController;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }
        if (distance<=agent.stoppingDistance)
        {
            rotatetotarget();
            StartCoroutine(waiter());
        }
    }
    void rotatetotarget()
    {       
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrotaiton = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotaiton, Time.deltaTime * 5f);
    }
    IEnumerator waiter()
    {
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        playerManager.playerDead();
        yield return new WaitForSeconds(3);      
        levelController.GameOver();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void WaitAndDestroy()
    {
        float destroytime = 1.5f;
        Destroy(gameObject, destroytime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")&&bulletMaterial.color.g==colorBar.color.g && bulletMaterial.color.b == colorBar.color.b && bulletMaterial.color.r == colorBar.color.r)
        {                         
            animator.SetTrigger("death");
            colorBar.color = Color.black;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            agent.acceleration = 0f;
            WaitAndDestroy();
            levelController.ScoreEnemy();
        }
    }


}
