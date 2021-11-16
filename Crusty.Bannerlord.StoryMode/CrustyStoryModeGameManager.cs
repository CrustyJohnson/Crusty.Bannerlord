using Crusty.Bannerlord.CrustyStoryMode;
using SandBox;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using StoryMode;

namespace Crusty.Bannerlord.CrustyStoryMode
{
    public class CrustyStoryModeGameManager : StoryModeGameManager
    {
        private int _seed = 1234;

        public CrustyStoryModeGameManager() => this._seed = (int)DateTime.Now.Ticks & (int)ushort.MaxValue;

        public CrustyStoryModeGameManager(int seed) : base(seed)
        {
            this._seed = seed;
        }


        public override void OnLoadFinished()
        {
            VideoPlaybackState state = Game.Current.GameStateManager.CreateState<VideoPlaybackState>();
            string moduleFullPath = ModuleHelper.GetModuleFullPath("SandBox");
            string videoPath = moduleFullPath + "Videos/campaign_intro.ivf";
            string audioPath = moduleFullPath + "Videos/campaign_intro.ogg";
            string subtitleFileBasePath = moduleFullPath + "Videos/campaign_intro";
            state.SetStartingParameters(videoPath, audioPath, subtitleFileBasePath, 60f, true);
            state.SetOnVideoFinisedDelegate(new Action(this.LaunchStoryModeCharacterCreation));
            Game.Current.GameStateManager.CleanAndPushState((GameState)state);
            this.IsLoaded = true;
        }

        private void LaunchStoryModeCharacterCreation() => Game.Current.GameStateManager.CleanAndPushState((GameState)Game.Current.GameStateManager.CreateState<CharacterCreationState>((object)new CrustyStoryModeCharacterCreationContent()));
    }
}

