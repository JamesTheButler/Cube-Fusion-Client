using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum commands{
    UP,
    RIGHT,
    DOWN,
    LEFT,
    NONE
}

public class UIManager : MonoBehaviour {
    public GameObject buttonPrefab;
    public GameObject canvas;
    public GameObject scrollpanel;

    public Sprite upImg;
    public Sprite downImg;
    public Sprite leftImg;
    public Sprite rightImg;
    public Sprite stopImg;
    
    public Button playButton;

    public ClientNetwork client;

    private float gap, startX, buttonWidth;

    List<commands> commandList;

    void Start() {
        commandList = new List<commands>();
        gap = 10f;
        startX = gap;
        buttonWidth = ((RectTransform)buttonPrefab.gameObject.transform).rect.width;
    }

    private void addCommandToList(commands cmd) {
        commandList.Add(cmd);
        displayQueue();
    }

    public void addUpCmd() { addCommandToList(commands.UP); }
    public void addDownCmd() { addCommandToList(commands.DOWN); }
    public void addRightCmd() { addCommandToList(commands.RIGHT); }
    public void addLeftCmd() { addCommandToList(commands.LEFT); }
    public void addStopCmd() { addCommandToList(commands.NONE); }

    public void displayQueue() {
        for(int j=0; j<canvas.transform.childCount; j++){
            Destroy(canvas.transform.GetChild(j).gameObject);
        }

        for (int i = 0; i < commandList.Count; i++) {
            createButton(i, commandList[i]);
        }

        //resize scroll panel
        ((RectTransform)scrollpanel.transform).sizeDelta = new Vector2(commandList.Count * (buttonWidth + gap) + startX, startX * 2 + buttonWidth);
    }

    public string serializeQueue()
    {
        string s = "";
        for (int i = 0; i < commandList.Count; i++)
        {
            switch (commandList[i])
            {
                case commands.UP:
                    s += "U";
                    break;
                case commands.DOWN:
                    s += "D";
                    break;
                case commands.RIGHT:
                    s += "R";
                    break;
                case commands.LEFT:
                    s += "L";
                    break;
                case commands.NONE:
                    s += "W";
                    break;
            }
        }
        return s;
    }

    public void confirmQueue() {
        client.SendQueue(serializeQueue());
        EnablePlay(false);
    }

    public void EnablePlay(bool enable)
    {
        playButton.interactable = enable;
    }

    private void createButton(int id, commands cmd) {
        GameObject button = Instantiate(buttonPrefab, canvas.transform);
       
        button.GetComponent<RectTransform>().position = new Vector3(startX+id*(buttonWidth+gap), gap, 0);
        button.GetComponent<SelfDelete>().id = id;
        switch (cmd)
        {
            case commands.UP:
                button.GetComponentInChildren<Image>().sprite = upImg;
                break;
            case commands.DOWN:
                button.GetComponentInChildren<Image>().sprite = downImg;
                break;
            case commands.RIGHT:
                button.GetComponentInChildren<Image>().sprite = rightImg;
                break;
            case commands.LEFT:
                button.GetComponentInChildren<Image>().sprite = leftImg;
                break;
            case commands.NONE:
                button.GetComponentInChildren<Image>().sprite = stopImg;
                break;
        }
    }

    public void deleteItemFromList(int id) {
        commandList.RemoveAt(id);
        displayQueue();

    }

    public void deletequeue() {
        commandList.Clear();
        displayQueue();
    }
}
