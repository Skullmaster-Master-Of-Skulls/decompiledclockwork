using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200013F RID: 319
	internal sealed class FollSiblingQuery : BaseAxisQuery
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x0004F9C8 File Offset: 0x0004E9C8
		public FollSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
			this.elementStk = new ClonableStack<XPathNavigator>();
			this.parentStk = new List<XPathNavigator>();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0004F9EB File Offset: 0x0004E9EB
		private FollSiblingQuery(FollSiblingQuery other) : base(other)
		{
			this.elementStk = other.elementStk.Clone();
			this.parentStk = new List<XPathNavigator>(other.parentStk);
			this.nextInput = Query.Clone(other.nextInput);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004FA27 File Offset: 0x0004EA27
		public override void Reset()
		{
			this.elementStk.Clear();
			this.parentStk.Clear();
			this.nextInput = null;
			base.Reset();
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004FA4C File Offset: 0x0004EA4C
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

		// Token: 0x06001226 RID: 4646 RVA: 0x0004FAA0 File Offset: 0x0004EAA0
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

		// Token: 0x06001227 RID: 4647 RVA: 0x0004FAD0 File Offset: 0x0004EAD0
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

		// Token: 0x06001228 RID: 4648 RVA: 0x0004FBD7 File Offset: 0x0004EBD7
		public override XPathNodeIterator Clone()
		{
			return new FollSiblingQuery(this);
		}

		// Token: 0x04000B63 RID: 2915
		private ClonableStack<XPathNavigator> elementStk;

		// Token: 0x04000B64 RID: 2916
		private List<XPathNavigator> parentStk;

		// Token: 0x04000B65 RID: 2917
		private XPathNavigator nextInput;
	}
}
