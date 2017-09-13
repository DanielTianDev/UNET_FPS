using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

    private const string PLAYER_TAG = "Player";

    //reference to our player camera. when we're shooting a ray, we want it to shoot from the exact center of that camear
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    public PlayerWeapon weapon;

    private void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Playershoot: No camera referenced");
            this.enabled = false;
        }
    }


    private void Update()
    {


        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    //since we made sure the PlayerShoot component is disabled on all remote clients, it's only called on the
    //LOCAL CLIENT!!!
    [Client] //only called on the client, and not the server
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask)) //if we hit something
        {
            if(_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }


    [Command] //methods that are called only on the server
    void CmdPlayerShot(string _playerID, int _damage)
    {
        print(_playerID + " has been shot");

        Player _player = GameManager.GetPlayer(_playerID);

        _player.TakeDamage(_damage);

    }


}
