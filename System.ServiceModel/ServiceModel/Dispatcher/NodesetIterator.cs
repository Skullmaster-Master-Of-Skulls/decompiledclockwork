using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004CE RID: 1230
	internal struct NodesetIterator
	{
		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B522B File Offset: 0x000B342B
		internal NodesetIterator(NodeSequence sequence)
		{
			this.sequence = sequence;
			this.items = sequence.Items;
			this.index = -1;
			this.indexStart = -1;
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000B524E File Offset: 0x000B344E
		internal int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000B5258 File Offset: 0x000B3458
		internal bool NextItem()
		{
			if (-1 == this.index)
			{
				this.index = this.indexStart;
				return true;
			}
			if (this.items[this.index].Last)
			{
				return false;
			}
			this.index++;
			return true;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000B52A5 File Offset: 0x000B34A5
		internal bool NextNodeset()
		{
			this.indexStart = this.index + 1;
			this.index = -1;
			return this.indexStart < this.sequence.Count;
		}

		// Token: 0x04002553 RID: 9555
		private int index;

		// Token: 0x04002554 RID: 9556
		private int indexStart;

		// Token: 0x04002555 RID: 9557
		private NodeSequence sequence;

		// Token: 0x04002556 RID: 9558
		private NodeSequenceItem[] items;
	}
}
