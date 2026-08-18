using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000170 RID: 368
	internal sealed class XPathSelfQuery : BaseAxisQuery
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0005555C File Offset: 0x0005455C
		public XPathSelfQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type) : base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00055569 File Offset: 0x00054569
		private XPathSelfQuery(XPathSelfQuery other) : base(other)
		{
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00055574 File Offset: 0x00054574
		public override XPathNavigator Advance()
		{
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				if (this.matches(this.currentNode))
				{
					this.position = 1;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000555B6 File Offset: 0x000545B6
		public override XPathNodeIterator Clone()
		{
			return new XPathSelfQuery(this);
		}
	}
}
