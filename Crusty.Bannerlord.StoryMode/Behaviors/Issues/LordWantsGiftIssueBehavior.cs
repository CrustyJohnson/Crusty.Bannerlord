using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment.Managers;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace Crusty.Bannerlord.StoryMode.Behaviors.Issues
{
    public class LordWantsGiftIssueBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents() =>
            CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener((object)this,
                new Action<Hero>(this.OnCheckForIssue));

        public override void SyncData(IDataStore dataStore)
        {
        }

        private bool ConditionsHold(Hero issueGiver)
        {
            if (Romance.GetRomanticLevel(Hero.MainHero, issueGiver) ==
                Romance.RomanceLevelEnum.FailedInCompatibility || Romance.GetRomanticLevel(Hero.MainHero, issueGiver) ==
                Romance.RomanceLevelEnum.FailedInPracticalities)
            {
                return true;
            }

            return false;
        }

        public void OnCheckForIssue(Hero hero)
        {
            if (this.ConditionsHold(hero))
            {
                Campaign.Current.IssueManager.AddPotentialIssueData(hero,
                    new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnIssueSelected),
                        typeof(LordWantsGiftIssueBehavior.LordWantsGiftIssue), IssueBase.IssueFrequency.VeryCommon));
                return;
            }

            Campaign.Current.IssueManager.AddPotentialIssueData(hero,
                new PotentialIssueData(typeof(LordWantsGiftIssue.LordWantsGiftQuest),
                    IssueBase.IssueFrequency.VeryCommon));
        }

        private IssueBase OnIssueSelected(in PotentialIssueData pid, Hero issueOwner)
        {
            return new LordWantsGiftIssueBehavior.LordWantsGiftIssue(issueOwner);
        }

        internal class LordWantsGiftIssue : IssueBase
        {
            public LordWantsGiftIssue(Hero issueOwner) : base(issueOwner, CampaignTime.Never)
            {
            }

            public override TextObject Title
            {
                get
                {
                    TextObject textObject = new TextObject("{POTENTIAL_SPOUSE} Wants a Gift", null);
                    textObject.SetTextVariable("POTENTIAL_SPOUSE", IssueOwner.Name);
                    return textObject;
                }
            }

            public override TextObject Description
            {
                get
                {
                    TextObject textObject =
                        new TextObject(
                            "In order to prove your dedication, {POTENTIAL SPOUSE} has asked you to find a gift for them.",
                            null);
                    textObject.SetTextVariable("POTENTIAL_SPOUSE", IssueOwner.Name);
                    return textObject;
                }
            }

            public override TextObject IssueBriefByIssueGiver => new TextObject(
                "As it stands, I don't think there is anyway we can be wed. However, after thinking for a little while, I decided there may be something you can do to change my mind.",
                null);

            public override TextObject IssueAcceptByPlayer
            {
                get { return new TextObject("Please, what can I do to prove myself to you?", null); }
            }

            public override TextObject IssueQuestSolutionExplanationByIssueGiver
            {
                get
                {
                    return new TextObject(
                        "Bring me a token of your choosing. Perhaps a physical object can manifest your earnest in ways your words cannot",
                        null);
                }
            }

            public override TextObject IssueQuestSolutionAcceptByPlayer
            {
                get { return new TextObject("it would be my honor", null); }
            }

            public override bool IsThereAlternativeSolution
            {
                get { return false; }
            }

            public override bool IsThereLordSolution
            {
                get { return false; }
            }

            public override IssueFrequency GetFrequency()
            {
                return IssueFrequency.VeryCommon;
            }

            public override bool IssueStayAliveConditions()
            {
                return IssueOwner.IsAlive && IssueOwner.CanMarry();
            }

            protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out PreconditionFlags flag,
                out Hero relationHero,
                out SkillObject skill)
            {
                relationHero = null;
                flag = IssueBase.PreconditionFlags.None;
                skill = null;
                return flag == IssueBase.PreconditionFlags.None;
            }

            protected override void CompleteIssueWithTimedOutConsequences()
            {
            }

            protected override QuestBase GenerateIssueQuest(string questId)
            {
                return new LordWantsGiftQuest(questId, base.IssueOwner, CampaignTime.Never, this.RewardGold);
            }

            protected override void OnGameLoad()
            {
            }

            internal class LordWantsGiftQuest : QuestBase
            {
                // Constructor with basic vars and any vars about the quest
                public LordWantsGiftQuest(string questId, Hero questGiver, CampaignTime duration, int rewardGold) :
                    base(questId, questGiver, duration, rewardGold)
                {
                    this.SetDialogs();
                    base.InitializeQuestOnCreation();
                }



                public override TextObject Title
                {
                    get { return new TextObject("{POTENTIAL_SPOUSE} wants a gift", null); }
                }

                private TextObject StageOnePlayerAcceptsQuestLogText
                {
                    get
                    {
                        TextObject textObject =
                            new TextObject(
                                "{POTENTIAL_SPOUSE} has asked you to deliver to them an item worthy of their love",
                                null);

                        textObject.SetTextVariable("POTENTIAL_SPOUSE", QuestGiver.Name);
                        return textObject;
                    }
                }


                public override bool IsRemainingTimeHidden
                {
                    get { return true; }
                }

                protected override void InitializeQuestOnGameLoad()
                {
                    this.SetDialogs();
                }

                protected override void SetDialogs()
                {

                    this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100)
                        .NpcLine(new TextObject("Thank you"), null, null)
                        .Condition(() =>
                            CharacterObject.OneToOneConversationCharacter == base.QuestGiver.CharacterObject)
                        .Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences))
                        .CloseDialog();

                    this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100)
                        .NpcLine(new TextObject("Have you found the perfect gift yet?", null), null, null)
                        .BeginPlayerOptions()
                        .PlayerOption(new TextObject("Yes, here it is.", null), null).NpcLine(new TextObject("Let's see then")).Consequence(new ConversationSentence.OnConsequenceDelegate(this.GiveGiftConsequence))
                        .PlayerOption(new TextObject("Not yet, but soon!", null), null).CloseDialog()
                        .EndPlayerOptions().CloseDialog();
                }

                private void GiveGiftConsequence()
                {
                    BarterManager.Instance.StartBarterOffer(Hero.MainHero,
                        Hero.OneToOneConversationHero,
                        PartyBase.MainParty,
                        MobileParty.ConversationParty?.Party);
                }


                private void QuestAcceptedConsequences()
                {
                    base.StartQuest();
                    PlayerAcceptQuestLog = base.AddDiscreteLog(this.StageOnePlayerAcceptsQuestLogText,
                        new TextObject("Find a suitable item"), 0, 1, null, false);
                }


                [SaveableField(1)] 
                private JournalLog PlayerAcceptQuestLog;

             }
            public class LordWantsGiftTypeDefiner : SaveableTypeDefiner
            {
                public LordWantsGiftTypeDefiner() : base(585850)
                {
                }

                protected override void DefineClassTypes()
                {
                    base.AddClassDefinition(typeof(LordWantsGiftIssueBehavior.LordWantsGiftIssue), 1);
                }
            }
        }
    }
}