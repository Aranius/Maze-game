using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Dialogue
    {
        public string Text { get; set; }
        public List<Dialogue> Responses { get; set; }
        public Action<PC> OnChosen { get; set; }
    }
}
