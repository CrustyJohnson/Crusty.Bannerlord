using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace Crusty.Bannerlord.StoryMode.Behaviors.NegotiatePeace
{
    public static class NegotiatePeace
    {

        private class PeaceNegotiationCampaignBehavior : CampaignBehaviorBase
        {
            public override void RegisterEvents()
            {
            }

            public void OnCheckForIssue(Hero hero)
            {
                if (this.ConditionsHold(hero))
                    Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(NegotiatePeace.PeaceNegotiationCampaignBehavior), IssueBase.IssueFrequency.Common));
                else
                    Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(NegotiatePeace.PeaceNegotiationIssue), IssueBase.IssueFrequency.Rare));
            }
            private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner) => (IssueBase)new PeaceNegotiationIssue(issueOwner);
            private bool ConditionsHold(Hero issueGiver) => issueGiver != null && issueGiver.IsActive && issueGiver.IsFactionLeader;


            public override void SyncData(IDataStore dataStore) { }
        }

        private class PeaceNegotiationIssue : IssueBase
        {


            public PeaceNegotiationIssue(Hero issueOwner) : base(issueOwner, CampaignTime.DaysFromNow(30f))
            {
            }

            public override TextObject IssueBriefByIssueGiver => throw new NotImplementedException();

            public override TextObject IssueAcceptByPlayer => throw new NotImplementedException();

            public override TextObject IssueQuestSolutionExplanationByIssueGiver => throw new NotImplementedException();

            public override TextObject IssueQuestSolutionAcceptByPlayer => throw new NotImplementedException();

            public override bool IsThereAlternativeSolution => throw new NotImplementedException();

            public override bool IsThereLordSolution => throw new NotImplementedException();

            public override TextObject Title => throw new NotImplementedException();

            public override TextObject Description => throw new NotImplementedException();

            public override IssueFrequency GetFrequency()
            {
                throw new NotImplementedException();
            }

            public override bool IssueStayAliveConditions()
            {
                throw new NotImplementedException();
            }

            protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
            {
                throw new NotImplementedException();
            }

            protected override void CompleteIssueWithTimedOutConsequences()
            {
                throw new NotImplementedException();
            }

            protected override QuestBase GenerateIssueQuest(string questId)
            {
                throw new NotImplementedException();
            }

            protected override void OnGameLoad()
            {
                throw new NotImplementedException();
            }
        }
        private class PeaceNegotiationQuest : QuestBase
        {
            public PeaceNegotiationQuest(string questId, Hero questGiver, CampaignTime duration, int rewardGold) : base(questId, questGiver, duration, rewardGold)
            {
            }

            public override TextObject Title => throw new NotImplementedException();

            public override bool IsRemainingTimeHidden => throw new NotImplementedException();

            protected override void InitializeQuestOnGameLoad()
            {
                throw new NotImplementedException();
            }

            protected override void SetDialogs()
            {
                throw new NotImplementedException();
            }
        }
    }
}
