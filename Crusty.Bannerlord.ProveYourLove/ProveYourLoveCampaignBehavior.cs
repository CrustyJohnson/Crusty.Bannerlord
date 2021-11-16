using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using System.Collections.Generic;

namespace Crusty.Bannerlord.ProveYourLove
{
    class ProveYourLoveCampaignBehavior : CampaignBehaviorBase
    {
        int conscienceCounter = 0;
        bool IsBad
        {
            get
            {  
                return true;
            }
        }
        bool IsGood
        {
            get
            {  
                return true;
            }
        }

        public override void RegisterEvents()
        {
            CampaignEvents.SetupPreConversationEvent.AddNonSerializedListener(this, SetupPreConversation);
            CampaignEvents.OnConversationEndedEvent.AddNonSerializedListener(this, OnConversationEnded);
        }
        private void OnConversationEnded()
        {
            Hero hero = new Hero();
            if(Hero.OneToOneConversation != null) {hero = Hero.OneToOneConversationHero;}
            else if(MobileParty.ConversationParty.LeaderHero != null) {hero = MobileParty.ConversationParty.LeaderHero;}
            else {return;}
            
            if (Romance.GetRomanticLevel(Hero.MainHero, hero) == Romance.RomanceLevelEnum.FailedInCompatibility 
                | Romance.GetRomanticLevel(Hero.MainHero, hero) == Romance.RomanceLevelEnum.FailedInCompatibility)
            {
                new ProveYourLoveCampaignBehavior.ProveYourLoveIssue(hero)
            }
            
        }
        private void SetupPreConversation()
        {
        
        Hero quest_giver = MobileParty.ConversationParty.LeaderHero;
        new honor = quest_giver.GetHeroTraits().Honor;
        new mercy = quest_giver.GetHeroTraits().Mercy;
        new valor = quest_giver.GetHeroTraits().Valor;
        new genorosity = quest_giver.GetHeroTraits().Genorosity;
        conscienceCounter = 0 + honor + mercy + valor + genorosity;
        conscienceCounter < 0 ? IsBad : IsGood; 
        
            try
            {
                if (Romance.GetRomanticLevel(Hero.MainHero, MobileParty.ConversationParty.LeaderHero) == Romance.RomanceLevelEnum.FailedInCompatibility
                | Romance.GetRomanticLevel(Hero.MainHero, MobileParty.ConversationParty.LeaderHero) == Romance.RomanceLevelEnum.FailedInPracticalities)

                {
                    Hero quest_giver = MobileParty.ConversationParty.LeaderHero;
                    if (MobileParty.ConversationParty.LeaderHero != null)
                    {
                        new ProveYourLoveQuest(quest_giver).StartQuest();
                    }
                }
            }
            catch { return; }
        }

        public override void SyncData(IDataStore dataStore)
        {

        }

        internal class ProveYourLoveQuest : QuestBase
        {
            private TextObject _StartQuestLog
            {
                get
                {
                    TextObject test = new TextObject("TEST");
                    return test;
                }
            }


            public override TextObject Title
            {
                get
                {
                    TextObject prove_your_love_title = new TextObject("Prove your love!");
                    return prove_your_love_title;
                }
            }

            public override bool IsRemainingTimeHidden
            {
                get
                {
                    return false;
                }
            }

            public ProveYourLoveQuest(Hero questGiver) : base("prove_your_love_quest", questGiver, CampaignTime.DaysFromNow(30), rewardGold: 0)
            {
            }
            protected override void InitializeQuestOnGameLoad()
            {
                SetDialogs();
            }

            protected override void SetDialogs()
            {
                bool isDishonorable = false;
                OfferDialogFlow = DialogFlow.CreateDialogFlow(QuestManager.QuestOfferToken, 110)
                    .NpcLine(new TextObject("After thinking on it for a while, I've decided there is something you can do to prove yourself to me. Are you interested?"))
                    .PlayerLine(new TextObject("What did you have in mind?"))
                    .NpcLine(new TextObject("KILL MY DAD"))
                        .Condition(() => QuestGiver.GetHeroTraits().Honor < 0)
                        .Consequence(() => isDishonorable = true)
                    .NpcLine(new TextObject("GIMME HORSE"))
                        .Condition(() => QuestGiver.GetHeroTraits().Honor >= 0)

                    .BeginPlayerOptions()
                        .PlayerOption(new TextObject("Actually, I don't think I can do that."))                     
                        .PlayerOption(new TextObject("I'll bring you their head, my love!"))
                            .Condition(() => isDishonorable)
                        .PlayerOption(new TextObject("I'll bring you their head, my love!"))
                            .Condition(() => !isDishonorable)


                    .CloseDialog();

                DiscussDialogFlow = DialogFlow.CreateDialogFlow(QuestManager.QuestDiscussToken, 110)
                    .NpcLine(new TextObject("Have you done what I've asked yet?"));
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
