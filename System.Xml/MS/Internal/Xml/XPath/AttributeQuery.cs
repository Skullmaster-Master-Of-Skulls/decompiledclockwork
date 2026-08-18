using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000126 RID: 294
	internal sealed class AttributeQuery : BaseAxisQuery
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x0004DB9C File Offset: 0x0004CB9C
		public AttributeQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type) : base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0004DBA9 File Offset: 0x0004CBA9
		private AttributeQuery(AttributeQuery other) : base(other)
		{
			this.onAttribute = other.onAttribute;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0004DBBE File Offset: 0x0004CBBE
		public override void Reset()
		{
			this.onAttribute = false;
			base.Reset();
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0004DBD0 File Offset: 0x0004CBD0
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this.onAttribute)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this.onAttribute = this.currentNode.MoveToFirstAttribute();
				}
				else
				{
					this.onAttribute = this.currentNode.MoveToNextAttribute();
				}
				if (this.onAttribute && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0004DC68 File Offset: 0x0004CC68
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Attribute && this.matches(context))
			{
				XPathNavigator xpathNavigator = context.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return this.qyInput.MatchNode(xpathNavigator);
				}
			}
			return null;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0004DCA7 File Offset: 0x0004CCA7
		public override XPathNodeIterator Clone()
		{
			return new AttributeQuery(this);
		}

		// Token: 0x04000B27 RID: 2855
		private bool onAttribute;
	}
}
