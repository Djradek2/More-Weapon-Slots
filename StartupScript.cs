using Bannerlord.UIExtenderEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MoreWeaponSlots
{
    internal class StartupScript : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            new Harmony("WeaponSlotPatch").PatchAll();
            base.OnSubModuleLoad();
            //UIExtender uiExtender = new UIExtender("SurrenderTweaks");
            //uiExtender.Register(typeof(StartupScript).Assembly);
            //uiExtender.Enable();
        }

        // Token: 0x06000007 RID: 7 RVA: 0x000021A8 File Offset: 0x000003A8
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            //InformationManager.DisplayMessage(new InformationMessage("All the units shall look the same!"));
        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
            if (starterObject is CampaignGameStarter starter)
            {
                starter.AddBehavior(SlotManager.Instance);
            }
        }
    }
}
