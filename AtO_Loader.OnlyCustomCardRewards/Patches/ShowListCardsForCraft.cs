using AtO_Loader.Patches;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using static Enums;

namespace AtO_Loader.OnlyCustomCardRewards.Patches;

[HarmonyPatch(typeof(CardCraftManager), "ShowListCardsForCraft")]
public static class ShowListCardsForCraft
{
    private static Dictionary<CardClass, List<string>> cardListNotUpgradedByClass;
    private static Dictionary<CardClass, List<string>> cardListByClass;

    [HarmonyPrefix]
    public static void OverwriteCardPool()
    {
        cardListByClass = Globals.Instance.CardListByClass;
        cardListNotUpgradedByClass = Globals.Instance.CardListNotUpgradedByClass;
        Globals.Instance.CardListByClass = DeserializeCards.CustomCards.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Id).ToList());
        Globals.Instance.CardListNotUpgradedByClass = DeserializeCards.CustomCards.ToDictionary(x => x.Key, x => x.Value.Where(y => y.CardUpgraded == CardUpgraded.No).Select(y => y.Id).ToList());
    }

    [HarmonyPostfix]
    public static void RevertCardPool()
    {
        Globals.Instance.CardListNotUpgradedByClass = cardListNotUpgradedByClass;
        Globals.Instance.CardListByClass = cardListByClass;
    }
}

