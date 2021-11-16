using Crusty.Bannerlord.StoryMode.GameModels;
using System;
using Crusty.Bannerlord.StoryMode.Behaviors;
using Crusty.Bannerlord.StoryMode.Behaviors.Issues;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace Crusty.Bannerlord.StoryMode
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            TextObject coreContentDisabledReason = new TextObject("{=V8BXjyYq}Disabled during installation.");

            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("CrustyStoryMode", new TextObject("Crusty's Story Mode", null), 9990, (Action)(() => MBGameManager.StartNewGame(new CrustyStoryModeGameManager())), (Func<(bool, TextObject)>)(() => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Crafting", new TextObject("Crafting", null), 9991, (() => MBGameManager.StartNewGame(new CrustyCraftingGameManager())), (Func<(bool, TextObject)>)(() => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));

        }
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
            if (!(game.GameType is Campaign))
                return;
            CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;
            this.AddModels(gameStarter);
            InformationManager.DisplayMessage(new InformationMessage("Crusty.Bannerlord.Story.GameModels - Loaded"));
            this.AddBehaviors(campaignGameStarter);
            InformationManager.DisplayMessage(new InformationMessage("Crusty.Bannerlord.Story.Behaviors - Loaded"));


        }

        private void AddBehaviors(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddBehavior(new CrustyHeroPrisonerBehavior());
            CrustyHelpers.OutMsg("CrustyHeroPrisonerBehavior Added!");
            campaignGameStarter.AddBehavior(new TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.BattleChallengeCampaignBehavior());
            CrustyHelpers.OutMsg("BattleChallengeCampaignBehavior Added!");
             // campaignGameStarter.AddBehavior(new CrustyPlayerCaptivityBehavior());
             // InformationManager.DisplayMessage(new InformationMessage("CrustyPlayerCaptivityBehavior Added!"));
            //campaignGameStarter.AddBehavior(new LordWantsGiftIssueBehavior());
            // campaignGameStarter.AddBehavior(new ProveYourLove());
            //CrustyHelpers.OutMsg("ProveYourLoveCampaignBehavior Added!");
        }


        private void AddModels(IGameStarter gameStarter)
        {
            gameStarter.AddModel(new CrustyGenericXpModel());
            gameStarter.AddModel(new CrustyCombatXpModel());
        }
    }
}