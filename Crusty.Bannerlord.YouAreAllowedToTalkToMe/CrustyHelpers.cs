using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;


namespace Crusty.Bannerlord.YouAreAllowedToTalkToMe
{
    public static class CrustyHelpers
    {
        public static void SwitchEquipment(CharacterObject characterObject, string itemId,
            EquipmentIndex equipmentIndex)
        {
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment equipment = characterObject.Equipment.Clone(false);
            equipmentList.Add(equipment);
            ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>(itemId);
            equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, new EquipmentElement(itemObject1));
            InformationManager.DisplayMessage(new InformationMessage(itemObject1.StringId.ToString()));
        }

        public static void ChangeOutfit(CharacterObject characterObject, bool noWeapons)
        {
            if (noWeapons == true)
            {
                Equipment outfit = CharacterObject.PlayerCharacter.Equipment.Clone(true);
                characterObject.Equipment.FillFrom(outfit);
            }
            else
            {
                Equipment outfit = CharacterObject.PlayerCharacter.Equipment.Clone();
                characterObject.Equipment.FillFrom(outfit);
            }
        }

        public static void OutMsg(string s)
        {
            InformationManager.DisplayMessage(new InformationMessage(s));
        }
    }
}
