using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004FB RID: 1275
	internal class UnionOpcode : Opcode
	{
		// Token: 0x06003046 RID: 12358 RVA: 0x000B8C02 File Offset: 0x000B6E02
		internal UnionOpcode() : base(OpcodeID.Union)
		{
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000B8C0C File Offset: 0x000B6E0C
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			int i = topArg.basePtr;
			int num = secondArg.basePtr;
			while (i <= topArg.endPtr)
			{
				NodeSequence sequence = context.Values[i].Sequence;
				NodeSequence sequence2 = context.Values[num].Sequence;
				context.SetValue(context, num, sequence2.Union(context, sequence));
				i++;
				num++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
