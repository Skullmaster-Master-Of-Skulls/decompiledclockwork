using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200012D RID: 301
	internal class ChildrenQuery : BaseAxisQuery
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x0004E1DA File Offset: 0x0004D1DA
		public ChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004E1F2 File Offset: 0x0004D1F2
		protected ChildrenQuery(ChildrenQuery other) : base(other)
		{
			this.iterator = Query.Clone(other.iterator);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004E217 File Offset: 0x0004D217
		public override void Reset()
		{
			this.iterator = XPathEmptyIterator.Instance;
			base.Reset();
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004E22C File Offset: 0x0004D22C
		public override XPathNavigator Advance()
		{
			while (!this.iterator.MoveNext())
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				if (base.NameTest)
				{
					if (base.TypeTest == XPathNodeType.ProcessingInstruction)
					{
						this.iterator = new IteratorFilter(xpathNavigator.SelectChildren(base.TypeTest), base.Name);
					}
					else
					{
						this.iterator = xpathNavigator.SelectChildren(base.Name, base.Namespace);
					}
				}
				else
				{
					this.iterator = xpathNavigator.SelectChildren(base.TypeTest);
				}
				this.position = 0;
			}
			this.position++;
			this.currentNode = this.iterator.Current;
			return this.currentNode;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0004E2E4 File Offset: 0x0004D2E4
		public sealed override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context == null || !this.matches(context))
			{
				return null;
			}
			XPathNavigator xpathNavigator = context.Clone();
			if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.MoveToParent())
			{
				return this.qyInput.MatchNode(xpathNavigator);
			}
			return null;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004E325 File Offset: 0x0004D325
		public override XPathNodeIterator Clone()
		{
			return new ChildrenQuery(this);
		}

		// Token: 0x04000B44 RID: 2884
		private XPathNodeIterator iterator = XPathEmptyIterator.Instance;
	}
}
