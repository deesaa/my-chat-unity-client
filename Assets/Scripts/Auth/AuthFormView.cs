using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AuthFormView : MonoBehaviour
{
    private Animator _animator;
    
    [SerializeField] private TMP_InputField UsernameInputField;
    [SerializeField] private Text UserValidationFailText;
    [SerializeField] private TMP_InputField PasswordInputField;
    [SerializeField] private Text PasswordValidationFailText;
    [SerializeField] private Button SubmitButton;

    public Action<string, string> OnSubmitNamePass;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Reset");
        SubmitButton.onClick.AddListener(OnSubmit);
    }

    private void OnSubmit()
    {
        OnSubmitNamePass(UsernameInputField.text, PasswordInputField.text);
    }

    private void OnDestroy()
    {
        SubmitButton.onClick.RemoveListener(OnSubmit);
    }

    public async Task SetSuccess()
    {
        _animator.SetTrigger("Success");
        await Task.Delay(TimeSpan.FromSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
    }

    public async Task Close()
    {
        _animator.SetTrigger("Close");
        await Task.Delay(TimeSpan.FromSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
        gameObject.SetActive(false);
    }

    public async Task Reset()
    {
        _animator.SetTrigger("Reset");
    }

    public async Task Open()
    {
        _animator.SetTrigger("Open");
        gameObject.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
    }

    public async Task ShowLoginFail()
    {
        _animator.SetTrigger("Fail");
        await Task.Delay(TimeSpan.FromSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
    }
}

