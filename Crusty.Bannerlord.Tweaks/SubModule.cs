using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using System;
using Crusty.Bannerlord.Tweaks.GameModels;
using SandBox;

namespace Crusty.Bannerlord.Tweaks
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            TextObject coreContentDisabledReason = new TextObject("{=V8BXjyYq}Disabled during installation.");
            
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("CrustySandboxGame", new TextObject("Crusty's Sandbox Mode", null), 9990, (Action)(() => MBGameManager.StartNewGame(new CrustySandBoxGameManager())), (Func<(bool, TextObject)>)(() => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption("CrustySandboxGame", new TextObject("Battle", null), 9991, (Action)(() => MBGameManager.StartNewGame(new CrustySandBoxGameManager())), (Func<(bool, TextObject)>)(() => (Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason))));


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
        }

        private void AddModels(IGameStarter gameStarter)
        {
            gameStarter.AddModel(new CrustyGenericXpModel());
            gameStarter.AddModel(new CrustyCombatXpModel());
        }


    }
}