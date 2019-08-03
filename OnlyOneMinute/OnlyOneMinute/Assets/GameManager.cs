using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public TextLine startingLine;
    public CanvasGroup screenCanvasGroup;
    public ScrollRect lines;
    public TextLine[] generalOptions;
    public GameObject textOutputPrefab;
    public GameObject textInputPrefab;
    public KeyCode returnKey;
    public string backKey;
    private TMP_InputField currentInput;
    public Transform displayContainer;
    public TextLine currentLine;
    public int lineWidth;
    public int lineHeight;
    public string inputSymbol = "> ";
    public string outputNotRecognisedText;

    
    
    void Start() {
        currentLine = startingLine;
        //Disable interaction on screen canvas
        screenCanvasGroup.blocksRaycasts = false;
        displayCurrentOptions();
    }
    
    void Update() {
        if (Input.GetKeyDown(returnKey)) {
            validateInput();
        }
        
        //Scroll to bottom
        lines.normalizedPosition = new Vector2(0,0);
        //Canvas.ForceUpdateCanvases();
        if (!currentInput.isFocused) {
            currentInput.Select();
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
        
        TextOutput currentOutput = Instantiate(textOutputPrefab, displayContainer).GetComponent<TextOutput>();
        currentOutput.setText(outputText);
        currentOutput.textBox.ForceMeshUpdate();
        RectTransform outputRect = (RectTransform)currentOutput.transform;
        Debug.Log(currentOutput.textBox.textInfo.lineCount);
        outputRect.sizeDelta = new Vector2(lineWidth,currentOutput.textBox.textInfo.lineCount*lineHeight);

        //Create input
        currentInput = Instantiate(textInputPrefab, displayContainer).GetComponent<TMP_InputField>();
        currentInput.Select();
    }

    private void refreshOutput() {
        
    }

    private void setCurrentLine(TextLine line) {
        currentLine = line;
        displayCurrentOptions();
    }
    
    private void validateInput() {
        TextLine[] children = currentLine.children;
        string input = currentInput.text.ToLower();
        
        //Loop through children to see if input matches key
        for (int i = 0; i < children.Length; i++) {
            if (currentInput.text.ToLower().Equals(children[i].key)) {
                setCurrentLine(children[i]);
                return;
            }
        }
        
        //Loop through general options to see if input matches key
        for (int i = 0; i < generalOptions.Length; i++) {
            if (generalOptions[i].key.ToLower().Equals(input)) {
                generalOptions[i].parent = currentLine;
                setCurrentLine(generalOptions[i]);
                return;
            }
        }
        
        //Check to go back
        if (input.Equals(backKey)) {
            if (currentLine.parent) {
                setCurrentLine(currentLine.parent);
            }
            else {
                createOutput("cannot go back.", true);
            }

            return;
        }
        
        createOutput(outputNotRecognisedText, true);
    }
}
