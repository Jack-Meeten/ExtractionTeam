using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI HeaderField;
    public TextMeshProUGUI ContentField;

    public LayoutElement LayoutElement;

    public int CharacterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            HeaderField.gameObject.SetActive(false);
        }
        else
        {
            HeaderField.gameObject.SetActive(true);
            HeaderField.text = header;
        }

        ContentField.text = content;

        int HeaderLength = HeaderField.text.Length;
        int ContentLength = ContentField.text.Length;

        LayoutElement.enabled = (HeaderLength > CharacterWrapLimit || ContentLength > CharacterWrapLimit) ? true : false;
    }

    private void Update()
    {
        if (Application.isEditor)
        {

            int HeaderLength = HeaderField.text.Length;
            int ContentLength = ContentField.text.Length;

            LayoutElement.enabled = (HeaderLength > CharacterWrapLimit || ContentLength > CharacterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
