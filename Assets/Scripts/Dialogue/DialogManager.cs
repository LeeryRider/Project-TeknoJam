using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    private int currentLineIndex = 0;

    public string GetNextLine()
    {
        if (currentLineIndex >= dialogueLines.Length)
        {
            return null; // No more dialogue lines
        }

        string line = dialogueLines[currentLineIndex];
        currentLineIndex++;
        return line;
    }

    public void ResetDialogue()
    {
        currentLineIndex = 0;
    }
}
