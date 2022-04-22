using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Material bulletMaterial;
    public Animator animator;
    public Rigidbody rb;
    public GameObject player;
    public LevelController levelController;
    public static PlayerManager Current;
    private float forceMagnitude;
    public GameObject coin;
    private void Start()
    {
        Current = this;
       
    }
    private void FixedUpdate()
    {
        if (PlayerController.Current._input.x != 0 || PlayerController.Current._input.z != 0)
        {
          animator.SetBool("running", true);
        }
        else
        {
          animator.SetBool("running", false);
        }
    }

    
    [SerializeField]
    
    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColorChangerGreen"))
        {
            bulletMaterial.color=new Color32(0,255,0,255);
        }
        if (other.CompareTag("ColorChangerRed"))
        {
            bulletMaterial.color = new Color32(255, 0, 0, 255);
        }
        if (other.CompareTag("ColorChangerBlue"))
        {
            bulletMaterial.color = new Color32(0, 0, 255, 255);
        }
        if (other.CompareTag("Shuttle"))
        {
            playerDead();
        }
        if (other.CompareTag("Deep")) 
        {
            playerDead();
        }
        if (other.CompareTag("Coin"))
        {
            levelController.ScoreCoin();
            Destroy(coin);
        }

    }
   public void playerDead()
    {
        animator.SetBool("dead", true);
        GetComponent<PlayerController>().enabled = false;
        levelController.GameOver();
    }
}
