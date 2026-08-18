using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200012E RID: 302
	internal sealed class CacheChildrenQuery : ChildrenQuery
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x0004E32D File Offset: 0x0004D32D
		public CacheChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
			this.elementStk = new ClonableStack<XPathNavigator>();
			this.positionStk = new ClonableStack<int>();
			this.needInput = true;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004E358 File Offset: 0x0004D358
		private CacheChildrenQuery(CacheChildrenQuery other) : base(other)
		{
			this.nextInput = Query.Clone(other.nextInput);
			this.elementStk = other.elementStk.Clone();
			this.positionStk = other.positionStk.Clone();
			this.needInput = other.needInput;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004E3AB File Offset: 0x0004D3AB
		public override void Reset()
		{
			this.nextInput = null;
			this.elementStk.Clear();
			this.positionStk.Clear();
			this.needInput = true;
			base.Reset();
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004E3D8 File Offset: 0x0004D3D8
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.needInput)
				{
					if (this.elementStk.Count == 0)
					{
						this.currentNode = this.GetNextInput();
						if (this.currentNode == null)
						{
							break;
						}
						if (!this.currentNode.MoveToFirstChild())
						{
							continue;
						}
						this.position = 0;
					}
					else
					{
						this.currentNode = this.elementStk.Pop();
						this.position = this.positionStk.Pop();
						if (!this.DecideNextNode())
						{
							continue;
						}
					}
					this.needInput = false;
				}
				else if (!this.currentNode.MoveToNext() || !this.DecideNextNode())
				{
					this.needInput = true;
					continue;
				}
				if (this.matches(this.currentNode))
				{
					goto Block_5;
				}
			}
			return null;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0004E4A0 File Offset: 0x0004D4A0
		private bool DecideNextNode()
		{
			this.nextInput = this.GetNextInput();
			if (this.nextInput != null && Query.CompareNodes(this.currentNode, this.nextInput) == XmlNodeOrder.After)
			{
				this.elementStk.Push(this.currentNode);
				this.positionStk.Push(this.position);
				this.currentNode = this.nextInput;
				this.nextInput = null;
				if (!this.currentNode.MoveToFirstChild())
				{
					return false;
				}
				this.position = 0;
			}
			return true;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004E524 File Offset: 0x0004D524
		private XPathNavigator GetNextInput()
		{
			XPathNavigator xpathNavigator;
			if (this.nextInput != null)
			{
				xpathNavigator = this.nextInput;
				this.nextInput = null;
			}
			else
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator != null)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
			}
			return xpathNavigator;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004E560 File Offset: 0x0004D560
		public override XPathNodeIterator Clone()
		{
			return new CacheChildrenQuery(this);
		}

		// Token: 0x04000B45 RID: 2885
		private XPathNavigator nextInput;

		// Token: 0x04000B46 RID: 2886
		private ClonableStack<XPathNavigator> elementStk;

		// Token: 0x04000B47 RID: 2887
		private ClonableStack<int> positionStk;

		// Token: 0x04000B48 RID: 2888
		private bool needInput;
	}
}
