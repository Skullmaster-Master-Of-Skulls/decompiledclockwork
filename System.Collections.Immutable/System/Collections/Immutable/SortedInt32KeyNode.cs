using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000017 RID: 23
	[DebuggerDisplay("{_key} = {_value}")]
	internal sealed class SortedInt32KeyNode<TValue> : IBinaryTree
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00002CB6 File Offset: 0x00000EB6
		private SortedInt32KeyNode()
		{
			this._frozen = true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002CC8 File Offset: 0x00000EC8
		private SortedInt32KeyNode(int key, TValue value, SortedInt32KeyNode<TValue> left, SortedInt32KeyNode<TValue> right, bool frozen = false)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(left, "left");
			Requires.NotNull<SortedInt32KeyNode<TValue>>(right, "right");
			this._key = key;
			this._value = value;
			this._left = left;
			this._right = right;
			this._frozen = frozen;
			this._height = checked(1 + Math.Max(left._height, right._height));
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002D32 File Offset: 0x00000F32
		public bool IsEmpty
		{
			get
			{
				return this._left == null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002D3D File Offset: 0x00000F3D
		public int Height
		{
			get
			{
				return (int)this._height;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002D45 File Offset: 0x00000F45
		public SortedInt32KeyNode<TValue> Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002D4D File Offset: 0x00000F4D
		public SortedInt32KeyNode<TValue> Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002D45 File Offset: 0x00000F45
		IBinaryTree IBinaryTree.Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002D4D File Offset: 0x00000F4D
		IBinaryTree IBinaryTree.Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002D65 File Offset: 0x00000F65
		int IBinaryTree.Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002D6C File Offset: 0x00000F6C
		public KeyValuePair<int, TValue> Value
		{
			get
			{
				return new KeyValuePair<int, TValue>(this._key, this._value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002D7F File Offset: 0x00000F7F
		internal IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, TValue> keyValuePair in this)
				{
					yield return keyValuePair.Value;
				}
				SortedInt32KeyNode<TValue>.Enumerator enumerator = default(SortedInt32KeyNode<TValue>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002D8F File Offset: 0x00000F8F
		public SortedInt32KeyNode<TValue>.Enumerator GetEnumerator()
		{
			return new SortedInt32KeyNode<TValue>.Enumerator(this);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002D97 File Offset: 0x00000F97
		internal SortedInt32KeyNode<TValue> SetItem(int key, TValue value, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
		{
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			return this.SetOrAdd(key, value, valueComparer, true, out replacedExistingValue, out mutated);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002DB2 File Offset: 0x00000FB2
		internal SortedInt32KeyNode<TValue> Remove(int key, out bool mutated)
		{
			return this.RemoveRecursive(key, out mutated);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002DBC File Offset: 0x00000FBC
		internal TValue GetValueOrDefault(int key)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this.Search(key);
			if (!sortedInt32KeyNode.IsEmpty)
			{
				return sortedInt32KeyNode._value;
			}
			return default(TValue);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002DEC File Offset: 0x00000FEC
		internal bool TryGetValue(int key, out TValue value)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this.Search(key);
			if (sortedInt32KeyNode.IsEmpty)
			{
				value = default(TValue);
				return false;
			}
			value = sortedInt32KeyNode._value;
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002E20 File Offset: 0x00001020
		internal void Freeze(Action<KeyValuePair<int, TValue>> freezeAction = null)
		{
			if (!this._frozen)
			{
				if (freezeAction != null)
				{
					freezeAction(new KeyValuePair<int, TValue>(this._key, this._value));
				}
				this._left.Freeze(freezeAction);
				this._right.Freeze(freezeAction);
				this._frozen = true;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002E70 File Offset: 0x00001070
		private static SortedInt32KeyNode<TValue> RotateLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> right = tree._right;
			return right.Mutate(tree.Mutate(null, right._left), null);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002EB4 File Offset: 0x000010B4
		private static SortedInt32KeyNode<TValue> RotateRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> left = tree._left;
			return left.Mutate(null, tree.Mutate(left._right, null));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002EF6 File Offset: 0x000010F6
		private static SortedInt32KeyNode<TValue> DoubleLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			return SortedInt32KeyNode<TValue>.RotateLeft(tree.Mutate(null, SortedInt32KeyNode<TValue>.RotateRight(tree._right)));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002F29 File Offset: 0x00001129
		private static SortedInt32KeyNode<TValue> DoubleRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			return SortedInt32KeyNode<TValue>.RotateRight(tree.Mutate(SortedInt32KeyNode<TValue>.RotateLeft(tree._left), null));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002F5C File Offset: 0x0000115C
		private static int Balance(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return (int)(tree._right._height - tree._left._height);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002F80 File Offset: 0x00001180
		private static bool IsRightHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) >= 2;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002F99 File Offset: 0x00001199
		private static bool IsLeftHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) <= -2;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002FB4 File Offset: 0x000011B4
		private static SortedInt32KeyNode<TValue> MakeBalanced(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (SortedInt32KeyNode<TValue>.IsRightHeavy(tree))
			{
				if (SortedInt32KeyNode<TValue>.Balance(tree._right) >= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateLeft(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleLeft(tree);
			}
			else
			{
				if (!SortedInt32KeyNode<TValue>.IsLeftHeavy(tree))
				{
					return tree;
				}
				if (SortedInt32KeyNode<TValue>.Balance(tree._left) <= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateRight(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleRight(tree);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003018 File Offset: 0x00001218
		private static SortedInt32KeyNode<TValue> NodeTreeFromList(IOrderedCollection<KeyValuePair<int, TValue>> items, int start, int length)
		{
			Requires.NotNull<IOrderedCollection<KeyValuePair<int, TValue>>>(items, "items");
			Requires.Range(start >= 0, "start", null);
			Requires.Range(length >= 0, "length", null);
			if (length == 0)
			{
				return SortedInt32KeyNode<TValue>.EmptyNode;
			}
			int num = (length - 1) / 2;
			int num2 = length - 1 - num;
			SortedInt32KeyNode<TValue> left = SortedInt32KeyNode<TValue>.NodeTreeFromList(items, start, num2);
			SortedInt32KeyNode<TValue> right = SortedInt32KeyNode<TValue>.NodeTreeFromList(items, start + num2 + 1, num);
			KeyValuePair<int, TValue> keyValuePair = items[start + num2];
			return new SortedInt32KeyNode<TValue>(keyValuePair.Key, keyValuePair.Value, left, right, true);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000030A0 File Offset: 0x000012A0
		private SortedInt32KeyNode<TValue> SetOrAdd(int key, TValue value, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
		{
			replacedExistingValue = false;
			if (this.IsEmpty)
			{
				mutated = true;
				return new SortedInt32KeyNode<TValue>(key, value, this, this, false);
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key > this._key)
			{
				SortedInt32KeyNode<TValue> right = this._right.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, right);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> left = this._left.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(left, null);
				}
			}
			else
			{
				if (valueComparer.Equals(this._value, value))
				{
					mutated = false;
					return this;
				}
				if (!overwriteExistingValue)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.DuplicateKey, new object[]
					{
						key
					}));
				}
				mutated = true;
				replacedExistingValue = true;
				sortedInt32KeyNode = new SortedInt32KeyNode<TValue>(key, value, this._left, this._right, false);
			}
			if (!mutated)
			{
				return sortedInt32KeyNode;
			}
			return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003198 File Offset: 0x00001398
		private SortedInt32KeyNode<TValue> RemoveRecursive(int key, out bool mutated)
		{
			if (this.IsEmpty)
			{
				mutated = false;
				return this;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key == this._key)
			{
				mutated = true;
				if (this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = SortedInt32KeyNode<TValue>.EmptyNode;
				}
				else if (this._right.IsEmpty && !this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._left;
				}
				else if (!this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._right;
				}
				else
				{
					SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right;
					while (!sortedInt32KeyNode2._left.IsEmpty)
					{
						sortedInt32KeyNode2 = sortedInt32KeyNode2._left;
					}
					bool flag;
					SortedInt32KeyNode<TValue> right = this._right.Remove(sortedInt32KeyNode2._key, out flag);
					sortedInt32KeyNode = sortedInt32KeyNode2.Mutate(this._left, right);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> left = this._left.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(left, null);
				}
			}
			else
			{
				SortedInt32KeyNode<TValue> right2 = this._right.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, right2);
				}
			}
			if (!sortedInt32KeyNode.IsEmpty)
			{
				return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
			}
			return sortedInt32KeyNode;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000032CC File Offset: 0x000014CC
		private SortedInt32KeyNode<TValue> Mutate(SortedInt32KeyNode<TValue> left = null, SortedInt32KeyNode<TValue> right = null)
		{
			if (this._frozen)
			{
				return new SortedInt32KeyNode<TValue>(this._key, this._value, left ?? this._left, right ?? this._right, false);
			}
			if (left != null)
			{
				this._left = left;
			}
			if (right != null)
			{
				this._right = right;
			}
			this._height = checked(1 + Math.Max(this._left._height, this._right._height));
			return this;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003343 File Offset: 0x00001543
		private SortedInt32KeyNode<TValue> Search(int key)
		{
			if (this.IsEmpty || key == this._key)
			{
				return this;
			}
			if (key > this._key)
			{
				return this._right.Search(key);
			}
			return this._left.Search(key);
		}

		// Token: 0x04000008 RID: 8
		internal static readonly SortedInt32KeyNode<TValue> EmptyNode = new SortedInt32KeyNode<TValue>();

		// Token: 0x04000009 RID: 9
		private readonly int _key;

		// Token: 0x0400000A RID: 10
		private TValue _value;

		// Token: 0x0400000B RID: 11
		private bool _frozen;

		// Token: 0x0400000C RID: 12
		private byte _height;

		// Token: 0x0400000D RID: 13
		private SortedInt32KeyNode<TValue> _left;

		// Token: 0x0400000E RID: 14
		private SortedInt32KeyNode<TValue> _right;

		// Token: 0x02000042 RID: 66
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<int, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x0600038A RID: 906 RVA: 0x000097FC File Offset: 0x000079FC
			internal Enumerator(SortedInt32KeyNode<TValue> root)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(root, "root");
				this._root = root;
				this._current = null;
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x0600038B RID: 907 RVA: 0x00009886 File Offset: 0x00007A86
			public KeyValuePair<int, TValue> Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x0600038C RID: 908 RVA: 0x000098A7 File Offset: 0x00007AA7
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x0600038D RID: 909 RVA: 0x000098AF File Offset: 0x00007AAF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600038E RID: 910 RVA: 0x000098BC File Offset: 0x00007ABC
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack;
				if (this._stack != null && this._stack.TryUse<SortedInt32KeyNode<TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x0600038F RID: 911 RVA: 0x00009914 File Offset: 0x00007B14
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						SortedInt32KeyNode<TValue> value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x06000390 RID: 912 RVA: 0x0000996E File Offset: 0x00007B6E
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._current = null;
				if (this._stack != null)
				{
					this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this).ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x06000391 RID: 913 RVA: 0x000099A2 File Offset: 0x00007BA2
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<SortedInt32KeyNode<TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<SortedInt32KeyNode<TValue>.Enumerator>(this);
				}
			}

			// Token: 0x06000392 RID: 914 RVA: 0x000099D0 File Offset: 0x00007BD0
			private void PushLeft(SortedInt32KeyNode<TValue> node)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(node, "node");
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<SortedInt32KeyNode<TValue>>(node));
					node = node.Left;
				}
			}

			// Token: 0x04000059 RID: 89
			private static readonly SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>();

			// Token: 0x0400005A RID: 90
			private readonly int _poolUserId;

			// Token: 0x0400005B RID: 91
			private SortedInt32KeyNode<TValue> _root;

			// Token: 0x0400005C RID: 92
			private SecurePooledObject<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>> _stack;

			// Token: 0x0400005D RID: 93
			private SortedInt32KeyNode<TValue> _current;
		}
	}
}
