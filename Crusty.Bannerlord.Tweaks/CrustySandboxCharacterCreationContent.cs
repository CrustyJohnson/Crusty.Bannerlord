using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace Crusty.Bannerlord.Tweaks
{
    public class CrustySandboxCharacterCreationContent : SandboxCharacterCreationContent
    {
        protected override void OnInitialized(CharacterCreation characterCreation)
        {
            /*this.AddParentsMenu(characterCreation);
            this.AddChildhoodMenu(characterCreation);
            this.AddEducationMenu(characterCreation);
            this.AddYouthMenu(characterCreation);
            this.AddAdulthoodMenu(characterCreation);
            this.AddAgeSelectionMenu(characterCreation);*/
            this.AddSpecialGiftMenu(characterCreation);
        }

        private void AddSpecialGiftMenu(CharacterCreation characterCreation)
        {
            CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("{=HDFEAYDk}Starting Age"), new TextObject("Your father handed down to you a special heirloom..."), new CharacterCreationOnInit(this.HeirloomOnInit));
            CharacterCreationCategory creationCategory = menu.AddMenuCategory();
            // Sword
            creationCategory.AddCategoryOption(new TextObject("A handsome sword"), new List<SkillObject>(), null, 0, 0, 0, null, new CharacterCreationOnSelect(this.HeirloomSword), new CharacterCreationApplyFinalEffects(this.HeirloomSword), new TextObject("TEST TEST TEST"), null, 0, 0, 0, 12, 5);
            // Bow
            creationCategory.AddCategoryOption(new TextObject("A seasoned bow"), new List<SkillObject>(), null, 0, 0, 0, null, new CharacterCreationOnSelect(this.HeirloomBow), new CharacterCreationApplyFinalEffects(this.HeirloomSword), new TextObject("TEST TEST TEST"), null, 0, 0, 0, 12, 5);
            //Crossbow
            creationCategory.AddCategoryOption(new TextObject("A sturdy crossbow"), new List<SkillObject>(), null, 0, 0, 0, null, new CharacterCreationOnSelect(this.HeirloomCrossbow), new CharacterCreationApplyFinalEffects(this.HeirloomSword), new TextObject("TEST TEST TEST"), null, 0, 0, 0, 12, 5);
            //Horse
            creationCategory.AddCategoryOption(new TextObject("Hal"), new List<SkillObject>(), null, 0, 0, 0, null, new CharacterCreationOnSelect(this.HeirloomHorse), new CharacterCreationApplyFinalEffects(this.HeirloomSword), new TextObject("Jesus Christ be praised!"), null, 0, 0, 0, 12, 5);
            characterCreation.AddNewMenu(menu);
        }

        private void HeirloomSword(CharacterCreation characterCreation)
        {

            characterCreation.IsPlayerAlone = true;
            characterCreation.HasSecondaryCharacter = false;
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ChangeCharsAnimation(new List<string>()
            { 
                "act_equip_2h_left_stance", "act_walk_idle_2h_left_stance"
            });
            RefreshPropsAndClothing(characterCreation, false, "early_retirement_2hsword_t3", false);
        }
        private void HeirloomBow(CharacterCreation characterCreation)
            {
                characterCreation.IsPlayerAlone = true;
                characterCreation.HasSecondaryCharacter = false;
                characterCreation.ClearFaceGenPrefab();
                characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_ready_bow"
      });
                RefreshPropsAndClothing(characterCreation, false, "composite_bow", true);
            }
        
            private void HeirloomCrossbow(CharacterCreation characterCreation)
        {
            characterCreation.IsPlayerAlone = true;
            characterCreation.HasSecondaryCharacter = true;
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ChangeCharsAnimation(new List<string>()
      {
        "act_equip_crossbow", "act_ready_crossbow"
      });
            RefreshPropsAndClothing(characterCreation, false, "crossbow", false);

        }
        private void HeirloomHorse(CharacterCreation characterCreation)
        {

            characterCreation.IsPlayerAlone = true;
            characterCreation.HasSecondaryCharacter = false;
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ChangeCharsAnimation(new List<string>()
            {
                "act_childhood_riding_2",
            });
            RefreshPropsAndClothing(characterCreation, false, "hunter", false);
        }

        private void SwordOnApply(CharacterCreation characterCreation)
            {
                //ItemObject itemObject = MBObjectManager.Instance.GetObject<ItemObject>("early_retirement_2hsword_t3");
                //CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject));
            }
            private void HeirloomOnInit(CharacterCreation characterCreation)
            {
                characterCreation.IsPlayerAlone = true;
                characterCreation.HasSecondaryCharacter = false;
                characterCreation.ClearFaceGenPrefab();
                RefreshPlayerAppearance(characterCreation);
            }

    }
}

