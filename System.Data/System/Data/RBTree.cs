using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x02000087 RID: 135
	internal abstract class RBTree<K> : IEnumerable
	{
		// Token: 0x060007E1 RID: 2017
		protected abstract int CompareNode(K record1, K record2);

		// Token: 0x060007E2 RID: 2018
		protected abstract int CompareSateliteTreeNode(K record1, K record2);

		// Token: 0x060007E3 RID: 2019 RVA: 0x001F5A58 File Offset: 0x001F4E58
		protected RBTree(TreeAccessMethod accessMethod)
		{
			this._accessMethod = accessMethod;
			this.InitTree();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x001F5A78 File Offset: 0x001F4E78
		private void InitTree()
		{
			this.root = 0;
			this._pageTable = new RBTree<K>.TreePage[32];
			this._pageTableMap = new int[(this._pageTable.Length + 32 - 1) / 32];
			this._inUsePageCount = 0;
			this.nextFreePageLine = 0;
			this.AllocPage(32);
			this._pageTable[0].Slots[0].nodeColor = RBTree<K>.NodeColor.black;
			this._pageTable[0].SlotMap[0] = 1;
			this._pageTable[0].InUseCount = 1;
			this._inUseNodeCount = 1;
			this._inUseSatelliteTreeCount = 0;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x001F5B18 File Offset: 0x001F4F18
		private void FreePage(RBTree<K>.TreePage page)
		{
			this.MarkPageFree(page);
			this._pageTable[page.PageId] = null;
			this._inUsePageCount--;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x001F5B48 File Offset: 0x001F4F48
		private RBTree<K>.TreePage AllocPage(int size)
		{
			int num = this.GetIndexOfPageWithFreeSlot(false);
			if (num != -1)
			{
				this._pageTable[num] = new RBTree<K>.TreePage(size);
				this.nextFreePageLine = num / 32;
			}
			else
			{
				RBTree<K>.TreePage[] array = new RBTree<K>.TreePage[this._pageTable.Length * 2];
				Array.Copy(this._pageTable, 0, array, 0, this._pageTable.Length);
				int[] array2 = new int[(array.Length + 32 - 1) / 32];
				Array.Copy(this._pageTableMap, 0, array2, 0, this._pageTableMap.Length);
				this.nextFreePageLine = this._pageTableMap.Length;
				num = this._pageTable.Length;
				this._pageTable = array;
				this._pageTableMap = array2;
				this._pageTable[num] = new RBTree<K>.TreePage(size);
			}
			this._pageTable[num].PageId = num;
			this._inUsePageCount++;
			return this._pageTable[num];
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x001F5C28 File Offset: 0x001F5028
		private void MarkPageFull(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] |= 1 << page.PageId % 32;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x001F5C68 File Offset: 0x001F5068
		private void MarkPageFree(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] &= ~(1 << page.PageId % 32);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x001F5CA8 File Offset: 0x001F50A8
		private static int GetIntValueFromBitMap(uint bitMap)
		{
			int num = 0;
			if ((bitMap & 4294901760U) != 0U)
			{
				num += 16;
				bitMap >>= 16;
			}
			if ((bitMap & 65280U) != 0U)
			{
				num += 8;
				bitMap >>= 8;
			}
			if ((bitMap & 240U) != 0U)
			{
				num += 4;
				bitMap >>= 4;
			}
			if ((bitMap & 12U) != 0U)
			{
				num += 2;
				bitMap >>= 2;
			}
			if ((bitMap & 2U) != 0U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x001F5D08 File Offset: 0x001F5108
		private void FreeNode(int nodeId)
		{
			RBTree<K>.TreePage treePage = this._pageTable[nodeId >> 16];
			int num = nodeId & 65535;
			treePage.Slots[num] = default(RBTree<K>.Node);
			treePage.SlotMap[num / 32] &= ~(1 << num % 32);
			treePage.InUseCount--;
			this._inUseNodeCount--;
			if (treePage.InUseCount == 0)
			{
				this.FreePage(treePage);
				return;
			}
			if (treePage.InUseCount == treePage.Slots.Length - 1)
			{
				this.MarkPageFree(treePage);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x001F5DA8 File Offset: 0x001F51A8
		private int GetIndexOfPageWithFreeSlot(bool allocatedPage)
		{
			int i = this.nextFreePageLine;
			int num = -1;
			while (i < this._pageTableMap.Length)
			{
				if (this._pageTableMap[i] < -1)
				{
					uint num2 = (uint)this._pageTableMap[i];
					while ((num2 ^ 4294967295U) != 0U)
					{
						uint num3 = ~num2 & num2 + 1U;
						if (((long)this._pageTableMap[i] & (long)((ulong)num3)) != 0L)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.PagePositionInSlotInUse);
						}
						num = i * 32 + RBTree<K>.GetIntValueFromBitMap(num3);
						if (allocatedPage)
						{
							if (this._pageTable[num] != null)
							{
								return num;
							}
						}
						else if (this._pageTable[num] == null)
						{
							return num;
						}
						num = -1;
						num2 |= num3;
					}
				}
				i++;
			}
			if (this.nextFreePageLine != 0)
			{
				this.nextFreePageLine = 0;
				num = this.GetIndexOfPageWithFreeSlot(allocatedPage);
			}
			return num;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x001F5E58 File Offset: 0x001F5258
		public int Count
		{
			get
			{
				return this._inUseNodeCount - 1;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x001F5E78 File Offset: 0x001F5278
		public bool HasDuplicates
		{
			get
			{
				return 0 != this._inUseSatelliteTreeCount;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x001F5E98 File Offset: 0x001F5298
		private int GetNewNode(K key)
		{
			int indexOfPageWithFreeSlot = this.GetIndexOfPageWithFreeSlot(true);
			RBTree<K>.TreePage treePage;
			if (indexOfPageWithFreeSlot != -1)
			{
				treePage = this._pageTable[indexOfPageWithFreeSlot];
			}
			else if (this._inUsePageCount < 4)
			{
				treePage = this.AllocPage(32);
			}
			else if (this._inUsePageCount < 32)
			{
				treePage = this.AllocPage(256);
			}
			else if (this._inUsePageCount < 128)
			{
				treePage = this.AllocPage(1024);
			}
			else if (this._inUsePageCount < 4096)
			{
				treePage = this.AllocPage(4096);
			}
			else if (this._inUsePageCount < 32768)
			{
				treePage = this.AllocPage(8192);
			}
			else
			{
				treePage = this.AllocPage(65536);
			}
			int num = treePage.AllocSlot(this);
			if (num == -1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NoFreeSlots);
			}
			treePage.Slots[num].selfId = (treePage.PageId << 16 | num);
			treePage.Slots[num].subTreeSize = 1;
			treePage.Slots[num].keyOfNode = key;
			return treePage.Slots[num].selfId;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x001F5FB8 File Offset: 0x001F53B8
		private int Successor(int x_id)
		{
			if (this.Right(x_id) != 0)
			{
				return this.Minimum(this.Right(x_id));
			}
			int num = this.Parent(x_id);
			while (num != 0 && x_id == this.Right(num))
			{
				x_id = num;
				num = this.Parent(num);
			}
			return num;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x001F6008 File Offset: 0x001F5408
		private bool Successor(ref int nodeId, ref int mainTreeNodeId)
		{
			if (nodeId == 0)
			{
				nodeId = this.Minimum(mainTreeNodeId);
				mainTreeNodeId = 0;
			}
			else
			{
				nodeId = this.Successor(nodeId);
				if (nodeId == 0 && mainTreeNodeId != 0)
				{
					nodeId = this.Successor(mainTreeNodeId);
					mainTreeNodeId = 0;
				}
			}
			if (nodeId != 0)
			{
				if (this.Next(nodeId) != 0)
				{
					if (mainTreeNodeId != 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NestedSatelliteTreeEnumerator);
					}
					mainTreeNodeId = nodeId;
					nodeId = this.Minimum(this.Next(nodeId));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x001F6078 File Offset: 0x001F5478
		private int Minimum(int x_id)
		{
			while (this.Left(x_id) != 0)
			{
				x_id = this.Left(x_id);
			}
			return x_id;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x001F60A8 File Offset: 0x001F54A8
		private int LeftRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Right(x_id);
			this.SetRight(x_id, this.Left(num));
			if (this.Left(num) != 0)
			{
				this.SetParent(this.Left(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetLeft(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x001F61E8 File Offset: 0x001F55E8
		private int RightRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Left(x_id);
			this.SetLeft(x_id, this.Right(num));
			if (this.Right(num) != 0)
			{
				this.SetParent(this.Right(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetRight(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x001F6328 File Offset: 0x001F5728
		private int RBInsert(int root_id, int x_id, int mainTreeNodeID, int position, bool append)
		{
			this._version++;
			int num = 0;
			int num2 = (root_id == 0) ? this.root : root_id;
			if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX && !append)
			{
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num3 = (root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num2)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num2));
					if (num3 < 0)
					{
						num2 = this.Left(num2);
					}
					else if (num3 > 0)
					{
						num2 = this.Right(num2);
					}
					else
					{
						if (root_id != 0)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinInsert);
						}
						if (this.Next(num2) != 0)
						{
							root_id = this.RBInsert(this.Next(num2), x_id, num2, -1, false);
							this.SetKey(num2, this.Key(this.Next(num2)));
						}
						else
						{
							int newNode = this.GetNewNode(this.Key(num2));
							this._inUseSatelliteTreeCount++;
							this.SetNext(newNode, num2);
							this.SetColor(newNode, this.color(num2));
							this.SetParent(newNode, this.Parent(num2));
							this.SetLeft(newNode, this.Left(num2));
							this.SetRight(newNode, this.Right(num2));
							if (this.Left(this.Parent(num2)) == num2)
							{
								this.SetLeft(this.Parent(num2), newNode);
							}
							else if (this.Right(this.Parent(num2)) == num2)
							{
								this.SetRight(this.Parent(num2), newNode);
							}
							if (this.Left(num2) != 0)
							{
								this.SetParent(this.Left(num2), newNode);
							}
							if (this.Right(num2) != 0)
							{
								this.SetParent(this.Right(num2), newNode);
							}
							if (this.root == num2)
							{
								this.root = newNode;
							}
							this.SetColor(num2, RBTree<K>.NodeColor.black);
							this.SetParent(num2, 0);
							this.SetLeft(num2, 0);
							this.SetRight(num2, 0);
							int size = this.SubTreeSize(num2);
							this.SetSubTreeSize(num2, 1);
							root_id = this.RBInsert(num2, x_id, newNode, -1, false);
							this.SetSubTreeSize(newNode, size);
						}
						return root_id;
					}
				}
			}
			else
			{
				if (this._accessMethod != TreeAccessMethod.INDEX_ONLY && !append)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod1);
				}
				if (position == -1)
				{
					position = this.SubTreeSize(this.root);
				}
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num4 = position - this.SubTreeSize(this.Left(num));
					if (num4 <= 0)
					{
						num2 = this.Left(num2);
					}
					else
					{
						num2 = this.Right(num2);
						if (num2 != 0)
						{
							position = num4 - 1;
						}
					}
				}
			}
			this.SetParent(x_id, num);
			if (num == 0)
			{
				if (root_id == 0)
				{
					this.root = x_id;
				}
				else
				{
					this.SetNext(mainTreeNodeID, x_id);
					this.SetKey(mainTreeNodeID, this.Key(x_id));
					root_id = x_id;
				}
			}
			else
			{
				int num5;
				if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX)
				{
					num5 = ((root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num)));
				}
				else
				{
					if (this._accessMethod != TreeAccessMethod.INDEX_ONLY)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod2);
					}
					num5 = ((position <= 0) ? -1 : 1);
				}
				if (num5 < 0)
				{
					this.SetLeft(num, x_id);
				}
				else
				{
					this.SetRight(num, x_id);
				}
			}
			this.SetLeft(x_id, 0);
			this.SetRight(x_id, 0);
			this.SetColor(x_id, RBTree<K>.NodeColor.red);
			while (this.color(this.Parent(x_id)) == RBTree<K>.NodeColor.red)
			{
				if (this.Parent(x_id) == this.Left(this.Parent(this.Parent(x_id))))
				{
					num = this.Right(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Right(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.LeftRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.RightRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
				else
				{
					num = this.Left(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Left(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.RightRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
			}
			if (root_id == 0)
			{
				this.SetColor(this.root, RBTree<K>.NodeColor.black);
			}
			else
			{
				this.SetColor(root_id, RBTree<K>.NodeColor.black);
			}
			return root_id;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x001F6828 File Offset: 0x001F5C28
		public void UpdateNodeKey(K currentKey, K newKey)
		{
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(currentKey);
			if (this.Parent(nodeByKey.NodeID) == 0 && nodeByKey.NodeID != this.root)
			{
				this.SetKey(nodeByKey.MainTreeNodeID, newKey);
			}
			this.SetKey(nodeByKey.NodeID, newKey);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x001F6878 File Offset: 0x001F5C78
		public K DeleteByIndex(int i)
		{
			RBTree<K>.NodePath nodeByIndex = this.GetNodeByIndex(i);
			K result = this.Key(nodeByIndex.NodeID);
			this.RBDeleteX(0, nodeByIndex.NodeID, nodeByIndex.MainTreeNodeID);
			return result;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x001F68B8 File Offset: 0x001F5CB8
		public int RBDelete(int z_id)
		{
			return this.RBDeleteX(0, z_id, 0);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x001F68D8 File Offset: 0x001F5CD8
		private int RBDeleteX(int root_id, int z_id, int mainTreeNodeID)
		{
			if (this.Next(z_id) != 0)
			{
				return this.RBDeleteX(this.Next(z_id), this.Next(z_id), z_id);
			}
			bool flag = false;
			int num = (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX) ? mainTreeNodeID : z_id;
			if (this.Next(num) != 0)
			{
				root_id = this.Next(num);
			}
			if (this.SubTreeSize(this.Next(num)) == 2)
			{
				flag = true;
			}
			else if (this.SubTreeSize(this.Next(num)) == 1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNextSizeInDelete);
			}
			int num2;
			if (this.Left(z_id) == 0 || this.Right(z_id) == 0)
			{
				num2 = z_id;
			}
			else
			{
				num2 = this.Successor(z_id);
			}
			int num3;
			if (this.Left(num2) != 0)
			{
				num3 = this.Left(num2);
			}
			else
			{
				num3 = this.Right(num2);
			}
			int num4 = this.Parent(num2);
			if (num3 != 0)
			{
				this.SetParent(num3, num4);
			}
			if (num4 == 0)
			{
				if (root_id == 0)
				{
					this.root = num3;
				}
				else
				{
					root_id = num3;
				}
			}
			else if (num2 == this.Left(num4))
			{
				this.SetLeft(num4, num3);
			}
			else
			{
				this.SetRight(num4, num3);
			}
			if (num2 != z_id)
			{
				this.SetKey(z_id, this.Key(num2));
				this.SetNext(z_id, this.Next(num2));
			}
			if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			for (int nodeId = num4; nodeId != 0; nodeId = this.Parent(nodeId))
			{
				this.RecomputeSize(nodeId);
			}
			if (root_id != 0)
			{
				for (int nodeId2 = num; nodeId2 != 0; nodeId2 = this.Parent(nodeId2))
				{
					this.DecreaseSize(nodeId2);
				}
			}
			if (this.color(num2) == RBTree<K>.NodeColor.black)
			{
				root_id = this.RBDeleteFixup(root_id, num3, num4, mainTreeNodeID);
			}
			if (flag)
			{
				if (num == 0 || this.SubTreeSize(this.Next(num)) != 1)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNodeSizeinDelete);
				}
				this._inUseSatelliteTreeCount--;
				int num5 = this.Next(num);
				this.SetLeft(num5, this.Left(num));
				this.SetRight(num5, this.Right(num));
				this.SetSubTreeSize(num5, this.SubTreeSize(num));
				this.SetColor(num5, this.color(num));
				if (this.Parent(num) != 0)
				{
					this.SetParent(num5, this.Parent(num));
					if (this.Left(this.Parent(num)) == num)
					{
						this.SetLeft(this.Parent(num), num5);
					}
					else
					{
						this.SetRight(this.Parent(num), num5);
					}
				}
				if (this.Left(num) != 0)
				{
					this.SetParent(this.Left(num), num5);
				}
				if (this.Right(num) != 0)
				{
					this.SetParent(this.Right(num), num5);
				}
				if (this.root == num)
				{
					this.root = num5;
				}
				this.FreeNode(num);
				num = 0;
			}
			else if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinEndDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			if (num2 != z_id)
			{
				this.SetLeft(num2, this.Left(z_id));
				this.SetRight(num2, this.Right(z_id));
				this.SetColor(num2, this.color(z_id));
				this.SetSubTreeSize(num2, this.SubTreeSize(z_id));
				if (this.Parent(z_id) != 0)
				{
					this.SetParent(num2, this.Parent(z_id));
					if (this.Left(this.Parent(z_id)) == z_id)
					{
						this.SetLeft(this.Parent(z_id), num2);
					}
					else
					{
						this.SetRight(this.Parent(z_id), num2);
					}
				}
				else
				{
					this.SetParent(num2, 0);
				}
				if (this.Left(z_id) != 0)
				{
					this.SetParent(this.Left(z_id), num2);
				}
				if (this.Right(z_id) != 0)
				{
					this.SetParent(this.Right(z_id), num2);
				}
				if (this.root == z_id)
				{
					this.root = num2;
				}
				else if (root_id == z_id)
				{
					root_id = num2;
				}
				if (num != 0 && this.Next(num) == z_id)
				{
					this.SetNext(num, num2);
				}
			}
			this.FreeNode(z_id);
			this._version++;
			return z_id;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x001F6CA8 File Offset: 0x001F60A8
		private int RBDeleteFixup(int root_id, int x_id, int px_id, int mainTreeNodeID)
		{
			if (x_id == 0 && px_id == 0)
			{
				return 0;
			}
			while (((root_id == 0) ? this.root : root_id) != x_id && this.color(x_id) == RBTree<K>.NodeColor.black)
			{
				if ((x_id != 0 && x_id == this.Left(this.Parent(x_id))) || (x_id == 0 && this.Left(px_id) == 0))
				{
					int num = (x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id));
					if (num == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.RBDeleteFixup);
					}
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(px_id, RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
					}
					if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black && this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
						}
						this.SetColor(num, this.color(px_id));
						this.SetColor(px_id, RBTree<K>.NodeColor.black);
						this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						x_id = ((root_id == 0) ? this.root : root_id);
						px_id = this.Parent(x_id);
					}
				}
				else
				{
					int num = this.Left(px_id);
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						if (x_id != 0)
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						else
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
							if (num == 0)
							{
								throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CannotRotateInvalidsuccessorNodeinDelete);
							}
						}
					}
					if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black && this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.LeftRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						if (x_id != 0)
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
						else
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
					}
				}
			}
			this.SetColor(x_id, RBTree<K>.NodeColor.black);
			return root_id;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x001F6FE8 File Offset: 0x001F63E8
		private int SearchSubTree(int root_id, K key)
		{
			if (root_id != 0 && this._accessMethod != TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethodInNonNillRootSubtree);
			}
			int num = (root_id == 0) ? this.root : root_id;
			while (num != 0)
			{
				int num2 = (root_id == 0) ? this.CompareNode(key, this.Key(num)) : this.CompareSateliteTreeNode(key, this.Key(num));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x001F7058 File Offset: 0x001F6458
		public int Search(K key)
		{
			int num = this.root;
			while (num != 0)
			{
				int num2 = this.CompareNode(key, this.Key(num));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x170000FA RID: 250
		public K this[int index]
		{
			get
			{
				return this.Key(this.GetNodeByIndex(index).NodeID);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x001F70C8 File Offset: 0x001F64C8
		private RBTree<K>.NodePath GetNodeByKey(K key)
		{
			int num = this.SearchSubTree(0, key);
			if (this.Next(num) != 0)
			{
				return new RBTree<K>.NodePath(this.SearchSubTree(this.Next(num), key), num);
			}
			K k = this.Key(num);
			if (!k.Equals(key))
			{
				num = 0;
			}
			return new RBTree<K>.NodePath(num, 0);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x001F7128 File Offset: 0x001F6528
		public int GetIndexByKey(K key)
		{
			int result = -1;
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(key);
			if (nodeByKey.NodeID != 0)
			{
				result = this.GetIndexByNodePath(nodeByKey);
			}
			return result;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x001F7158 File Offset: 0x001F6558
		public int GetIndexByNode(int node)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(node);
			}
			if (this.Next(node) != 0)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			int num = this.SearchSubTree(0, this.Key(node));
			if (num == node)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			return this.ComputeIndexWithSatelliteByNode(num) + this.ComputeIndexByNode(node);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x001F71B8 File Offset: 0x001F65B8
		private int GetIndexByNodePath(RBTree<K>.NodePath path)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(path.NodeID);
			}
			if (path.MainTreeNodeID == 0)
			{
				return this.ComputeIndexWithSatelliteByNode(path.NodeID);
			}
			return this.ComputeIndexWithSatelliteByNode(path.MainTreeNodeID) + this.ComputeIndexByNode(path.NodeID);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x001F7218 File Offset: 0x001F6618
		private int ComputeIndexByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + 1;
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x001F7268 File Offset: 0x001F6668
		private int ComputeIndexWithSatelliteByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + ((this.Next(num2) == 0) ? 1 : this.SubTreeSize(this.Next(num2)));
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x001F72C8 File Offset: 0x001F66C8
		private RBTree<K>.NodePath GetNodeByIndex(int userIndex)
		{
			int num;
			int mainTreeNodeID;
			if (this._inUseSatelliteTreeCount == 0)
			{
				num = this.ComputeNodeByIndex(this.root, userIndex + 1);
				mainTreeNodeID = 0;
			}
			else
			{
				num = this.ComputeNodeByIndex(userIndex, out mainTreeNodeID);
			}
			if (num != 0)
			{
				return new RBTree<K>.NodePath(num, mainTreeNodeID);
			}
			if (TreeAccessMethod.INDEX_ONLY == this._accessMethod)
			{
				throw ExceptionBuilder.RowOutOfRange(userIndex);
			}
			throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x001F7328 File Offset: 0x001F6728
		private int ComputeNodeByIndex(int index, out int satelliteRootId)
		{
			index++;
			satelliteRootId = 0;
			int num = this.root;
			int num2;
			while (num != 0 && ((num2 = this.SubTreeSize(this.Left(num)) + 1) != index || this.Next(num) != 0))
			{
				if (index < num2)
				{
					num = this.Left(num);
				}
				else
				{
					if (this.Next(num) != 0 && index >= num2 && index <= num2 + this.SubTreeSize(this.Next(num)) - 1)
					{
						satelliteRootId = num;
						index = index - num2 + 1;
						return this.ComputeNodeByIndex(this.Next(num), index);
					}
					if (this.Next(num) == 0)
					{
						index -= num2;
					}
					else
					{
						index -= num2 + this.SubTreeSize(this.Next(num)) - 1;
					}
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x001F73E8 File Offset: 0x001F67E8
		private int ComputeNodeByIndex(int x_id, int index)
		{
			while (x_id != 0)
			{
				int num = this.Left(x_id);
				int num2 = this.SubTreeSize(num) + 1;
				if (index < num2)
				{
					x_id = num;
				}
				else
				{
					if (num2 >= index)
					{
						break;
					}
					x_id = this.Right(x_id);
					index -= num2;
				}
			}
			return x_id;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x001F7428 File Offset: 0x001F6828
		public int Insert(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x001F7458 File Offset: 0x001F6858
		public int Add(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x001F7488 File Offset: 0x001F6888
		public IEnumerator GetEnumerator()
		{
			return new RBTree<K>.RBTreeEnumerator(this);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x001F74A8 File Offset: 0x001F68A8
		public int IndexOf(int nodeId, K item)
		{
			int result = -1;
			if (nodeId != 0)
			{
				if (this.Key(nodeId) == item)
				{
					return this.GetIndexByNode(nodeId);
				}
				if ((result = this.IndexOf(this.Left(nodeId), item)) != -1)
				{
					return result;
				}
				if ((result = this.IndexOf(this.Right(nodeId), item)) != -1)
				{
					return result;
				}
			}
			return result;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x001F7508 File Offset: 0x001F6908
		public int Insert(int position, K item)
		{
			return this.InsertAt(position, item, false);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x001F7528 File Offset: 0x001F6928
		public int InsertAt(int position, K item, bool append)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, position, append);
			return newNode;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x001F7558 File Offset: 0x001F6958
		public void RemoveAt(int position)
		{
			this.DeleteByIndex(position);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x001F7578 File Offset: 0x001F6978
		public void Clear()
		{
			this.InitTree();
			this._version++;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x001F75A8 File Offset: 0x001F69A8
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array.SetValue(this.Key(num), index + i);
				num = this.Successor(num);
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x001F7628 File Offset: 0x001F6A28
		public void CopyTo(K[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array[index + i] = this.Key(num);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x001F76A8 File Offset: 0x001F6AA8
		private void SetRight(int nodeId, int rightNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].rightId = rightNodeId;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x001F76D8 File Offset: 0x001F6AD8
		private void SetLeft(int nodeId, int leftNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].leftId = leftNodeId;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x001F7708 File Offset: 0x001F6B08
		private void SetParent(int nodeId, int parentNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].parentId = parentNodeId;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x001F7738 File Offset: 0x001F6B38
		private void SetColor(int nodeId, RBTree<K>.NodeColor color)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nodeColor = color;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x001F7768 File Offset: 0x001F6B68
		private void SetKey(int nodeId, K key)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].keyOfNode = key;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x001F7798 File Offset: 0x001F6B98
		private void SetNext(int nodeId, int nextNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nextId = nextNodeId;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x001F77C8 File Offset: 0x001F6BC8
		private void SetSubTreeSize(int nodeId, int size)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].subTreeSize = size;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x001F77F8 File Offset: 0x001F6BF8
		private void IncreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16].Slots;
			int num = nodeId & 65535;
			slots[num].subTreeSize = slots[num].subTreeSize + 1;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x001F7838 File Offset: 0x001F6C38
		private void RecomputeSize(int nodeId)
		{
			int subTreeSize = this.SubTreeSize(this.Left(nodeId)) + this.SubTreeSize(this.Right(nodeId)) + ((this.Next(nodeId) == 0) ? 1 : this.SubTreeSize(this.Next(nodeId)));
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].subTreeSize = subTreeSize;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x001F78A8 File Offset: 0x001F6CA8
		private void DecreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16].Slots;
			int num = nodeId & 65535;
			slots[num].subTreeSize = slots[num].subTreeSize - 1;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x001F78E8 File Offset: 0x001F6CE8
		[Conditional("DEBUG")]
		private void VerifySize(int nodeId, int size)
		{
			this.SubTreeSize(this.Left(nodeId));
			this.SubTreeSize(this.Right(nodeId));
			if (this.Next(nodeId) != 0)
			{
				this.SubTreeSize(this.Next(nodeId));
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x001F7928 File Offset: 0x001F6D28
		public int Right(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].rightId;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x001F7958 File Offset: 0x001F6D58
		public int Left(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].leftId;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x001F7988 File Offset: 0x001F6D88
		public int Parent(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].parentId;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x001F79B8 File Offset: 0x001F6DB8
		private RBTree<K>.NodeColor color(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nodeColor;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x001F79E8 File Offset: 0x001F6DE8
		public int Next(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nextId;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x001F7A18 File Offset: 0x001F6E18
		public int SubTreeSize(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].subTreeSize;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x001F7A48 File Offset: 0x001F6E48
		public K Key(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].keyOfNode;
		}

		// Token: 0x04000765 RID: 1893
		internal const int DefaultPageSize = 32;

		// Token: 0x04000766 RID: 1894
		internal const int NIL = 0;

		// Token: 0x04000767 RID: 1895
		private RBTree<K>.TreePage[] _pageTable;

		// Token: 0x04000768 RID: 1896
		private int[] _pageTableMap;

		// Token: 0x04000769 RID: 1897
		private int _inUsePageCount;

		// Token: 0x0400076A RID: 1898
		private int nextFreePageLine;

		// Token: 0x0400076B RID: 1899
		public int root;

		// Token: 0x0400076C RID: 1900
		private int _version;

		// Token: 0x0400076D RID: 1901
		private int _inUseNodeCount;

		// Token: 0x0400076E RID: 1902
		private int _inUseSatelliteTreeCount;

		// Token: 0x0400076F RID: 1903
		private readonly TreeAccessMethod _accessMethod;

		// Token: 0x02000088 RID: 136
		private enum NodeColor
		{
			// Token: 0x04000771 RID: 1905
			red,
			// Token: 0x04000772 RID: 1906
			black
		}

		// Token: 0x02000089 RID: 137
		private struct Node
		{
			// Token: 0x04000773 RID: 1907
			internal int selfId;

			// Token: 0x04000774 RID: 1908
			internal int leftId;

			// Token: 0x04000775 RID: 1909
			internal int rightId;

			// Token: 0x04000776 RID: 1910
			internal int parentId;

			// Token: 0x04000777 RID: 1911
			internal int nextId;

			// Token: 0x04000778 RID: 1912
			internal int subTreeSize;

			// Token: 0x04000779 RID: 1913
			internal K keyOfNode;

			// Token: 0x0400077A RID: 1914
			internal RBTree<K>.NodeColor nodeColor;
		}

		// Token: 0x0200008A RID: 138
		private struct NodePath
		{
			// Token: 0x06000822 RID: 2082 RVA: 0x001F7A78 File Offset: 0x001F6E78
			internal NodePath(int nodeID, int mainTreeNodeID)
			{
				this.NodeID = nodeID;
				this.MainTreeNodeID = mainTreeNodeID;
			}

			// Token: 0x0400077B RID: 1915
			internal readonly int NodeID;

			// Token: 0x0400077C RID: 1916
			internal readonly int MainTreeNodeID;
		}

		// Token: 0x0200008B RID: 139
		private sealed class TreePage
		{
			// Token: 0x06000823 RID: 2083 RVA: 0x001F7A98 File Offset: 0x001F6E98
			internal TreePage(int size)
			{
				if (size > 65536)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidPageSize);
				}
				this.Slots = new RBTree<K>.Node[size];
				this.SlotMap = new int[(size + 32 - 1) / 32];
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x001F7AE8 File Offset: 0x001F6EE8
			internal int AllocSlot(RBTree<K> tree)
			{
				int num = -1;
				if (this._inUseCount < this.Slots.Length)
				{
					for (int i = this._nextFreeSlotLine; i < this.SlotMap.Length; i++)
					{
						if (this.SlotMap[i] < -1)
						{
							int num2 = ~this.SlotMap[i] & this.SlotMap[i] + 1;
							this.SlotMap[i] |= num2;
							this._inUseCount++;
							if (this._inUseCount == this.Slots.Length)
							{
								tree.MarkPageFull(this);
							}
							tree._inUseNodeCount++;
							num = RBTree<K>.GetIntValueFromBitMap((uint)num2);
							this._nextFreeSlotLine = i;
							num = i * 32 + num;
							break;
						}
					}
					if (num == -1 && this._nextFreeSlotLine != 0)
					{
						this._nextFreeSlotLine = 0;
						num = this.AllocSlot(tree);
					}
				}
				return num;
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000825 RID: 2085 RVA: 0x001F7BD8 File Offset: 0x001F6FD8
			// (set) Token: 0x06000826 RID: 2086 RVA: 0x001F7BF8 File Offset: 0x001F6FF8
			internal int InUseCount
			{
				get
				{
					return this._inUseCount;
				}
				set
				{
					this._inUseCount = value;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000827 RID: 2087 RVA: 0x001F7C18 File Offset: 0x001F7018
			// (set) Token: 0x06000828 RID: 2088 RVA: 0x001F7C38 File Offset: 0x001F7038
			internal int PageId
			{
				get
				{
					return this._pageId;
				}
				set
				{
					this._pageId = value;
				}
			}

			// Token: 0x0400077D RID: 1917
			public const int slotLineSize = 32;

			// Token: 0x0400077E RID: 1918
			internal readonly RBTree<K>.Node[] Slots;

			// Token: 0x0400077F RID: 1919
			internal readonly int[] SlotMap;

			// Token: 0x04000780 RID: 1920
			private int _inUseCount;

			// Token: 0x04000781 RID: 1921
			private int _pageId;

			// Token: 0x04000782 RID: 1922
			private int _nextFreeSlotLine;
		}

		// Token: 0x0200008C RID: 140
		internal struct RBTreeEnumerator : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x06000829 RID: 2089 RVA: 0x001F7C58 File Offset: 0x001F7058
			internal RBTreeEnumerator(RBTree<K> tree)
			{
				this.tree = tree;
				this.version = tree._version;
				this.index = 0;
				this.mainTreeNodeId = tree.root;
				this.current = default(K);
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x001F7C98 File Offset: 0x001F7098
			internal RBTreeEnumerator(RBTree<K> tree, int position)
			{
				this.tree = tree;
				this.version = tree._version;
				if (position == 0)
				{
					this.index = 0;
					this.mainTreeNodeId = tree.root;
				}
				else
				{
					this.index = tree.ComputeNodeByIndex(position - 1, out this.mainTreeNodeId);
					if (this.index == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
					}
				}
				this.current = default(K);
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x001F7D08 File Offset: 0x001F7108
			public void Dispose()
			{
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x001F7D18 File Offset: 0x001F7118
			public bool MoveNext()
			{
				if (this.version != this.tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				bool result = this.tree.Successor(ref this.index, ref this.mainTreeNodeId);
				this.current = this.tree.Key(this.index);
				return result;
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x001F7D78 File Offset: 0x001F7178
			public K Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600082E RID: 2094 RVA: 0x001F7D98 File Offset: 0x001F7198
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x001F7DB8 File Offset: 0x001F71B8
			void IEnumerator.Reset()
			{
				if (this.version != this.tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				this.index = 0;
				this.mainTreeNodeId = this.tree.root;
				this.current = default(K);
			}

			// Token: 0x04000783 RID: 1923
			private readonly RBTree<K> tree;

			// Token: 0x04000784 RID: 1924
			private readonly int version;

			// Token: 0x04000785 RID: 1925
			private int index;

			// Token: 0x04000786 RID: 1926
			private int mainTreeNodeId;

			// Token: 0x04000787 RID: 1927
			private K current;
		}
	}
}
