using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000E RID: 14
	internal sealed class CacheChildrenQuery : ChildrenQuery
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002A5E File Offset: 0x00000C5E
		public CacheChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
			this.elementStk = new ClonableStack<XPathNavigator>();
			this.positionStk = new ClonableStack<int>();
			this.needInput = true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A88 File Offset: 0x00000C88
		private CacheChildrenQuery(CacheChildrenQuery other) : base(other)
		{
			this.nextInput = Query.Clone(other.nextInput);
			this.elementStk = other.elementStk.Clone();
			this.positionStk = other.positionStk.Clone();
			this.needInput = other.needInput;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002ADB File Offset: 0x00000CDB
		public override void Reset()
		{
			this.nextInput = null;
			this.elementStk.Clear();
			this.positionStk.Clear();
			this.needInput = true;
			base.Reset();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B08 File Offset: 0x00000D08
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

		// Token: 0x06000053 RID: 83 RVA: 0x00002BD0 File Offset: 0x00000DD0
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

		// Token: 0x06000054 RID: 84 RVA: 0x00002C54 File Offset: 0x00000E54
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

		// Token: 0x06000055 RID: 85 RVA: 0x00002C90 File Offset: 0x00000E90
		public override XPathNodeIterator Clone()
		{
			return new CacheChildrenQuery(this);
		}

		// Token: 0x0400006A RID: 106
		private XPathNavigator nextInput;

		// Token: 0x0400006B RID: 107
		private ClonableStack<XPathNavigator> elementStk;

		// Token: 0x0400006C RID: 108
		private ClonableStack<int> positionStk;

		// Token: 0x0400006D RID: 109
		private bool needInput;
	}
}
