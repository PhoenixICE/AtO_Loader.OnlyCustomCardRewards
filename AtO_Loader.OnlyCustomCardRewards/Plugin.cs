using AtO_Loader.OnlyCustomCardRewards.Patches;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AtO_Loader.OnlyCustomCardRewards;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public partial class Plugin : BaseUnityPlugin
{
    private const string ModGUID = "IcyPhoenix.OnlyCustomCardRewards";
    private const string ModName = "AtO_Loader.OnlyCustomCardRewards";
    private const string ModVersion = "0.0.1.0";
    private readonly Harmony harmony = new(ModGUID);

    /// <summary>
    /// Gets or sets harmony Logger instance.
    /// </summary>
    internal static new ManualLogSource Logger { get; set; }

    private void Awake()
    {
        Plugin.Logger = base.Logger;
        this.harmony.PatchAll(typeof(SetRewards));
        this.harmony.PatchAll(typeof(ShowListCardsForCraft));
    }
}
