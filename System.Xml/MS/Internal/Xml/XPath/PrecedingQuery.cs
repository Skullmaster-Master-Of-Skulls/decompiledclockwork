using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000155 RID: 341
	internal sealed class PrecedingQuery : BaseAxisQuery
	{
		// Token: 0x060012BE RID: 4798 RVA: 0x000513F9 File Offset: 0x000503F9
		public PrecedingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
			this.ancestorStk = new ClonableStack<XPathNavigator>();
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00051411 File Offset: 0x00050411
		private PrecedingQuery(PrecedingQuery other) : base(other)
		{
			this.workIterator = Query.Clone(other.workIterator);
			this.ancestorStk = other.ancestorStk.Clone();
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0005143C File Offset: 0x0005043C
		public override void Reset()
		{
			this.workIterator = null;
			this.ancestorStk.Clear();
			base.Reset();
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00051458 File Offset: 0x00050458
		public override XPathNavigator Advance()
		{
			if (this.workIterator == null)
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
				do
				{
					xpathNavigator2.MoveTo(xpathNavigator);
				}
				while ((xpathNavigator = this.qyInput.Advance()) != null);
				if (xpathNavigator2.NodeType == XPathNodeType.Attribute || xpathNavigator2.NodeType == XPathNodeType.Namespace)
				{
					xpathNavigator2.MoveToParent();
				}
				do
				{
					this.ancestorStk.Push(xpathNavigator2.Clone());
				}
				while (xpathNavigator2.MoveToParent());
				this.workIterator = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
			}
			while (this.workIterator.MoveNext())
			{
				this.currentNode = this.workIterator.Current;
				if (this.currentNode.IsSamePosition(this.ancestorStk.Peek()))
				{
					this.ancestorStk.Pop();
					if (this.ancestorStk.Count == 0)
					{
						this.currentNode = null;
						this.workIterator = null;
						return null;
					}
				}
				else if (this.matches(this.currentNode))
				{
					this.position++;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00051568 File Offset: 0x00050568
		public override XPathNodeIterator Clone()
		{
			return new PrecedingQuery(this);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00051570 File Offset: 0x00050570
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x04000BBB RID: 3003
		private XPathNodeIterator workIterator;

		// Token: 0x04000BBC RID: 3004
		private ClonableStack<XPathNavigator> ancestorStk;
	}
}
