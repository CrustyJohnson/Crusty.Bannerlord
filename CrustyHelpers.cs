using System;

namespace Crusty.Bannerlord.Helpers
{

    public static void GivePlayerItem(string itemId, EquipmentIndex equipmentIndex)
    {
        ItemObject itemObject = Game.Current.ObjectManager.GetObject<ItemObject>(itemId);
        CharacterObject.PlayerCharacter.Equipment.GetEquipmentFromSlot(equipmentIndex)
        CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, new EquipmentElement(itemObject));
    }




