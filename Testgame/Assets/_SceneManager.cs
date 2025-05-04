using UnityEngine;

public class _SceneManager : MonoBehaviour
{
    public static _SceneManager instance;
    public Vector2 lastcheckpoint;
    private void Awake()
    {
        if(lastcheckpoint == null)
        {
            lastcheckpoint = GameObject.Find("Player").transform.position;
        }
        if (instance)
        {

            //GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth);
            //instead set a flag for insta death on player start and set to right checkpoint
            Destroy (gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
        
    }


}
