﻿using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Crusty.Bannerlord.Tweaks.GameModels
{
    class CrustyCombatXpModel : DefaultCombatXpModel
    {
        public class DefaultCombatXpModel : CombatXpModel
        {
            public override SkillObject GetSkillForWeapon(WeaponComponentData weapon)
            {
                SkillObject skillObject = DefaultSkills.Athletics;
                if (weapon != null)
                    skillObject = weapon.RelevantSkill;
                return skillObject;
            }

            public override void GetXpFromHit(
              CharacterObject attackerTroop,
              CharacterObject captain,
              CharacterObject attackedTroop,
              PartyBase party,
              int damage,
              bool isFatal,
              CombatXpModel.MissionTypeEnum missionType,
              out int xpAmount)
            {
                int val2 = attackedTroop.MaxHitPoints();
                double num1 = 0.400000005960464 * (((party?.MapEvent == null ? (double)Campaign.Current.Models.MilitaryPowerModel.GetTroopPowerBasedOnContext(attackerTroop) : (double)Campaign.Current.Models.MilitaryPowerModel.GetTroopPowerBasedOnContext(attackerTroop, party.MapEvent.EventType, party.Side, missionType == CombatXpModel.MissionTypeEnum.SimulationBattle)) + 0.5) * (double)(Math.Min(damage, val2) + (isFatal ? val2 : 0)));
                double num2;
                switch (missionType)
                {
                    case CombatXpModel.MissionTypeEnum.Battle:
                        num2 = 1.0;
                        break;
                    case CombatXpModel.MissionTypeEnum.PracticeFight:
                        num2 = 1.0;
                        break;
                    case CombatXpModel.MissionTypeEnum.Tournament:
                        num2 = 1.0;
                        break;
                    case CombatXpModel.MissionTypeEnum.SimulationBattle:
                        num2 = 0.899999976158142;
                        break;
                    case CombatXpModel.MissionTypeEnum.NoXp:
                        num2 = 0.0;
                        break;
                    default:
                        num2 = 1.0;
                        break;
                }
                ExplainedNumber xpToGain = new ExplainedNumber((float)(num1 * num2));
                if (party != null)
                    this.GetBattleXpBonusFromPerks(party, ref xpToGain, attackerTroop);
                if (captain != null && captain.IsHero && captain.GetPerkValue(DefaultPerks.Leadership.InspiringLeader))
                    xpToGain.AddFactor(DefaultPerks.Leadership.InspiringLeader.SecondaryBonus, DefaultPerks.Leadership.InspiringLeader.Name);
                xpAmount = MathF.Round(xpToGain.ResultNumber);
            }

            public override float GetXpMultiplierFromShotDifficulty(float shotDifficulty)
            {
                if ((double)shotDifficulty > 14.3999996185303)
                    shotDifficulty = 14.4f;
                return MBMath.Lerp(0.0f, 2f, (float)(((double)shotDifficulty - 1.0) / 13.3999996185303));
            }

            public override float CaptainRadius => 10f;

            private void GetBattleXpBonusFromPerks(
              PartyBase party,
              ref ExplainedNumber xpToGain,
              CharacterObject troop)
            {
                if (party.IsMobile && party.MobileParty.LeaderHero != null)
                {
                    if (!troop.IsRanged && party.MobileParty.HasPerk(DefaultPerks.OneHanded.Trainer, true))
                        xpToGain.AddFactor(DefaultPerks.OneHanded.Trainer.SecondaryBonus * 0.01f, DefaultPerks.OneHanded.Trainer.Name);
                    if (troop.HasThrowingWeapon() && party.MobileParty.HasPerk(DefaultPerks.Throwing.Resourceful, true))
                        xpToGain.AddFactor(DefaultPerks.Throwing.Resourceful.SecondaryBonus * 0.01f, DefaultPerks.Throwing.Resourceful.Name);
                    if (troop.IsInfantry)
                    {
                        if (party.MobileParty.HasPerk(DefaultPerks.OneHanded.CorpsACorps))
                            xpToGain.AddFactor(DefaultPerks.OneHanded.CorpsACorps.PrimaryBonus * 0.01f, DefaultPerks.OneHanded.CorpsACorps.Name);
                        if (party.MobileParty.HasPerk(DefaultPerks.TwoHanded.BaptisedInBlood, true))
                            xpToGain.AddFactor(DefaultPerks.TwoHanded.BaptisedInBlood.SecondaryBonus * 0.01f, DefaultPerks.TwoHanded.BaptisedInBlood.Name);
                    }
                    if (party.MobileParty.HasPerk(DefaultPerks.OneHanded.LeadByExample))
                        xpToGain.AddFactor(DefaultPerks.OneHanded.LeadByExample.PrimaryBonus * 0.01f, DefaultPerks.OneHanded.LeadByExample.Name);
                    if (troop.IsRanged && party.MobileParty.HasPerk(DefaultPerks.Crossbow.MountedCrossbowman, true))
                        xpToGain.AddFactor(DefaultPerks.Crossbow.MountedCrossbowman.SecondaryBonus * 0.01f, DefaultPerks.Crossbow.MountedCrossbowman.Name);
                    if (troop.Culture.IsBandit && party.MobileParty.HasPerk(DefaultPerks.Roguery.NoRestForTheWicked))
                        xpToGain.AddFactor(DefaultPerks.Roguery.NoRestForTheWicked.PrimaryBonus * 0.01f, DefaultPerks.Roguery.NoRestForTheWicked.Name);
                }
                if (!party.IsMobile || !party.MobileParty.IsGarrison || party.MobileParty.CurrentSettlement?.Town.Governor == null)
                    return;
                PerkHelper.AddPerkBonusForTown(DefaultPerks.TwoHanded.ArrowDeflection, party.MobileParty.CurrentSettlement.Town, ref xpToGain);
                if (!troop.IsMounted)
                    return;
                PerkHelper.AddPerkBonusForTown(DefaultPerks.Polearm.Guards, party.MobileParty.CurrentSettlement.Town, ref xpToGain);
            }
        }
    }

}
