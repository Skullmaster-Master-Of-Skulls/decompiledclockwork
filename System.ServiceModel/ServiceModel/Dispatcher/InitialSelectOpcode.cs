using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F6 RID: 1270
	internal class InitialSelectOpcode : SelectOpcode
	{
		// Token: 0x0600303C RID: 12348 RVA: 0x000B87E4 File Offset: 0x000B69E4
		internal InitialSelectOpcode(NodeSelectCriteria criteria) : base(OpcodeID.InitialSelect, criteria, OpcodeFlags.InitialSelect)
		{
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000B87F4 File Offset: 0x000B69F4
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			Value[] sequences = context.Sequences;
			bool sequenceStackInUse = context.SequenceStackInUse;
			context.PushSequenceFrame();
			for (int i = topSequenceArg.basePtr; i <= topSequenceArg.endPtr; i++)
			{
				NodeSequence sequence = sequences[i].Sequence;
				if (sequence.Count == 0)
				{
					if (!sequenceStackInUse)
					{
						context.PushSequence(NodeSequence.Empty);
					}
				}
				else
				{
					NodeSequenceItem[] items = sequence.Items;
					for (int j = 0; j < sequence.Count; j++)
					{
						SeekableXPathNavigator navigator = items[j].GetNavigator();
						NodeSequence nodeSequence = context.CreateSequence();
						nodeSequence.StartNodeset();
						this.criteria.Select(navigator, nodeSequence);
						nodeSequence.StopNodeset();
						context.PushSequence(nodeSequence);
					}
				}
			}
			return this.next;
		}
	}
}
