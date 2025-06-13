using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    public int CutsceneID = 0;
    public TextMeshProUGUI DialogueText;
    public GameObject DialogueBg;
    public bool isDialogueActive = false;
    public GameObject Player;
    void Start()
    {
        DialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        DialogueBg = GameObject.Find("DialogueBg");
        Player = GameObject.Find("Player");
    }
    void Update()
    {
        if (!isDialogueActive)
        {
            DialogueBg.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDialogueActive)
        {
            if (CutsceneID == 1)
            {
                DialogueText.text = "";
                StartCoroutine(Dialogue("I SHOULD TRY CHECKING THAT TERMINAL, IT MIGHT HAVE SOME USEFUL INFORMATION...", 0.025f));
                timeline.Play();
            }
            else if (CutsceneID == 2)
            {
                DialogueText.text = "";
                StartCoroutine(Dialogue("HMM...I CAN USE IT TO ACCESS THE TIMESTREAM. OH, THERE'S SOME ANOMALIES NEARBY, I SHOULDN'T MESS WITH THOSE!", 0.01f));
                timeline.Play();
            }
        }
    }
    private IEnumerator Dialogue(string text, float delay)
    {
        Player.GetComponent<playerMovement>().enabled = false;
        Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        isDialogueActive = true;
        for (int i = 0; i < text.Length; i++)
        {
            DialogueText.text += text[i];
            yield return new WaitForSeconds(delay);
        }
        isDialogueActive = false;
        Player.GetComponent<playerMovement>().enabled = true;
    }
}
