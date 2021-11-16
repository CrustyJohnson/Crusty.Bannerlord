using System;
using TaleWorlds.Core;

namespace Crusty.Bannerlord.Helpers
{
public class CrustyHelpers
{

    public static void GivePlayerItem(string itemId, EquipmentIndex equipmentIndex)
    {
        ItemObject itemObject = Game.Current.ObjectManager.GetObject<ItemObject>(itemId);
        CharacterObject.PlayerCharacter.Equipment.GetEquipmentFromSlot(equipmentIndex)
        CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, new EquipmentElement(itemObject));
    }
    
    public static void Msg(string)
    {
        return InformationManager.DisplayMessage(new InformationMessage(string));
    }

}


