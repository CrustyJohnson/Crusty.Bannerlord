using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using StoryMode.CharacterCreationContent;


namespace Crusty.Bannerlord.StoryMode
{
    public class CrustyStoryModeCharacterCreationContent : StoryModeCharacterCreationContent

    {
        protected override void OnInitialized(CharacterCreation characterCreation)
        {
            base.AddParentsMenu(characterCreation);
            base.AddChildhoodMenu(characterCreation);
            base.AddEducationMenu(characterCreation);
            base.AddYouthMenu(characterCreation);
            base.AddAdulthoodMenu(characterCreation);
            base.AddEscapeMenu(characterCreation);
            AddHeirloomMenu(characterCreation);
            AddHeirloomMenu2(characterCreation);

        }


        private void AddHeirloomMenu(CharacterCreation characterCreation)
        {
            // Menu Root
            MBTextManager.SetTextVariable("EXP_VALUE", base.SkillLevelToAdd);
            CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("Heirloom"), new TextObject("The last day you saw your father, he bestowed upon you..."), new CharacterCreationOnInit(this.HeirloomOnInit));
            // Sword
            CharacterCreationCategory creationCategory = menu.AddMenuCategory();
            creationCategory.AddCategoryOption(new TextObject("A handsome sword."), new List<SkillObject>(), null, 12,
                0, 6, null, new CharacterCreationOnSelect(this.SwordHeirloomOnSelect),
                new CharacterCreationApplyFinalEffects(SwordHeirloomOnApply),
                new TextObject("It rests comfortably in your hands"));
            // Polearm
            creationCategory.AddCategoryOption(new TextObject("A menacing polearm."), new List<SkillObject>(), null, 12,
                0, 6, null, new CharacterCreationOnSelect(this.PolearmHeirloomOnSelect),
                new CharacterCreationApplyFinalEffects(PolearmHeirloomOnApply),
                new TextObject("It rests comfortably in your hands"));
            // Axe
            creationCategory.AddCategoryOption(new TextObject("A handsome axe."), new List<SkillObject>(), null, 12,
                0, 6, null, new CharacterCreationOnSelect(this.AxeHeirloomOnSelect),
                new CharacterCreationApplyFinalEffects(AxeHeirloomOnApply),
                new TextObject("It rests comfortably in your hands"));


            //Add the menu
            characterCreation.AddNewMenu(menu);
        }

