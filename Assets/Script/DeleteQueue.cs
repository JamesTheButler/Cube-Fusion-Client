using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteQueue : MonoBehaviour {

    public void deleteQueue()
    {
        FindObjectOfType<UIManager>().deletequeue();

    } 
}
