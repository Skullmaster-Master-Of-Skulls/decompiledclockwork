using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004BD RID: 1213
	internal class NegateOpcode : MathOpcode
	{
		// Token: 0x06002E16 RID: 11798 RVA: 0x000B3CD5 File Offset: 0x000B1ED5
		internal NegateOpcode() : base(OpcodeID.Negate, MathOperator.Negate)
		{
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000B3CE0 File Offset: 0x000B1EE0
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				values[i].Negate();
			}
			return this.next;
		}
	}
}
