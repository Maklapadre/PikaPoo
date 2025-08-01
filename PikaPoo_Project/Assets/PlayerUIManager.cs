using UnityEngine;
using Unity.Netcode;

public class PlayerUIManager : MonoBehaviour
{
    private static PlayerUIManager instance;

    [Header("NETWORK JOIN")]
    [SerializeField] bool startGameAsClient;

    public static PlayerUIManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (startGameAsClient)
        {
            startGameAsClient = false;
            // SHUT DOWN THE HOST
            NetworkManager.Singleton.Shutdown();

            // START THE CLIENT
            NetworkManager.Singleton.StartClient();
        }
    }
}
