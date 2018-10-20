using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLoader : MonoBehaviour {
    public GameObject player1Model;
    public GameObject player2Model;

    private void Start() {
        player1Model.SetActive(false);
        player2Model.SetActive(false);
    }

    public void showModel (int id) {
        switch (id) {
            case 0:
                player1Model.SetActive(false);
                player2Model.SetActive(false);
                break;
            case 1:
                player1Model.SetActive(true);
                player2Model.SetActive(false);
                break;
            case 2:
                player1Model.SetActive(false);
                player2Model.SetActive(true);
                break;
        }
    }
}
