using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Color32 myColor;
    [SerializeField] Material myMaterial;
    private byte r, g, b, a = 255;
    private int multiplier = 100;
    private int moduloDivider = 255;


    // Update is called once per frame
    void Update()
    {
        r = (byte)((transform.position.x * multiplier) % moduloDivider);
        g = (byte)((transform.position.z * multiplier) % moduloDivider);
        b = (byte)(((transform.position.x + transform.position.z) * multiplier) % moduloDivider);
        myColor = new Color32(r,g,b,a);
        myMaterial.color = myColor;
    }
}
