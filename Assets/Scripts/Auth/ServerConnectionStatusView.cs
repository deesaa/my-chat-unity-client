using System;
using UnityEngine;

public class ServerConnectionStatusView : MonoBehaviour
{
    [SerializeField] private Transform TryingToConnectView;
    [SerializeField] private Transform ConnectedView;
    public enum Status
    {
        TRYING_TO_CONNECT,
        CONNECTED
    }

    private void DisableAll()
    {
        TryingToConnectView.gameObject.SetActive(false);
        ConnectedView.gameObject.SetActive(false);
    }

    public void SetStatus(Status status)
    {
        DisableAll();
        switch (status)
        {
            case Status.TRYING_TO_CONNECT:
                TryingToConnectView.gameObject.SetActive(true);
                break;
            case Status.CONNECTED:
                ConnectedView.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }
    }
}