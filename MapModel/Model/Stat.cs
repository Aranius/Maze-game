using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Stat
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Stat(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
