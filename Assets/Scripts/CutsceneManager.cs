using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialogueUI;
    public Image portraitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Story Data")]
    public Dialogue[] conversation;

    private Queue<string> sentences;
    private int currentDialogueIndex = 0;

    void Start()
    {
        sentences = new Queue<string>();
        StartCutscene();
    }

    public void StartCutscene()
    {
        // 1. FREEZE THE GAME WORLD
        Time.timeScale = 0f;

        // 2. Enable UI
        dialogueUI.SetActive(true);

        // 3. Start the conversation
        currentDialogueIndex = 0;
        StartDialogue(conversation[currentDialogueIndex]);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.characterName;
        portraitImage.sprite = dialogue.portrait;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < conversation.Length)
            {
                StartDialogue(conversation[currentDialogueIndex]);
            }
            else
            {
                EndDialogue();
            }
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            // "yield return null" waits 1 frame. 
            // Because this Coroutine runs on the UI, it works even if Time.timeScale is 0.
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);

        // 4. UNFREEZE THE GAME WORLD
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Allow clicking through dialogue even while game is paused
        if (dialogueUI.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            DisplayNextSentence();
        }
    }
}