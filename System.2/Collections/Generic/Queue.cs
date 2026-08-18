using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020003C4 RID: 964
	[DebuggerTypeProxy(typeof(System_QueueDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x000AA017 File Offset: 0x000A8217
		[__DynamicallyInvokable]
		public Queue()
		{
			this._array = Queue<T>._emptyArray;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000AA02A File Offset: 0x000A822A
		[__DynamicallyInvokable]
		public Queue(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
			}
			this._array = new T[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000AA060 File Offset: 0x000A8260
		[__DynamicallyInvokable]
		public Queue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this._array = new T[4];
			this._size = 0;
			this._version = 0;
			foreach (T item in collection)
			{
				this.Enqueue(item);
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x000AA0D0 File Offset: 0x000A82D0
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x000AA0D8 File Offset: 0x000A82D8
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x000AA0DB File Offset: 0x000A82DB
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

		// Token: 0x0600245A RID: 9306 RVA: 0x000AA100 File Offset: 0x000A8300
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this._head < this._tail)
			{
				Array.Clear(this._array, this._head, this._size);
			}
			else
			{
				Array.Clear(this._array, this._head, this._array.Length - this._head);
				Array.Clear(this._array, 0, this._tail);
			}
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._version++;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000AA18C File Offset: 0x000A838C
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			int num = array.Length;
			if (num - arrayIndex < this._size)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			int num2 = (num - arrayIndex < this._size) ? (num - arrayIndex) : this._size;
			if (num2 == 0)
			{
				return;
			}
			int num3 = (this._array.Length - this._head < num2) ? (this._array.Length - this._head) : num2;
			Array.Copy(this._array, this._head, array, arrayIndex, num3);
			num2 -= num3;
			if (num2 > 0)
			{
				Array.Copy(this._array, 0, array, arrayIndex + this._array.Length - this._head, num2);
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000AA248 File Offset: 0x000A8448
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
			int length = array.Length;
			if (index < 0 || index > length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (length - index < this._size)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			int num = (length - index < this._size) ? (length - index) : this._size;
			if (num == 0)
			{
				return;
			}
			try
			{
				int num2 = (this._array.Length - this._head < num) ? (this._array.Length - this._head) : num;
				Array.Copy(this._array, this._head, array, index, num2);
				num -= num2;
				if (num > 0)
				{
					Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000AA340 File Offset: 0x000A8540
		[__DynamicallyInvokable]
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * 200L / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = item;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000AA3D7 File Offset: 0x000A85D7
		[__DynamicallyInvokable]
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000AA3DF File Offset: 0x000A85DF
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000AA3EC File Offset: 0x000A85EC
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000AA3FC File Offset: 0x000A85FC
		[__DynamicallyInvokable]
		public T Dequeue()
		{
			if (this._size == 0)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EmptyQueue);
			}
			T result = this._array[this._head];
			this._array[this._head] = default(T);
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return result;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000AA478 File Offset: 0x000A8678
		[__DynamicallyInvokable]
		public T Peek()
		{
			if (this._size == 0)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EmptyQueue);
			}
			return this._array[this._head];
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000AA49C File Offset: 0x000A869C
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			int num = this._head;
			int size = this._size;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			while (size-- > 0)
			{
				if (item == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && @default.Equals(this._array[num], item))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000AA51C File Offset: 0x000A871C
		internal T GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000AA53C File Offset: 0x000A873C
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			if (this._size == 0)
			{
				return array;
			}
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000AA5D0 File Offset: 0x000A87D0
		private void SetCapacity(int capacity)
		{
			T[] array = new T[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000AA690 File Offset: 0x000A8890
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				this.SetCapacity(this._size);
			}
		}

		// Token: 0x04002013 RID: 8211
		private T[] _array;

		// Token: 0x04002014 RID: 8212
		private int _head;

		// Token: 0x04002015 RID: 8213
		private int _tail;

		// Token: 0x04002016 RID: 8214
		private int _size;

		// Token: 0x04002017 RID: 8215
		private int _version;

		// Token: 0x04002018 RID: 8216
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002019 RID: 8217
		private const int _MinimumGrow = 4;

		// Token: 0x0400201A RID: 8218
		private const int _ShrinkThreshold = 32;

		// Token: 0x0400201B RID: 8219
		private const int _GrowFactor = 200;

		// Token: 0x0400201C RID: 8220
		private const int _DefaultCapacity = 4;

		// Token: 0x0400201D RID: 8221
		private static T[] _emptyArray = new T[0];

		// Token: 0x020007F3 RID: 2035
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06004435 RID: 17461 RVA: 0x0011EA27 File Offset: 0x0011CC27
			internal Enumerator(Queue<T> q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x06004436 RID: 17462 RVA: 0x0011EA54 File Offset: 0x0011CC54
			[__DynamicallyInvokable]
			public void Dispose()
			{
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x06004437 RID: 17463 RVA: 0x0011EA6C File Offset: 0x0011CC6C
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -2;
					this._currentElement = default(T);
					return false;
				}
				this._currentElement = this._q.GetElement(this._index);
				return true;
			}

			// Token: 0x17000F78 RID: 3960
			// (get) Token: 0x06004438 RID: 17464 RVA: 0x0011EAEE File Offset: 0x0011CCEE
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this._index < 0)
					{
						if (this._index == -1)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
						}
						else
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
						}
					}
					return this._currentElement;
				}
			}

			// Token: 0x17000F79 RID: 3961
			// (get) Token: 0x06004439 RID: 17465 RVA: 0x0011EB18 File Offset: 0x0011CD18
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this._index < 0)
					{
						if (this._index == -1)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
						}
						else
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
						}
					}
					return this._currentElement;
				}
			}

			// Token: 0x0600443A RID: 17466 RVA: 0x0011EB47 File Offset: 0x0011CD47
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this._version != this._q._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x04003520 RID: 13600
			private Queue<T> _q;

			// Token: 0x04003521 RID: 13601
			private int _index;

			// Token: 0x04003522 RID: 13602
			private int _version;

			// Token: 0x04003523 RID: 13603
			private T _currentElement;
		}
	}
}
