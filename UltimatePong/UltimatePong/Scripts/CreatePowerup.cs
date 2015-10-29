using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    class CreatePowerup : Instruction
    {
        public override InstructionResult Execute(float dt)
        {
            return InstructionResult.DoneAndCreatePowerup;
        }

        public override Instruction Reset()
        {
            return new CreatePowerup();
        }
    }
}
