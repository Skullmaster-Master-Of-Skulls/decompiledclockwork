using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004FC RID: 1276
	internal class MergeOpcode : Opcode
	{
		// Token: 0x06003048 RID: 12360 RVA: 0x000B8C8C File Offset: 0x000B6E8C
		internal MergeOpcode() : base(OpcodeID.Merge)
		{
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000B8C98 File Offset: 0x000B6E98
		internal override Opcode Eval(ProcessingContext context)
		{
			Value[] values = context.Values;
			StackFrame topArg = context.TopArg;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				NodeSequence sequence = values[i].Sequence;
				NodeSequence nodeSequence = context.CreateSequence();
				for (int j = 0; j < sequence.Count; j++)
				{
					NodeSequenceItem nodeSequenceItem = sequence[j];
					nodeSequence.AddCopy(ref nodeSequenceItem);
				}
				nodeSequence.Merge();
				context.SetValue(context, i, nodeSequence);
			}
			return this.next;
		}
	}
}
