using OmenModels;
using System;

namespace OmenShips
{
    public static class OmenConverters
    {
        public static Func<Starship, string> StarshipNameConverter = p => p?.Name;

        public static Func<StarshipHull, string> HullNameConverter = p => p?.Name;

        public static Func<StarshipClass, string> ClassNameConverter = p => p?.Name;

        public static Func<ShipModule, string> ModuleNameConverter = p => p?.Name;
    }
}
