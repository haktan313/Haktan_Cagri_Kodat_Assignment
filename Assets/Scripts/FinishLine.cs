using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private UIManager uiManagerRef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                uiManagerRef.DisplayWinner(player.playerName);
            }
        }
    }
}
