using System;
using System.Collections;

namespace System.util.collections
{
	// Token: 0x02000423 RID: 1059
	public class OrderedTreeEnumerator : IEnumerator
	{
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000DBB5D File Offset: 0x000DAB5D
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000DBB65 File Offset: 0x000DAB65
		public IComparable Key
		{
			get
			{
				return this.ordKey;
			}
			set
			{
				this.ordKey = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000DBB6E File Offset: 0x000DAB6E
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x000DBB76 File Offset: 0x000DAB76
		public object Value
		{
			get
			{
				return this.objValue;
			}
			set
			{
				this.objValue = value;
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000DBB7F File Offset: 0x000DAB7F
		private OrderedTreeEnumerator()
		{
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000DBB8E File Offset: 0x000DAB8E
		public OrderedTreeEnumerator(OrderedTreeNode tnode, bool keys, bool ascending, OrderedTreeNode sentinelNode)
		{
			this.sentinelNode = sentinelNode;
			this.stack = new Stack();
			this.keys = keys;
			this.ascending = ascending;
			this.tnode = tnode;
			this.Reset();
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000DBBCC File Offset: 0x000DABCC
		public void Reset()
		{
			this.pre = true;
			this.stack.Clear();
			if (this.ascending)
			{
				while (this.tnode != this.sentinelNode)
				{
					this.stack.Push(this.tnode);
					this.tnode = this.tnode.Left;
				}
				return;
			}
			while (this.tnode != this.sentinelNode)
			{
				this.stack.Push(this.tnode);
				this.tnode = this.tnode.Right;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000DBC56 File Offset: 0x000DAC56
		public object Current
		{
			get
			{
				if (this.pre)
				{
					throw new InvalidOperationException("Current");
				}
				if (!this.keys)
				{
					return this.Value;
				}
				return this.Key;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000DBC80 File Offset: 0x000DAC80
		public bool HasMoreElements()
		{
			return this.stack.Count > 0;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000DBC90 File Offset: 0x000DAC90
		public object NextElement()
		{
			if (this.stack.Count == 0)
			{
				throw new InvalidOperationException("Element not found");
			}
			OrderedTreeNode orderedTreeNode = (OrderedTreeNode)this.stack.Peek();
			if (this.ascending)
			{
				if (orderedTreeNode.Right == this.sentinelNode)
				{
					OrderedTreeNode orderedTreeNode2 = (OrderedTreeNode)this.stack.Pop();
					while (this.HasMoreElements())
					{
						if (((OrderedTreeNode)this.stack.Peek()).Right != orderedTreeNode2)
						{
							break;
						}
						orderedTreeNode2 = (OrderedTreeNode)this.stack.Pop();
					}
				}
				else
				{
					for (OrderedTreeNode orderedTreeNode3 = orderedTreeNode.Right; orderedTreeNode3 != this.sentinelNode; orderedTreeNode3 = orderedTreeNode3.Left)
					{
						this.stack.Push(orderedTreeNode3);
					}
				}
			}
			else if (orderedTreeNode.Left == this.sentinelNode)
			{
				OrderedTreeNode orderedTreeNode4 = (OrderedTreeNode)this.stack.Pop();
				while (this.HasMoreElements())
				{
					if (((OrderedTreeNode)this.stack.Peek()).Left != orderedTreeNode4)
					{
						break;
					}
					orderedTreeNode4 = (OrderedTreeNode)this.stack.Pop();
				}
			}
			else
			{
				for (OrderedTreeNode orderedTreeNode5 = orderedTreeNode.Left; orderedTreeNode5 != this.sentinelNode; orderedTreeNode5 = orderedTreeNode5.Right)
				{
					this.stack.Push(orderedTreeNode5);
				}
			}
			this.Key = orderedTreeNode.Key;
			this.Value = orderedTreeNode.Data;
			if (!this.keys)
			{
				return orderedTreeNode.Data;
			}
			return orderedTreeNode.Key;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000DBDFD File Offset: 0x000DADFD
		public bool MoveNext()
		{
			if (this.HasMoreElements())
			{
				this.NextElement();
				this.pre = false;
				return true;
			}
			this.pre = true;
			return false;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000DBE1F File Offset: 0x000DAE1F
		public OrderedTreeEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x04001905 RID: 6405
		private Stack stack;

		// Token: 0x04001906 RID: 6406
		private bool keys;

		// Token: 0x04001907 RID: 6407
		private bool ascending;

		// Token: 0x04001908 RID: 6408
		private OrderedTreeNode tnode;

		// Token: 0x04001909 RID: 6409
		private OrderedTreeNode sentinelNode;

		// Token: 0x0400190A RID: 6410
		private bool pre = true;

		// Token: 0x0400190B RID: 6411
		private IComparable ordKey;

		// Token: 0x0400190C RID: 6412
		private object objValue;
	}
}
