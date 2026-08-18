using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000144 RID: 324
	internal sealed class GroupQuery : BaseAxisQuery
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x0004FD81 File Offset: 0x0004ED81
		public GroupQuery(Query qy) : base(qy)
		{
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004FD8A File Offset: 0x0004ED8A
		private GroupQuery(GroupQuery other) : base(other)
		{
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004FD93 File Offset: 0x0004ED93
		public override XPathNavigator Advance()
		{
			this.currentNode = this.qyInput.Advance();
			if (this.currentNode != null)
			{
				this.position++;
			}
			return this.currentNode;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004FDC2 File Offset: 0x0004EDC2
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.qyInput.Evaluate(nodeIterator);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004FDD0 File Offset: 0x0004EDD0
		public override XPathNodeIterator Clone()
		{
			return new GroupQuery(this);
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0004FDD8 File Offset: 0x0004EDD8
		public override XPathResultType StaticType
		{
			get
			{
				return this.qyInput.StaticType;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0004FDE5 File Offset: 0x0004EDE5
		public override QueryProps Properties
		{
			get
			{
				return QueryProps.Position;
			}
		}
	}
}
