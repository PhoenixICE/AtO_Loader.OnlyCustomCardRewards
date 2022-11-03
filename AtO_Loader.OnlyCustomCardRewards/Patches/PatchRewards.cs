using AtO_Loader.Patches;
using AtO_Loader.Patches.CustomDataLoader;
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

    class EnumeratorWrapper : IEnumerable
    {
        public IEnumerator enumerator;
        public Action PrefixAction; 
        public Action PostfixAction;
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        public IEnumerator GetEnumerator()
        {
            PrefixAction();
            while (enumerator.MoveNext())
            {
                yield return null;
            }
            PostfixAction();
        }
    }

    public static void OverwriteCardPool()
    {
        CardListNotUpgradedByClass = Globals.Instance.CardListNotUpgradedByClass;
        Globals.Instance.CardListNotUpgradedByClass = CreateCardClonesPrefix.CustomCards.ToDictionary(x => x.Key, x => x.Value.Where(y => y.CardUpgraded == CardUpgraded.No).Select(y => y.Id).ToList());
    }

    public static void RevertCardPool()
    {
        Globals.Instance.CardListNotUpgradedByClass = CardListNotUpgradedByClass;
    }

    public static void Postfix(ref IEnumerator __result)
    {
        var myEnumerator = new EnumeratorWrapper()
        {
            enumerator = __result,
            PrefixAction = () => OverwriteCardPool(),
            PostfixAction = () => RevertCardPool(),
        };
        __result = myEnumerator.GetEnumerator();
    }
}