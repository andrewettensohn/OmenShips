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

        public int PlasmaDamage { get; set; }
        public int RailgunDamage { get; set; }
        public int MissileDamage { get; set; }
        public int ProjectileDamage { get; set; }

        public int ShieldStrength { get; set; }
        public int ArmorStrength { get; set; }

        public int SensorStrength { get; set; }
        public int StealthRating { get; set; }
        public int SpeedRating { get; set; }

        public int Value { get; set; }


        public string[] PowerGridLabels = { "Available Power", "Used Power" };

        public StarshipStatsViewModel(Starship starship)
        {
            _starship = starship;

            //Set Power Grid
            int potentialPower = starship.Modules.Sum(x => x.PowerProduction);
            int usedPower = starship.Modules.Sum(x => x.PowerRequirement);
            _powerGridData = new double[] { potentialPower, usedPower };

            PlasmaDamage = starship.Modules.Where(x => x.DamageType == DamageType.Plasma).Sum(x => x.Damage);
            RailgunDamage = starship.Modules.Where(x => x.DamageType == DamageType.Railgun).Sum(x => x.Damage);
            MissileDamage = starship.Modules.Where(x => x.DamageType == DamageType.Missile).Sum(x => x.Damage);
            ProjectileDamage = starship.Modules.Where(x => x.DamageType == DamageType.Projectile).Sum(x => x.Damage);

            ShieldStrength = starship.Modules.Where(x => x.Category == ModuleCategory.Shield).Sum(x => x.Shield);
            ArmorStrength = starship.Modules.Where(x => x.Category == ModuleCategory.Armor).Sum(x => x.Armor);

            SensorStrength = starship.Modules.Where(x => x.Category == ModuleCategory.Sensor).Sum(x => x.Sensor);
            StealthRating = starship.Modules.Where(x => x.Category == ModuleCategory.BlackOps).Sum(x => x.Stealth) + starship.StarshipClass.BaseStealth;
            SpeedRating = starship.Modules.Sum(x => x.Speed);

            Value = starship.Modules.Sum(x => x.Value);
        }
    }
}

