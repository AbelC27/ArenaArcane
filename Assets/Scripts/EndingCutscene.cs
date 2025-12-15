using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject uiPanel; 
    public Image portraitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences = new Queue<string>();

    void Start()
    {

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    public void StartEndingDialogue(Dialogue dialogue)
    {
        UnityEngine.Debug.Log("URGENT: Scriptul a pornit! Încerc să activez fereastra..."); // <--- Adaugă asta

        Time.timeScale = 0f;

        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
            // Această linie mută panelul la finalul listei vizuale, deci va fi desenat ultimul (deasupra)
            uiPanel.transform.SetAsLastSibling();
        }

        Time.timeScale = 0f;
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
            Transform bg = transform.Find("Image");
            if (bg != null) bg.gameObject.SetActive(true);
        }

        if (nameText != null) nameText.text = dialogue.characterName;
        if (portraitImage != null) portraitImage.sprite = dialogue.portrait;

        sentences.Clear();
        if (dialogue.sentences != null)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
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
            yield return null;
        }
    }

    void EndDialogue()
    {

        UnityEngine.Debug.Log("Conversație terminată.");
    }

    void Update()
    {
        if (uiPanel.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            DisplayNextSentence();
        }
    }
}