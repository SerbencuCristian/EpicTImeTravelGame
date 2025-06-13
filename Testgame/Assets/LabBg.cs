using UnityEngine;

public class LabBg : MonoBehaviour
{
    public GameController GameController;
    public Sprite Spritepast;
    public Sprite Spritepresent;
    public Sprite Spritfuture;
    public LoadScript LoadScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        LoadScript = GameObject.Find("LoadCanvas").GetComponent<LoadScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.timeindicator == 1 || (LoadScript.lastpress == 1 && LoadScript.HoldingTS))
        {
            GetComponent<SpriteRenderer>().sprite = Spritepast;
        }
        else if (GameController.timeindicator == 2 || (LoadScript.lastpress == 2 && LoadScript.HoldingTS))
        {
            GetComponent<SpriteRenderer>().sprite = Spritepresent;
        }
        else if (GameController.timeindicator == 3 || (LoadScript.lastpress == 3 && LoadScript.HoldingTS))
        {
            GetComponent<SpriteRenderer>().sprite = Spritfuture;
        }
    }
}
