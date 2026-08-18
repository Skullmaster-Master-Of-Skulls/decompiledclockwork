using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B8 RID: 1208
	internal class PlusOpcode : MathOpcode
	{
		// Token: 0x06002E0C RID: 11788 RVA: 0x000B3A58 File Offset: 0x000B1C58
		internal PlusOpcode() : base(OpcodeID.Plus, MathOperator.Plus)
		{
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000B3A64 File Offset: 0x000B1C64
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			int i = topArg.basePtr;
			int num = secondArg.basePtr;
			while (i <= topArg.endPtr)
			{
				values[num].Add(values[i].Double);
				i++;
				num++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
