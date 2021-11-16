using System.Runtime.Remoting.Messaging;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.Issues;
using TaleWorlds.ObjectSystem;

namespace Crusty.Bannerlord.StoryMode.Behaviors
{
    public class ProveYourLove : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionlaunched);
        }


        private void OnSessionlaunched(CampaignGameStarter campaignGameStarter)
        {
            this.AddDialogs(campaignGameStarter);
        }

        private void AddDialogs(CampaignGameStarter starter)
        {
            starter.AddDialogLine("prove_your_love_1",
                "lord_start_courtship_response",
                "prove_your_love_2",
                "Actually, there may be something you can do to change my mind.",
                this.prove_your_love_init_on_condition,
                null);
            starter.AddPlayerLine("prove_your_love_2_",
                "prove_your_love_2",
                null,
                "Anything, just tell me.",
                null,
                this.Consequence,
                110);
            starter.AddPlayerLine("prove_your_love_2b",
                "prove_your_love_2",
                "close_window",
                "Nevermind.",
                null,
                null);


        }

        private void Consequence()
        {
            Hero issueOwner = Hero.OneToOneConversationHero;
            new LadysKnightOutIssueBehavior.LadysKnightOutIssue(issueOwner);
        }


        private bool prove_your_love_init_on_condition()
        {
            CrustyHelpers.OutMsg("Prove Your Love Activated!");
            Romance.RomanceLevelEnum romanticLevel = Romance.GetRomanticLevel(Hero.MainHero, Hero.OneToOneConversationHero);
            if (Hero.OneToOneConversationHero == null)
                return false;
            if (romanticLevel == Romance.RomanceLevelEnum.FailedInPracticalities)
                return true;
            if (romanticLevel == Romance.RomanceLevelEnum.FailedInCompatibility)
                return true;
            if (romanticLevel == Romance.RomanceLevelEnum.Rejection)
                return true;
            return false;
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}