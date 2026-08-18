using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000032 RID: 50
	internal sealed class PrecedingQuery : BaseAxisQuery
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00005DB5 File Offset: 0x00003FB5
		public PrecedingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
			this.ancestorStk = new ClonableStack<XPathNavigator>();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005DCD File Offset: 0x00003FCD
		private PrecedingQuery(PrecedingQuery other) : base(other)
		{
			this.workIterator = Query.Clone(other.workIterator);
			this.ancestorStk = other.ancestorStk.Clone();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public override void Reset()
		{
			this.workIterator = null;
			this.ancestorStk.Clear();
			base.Reset();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005E14 File Offset: 0x00004014
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

		// Token: 0x0600017C RID: 380 RVA: 0x00005F24 File Offset: 0x00004124
		public override XPathNodeIterator Clone()
		{
			return new PrecedingQuery(this);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005F2C File Offset: 0x0000412C
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x040000B3 RID: 179
		private XPathNodeIterator workIterator;

		// Token: 0x040000B4 RID: 180
		private ClonableStack<XPathNavigator> ancestorStk;
	}
}
