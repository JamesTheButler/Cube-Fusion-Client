using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum commands{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public class UIManager : MonoBehaviour {
    public Text queueText;
    public GameObject buttonPrefab;
    public GameObject canvas;

    public Sprite upImg;
    public Sprite downImg;
    public Sprite leftImg;
    public Sprite rightImg;

    public ClientNetwork client;


    List<commands> commandList;

    void Start() {
        commandList = new List<commands>();
    }

    private void addCommandToList(commands cmd) {
        commandList.Add(cmd);
        displayQueue();
    }

    public void addUpCmd() { addCommandToList(commands.UP); }
    public void addDownCmd() { addCommandToList(commands.DOWN); }
    public void addRightCmd() { addCommandToList(commands.RIGHT); }
    public void addLeftCmd() { addCommandToList(commands.LEFT); }

    public void displayQueue() {
        for(int j=0; j<canvas.transform.childCount; j++){
            Destroy(canvas.transform.GetChild(j).gameObject);
        }

        queueText.text = serializeQueue();
    }

    public string serializeQueue()
    {
        string s = "";
        for (int i = 0; i < commandList.Count; i++)
        {
            createButton(i, commandList[i]);
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
            }
        }
        return s;
    }

    public void confirmQueue() {
        client.SendQueue(serializeQueue());
    }

    private void createButton(int id, commands cmd)
    {
        GameObject button = Instantiate(buttonPrefab, canvas.transform);
        button.GetComponent<RectTransform>().position = new Vector3( 20f+id*220, 20f, 0);
        button.GetComponent<SelfDelete>().id = id;
        Debug.Log(button.GetComponent<SelfDelete>().id);
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
        }
    }

    public void deleteItemFromList(int id)
    {
        commandList.RemoveAt(id);
        displayQueue();

    }

    public void deletequeue()
    {
        commandList.Clear();
        displayQueue();
    }
}
