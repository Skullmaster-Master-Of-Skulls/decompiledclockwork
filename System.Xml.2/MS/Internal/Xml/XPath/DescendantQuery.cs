using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000016 RID: 22
	internal class DescendantQuery : DescendantBaseQuery
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000033CE File Offset: 0x000015CE
		internal DescendantQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis) : base(qyParent, Name, Prefix, Type, matchSelf, abbrAxis)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000033DF File Offset: 0x000015DF
		public DescendantQuery(DescendantQuery other) : base(other)
		{
			this.nodeIterator = Query.Clone(other.nodeIterator);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000033F9 File Offset: 0x000015F9
		public override void Reset()
		{
			this.nodeIterator = null;
			base.Reset();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003408 File Offset: 0x00001608
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.nodeIterator == null)
				{
					this.position = 0;
					XPathNavigator xpathNavigator = this.qyInput.Advance();
					if (xpathNavigator == null)
					{
						break;
					}
					if (base.NameTest)
					{
						if (base.TypeTest == XPathNodeType.ProcessingInstruction)
						{
							this.nodeIterator = new IteratorFilter(xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf), base.Name);
						}
						else
						{
							this.nodeIterator = xpathNavigator.SelectDescendants(base.Name, base.Namespace, this.matchSelf);
						}
					}
					else
					{
						this.nodeIterator = xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf);
					}
				}
				if (this.nodeIterator.MoveNext())
				{
					goto Block_4;
				}
				this.nodeIterator = null;
			}
			return null;
			Block_4:
			this.position++;
			this.currentNode = this.nodeIterator.Current;
			return this.currentNode;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034E4 File Offset: 0x000016E4
		public override XPathNodeIterator Clone()
		{
			return new DescendantQuery(this);
		}

		// Token: 0x0400007B RID: 123
		private XPathNodeIterator nodeIterator;
	}
}
