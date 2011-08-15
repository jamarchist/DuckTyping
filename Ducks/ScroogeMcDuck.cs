using System;
using System.Collections.Generic;

namespace Ducks
{
    public class ScroogeMcDuck
    {
        public string Color { get; set; }

        public Direction Fly()
        {
            return Direction.North;
        }

        public Direction Fly(Direction direction)
        {
            return direction;
        }

        public string Quack()
        {
            return "Bah! Humbug!";
        }

        public string Quack(int times)
        {
            var quack = String.Empty;
            for (int i = 0; i < times; i++)
            {
                quack += "Bah!";
            }
            return quack;
        }

        public string Quack(string sound)
        {
            return sound;
        }

        public string Quack(object sound)
        {
            return sound.ToString();
        }

        public string Quack(params object[] sounds)
        {
            var quack = String.Empty;
            foreach (var sound in sounds)
            {
                quack += sound.ToString();
            }
            return quack;
        }

        public object this[string attribute]
        {
            get { return properties[attribute]; }
            set { properties[attribute] = value; }
        }

        private readonly IDictionary<string, object> properties = new Dictionary<string, object>();
    }
}
