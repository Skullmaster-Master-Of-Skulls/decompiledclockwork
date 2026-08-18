using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B9 RID: 1209
	internal class MinusOpcode : MathOpcode
	{
		// Token: 0x06002E0E RID: 11790 RVA: 0x000B3ACF File Offset: 0x000B1CCF
		internal MinusOpcode() : base(OpcodeID.Minus, MathOperator.Minus)
		{
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000B3ADC File Offset: 0x000B1CDC
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			int i = topArg.basePtr;
			int num = secondArg.basePtr;
			while (i <= topArg.endPtr)
			{
				values[num].Double = values[i].Double - values[num].Double;
				i++;
				num++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
