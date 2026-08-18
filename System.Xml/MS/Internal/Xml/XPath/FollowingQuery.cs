using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200013E RID: 318
	internal sealed class FollowingQuery : BaseAxisQuery
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x0004F844 File Offset: 0x0004E844
		public FollowingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004F851 File Offset: 0x0004E851
		private FollowingQuery(FollowingQuery other) : base(other)
		{
			this.input = Query.Clone(other.input);
			this.iterator = Query.Clone(other.iterator);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0004F87C File Offset: 0x0004E87C
		public override void Reset()
		{
			this.iterator = null;
			base.Reset();
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0004F88C File Offset: 0x0004E88C
		public override XPathNavigator Advance()
		{
			if (this.iterator == null)
			{
				this.input = this.qyInput.Advance();
				if (this.input == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator;
				do
				{
					xpathNavigator = this.input.Clone();
					this.input = this.qyInput.Advance();
				}
				while (xpathNavigator.IsDescendant(this.input));
				this.input = xpathNavigator;
				this.iterator = XPathEmptyIterator.Instance;
			}
			while (!this.iterator.MoveNext())
			{
				bool matchSelf;
				if (this.input.NodeType == XPathNodeType.Attribute || this.input.NodeType == XPathNodeType.Namespace)
				{
					this.input.MoveToParent();
					matchSelf = false;
				}
				else
				{
					while (!this.input.MoveToNext())
					{
						if (!this.input.MoveToParent())
						{
							return null;
						}
					}
					matchSelf = true;
				}
				if (base.NameTest)
				{
					this.iterator = this.input.SelectDescendants(base.Name, base.Namespace, matchSelf);
				}
				else
				{
					this.iterator = this.input.SelectDescendants(base.TypeTest, matchSelf);
				}
			}
			this.position++;
			this.currentNode = this.iterator.Current;
			return this.currentNode;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004F9C0 File Offset: 0x0004E9C0
		public override XPathNodeIterator Clone()
		{
			return new FollowingQuery(this);
		}

		// Token: 0x04000B61 RID: 2913
		private XPathNavigator input;

		// Token: 0x04000B62 RID: 2914
		private XPathNodeIterator iterator;
	}
}
