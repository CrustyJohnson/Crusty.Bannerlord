using System;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using SandBox;
using Crusty.Bannerlord.AWomansLot.AWomansLotCharacterCreation;
using TaleWorlds.SaveSystem.Load;
using StoryMode;

namespace Crusty.Bannerlord.AWomansLot
{
    public class AWomansLotGameManager : SandBoxGameManager { 


        public override void OnLoadFinished()
        {
            LaunchAWomansLotCharacterCreation();
            IsLoaded = true;
        }



        private void LaunchAWomansLotCharacterCreation() => Game.Current.GameStateManager.CleanAndPushState((GameState)Game.Current.GameStateManager.CreateState<CharacterCreationState>((object)new AWomansLotCharacterCreationContent()));
    }

}

