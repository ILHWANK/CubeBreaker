 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public void ChangeColor()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
