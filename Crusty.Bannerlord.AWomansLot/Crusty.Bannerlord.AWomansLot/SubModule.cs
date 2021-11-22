using Crusty.Bannerlord.AWomansLot.GameModels;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace Crusty.Bannerlord.AWomansLot
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            TextObject coreContentDisabledReason = new TextObject("{=V8BXjyYq}Disabled during installation.");

            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("A Woman's Lot", new TextObject("A Woman's Lot", null), 
                1337, (Action)(() => MBGameManager.StartNewGame(new AWomansLotGameManager())), (Func<(bool, TextObject)>)(() 
                => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));

        }
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
            if (!(game.GameType is Campaign))
                return;
            CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarter;
            this.AddModels(gameStarter);
            this.AddBehaviors(campaignGameStarter);



        }

        private void AddBehaviors(CampaignGameStarter campaignGameStarter)
        {

            campaignGameStarter.AddBehavior(new Behaviors.AWomansLotFirstPhaseBehavior());
            

        }


        private void AddModels(IGameStarter gameStarter)
        {
            gameStarter.AddModel(new AWomansLotXpModel());
            gameStarter.AddModel(new AWomansLotCombatXpModel());
        }
    }
}