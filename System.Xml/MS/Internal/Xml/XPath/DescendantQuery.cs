using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000136 RID: 310
	internal class DescendantQuery : DescendantBaseQuery
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x0004EBB4 File Offset: 0x0004DBB4
		internal DescendantQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis) : base(qyParent, Name, Prefix, Type, matchSelf, abbrAxis)
		{
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004EBC5 File Offset: 0x0004DBC5
		public DescendantQuery(DescendantQuery other) : base(other)
		{
			this.nodeIterator = Query.Clone(other.nodeIterator);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004EBDF File Offset: 0x0004DBDF
		public override void Reset()
		{
			this.nodeIterator = null;
			base.Reset();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004EBF0 File Offset: 0x0004DBF0
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

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004ECCC File Offset: 0x0004DCCC
		public override XPathNodeIterator Clone()
		{
			return new DescendantQuery(this);
		}

		// Token: 0x04000B55 RID: 2901
		private XPathNodeIterator nodeIterator;
	}
}
