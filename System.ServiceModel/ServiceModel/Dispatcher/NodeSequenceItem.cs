using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C9 RID: 1225
	internal struct NodeSequenceItem
	{
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000B4673 File Offset: 0x000B2873
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000B467B File Offset: 0x000B287B
		internal NodeSequenceItemFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000B4684 File Offset: 0x000B2884
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000B4691 File Offset: 0x000B2891
		internal bool Last
		{
			get
			{
				return (NodeSequenceItemFlags.NodesetLast & this.flags) > NodeSequenceItemFlags.None;
			}
			set
			{
				if (value)
				{
					this.flags |= NodeSequenceItemFlags.NodesetLast;
					return;
				}
				this.flags &= (NodeSequenceItemFlags)254;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000B46B7 File Offset: 0x000B28B7
		internal string LocalName
		{
			get
			{
				return this.node.LocalName;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x000B46C4 File Offset: 0x000B28C4
		internal string Name
		{
			get
			{
				return this.node.Name;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000B46D1 File Offset: 0x000B28D1
		internal string Namespace
		{
			get
			{
				return this.node.Namespace;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000B46DE File Offset: 0x000B28DE
		internal QueryNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000B46E6 File Offset: 0x000B28E6
		internal int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000B46EE File Offset: 0x000B28EE
		// (set) Token: 0x06002E69 RID: 11881 RVA: 0x000B46F6 File Offset: 0x000B28F6
		internal int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000B46FF File Offset: 0x000B28FF
		internal bool Compare(double dblVal, RelationOperator op)
		{
			return QueryValueModel.Compare(this.NumberValue(), dblVal, op);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000B470E File Offset: 0x000B290E
		internal bool Compare(string strVal, RelationOperator op)
		{
			return QueryValueModel.Compare(this.StringValue(), strVal, op);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000B471D File Offset: 0x000B291D
		internal bool Compare(ref NodeSequenceItem item, RelationOperator op)
		{
			return QueryValueModel.Compare(this.StringValue(), item.StringValue(), op);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000B4731 File Offset: 0x000B2931
		internal bool Equals(string literal)
		{
			return QueryValueModel.Equals(this.StringValue(), literal);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000B473F File Offset: 0x000B293F
		internal bool Equals(double literal)
		{
			return this.NumberValue() == literal;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000B474A File Offset: 0x000B294A
		internal SeekableXPathNavigator GetNavigator()
		{
			return this.node.MoveTo();
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000B4757 File Offset: 0x000B2957
		internal long GetNavigatorPosition()
		{
			return this.node.Position;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000B4764 File Offset: 0x000B2964
		internal double NumberValue()
		{
			return QueryValueModel.Double(this.StringValue());
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000B4771 File Offset: 0x000B2971
		internal void Set(SeekableXPathNavigator node, int position, int size)
		{
			this.node = new QueryNode(node);
			this.position = position;
			this.size = size;
			this.flags = NodeSequenceItemFlags.None;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000B4794 File Offset: 0x000B2994
		internal void Set(QueryNode node, int position, int size)
		{
			this.node = node;
			this.position = position;
			this.size = size;
			this.flags = NodeSequenceItemFlags.None;
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000B47B2 File Offset: 0x000B29B2
		internal void Set(ref NodeSequenceItem item, int position, int size)
		{
			this.node = item.node;
			this.position = position;
			this.size = size;
			this.flags = item.flags;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000B47DA File Offset: 0x000B29DA
		internal void SetPositionAndSize(int position, int size)
		{
			this.position = position;
			this.size = size;
			this.flags &= (NodeSequenceItemFlags)254;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000B47FC File Offset: 0x000B29FC
		internal void SetSizeAndLast()
		{
			this.size = 1;
			this.flags |= NodeSequenceItemFlags.NodesetLast;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000B4813 File Offset: 0x000B2A13
		internal string StringValue()
		{
			return this.node.Value;
		}

		// Token: 0x0400253E RID: 9534
		private NodeSequenceItemFlags flags;

		// Token: 0x0400253F RID: 9535
		private QueryNode node;

		// Token: 0x04002540 RID: 9536
		private int position;

		// Token: 0x04002541 RID: 9537
		private int size;
	}
}
