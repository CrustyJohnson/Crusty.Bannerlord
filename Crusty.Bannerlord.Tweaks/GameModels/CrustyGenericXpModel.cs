
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace Crusty.Bannerlord.Tweaks.GameModels
{
    class CrustyGenericXpModel : DefaultGenericXpModel
    {
        public override float GetXpMultiplier(Hero hero) => 2f;

    }
}
