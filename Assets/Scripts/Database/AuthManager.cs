using UnityEngine;
using System;
using TMPro;

using Client = Supabase.Client;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text feedbackText;

    // Supabase connection as a singleton
    private Client supabase;

    private async void Start()
    {
        supabase = await Database.GetClientAsync();
    }

    public async void RegisterNewUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string message = null;

        try
        {
            var response = await supabase.Auth.SignUp(email, password);
            Debug.Log("User registered: " + response.User.Email);
        }
        catch (Exception e)
        {
            message = "Registration failed: " + e.Message;
            Debug.LogError(message);
        }

        if (feedbackText != null && message != null)
        {
            feedbackText.text = message;
        }
    }
}
