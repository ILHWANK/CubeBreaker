using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{

    public void OnOnputFeildTextChanged(string newText)
    {
        Debug.Log("타이핑 하는 중!");
        Debug.Log(newText);
    }
    public void OnInputFeildTextDone(string newText)
    {
        Debug.Log("타이핑 완료!");
        Debug.Log(newText);
    }
}
