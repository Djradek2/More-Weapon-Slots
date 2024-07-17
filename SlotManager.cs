using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;


namespace MoreWeaponSlots
{
    public class SlotSaveTypes : SaveableTypeDefiner
    {
        public SlotSaveTypes() : base(771045) { }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(SlotManager), 1, null);
        }

    }

    public class SlotManager : CampaignBehaviorBase
    {
        //[SaveableField(1)]
        private static SlotManager instance = null;
        //[SaveableField(2)]
        public string[] slots = new string[5];

        private SlotManager()
        {

        }

        public override void SyncData(IDataStore dataStore) //literally just never runs
        {
            //dataStore.SyncData("instance", ref instance);
            //dataStore.SyncData("slots", ref slots);

            var dataToSave = new List<string>();
            if (dataStore.IsSaving)
            {
                dataToSave = Instance.slots.ToList();
            }
            dataStore.SyncData("dataToSave", ref dataToSave);
            if (dataStore.IsLoading)
            {
                Instance.slots = dataToSave.ToArray();
            }
        }

        public override void RegisterEvents() { }

        public static SlotManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SlotManager(); //this will load from saves later
                }
                return instance;
            }
        }

        public void Slot1Inquiry()
        {
            var textInquiryData = new TextInquiryData(
            new TextObject("Input Item Name (5):").ToString(),
            "",
            true,
            true,
            "Set",
            "Cancel",
            new Action<string>(Slot1Setter),
            null);
            InformationManager.ShowTextInquiry(textInquiryData, true);
        }
        public void Slot2Inquiry()
        {
            var textInquiryData = new TextInquiryData(
            new TextObject("Input Item Name (6):").ToString(),
            "",
            true,
            true,
            "Set",
            "Cancel",
            new Action<string>(Slot2Setter),
            null);
            InformationManager.ShowTextInquiry(textInquiryData, true);
        }
        public void Slot3Inquiry()
        {
            var textInquiryData = new TextInquiryData(
            new TextObject("Input Item Name (7):").ToString(),
            "",
            true,
            true,
            "Set",
            "Cancel",
            new Action<string>(Slot3Setter),
            null);
            InformationManager.ShowTextInquiry(textInquiryData, true);
        }
        public void Slot4Inquiry()
        {
            var textInquiryData = new TextInquiryData(
            new TextObject("Input Item Name (8):").ToString(),
            "",
            true,
            true,
            "Set",
            "Cancel",
            new Action<string>(Slot4Setter),
            null);
            InformationManager.ShowTextInquiry(textInquiryData, true);
        }
        public void Slot5Inquiry()
        {
            var textInquiryData = new TextInquiryData(
            new TextObject("Input Item Name (9):").ToString(),
            "",
            true,
            true,
            "Set",
            "Cancel",
            new Action<string>(Slot5Setter),
            null);
            InformationManager.ShowTextInquiry(textInquiryData, true);
        }
        public void Slot1Setter(string data)
        {
            data = data.Trim();
            string potentialModifier = Regex.Match(data, @"^([\w\-]+)").Value;
            if(potentialModifier == "Legendary" || potentialModifier == "Masterwork" || potentialModifier == "Balanced" || potentialModifier == "Dull" || potentialModifier == "Rusty"
            || potentialModifier == "Cracked" || potentialModifier == "Splintered" || potentialModifier == "Tuned" || potentialModifier == "Bent" || potentialModifier == "Unbalanced"
            || potentialModifier == "Quality" || potentialModifier == "Dented" || potentialModifier == "Lordly" || potentialModifier == "Thick" || potentialModifier == "Battered"
            || potentialModifier == "Fine") //will not protect against "Well made" and "Large Bag of"
            {
                string firstWord = Regex.Match(data, @"^([\w\-]+)").Value.Trim();
                data = data.Substring(firstWord.Length).Trim();
            }

            slots[0] = data;
            InformationManager.DisplayMessage(new InformationMessage(data));
        }

        public void Slot2Setter(string data)
        {
            data = data.Trim();
            string potentialModifier = Regex.Match(data, @"^([\w\-]+)").Value;
            if (potentialModifier == "Legendary" || potentialModifier == "Masterwork" || potentialModifier == "Balanced" || potentialModifier == "Dull" || potentialModifier == "Rusty"
            || potentialModifier == "Cracked" || potentialModifier == "Splintered" || potentialModifier == "Tuned" || potentialModifier == "Bent" || potentialModifier == "Unbalanced"
            || potentialModifier == "Quality" || potentialModifier == "Dented" || potentialModifier == "Lordly" || potentialModifier == "Thick" || potentialModifier == "Battered"
            || potentialModifier == "Fine") //will not protect against "Well made" and "Large Bag of"
            {
                string firstWord = Regex.Match(data, @"^([\w\-]+)").Value.Trim();
                data = data.Substring(firstWord.Length).Trim();
            }

            slots[1] = data;
            InformationManager.DisplayMessage(new InformationMessage(data));
        }

        public void Slot3Setter(string data)
        {
            data = data.Trim();
            string potentialModifier = Regex.Match(data, @"^([\w\-]+)").Value;
            if (potentialModifier == "Legendary" || potentialModifier == "Masterwork" || potentialModifier == "Balanced" || potentialModifier == "Dull" || potentialModifier == "Rusty"
            || potentialModifier == "Cracked" || potentialModifier == "Splintered" || potentialModifier == "Tuned" || potentialModifier == "Bent" || potentialModifier == "Unbalanced"
            || potentialModifier == "Quality" || potentialModifier == "Dented" || potentialModifier == "Lordly" || potentialModifier == "Thick" || potentialModifier == "Battered"
            || potentialModifier == "Fine") //will not protect against "Well made" and "Large Bag of"
            {
                string firstWord = Regex.Match(data, @"^([\w\-]+)").Value.Trim();
                data = data.Substring(firstWord.Length).Trim();
            }

            slots[2] = data;
            InformationManager.DisplayMessage(new InformationMessage(data));
        }

        public void Slot4Setter(string data)
        {
            data = data.Trim();
            string potentialModifier = Regex.Match(data, @"^([\w\-]+)").Value;
            if (potentialModifier == "Legendary" || potentialModifier == "Masterwork" || potentialModifier == "Balanced" || potentialModifier == "Dull" || potentialModifier == "Rusty"
            || potentialModifier == "Cracked" || potentialModifier == "Splintered" || potentialModifier == "Tuned" || potentialModifier == "Bent" || potentialModifier == "Unbalanced"
            || potentialModifier == "Quality" || potentialModifier == "Dented" || potentialModifier == "Lordly" || potentialModifier == "Thick" || potentialModifier == "Battered"
            || potentialModifier == "Fine") //will not protect against "Well made" and "Large Bag of"
            {
                string firstWord = Regex.Match(data, @"^([\w\-]+)").Value.Trim();
                data = data.Substring(firstWord.Length).Trim();
            }

            slots[3] = data;
            InformationManager.DisplayMessage(new InformationMessage(data));
        }

        public void Slot5Setter(string data)
        {
            data = data.Trim();
            string potentialModifier = Regex.Match(data, @"^([\w\-]+)").Value;
            if (potentialModifier == "Legendary" || potentialModifier == "Masterwork" || potentialModifier == "Balanced" || potentialModifier == "Dull" || potentialModifier == "Rusty"
            || potentialModifier == "Cracked" || potentialModifier == "Splintered" || potentialModifier == "Tuned" || potentialModifier == "Bent" || potentialModifier == "Unbalanced"
            || potentialModifier == "Quality" || potentialModifier == "Dented" || potentialModifier == "Lordly" || potentialModifier == "Thick" || potentialModifier == "Battered"
            || potentialModifier == "Fine") //will not protect against "Well made" and "Large Bag of"
            {
                string firstWord = Regex.Match(data, @"^([\w\-]+)").Value.Trim();
                data = data.Substring(firstWord.Length).Trim();
            }

            slots[4] = data;
            InformationManager.DisplayMessage(new InformationMessage(data));
        }
    }
}
