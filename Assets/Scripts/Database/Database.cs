using System.Threading.Tasks;
using UnityEngine;

using Client = Supabase.Client;

public static class Database
{
    private static Client supabase;
    private static Task<Client> initializer;

    public static Task<Client> GetClientAsync()
    {
        if (supabase != null) return Task.FromResult(supabase);
        
        if (initializer != null) return initializer;

        initializer = InitializeSupabaseAsync();
        return initializer;
    }

    private static async Task<Client> InitializeSupabaseAsync()
    {
        var env = EnvLoader.Load();
        string SUPABASE_URL = env["SUPABASE_URL"];
        string SUPABASE_PUBLIC_KEY = env["SUPABASE_PUBLIC_KEY"];

        var client = new Client(SUPABASE_URL, SUPABASE_PUBLIC_KEY);
        await client.InitializeAsync();
        supabase = client;
        Debug.Log("Supabase client initialized in Database singleton");
        return supabase;
    }
}
