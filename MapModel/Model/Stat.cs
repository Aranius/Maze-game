using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Stat
    {
        public StatEnum StatType { get; set; }
        public int Value { get; set; }

        public enum StatEnum
        {
            Attack,
            Defense,
            Speed,
            Health
        }

        public Stat(StatEnum statType, int value)
        {
            StatType = statType;
            Value = value;
        }
    }
}