        private void AxeHeirloomOnApply(CharacterCreation characterCreation)
        {
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Aserai)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("aserai_2haxe_2_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_2haxe_1_t2");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Empire)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("imperial_axe_t3");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Khuzait)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("simple_sparth_axe_t2");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Sturgia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("sturgia_2haxe_1_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Vlandia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("vlandia_axe_2_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
        }
        private void AxeHeirloomOnSelect(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ClearCharactersEquipment();
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment equipment = CharacterObject.PlayerCharacter.Equipment.Clone(true);
            equipmentList.Add(equipment);
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Aserai)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("aserai_2haxe_2_t4");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_2haxe_1_t2");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Empire)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("imperial_axe_t3");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Khuzait)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("simple_sparth_axe_t2");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Sturgia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("sturgia_2haxe_1_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Vlandia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("vlandia_axe_2_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            characterCreation.ChangeCharactersEquipment(equipmentList);
            characterCreation.ChangeCharsAnimation(new List<string>()
            {
                "act_childhood_grit"
            });
        }


        private void AddHeirloomMenu2(CharacterCreation characterCreation)
        {
            // Menu Root
            MBTextManager.SetTextVariable("EXP_VALUE", base.SkillLevelToAdd);
            CharacterCreationMenu menu = new CharacterCreationMenu(new TextObject("Heirloom2"), new TextObject("The day you left home, your mother bestowed onto you..."), new CharacterCreationOnInit(this.HeirloomOnInit));
            // Option 1
            CharacterCreationCategory creationCategory = menu.AddMenuCategory();
            creationCategory.AddCategoryOption(new TextObject("A trusty crossbow."), new List<SkillObject>(), null, 12, 0, 6, null, new CharacterCreationOnSelect(this.CrossbowHeirloomOnSelect), new CharacterCreationApplyFinalEffects(CrossbowHeirloomOnApply), new TextObject("It bears the scars of many a hunt"));

            //Option 2
            creationCategory.AddCategoryOption(new TextObject("A lithe bow."), new List<SkillObject>(), null, 12, 0, 6, null, new CharacterCreationOnSelect(this.BowHeirloomOnSelect), new CharacterCreationApplyFinalEffects(BowHeirloomOnApply), new TextObject("The sound it creates, as you fiddle with the bowstring, is beautiful."));

            // Option 3

            //Add the menu
            characterCreation.AddNewMenu(menu);
        }

        private void PolearmHeirloomOnApply(CharacterCreation characterCreation)
        {
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Aserai)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_polearm_1_t5");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_polearm_1_t5");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Empire)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("empire_polearm_1_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Khuzait)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("khuzait_polearm_1_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Sturgia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("sturgia_polearm_1_t5");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Vlandia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("vlandia_polearm_1_t5");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(itemObject1));
            }

        }

        private void PolearmHeirloomOnSelect(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenMounts();
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ClearCharactersEquipment();
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment equipment = CharacterObject.PlayerCharacter.Equipment.Clone(true);
            equipmentList.Add(equipment);
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Aserai)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("easter_polesword_t4");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_polearm_1_t5");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Empire)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("empire_polearm_1_t4");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Khuzait)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("khuzait_polearm_1_t4");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Sturgia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("sturgia_polearm_1_t5");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Vlandia)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("vlandia_polearm_1_t5");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            characterCreation.ChangeCharactersEquipment(equipmentList);
            characterCreation.ChangeCharsAnimation(new List<string>()
            {
                "act_childhood_polearm"
            });
        }

        private void CrossbowHeirloomOnApply(CharacterCreation characterCreation)
        {
            ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("crossbow_b");
            CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, new EquipmentElement(itemObject1));
        }

        private void CrossbowHeirloomOnSelect(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenMounts();
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ClearCharactersEquipment();
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment equipment = CharacterObject.PlayerCharacter.Equipment.Clone(true);
            equipmentList.Add(equipment);
            ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("crossbow_b");
            equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            characterCreation.ChangeCharactersEquipment(equipmentList);
            characterCreation.ChangeCharsAnimation(new List<string>()
            {
                "act_ready_crossbow"
            });

        }


        private void BowHeirloomOnApply(CharacterCreation characterCreation)
        {
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("clairsearch_bow");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2,
                    new EquipmentElement(itemObject1));
            }
            else
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("composite_bow");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2,
                    new EquipmentElement(itemObject1));
            }
        }

        private void BowHeirloomOnSelect(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ClearCharactersEquipment();
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment equipment = CharacterObject.PlayerCharacter.Equipment.Clone(true);
            equipmentList.Add(equipment);
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject = MBObjectManager.Instance.GetObject<ItemObject>("clairsearch_bow");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject));
            }
            else
            {
                ItemObject itemObject = MBObjectManager.Instance.GetObject<ItemObject>("composite_bow");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject));
            }
            characterCreation.ChangeCharactersEquipment(equipmentList);
            characterCreation.ChangeCharsAnimation(new List<string>()
            {
                "act_ready_bow"
            });

        }

        private void SwordHeirloomOnApply(CharacterCreation characterCreation)
        {
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Aserai)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("hadhramawt");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1,
                    new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_2hsword_4_t4");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1,
                    new EquipmentElement(itemObject1));
            }
            else
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("early_retirement_2hsword_t3");
                CharacterObject.PlayerCharacter.Equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1,
                    new EquipmentElement(itemObject1));
            }

            EquipmentHelper.AssignHeroEquipmentFromEquipment(Hero.MainHero, CharacterObject.PlayerCharacter.Equipment);
        }

        private void SwordHeirloomOnSelect(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenPrefab();
            characterCreation.ClearCharactersEquipment();
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment equipment = CharacterObject.PlayerCharacter.Equipment.Clone(true);
            equipmentList.Add(equipment);
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Aserai)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("hadhramawt");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            if (Hero.MainHero.Culture.GetCultureCode() == CultureCode.Battania)
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("battania_2hsword_4_t4");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            else
            {
                ItemObject itemObject1 = MBObjectManager.Instance.GetObject<ItemObject>("early_retirement_2hsword_t3");
                equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon0, new EquipmentElement(itemObject1));
            }
            characterCreation.ChangeCharactersEquipment(equipmentList);
            characterCreation.ChangeCharsAnimation(new List<string>()
            {
                "act_childhood_grit"
            });
        }

        


        private void HeirloomOnInit(CharacterCreation characterCreation)
        {

            ClearMountEntity(characterCreation);
            List<FaceGenChar> newChars = new List<FaceGenChar>();
            BodyProperties originalBodyProperties = CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1);
            originalBodyProperties = FaceGen.GetBodyPropertiesWithAge(ref originalBodyProperties, 18f);
            newChars.Add(new FaceGenChar(originalBodyProperties, CharacterObject.PlayerCharacter.Equipment.Clone(true), CharacterObject.PlayerCharacter.IsFemale, "act_childhood_schooled"));
            characterCreation.ChangeFaceGenChars(newChars);
        }

    }
}
