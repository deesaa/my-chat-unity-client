using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UsersListView : MonoBehaviour
{
    [SerializeField] private Transform UsersContainer;
    private List<UserListUserInfoView> _users = new List<UserListUserInfoView>();
    public void AddUserInfo(UserListUserInfoView userInfoView)
    {
        _users.Add(userInfoView);
        userInfoView.transform.SetParent(UsersContainer);
    }

    public void Clear()
    {
        foreach (var user in _users)
        {
            user.Destroy();
        }
        _users.Clear();
        foreach (Transform t in UsersContainer)
        {
            Destroy(t.gameObject);
        }
    }

    public bool Contains(string username)
    {
        return _users.Exists(user => user.Username == username);
    }

    public void SetUserOnline(string username)
    {
        foreach (var user in _users.Where(user => user.Username == username))
        {
            user.SetOnline(true);
        }
    }
    
    public void SetUserOffline(string username)
    {
        foreach (var user in _users.Where(user => user.Username == username))
        {
            user.SetOnline(false);
        }
    }
}