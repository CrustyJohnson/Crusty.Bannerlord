using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace Crusty.Bannerlord.HoneyDo
{
    public class HoneyDoCampaignBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, OnCheckForIssue);
        }


        public void OnCheckForIssue(Hero hero)
        {
            if (this.ConditionsHold(hero))
                Campaign.Current.IssueManager.AddPotentialIssueData(hero,
                    new PotentialIssueData(
                        new PotentialIssueData.StartIssueDelegate(this.OnStartIssue),
                        typeof(HoneyDo.HoneyDoIssue),
                        IssueBase.IssueFrequency.Common));
            else
                Campaign.Current.IssueManager.AddPotentialIssueData(hero,
                    new PotentialIssueData(typeof(HoneyDo.HoneyDoIssue),
                    IssueBase.IssueFrequency.Common));
        }



        private bool ConditionsHold(Hero hero) => hero == Hero.MainHero.Spouse;

        private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner) => (IssueBase)new HoneyDo.HoneyDoIssue(issueOwner);



        public override void SyncData(IDataStore dataStore)
        {
        }

    }
    public class HoneyDoIssue : IssueBase
    {
        private Hero issueOwner;
        public static List<String> HoneyDoQuests = new List<String>() {"prison_break"};
        private string quest = HoneyDoQuests.GetRandomElement();
        protected override bool IssueQuestCanBeDuplicated => false;



        public HoneyDoIssue(Hero issueOwner)
        : base(issueOwner, CampaignTime.DaysFromNow(30f))
        {
            this.InitializeQuestVariables();
        }
        protected override void OnGameLoad() => this.InitializeQuestVariables();
        private void InitializeQuestVariables()
        {
            this.spouse_friend = Hero.AllAliveHeroes.First(Hero.MainHero.Spouse.IsFriend);
        }

        public override TextObject IssueBriefByIssueGiver
        {
            get
            {
                TextObject textObject = new TextObject("Hello my dear, I was wondering if you wouldn't mind doing me a favor?");
                return textObject;
            }
        }

        public override TextObject IssueAcceptByPlayer
        {
            get
            {
                TextObject textObject = new TextObject("What is it, {SPOUSE_NAME}?");
                textObject.SetTextVariable("SPOUSE_NAME", Hero.MainHero.Spouse.Name);
                return textObject;
            }
        }

        public override TextObject IssueQuestSolutionExplanationByIssueGiver
        {
            get
            {

                TextObject textObject = new TextObject("");
                string quest = this.Quest;

                if (quest == "prison_break") 
                {
                    TextObject text = new TextObject("My friend, {SPOUSE_FRIEND}, has been wrongfully imprisoned. Could you please do something to" +
                    "help them? It would mean the world to me");
                    text.SetTextVariable("SPOUSE_FRIEND", spouse_friend.Name);
                    textObject = text;
                }
                return textObject;

                   
            }
        }

        public override TextObject IssueQuestSolutionAcceptByPlayer
        {
            get
            {
                if (Hero.MainHero.Spouse.GetRelationWithPlayer() > 20) { return new TextObject("My pleasure."); }
                if (Hero.MainHero.Spouse.GetRelationWithPlayer() > 40) { return new TextObject("For you, anything."); }
                if (Hero.MainHero.Spouse.GetRelationWithPlayer() > 60) { return new TextObject("Anything you want, all the time!"); }
                return new TextObject("Fine.");

            }
        }

        public override bool IsThereAlternativeSolution => false;

        public override bool IsThereLordSolution => false;

        public override TextObject Title
        {
            get
            {
                string quest = this.Quest;
                string _string = "";
                if (quest == "prisoner_break") { _string = ": Prison Break"; }
                return new TextObject("Honey Do"+_string);
            }
        }



        private string Quest { get => quest; set => quest = value; }

        public override TextObject Description
        {
            get
            {
                
                    TextObject text = new TextObject("Your spouse, {SPOUSE}'s friend, {SPOUSE_FRIEND.NAME} has been imprisoned. Help them get out");
                    text.SetTextVariable("SPOUSE", Hero.MainHero.Spouse.Name);
                    text.SetTextVariable("SPOUSE_FRIEND.NAME", spouse_friend.Name);
                    return text;
                
            }
        }

        private Hero spouse_friend = Hero.AllAliveHeroes.First(Hero.MainHero.Spouse.IsFriend);

        public override IssueFrequency GetFrequency() => IssueBase.IssueFrequency.Common;

        public override bool IssueStayAliveConditions()
        {
           return !Hero.MainHero.Spouse.IsDead && !spouse_friend.IsDead && spouse_friend.IsPrisoner;
        }

        protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            skill = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            relationHero = issueGiver;
            flag = IssueBase.PreconditionFlags.None;
            return true;
        }

        protected override void CompleteIssueWithTimedOutConsequences()
        {
            
        }

        protected override QuestBase GenerateIssueQuest(string questId) 
            => (QuestBase)new HoneyDo.HoneyDoQuest(questId, this.IssueOwner, CampaignTime.DaysFromNow(30f), this.RewardGold);


    }
    public class HoneyDoQuest : QuestBase
    {
        public HoneyDoQuest(string questId, Hero questGiver, CampaignTime duration, int rewardGold) : base("honey_do_quest", questGiver, duration, rewardGold)
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
