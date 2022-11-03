using AtO_Loader.Patches.CustomDataLoader;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using static Enums;

namespace AtO_Loader.OnlyCustomCardRewards.Patches;

[HarmonyPatch(typeof(RewardsManager),  "SetRewards")]
public class SetRewards
{
    private static Dictionary<CardClass, List<string>> cardListNotUpgradedByClass;

    [HarmonyPrefix]
    public static void OverwriteCardPool()
    {
        cardListNotUpgradedByClass = Globals.Instance.CardListNotUpgradedByClass;
        Globals.Instance.CardListNotUpgradedByClass = CreateCardClonesPrefix.CustomCards.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Id).ToList());
    }

    [HarmonyPostfix]
    public static void RevertCardPool()
    {
        Globals.Instance.CardListNotUpgradedByClass = cardListNotUpgradedByClass;
    }
}

