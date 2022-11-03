using AtO_Loader.Patches.CustomDataLoader;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using static Enums;

namespace AtO_Loader.OnlyCustomCardRewards.Patches;

[HarmonyPatch(typeof(CardCraftManager), "ShowListCardsForCraft")]
public static class CardCraftManager
{
    private static Dictionary<CardClass, List<string>> cardListNotUpgradedByClass;
    private static Dictionary<CardClass, List<string>> cardListByClass;

    [HarmonyPrefix]
    public static void OverwriteCardPool()
    {
        cardListByClass = Globals.Instance.CardListByClass;
        cardListNotUpgradedByClass = Globals.Instance.CardListNotUpgradedByClass;
        var tempDict = CreateCardClonesPrefix.CustomCards.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Id).ToList());
        Globals.Instance.CardListByClass = tempDict;
        Globals.Instance.CardListNotUpgradedByClass = tempDict;
    }

    [HarmonyPostfix]
    public static void RevertCardPool()
    {
        Globals.Instance.CardListNotUpgradedByClass = cardListNotUpgradedByClass;
        Globals.Instance.CardListByClass = cardListByClass;
    }
}

