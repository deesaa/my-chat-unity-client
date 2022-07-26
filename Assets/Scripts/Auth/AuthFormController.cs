using System;
using ChatClient;
using Newtonsoft.Json;
using UnityEngine;

public class AuthFormController : MonoBehaviour
{
    [SerializeField] private AuthFormView AuthFormView;
    [SerializeField] private ServerConnectionStatusView ServerConnectionStatusView;
    private ChatConnection _chatConnection;

    public void Install(ChatConnection chatConnection)
    {
        if (_chatConnection != null)
        {
            Debug.LogWarning("Already installed");
            return;
        }
        
        _chatConnection = chatConnection;
        
        AuthFormView.OnSubmitNamePass = (name, password) =>
        {
            _chatConnection.TryEnterNamePass(name, password);
        };

        UnityChatListener.OnLoginSuccessEvent += OnLoginSuccess;
        UnityChatListener.OnLoginFailEvent += OnLoginFail;
        
        UnityChatListener.OnServerConnectionRetryEvent += OnServerConnectionRetry;
        UnityChatListener.OnServerDisconnectedEvent += OnServerDisconnected;
        UnityChatListener.OnServerConnectedEvent += OnServerConnected;
    }

    private void OnServerConnectionRetry() =>
        ServerConnectionStatusView.SetStatus(ServerConnectionStatusView.Status.TRYING_TO_CONNECT);
    
    private void OnServerDisconnected() =>
        ServerConnectionStatusView.SetStatus(ServerConnectionStatusView.Status.TRYING_TO_CONNECT);
    
    private void OnServerConnected() =>
        ServerConnectionStatusView.SetStatus(ServerConnectionStatusView.Status.CONNECTED);

    private async void OnLoginSuccess()
    {
        await AuthFormView.SetSuccess();
        await AuthFormView.Close();
    }
        
    private async void OnLoginFail()
    {
        await AuthFormView.Reset();
        await AuthFormView.Open();
        await AuthFormView.ShowLoginFail();
    }

    private void OnDestroy()
    {
        UnityChatListener.OnLoginSuccessEvent -= OnLoginSuccess;
        UnityChatListener.OnLoginFailEvent -= OnLoginFail;
        
        UnityChatListener.OnServerConnectionRetryEvent -= OnServerConnectionRetry;
        UnityChatListener.OnServerDisconnectedEvent -= OnServerDisconnected;
        UnityChatListener.OnServerConnectedEvent -= OnServerConnected;
    }
}