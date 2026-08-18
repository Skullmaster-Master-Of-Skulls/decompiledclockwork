using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F8 RID: 1272
	internal class OrdinalOpcode : Opcode
	{
		// Token: 0x06003040 RID: 12352 RVA: 0x000B89B8 File Offset: 0x000B6BB8
		internal OrdinalOpcode() : base(OpcodeID.Ordinal)
		{
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000B89C4 File Offset: 0x000B6BC4
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			StackFrame topArg = context.TopArg;
			Value[] sequences = context.Sequences;
			int i = topSequenceArg.basePtr;
			int num = topArg.basePtr;
			while (i <= topSequenceArg.endPtr)
			{
				NodeSequence sequence = sequences[i].Sequence;
				for (int j = 0; j < sequence.Count; j++)
				{
					context.Values[num].Boolean = ((double)sequence[j].Position == context.Values[num].Double);
					num++;
				}
				i++;
			}
			return this.next;
		}
	}
}
