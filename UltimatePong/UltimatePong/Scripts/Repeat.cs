using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    class Repeat : Instruction
    {
        Instruction body;
        public Repeat(Instruction body)
        {
            this.body = body;
        }

        public override InstructionResult Execute(float dt)
        {
            switch (body.Execute(dt))
            {
                case InstructionResult.Done:
                    body = body.Reset();
                    return InstructionResult.Running;
                case InstructionResult.DoneAndCreatePowerup:
                    body = body.Reset();
                    return InstructionResult.RunningAndCreatePowerup;
                case InstructionResult.Running:
                    return InstructionResult.Running;
                case InstructionResult.RunningAndCreatePowerup:
                    return InstructionResult.RunningAndCreatePowerup;
            }
            return InstructionResult.Running;
        }

        public override Instruction Reset()
        {
            return new Repeat(body.Reset());
        }
    }
}
