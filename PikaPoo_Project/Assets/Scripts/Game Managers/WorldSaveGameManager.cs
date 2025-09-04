using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    private static WorldSaveGameManager instance;
    public static WorldSaveGameManager Instance { get => instance; set => instance = value; }

    [SerializeField] int worldSceneIndex = 1;

    public void Awake()
    {
        if (instance == null) {  instance = this; }
        else { Destroy(gameObject); }
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        yield return null;  
    }
}
