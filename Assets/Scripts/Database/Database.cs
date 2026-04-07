using System;
using System.Threading.Tasks;
using UnityEngine;

using Client = Supabase.Client;

public static class Database
{
    private static Client supabase;
    private static Task<Client> initializer;
    private static bool production;

    public static Task<Client> GetClientAsync()
    {
        production = true; // Set this to false to use local env vars for development
        
        if (supabase != null) return Task.FromResult(supabase);

        if (initializer != null) return initializer;

        initializer = InitializeSupabaseAsync();
        return initializer;
    }

    private static async Task<Client> InitializeSupabaseAsync()
    {

        string SUPABASE_URL = null;
        string SUPABASE_PUBLIC_KEY = null;

        if (production)
        {
            SUPABASE_URL = "https://muvurbxhnhflhmswaufs.supabase.co";
            SUPABASE_PUBLIC_KEY = "sb_publishable_hoN8B7s9R9erboOU-gTmqg_08SC-139";
        }
        else
        {
            var env = EnvLoader.Load();
            SUPABASE_URL = env["SUPABASE_URL"];
            SUPABASE_PUBLIC_KEY = env["SUPABASE_PUBLIC_KEY"];
        }

        var client = new Client(SUPABASE_URL, SUPABASE_PUBLIC_KEY);
        await client.InitializeAsync();
        supabase = client;
        Debug.Log("Supabase client initialized in Database singleton");
        return supabase;
    }
}
