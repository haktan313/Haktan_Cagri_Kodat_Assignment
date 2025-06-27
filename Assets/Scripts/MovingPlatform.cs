using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class MovingPlatform : NetworkBehaviour
{
    public enum MovementDirection
    {
        Horizontal,
        Vertical
    }
    [SerializeField] private MovementDirection direction = MovementDirection.Horizontal;
    [SerializeField] private float minPosition;
    [SerializeField] private float maxPosition = 5f;
    [SerializeField] private float speed = 2f;
    
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 2f;

    private bool canMove = true;
    private bool isWaiting = false;

    private void Update()
    {
        if (!IsServer || isWaiting) { return;}
        MovePlatform();
    }
    
    private void MovePlatform()
    {
        Vector3 currentPosition = transform.position;

        switch (direction)
        {
            case MovementDirection.Horizontal:
                MoveAlongAxis(ref currentPosition.x, minPosition, maxPosition);
                break;
            case MovementDirection.Vertical:
                MoveAlongAxis(ref currentPosition.y, minPosition, maxPosition);
                break;
        }
        
        transform.position = currentPosition;
        SyncPositionWithClients(currentPosition);
    }
    
    private void MoveAlongAxis(ref float currentAxisValue, float minValue, float maxValue)
    {
        float a = canMove ? maxValue : minValue;
        currentAxisValue = Mathf.MoveTowards(currentAxisValue, a, speed * Time.deltaTime);
        if (Mathf.Approximately(currentAxisValue, a))
        {
            if(canMove){canMove = false;}
            else{StartCoroutine(HandleRandomDelay());}
        }
    }
    
    private IEnumerator HandleRandomDelay()
    {
        isWaiting = true; 
        
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);

        canMove = true; 
        isWaiting = false;
    }

    private void SyncPositionWithClients(Vector3 newPosition)
    {
        UpdatePlatformPositionClientRpc(newPosition);
    }
    
    [ClientRpc]
    private void UpdatePlatformPositionClientRpc(Vector3 newPosition)
    {
        if (!IsServer)
        {
            transform.position = newPosition;
        }
    }
}
