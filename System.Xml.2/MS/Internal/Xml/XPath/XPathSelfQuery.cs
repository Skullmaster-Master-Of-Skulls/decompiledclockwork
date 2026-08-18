using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004B RID: 75
	internal sealed class XPathSelfQuery : BaseAxisQuery
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00009FA0 File Offset: 0x000081A0
		public XPathSelfQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type) : base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009FAD File Offset: 0x000081AD
		private XPathSelfQuery(XPathSelfQuery other) : base(other)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009FB8 File Offset: 0x000081B8
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

		// Token: 0x0600026F RID: 623 RVA: 0x00009FFA File Offset: 0x000081FA
		public override XPathNodeIterator Clone()
		{
			return new XPathSelfQuery(this);
		}
	}
}
