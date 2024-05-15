using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject dialogueBox2;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI dialogueText2;
    [SerializeField] int lettersPerSecond;
    public event Action OnShowDialogue;
    public event Action OnHideDialogue;
    public static DialogueManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;
    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialogue?.Invoke();
        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        dialogueBox2.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0], dialogue.Names[0]));
    }
    public void HandleUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine], dialogue.Names[currentLine]));
            }
            else
            {
                FindObjectOfType<AudioManager>().StopMusic();
                FindObjectOfType<AudioManager>().Play("Green Guy L2");
                dialogueBox.SetActive(false);
                dialogueBox2.SetActive(false);
                currentLine = 0;
                OnHideDialogue?.Invoke();
            }
        }

    }
    public IEnumerator TypeDialogue(string line,string name)
    {
        isTyping = true;
        dialogueText.text = "";
        dialogueText2.text = name;
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}