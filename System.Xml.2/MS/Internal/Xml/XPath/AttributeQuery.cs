using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000008 RID: 8
	internal sealed class AttributeQuery : BaseAxisQuery
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000214E File Offset: 0x0000034E
		public AttributeQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type) : base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000215B File Offset: 0x0000035B
		private AttributeQuery(AttributeQuery other) : base(other)
		{
			this.onAttribute = other.onAttribute;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		public override void Reset()
		{
			this.onAttribute = false;
			base.Reset();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002180 File Offset: 0x00000380
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002218 File Offset: 0x00000418
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

		// Token: 0x06000014 RID: 20 RVA: 0x00002257 File Offset: 0x00000457
		public override XPathNodeIterator Clone()
		{
			return new AttributeQuery(this);
		}

		// Token: 0x04000054 RID: 84
		private bool onAttribute;
	}
}
