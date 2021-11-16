// Decompiled with JetBrains decompiler
// Type: TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.Towns.PrisonBreakCampaignBehavior
// Assembly: TaleWorlds.CampaignSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D179B4BA-CB1C-4C26-847F-00CFDAFE2D85
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.CampaignSystem.dll

using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;


namespace Crusty.Bannerlord.StoryMode.Behaviors
{
    public class CrustyPlayerCaptivityBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.AfterDailyTickEvent.AddNonSerializedListener(this, OnDailyTickEvent);
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));

        }

        private void OnDailyTickEvent()
        {
            PlayerEncounter.EnterSettlement();
            GameMenu.ActivateGameMenu("player_prisoner_daily_menu");
        }

        private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
        {
            this.AddGameMenus(campaignGameStarter);
        }
        string scene = Settlement.CurrentSettlement.LocationComplex.GetScene("empire_dungeon_a",
            Settlement.CurrentSettlement.Town.GetWallLevel());
        Location location = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("prison");
        CharacterObject prisonerHero = Settlement.CurrentSettlement.Town.GetPrisonerHeroes()
            .Find((CharacterObject x) => !x.IsPlayerCharacter);
        private void AddGameMenus(CampaignGameStarter gameSystemInitializer)
        {
            gameSystemInitializer.AddGameMenu("player_prisoner_daily_menu", "You survived another day in prison", null);
            gameSystemInitializer.AddGameMenuOption("player_prisoner_daily_menu", "attempt_escape", "Attempt escape", (args => Hero.MainHero.IsPrisoner), 
                (MenuCallbackArgs args) =>
                {
                    CampaignMission.OpenPrisonBreakMission(scene, location, prisonerHero);

        });
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}

