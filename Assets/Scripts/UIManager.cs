using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText; 
    
    public void DisplayWinner(string playerName)
    {
        if(winText == null) { return; }
        winText.text = $"{playerName} won!";
    }
}
