using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020003CB RID: 971
	[DebuggerTypeProxy(typeof(SortedSetDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, ISerializable, IDeserializationCallback, IReadOnlyCollection<T>
	{
		// Token: 0x060024E9 RID: 9449 RVA: 0x000ABBDF File Offset: 0x000A9DDF
		[__DynamicallyInvokable]
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000ABBF2 File Offset: 0x000A9DF2
		[__DynamicallyInvokable]
		public SortedSet(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				this.comparer = Comparer<T>.Default;
				return;
			}
			this.comparer = comparer;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000ABC10 File Offset: 0x000A9E10
		[__DynamicallyInvokable]
		public SortedSet(IEnumerable<T> collection) : this(collection, Comparer<T>.Default)
		{
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000ABC20 File Offset: 0x000A9E20
		[__DynamicallyInvokable]
		public SortedSet(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			SortedSet<T> sortedSet = collection as SortedSet<T>;
			SortedSet<T> sortedSet2 = collection as SortedSet<T>.TreeSubSet;
			if (sortedSet == null || sortedSet2 != null || !SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				List<T> list = new List<T>(collection);
				list.Sort(this.comparer);
				for (int i = 1; i < list.Count; i++)
				{
					if (comparer.Compare(list[i], list[i - 1]) == 0)
					{
						list.RemoveAt(i);
						i--;
					}
				}
				this.root = SortedSet<T>.ConstructRootFromSortedArray(list.ToArray(), 0, list.Count - 1, null);
				this.count = list.Count;
				this.version = 0;
				return;
			}
			if (sortedSet.Count == 0)
			{
				this.count = 0;
				this.version = 0;
				this.root = null;
				return;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(sortedSet.Count) + 2);
			Stack<SortedSet<T>.Node> stack2 = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(sortedSet.Count) + 2);
			SortedSet<T>.Node node = sortedSet.root;
			SortedSet<T>.Node node2 = (node != null) ? new SortedSet<T>.Node(node.Item, node.IsRed) : null;
			this.root = node2;
			while (node != null)
			{
				stack.Push(node);
				stack2.Push(node2);
				node2.Left = ((node.Left != null) ? new SortedSet<T>.Node(node.Left.Item, node.Left.IsRed) : null);
				node = node.Left;
				node2 = node2.Left;
			}
			while (stack.Count != 0)
			{
				node = stack.Pop();
				node2 = stack2.Pop();
				SortedSet<T>.Node node3 = node.Right;
				SortedSet<T>.Node node4 = null;
				if (node3 != null)
				{
					node4 = new SortedSet<T>.Node(node3.Item, node3.IsRed);
				}
				node2.Right = node4;
				while (node3 != null)
				{
					stack.Push(node3);
					stack2.Push(node4);
					node4.Left = ((node3.Left != null) ? new SortedSet<T>.Node(node3.Left.Item, node3.Left.IsRed) : null);
					node3 = node3.Left;
					node4 = node4.Left;
				}
			}
			this.count = sortedSet.count;
			this.version = 0;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000ABE75 File Offset: 0x000AA075
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000ABE84 File Offset: 0x000AA084
		private void AddAllElements(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (!this.Contains(item))
				{
					this.Add(item);
				}
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000ABED8 File Offset: 0x000AA0D8
		private void RemoveAllElements(IEnumerable<T> collection)
		{
			T min = this.Min;
			T max = this.Max;
			foreach (T t in collection)
			{
				if (this.comparer.Compare(t, min) >= 0 && this.comparer.Compare(t, max) <= 0 && this.Contains(t))
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000ABF58 File Offset: 0x000AA158
		private bool ContainsAllElements(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (!this.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000ABFAC File Offset: 0x000AA1AC
		internal bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			return this.InOrderTreeWalk(action, false);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000ABFB8 File Offset: 0x000AA1B8
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action, bool reverse)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = (reverse ? node.Right : node.Left))
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = reverse ? node.Left : node.Right; node2 != null; node2 = (reverse ? node2.Right : node2.Left))
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000AC058 File Offset: 0x000AA258
		internal virtual bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			List<SortedSet<T>.Node> list = new List<SortedSet<T>.Node>();
			list.Add(this.root);
			while (list.Count != 0)
			{
				SortedSet<T>.Node node = list[0];
				list.RemoveAt(0);
				if (!action(node))
				{
					return false;
				}
				if (node.Left != null)
				{
					list.Add(node.Left);
				}
				if (node.Right != null)
				{
					list.Add(node.Right);
				}
			}
			return true;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000AC0CE File Offset: 0x000AA2CE
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000AC0DC File Offset: 0x000AA2DC
		[__DynamicallyInvokable]
		public IComparer<T> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000AC0E4 File Offset: 0x000AA2E4
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000AC0E7 File Offset: 0x000AA2E7
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000AC0EA File Offset: 0x000AA2EA
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000AC10C File Offset: 0x000AA30C
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000AC10E File Offset: 0x000AA30E
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000AC111 File Offset: 0x000AA311
		[__DynamicallyInvokable]
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000AC11A File Offset: 0x000AA31A
		[__DynamicallyInvokable]
		void ICollection<!0>.Add(T item)
		{
			this.AddIfNotPresent(item);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000AC124 File Offset: 0x000AA324
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, false);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node greatGrandParent = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.IsRed = false;
					return false;
				}
				if (SortedSet<T>.Is4Node(node))
				{
					SortedSet<T>.Split4Node(node);
					if (SortedSet<T>.IsRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, greatGrandParent);
					}
				}
				greatGrandParent = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node4 = new SortedSet<T>.Node(item);
			if (num > 0)
			{
				node2.Right = node4;
			}
			else
			{
				node2.Left = node4;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node4, ref node2, node3, greatGrandParent);
			}
			this.root.IsRed = false;
			this.count++;
			return true;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000AC22F File Offset: 0x000AA42F
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000AC238 File Offset: 0x000AA438
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node parentOfMatch = null;
			bool flag = false;
			while (node != null)
			{
				if (SortedSet<T>.Is2Node(node))
				{
					if (node2 == null)
					{
						node.IsRed = true;
					}
					else
					{
						SortedSet<T>.Node node5 = SortedSet<T>.GetSibling(node, node2);
						if (node5.IsRed)
						{
							if (node2.Right == node5)
							{
								SortedSet<T>.RotateLeft(node2);
							}
							else
							{
								SortedSet<T>.RotateRight(node2);
							}
							node2.IsRed = true;
							node5.IsRed = false;
							this.ReplaceChildOfNodeOrRoot(node3, node2, node5);
							node3 = node5;
							if (node2 == node4)
							{
								parentOfMatch = node5;
							}
							node5 = ((node2.Left == node) ? node2.Right : node2.Left);
						}
						if (SortedSet<T>.Is2Node(node5))
						{
							SortedSet<T>.Merge2Nodes(node2, node, node5);
						}
						else
						{
							TreeRotation treeRotation = SortedSet<T>.RotationNeeded(node2, node, node5);
							SortedSet<T>.Node node6 = null;
							switch (treeRotation)
							{
							case TreeRotation.LeftRotation:
								node5.Right.IsRed = false;
								node6 = SortedSet<T>.RotateLeft(node2);
								break;
							case TreeRotation.RightRotation:
								node5.Left.IsRed = false;
								node6 = SortedSet<T>.RotateRight(node2);
								break;
							case TreeRotation.RightLeftRotation:
								node6 = SortedSet<T>.RotateRightLeft(node2);
								break;
							case TreeRotation.LeftRightRotation:
								node6 = SortedSet<T>.RotateLeftRight(node2);
								break;
							}
							node6.IsRed = node2.IsRed;
							node2.IsRed = false;
							node.IsRed = true;
							this.ReplaceChildOfNodeOrRoot(node3, node2, node6);
							if (node2 == node4)
							{
								parentOfMatch = node6;
							}
						}
					}
				}
				int num = flag ? -1 : this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					flag = true;
					node4 = node;
					parentOfMatch = node2;
				}
				node3 = node2;
				node2 = node;
				if (num < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, parentOfMatch, node2, node3);
				this.count--;
			}
			if (this.root != null)
			{
				this.root.IsRed = false;
			}
			return flag;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000AC420 File Offset: 0x000AA620
		[__DynamicallyInvokable]
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000AC43E File Offset: 0x000AA63E
		[__DynamicallyInvokable]
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000AC44A File Offset: 0x000AA64A
		[__DynamicallyInvokable]
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000AC45A File Offset: 0x000AA65A
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000AC46C File Offset: 0x000AA66C
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (index > array.Length || count > array.Length - index)
			{
				throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000AC530 File Offset: 0x000AA730
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects = objects;
					int index2 = index;
					index = index2 + 1;
					objects[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000AC604 File Offset: 0x000AA804
		[__DynamicallyInvokable]
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000AC60C File Offset: 0x000AA80C
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000AC619 File Offset: 0x000AA819
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000AC626 File Offset: 0x000AA826
		private static SortedSet<T>.Node GetSibling(SortedSet<T>.Node node, SortedSet<T>.Node parent)
		{
			if (parent.Left == node)
			{
				return parent.Right;
			}
			return parent.Left;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000AC640 File Offset: 0x000AA840
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? SortedSet<T>.RotateLeft(grandParent) : SortedSet<T>.RotateRight(grandParent));
			}
			else
			{
				node = (flag2 ? SortedSet<T>.RotateLeftRight(grandParent) : SortedSet<T>.RotateRightLeft(grandParent));
				parent = greatGrandParent;
			}
			grandParent.IsRed = true;
			node.IsRed = false;
			this.ReplaceChildOfNodeOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000AC6A9 File Offset: 0x000AA8A9
		private static bool Is2Node(SortedSet<T>.Node node)
		{
			return SortedSet<T>.IsBlack(node) && SortedSet<T>.IsNullOrBlack(node.Left) && SortedSet<T>.IsNullOrBlack(node.Right);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000AC6CD File Offset: 0x000AA8CD
		private static bool Is4Node(SortedSet<T>.Node node)
		{
			return SortedSet<T>.IsRed(node.Left) && SortedSet<T>.IsRed(node.Right);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000AC6E9 File Offset: 0x000AA8E9
		private static bool IsBlack(SortedSet<T>.Node node)
		{
			return node != null && !node.IsRed;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000AC6F9 File Offset: 0x000AA8F9
		private static bool IsNullOrBlack(SortedSet<T>.Node node)
		{
			return node == null || !node.IsRed;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000AC709 File Offset: 0x000AA909
		private static bool IsRed(SortedSet<T>.Node node)
		{
			return node != null && node.IsRed;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000AC716 File Offset: 0x000AA916
		private static void Merge2Nodes(SortedSet<T>.Node parent, SortedSet<T>.Node child1, SortedSet<T>.Node child2)
		{
			parent.IsRed = false;
			child1.IsRed = true;
			child2.IsRed = true;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000AC72D File Offset: 0x000AA92D
		private void ReplaceChildOfNodeOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent == null)
			{
				this.root = newChild;
				return;
			}
			if (parent.Left == child)
			{
				parent.Left = newChild;
				return;
			}
			parent.Right = newChild;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000AC754 File Offset: 0x000AA954
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node succesor, SortedSet<T>.Node parentOfSuccesor)
		{
			if (succesor == match)
			{
				succesor = match.Left;
			}
			else
			{
				if (succesor.Right != null)
				{
					succesor.Right.IsRed = false;
				}
				if (parentOfSuccesor != match)
				{
					parentOfSuccesor.Left = succesor.Right;
					succesor.Right = match.Right;
				}
				succesor.Left = match.Left;
			}
			if (succesor != null)
			{
				succesor.IsRed = match.IsRed;
			}
			this.ReplaceChildOfNodeOrRoot(parentOfMatch, match, succesor);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000AC7C8 File Offset: 0x000AA9C8
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000AC810 File Offset: 0x000AAA10
		internal virtual int InternalIndexOf(T item)
		{
			SortedSet<T>.Node node = this.root;
			int num = 0;
			while (node != null)
			{
				int num2 = this.comparer.Compare(item, node.Item);
				if (num2 == 0)
				{
					return num;
				}
				node = ((num2 < 0) ? node.Left : node.Right);
				num = ((num2 < 0) ? (2 * num + 1) : (2 * num + 2));
			}
			return -1;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000AC868 File Offset: 0x000AAA68
		internal SortedSet<T>.Node FindRange(T from, T to)
		{
			return this.FindRange(from, to, true, true);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000AC874 File Offset: 0x000AAA74
		internal SortedSet<T>.Node FindRange(T from, T to, bool lowerBoundActive, bool upperBoundActive)
		{
			SortedSet<T>.Node node = this.root;
			while (node != null)
			{
				if (lowerBoundActive && this.comparer.Compare(from, node.Item) > 0)
				{
					node = node.Right;
				}
				else
				{
					if (!upperBoundActive || this.comparer.Compare(to, node.Item) >= 0)
					{
						return node;
					}
					node = node.Left;
				}
			}
			return null;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000AC8D3 File Offset: 0x000AAAD3
		internal void UpdateVersion()
		{
			this.version++;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000AC8E4 File Offset: 0x000AAAE4
		private static SortedSet<T>.Node RotateLeft(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node right = node.Right;
			node.Right = right.Left;
			right.Left = node;
			return right;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000AC90C File Offset: 0x000AAB0C
		private static SortedSet<T>.Node RotateLeftRight(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node left = node.Left;
			SortedSet<T>.Node right = left.Right;
			node.Left = right.Right;
			right.Right = node;
			left.Right = right.Left;
			right.Left = left;
			return right;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000AC950 File Offset: 0x000AAB50
		private static SortedSet<T>.Node RotateRight(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node left = node.Left;
			node.Left = left.Right;
			left.Right = node;
			return left;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000AC978 File Offset: 0x000AAB78
		private static SortedSet<T>.Node RotateRightLeft(SortedSet<T>.Node node)
		{
			SortedSet<T>.Node right = node.Right;
			SortedSet<T>.Node left = right.Left;
			node.Right = left.Left;
			left.Left = node;
			right.Left = left.Right;
			left.Right = right;
			return left;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000AC9BA File Offset: 0x000AABBA
		private static TreeRotation RotationNeeded(SortedSet<T>.Node parent, SortedSet<T>.Node current, SortedSet<T>.Node sibling)
		{
			if (SortedSet<T>.IsRed(sibling.Left))
			{
				if (parent.Left == current)
				{
					return TreeRotation.RightLeftRotation;
				}
				return TreeRotation.RightRotation;
			}
			else
			{
				if (parent.Left == current)
				{
					return TreeRotation.LeftRotation;
				}
				return TreeRotation.LeftRightRotation;
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000AC9E2 File Offset: 0x000AABE2
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
		{
			return new SortedSetEqualityComparer<T>();
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000AC9E9 File Offset: 0x000AABE9
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
		{
			return new SortedSetEqualityComparer<T>(memberEqualityComparer);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000AC9F4 File Offset: 0x000AABF4
		internal static bool SortedSetEquals(SortedSet<T> set1, SortedSet<T> set2, IComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (SortedSet<T>.AreComparersEqual(set1, set2))
			{
				return set1.Count == set2.Count && set1.SetEquals(set2);
			}
			bool flag = false;
			foreach (T x in set1)
			{
				flag = false;
				foreach (T y in set2)
				{
					if (comparer.Compare(x, y) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000ACAC0 File Offset: 0x000AACC0
		private static bool AreComparersEqual(SortedSet<T> set1, SortedSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000ACAD3 File Offset: 0x000AACD3
		private static void Split4Node(SortedSet<T>.Node node)
		{
			node.IsRed = true;
			node.Left.IsRed = false;
			node.Right.IsRed = false;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000ACAF4 File Offset: 0x000AACF4
		internal T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000ACB18 File Offset: 0x000AAD18
		[__DynamicallyInvokable]
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.count == 0)
			{
				SortedSet<T> sortedSet2 = new SortedSet<T>(sortedSet, this.comparer);
				this.root = sortedSet2.root;
				this.count = sortedSet2.count;
				this.version++;
				return;
			}
			if (sortedSet != null && treeSubSet == null && SortedSet<T>.AreComparersEqual(this, sortedSet) && sortedSet.Count > this.Count / 2)
			{
				T[] array = new T[sortedSet.Count + this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						array[num++] = enumerator.Current;
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						array[num++] = enumerator2.Current;
						flag2 = !enumerator2.MoveNext();
					}
				}
				if (!flag || !flag2)
				{
					SortedSet<T>.Enumerator enumerator3 = flag ? enumerator2 : enumerator;
					do
					{
						array[num++] = enumerator3.Current;
					}
					while (enumerator3.MoveNext());
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.AddAllElements(other);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000ACD04 File Offset: 0x000AAF04
		private static SortedSet<T>.Node ConstructRootFromSortedArray(T[] arr, int startIndex, int endIndex, SortedSet<T>.Node redNode)
		{
			int num = endIndex - startIndex + 1;
			if (num == 0)
			{
				return null;
			}
			SortedSet<T>.Node node;
			if (num == 1)
			{
				node = new SortedSet<T>.Node(arr[startIndex], false);
				if (redNode != null)
				{
					node.Left = redNode;
				}
			}
			else if (num == 2)
			{
				node = new SortedSet<T>.Node(arr[startIndex], false);
				node.Right = new SortedSet<T>.Node(arr[endIndex], false);
				node.Right.IsRed = true;
				if (redNode != null)
				{
					node.Left = redNode;
				}
			}
			else if (num == 3)
			{
				node = new SortedSet<T>.Node(arr[startIndex + 1], false);
				node.Left = new SortedSet<T>.Node(arr[startIndex], false);
				node.Right = new SortedSet<T>.Node(arr[endIndex], false);
				if (redNode != null)
				{
					node.Left.Left = redNode;
				}
			}
			else
			{
				int num2 = (startIndex + endIndex) / 2;
				node = new SortedSet<T>.Node(arr[num2], false);
				node.Left = SortedSet<T>.ConstructRootFromSortedArray(arr, startIndex, num2 - 1, redNode);
				if (num % 2 == 0)
				{
					node.Right = SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 2, endIndex, new SortedSet<T>.Node(arr[num2 + 1], true));
				}
				else
				{
					node.Right = SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 1, endIndex, null);
				}
			}
			return node;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000ACE30 File Offset: 0x000AB030
		[__DynamicallyInvokable]
		public virtual void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				T[] array = new T[this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				T max = this.Max;
				T min = this.Min;
				while (!flag && !flag2 && this.Comparer.Compare(enumerator2.Current, max) <= 0)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						flag2 = !enumerator2.MoveNext();
					}
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000ACF8C File Offset: 0x000AB18C
		internal virtual void IntersectWithEnumerable(IEnumerable<T> other)
		{
			List<T> list = new List<T>(this.Count);
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					list.Add(item);
					this.Remove(item);
				}
			}
			this.Clear();
			this.AddAllElements(list);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000AD000 File Offset: 0x000AB200
		[__DynamicallyInvokable]
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				if (this.comparer.Compare(sortedSet.Max, this.Min) < 0 || this.comparer.Compare(sortedSet.Min, this.Max) > 0)
				{
					return;
				}
				T min = this.Min;
				T max = this.Max;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						if (this.comparer.Compare(t, min) >= 0)
						{
							if (this.comparer.Compare(t, max) > 0)
							{
								break;
							}
							this.Remove(t);
						}
					}
					return;
				}
			}
			this.RemoveAllElements(other);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000AD0F8 File Offset: 0x000AB2F8
		[__DynamicallyInvokable]
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				this.SymmetricExceptWithSameEC(sortedSet);
				return;
			}
			T[] array = new List<T>(other).ToArray();
			Array.Sort<T>(array, this.Comparer);
			this.SymmetricExceptWithSameEC(array);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000AD168 File Offset: 0x000AB368
		internal void SymmetricExceptWithSameEC(ISet<T> other)
		{
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					this.Remove(item);
				}
				else
				{
					this.Add(item);
				}
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000AD1C4 File Offset: 0x000AB3C4
		internal void SymmetricExceptWithSameEC(T[] other)
		{
			if (other.Length == 0)
			{
				return;
			}
			T y = other[0];
			for (int i = 0; i < other.Length; i++)
			{
				while (i < other.Length && i != 0 && this.comparer.Compare(other[i], y) == 0)
				{
					i++;
				}
				if (i >= other.Length)
				{
					break;
				}
				if (this.Contains(other[i]))
				{
					this.Remove(other[i]);
				}
				else
				{
					this.Add(other[i]);
				}
				y = other[i];
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000AD24C File Offset: 0x000AB44C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				return this.Count <= sortedSet.Count && this.IsSubsetOfSortedSetWithSameEC(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this.Count && elementCount.unfoundCount >= 0;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000AD2C4 File Offset: 0x000AB4C4
		private bool IsSubsetOfSortedSetWithSameEC(SortedSet<T> asSorted)
		{
			SortedSet<T> viewBetween = asSorted.GetViewBetween(this.Min, this.Max);
			foreach (T item in this)
			{
				if (!viewBetween.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000AD330 File Offset: 0x000AB530
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && this.Count == 0)
			{
				return (other as ICollection).Count > 0;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				return this.Count < sortedSet.Count && this.IsSubsetOfSortedSetWithSameEC(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this.Count && elementCount.unfoundCount > 0;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000AD3B8 File Offset: 0x000AB5B8
		[__DynamicallyInvokable]
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				return this.ContainsAllElements(other);
			}
			if (this.Count < sortedSet.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T item in sortedSet)
			{
				if (!viewBetween.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000AD474 File Offset: 0x000AB674
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !SortedSet<T>.AreComparersEqual(sortedSet, this))
			{
				SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
				return elementCount.uniqueCount < this.Count && elementCount.unfoundCount == 0;
			}
			if (sortedSet.Count >= this.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T item in sortedSet)
			{
				if (!viewBetween.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000AD558 File Offset: 0x000AB758
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet))
			{
				IEnumerator<T> enumerator = this.GetEnumerator();
				IEnumerator<T> enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					if (this.Comparer.Compare(enumerator.Current, enumerator2.Current) != 0)
					{
						return false;
					}
					flag = !enumerator.MoveNext();
					flag2 = !enumerator2.MoveNext();
				}
				return flag && flag2;
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount == this.Count && elementCount.unfoundCount == 0;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000AD618 File Offset: 0x000AB818
		[__DynamicallyInvokable]
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection<T> && (other as ICollection<T>).Count == 0)
			{
				return false;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && SortedSet<T>.AreComparersEqual(this, sortedSet) && (this.comparer.Compare(this.Min, sortedSet.Max) > 0 || this.comparer.Compare(this.Max, sortedSet.Min) < 0))
			{
				return false;
			}
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000AD6E4 File Offset: 0x000AB8E4
		[SecurityCritical]
		private unsafe SortedSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			SortedSet<T>.ElementCount result;
			if (this.Count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				result.uniqueCount = 0;
				result.unfoundCount = num;
				return result;
			}
			int n = this.Count;
			int num2 = BitHelper.ToIntArrayLength(n);
			BitHelper bitHelper;
			if (num2 <= 100)
			{
				int* bitArrayPtr = stackalloc int[checked(unchecked((UIntPtr)num2) * 4)];
				bitHelper = new BitHelper(bitArrayPtr, num2);
			}
			else
			{
				int[] bitArray = new int[num2];
				bitHelper = new BitHelper(bitArray, num2);
			}
			int num3 = 0;
			int num4 = 0;
			foreach (T item in other)
			{
				int num5 = this.InternalIndexOf(item);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			result.uniqueCount = num4;
			result.unfoundCount = num3;
			return result;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000AD80C File Offset: 0x000ABA0C
		[__DynamicallyInvokable]
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> matches = new List<T>(this.Count);
			this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
			{
				if (match(n.Item))
				{
					matches.Add(n.Item);
				}
				return true;
			});
			int num = 0;
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				if (this.Remove(matches[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000AD890 File Offset: 0x000ABA90
		[__DynamicallyInvokable]
		public T Min
		{
			[__DynamicallyInvokable]
			get
			{
				T ret = default(T);
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
				{
					ret = n.Item;
					return false;
				});
				return ret;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000AD8C8 File Offset: 0x000ABAC8
		[__DynamicallyInvokable]
		public T Max
		{
			[__DynamicallyInvokable]
			get
			{
				T ret = default(T);
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
				{
					ret = n.Item;
					return false;
				}, true);
				return ret;
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000AD901 File Offset: 0x000ABB01
		[__DynamicallyInvokable]
		public IEnumerable<T> Reverse()
		{
			SortedSet<T>.Enumerator e = new SortedSet<T>.Enumerator(this, true);
			while (e.MoveNext())
			{
				T t = e.Current;
				yield return t;
			}
			yield break;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000AD911 File Offset: 0x000ABB11
		[__DynamicallyInvokable]
		public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
		{
			if (this.Comparer.Compare(lowerValue, upperValue) > 0)
			{
				throw new ArgumentException("lowerBound is greater than upperBound");
			}
			return new SortedSet<T>.TreeSubSet(this, lowerValue, upperValue, true, true);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000AD938 File Offset: 0x000ABB38
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000AD944 File Offset: 0x000ABB44
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000AD9C9 File Offset: 0x000ABBC9
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000AD9D4 File Offset: 0x000ABBD4
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_InvalidOnDeser);
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingValues);
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MismatchedCount);
			}
			this.siInfo = null;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000ADAA4 File Offset: 0x000ABCA4
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			SortedSet<T>.Node node = this.FindNode(equalValue);
			if (node != null)
			{
				actualValue = node.Item;
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000ADAD4 File Offset: 0x000ABCD4
		private static int log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x04002038 RID: 8248
		private SortedSet<T>.Node root;

		// Token: 0x04002039 RID: 8249
		private IComparer<T> comparer;

		// Token: 0x0400203A RID: 8250
		private int count;

		// Token: 0x0400203B RID: 8251
		private int version;

		// Token: 0x0400203C RID: 8252
		private object _syncRoot;

		// Token: 0x0400203D RID: 8253
		private const string ComparerName = "Comparer";

		// Token: 0x0400203E RID: 8254
		private const string CountName = "Count";

		// Token: 0x0400203F RID: 8255
		private const string ItemsName = "Items";

		// Token: 0x04002040 RID: 8256
		private const string VersionName = "Version";

		// Token: 0x04002041 RID: 8257
		private const string TreeName = "Tree";

		// Token: 0x04002042 RID: 8258
		private const string NodeValueName = "Item";

		// Token: 0x04002043 RID: 8259
		private const string EnumStartName = "EnumStarted";

		// Token: 0x04002044 RID: 8260
		private const string ReverseName = "Reverse";

		// Token: 0x04002045 RID: 8261
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x04002046 RID: 8262
		private const string minName = "Min";

		// Token: 0x04002047 RID: 8263
		private const string maxName = "Max";

		// Token: 0x04002048 RID: 8264
		private const string lBoundActiveName = "lBoundActive";

		// Token: 0x04002049 RID: 8265
		private const string uBoundActiveName = "uBoundActive";

		// Token: 0x0400204A RID: 8266
		private SerializationInfo siInfo;

		// Token: 0x0400204B RID: 8267
		internal const int StackAllocThreshold = 100;

		// Token: 0x02000800 RID: 2048
		[Serializable]
		internal sealed class TreeSubSet : SortedSet<T>, ISerializable, IDeserializationCallback
		{
			// Token: 0x060044A7 RID: 17575 RVA: 0x0011FA64 File Offset: 0x0011DC64
			public TreeSubSet(SortedSet<T> Underlying, T Min, T Max, bool lowerBoundActive, bool upperBoundActive) : base(Underlying.Comparer)
			{
				this.underlying = Underlying;
				this.min = Min;
				this.max = Max;
				this.lBoundActive = lowerBoundActive;
				this.uBoundActive = upperBoundActive;
				this.root = this.underlying.FindRange(this.min, this.max, this.lBoundActive, this.uBoundActive);
				this.count = 0;
				this.version = -1;
				this.VersionCheckImpl();
			}

			// Token: 0x060044A8 RID: 17576 RVA: 0x0011FADF File Offset: 0x0011DCDF
			private TreeSubSet()
			{
				this.comparer = null;
			}

			// Token: 0x060044A9 RID: 17577 RVA: 0x0011FAEE File Offset: 0x0011DCEE
			private TreeSubSet(SerializationInfo info, StreamingContext context)
			{
				this.siInfo = info;
				this.OnDeserializationImpl(info);
			}

			// Token: 0x060044AA RID: 17578 RVA: 0x0011FB04 File Offset: 0x0011DD04
			internal override bool AddIfNotPresent(T item)
			{
				if (!this.IsWithinRange(item))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.collection);
				}
				bool result = this.underlying.AddIfNotPresent(item);
				this.VersionCheck();
				return result;
			}

			// Token: 0x060044AB RID: 17579 RVA: 0x0011FB34 File Offset: 0x0011DD34
			public override bool Contains(T item)
			{
				this.VersionCheck();
				return base.Contains(item);
			}

			// Token: 0x060044AC RID: 17580 RVA: 0x0011FB44 File Offset: 0x0011DD44
			internal override bool DoRemove(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return false;
				}
				bool result = this.underlying.Remove(item);
				this.VersionCheck();
				return result;
			}

			// Token: 0x060044AD RID: 17581 RVA: 0x0011FB70 File Offset: 0x0011DD70
			public override void Clear()
			{
				if (this.count == 0)
				{
					return;
				}
				List<T> toRemove = new List<T>();
				this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
				{
					toRemove.Add(n.Item);
					return true;
				});
				while (toRemove.Count != 0)
				{
					this.underlying.Remove(toRemove[toRemove.Count - 1]);
					toRemove.RemoveAt(toRemove.Count - 1);
				}
				this.root = null;
				this.count = 0;
				this.version = this.underlying.version;
			}

			// Token: 0x060044AE RID: 17582 RVA: 0x0011FC14 File Offset: 0x0011DE14
			internal override bool IsWithinRange(T item)
			{
				int num = this.lBoundActive ? base.Comparer.Compare(this.min, item) : -1;
				if (num > 0)
				{
					return false;
				}
				num = (this.uBoundActive ? base.Comparer.Compare(this.max, item) : 1);
				return num >= 0;
			}

			// Token: 0x060044AF RID: 17583 RVA: 0x0011FC6C File Offset: 0x0011DE6C
			internal override bool InOrderTreeWalk(TreeWalkPredicate<T> action, bool reverse)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(this.count + 1));
				SortedSet<T>.Node node = this.root;
				while (node != null)
				{
					if (this.IsWithinRange(node.Item))
					{
						stack.Push(node);
						node = (reverse ? node.Right : node.Left);
					}
					else if (this.lBoundActive && base.Comparer.Compare(this.min, node.Item) > 0)
					{
						node = node.Right;
					}
					else
					{
						node = node.Left;
					}
				}
				while (stack.Count != 0)
				{
					node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					SortedSet<T>.Node node2 = reverse ? node.Left : node.Right;
					while (node2 != null)
					{
						if (this.IsWithinRange(node2.Item))
						{
							stack.Push(node2);
							node2 = (reverse ? node2.Right : node2.Left);
						}
						else if (this.lBoundActive && base.Comparer.Compare(this.min, node2.Item) > 0)
						{
							node2 = node2.Right;
						}
						else
						{
							node2 = node2.Left;
						}
					}
				}
				return true;
			}

			// Token: 0x060044B0 RID: 17584 RVA: 0x0011FD9C File Offset: 0x0011DF9C
			internal override bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				List<SortedSet<T>.Node> list = new List<SortedSet<T>.Node>();
				list.Add(this.root);
				while (list.Count != 0)
				{
					SortedSet<T>.Node node = list[0];
					list.RemoveAt(0);
					if (this.IsWithinRange(node.Item) && !action(node))
					{
						return false;
					}
					if (node.Left != null && (!this.lBoundActive || base.Comparer.Compare(this.min, node.Item) < 0))
					{
						list.Add(node.Left);
					}
					if (node.Right != null && (!this.uBoundActive || base.Comparer.Compare(this.max, node.Item) > 0))
					{
						list.Add(node.Right);
					}
				}
				return true;
			}

			// Token: 0x060044B1 RID: 17585 RVA: 0x0011FE70 File Offset: 0x0011E070
			internal override SortedSet<T>.Node FindNode(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return null;
				}
				this.VersionCheck();
				return base.FindNode(item);
			}

			// Token: 0x060044B2 RID: 17586 RVA: 0x0011FE8C File Offset: 0x0011E08C
			internal override int InternalIndexOf(T item)
			{
				int num = -1;
				foreach (T y in this)
				{
					num++;
					if (base.Comparer.Compare(item, y) == 0)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x060044B3 RID: 17587 RVA: 0x0011FEF0 File Offset: 0x0011E0F0
			internal override void VersionCheck()
			{
				this.VersionCheckImpl();
			}

			// Token: 0x060044B4 RID: 17588 RVA: 0x0011FEF8 File Offset: 0x0011E0F8
			private void VersionCheckImpl()
			{
				if (this.version != this.underlying.version)
				{
					this.root = this.underlying.FindRange(this.min, this.max, this.lBoundActive, this.uBoundActive);
					this.version = this.underlying.version;
					this.count = 0;
					base.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
					{
						this.count++;
						return true;
					});
				}
			}

			// Token: 0x060044B5 RID: 17589 RVA: 0x0011FF6C File Offset: 0x0011E16C
			public override SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
			{
				if (this.lBoundActive && base.Comparer.Compare(this.min, lowerValue) > 0)
				{
					throw new ArgumentOutOfRangeException("lowerValue");
				}
				if (this.uBoundActive && base.Comparer.Compare(this.max, upperValue) < 0)
				{
					throw new ArgumentOutOfRangeException("upperValue");
				}
				return (SortedSet<T>.TreeSubSet)this.underlying.GetViewBetween(lowerValue, upperValue);
			}

			// Token: 0x060044B6 RID: 17590 RVA: 0x0011FFE0 File Offset: 0x0011E1E0
			internal override void IntersectWithEnumerable(IEnumerable<T> other)
			{
				List<T> list = new List<T>(base.Count);
				foreach (T item in other)
				{
					if (this.Contains(item))
					{
						list.Add(item);
						base.Remove(item);
					}
				}
				this.Clear();
				base.AddAllElements(list);
			}

			// Token: 0x060044B7 RID: 17591 RVA: 0x00120054 File Offset: 0x0011E254
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x060044B8 RID: 17592 RVA: 0x00120060 File Offset: 0x0011E260
			protected override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
				}
				info.AddValue("Max", this.max, typeof(T));
				info.AddValue("Min", this.min, typeof(T));
				info.AddValue("lBoundActive", this.lBoundActive);
				info.AddValue("uBoundActive", this.uBoundActive);
				base.GetObjectData(info, context);
			}

			// Token: 0x060044B9 RID: 17593 RVA: 0x001200E0 File Offset: 0x0011E2E0
			void IDeserializationCallback.OnDeserialization(object sender)
			{
			}

			// Token: 0x060044BA RID: 17594 RVA: 0x001200E2 File Offset: 0x0011E2E2
			protected override void OnDeserialization(object sender)
			{
				this.OnDeserializationImpl(sender);
			}

			// Token: 0x060044BB RID: 17595 RVA: 0x001200EC File Offset: 0x0011E2EC
			private void OnDeserializationImpl(object sender)
			{
				if (this.siInfo == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_InvalidOnDeser);
				}
				this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
				int @int = this.siInfo.GetInt32("Count");
				this.max = (T)((object)this.siInfo.GetValue("Max", typeof(T)));
				this.min = (T)((object)this.siInfo.GetValue("Min", typeof(T)));
				this.lBoundActive = this.siInfo.GetBoolean("lBoundActive");
				this.uBoundActive = this.siInfo.GetBoolean("uBoundActive");
				this.underlying = new SortedSet<T>();
				if (@int != 0)
				{
					T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
					if (array == null)
					{
						ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingValues);
					}
					for (int i = 0; i < array.Length; i++)
					{
						this.underlying.Add(array[i]);
					}
				}
				this.underlying.version = this.siInfo.GetInt32("Version");
				this.count = this.underlying.count;
				this.version = this.underlying.version - 1;
				this.VersionCheck();
				if (this.count != @int)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MismatchedCount);
				}
				this.siInfo = null;
			}

			// Token: 0x04003545 RID: 13637
			private SortedSet<T> underlying;

			// Token: 0x04003546 RID: 13638
			private T min;

			// Token: 0x04003547 RID: 13639
			private T max;

			// Token: 0x04003548 RID: 13640
			private bool lBoundActive;

			// Token: 0x04003549 RID: 13641
			private bool uBoundActive;
		}

		// Token: 0x02000801 RID: 2049
		internal class Node
		{
			// Token: 0x060044BD RID: 17597 RVA: 0x00120277 File Offset: 0x0011E477
			public Node(T item)
			{
				this.Item = item;
				this.IsRed = true;
			}

			// Token: 0x060044BE RID: 17598 RVA: 0x0012028D File Offset: 0x0011E48D
			public Node(T item, bool isRed)
			{
				this.Item = item;
				this.IsRed = isRed;
			}

			// Token: 0x0400354A RID: 13642
			public bool IsRed;

			// Token: 0x0400354B RID: 13643
			public T Item;

			// Token: 0x0400354C RID: 13644
			public SortedSet<T>.Node Left;

			// Token: 0x0400354D RID: 13645
			public SortedSet<T>.Node Right;
		}

		// Token: 0x02000802 RID: 2050
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x060044BF RID: 17599 RVA: 0x001202A4 File Offset: 0x0011E4A4
			internal Enumerator(SortedSet<T> set)
			{
				this.tree = set;
				this.tree.VersionCheck();
				this.version = this.tree.version;
				this.stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(set.Count + 1));
				this.current = null;
				this.reverse = false;
				this.siInfo = null;
				this.Intialize();
			}

			// Token: 0x060044C0 RID: 17600 RVA: 0x0012030C File Offset: 0x0011E50C
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this.tree = set;
				this.tree.VersionCheck();
				this.version = this.tree.version;
				this.stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(set.Count + 1));
				this.current = null;
				this.reverse = reverse;
				this.siInfo = null;
				this.Intialize();
			}

			// Token: 0x060044C1 RID: 17601 RVA: 0x00120371 File Offset: 0x0011E571
			private Enumerator(SerializationInfo info, StreamingContext context)
			{
				this.tree = null;
				this.version = -1;
				this.current = null;
				this.reverse = false;
				this.stack = null;
				this.siInfo = info;
			}

			// Token: 0x060044C2 RID: 17602 RVA: 0x0012039D File Offset: 0x0011E59D
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x060044C3 RID: 17603 RVA: 0x001203A8 File Offset: 0x0011E5A8
			private void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
				}
				info.AddValue("Tree", this.tree, typeof(SortedSet<T>));
				info.AddValue("EnumVersion", this.version);
				info.AddValue("Reverse", this.reverse);
				info.AddValue("EnumStarted", !this.NotStartedOrEnded);
				info.AddValue("Item", (this.current == null) ? SortedSet<T>.Enumerator.dummyNode.Item : this.current.Item, typeof(T));
			}

			// Token: 0x060044C4 RID: 17604 RVA: 0x00120448 File Offset: 0x0011E648
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.OnDeserialization(sender);
			}

			// Token: 0x060044C5 RID: 17605 RVA: 0x00120454 File Offset: 0x0011E654
			private void OnDeserialization(object sender)
			{
				if (this.siInfo == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_InvalidOnDeser);
				}
				this.tree = (SortedSet<T>)this.siInfo.GetValue("Tree", typeof(SortedSet<T>));
				this.version = this.siInfo.GetInt32("EnumVersion");
				this.reverse = this.siInfo.GetBoolean("Reverse");
				bool boolean = this.siInfo.GetBoolean("EnumStarted");
				this.stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.log2(this.tree.Count + 1));
				this.current = null;
				if (boolean)
				{
					T y = (T)((object)this.siInfo.GetValue("Item", typeof(T)));
					this.Intialize();
					while (this.MoveNext() && this.tree.Comparer.Compare(this.Current, y) != 0)
					{
					}
				}
			}

			// Token: 0x060044C6 RID: 17606 RVA: 0x00120544 File Offset: 0x0011E744
			private void Intialize()
			{
				this.current = null;
				SortedSet<T>.Node node = this.tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = this.reverse ? node.Right : node.Left;
					SortedSet<T>.Node node3 = this.reverse ? node.Left : node.Right;
					if (this.tree.IsWithinRange(node.Item))
					{
						this.stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this.tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			// Token: 0x060044C7 RID: 17607 RVA: 0x001205DC File Offset: 0x0011E7DC
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				this.tree.VersionCheck();
				if (this.version != this.tree.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.stack.Count == 0)
				{
					this.current = null;
					return false;
				}
				this.current = this.stack.Pop();
				SortedSet<T>.Node node = this.reverse ? this.current.Left : this.current.Right;
				while (node != null)
				{
					SortedSet<T>.Node node2 = this.reverse ? node.Right : node.Left;
					SortedSet<T>.Node node3 = this.reverse ? node.Left : node.Right;
					if (this.tree.IsWithinRange(node.Item))
					{
						this.stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this.tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			// Token: 0x060044C8 RID: 17608 RVA: 0x001206CD File Offset: 0x0011E8CD
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			// Token: 0x17000F9D RID: 3997
			// (get) Token: 0x060044C9 RID: 17609 RVA: 0x001206D0 File Offset: 0x0011E8D0
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.current != null)
					{
						return this.current.Item;
					}
					return default(T);
				}
			}

			// Token: 0x17000F9E RID: 3998
			// (get) Token: 0x060044CA RID: 17610 RVA: 0x001206FA File Offset: 0x0011E8FA
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.current == null)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.Item;
				}
			}

			// Token: 0x17000F9F RID: 3999
			// (get) Token: 0x060044CB RID: 17611 RVA: 0x0012071B File Offset: 0x0011E91B
			internal bool NotStartedOrEnded
			{
				get
				{
					return this.current == null;
				}
			}

			// Token: 0x060044CC RID: 17612 RVA: 0x00120726 File Offset: 0x0011E926
			internal void Reset()
			{
				if (this.version != this.tree.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.stack.Clear();
				this.Intialize();
			}

			// Token: 0x060044CD RID: 17613 RVA: 0x00120753 File Offset: 0x0011E953
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x0400354E RID: 13646
			private SortedSet<T> tree;

			// Token: 0x0400354F RID: 13647
			private int version;

			// Token: 0x04003550 RID: 13648
			private Stack<SortedSet<T>.Node> stack;

			// Token: 0x04003551 RID: 13649
			private SortedSet<T>.Node current;

			// Token: 0x04003552 RID: 13650
			private static SortedSet<T>.Node dummyNode = new SortedSet<T>.Node(default(T));

			// Token: 0x04003553 RID: 13651
			private bool reverse;

			// Token: 0x04003554 RID: 13652
			private SerializationInfo siInfo;
		}

		// Token: 0x02000803 RID: 2051
		internal struct ElementCount
		{
			// Token: 0x04003555 RID: 13653
			internal int uniqueCount;

			// Token: 0x04003556 RID: 13654
			internal int unfoundCount;
		}
	}
}
