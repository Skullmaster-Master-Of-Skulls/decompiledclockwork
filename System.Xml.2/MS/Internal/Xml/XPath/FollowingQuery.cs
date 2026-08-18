using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001E RID: 30
	internal sealed class FollowingQuery : BaseAxisQuery
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004064 File Offset: 0x00002264
		public FollowingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004071 File Offset: 0x00002271
		private FollowingQuery(FollowingQuery other) : base(other)
		{
			this.input = Query.Clone(other.input);
			this.iterator = Query.Clone(other.iterator);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000409C File Offset: 0x0000229C
		public override void Reset()
		{
			this.iterator = null;
			base.Reset();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000040AC File Offset: 0x000022AC
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

		// Token: 0x060000CC RID: 204 RVA: 0x000041E0 File Offset: 0x000023E0
		public override XPathNodeIterator Clone()
		{
			return new FollowingQuery(this);
		}

		// Token: 0x04000087 RID: 135
		private XPathNavigator input;

		// Token: 0x04000088 RID: 136
		private XPathNodeIterator iterator;
	}
}
