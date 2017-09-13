using UnityEngine;
using UnityEngine.Networking;
//Player manager


public class Player : NetworkBehaviour
{

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    private void Awake()
    {
        SetDefaults();
    }


    public void TakeDamage(int _amount) //only called on server?
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + " now has " + currentHealth + " hp.");
    } 

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

}
