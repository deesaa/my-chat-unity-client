using ChatClient;
using JsonMessage.DTO;
using UnityEngine;

public class UsersListController : MonoBehaviour
{
    [SerializeField] private UsersListView usersList;
    [SerializeField] private UserListUserInfoView userListUserInfoViewPrefab;
    private ChatConnection _chatConnection;

    public void Install(ChatConnection chatConnection)
    {
        if (_chatConnection != null)
        {
            Debug.LogWarning("Already installed");
            return;
        }
        
        _chatConnection = chatConnection;
        
        UnityChatListener.OnOtherClientConnectedEvent += OnUserJoined;
        UnityChatListener.OnOtherClientDisconnectedEvent += OnUserLeft;
        UnityChatListener.OnUsersOnServerListEvent += FillUsersData;
    }

    private void FillUsersData(UserDto[] users)
    {
        usersList.Clear();

        foreach (var user in users)
        {
            UserListUserInfoView userInfoView =
                UserListUserInfoView.Create(userListUserInfoViewPrefab, user.Username, user.IsOnline);
            usersList.AddUserInfo(userInfoView);
        }
    }

    private void OnUserJoined(string username)
    {
        if (usersList.Contains(username))
            usersList.SetUserOnline(username);
        else
        {
            UserListUserInfoView userInfoView =
                UserListUserInfoView.Create(userListUserInfoViewPrefab, username, true);
            usersList.AddUserInfo(userInfoView);
        }
    }

    private void OnUserLeft(string username)
    {
        if (usersList.Contains(username))
            usersList.SetUserOffline(username);
        else
        {
            UserListUserInfoView userInfoView =
                UserListUserInfoView.Create(userListUserInfoViewPrefab, username, true);
            usersList.AddUserInfo(userInfoView);
        }
    }

    private void OnDestroy()
    {
        UnityChatListener.OnOtherClientConnectedEvent -= OnUserJoined;
        UnityChatListener.OnOtherClientDisconnectedEvent -= OnUserLeft;
        UnityChatListener.OnUsersOnServerListEvent -= FillUsersData;
    }
}