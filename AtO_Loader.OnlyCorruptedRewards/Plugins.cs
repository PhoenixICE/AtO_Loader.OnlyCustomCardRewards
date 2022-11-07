using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using static Enums;
using System.Collections.Generic;

namespace AtO_Loader.OnlyCustomCardRewards;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public partial class Plugin : BaseUnityPlugin
{
    private const string ModGUID = "IcyPhoenix.OnlyCorruptedRewards";
    private const string ModName = "AtO_Loader.OnlyCorruptedRewards";
    private const string ModVersion = "0.0.1";
    private readonly Harmony harmony = new(ModGUID);

    private void Awake()
    {
        this.harmony.PatchAll(typeof(GetCardByRarity));
    }
}

[HarmonyPatch(typeof(Functions), "GetCardByRarity")]
public class GetCardByRarity
{
    [HarmonyPostfix]
    public static void OverwriteCardPool(ref string __result, CardData _cardData)
    {
        __result = _cardData?.UpgradesToRare?.Id ?? __result;
    }
}
