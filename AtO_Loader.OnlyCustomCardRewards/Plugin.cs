using AtO_Loader.OnlyCustomCardRewards.Patches;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AtO_Loader.OnlyCustomCardRewards;

[BepInPlugin(modGUID, modName, ModVersion)]
public partial class Plugin : BaseUnityPlugin
{
    private const string modGUID = "IcyPhoenix.OnlyCustomCardRewards";
    private const string modName = "AtO_Loader.OnlyCustomCardRewards";
    private const string ModVersion = "0.0.1.0";
    private readonly Harmony harmony = new(modGUID);
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        Plugin.Logger = base.Logger;
        this.harmony.PatchAll(typeof(ShowListCardsForCraft));
    }
}
