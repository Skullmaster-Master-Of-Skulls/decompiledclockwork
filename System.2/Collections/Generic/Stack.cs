using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020003C6 RID: 966
	[DebuggerTypeProxy(typeof(System_StackDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x000AB11B File Offset: 0x000A931B
		[__DynamicallyInvokable]
		public Stack()
		{
			this._array = Stack<T>._emptyArray;
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000AB13C File Offset: 0x000A933C
		[__DynamicallyInvokable]
		public Stack(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
			}
			this._array = new T[capacity];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000AB16C File Offset: 0x000A936C
		[__DynamicallyInvokable]
		public Stack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				this._array = new T[count];
				collection2.CopyTo(this._array, 0);
				this._size = count;
				return;
			}
			this._size = 0;
			this._array = new T[4];
			foreach (T item in collection)
			{
				this.Push(item);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000AB208 File Offset: 0x000A9408
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000AB210 File Offset: 0x000A9410
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000AB213 File Offset: 0x000A9413
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

		// Token: 0x060024A8 RID: 9384 RVA: 0x000AB235 File Offset: 0x000A9435
		[__DynamicallyInvokable]
		public void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000AB260 File Offset: 0x000A9460
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			int size = this._size;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			while (size-- > 0)
			{
				if (item == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && @default.Equals(this._array[size], item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000AB2CC File Offset: 0x000A94CC
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this._size)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._array, 0, array, arrayIndex, this._size);
			Array.Reverse(array, arrayIndex, this._size);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000AB32C File Offset: 0x000A952C
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
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
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this._size)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			try
			{
				Array.Copy(this._array, 0, array, arrayIndex, this._size);
				Array.Reverse(array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000AB3CC File Offset: 0x000A95CC
		[__DynamicallyInvokable]
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000AB3D4 File Offset: 0x000A95D4
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000AB3E1 File Offset: 0x000A95E1
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000AB3F0 File Offset: 0x000A95F0
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				T[] array = new T[this._size];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
				this._version++;
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000AB450 File Offset: 0x000A9650
		[__DynamicallyInvokable]
		public T Peek()
		{
			if (this._size == 0)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EmptyStack);
			}
			return this._array[this._size - 1];
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000AB474 File Offset: 0x000A9674
		[__DynamicallyInvokable]
		public T Pop()
		{
			if (this._size == 0)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EmptyStack);
			}
			this._version++;
			T[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			T result = array[num];
			this._array[this._size] = default(T);
			return result;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000AB4D8 File Offset: 0x000A96D8
		[__DynamicallyInvokable]
		public void Push(T item)
		{
			if (this._size == this._array.Length)
			{
				T[] array = new T[(this._array.Length == 0) ? 4 : (2 * this._array.Length)];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			T[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = item;
			this._version++;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000AB558 File Offset: 0x000A9758
		[__DynamicallyInvokable]
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x0400202A RID: 8234
		private T[] _array;

		// Token: 0x0400202B RID: 8235
		private int _size;

		// Token: 0x0400202C RID: 8236
		private int _version;

		// Token: 0x0400202D RID: 8237
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0400202E RID: 8238
		private const int _defaultCapacity = 4;

		// Token: 0x0400202F RID: 8239
		private static T[] _emptyArray = new T[0];

		// Token: 0x020007F9 RID: 2041
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06004474 RID: 17524 RVA: 0x0011F2D8 File Offset: 0x0011D4D8
			internal Enumerator(Stack<T> stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this.currentElement = default(T);
			}

			// Token: 0x06004475 RID: 17525 RVA: 0x0011F306 File Offset: 0x0011D506
			[__DynamicallyInvokable]
			public void Dispose()
			{
				this._index = -1;
			}

			// Token: 0x06004476 RID: 17526 RVA: 0x0011F310 File Offset: 0x0011D510
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				bool flag;
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					flag = (this._index >= 0);
					if (flag)
					{
						this.currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				flag = (num >= 0);
				if (flag)
				{
					this.currentElement = this._stack._array[this._index];
				}
				else
				{
					this.currentElement = default(T);
				}
				return flag;
			}

			// Token: 0x17000F8D RID: 3981
			// (get) Token: 0x06004477 RID: 17527 RVA: 0x0011F3D3 File Offset: 0x0011D5D3
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this._index == -2)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
					}
					if (this._index == -1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
					}
					return this.currentElement;
				}
			}

			// Token: 0x17000F8E RID: 3982
			// (get) Token: 0x06004478 RID: 17528 RVA: 0x0011F3FC File Offset: 0x0011D5FC
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this._index == -2)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
					}
					if (this._index == -1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
					}
					return this.currentElement;
				}
			}

			// Token: 0x06004479 RID: 17529 RVA: 0x0011F42A File Offset: 0x0011D62A
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this._version != this._stack._version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this._index = -2;
				this.currentElement = default(T);
			}

			// Token: 0x04003536 RID: 13622
			private Stack<T> _stack;

			// Token: 0x04003537 RID: 13623
			private int _index;

			// Token: 0x04003538 RID: 13624
			private int _version;

			// Token: 0x04003539 RID: 13625
			private T currentElement;
		}
	}
}
