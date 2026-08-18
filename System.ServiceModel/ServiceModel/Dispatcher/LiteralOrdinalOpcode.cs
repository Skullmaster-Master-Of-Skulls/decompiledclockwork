using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F9 RID: 1273
	internal class LiteralOrdinalOpcode : Opcode
	{
		// Token: 0x06003042 RID: 12354 RVA: 0x000B8A6D File Offset: 0x000B6C6D
		internal LiteralOrdinalOpcode(int ordinal) : base(OpcodeID.LiteralOrdinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000B8A80 File Offset: 0x000B6C80
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			Value[] sequences = context.Sequences;
			context.PushFrame();
			for (int i = topSequenceArg.basePtr; i <= topSequenceArg.endPtr; i++)
			{
				NodeSequence sequence = sequences[i].Sequence;
				for (int j = 0; j < sequence.Count; j++)
				{
					context.Push(sequence[j].Position == this.ordinal);
				}
			}
			return this.next;
		}

		// Token: 0x040025F6 RID: 9718
		private int ordinal;
	}
}
