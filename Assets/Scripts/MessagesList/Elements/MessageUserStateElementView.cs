using System;
using TMPro;
using UnityEngine;

public class MessageUserStateElementView : MonoBehaviour, IMessageElementView
{
    public Transform Transform => transform;
    public void Destroy()
    {
        Destroy(gameObject.transform);
    }

    [SerializeField] private TMP_Text Text;
    private string _username;

    public void SetJoin(string username)
    {
        _username = username;
        Text.text = $"{_username} JUST JOINED - WELCOME BUDDY";
    }
    
    public void SetLeft(string username)
    {
        _username = username;
        Text.text = $"{_username} JUST LEFT - BYE BYE";
    }

    public static MessageUserStateElementView Create(MessageUserStateElementView prefab, string username, bool isJoined)
    {
        var instance = Instantiate(prefab);
        if(isJoined)
            instance.SetJoin(username);
        else
            instance.SetLeft(username);
        return instance;
    }
}