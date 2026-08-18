using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F5 RID: 1269
	internal class SelectOpcode : Opcode
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x000B85CB File Offset: 0x000B67CB
		internal SelectOpcode(NodeSelectCriteria criteria) : this(OpcodeID.Select, criteria)
		{
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000B85D6 File Offset: 0x000B67D6
		internal SelectOpcode(OpcodeID id, NodeSelectCriteria criteria) : this(id, criteria, OpcodeFlags.None)
		{
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000B85E4 File Offset: 0x000B67E4
		internal SelectOpcode(OpcodeID id, NodeSelectCriteria criteria, OpcodeFlags flags) : base(id)
		{
			this.criteria = criteria;
			this.flags |= (flags | OpcodeFlags.Select);
			if (criteria.IsCompressable && (this.flags & OpcodeFlags.InitialSelect) == OpcodeFlags.None)
			{
				this.flags |= OpcodeFlags.CompressableSelect;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x000B8638 File Offset: 0x000B6838
		internal NodeSelectCriteria Criteria
		{
			get
			{
				return this.criteria;
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000B8640 File Offset: 0x000B6840
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.criteria.Equals(((SelectOpcode)op).criteria);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000B8664 File Offset: 0x000B6864
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topSequenceArg = context.TopSequenceArg;
			Value[] sequences = context.Sequences;
			for (int i = topSequenceArg.basePtr; i <= topSequenceArg.endPtr; i++)
			{
				NodeSequence sequence = sequences[i].Sequence;
				int count = sequence.Count;
				if (count == 0)
				{
					context.ReplaceSequenceAt(i, NodeSequence.Empty);
					context.ReleaseSequence(sequence);
				}
				else
				{
					NodeSequenceItem[] items = sequence.Items;
					if (sequence.CanReuse(context))
					{
						SeekableXPathNavigator navigator = items[0].GetNavigator();
						sequence.Clear();
						sequence.StartNodeset();
						this.criteria.Select(navigator, sequence);
						sequence.StopNodeset();
					}
					else
					{
						NodeSequence nodeSequence = null;
						for (int j = 0; j < count; j++)
						{
							SeekableXPathNavigator navigator = items[j].GetNavigator();
							if (nodeSequence == null)
							{
								nodeSequence = context.CreateSequence();
							}
							nodeSequence.StartNodeset();
							this.criteria.Select(navigator, nodeSequence);
							nodeSequence.StopNodeset();
						}
						context.ReplaceSequenceAt(i, (nodeSequence != null) ? nodeSequence : NodeSequence.Empty);
						context.ReleaseSequence(sequence);
					}
				}
			}
			return this.next;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000B8784 File Offset: 0x000B6984
		internal override Opcode Eval(NodeSequence sequence, SeekableXPathNavigator node)
		{
			if (this.next == null || (this.next.Flags & OpcodeFlags.CompressableSelect) == OpcodeFlags.None)
			{
				sequence.StartNodeset();
				this.criteria.Select(node, sequence);
				sequence.StopNodeset();
				return this.next;
			}
			return this.criteria.Select(node, sequence, (SelectOpcode)this.next);
		}

		// Token: 0x040025F5 RID: 9717
		protected NodeSelectCriteria criteria;
	}
}
