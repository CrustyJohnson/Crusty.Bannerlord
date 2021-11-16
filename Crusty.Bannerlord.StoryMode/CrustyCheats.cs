using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Crusty.Bannerlord.CrustyStoryMode
{
       public static class CrustyCheats
        {
            [CommandLineFunctionality.CommandLineArgumentFunction("test", "crusty")]
            public static void EmptyEquipment()
            {
                try
                {
                    Hero hero1 = Hero.MainHero;
                    Hero hero2 = Hero.OneToOneConversationHero;
                    hero2.BattleEquipment.FillFrom(new Equipment());

                }
                catch
                {
                    InformationManager.DisplayMessage(new InformationMessage("That didn't work"));
                }
            }

        }
    }

