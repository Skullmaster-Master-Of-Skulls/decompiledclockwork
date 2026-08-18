using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000010 RID: 16
	internal class ChildrenQuery : BaseAxisQuery
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public ChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DDE File Offset: 0x00000FDE
		protected ChildrenQuery(ChildrenQuery other) : base(other)
		{
			this.iterator = Query.Clone(other.iterator);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E03 File Offset: 0x00001003
		public override void Reset()
		{
			this.iterator = XPathEmptyIterator.Instance;
			base.Reset();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E18 File Offset: 0x00001018
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

		// Token: 0x06000066 RID: 102 RVA: 0x00002ED0 File Offset: 0x000010D0
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

		// Token: 0x06000067 RID: 103 RVA: 0x00002F11 File Offset: 0x00001111
		public override XPathNodeIterator Clone()
		{
			return new ChildrenQuery(this);
		}

		// Token: 0x04000070 RID: 112
		private XPathNodeIterator iterator = XPathEmptyIterator.Instance;
	}
}
