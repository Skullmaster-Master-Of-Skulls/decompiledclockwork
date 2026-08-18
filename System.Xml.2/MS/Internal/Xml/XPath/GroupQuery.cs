using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000023 RID: 35
	internal sealed class GroupQuery : BaseAxisQuery
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004544 File Offset: 0x00002744
		public GroupQuery(Query qy) : base(qy)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000454D File Offset: 0x0000274D
		private GroupQuery(GroupQuery other) : base(other)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004556 File Offset: 0x00002756
		public override XPathNavigator Advance()
		{
			this.currentNode = this.qyInput.Advance();
			if (this.currentNode != null)
			{
				this.position++;
			}
			return this.currentNode;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004585 File Offset: 0x00002785
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.qyInput.Evaluate(nodeIterator);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004593 File Offset: 0x00002793
		public override XPathNodeIterator Clone()
		{
			return new GroupQuery(this);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000459B File Offset: 0x0000279B
		public override XPathResultType StaticType
		{
			get
			{
				return this.qyInput.StaticType;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000045A8 File Offset: 0x000027A8
		public override QueryProps Properties
		{
			get
			{
				return QueryProps.Position;
			}
		}
	}
}
