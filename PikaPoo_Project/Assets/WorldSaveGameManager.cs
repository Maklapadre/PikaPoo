using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    private static WorldSaveGameManager instance;
    public static WorldSaveGameManager Instance { get => instance; set => instance = value; }

    [SerializeField] int worldScenceIndex = 1;

    public void Awake()
    {
        if (instance == null) {  instance = this; }
        else { Destroy(gameObject); }
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldScenceIndex);

        yield return null;  
    }
}
