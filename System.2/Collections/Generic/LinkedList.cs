using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020003C2 RID: 962
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(System_CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class LinkedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		// Token: 0x06002426 RID: 9254 RVA: 0x000A9700 File Offset: 0x000A7900
		[__DynamicallyInvokable]
		public LinkedList()
		{
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000A9708 File Offset: 0x000A7908
		[__DynamicallyInvokable]
		public LinkedList(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (T value in collection)
			{
				this.AddLast(value);
			}
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000A9768 File Offset: 0x000A7968
		protected LinkedList(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000A9777 File Offset: 0x000A7977
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000A977F File Offset: 0x000A797F
		[__DynamicallyInvokable]
		public LinkedListNode<T> First
		{
			[__DynamicallyInvokable]
			get
			{
				return this.head;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x000A9787 File Offset: 0x000A7987
		[__DynamicallyInvokable]
		public LinkedListNode<T> Last
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.head != null)
				{
					return this.head.prev;
				}
				return null;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000A979E File Offset: 0x000A799E
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000A97A1 File Offset: 0x000A79A1
		[__DynamicallyInvokable]
		void ICollection<!0>.Add(T value)
		{
			this.AddLast(value);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000A97AC File Offset: 0x000A79AC
		[__DynamicallyInvokable]
		public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
		{
			this.ValidateNode(node);
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(node.list, value);
			this.InternalInsertNodeBefore(node.next, linkedListNode);
			return linkedListNode;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000A97DB File Offset: 0x000A79DB
		[__DynamicallyInvokable]
		public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.ValidateNode(node);
			this.ValidateNewNode(newNode);
			this.InternalInsertNodeBefore(node.next, newNode);
			newNode.list = this;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000A9800 File Offset: 0x000A7A00
		[__DynamicallyInvokable]
		public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
		{
			this.ValidateNode(node);
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(node.list, value);
			this.InternalInsertNodeBefore(node, linkedListNode);
			if (node == this.head)
			{
				this.head = linkedListNode;
			}
			return linkedListNode;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000A983A File Offset: 0x000A7A3A
		[__DynamicallyInvokable]
		public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.ValidateNode(node);
			this.ValidateNewNode(newNode);
			this.InternalInsertNodeBefore(node, newNode);
			newNode.list = this;
			if (node == this.head)
			{
				this.head = newNode;
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000A986C File Offset: 0x000A7A6C
		[__DynamicallyInvokable]
		public LinkedListNode<T> AddFirst(T value)
		{
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(this, value);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(linkedListNode);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, linkedListNode);
				this.head = linkedListNode;
			}
			return linkedListNode;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000A98A7 File Offset: 0x000A7AA7
		[__DynamicallyInvokable]
		public void AddFirst(LinkedListNode<T> node)
		{
			this.ValidateNewNode(node);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(node);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, node);
				this.head = node;
			}
			node.list = this;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000A98DC File Offset: 0x000A7ADC
		[__DynamicallyInvokable]
		public LinkedListNode<T> AddLast(T value)
		{
			LinkedListNode<T> linkedListNode = new LinkedListNode<T>(this, value);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(linkedListNode);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, linkedListNode);
			}
			return linkedListNode;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000A9910 File Offset: 0x000A7B10
		[__DynamicallyInvokable]
		public void AddLast(LinkedListNode<T> node)
		{
			this.ValidateNewNode(node);
			if (this.head == null)
			{
				this.InternalInsertNodeToEmptyList(node);
			}
			else
			{
				this.InternalInsertNodeBefore(this.head, node);
			}
			node.list = this;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000A9940 File Offset: 0x000A7B40
		[__DynamicallyInvokable]
		public void Clear()
		{
			LinkedListNode<T> next = this.head;
			while (next != null)
			{
				LinkedListNode<T> linkedListNode = next;
				next = next.Next;
				linkedListNode.Invalidate();
			}
			this.head = null;
			this.count = 0;
			this.version++;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000A9984 File Offset: 0x000A7B84
		[__DynamicallyInvokable]
		public bool Contains(T value)
		{
			return this.Find(value) != null;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000A9990 File Offset: 0x000A7B90
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
				{
					index
				}));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			LinkedListNode<T> next = this.head;
			if (next != null)
			{
				do
				{
					array[index++] = next.item;
					next = next.next;
				}
				while (next != this.head);
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000A9A24 File Offset: 0x000A7C24
		[__DynamicallyInvokable]
		public LinkedListNode<T> Find(T value)
		{
			LinkedListNode<T> next = this.head;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (next != null)
			{
				if (value != null)
				{
					while (!@default.Equals(next.item, value))
					{
						next = next.next;
						if (next == this.head)
						{
							goto IL_5A;
						}
					}
					return next;
				}
				while (next.item != null)
				{
					next = next.next;
					if (next == this.head)
					{
						goto IL_5A;
					}
				}
				return next;
			}
			IL_5A:
			return null;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000A9A8C File Offset: 0x000A7C8C
		[__DynamicallyInvokable]
		public LinkedListNode<T> FindLast(T value)
		{
			if (this.head == null)
			{
				return null;
			}
			LinkedListNode<T> prev = this.head.prev;
			LinkedListNode<T> linkedListNode = prev;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (linkedListNode != null)
			{
				if (value != null)
				{
					while (!@default.Equals(linkedListNode.item, value))
					{
						linkedListNode = linkedListNode.prev;
						if (linkedListNode == prev)
						{
							goto IL_61;
						}
					}
					return linkedListNode;
				}
				while (linkedListNode.item != null)
				{
					linkedListNode = linkedListNode.prev;
					if (linkedListNode == prev)
					{
						goto IL_61;
					}
				}
				return linkedListNode;
			}
			IL_61:
			return null;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000A9AFB File Offset: 0x000A7CFB
		[__DynamicallyInvokable]
		public LinkedList<T>.Enumerator GetEnumerator()
		{
			return new LinkedList<T>.Enumerator(this);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000A9B03 File Offset: 0x000A7D03
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000A9B10 File Offset: 0x000A7D10
		[__DynamicallyInvokable]
		public bool Remove(T value)
		{
			LinkedListNode<T> linkedListNode = this.Find(value);
			if (linkedListNode != null)
			{
				this.InternalRemoveNode(linkedListNode);
				return true;
			}
			return false;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000A9B32 File Offset: 0x000A7D32
		[__DynamicallyInvokable]
		public void Remove(LinkedListNode<T> node)
		{
			this.ValidateNode(node);
			this.InternalRemoveNode(node);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000A9B42 File Offset: 0x000A7D42
		[__DynamicallyInvokable]
		public void RemoveFirst()
		{
			if (this.head == null)
			{
				throw new InvalidOperationException(SR.GetString("LinkedListEmpty"));
			}
			this.InternalRemoveNode(this.head);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000A9B68 File Offset: 0x000A7D68
		[__DynamicallyInvokable]
		public void RemoveLast()
		{
			if (this.head == null)
			{
				throw new InvalidOperationException(SR.GetString("LinkedListEmpty"));
			}
			this.InternalRemoveNode(this.head.prev);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000A9B94 File Offset: 0x000A7D94
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.version);
			info.AddValue("Count", this.count);
			if (this.count != 0)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Data", array, typeof(T[]));
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000A9C04 File Offset: 0x000A7E04
		public virtual void OnDeserialization(object sender)
		{
			if (this.siInfo == null)
			{
				return;
			}
			int @int = this.siInfo.GetInt32("Version");
			int int2 = this.siInfo.GetInt32("Count");
			if (int2 != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Data", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException(SR.GetString("Serialization_MissingValues"));
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddLast(array[i]);
				}
			}
			else
			{
				this.head = null;
			}
			this.version = @int;
			this.siInfo = null;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000A9CA8 File Offset: 0x000A7EA8
		private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			newNode.next = node;
			newNode.prev = node.prev;
			node.prev.next = newNode;
			node.prev = newNode;
			this.version++;
			this.count++;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000A9CF7 File Offset: 0x000A7EF7
		private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
		{
			newNode.next = newNode;
			newNode.prev = newNode;
			this.head = newNode;
			this.version++;
			this.count++;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000A9D2C File Offset: 0x000A7F2C
		internal void InternalRemoveNode(LinkedListNode<T> node)
		{
			if (node.next == node)
			{
				this.head = null;
			}
			else
			{
				node.next.prev = node.prev;
				node.prev.next = node.next;
				if (this.head == node)
				{
					this.head = node.next;
				}
			}
			node.Invalidate();
			this.count--;
			this.version++;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000A9DA4 File Offset: 0x000A7FA4
		internal void ValidateNewNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.list != null)
			{
				throw new InvalidOperationException(SR.GetString("LinkedListNodeIsAttached"));
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000A9DCC File Offset: 0x000A7FCC
		internal void ValidateNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.list != this)
			{
				throw new InvalidOperationException(SR.GetString("ExternalLinkedListNode"));
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x000A9DF5 File Offset: 0x000A7FF5
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000A9DF8 File Offset: 0x000A7FF8
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000A9E1C File Offset: 0x000A801C
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_MultiRank"));
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException(SR.GetString("Arg_NonZeroLowerBound"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
				{
					index
				}));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				throw new ArgumentException(SR.GetString("Invalid_Array_Type"));
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException(SR.GetString("Invalid_Array_Type"));
			}
			LinkedListNode<T> next = this.head;
			try
			{
				if (next != null)
				{
					do
					{
						array3[index++] = next.item;
						next = next.next;
					}
					while (next != this.head);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException(SR.GetString("Invalid_Array_Type"));
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000A9F70 File Offset: 0x000A8170
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002007 RID: 8199
		internal LinkedListNode<T> head;

		// Token: 0x04002008 RID: 8200
		internal int count;

		// Token: 0x04002009 RID: 8201
		internal int version;

		// Token: 0x0400200A RID: 8202
		private object _syncRoot;

		// Token: 0x0400200B RID: 8203
		private SerializationInfo siInfo;

		// Token: 0x0400200C RID: 8204
		private const string VersionName = "Version";

		// Token: 0x0400200D RID: 8205
		private const string CountName = "Count";

		// Token: 0x0400200E RID: 8206
		private const string ValuesName = "Data";

		// Token: 0x020007F2 RID: 2034
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x0600442C RID: 17452 RVA: 0x0011E6EF File Offset: 0x0011C8EF
			internal Enumerator(LinkedList<T> list)
			{
				this.list = list;
				this.version = list.version;
				this.node = list.head;
				this.current = default(T);
				this.index = 0;
				this.siInfo = null;
			}

			// Token: 0x0600442D RID: 17453 RVA: 0x0011E72A File Offset: 0x0011C92A
			internal Enumerator(SerializationInfo info, StreamingContext context)
			{
				this.siInfo = info;
				this.list = null;
				this.version = 0;
				this.node = null;
				this.current = default(T);
				this.index = 0;
			}

			// Token: 0x17000F76 RID: 3958
			// (get) Token: 0x0600442E RID: 17454 RVA: 0x0011E75B File Offset: 0x0011C95B
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000F77 RID: 3959
			// (get) Token: 0x0600442F RID: 17455 RVA: 0x0011E763 File Offset: 0x0011C963
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.list.Count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current;
				}
			}

			// Token: 0x06004430 RID: 17456 RVA: 0x0011E794 File Offset: 0x0011C994
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.node == null)
				{
					this.index = this.list.Count + 1;
					return false;
				}
				this.index++;
				this.current = this.node.item;
				this.node = this.node.next;
				if (this.node == this.list.head)
				{
					this.node = null;
				}
				return true;
			}

			// Token: 0x06004431 RID: 17457 RVA: 0x0011E82C File Offset: 0x0011CA2C
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				this.current = default(T);
				this.node = this.list.head;
				this.index = 0;
			}

			// Token: 0x06004432 RID: 17458 RVA: 0x0011E880 File Offset: 0x0011CA80
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			// Token: 0x06004433 RID: 17459 RVA: 0x0011E884 File Offset: 0x0011CA84
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("LinkedList", this.list);
				info.AddValue("Version", this.version);
				info.AddValue("Current", this.current);
				info.AddValue("Index", this.index);
			}

			// Token: 0x06004434 RID: 17460 RVA: 0x0011E8E8 File Offset: 0x0011CAE8
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				if (this.list != null)
				{
					return;
				}
				if (this.siInfo == null)
				{
					throw new SerializationException(SR.GetString("Serialization_InvalidOnDeser"));
				}
				this.list = (LinkedList<T>)this.siInfo.GetValue("LinkedList", typeof(LinkedList<T>));
				this.version = this.siInfo.GetInt32("Version");
				this.current = (T)((object)this.siInfo.GetValue("Current", typeof(T)));
				this.index = this.siInfo.GetInt32("Index");
				if (this.list.siInfo != null)
				{
					this.list.OnDeserialization(sender);
				}
				if (this.index == this.list.Count + 1)
				{
					this.node = null;
				}
				else
				{
					this.node = this.list.First;
					if (this.node != null && this.index != 0)
					{
						for (int i = 0; i < this.index; i++)
						{
							this.node = this.node.next;
						}
						if (this.node == this.list.First)
						{
							this.node = null;
						}
					}
				}
				this.siInfo = null;
			}

			// Token: 0x04003516 RID: 13590
			private LinkedList<T> list;

			// Token: 0x04003517 RID: 13591
			private LinkedListNode<T> node;

			// Token: 0x04003518 RID: 13592
			private int version;

			// Token: 0x04003519 RID: 13593
			private T current;

			// Token: 0x0400351A RID: 13594
			private int index;

			// Token: 0x0400351B RID: 13595
			private SerializationInfo siInfo;

			// Token: 0x0400351C RID: 13596
			private const string LinkedListName = "LinkedList";

			// Token: 0x0400351D RID: 13597
			private const string CurrentValueName = "Current";

			// Token: 0x0400351E RID: 13598
			private const string VersionName = "Version";

			// Token: 0x0400351F RID: 13599
			private const string IndexName = "Index";
		}
	}
}
