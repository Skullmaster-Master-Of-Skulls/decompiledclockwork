using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004BC RID: 1212
	internal class ModulusOpcode : MathOpcode
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x000B3C51 File Offset: 0x000B1E51
		internal ModulusOpcode() : base(OpcodeID.Mod, MathOperator.Mod)
		{
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000B3C5C File Offset: 0x000B1E5C
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			int i = topArg.basePtr;
			int num = secondArg.basePtr;
			while (i <= topArg.endPtr)
			{
				values[num].Double = values[i].Double % values[num].Double;
				i++;
				num++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
