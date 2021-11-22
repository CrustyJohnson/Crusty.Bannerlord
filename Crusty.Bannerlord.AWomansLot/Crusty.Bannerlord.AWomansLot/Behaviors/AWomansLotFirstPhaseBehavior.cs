using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Crusty.Bannerlord.AWomansLot.Behaviors
{
    public class AWomansLotFirstPhaseBehavior : CampaignBehaviorBase
    {
        private Hero antagonist = Hero.AllAliveHeroes.FirstOrDefault(t =>
            t != Hero.MainHero && t.PartyBelongedTo != null && t.Clan.IsMafia && t.Culture == Hero.MainHero.Culture);
        private Hero betrothed = Hero.AllAliveHeroes.FirstOrDefault(t =>
            t != Hero.MainHero && t.CanMarry() && !t.IsFemale);
        private int phase;
        private PartyBase dowryParty = Hero.MainHero.Father.PartyBelongedTo.Party;

        public Settlement AntagonistCamp { get { return Helpers.SettlementHelper.FindRandomHideout(); } }

        public Hero Antagonist { get => antagonist; private set => antagonist = value; }
        public Hero Betrothed { get => betrothed; private set => betrothed = value; }
        public int AWomansLotPhase { get => phase; set => phase = value; }
        public PartyBase DowryParty { get => dowryParty; private set => dowryParty = value; }

        public override void RegisterEvents()
        {
            phase = 0;
            AWomansLotPhase = 0;
            CampaignEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener(this, CharacterCreationIsOver);
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
            CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, OnHourlyTick);
            CampaignEvents.TickEvent.AddNonSerializedListener(this, Tick);
        }

        private void Tick(float obj)
        {
            MobileParty.MainParty.SetMoveGoToPoint(Hero.MainHero.Father.PartyBelongedTo.GetPosition2D + new Vec2(0.25f, 0.25f));
        }

        private void OnHourlyTick()
        {

            if (AWomansLotPhase == 0)
            {
                if (Hero.MainHero.Father.PartyBelongedTo.Party != null)
                {
                    DowryParty = Hero.MainHero.Father.PartyBelongedTo.Party;
                    Hero.MainHero.Father.PartyBelongedTo.Party.SetAsCameraFollowParty();
                    DowryParty.MobileParty.SetMoveEngageParty(this.Betrothed.PartyBelongedTo);
                }

                foreach (Hero hero in Hero.AllAliveHeroes)
                {
                    if (hero.PartyBelongedTo != null && (hero.Clan.IsMafia | hero.Clan.IsBanditFaction | hero.Clan.IsClanTypeMercenary))
                    {
                        hero.PartyBelongedTo.SetMoveEngageParty(MobileParty.MainParty);
                        if (!hero.Clan.IsAtWarWith(Clan.PlayerClan))
                            TaleWorlds.CampaignSystem.Actions.DeclareWarAction.Apply(hero.Clan.MapFaction, Hero.MainHero.Clan.MapFaction);
                    }
                }



            }
        }




        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            starter.AddDialogLine("phase1_start", "start", "phase_one_a", "It's a dangerous world, especially for caravans loaded with all those valuables. How about you" +
                "hand them over to us for safe keeping? Hahaha",
                () => Hero.OneToOneConversationHero.Clan.IsMafia, () => Antagonist = Hero.OneToOneConversationHero, 110);

            starter.AddDialogLine("phase1_start", "start", "phase_one_a", "It's a dangerous world, especially for caravans loaded with all those valuables. How about you" +
                "hand them over to us for safe keeping? Hahaha",
                () => Hero.OneToOneConversationHero == Antagonist, () => this.AWomansLotPhase = 1, 110);
        }


        private void CharacterCreationIsOver()
        {


            Hero.MainHero.BattleEquipment.Horse.Clear();
            Hero Betrothed = Hero.AllAliveHeroes.GetRandomElementWithPredicate(t => t != Hero.MainHero && t.CanMarry() && !t.IsFemale && t.Culture != Hero.MainHero.Culture);
            this.Betrothed = Betrothed;
            InformationManager.ShowInquiry(new InquiryData("A Woman's Lot",
                "You have been betrothed to " + this.Betrothed.Name.ToString() + " As is the custom, you will be joining his clan. Your father and a few of his " +
                "workers have packed a hanbdsome dowry into a caravan. Soon you shall set off with him to be delivered.",
                true, false, "So be it.", null, () => InformationManager.HideInquiry(), null));
            AWomansLotPhase = 0;
            betrothed = Betrothed;
            ItemRoster caravanItems = new ItemRoster();
            caravanItems.AddToCounts(DefaultItems.Hides, 1000);
            MobileParty.MainParty.RemoveParty();

            TaleWorlds.CampaignSystem.Actions.ChangeClanLeaderAction.ApplyWithSelectedNewLeader(Clan.PlayerClan, Hero.MainHero.Father);
            PartyBase.MainParty.Visuals.SetMapIconAsDirty();
            MobileParty fatherParty = CaravanPartyComponent.CreateCaravanParty(Hero.MainHero.Father, Hero.MainHero.Father.HomeSettlement, true, Hero.MainHero.Father, caravanItems, 30);
            fatherParty.SetCustomName(new TaleWorlds.Localization.TextObject("Dowry Train"));
            fatherParty.AddElementToMemberRoster(Hero.MainHero.Mother.CharacterObject, 1);
            fatherParty.Party.AddElementToMemberRoster(Game.Current.ObjectManager.GetObject<CharacterObject>("battanian_clanwarrior"), 10);

            //DeclareWarAction.Apply(Antagonist.MapFaction, Hero.MainHero.MapFaction);
            //TakePrisonerAction.Apply(Antagonist.PartyBelongedTo.Party, Hero.MainHero);
            //EncounterManager.StartPartyEncounter(Antagonist.PartyBelongedTo.Party, MobileParty.MainParty.Party);



            //Antagonist.PartyBelongedTo.SetMoveGoToSettlement(settlement);


        }
        public override void SyncData(IDataStore dataStore)
        {

        }
    }
}
