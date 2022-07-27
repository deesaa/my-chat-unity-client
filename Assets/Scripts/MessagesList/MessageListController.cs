using ChatClient;
using JsonMessage.DTO;
using UnityEngine;


public class MessageListController : MonoBehaviour
{
    [SerializeField] private MessageTextElementView messageTextElementViewPrefab;
    [SerializeField] private MessageUserStateElementView messageUserStateElementViewPrefab;
    [SerializeField] private MessageListView messageList;
    private ChatConnection _chatConnection;
    
    public void Install(ChatConnection chatConnection)
    {
        if (_chatConnection != null)
        {
            Debug.LogWarning("Already installed");
            return;
        }
        
        _chatConnection = chatConnection;
        
        messageList.OnSubmitMessage = (message) =>
        {
            _chatConnection.WriteMessage(message);
        };

        UnityChatListener.OnMessageFromServerEvent += OnNewMessage;
        UnityChatListener.OnOtherClientConnectedEvent += OnUserJoined;
        UnityChatListener.OnOtherClientDisconnectedEvent += OnUserLeft;
        UnityChatListener.OnUserChangedTextColorEvent += OnUserTextColorChanged;
        UnityChatListener.OnLoginSuccessEvent += OnLoginSuccess;
        
        messageList.ClearMessages();
    }

    private void OnLoginSuccess()
    {
        PullMessagesFromServerRequest();
    }

    private void PullMessagesFromServerRequest()
    {
        messageList.ClearMessages();
        _chatConnection.RequestLastMessages(10);
    }

    private void OnNewMessage(MessageDto message)
    {
        MessageTextElementView messageElement = MessageTextElementView.Create(messageTextElementViewPrefab, message);
        messageList.AddMessage(messageElement);
    }

    private void OnUserJoined(string username)
    {
        MessageUserStateElementView messageElement = MessageUserStateElementView.Create(messageUserStateElementViewPrefab, username, true);
        messageList.AddMessage(messageElement);
    }

    private void OnUserLeft(string username)
    {
        MessageUserStateElementView messageElement = MessageUserStateElementView.Create(messageUserStateElementViewPrefab, username, false);
        messageList.AddMessage(messageElement);
    }

    private void OnUserTextColorChanged(string username, string newColor)
    {
        messageList.ChangeTextColorForUser(username, newColor);
    }

    private void OnDestroy()
    {
        UnityChatListener.OnMessageFromServerEvent -= OnNewMessage;
        UnityChatListener.OnOtherClientConnectedEvent -= OnUserJoined;
        UnityChatListener.OnOtherClientDisconnectedEvent -= OnUserLeft;
        UnityChatListener.OnUserChangedTextColorEvent -= OnUserTextColorChanged;
        UnityChatListener.OnLoginSuccessEvent -= OnLoginSuccess;
    }
}