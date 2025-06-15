using UnityEngine;

public class Door : MonoBehaviour
{
    public void DoorDone()
    {
        GetComponent<Animator>().speed = 0f;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
