using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004FA RID: 1274
	internal class ApplyFilterOpcode : Opcode
	{
		// Token: 0x06003044 RID: 12356 RVA: 0x000B8AFE File Offset: 0x000B6CFE
		internal ApplyFilterOpcode() : base(OpcodeID.Filter)
		{
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000B8B08 File Offset: 0x000B6D08
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			StackFrame topArg = context.TopArg;
			NodeSequenceBuilder nodeSequenceBuilder = new NodeSequenceBuilder(context);
			Value[] sequences = context.Sequences;
			int i = topSequenceArg.basePtr;
			int num = topArg.basePtr;
			while (i <= topSequenceArg.endPtr)
			{
				NodeSequence sequence = sequences[i].Sequence;
				if (sequence.Count > 0)
				{
					NodesetIterator nodesetIterator = new NodesetIterator(sequence);
					while (nodesetIterator.NextNodeset())
					{
						nodeSequenceBuilder.StartNodeset();
						while (nodesetIterator.NextItem())
						{
							if (context.Values[num].Boolean)
							{
								nodeSequenceBuilder.Add(ref sequence.Items[nodesetIterator.Index]);
							}
							num++;
						}
						nodeSequenceBuilder.EndNodeset();
					}
					context.ReplaceSequenceAt(i, nodeSequenceBuilder.Sequence);
					context.ReleaseSequence(sequence);
					nodeSequenceBuilder.Sequence = null;
				}
				i++;
			}
			context.PopFrame();
			return this.next;
		}
	}
}
