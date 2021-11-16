using SandBox;
using TaleWorlds.Core;

using TaleWorlds.CampaignSystem.CharacterCreationContent;


namespace Crusty.Bannerlord.Tweaks
{
    public class CrustySandBoxGameManager : SandBoxGameManager
    {
        public override void OnLoadFinished()
        {
           LaunchCrustyCharacterCreation();
        }

        private void LaunchCrustyCharacterCreation() => Game.Current.GameStateManager.CleanAndPushState(Game.Current.GameStateManager.CreateState<CharacterCreationState>(new CrustySandboxCharacterCreationContent()));
    }
}