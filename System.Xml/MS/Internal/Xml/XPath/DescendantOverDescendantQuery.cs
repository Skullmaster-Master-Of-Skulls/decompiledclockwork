using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000137 RID: 311
	internal sealed class DescendantOverDescendantQuery : DescendantBaseQuery
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0004ECD4 File Offset: 0x0004DCD4
		public DescendantOverDescendantQuery(Query qyParent, bool matchSelf, string name, string prefix, XPathNodeType typeTest, bool abbrAxis) : base(qyParent, name, prefix, typeTest, matchSelf, abbrAxis)
		{
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004ECE5 File Offset: 0x0004DCE5
		private DescendantOverDescendantQuery(DescendantOverDescendantQuery other) : base(other)
		{
			this.level = other.level;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004ECFA File Offset: 0x0004DCFA
		public override void Reset()
		{
			this.level = 0;
			base.Reset();
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004ED0C File Offset: 0x0004DD0C
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				IL_00:
				if (this.level == 0)
				{
					this.currentNode = this.qyInput.Advance();
					this.position = 0;
					if (this.currentNode == null)
					{
						break;
					}
					if (this.matchSelf && this.matches(this.currentNode))
					{
						goto Block_3;
					}
					this.currentNode = this.currentNode.Clone();
					if (!this.MoveToFirstChild())
					{
						continue;
					}
				}
				else if (!this.MoveUpUntillNext())
				{
					continue;
				}
				while (!this.matches(this.currentNode))
				{
					if (!this.MoveToFirstChild())
					{
						goto IL_00;
					}
				}
				goto Block_5;
			}
			return null;
			Block_3:
			this.position = 1;
			return this.currentNode;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004EDB9 File Offset: 0x0004DDB9
		private bool MoveToFirstChild()
		{
			if (this.currentNode.MoveToFirstChild())
			{
				this.level++;
				return true;
			}
			return false;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004EDD9 File Offset: 0x0004DDD9
		private bool MoveUpUntillNext()
		{
			while (!this.currentNode.MoveToNext())
			{
				this.level--;
				if (this.level == 0)
				{
					return false;
				}
				this.currentNode.MoveToParent();
			}
			return true;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004EE0F File Offset: 0x0004DE0F
		public override XPathNodeIterator Clone()
		{
			return new DescendantOverDescendantQuery(this);
		}

		// Token: 0x04000B56 RID: 2902
		private int level;
	}
}
