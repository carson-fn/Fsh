using UnityEngine;

public class BiomeScreenLogic : MonoBehaviour
{
    public void setBiome(int biome)
    {
        Debug.Log($"PICKING BIOME {biome}\n\n");
        LogicScript.setBiome(biome);
    }
}
