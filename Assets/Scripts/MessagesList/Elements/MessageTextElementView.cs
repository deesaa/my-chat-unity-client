using System;
using JsonMessage.DTO;
using TMPro;
using UnityEngine;

public class MessageTextElementView : MonoBehaviour, IMessageElementView
{
    public Transform Transform => transform;

    public ColorBase ColorBase;
    
    [SerializeField] private TMP_Text DateTime;
    [SerializeField] private TMP_Text Message;
    [SerializeField] private TMP_Text UsernameText;

    public string SenderUsername => UsernameText.text;

    public void SetTextColor(string color)
    {
        Message.color = ColorBase.GetColor(color);
    }

    public void SetMessageText(string text)
    {
        Message.text = text;
    }

    public void SetUsernameText(string username)
    {
        UsernameText.text = username;
    }

    public void SetMessageDate(DateTime dateTime)
    {
        DateTime.text = dateTime.ToShortDateString();
    }

    public static MessageTextElementView Create(MessageTextElementView prefab, string username, string text, DateTime dateTime, string textColor)
    {
        var instance = Instantiate(prefab);
        instance.SetUsernameText(username);
        instance.SetMessageText(text);
        instance.SetMessageDate(dateTime);
        instance.SetTextColor(textColor);
        return instance;
    }
    
    public void Destroy()
    {
        Destroy(gameObject.transform);
    }

    public static MessageTextElementView Create(MessageTextElementView prefab, MessageDto message)
    {
        return Create(prefab, message.User.Username, message.Message, message.UtcTime.ToLocalTime(),
            message.User.Color);
    }
}