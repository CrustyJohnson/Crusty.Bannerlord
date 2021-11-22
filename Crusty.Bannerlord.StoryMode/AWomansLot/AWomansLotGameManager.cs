using SandBox;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using StoryMode;
using Crusty.Bannerlord.StoryMode.AWomansLot.AWomansLotCharacterCreation;

namespace Crusty.Bannerlord.StoryMode.AWomansLot
{
    public class AWomansLotGameManager : StoryModeGameManager
    {
        private int _seed = 1234;

        public AWomansLotGameManager() => _seed = (int)DateTime.Now.Ticks & ushort.MaxValue;

        public AWomansLotGameManager(int seed) : base(seed)
        {
            _seed = seed;
        }


        public override void OnLoadFinished()
        {
            VideoPlaybackState state = Game.Current.GameStateManager.CreateState<VideoPlaybackState>();
            string moduleFullPath = ModuleHelper.GetModuleFullPath("SandBox");
            string videoPath = moduleFullPath + "Videos/campaign_intro.ivf";
            string audioPath = moduleFullPath + "Videos/campaign_intro.ogg";
            string subtitleFileBasePath = moduleFullPath + "Videos/campaign_intro";
            state.SetStartingParameters(videoPath, audioPath, subtitleFileBasePath, 60f, true);
            state.SetOnVideoFinisedDelegate(new Action(LaunchStoryModeCharacterCreation));
            Game.Current.GameStateManager.CleanAndPushState(state);
            IsLoaded = true;
        }

        private void LaunchStoryModeCharacterCreation() => Game.Current.GameStateManager.CleanAndPushState(Game.Current.GameStateManager.CreateState<CharacterCreationState>((object)new AWomansLotCharacterCreationContent()));
    }
}

