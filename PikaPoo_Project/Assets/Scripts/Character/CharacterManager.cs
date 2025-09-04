using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController cc;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        cc = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {

    }
}
