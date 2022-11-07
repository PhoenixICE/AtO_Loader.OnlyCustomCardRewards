using AtO_Loader.Patches;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Enums;

namespace AtO_Loader.OnlyCustomCardRewards.Patches;

[HarmonyPatch(typeof(RewardsManager), "SetRewards")]
public class SetRewards
{
    public static Dictionary<CardClass, List<string>> CardListNotUpgradedByClass;

    public static void OverwriteCardPool()
    {
        CardListNotUpgradedByClass = Globals.Instance.CardListNotUpgradedByClass;
        Globals.Instance.CardListNotUpgradedByClass = DeserializeCards.CustomCards.ToDictionary(x => x.Key, x => x.Value.Where(y => y.CardUpgraded == CardUpgraded.No).Select(y => y.Id).ToList());
    }

    public static void RevertCardPool()
    {
        Globals.Instance.CardListNotUpgradedByClass = CardListNotUpgradedByClass;
    }

    public static IEnumerator Postfix(IEnumerator __result)
    {
        OverwriteCardPool();

        while (__result.MoveNext()) yield return __result.Current;

        RevertCardPool();
    }
}