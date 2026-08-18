using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004CA RID: 1226
	internal class NodeSequence
	{
		// Token: 0x06002E78 RID: 11896 RVA: 0x000B4820 File Offset: 0x000B2A20
		internal NodeSequence() : this(8, null)
		{
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000B482A File Offset: 0x000B2A2A
		internal NodeSequence(int capacity) : this(capacity, null)
		{
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000B4834 File Offset: 0x000B2A34
		internal NodeSequence(int capacity, ProcessingContext ownerContext)
		{
			this.items = new NodeSequenceItem[capacity];
			this.ownerContext = ownerContext;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000B484F File Offset: 0x000B2A4F
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000B0A RID: 2826
		internal NodeSequenceItem this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000B4865 File Offset: 0x000B2A65
		internal NodeSequenceItem[] Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000B486D File Offset: 0x000B2A6D
		internal bool IsNotEmpty
		{
			get
			{
				return this.count > 0;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000B4878 File Offset: 0x000B2A78
		internal string LocalName
		{
			get
			{
				if (this.count > 0)
				{
					return this.items[0].LocalName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000B489A File Offset: 0x000B2A9A
		internal string Name
		{
			get
			{
				if (this.count > 0)
				{
					return this.items[0].Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000B48BC File Offset: 0x000B2ABC
		internal string Namespace
		{
			get
			{
				if (this.count > 0)
				{
					return this.items[0].Namespace;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000B48DE File Offset: 0x000B2ADE
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x000B48E6 File Offset: 0x000B2AE6
		internal NodeSequence Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000B48EF File Offset: 0x000B2AEF
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x000B48F7 File Offset: 0x000B2AF7
		internal ProcessingContext OwnerContext
		{
			get
			{
				return this.ownerContext;
			}
			set
			{
				this.ownerContext = value;
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000B4900 File Offset: 0x000B2B00
		internal void Add(XPathNodeIterator iter)
		{
			while (iter.MoveNext())
			{
				XPathNavigator xpathNavigator = iter.Current;
				SeekableXPathNavigator seekableXPathNavigator = xpathNavigator as SeekableXPathNavigator;
				if (seekableXPathNavigator == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryMustBeSeekable")));
				}
				this.Add(seekableXPathNavigator);
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000B494C File Offset: 0x000B2B4C
		internal void Add(SeekableXPathNavigator node)
		{
			if (this.count == this.items.Length)
			{
				this.Grow(this.items.Length * 2);
			}
			this.position++;
			NodeSequenceItem[] array = this.items;
			int num = this.count;
			this.count = num + 1;
			array[num].Set(node, this.position, this.sizePosition);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000B49B8 File Offset: 0x000B2BB8
		internal void Add(QueryNode node)
		{
			if (this.count == this.items.Length)
			{
				this.Grow(this.items.Length * 2);
			}
			this.position++;
			NodeSequenceItem[] array = this.items;
			int num = this.count;
			this.count = num + 1;
			array[num].Set(node, this.position, this.sizePosition);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000B4A24 File Offset: 0x000B2C24
		internal void Add(ref NodeSequenceItem item)
		{
			if (this.count == this.items.Length)
			{
				this.Grow(this.items.Length * 2);
			}
			this.position++;
			NodeSequenceItem[] array = this.items;
			int num = this.count;
			this.count = num + 1;
			array[num].Set(ref item, this.position, this.sizePosition);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000B4A90 File Offset: 0x000B2C90
		internal void AddCopy(ref NodeSequenceItem item, int size)
		{
			if (this.count == this.items.Length)
			{
				this.Grow(this.items.Length * 2);
			}
			this.items[this.count] = item;
			NodeSequenceItem[] array = this.items;
			int num = this.count;
			this.count = num + 1;
			array[num].Size = size;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000B4AF8 File Offset: 0x000B2CF8
		internal void AddCopy(ref NodeSequenceItem item)
		{
			if (this.count == this.items.Length)
			{
				this.Grow(this.items.Length * 2);
			}
			NodeSequenceItem[] array = this.items;
			int num = this.count;
			this.count = num + 1;
			array[num] = item;
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000B4B47 File Offset: 0x000B2D47
		internal bool CanReuse(ProcessingContext context)
		{
			return this.count == 1 && this.ownerContext == context && this.refCount == 1;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000B4B66 File Offset: 0x000B2D66
		internal void Clear()
		{
			this.count = 0;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000B4B6F File Offset: 0x000B2D6F
		internal void Reset(NodeSequence nextSeq)
		{
			this.count = 0;
			this.refCount = 0;
			this.next = nextSeq;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000B4B88 File Offset: 0x000B2D88
		internal bool Compare(double val, RelationOperator op)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Compare(val, op))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000B4BC0 File Offset: 0x000B2DC0
		internal bool Compare(string val, RelationOperator op)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Compare(val, op))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000B4BF8 File Offset: 0x000B2DF8
		internal bool Compare(ref NodeSequenceItem item, RelationOperator op)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Compare(ref item, op))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000B4C30 File Offset: 0x000B2E30
		internal bool Compare(NodeSequence sequence, RelationOperator op)
		{
			for (int i = 0; i < sequence.count; i++)
			{
				if (this.Compare(ref sequence.items[i], op))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000B4C68 File Offset: 0x000B2E68
		internal bool Equals(string val)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(val))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000B4CA0 File Offset: 0x000B2EA0
		internal bool Equals(double val)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(val))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000B4CD8 File Offset: 0x000B2ED8
		internal static int GetContextSize(NodeSequence sequence, int itemIndex)
		{
			int size = sequence.items[itemIndex].Size;
			if (size <= 0)
			{
				return sequence.items[-size].Size;
			}
			return size;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000B4D10 File Offset: 0x000B2F10
		private void Grow(int newSize)
		{
			NodeSequenceItem[] destinationArray = new NodeSequenceItem[newSize];
			if (this.items != null)
			{
				Array.Copy(this.items, destinationArray, this.items.Length);
			}
			this.items = destinationArray;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000B4D47 File Offset: 0x000B2F47
		internal void Merge()
		{
			this.Merge(true);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000B4D50 File Offset: 0x000B2F50
		internal void Merge(bool renumber)
		{
			if (this.count == 0)
			{
				return;
			}
			if (renumber)
			{
				this.RenumberItems();
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000B4D64 File Offset: 0x000B2F64
		private void RenumberItems()
		{
			if (this.count > 0)
			{
				for (int i = 0; i < this.count; i++)
				{
					this.items[i].SetPositionAndSize(i + 1, this.count);
				}
				NodeSequenceItem[] array = this.items;
				int num = this.count - 1;
				array[num].Flags = (array[num].Flags | NodeSequenceItemFlags.NodesetLast);
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000B4DC5 File Offset: 0x000B2FC5
		internal void StartNodeset()
		{
			this.position = 0;
			this.sizePosition = -this.count;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000B4DDC File Offset: 0x000B2FDC
		internal void StopNodeset()
		{
			int num = this.position;
			if (num != 0)
			{
				if (num != 1)
				{
					int num2 = -this.sizePosition;
					this.items[num2].Size = this.position;
					this.items[num2 + this.position - 1].Last = true;
					return;
				}
				this.items[-this.sizePosition].SetSizeAndLast();
			}
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000B4E49 File Offset: 0x000B3049
		internal string StringValue()
		{
			if (this.count > 0)
			{
				return this.items[0].StringValue();
			}
			return string.Empty;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000B4E6C File Offset: 0x000B306C
		internal NodeSequence Union(ProcessingContext context, NodeSequence otherSeq)
		{
			NodeSequence nodeSequence = context.CreateSequence();
			SortedBuffer<QueryNode, QueryNodeComparer> sortedBuffer = new SortedBuffer<QueryNode, QueryNodeComparer>(NodeSequence.staticQueryNodeComparerInstance);
			for (int i = 0; i < this.count; i++)
			{
				sortedBuffer.Add(this.items[i].Node);
			}
			for (int j = 0; j < otherSeq.count; j++)
			{
				sortedBuffer.Add(otherSeq.items[j].Node);
			}
			for (int k = 0; k < sortedBuffer.Count; k++)
			{
				nodeSequence.Add(sortedBuffer[k]);
			}
			nodeSequence.RenumberItems();
			return nodeSequence;
		}

		// Token: 0x04002542 RID: 9538
		private int count;

		// Token: 0x04002543 RID: 9539
		internal static NodeSequence Empty = new NodeSequence(0);

		// Token: 0x04002544 RID: 9540
		private NodeSequenceItem[] items;

		// Token: 0x04002545 RID: 9541
		private NodeSequence next;

		// Token: 0x04002546 RID: 9542
		private ProcessingContext ownerContext;

		// Token: 0x04002547 RID: 9543
		private int position;

		// Token: 0x04002548 RID: 9544
		internal int refCount;

		// Token: 0x04002549 RID: 9545
		private int sizePosition;

		// Token: 0x0400254A RID: 9546
		private static readonly QueryNodeComparer staticQueryNodeComparerInstance = new QueryNodeComparer();
	}
}
