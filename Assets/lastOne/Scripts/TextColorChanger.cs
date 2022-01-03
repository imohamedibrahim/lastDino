using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextColorChanger : MonoBehaviour
{
    [SerializeField]
    private float smoothnessInShading = 0.1f;
    private TextMeshProUGUI text;
    private Color leftCornerColor;
    private Color rightCornerColor;
    private float flag = 0.01f;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        leftCornerColor = text.colorGradient.topLeft;
        rightCornerColor = text.colorGradient.topRight;
    }

    void Update()
    {
        if(leftCornerColor.g >= text.colorGradient.topLeft.g)
        {
            flag = 0.1f * smoothnessInShading;
        }
        else if(text.colorGradient.topLeft.g >= 1)
        {
            flag = -0.1f  * smoothnessInShading;
        }
        text.colorGradient = new VertexGradient(new Color(text.colorGradient.topLeft.r, text.colorGradient.topLeft.g + flag, text.colorGradient.topLeft.b), new Color(text.colorGradient.topRight.r, text.colorGradient.topRight.g + flag, text.colorGradient.topRight.b), text.colorGradient.bottomLeft,text.colorGradient.bottomRight);
    }
}
