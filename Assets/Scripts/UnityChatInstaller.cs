using ChatClient;
using ChatClient.Configuration;
using UnityEngine;

public static class ChatConfigurationFactory
{
    public static ChatConfiguration GetUnityConfiguration()
    {
        ChatConfiguration defaultConfiguration = new ChatConfiguration();
        return defaultConfiguration;
    }
}


public class UnityChatInstaller : MonoBehaviour
{
    private ChatConnection ChatConnection { get; set; }
    private IChatListener _chatListener;

    [SerializeField] private AuthFormController authFormController;
    [SerializeField] private MessageListController messageListController;
    [SerializeField] private UsersListController usersListController;
    private void Awake()
    {
        ChatConfiguration chatConfiguration = ChatConfigurationFactory.GetUnityConfiguration();
        ChatConnection = new ChatConnection(chatConfiguration);
        _chatListener = new GameObject("Chat Listener").AddComponent<UnityChatListener>();
        ChatConnection.AddListener(_chatListener);
        
        authFormController.Install(ChatConnection);
        messageListController.Install(ChatConnection);
        usersListController.Install(ChatConnection);
    }

    private void Start()
    {
        ChatConnection.BeginConnect();
    }
}