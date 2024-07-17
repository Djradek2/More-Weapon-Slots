using HarmonyLib;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Missions;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.CampaignSystem.Party;

namespace MoreWeaponSlots
{
    [DefaultView]
    public class ExtraWeaponSlots : MissionView
    {
        public short[] consumed = new short[5];
        public bool[] nondefault = new bool[5];
        public string[] currentSlots = new string[5];
        //public string[] currentModifiers = new string[5]; //TODO (unimplemented)

        protected void NonspawnedItemPickup(int index) //Agent playerAgent, MissionWeapon spawnedItemEntity, EquipmentIndex slotIndex
        {
            //MissionWeapon missionWeapon = new MissionWeapon(MBObjectManager.Instance.GetObject<ItemObject>("peasant_pitchfork_2_t1"), null, null);
            ItemObject ItemHolder = new ItemObject();
            try
            {
                ItemHolder = TaleWorlds.CampaignSystem.CampaignCheats.GetItemObject(currentSlots[index]); //Piercing Arrows, Heavy Round Shield, Pitchfork
                if(MobileParty.MainParty.ItemRoster.GetItemNumber(ItemHolder) == 0 && nondefault[index] == false)
                {
                    InformationManager.DisplayMessage(new InformationMessage("Item is not present in inventory"));
                    return;
                }
            } catch {
                InformationManager.DisplayMessage(new InformationMessage("Item Not Found"));
                return;
            }

            MissionWeapon missionWeapon = new MissionWeapon(ItemHolder, null, null);
            if (missionWeapon.HitPoints > 0) //missionWeapon.Item.PrimaryWeapon.IsConsumable <- this condition works only for arrows/bolts, not shields
            {
                //missionWeapon = missionWeapon.Consume(15); //(short)(missionWeapon.MaxAmmo - (short)15)
                missionWeapon.Consume(consumed[index]); //this will actually consume 15 arrows or shield HPs, seems .Amount is the current remaining, shields have their hitpoints in Amount and HitPoints but not Ammo, Pitchfork has everything as 0x0000, Arrows had their HitPoints and Amount reduced, Ammo is also 0x0000 and MaxAmmo is still 0x0017
            }
            try
            {
                if (Mission.Current.MainAgent.Equipment[3].Amount > 0)
                {
                    consumed[index] = (short)(Mission.Current.MainAgent.Equipment[3].MaxAmmo - Mission.Current.MainAgent.Equipment[3].Amount);
                }
                nondefault[index] = true;
                currentSlots[index] = Mission.Current.MainAgent.Equipment[3].Item.Name.ToString();
            } catch {
                currentSlots[index] = null;
            }
            try
            {
                Mission.Current.MainAgent.EquipWeaponWithNewEntity(EquipmentIndex.Weapon3, ref missionWeapon);
            } catch { }
        }

        public override void OnMissionScreenInitialize()
        {
            consumed = new short[5];
            nondefault = new bool[5];
            Array.Copy(SlotManager.Instance.slots, currentSlots, currentSlots.Length);
            base.OnMissionScreenInitialize();
        }

        public override void OnMissionScreenTick(float dt)
        {
            base.OnMissionScreenTick(dt);
            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.Numpad5)) //Numpad5
            {
                NonspawnedItemPickup(0);
                return;
            }
            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.Numpad6)) //Numpad5
            {
                NonspawnedItemPickup(1);
                return;
            }
            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.Numpad7)) //Numpad5
            {
                NonspawnedItemPickup(2);
                return;
            }
            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.Numpad8)) //Numpad5
            {
                NonspawnedItemPickup(3);
                return;
            }
            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.Numpad9)) //Numpad5
            {
                NonspawnedItemPickup(4);
                return;
            }
        }
    }
}
