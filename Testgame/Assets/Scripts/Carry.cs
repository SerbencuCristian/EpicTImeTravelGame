using UnityEngine;

public class Carry : MonoBehaviour
{
    public static Carry instance;
    public int save = 1;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
