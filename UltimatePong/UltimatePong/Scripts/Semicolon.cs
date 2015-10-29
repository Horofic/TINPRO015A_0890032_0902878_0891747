using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    class Semicolon : Instruction
    {
        Instruction A, B;
        bool isADone = false, isBDone = false;
        public Semicolon(Instruction A, Instruction B)
        {
            this.A = A;
            this.B = B;
        }

        public override InstructionResult Execute(float dt)
        {
            if (!isADone)
            {
                var Ares = A.Execute(dt);
                switch (Ares)
                {
                    case InstructionResult.Done:
                        isADone = true;
                        return InstructionResult.Running;
                    case InstructionResult.DoneAndCreatePowerup:
                        isADone = true;
                        return InstructionResult.RunningAndCreatePowerup;
                    default:
                        return Ares;
                }
            }
            else
            {
                if (!isBDone)
                {
                    var Bres = B.Execute(dt);
                    switch (Bres)
                    {
                        case InstructionResult.Done:
                            isBDone = true;
                            break;
                        case InstructionResult.DoneAndCreatePowerup:
                            isBDone = true;
                            break;
                    }
                    return Bres;
                }
                else
                {
                    return InstructionResult.Done;
                }
            }
        }

        public override Instruction Reset()
        {
            return new Semicolon(A.Reset(), B.Reset());
        }
    }
}
