using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004BA RID: 1210
	internal class MultiplyOpcode : MathOpcode
	{
		// Token: 0x06002E10 RID: 11792 RVA: 0x000B3B55 File Offset: 0x000B1D55
		internal MultiplyOpcode() : base(OpcodeID.Multiply, MathOperator.Multiply)
		{
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000B3B60 File Offset: 0x000B1D60
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			int i = topArg.basePtr;
			int num = secondArg.basePtr;
			while (i <= topArg.endPtr)
			{
				values[num].Multiply(values[i].Double);
				i++;
				num++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
