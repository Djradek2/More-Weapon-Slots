using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using static TaleWorlds.Core.Equipment; //.GetRandomizedEquipment
using SandBox.GauntletUI;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;
using TaleWorlds.MountAndBlade;

namespace MoreWeaponSlots
{
    //[HarmonyPatch(typeof(Equipment), "GetRandomEquipmentElements")]
    //internal class MainParty
    //{
    //    // Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
    //    public static bool Prefix(ref Equipment __result, BasicCharacterObject character, bool randomEquipmentModifier, bool isCivilianEquipment = false, int seed = -1)
    //    {
    //        //MethodInfo hmm = typeof(Equipment).GetMethod("GetRandomizedEquipment", BindingFlags.NonPublic | BindingFlags.Instance);
    //        var EquipmentGetter = AccessTools.Method(typeof(Equipment), "GetRandomizedEquipment");
    //        Equipment equipment = new Equipment(isCivilianEquipment);
    //        List<Equipment> list = (from eq in character.AllEquipments
    //                                where eq.IsCivilian == isCivilianEquipment && !eq.IsEmpty()
    //                                select eq).ToList<Equipment>();
    //        if (list.IsEmpty<Equipment>())
    //        {
    //            __result = equipment;
    //            return false;
    //        }
    //        int count = list.Count;
    //        Random random = new Random(seed);
    //        int weaponSetNo = 0; //MBRandom.RandomInt(count);
    //        int weaponSetNo2 = 0; //MBRandom.RandomInt(count);
    //        int weaponSetNo3 = 0; //MBRandom.RandomInt(count);
    //        for (int i = 0; i < 12; i++)
    //        {
    //            if (seed != -1)
    //            {
    //                weaponSetNo = 0; //random.Next() % count;
    //                weaponSetNo2 = 0; //random.Next() % count;
    //                weaponSetNo3 = 0; //random.Next() % count;
    //            }
    //            if (i > 1)
    //            {
    //                if (i - 2 > 1)
    //                {
    //                    equipment[i] = (EquipmentElement)EquipmentGetter.Invoke(null, new object[] {list, (EquipmentIndex)i, weaponSetNo3, randomEquipmentModifier });
    //                }
    //                else
    //                {
    //                    equipment[i] = (EquipmentElement)EquipmentGetter.Invoke(null, new object[] {list, (EquipmentIndex)i, weaponSetNo2, randomEquipmentModifier });
    //                }
    //            }
    //            else
    //            {
    //                equipment[i] = (EquipmentElement)EquipmentGetter.Invoke(null, new object[] {list, (EquipmentIndex)i, weaponSetNo, randomEquipmentModifier });
    //            }
    //        }
    //        __result = equipment;
    //        return false;
    //    }
    //}

    [HarmonyPatch]
    internal class InventoryPatches
    {
        //static WeaponGUIData globalTextField1;

        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(GauntletInventoryScreen), "OnInitialize")]
        //public static void OpenPatch(GauntletInventoryScreen __instance) //ref string __result, 
        //{
        //    //var addTo = Traverse.Create(__instance).Field("_gauntletLayer").GetValue();
        //    //var to_add = new GauntletLayer(1)
        //    //{
        //    //    IsFocusLayer = true
        //    //};

        //    //WeaponSlotGUI _dataSource = new WeaponSlotGUI();
        //    //Traverse.Create(__instance).Field("_gauntletLayer").Method("LoadMovie", "WeaponSlotHUD", _dataSource);
        //    //InformationManager.DisplayMessage(new InformationMessage("Patched the OnInit method!"));

        //    //globalTextField1 = new WeaponGUIData(); //move to startup function
        //    //ScreenManager.AddGlobalLayer(globalTextField1, true);
        //    //ScreenManager.TrySetFocus(globalTextField1._gauntletLayer);

        //    //globalTextField1 = new WeaponGUIData();
        //    //ScreenManager.TopScreen.AddLayer(globalTextField1._gauntletLayer); //ReplaceTopScreen
        //    //ScreenManager.TrySetFocus(globalTextField1._gauntletLayer); //maybe?

        //    SlotManager slots = SlotManager.Instance;
        //    slots.Slot1Inquiry();
        //    InformationManager.DisplayMessage(new InformationMessage("Patched the OnInit method!"));
        //}

        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(GauntletInventoryScreen), "OnFinalize")]
        //public static void ClosePatch(GauntletInventoryScreen __instance) //ref string __result, 
        //{
        //    InformationManager.DisplayMessage(new InformationMessage("Disabled the OnInit method!"));
        //    //globalTextField1.OnFinalize(); //remove this after i move the startup
        //    //ScreenManager.RemoveGlobalLayer(globalTextField1);
        //}

        [HarmonyPostfix]
        [HarmonyPatch(typeof(GauntletInventoryScreen), "OnFrameTick")]
        public static void HotkeyPatch(GauntletInventoryScreen __instance)
        {
            if (!Convert.ToBoolean(Traverse.Create(__instance).Field("_dataSource").Method("IsSearchAvailable").GetValue()) || !Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Method("IsFocusedOnInput").GetValue()))
            {
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.Numpad5).GetValue()))   
                {
                    SlotManager.Instance.Slot1Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.Numpad6).GetValue()))
                {
                    SlotManager.Instance.Slot2Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.Numpad7).GetValue()))
                {
                    SlotManager.Instance.Slot3Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.Numpad8).GetValue()))
                {
                    SlotManager.Instance.Slot4Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.Numpad9).GetValue()))
                {
                    SlotManager.Instance.Slot5Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.D5).GetValue()))
                {
                    SlotManager.Instance.Slot1Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.D6).GetValue()))
                {
                    SlotManager.Instance.Slot2Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.D7).GetValue()))
                {
                    SlotManager.Instance.Slot3Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.D8).GetValue()))
                {
                    SlotManager.Instance.Slot4Inquiry();
                    return;
                }
                if (Convert.ToBoolean(Traverse.Create(__instance).Field("_gauntletLayer").Property("Input").Method("IsKeyPressed", TaleWorlds.InputSystem.InputKey.D9).GetValue()))
                {
                    SlotManager.Instance.Slot5Inquiry();
                    return;
                }
            }
        }
    }
}
