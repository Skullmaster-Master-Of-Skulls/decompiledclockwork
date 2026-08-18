using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004CF RID: 1231
	internal struct NodeSequenceBuilder
	{
		// Token: 0x06002EB4 RID: 11956 RVA: 0x000B52CF File Offset: 0x000B34CF
		internal NodeSequenceBuilder(ProcessingContext context, NodeSequence sequence)
		{
			this.context = context;
			this.sequence = sequence;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000B52DF File Offset: 0x000B34DF
		internal NodeSequenceBuilder(ProcessingContext context)
		{
			this = new NodeSequenceBuilder(context, null);
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000B52E9 File Offset: 0x000B34E9
		// (set) Token: 0x06002EB7 RID: 11959 RVA: 0x000B52FF File Offset: 0x000B34FF
		internal NodeSequence Sequence
		{
			get
			{
				if (this.sequence == null)
				{
					return NodeSequence.Empty;
				}
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B5308 File Offset: 0x000B3508
		internal void Add(ref NodeSequenceItem item)
		{
			if (this.sequence == null)
			{
				this.sequence = this.context.CreateSequence();
				this.sequence.StartNodeset();
			}
			this.sequence.Add(ref item);
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000B533A File Offset: 0x000B353A
		internal void EndNodeset()
		{
			if (this.sequence != null)
			{
				this.sequence.StopNodeset();
			}
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000B534F File Offset: 0x000B354F
		internal void StartNodeset()
		{
			if (this.sequence != null)
			{
				this.sequence.StartNodeset();
			}
		}

		// Token: 0x04002557 RID: 9559
		private ProcessingContext context;

		// Token: 0x04002558 RID: 9560
		private NodeSequence sequence;
	}
}
