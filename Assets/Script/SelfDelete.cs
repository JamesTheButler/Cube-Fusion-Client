using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDelete : MonoBehaviour {
    public int id;

    public void deleteOnClick() {
        FindObjectOfType<UIManager>().deleteItemFromList(this.id);
    }
}
