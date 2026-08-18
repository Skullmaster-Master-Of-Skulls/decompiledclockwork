using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x0200011C RID: 284
	internal abstract class RBTree<K> : IEnumerable
	{
		// Token: 0x060010FE RID: 4350
		protected abstract int CompareNode(K record1, K record2);

		// Token: 0x060010FF RID: 4351
		protected abstract int CompareSateliteTreeNode(K record1, K record2);

		// Token: 0x06001100 RID: 4352 RVA: 0x00083428 File Offset: 0x00082828
		protected RBTree(TreeAccessMethod accessMethod)
		{
			this._accessMethod = accessMethod;
			this.InitTree();
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00083448 File Offset: 0x00082848
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

		// Token: 0x06001102 RID: 4354 RVA: 0x000834E0 File Offset: 0x000828E0
		private void FreePage(RBTree<K>.TreePage page)
		{
			this.MarkPageFree(page);
			this._pageTable[page.PageId] = null;
			this._inUsePageCount--;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00083510 File Offset: 0x00082910
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

		// Token: 0x06001104 RID: 4356 RVA: 0x000835EC File Offset: 0x000829EC
		private void MarkPageFull(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] |= 1 << page.PageId % 32;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00083620 File Offset: 0x00082A20
		private void MarkPageFree(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] &= ~(1 << page.PageId % 32);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00083654 File Offset: 0x00082A54
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

		// Token: 0x06001107 RID: 4359 RVA: 0x000836B4 File Offset: 0x00082AB4
		private void FreeNode(int nodeId)
		{
			RBTree<K>.TreePage treePage = this._pageTable[nodeId >> 16];
			int num = nodeId & 65535;
			treePage.Slots[num] = default(RBTree<K>.Node);
			treePage.SlotMap[num / 32] &= ~(1 << num % 32);
			RBTree<K>.TreePage treePage2 = treePage;
			int inUseCount = treePage2.InUseCount;
			treePage2.InUseCount = inUseCount - 1;
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

		// Token: 0x06001108 RID: 4360 RVA: 0x0008374C File Offset: 0x00082B4C
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x000837F0 File Offset: 0x00082BF0
		public int Count
		{
			get
			{
				return this._inUseNodeCount - 1;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00083808 File Offset: 0x00082C08
		public bool HasDuplicates
		{
			get
			{
				return this._inUseSatelliteTreeCount != 0;
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00083820 File Offset: 0x00082C20
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

		// Token: 0x0600110C RID: 4364 RVA: 0x0008393C File Offset: 0x00082D3C
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

		// Token: 0x0600110D RID: 4365 RVA: 0x00083984 File Offset: 0x00082D84
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

		// Token: 0x0600110E RID: 4366 RVA: 0x000839F4 File Offset: 0x00082DF4
		private int Minimum(int x_id)
		{
			while (this.Left(x_id) != 0)
			{
				x_id = this.Left(x_id);
			}
			return x_id;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00083A18 File Offset: 0x00082E18
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

		// Token: 0x06001110 RID: 4368 RVA: 0x00083B50 File Offset: 0x00082F50
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

		// Token: 0x06001111 RID: 4369 RVA: 0x00083C88 File Offset: 0x00083088
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

		// Token: 0x06001112 RID: 4370 RVA: 0x0008417C File Offset: 0x0008357C
		public void UpdateNodeKey(K currentKey, K newKey)
		{
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(currentKey);
			if (this.Parent(nodeByKey.NodeID) == 0 && nodeByKey.NodeID != this.root)
			{
				this.SetKey(nodeByKey.MainTreeNodeID, newKey);
			}
			this.SetKey(nodeByKey.NodeID, newKey);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000841C8 File Offset: 0x000835C8
		public K DeleteByIndex(int i)
		{
			RBTree<K>.NodePath nodeByIndex = this.GetNodeByIndex(i);
			K result = this.Key(nodeByIndex.NodeID);
			this.RBDeleteX(0, nodeByIndex.NodeID, nodeByIndex.MainTreeNodeID);
			return result;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00084200 File Offset: 0x00083600
		public int RBDelete(int z_id)
		{
			return this.RBDeleteX(0, z_id, 0);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00084218 File Offset: 0x00083618
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

		// Token: 0x06001116 RID: 4374 RVA: 0x000845E8 File Offset: 0x000839E8
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

		// Token: 0x06001117 RID: 4375 RVA: 0x00084924 File Offset: 0x00083D24
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

		// Token: 0x06001118 RID: 4376 RVA: 0x00084994 File Offset: 0x00083D94
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

		// Token: 0x17000294 RID: 660
		public K this[int index]
		{
			get
			{
				return this.Key(this.GetNodeByIndex(index).NodeID);
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000849F8 File Offset: 0x00083DF8
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

		// Token: 0x0600111B RID: 4379 RVA: 0x00084A54 File Offset: 0x00083E54
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

		// Token: 0x0600111C RID: 4380 RVA: 0x00084A7C File Offset: 0x00083E7C
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

		// Token: 0x0600111D RID: 4381 RVA: 0x00084AD4 File Offset: 0x00083ED4
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

		// Token: 0x0600111E RID: 4382 RVA: 0x00084B24 File Offset: 0x00083F24
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

		// Token: 0x0600111F RID: 4383 RVA: 0x00084B6C File Offset: 0x00083F6C
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

		// Token: 0x06001120 RID: 4384 RVA: 0x00084BCC File Offset: 0x00083FCC
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

		// Token: 0x06001121 RID: 4385 RVA: 0x00084C24 File Offset: 0x00084024
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

		// Token: 0x06001122 RID: 4386 RVA: 0x00084CE4 File Offset: 0x000840E4
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

		// Token: 0x06001123 RID: 4387 RVA: 0x00084D24 File Offset: 0x00084124
		public int Insert(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00084D48 File Offset: 0x00084148
		public int Add(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00084D6C File Offset: 0x0008416C
		public IEnumerator GetEnumerator()
		{
			return new RBTree<K>.RBTreeEnumerator(this);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00084D84 File Offset: 0x00084184
		public int IndexOf(int nodeId, K item)
		{
			int result = -1;
			if (nodeId == 0)
			{
				return result;
			}
			if (this.Key(nodeId) == item)
			{
				return this.GetIndexByNode(nodeId);
			}
			if ((result = this.IndexOf(this.Left(nodeId), item)) != -1)
			{
				return result;
			}
			return this.IndexOf(this.Right(nodeId), item);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00084DE0 File Offset: 0x000841E0
		public int Insert(int position, K item)
		{
			return this.InsertAt(position, item, false);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00084DF8 File Offset: 0x000841F8
		public int InsertAt(int position, K item, bool append)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, position, append);
			return newNode;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00084E1C File Offset: 0x0008421C
		public void RemoveAt(int position)
		{
			this.DeleteByIndex(position);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00084E34 File Offset: 0x00084234
		public void Clear()
		{
			this.InitTree();
			this._version++;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00084E58 File Offset: 0x00084258
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

		// Token: 0x0600112C RID: 4396 RVA: 0x00084ED8 File Offset: 0x000842D8
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

		// Token: 0x0600112D RID: 4397 RVA: 0x00084F50 File Offset: 0x00084350
		private void SetRight(int nodeId, int rightNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].rightId = rightNodeId;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00084F80 File Offset: 0x00084380
		private void SetLeft(int nodeId, int leftNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].leftId = leftNodeId;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00084FB0 File Offset: 0x000843B0
		private void SetParent(int nodeId, int parentNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].parentId = parentNodeId;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00084FE0 File Offset: 0x000843E0
		private void SetColor(int nodeId, RBTree<K>.NodeColor color)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nodeColor = color;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00085010 File Offset: 0x00084410
		private void SetKey(int nodeId, K key)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].keyOfNode = key;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00085040 File Offset: 0x00084440
		private void SetNext(int nodeId, int nextNodeId)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nextId = nextNodeId;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00085070 File Offset: 0x00084470
		private void SetSubTreeSize(int nodeId, int size)
		{
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].subTreeSize = size;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000850A0 File Offset: 0x000844A0
		private void IncreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16].Slots;
			int num = nodeId & 65535;
			slots[num].subTreeSize = slots[num].subTreeSize + 1;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000850D4 File Offset: 0x000844D4
		private void RecomputeSize(int nodeId)
		{
			int subTreeSize = this.SubTreeSize(this.Left(nodeId)) + this.SubTreeSize(this.Right(nodeId)) + ((this.Next(nodeId) == 0) ? 1 : this.SubTreeSize(this.Next(nodeId)));
			this._pageTable[nodeId >> 16].Slots[nodeId & 65535].subTreeSize = subTreeSize;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0008513C File Offset: 0x0008453C
		private void DecreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16].Slots;
			int num = nodeId & 65535;
			slots[num].subTreeSize = slots[num].subTreeSize - 1;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00085170 File Offset: 0x00084570
		[Conditional("DEBUG")]
		private void VerifySize(int nodeId, int size)
		{
			int num = this.SubTreeSize(this.Left(nodeId)) + this.SubTreeSize(this.Right(nodeId)) + ((this.Next(nodeId) == 0) ? 1 : this.SubTreeSize(this.Next(nodeId)));
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000851B4 File Offset: 0x000845B4
		public int Right(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].rightId;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000851E4 File Offset: 0x000845E4
		public int Left(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].leftId;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00085214 File Offset: 0x00084614
		public int Parent(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].parentId;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00085244 File Offset: 0x00084644
		private RBTree<K>.NodeColor color(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nodeColor;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00085274 File Offset: 0x00084674
		public int Next(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].nextId;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000852A4 File Offset: 0x000846A4
		public int SubTreeSize(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].subTreeSize;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000852D4 File Offset: 0x000846D4
		public K Key(int nodeId)
		{
			return this._pageTable[nodeId >> 16].Slots[nodeId & 65535].keyOfNode;
		}

		// Token: 0x040005BA RID: 1466
		internal const int DefaultPageSize = 32;

		// Token: 0x040005BB RID: 1467
		internal const int NIL = 0;

		// Token: 0x040005BC RID: 1468
		private RBTree<K>.TreePage[] _pageTable;

		// Token: 0x040005BD RID: 1469
		private int[] _pageTableMap;

		// Token: 0x040005BE RID: 1470
		private int _inUsePageCount;

		// Token: 0x040005BF RID: 1471
		private int nextFreePageLine;

		// Token: 0x040005C0 RID: 1472
		public int root;

		// Token: 0x040005C1 RID: 1473
		private int _version;

		// Token: 0x040005C2 RID: 1474
		private int _inUseNodeCount;

		// Token: 0x040005C3 RID: 1475
		private int _inUseSatelliteTreeCount;

		// Token: 0x040005C4 RID: 1476
		private readonly TreeAccessMethod _accessMethod;

		// Token: 0x02000352 RID: 850
		private enum NodeColor
		{
			// Token: 0x04001EE6 RID: 7910
			red,
			// Token: 0x04001EE7 RID: 7911
			black
		}

		// Token: 0x02000353 RID: 851
		private struct Node
		{
			// Token: 0x04001EE8 RID: 7912
			internal int selfId;

			// Token: 0x04001EE9 RID: 7913
			internal int leftId;

			// Token: 0x04001EEA RID: 7914
			internal int rightId;

			// Token: 0x04001EEB RID: 7915
			internal int parentId;

			// Token: 0x04001EEC RID: 7916
			internal int nextId;

			// Token: 0x04001EED RID: 7917
			internal int subTreeSize;

			// Token: 0x04001EEE RID: 7918
			internal K keyOfNode;

			// Token: 0x04001EEF RID: 7919
			internal RBTree<K>.NodeColor nodeColor;
		}

		// Token: 0x02000354 RID: 852
		private struct NodePath
		{
			// Token: 0x0600340F RID: 13327 RVA: 0x0014001C File Offset: 0x0013F41C
			internal NodePath(int nodeID, int mainTreeNodeID)
			{
				this.NodeID = nodeID;
				this.MainTreeNodeID = mainTreeNodeID;
			}

			// Token: 0x04001EF0 RID: 7920
			internal readonly int NodeID;

			// Token: 0x04001EF1 RID: 7921
			internal readonly int MainTreeNodeID;
		}

		// Token: 0x02000355 RID: 853
		private sealed class TreePage
		{
			// Token: 0x06003410 RID: 13328 RVA: 0x00140038 File Offset: 0x0013F438
			internal TreePage(int size)
			{
				if (size > 65536)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidPageSize);
				}
				this.Slots = new RBTree<K>.Node[size];
				this.SlotMap = new int[(size + 32 - 1) / 32];
			}

			// Token: 0x06003411 RID: 13329 RVA: 0x0014007C File Offset: 0x0013F47C
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

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06003412 RID: 13330 RVA: 0x0014015C File Offset: 0x0013F55C
			// (set) Token: 0x06003413 RID: 13331 RVA: 0x00140170 File Offset: 0x0013F570
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

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06003414 RID: 13332 RVA: 0x00140184 File Offset: 0x0013F584
			// (set) Token: 0x06003415 RID: 13333 RVA: 0x00140198 File Offset: 0x0013F598
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

			// Token: 0x04001EF2 RID: 7922
			public const int slotLineSize = 32;

			// Token: 0x04001EF3 RID: 7923
			internal readonly RBTree<K>.Node[] Slots;

			// Token: 0x04001EF4 RID: 7924
			internal readonly int[] SlotMap;

			// Token: 0x04001EF5 RID: 7925
			private int _inUseCount;

			// Token: 0x04001EF6 RID: 7926
			private int _pageId;

			// Token: 0x04001EF7 RID: 7927
			private int _nextFreeSlotLine;
		}

		// Token: 0x02000356 RID: 854
		internal struct RBTreeEnumerator : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x06003416 RID: 13334 RVA: 0x001401AC File Offset: 0x0013F5AC
			internal RBTreeEnumerator(RBTree<K> tree)
			{
				this.tree = tree;
				this.version = tree._version;
				this.index = 0;
				this.mainTreeNodeId = tree.root;
				this.current = default(K);
			}

			// Token: 0x06003417 RID: 13335 RVA: 0x001401EC File Offset: 0x0013F5EC
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

			// Token: 0x06003418 RID: 13336 RVA: 0x00140258 File Offset: 0x0013F658
			public void Dispose()
			{
			}

			// Token: 0x06003419 RID: 13337 RVA: 0x00140268 File Offset: 0x0013F668
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

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x0600341A RID: 13338 RVA: 0x001402C0 File Offset: 0x0013F6C0
			public K Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x0600341B RID: 13339 RVA: 0x001402D4 File Offset: 0x0013F6D4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600341C RID: 13340 RVA: 0x001402EC File Offset: 0x0013F6EC
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

			// Token: 0x04001EF8 RID: 7928
			private readonly RBTree<K> tree;

			// Token: 0x04001EF9 RID: 7929
			private readonly int version;

			// Token: 0x04001EFA RID: 7930
			private int index;

			// Token: 0x04001EFB RID: 7931
			private int mainTreeNodeId;

			// Token: 0x04001EFC RID: 7932
			private K current;
		}
	}
}
