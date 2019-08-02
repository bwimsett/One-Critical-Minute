using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public TextLine startingLine;
    public KeyCode returnKey;
    public GameObject textOutputPrefab;
    public GameObject textInputPrefab;
    public TMP_InputField currentInput;
    public Transform displayContainer;
    public TextLine currentLine;
    public string inputSymbol = "> ";
    public string outputNotRecognisedText;
    
    void Start() {
        currentLine = startingLine;
        displayCurrentOptions();
    }
    
    void Update() {
        if (Input.GetKeyDown(returnKey)) {
            validateInput();
        }
        
    }

    private void displayCurrentOptions() {
        TextLine[] children = currentLine.children;
        
        //Create text lines for each of the children
        for (int i = 0; i < children.Length; i++) {
            if (i == 0) {
                createOutput(children[i].preview, true);
            }
            else {
                createOutput(children[i].preview, false);
            }
        }


    }

    private void createOutput(string outputText, bool printInput) {
        //Destroy text input
        if (currentInput) {
            if (printInput) {
                string inputText = currentInput.text;
                TextOutput inputTextToOutput = Instantiate(textOutputPrefab, displayContainer).GetComponent<TextOutput>();
                inputTextToOutput.setText(inputSymbol+inputText);
            }
            
            Destroy(currentInput.gameObject);
        }
        
        TextOutput outputLine = Instantiate(textOutputPrefab, displayContainer).GetComponent<TextOutput>();
        outputLine.setText(outputText);

        //Create input
        currentInput = Instantiate(textInputPrefab, displayContainer).GetComponent<TMP_InputField>();
        currentInput.Select();
    }

    private void setCurrentLine(TextLine line) {
        currentLine = line;
        displayCurrentOptions();
    }
    
    private void validateInput() {
        TextLine[] children = currentLine.children;
        string input = currentInput.text;
        
        //Loop through children to see if input matches key
        for (int i = 0; i < children.Length; i++) {
            if (currentInput.text.Equals(children[i].key)) {
                setCurrentLine(children[i]);
                return;
            }
        }
        
        createOutput(outputNotRecognisedText, true);
    }
}
