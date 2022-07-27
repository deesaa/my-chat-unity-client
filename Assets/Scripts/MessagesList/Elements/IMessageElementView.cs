using UnityEngine;

public interface IMessageElementView
{
    public Transform Transform { get; }
    void Destroy();
}