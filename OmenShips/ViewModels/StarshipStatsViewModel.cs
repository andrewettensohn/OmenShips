using OmenModels;
using System.Linq;

namespace OmenShips.ViewModels
{
    public class StarshipStatsViewModel : BaseViewModel
    {
        public int ChartIndex = -1;

        private double[] _powerGridData;
        public double[] PowerGridData
        {
            get => _powerGridData;
            set
            {
                SetValue(ref _powerGridData, value);
            }
        }

        private Starship _starship;
        public Starship Starship
        {
            get => _starship;
            set
            {
                SetValue(ref _starship, value);
            }
        }

        public string[] PowerGridLabels = { "Available Power", "Used Power" };

        public StarshipStatsViewModel(Starship starship)
        {
            //Set Power Grid
            int potentialPower = starship.Modules.Sum(x => x.PowerProduction);
            int usedPower = starship.Modules.Sum(x => x.PowerRequirement);
            _powerGridData = new double[] { potentialPower, usedPower };

        }
    }
}

