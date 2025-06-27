using Unity.Netcode;
using UnityEngine;

public class PlayerRespawn : NetworkBehaviour
{
    [SerializeField] private float respawnTime = 5f; 
    [SerializeField] private Vector3 respawnPosition = Vector3.zero;

    private Rigidbody rb; 
    private float timeWithoutContact; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsOwner) {return;}
        
        if(IsTouchingAnyObject()){timeWithoutContact = 0f;}
        else{timeWithoutContact += Time.deltaTime;}
        
        if (timeWithoutContact >= respawnTime) { RequestRespawnServerRpc();}
    }
    private bool IsTouchingAnyObject()
    {
        RaycastHit hitResult;
        return Physics.Raycast(transform.position, Vector3.down, out hitResult, 3f);
    }
    
    [ServerRpc]
    private void RequestRespawnServerRpc()
    {
        timeWithoutContact = 0;
        RespawnPlayerServerRpc();
    }
    
    [ServerRpc]
    private void RespawnPlayerServerRpc()
    {
        if (!IsServer) { return;}
        transform.position = respawnPosition;

        if (rb == null) { return;}
        rb.linearVelocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
    }
}
