using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace Crusty.Bannerlord.CrustyStoryMode.GameModels
{
    class CrustyGenericXpModel : DefaultGenericXpModel
    {
        public override float GetXpMultiplier(Hero hero) => 1f;

    }
}
