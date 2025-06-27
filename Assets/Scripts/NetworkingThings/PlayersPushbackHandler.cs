using Unity.Netcode;
using UnityEngine;


public class PlayersPushbackHandler  : NetworkBehaviour
{
    [SerializeField] private float knockbackForce = 10f; 
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer){ return;} 

        if (collision.collider.CompareTag("Player"))
        {
            NetworkObject playerNetworkObject = collision.collider.GetComponent<NetworkObject>();
            if (playerNetworkObject != null)
            {
                ApplyKnockbackToPlayer(playerNetworkObject, collision.contacts[0].point);
            }
        }
    }
    
    private void ApplyKnockbackToPlayer(NetworkObject playerNetworkObject, Vector3 contactPoint)
    {
        Rigidbody playerRigidbody = playerNetworkObject.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            Vector3 knockbackDirection = (playerRigidbody.position - contactPoint).normalized;
            
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }
}
