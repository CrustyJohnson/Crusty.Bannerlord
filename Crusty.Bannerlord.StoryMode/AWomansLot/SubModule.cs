using Crusty.Bannerlord.StoryMode.AWomansLot;
using Crusty.Bannerlord.StoryMode.GameModels;
using System;
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

            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("CrustyStoryMode", new TextObject("Crusty's Story Mode", null), 
                9990, (Action)(() => MBGameManager.StartNewGame(new AWomansLotGameManager())), (Func<(bool, TextObject)>)(() 
                => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));

            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("CrustyStoryMode", new TextObject("A Woman's Lot", null), 
                9990, (Action)(() => MBGameManager.StartNewGame(new AWomansLotGameManager())), (Func<(bool, TextObject)>)(() 
                => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));

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

            /*campaignGameStarter.AddBehavior(new TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.BattleChallengeCampaignBehavior());
            CrustyHelpers.OutMsg("BattleChallengeCampaignBehavior Added!");
            campaignGameStarter.AddBehavior(new HoneyDo.HoneyDoCampaignBehavior());
            CrustyHelpers.OutMsg("HoneyDo Added!");*/
            

        }


        private void AddModels(IGameStarter gameStarter)
        {
            gameStarter.AddModel(new AWomansLotXpModel());
            gameStarter.AddModel(new AWomansLotCombatXpModel());
        }
    }
}