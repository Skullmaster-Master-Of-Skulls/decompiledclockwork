using System;

namespace System.util.collections
{
	// Token: 0x02000422 RID: 1058
	public class OrderedTree
	{
		// Token: 0x060023E0 RID: 9184 RVA: 0x000DB078 File Offset: 0x000DA078
		public OrderedTree()
		{
			this.sentinelNode = new OrderedTreeNode();
			this.sentinelNode.Left = (this.sentinelNode.Right = this.sentinelNode);
			this.sentinelNode.Parent = null;
			this.sentinelNode.Color = true;
			this.rbTree = this.sentinelNode;
			this.lastNodeFound = this.sentinelNode;
		}

		// Token: 0x17000628 RID: 1576
		public object this[IComparable key]
		{
			get
			{
				return this.GetData(key);
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("Key is null");
				}
				OrderedTreeNode orderedTreeNode = new OrderedTreeNode();
				OrderedTreeNode orderedTreeNode2 = this.rbTree;
				while (orderedTreeNode2 != this.sentinelNode)
				{
					orderedTreeNode.Parent = orderedTreeNode2;
					int num = key.CompareTo(orderedTreeNode2.Key);
					if (num == 0)
					{
						this.lastNodeFound = orderedTreeNode2;
						orderedTreeNode2.Data = value;
						return;
					}
					if (num > 0)
					{
						orderedTreeNode2 = orderedTreeNode2.Right;
					}
					else
					{
						orderedTreeNode2 = orderedTreeNode2.Left;
					}
				}
				orderedTreeNode.Key = key;
				orderedTreeNode.Data = value;
				orderedTreeNode.Left = this.sentinelNode;
				orderedTreeNode.Right = this.sentinelNode;
				if (orderedTreeNode.Parent != null)
				{
					int num = orderedTreeNode.Key.CompareTo(orderedTreeNode.Parent.Key);
					if (num > 0)
					{
						orderedTreeNode.Parent.Right = orderedTreeNode;
					}
					else
					{
						orderedTreeNode.Parent.Left = orderedTreeNode;
					}
				}
				else
				{
					this.rbTree = orderedTreeNode;
				}
				this.RestoreAfterInsert(orderedTreeNode);
				this.lastNodeFound = orderedTreeNode;
				this.intCount++;
			}
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000DB1E8 File Offset: 0x000DA1E8
		public void Add(IComparable key, object data)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Key is null");
			}
			OrderedTreeNode orderedTreeNode = new OrderedTreeNode();
			OrderedTreeNode orderedTreeNode2 = this.rbTree;
			while (orderedTreeNode2 != this.sentinelNode)
			{
				orderedTreeNode.Parent = orderedTreeNode2;
				int num = key.CompareTo(orderedTreeNode2.Key);
				if (num == 0)
				{
					throw new ArgumentException("Key duplicated");
				}
				if (num > 0)
				{
					orderedTreeNode2 = orderedTreeNode2.Right;
				}
				else
				{
					orderedTreeNode2 = orderedTreeNode2.Left;
				}
			}
			orderedTreeNode.Key = key;
			orderedTreeNode.Data = data;
			orderedTreeNode.Left = this.sentinelNode;
			orderedTreeNode.Right = this.sentinelNode;
			if (orderedTreeNode.Parent != null)
			{
				int num = orderedTreeNode.Key.CompareTo(orderedTreeNode.Parent.Key);
				if (num > 0)
				{
					orderedTreeNode.Parent.Right = orderedTreeNode;
				}
				else
				{
					orderedTreeNode.Parent.Left = orderedTreeNode;
				}
			}
			else
			{
				this.rbTree = orderedTreeNode;
			}
			this.RestoreAfterInsert(orderedTreeNode);
			this.lastNodeFound = orderedTreeNode;
			this.intCount++;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000DB2DC File Offset: 0x000DA2DC
		private void RestoreAfterInsert(OrderedTreeNode x)
		{
			while (x != this.rbTree && !x.Parent.Color)
			{
				if (x.Parent == x.Parent.Parent.Left)
				{
					OrderedTreeNode orderedTreeNode = x.Parent.Parent.Right;
					if (orderedTreeNode != null && !orderedTreeNode.Color)
					{
						x.Parent.Color = true;
						orderedTreeNode.Color = true;
						x.Parent.Parent.Color = false;
						x = x.Parent.Parent;
					}
					else
					{
						if (x == x.Parent.Right)
						{
							x = x.Parent;
							this.RotateLeft(x);
						}
						x.Parent.Color = true;
						x.Parent.Parent.Color = false;
						this.RotateRight(x.Parent.Parent);
					}
				}
				else
				{
					OrderedTreeNode orderedTreeNode = x.Parent.Parent.Left;
					if (orderedTreeNode != null && !orderedTreeNode.Color)
					{
						x.Parent.Color = true;
						orderedTreeNode.Color = true;
						x.Parent.Parent.Color = false;
						x = x.Parent.Parent;
					}
					else
					{
						if (x == x.Parent.Left)
						{
							x = x.Parent;
							this.RotateRight(x);
						}
						x.Parent.Color = true;
						x.Parent.Parent.Color = false;
						this.RotateLeft(x.Parent.Parent);
					}
				}
			}
			this.rbTree.Color = true;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000DB46C File Offset: 0x000DA46C
		public void RotateLeft(OrderedTreeNode x)
		{
			OrderedTreeNode right = x.Right;
			x.Right = right.Left;
			if (right.Left != this.sentinelNode)
			{
				right.Left.Parent = x;
			}
			if (right != this.sentinelNode)
			{
				right.Parent = x.Parent;
			}
			if (x.Parent != null)
			{
				if (x == x.Parent.Left)
				{
					x.Parent.Left = right;
				}
				else
				{
					x.Parent.Right = right;
				}
			}
			else
			{
				this.rbTree = right;
			}
			right.Left = x;
			if (x != this.sentinelNode)
			{
				x.Parent = right;
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000DB50C File Offset: 0x000DA50C
		public void RotateRight(OrderedTreeNode x)
		{
			OrderedTreeNode left = x.Left;
			x.Left = left.Right;
			if (left.Right != this.sentinelNode)
			{
				left.Right.Parent = x;
			}
			if (left != this.sentinelNode)
			{
				left.Parent = x.Parent;
			}
			if (x.Parent != null)
			{
				if (x == x.Parent.Right)
				{
					x.Parent.Right = left;
				}
				else
				{
					x.Parent.Left = left;
				}
			}
			else
			{
				this.rbTree = left;
			}
			left.Right = x;
			if (x != this.sentinelNode)
			{
				x.Parent = left;
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000DB5AC File Offset: 0x000DA5AC
		public bool ContainsKey(IComparable key)
		{
			OrderedTreeNode orderedTreeNode = this.rbTree;
			while (orderedTreeNode != this.sentinelNode)
			{
				int num = key.CompareTo(orderedTreeNode.Key);
				if (num == 0)
				{
					this.lastNodeFound = orderedTreeNode;
					return true;
				}
				if (num < 0)
				{
					orderedTreeNode = orderedTreeNode.Left;
				}
				else
				{
					orderedTreeNode = orderedTreeNode.Right;
				}
			}
			return false;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000DB5FC File Offset: 0x000DA5FC
		public object GetData(IComparable key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Key is null");
			}
			OrderedTreeNode orderedTreeNode = this.rbTree;
			while (orderedTreeNode != this.sentinelNode)
			{
				int num = key.CompareTo(orderedTreeNode.Key);
				if (num == 0)
				{
					this.lastNodeFound = orderedTreeNode;
					return orderedTreeNode.Data;
				}
				if (num < 0)
				{
					orderedTreeNode = orderedTreeNode.Left;
				}
				else
				{
					orderedTreeNode = orderedTreeNode.Right;
				}
			}
			return null;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000DB65C File Offset: 0x000DA65C
		public IComparable GetMinKey()
		{
			OrderedTreeNode left = this.rbTree;
			if (left == null || left == this.sentinelNode)
			{
				throw new InvalidOperationException("Tree is empty");
			}
			while (left.Left != this.sentinelNode)
			{
				left = left.Left;
			}
			this.lastNodeFound = left;
			return left.Key;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000DB6AC File Offset: 0x000DA6AC
		public IComparable GetMaxKey()
		{
			OrderedTreeNode right = this.rbTree;
			if (right == null || right == this.sentinelNode)
			{
				throw new InvalidOperationException("Tree is empty");
			}
			while (right.Right != this.sentinelNode)
			{
				right = right.Right;
			}
			this.lastNodeFound = right;
			return right.Key;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000DB6F9 File Offset: 0x000DA6F9
		public object GetMinValue()
		{
			return this.GetData(this.GetMinKey());
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000DB707 File Offset: 0x000DA707
		public object GetMaxValue()
		{
			return this.GetData(this.GetMaxKey());
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000DB715 File Offset: 0x000DA715
		public OrderedTreeEnumerator GetEnumerator()
		{
			return this.Elements(true);
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000DB71E File Offset: 0x000DA71E
		public OrderedTreeEnumerator Keys
		{
			get
			{
				return this.KeyElements(true);
			}
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000DB727 File Offset: 0x000DA727
		public OrderedTreeEnumerator KeyElements(bool ascending)
		{
			return new OrderedTreeEnumerator(this.rbTree, true, ascending, this.sentinelNode);
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000DB73C File Offset: 0x000DA73C
		public OrderedTreeEnumerator Values
		{
			get
			{
				return this.Elements(true);
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000DB745 File Offset: 0x000DA745
		public OrderedTreeEnumerator Elements()
		{
			return this.Elements(true);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000DB74E File Offset: 0x000DA74E
		public OrderedTreeEnumerator Elements(bool ascending)
		{
			return new OrderedTreeEnumerator(this.rbTree, false, ascending, this.sentinelNode);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000DB763 File Offset: 0x000DA763
		public bool IsEmpty()
		{
			return this.rbTree == null || this.rbTree == this.sentinelNode;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000DB780 File Offset: 0x000DA780
		public void Remove(IComparable key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Key is null");
			}
			OrderedTreeNode orderedTreeNode;
			if (key.CompareTo(this.lastNodeFound.Key) == 0)
			{
				orderedTreeNode = this.lastNodeFound;
			}
			else
			{
				orderedTreeNode = this.rbTree;
				while (orderedTreeNode != this.sentinelNode)
				{
					int num = key.CompareTo(orderedTreeNode.Key);
					if (num == 0)
					{
						break;
					}
					if (num < 0)
					{
						orderedTreeNode = orderedTreeNode.Left;
					}
					else
					{
						orderedTreeNode = orderedTreeNode.Right;
					}
				}
				if (orderedTreeNode == this.sentinelNode)
				{
					return;
				}
			}
			this.Delete(orderedTreeNode);
			this.intCount--;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000DB810 File Offset: 0x000DA810
		private void Delete(OrderedTreeNode z)
		{
			OrderedTreeNode orderedTreeNode = new OrderedTreeNode();
			OrderedTreeNode orderedTreeNode2;
			if (z.Left == this.sentinelNode || z.Right == this.sentinelNode)
			{
				orderedTreeNode2 = z;
			}
			else
			{
				orderedTreeNode2 = z.Right;
				while (orderedTreeNode2.Left != this.sentinelNode)
				{
					orderedTreeNode2 = orderedTreeNode2.Left;
				}
			}
			if (orderedTreeNode2.Left != this.sentinelNode)
			{
				orderedTreeNode = orderedTreeNode2.Left;
			}
			else
			{
				orderedTreeNode = orderedTreeNode2.Right;
			}
			orderedTreeNode.Parent = orderedTreeNode2.Parent;
			if (orderedTreeNode2.Parent != null)
			{
				if (orderedTreeNode2 == orderedTreeNode2.Parent.Left)
				{
					orderedTreeNode2.Parent.Left = orderedTreeNode;
				}
				else
				{
					orderedTreeNode2.Parent.Right = orderedTreeNode;
				}
			}
			else
			{
				this.rbTree = orderedTreeNode;
			}
			if (orderedTreeNode2 != z)
			{
				z.Key = orderedTreeNode2.Key;
				z.Data = orderedTreeNode2.Data;
			}
			if (orderedTreeNode2.Color)
			{
				this.RestoreAfterDelete(orderedTreeNode);
			}
			this.lastNodeFound = this.sentinelNode;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000DB8FC File Offset: 0x000DA8FC
		private void RestoreAfterDelete(OrderedTreeNode x)
		{
			while (x != this.rbTree && x.Color)
			{
				if (x == x.Parent.Left)
				{
					OrderedTreeNode orderedTreeNode = x.Parent.Right;
					if (!orderedTreeNode.Color)
					{
						orderedTreeNode.Color = true;
						x.Parent.Color = false;
						this.RotateLeft(x.Parent);
						orderedTreeNode = x.Parent.Right;
					}
					if (orderedTreeNode.Left.Color && orderedTreeNode.Right.Color)
					{
						orderedTreeNode.Color = false;
						x = x.Parent;
					}
					else
					{
						if (orderedTreeNode.Right.Color)
						{
							orderedTreeNode.Left.Color = true;
							orderedTreeNode.Color = false;
							this.RotateRight(orderedTreeNode);
							orderedTreeNode = x.Parent.Right;
						}
						orderedTreeNode.Color = x.Parent.Color;
						x.Parent.Color = true;
						orderedTreeNode.Right.Color = true;
						this.RotateLeft(x.Parent);
						x = this.rbTree;
					}
				}
				else
				{
					OrderedTreeNode orderedTreeNode = x.Parent.Left;
					if (!orderedTreeNode.Color)
					{
						orderedTreeNode.Color = true;
						x.Parent.Color = false;
						this.RotateRight(x.Parent);
						orderedTreeNode = x.Parent.Left;
					}
					if (orderedTreeNode.Right.Color && orderedTreeNode.Left.Color)
					{
						orderedTreeNode.Color = false;
						x = x.Parent;
					}
					else
					{
						if (orderedTreeNode.Left.Color)
						{
							orderedTreeNode.Right.Color = true;
							orderedTreeNode.Color = false;
							this.RotateLeft(orderedTreeNode);
							orderedTreeNode = x.Parent.Left;
						}
						orderedTreeNode.Color = x.Parent.Color;
						x.Parent.Color = true;
						orderedTreeNode.Left.Color = true;
						this.RotateRight(x.Parent);
						x = this.rbTree;
					}
				}
			}
			x.Color = true;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000DBAF6 File Offset: 0x000DAAF6
		public void RemoveMin()
		{
			if (this.rbTree == null || this.rbTree == this.sentinelNode)
			{
				return;
			}
			this.Remove(this.GetMinKey());
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000DBB1B File Offset: 0x000DAB1B
		public void RemoveMax()
		{
			if (this.rbTree == null || this.rbTree == this.sentinelNode)
			{
				return;
			}
			this.Remove(this.GetMaxKey());
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000DBB40 File Offset: 0x000DAB40
		public void Clear()
		{
			this.rbTree = this.sentinelNode;
			this.intCount = 0;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000DBB55 File Offset: 0x000DAB55
		public int Count
		{
			get
			{
				return this.intCount;
			}
		}

		// Token: 0x04001901 RID: 6401
		private int intCount;

		// Token: 0x04001902 RID: 6402
		private OrderedTreeNode rbTree;

		// Token: 0x04001903 RID: 6403
		private OrderedTreeNode sentinelNode;

		// Token: 0x04001904 RID: 6404
		private OrderedTreeNode lastNodeFound;
	}
}
