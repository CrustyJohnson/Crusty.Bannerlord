using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using System;
using TaleWorlds.Core;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;

namespace Crusty.Bannerlord.ProveYourLove
{
    class ProveYourLoveCampaignBehavior : CampaignBehaviorBase
    {
        protected bool QuestInitiatedForIssueOwner = false;
        private bool _questIsAccepted = false;

        public bool QuestIsAccepted { get => _questIsAccepted; private set => _questIsAccepted = value; }

        public override void RegisterEvents()
        {
            CampaignEvents.ConversationEnded.AddNonSerializedListener(this, OnConversationEnded);
            
            
            
        }

        private void OnConversationEnded(CharacterObject characterObject)
        {
            Hero questGiver = characterObject.HeroObject;

            if (Romance.GetRomanticLevel(Hero.MainHero, characterObject.HeroObject) == Romance.RomanceLevelEnum.FailedInCompatibility
                | Romance.GetRomanticLevel(Hero.MainHero, characterObject.HeroObject) == Romance.RomanceLevelEnum.FailedInCompatibility)

            {

                new ProveYourLoveQuest(questGiver);
                DialogFlow prove_your_love_start = DialogFlow.CreateDialogFlow("lord_start_courtship_response", int.MaxValue)
                    .NpcLine(new TextObject("Actually, after thinking it for a while, there is something you can do that may help prove yourself to me"))
                        .Condition(() => Hero.OneToOneConversationHero == questGiver)
                    .PlayerLine(new TextObject("What can I do?"))
                    .NpcLine(new TextObject("My clan's bastard leader, {QUEST_GIVER_CLAN_LEADER}, has always had it in for me. If it weren't for them, " +
                                            "I'd marry you this instant. If only some tragic incident were to befall them...then nothing could stop us from being together")
                        .SetTextVariable("QUEST_GIVER_CLAN_LEADER", questGiver.Clan.Leader.Name))
                    .BeginPlayerOptions()
                        .PlayerOption(new TextObject("Say no more, I'll look into it"))
                                .Consequence(() => new ProveYourLoveQuest(questGiver).StartQuest())
                            .NpcLine(new TextObject("That is wonderful my love! If you succeed, I'll know you are willing to do what it takes to have me!"))
                                .Consequence(() => QuestIsAccepted = true)
                        .CloseDialog()
                        .PlayerOption(new TextObject("I must consider this for some time. I'll come back later"))
                            .NpcLine(new TextObject("Well don't ponder it for too long, there are plenty of other suitors who would surely be willing"))
                        .CloseDialog()
                    .EndPlayerOptions()
                    .CloseDialog();
                Campaign.Current.ConversationManager.AddDialogFlow(prove_your_love_start, this);

            }

        }
        public override void SyncData(IDataStore dataStore)
        {

        }

        internal class ProveYourLoveQuest : QuestBase
        {
            private Hero _ClanLeader
            {
                get
                {
                    Hero hero = QuestGiver.Clan.Leader;
                    return hero;
                }
            }
            protected override void RegisterEvents()
            {
                CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, heroKilled);
                
            }

            private void heroKilled(Hero arg1, Hero arg2, KillCharacterAction.KillCharacterActionDetail arg3, bool arg4)
            {
                if (_ClanLeader.IsDead)
                {
                    AddLog(_HeroKilledLog);
                }
            }

            private TextObject _HeroKilledLog
            {
                get
                {
                    TextObject textObject = new TextObject("{CLAN_LEADER} has died. Go tell your lover the good news!");
                    textObject.SetTextVariable("CLAN_LEADER", QuestGiver.Clan.Leader.Name);
                    return textObject;
                }
            }
            private TextObject _StartQuestLog
            {
                get
                {
                    TextObject textObject = new TextObject("{CLAN_LEADER} must die. Find a way for that to happen");
                    textObject.SetTextVariable("CLAN_LEADER", QuestGiver.Clan.Leader.Name);
                    return textObject;
                }
            }

            public ProveYourLoveQuest(Hero questGiver) : base("prove_your_love_quest", questGiver, CampaignTime.DaysFromNow(30f), rewardGold: 0)
            {
                SetDialogs();
                AddLog(_StartQuestLog);
           
           }

            public override TextObject Title => new TextObject("Prove Your Love to " + QuestGiver.Name);


            public override bool IsRemainingTimeHidden => true;


            protected override void InitializeQuestOnGameLoad()
            {
                SetDialogs();
            }

            protected override void SetDialogs()
            {
                
                DialogFlow prove_your_love_competed_success = DialogFlow.CreateDialogFlow("hero_main_options", int.MaxValue)
                    .PlayerLine(new TextObject("It's about {QUEST_GIVER_CLAN_LEADER}...").SetTextVariable("QUEST_GIVER_CLAN_LEADER", _ClanLeader.Name))
                    .Condition(() => _ClanLeader.IsDead)
                    .NpcLine(new TextObject("Oh yes, did you take care of our little problem yet?" +
                    " Or perhaps fortune smiled on us and took care of it for us?"))
                    .BeginPlayerOptions()
                        .PlayerOption(new TextObject("The fool is dead, we can elope at once!"))
                                .Condition(() => QuestGiver.Clan.Leader.IsDead)
                            .NpcLine(new TextObject("What wonderful news, let's go my love!"))
                                .Consequence(() => CompleteQuestWithSuccess())
                        .CloseDialog()
                    .PlayerOption(new TextObject("Not not yet..."))
                        .Condition(() => QuestGiver.Clan.Leader.IsAlive)
                        .NpcLine(new TextObject("Hurry up with it! Don't you want to marry me?"))
                    .CloseDialog()
                    .EndPlayerOptions()
                    .CloseDialog();



                Campaign.Current.ConversationManager.AddDialogFlow(prove_your_love_competed_success, this);

            }

            protected override void OnStartQuest()
            {
                base.OnStartQuest();
            }
        }


    }
    public class ProveYourLoveCampaignBehaviorTypeDefiner : CampaignBehaviorBase.SaveableCampaignBehaviorTypeDefiner
    {
        public ProveYourLoveCampaignBehaviorTypeDefiner()
            // This number is the SaveID, supposed to be a unique identifier
            : base(847_000_000)
        {
        }

        protected override void DefineClassTypes()
        {
            // Your quest goes here, second argument is the SaveID
            AddClassDefinition(typeof(ProveYourLoveCampaignBehavior.ProveYourLoveQuest), 1);
        }
    }
}
