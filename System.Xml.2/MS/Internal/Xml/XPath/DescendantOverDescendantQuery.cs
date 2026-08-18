using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000017 RID: 23
	internal sealed class DescendantOverDescendantQuery : DescendantBaseQuery
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000034EC File Offset: 0x000016EC
		public DescendantOverDescendantQuery(Query qyParent, bool matchSelf, string name, string prefix, XPathNodeType typeTest, bool abbrAxis) : base(qyParent, name, prefix, typeTest, matchSelf, abbrAxis)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000034FD File Offset: 0x000016FD
		private DescendantOverDescendantQuery(DescendantOverDescendantQuery other) : base(other)
		{
			this.level = other.level;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003512 File Offset: 0x00001712
		public override void Reset()
		{
			this.level = 0;
			base.Reset();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003524 File Offset: 0x00001724
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

		// Token: 0x06000094 RID: 148 RVA: 0x000035D1 File Offset: 0x000017D1
		private bool MoveToFirstChild()
		{
			if (this.currentNode.MoveToFirstChild())
			{
				this.level++;
				return true;
			}
			return false;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000035F4 File Offset: 0x000017F4
		private bool MoveUpUntillNext()
		{
			while (!this.currentNode.MoveToNext())
			{
				this.level--;
				if (this.level == 0)
				{
					return false;
				}
				bool flag = this.currentNode.MoveToParent();
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003635 File Offset: 0x00001835
		public override XPathNodeIterator Clone()
		{
			return new DescendantOverDescendantQuery(this);
		}

		// Token: 0x0400007C RID: 124
		private int level;
	}
}
