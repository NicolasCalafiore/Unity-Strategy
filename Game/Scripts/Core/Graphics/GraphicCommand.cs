using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Scripts.Core.Graphics
{

    public enum COMMANDTYPE
    {
        HEX,
        UNIT,
    }
    public class GraphicCommand
    {

        public COMMANDTYPE type;
        public string instruc;

        public GraphicCommand(COMMANDTYPE type, string instruc) {
            this.type = type;
            this.instruc = instruc;
        }
    }
}
