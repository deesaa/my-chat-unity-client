using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InputMessageForm : MonoBehaviour
{
    [SerializeField] private Button EnterButton;
    [SerializeField] private TMP_InputField InputField;
    public Action<string> OnNewMessageEnter;

    private void Awake()
    {
        EnterButton.onClick.AddListener(TryEnterNewMessage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TryEnterNewMessage();
        }
    }

    private void TryEnterNewMessage()
    {
        string text = InputField.text;
        InputField.text = String.Empty;
        OnNewMessageEnter(text);
    }

    private void OnDestroy()
    {
        EnterButton.onClick.RemoveListener(TryEnterNewMessage);
    }
}
