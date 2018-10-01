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
        string s = "";
        for (int i = 0; i<commandList.Count; i++) {
            switch (commandList[i]) {
                case commands.UP: s +="U"; break;
                case commands.DOWN: s += "D"; break;
                case commands.RIGHT: s += "R"; break;
                case commands.LEFT: s += "L"; break;
            }
        }
        queueText.text = s;
    }

    public void confirmQueue() {
        Debug.LogError("not implemented yet");
    }
}
