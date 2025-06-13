using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameController GameController;
    public LoadScript LoadScript;
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        LoadScript = GameObject.Find("LoadCanvas").GetComponent<LoadScript>();
    }
    void Update()
    {
        if ((GameController.timeindicator == 2 && !LoadScript.HoldingTS) ||(LoadScript.lastpress == 2 && LoadScript.HoldingTS))
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
