using AtO_Loader.Patches;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using static Enums;

namespace AtO_Loader.OnlyCustomCardRewards.Patches;

[HarmonyPatch(typeof(CorruptionManager), "InitCorruption")]
public class InitCorruption
{
    private static Dictionary<CardType, List<string>> cardListByType;

    [HarmonyPrefix]
    public static void OverwriteCardPool()
    {
        cardListByType = Globals.Instance.CardListByType;
        Globals.Instance.CardListByType = DeserializeCards.CustomCards.Values
            .SelectMany(x => x)
            .Where(x => x.CardUpgraded == CardUpgraded.Rare)
            .GroupBy(x => x.CardType)
            .ToDictionary(x => x.Key, x => x.Select(x => x.Id).ToList());
    }

    [HarmonyPostfix]
    public static void RevertCardPool()
    {
        Globals.Instance.CardListByType = cardListByType;
    }
}