using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
public class MessageListView : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField] private InputMessageForm InputMessageForm;
    [SerializeField] private Transform MessagesContainer;

    private List<IMessageElementView> _messages = new List<IMessageElementView>();
    public Action<string> OnSubmitMessage
    {
        get => InputMessageForm.OnNewMessageEnter;
        set => InputMessageForm.OnNewMessageEnter = value;
    }

    public void AddMessage(IMessageElementView messageElementView)
    {
        _messages.Add(messageElementView);
        messageElementView.Transform.SetParent(MessagesContainer);
    }

    public void ClearMessages()
    {
        foreach (var messageElementView in _messages)
        {
            messageElementView.Destroy();
        }
        _messages.Clear();
        foreach (Transform message in MessagesContainer)
        {
            Destroy(message.gameObject);
        }
    }
    
    public void ChangeTextColorForUser(string username, string newColor)
    {
        foreach (var messageElementView in _messages)
        {
            if (messageElementView is MessageTextElementView messageText)
            {
                if (messageText.SenderUsername == username)
                {
                    messageText.SetTextColor(newColor);
                }
            }
        }
    }
}