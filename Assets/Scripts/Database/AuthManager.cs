using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

using Client = Supabase.Client;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text feedbackText;

    // Supabase connection as a singleton
    private Client supabase;

    private void SetFeedback(string message)
    {
        if (feedbackText != null)
            feedbackText.text = message;
    }
   private async void Start()
    {
        try
        {
            supabase = await Database.GetClientAsync();
            SetFeedback("Database connected.");
        }
        catch (Exception e)
        {
            Debug.LogError("Supabase init failed: " + e.Message);
            SetFeedback("Database connection failed.");
        }
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

    public async void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string message = null;

        try
        {
            var response = await supabase.Auth.SignIn(email, password);
            Debug.Log("User logged in: " + response.User.Email);

            string userId = response.User.Id;
            PlayerProfileManager.Instance.InitializeForUser(userId);
            SceneManager.LoadScene("Loading");
        }
        catch (Exception e)
        {
            message = "Login failed: " + e.Message;
            Debug.LogError(message);
        }

        if (feedbackText != null && message != null)
        {
            feedbackText.text = message;
        }
    }
}
