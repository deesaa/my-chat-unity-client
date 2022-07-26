using System;
using System.Collections;
using System.Collections.Generic;
using ChatClient;
using JsonMessage.DTO;
using UnityEngine;



public class UnityChatListener : MonoBehaviour, IChatListener
{
    public static event Action OnLoginSuccessEvent;
    public static event Action OnServerConnectionRetryEvent;
    public static event Action OnLoginFailEvent;
    public static event Action<MessageDto> OnMessageFromServerEvent;
    public static event Action OnServerConnectedEvent;
    public static event Action OnServerDisconnectedEvent;
    public static event Action<UserDto[]> OnUsersOnServerListEvent;
    public static event Action<string> OnOtherClientConnectedEvent;
    public static event Action<string> OnOtherClientDisconnectedEvent;
    public static event Action<string, string> OnUserChangedTextColorEvent;

    public void OnMessageFromServer(MessageDto message) =>
        UnityMainThreadDispatcher.Instance().Enqueue(() => OnMessageFromServerEvent?.Invoke(message));

    public void OnServerConnected() => 
        UnityMainThreadDispatcher.Instance().Enqueue(() => OnServerConnectedEvent?.Invoke());

    public void OnServerDisconnected() => 
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnServerDisconnectedEvent?.Invoke());

    public void OnUsersOnServerList(UserDto[] users) => 
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnUsersOnServerListEvent?.Invoke(users));
    
    public void OnOtherClientConnected(string username) =>
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnOtherClientConnectedEvent?.Invoke(username));

    public void OnOtherClientDisconnected(string username) =>
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnOtherClientDisconnectedEvent?.Invoke(username));

    public void OnUserChangedTextColor(string username, string newTextColor) =>
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnUserChangedTextColorEvent?.Invoke(username, newTextColor));
    
    public void OnLoginSuccess() => 
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnLoginSuccessEvent?.Invoke());

    public void OnLoginFail() => 
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnLoginFailEvent?.Invoke());
    
    public void OnServerConnectionRetry() => 
        UnityMainThreadDispatcher.Instance().Enqueue(() =>OnServerConnectionRetryEvent?.Invoke());
}