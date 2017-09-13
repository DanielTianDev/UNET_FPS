using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))] //we need player for this to work.
public class PlayerSetup : NetworkBehaviour { //networkbehaviour has netid.
    
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer) //if this object isn't controlled by the system, then we have to disable all the components.
        {
            DisableComponents();
            AssignRemoteLayer();

        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    public override void OnStartClient() //called everytime a client is setup locally.
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }


    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable() //also called when object is destroyed
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true); 
        }

        GameManager.UnRegisterPlayer(transform.name); //we've set the transform.name to the id of the player.

    }

}
