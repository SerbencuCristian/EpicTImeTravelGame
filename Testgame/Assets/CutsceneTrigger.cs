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
    public GameObject GameController;
    void Start()
    {
        DialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        DialogueBg = GameObject.Find("DialogueBg");
        Player = GameObject.Find("Player");
        GameController = GameObject.Find("GameController");
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
            if (CutsceneID == 0)
            {
                DialogueText.text = "";
                StartCoroutine(Dialogue("I SHOULD TRY CHECKING THAT TERMINAL, IT MIGHT HAVE SOME USEFUL INFORMATION...", 0.025f));
                timeline.Play();
                GameController.GetComponent<GameController>().triggeredScenes[CutsceneID] = true;

            }
            else if (CutsceneID == 1)
            {
                DialogueText.text = "";
                StartCoroutine(Dialogue("HMM...I CAN USE IT TO ACCESS THE TIMESTREAM. OH, THERE'S SOME ANOMALIES NEARBY, I SHOULDN'T MESS WITH THOSE!", 0.01f));
                timeline.Play();
                GameController.GetComponent<GameController>().triggeredScenes[CutsceneID] = true;
            }
            else if (CutsceneID == 2)
            {
                DialogueText.text = "";
                StartCoroutine(Dialogue("MAYBE I CAN SCRAP SOMETHING FROM THIS MACHINE", 0.01f));
                timeline.Play();
                GameObject.Find("LoadCanvas").GetComponent<LoadScript>().TimeTravelEnabled = true;
                GameObject.Find("Door").GetComponent<Animator>().SetTrigger("Open");
                GameController.GetComponent<GameController>().triggeredScenes[CutsceneID] = true;
            }
        }
    }
    private IEnumerator Dialogue(string text, float delay)
    {
        Player.GetComponent<playerMovement>().enabled = false;
        Player.GetComponent<PlayerShoot>().enabled = false;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        isDialogueActive = true;
        for (int i = 0; i < text.Length; i++)
        {
            DialogueText.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }
    void OnDisable()
    {
        if (Player == null) return;
        isDialogueActive = false;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Player.GetComponent<PlayerShoot>().enabled = true;
        Player.GetComponent<playerMovement>().enabled = true;
        Player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -0.1f);
        DialogueBg.SetActive(false);
    }
}
