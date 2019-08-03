
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
    public Sprite[] pages;
    public Image image;
    private int currentPageIndex = 0;

    public KeyCode nextKey;
    public KeyCode prevKey;
    
    public bool receiveInput = false;
    
    public void Update() {
        if (!receiveInput) {
            return;
        }
        
        if (Input.GetKeyDown(nextKey)) {
            nextPage();
        } else if (Input.GetKeyDown(prevKey)) {
            previousPage();
        }
    }

    public void toggle() {
        if (!receiveInput) {
            open();
        }
        else {
            close();
        }
    }

    private void open() {
        image.enabled = true;
        receiveInput = true;
    }

    private void close() {
        image.enabled = false;
        receiveInput = false;
    }
    
    public void nextPage() {
        if (currentPageIndex < pages.Length - 1) {
            currentPageIndex++;
        }
        
        refresh();
    }

    public void previousPage() {
        if (currentPageIndex > 0) {
            currentPageIndex--;
        }
        
        refresh();
    }

    public void refresh() {
        image.sprite = pages[currentPageIndex];
    }


}
