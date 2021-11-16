using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace Crusty.Bannerlord.ProveYourLove
{
    public class SubModule : MBSubModuleBase
    {

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
			try
			{
			campaignGameStarter.AddBehavior(new ProveYourLoveCampaignBehavior());
			CrustyHelpers.OutMsg("ProveYourLoveCampaignBehavior Added!");
			}
			catch
			{
				CrustyHelpers.OutMsg("LOADING FAILED - ProveYourLoveCampaignBehavior");
			}
        }
    }
}