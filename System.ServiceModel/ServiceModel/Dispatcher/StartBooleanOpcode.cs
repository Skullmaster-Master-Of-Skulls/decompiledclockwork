using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200047E RID: 1150
	internal class StartBooleanOpcode : Opcode
	{
		// Token: 0x06002CB4 RID: 11444 RVA: 0x000AE7E2 File Offset: 0x000AC9E2
		internal StartBooleanOpcode(bool test) : base(OpcodeID.StartBoolean)
		{
			this.test = test;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000AE7F3 File Offset: 0x000AC9F3
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && ((StartBooleanOpcode)op).test == this.test;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000AE814 File Offset: 0x000ACA14
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			Value[] values = context.Values;
			StackFrame topArg = context.TopArg;
			Value[] sequences = context.Sequences;
			context.PushSequenceFrame();
			for (int i = topSequenceArg.basePtr; i <= topSequenceArg.endPtr; i++)
			{
				NodeSequence sequence = sequences[i].Sequence;
				if (sequence.Count > 0)
				{
					NodeSequenceItem[] items = sequence.Items;
					NodeSequence nodeSequence = null;
					int j = topArg.basePtr;
					int num = 0;
					while (j <= topArg.endPtr)
					{
						if (this.test == values[j].Boolean)
						{
							if (nodeSequence == null)
							{
								nodeSequence = context.CreateSequence();
							}
							nodeSequence.AddCopy(ref items[num], NodeSequence.GetContextSize(sequence, num));
						}
						else if (items[num].Last && nodeSequence != null)
						{
							nodeSequence.Items[nodeSequence.Count - 1].Last = true;
						}
						j++;
						num++;
					}
					context.PushSequence((nodeSequence == null) ? NodeSequence.Empty : nodeSequence);
				}
			}
			return this.next;
		}

		// Token: 0x0400244B RID: 9291
		private bool test;
	}
}
