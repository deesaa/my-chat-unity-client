using TMPro;
using UnityEngine;

public class UserListUserInfoView : MonoBehaviour
{
    public string Username => UsernameText.text;
    
    [SerializeField] private TMP_Text UsernameText;
    [SerializeField] private Transform OnlineView;
    [SerializeField] private Transform OfflineView;
    private bool _isOnline;
    
    public void SetOnline(bool isOnline)
    {
        _isOnline = isOnline;
        OnlineView.gameObject.SetActive(_isOnline);
        OfflineView.gameObject.SetActive(!_isOnline);
    }

    public void SetName(string username)
    {
        UsernameText.text = username;
    }
    
    public static UserListUserInfoView Create(UserListUserInfoView prefab, string username, bool isOnline)
    {
        var instance = Instantiate(prefab);
        instance.SetOnline(isOnline);
        instance.SetName(username);
        return instance;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}