using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace Crusty.Bannerlord.AWomansLot.GameModels
{
    class AWomansLotXpModel : DefaultGenericXpModel
    {
        public override float GetXpMultiplier(Hero hero) => 2f;

    }
}
