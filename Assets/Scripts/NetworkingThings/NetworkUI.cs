using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostStartButton;
    [SerializeField] private Button joinStartButton;

    private void Start()
    {
        hostStartButton.onClick.AddListener(()=> { NetworkManager.Singleton.StartHost(); Hide();});
        joinStartButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); Hide();});
    }
    void Hide()
    {
        hostStartButton.gameObject.SetActive(false);
        joinStartButton.gameObject.SetActive(false);
    }
}