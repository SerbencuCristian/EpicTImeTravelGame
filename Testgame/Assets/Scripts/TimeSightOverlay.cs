using UnityEngine;

public class TimeSightOverlay : MonoBehaviour
{
    public bool isHolding = false;
    public void TimeSightOn()
    {
        GameObject.Find("LoadCanvas").GetComponent<LoadScript>().TimeSightAnim();
        isHolding = true;
    }
    public void TimeSightOff()
    {
        GameObject.Find("LoadCanvas").GetComponent<LoadScript>().TimeSightOff();
        isHolding = false;
    }

}
