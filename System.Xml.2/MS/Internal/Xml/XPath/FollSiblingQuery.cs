using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001F RID: 31
	internal sealed class FollSiblingQuery : BaseAxisQuery
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000041E8 File Offset: 0x000023E8
		public FollSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
			this.elementStk = new ClonableStack<XPathNavigator>();
			this.parentStk = new List<XPathNavigator>();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000420B File Offset: 0x0000240B
		private FollSiblingQuery(FollSiblingQuery other) : base(other)
		{
			this.elementStk = other.elementStk.Clone();
			this.parentStk = new List<XPathNavigator>(other.parentStk);
			this.nextInput = Query.Clone(other.nextInput);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004247 File Offset: 0x00002447
		public override void Reset()
		{
			this.elementStk.Clear();
			this.parentStk.Clear();
			this.nextInput = null;
			base.Reset();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000426C File Offset: 0x0000246C
		private bool Visited(XPathNavigator nav)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < this.parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(this.parentStk[i]))
				{
					return true;
				}
			}
			this.parentStk.Add(xpathNavigator);
			return false;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000042C0 File Offset: 0x000024C0
		private XPathNavigator FetchInput()
		{
			XPathNavigator xpathNavigator;
			for (;;)
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					break;
				}
				if (!this.Visited(xpathNavigator))
				{
					goto Block_1;
				}
			}
			return null;
			Block_1:
			return xpathNavigator.Clone();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000042F0 File Offset: 0x000024F0
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.currentNode == null)
				{
					if (this.nextInput == null)
					{
						this.nextInput = this.FetchInput();
					}
					if (this.elementStk.Count == 0)
					{
						if (this.nextInput == null)
						{
							break;
						}
						this.currentNode = this.nextInput;
						this.nextInput = this.FetchInput();
					}
					else
					{
						this.currentNode = this.elementStk.Pop();
					}
				}
				while (this.currentNode.IsDescendant(this.nextInput))
				{
					this.elementStk.Push(this.currentNode);
					this.currentNode = this.nextInput;
					this.nextInput = this.qyInput.Advance();
					if (this.nextInput != null)
					{
						this.nextInput = this.nextInput.Clone();
					}
				}
				while (this.currentNode.MoveToNext())
				{
					if (this.matches(this.currentNode))
					{
						goto Block_6;
					}
				}
				this.currentNode = null;
			}
			return null;
			Block_6:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000043F7 File Offset: 0x000025F7
		public override XPathNodeIterator Clone()
		{
			return new FollSiblingQuery(this);
		}

		// Token: 0x04000089 RID: 137
		private ClonableStack<XPathNavigator> elementStk;

		// Token: 0x0400008A RID: 138
		private List<XPathNavigator> parentStk;

		// Token: 0x0400008B RID: 139
		private XPathNavigator nextInput;
	}
}
