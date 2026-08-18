using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004BB RID: 1211
	internal class DivideOpcode : MathOpcode
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x000B3BCB File Offset: 0x000B1DCB
		internal DivideOpcode() : base(OpcodeID.Divide, MathOperator.Div)
		{
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000B3BD8 File Offset: 0x000B1DD8
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			int i = topArg.basePtr;
			int num = secondArg.basePtr;
			while (i <= topArg.endPtr)
			{
				values[num].Double = values[i].Double / values[num].Double;
				i++;
				num++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
