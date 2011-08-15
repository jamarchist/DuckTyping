using System;

namespace Ducks
{
    public class DuckHunter
    {
        public string Track(IDuck duck)
        {
            return String.Format("The duckhunter sees a {0} duck flying {1}.", duck.Color, duck.Fly());
        }

        public string Listen(IDuck duck)
        {
            return String.Format("The duckhunter hears, {0}.", duck.Quack());
        }
    }
}
