using UnityEngine;
using UnityEngine.Playables;
public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timeline.Play();
        }
    }
}
