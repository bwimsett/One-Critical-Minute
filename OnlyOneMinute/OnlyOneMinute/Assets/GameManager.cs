using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public TextLine startingLine;
    public KeyCode returnKey;
    public GameObject textOutputPrefab;
    public GameObject textInputPrefab;
    public TMP_InputField currentInput;
    public Transform displayContainer;
    private TextLine currentLine;
    public string inputSymbol = "> ";
    public string outputNotRecognisedText;
    
    void Start() {
        currentLine = startingLine;
        displayCurrentOptions();
    }
    
    void Update() {
        if (Input.GetKey(returnKey)) {
            validateInput();
        }
        
    }

    private void displayCurrentOptions() {
        TextLine[] children = currentLine.children;

        //Convert input to uneditable
        if (currentInput) {
            string inputText = currentInput.text;
            TextOutput inputTextToOutput = Instantiate(textOutputPrefab, displayContainer).GetComponent<TextOutput>();
            inputTextToOutput.setText(inputSymbol+inputText);
            Destroy(currentInput.gameObject);
        }
        
        //Create text lines for each of the children
        for (int i = 0; i < children.Length; i++) {
            createOutput(children[i].preview);
        }

        //Create text input
        currentInput = Instantiate(textInputPrefab, displayContainer).GetComponent<TMP_InputField>();
    }

    private void createOutput(string outputText) {
        TextOutput outputLine = Instantiate(textOutputPrefab, displayContainer).GetComponent<TextOutput>();
        outputLine.setText(outputText);
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
        
        createOutput(outputNotRecognisedText);
    }
}
