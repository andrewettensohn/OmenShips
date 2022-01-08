using MudBlazor;
using OmenModels;
using System.Linq;

namespace OmenShips.Rules
{
    public static class StarshipRules
    {
        public static RuleOutcome RunRulesForModuleInstallation(Starship starship, ShipModule newModule)
        {
            int availableSlots = starship.Modules.Count(x => x.Category == ModuleCategory.EmptySlot);
            int producedPower = starship.Modules.Sum(x => x.PowerProduction);
            int usedPower = starship.Modules.Sum(x => x.PowerRequirement);

            if (newModule.Category == ModuleCategory.Engine && starship.Modules.Any(x => x.Category == ModuleCategory.Engine))
            {
                return new RuleOutcome { IsSuccess = false, Message = "There is already an engine on this ship." };
            }

            if (newModule.SlotSpacesRequired > availableSlots)
            {
                return new RuleOutcome { IsSuccess = false, Message = "Not enough module slots to fit this module." };
            }

            if (newModule.PowerRequirement + usedPower > producedPower && newModule.Category != ModuleCategory.Reactor)
            {
                return new RuleOutcome { IsSuccess = false, Message = "Not enough power to fit this module." };
            }

            return new RuleOutcome { IsSuccess = true };
        }
    }
}
