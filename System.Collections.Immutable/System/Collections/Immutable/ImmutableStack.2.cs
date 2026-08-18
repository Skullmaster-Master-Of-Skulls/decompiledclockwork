using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000035 RID: 53
	[DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {_head}")]
	[DebuggerTypeProxy(typeof(ImmutableStackDebuggerProxy<>))]
	public sealed class ImmutableStack<T> : IImmutableStack<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000915E File Offset: 0x0000735E
		private ImmutableStack()
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00009166 File Offset: 0x00007366
		private ImmutableStack(T head, ImmutableStack<T> tail)
		{
			Requires.NotNull<ImmutableStack<T>>(tail, "tail");
			this._head = head;
			this._tail = tail;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009187 File Offset: 0x00007387
		public static ImmutableStack<T> Empty
		{
			get
			{
				return ImmutableStack<T>.s_EmptyField;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000918E File Offset: 0x0000738E
		public ImmutableStack<T> Clear()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009195 File Offset: 0x00007395
		IImmutableStack<T> IImmutableStack<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000919D File Offset: 0x0000739D
		public bool IsEmpty
		{
			get
			{
				return this._tail == null;
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000091A8 File Offset: 0x000073A8
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._head;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000091C3 File Offset: 0x000073C3
		public ImmutableStack<T> Push(T value)
		{
			return new ImmutableStack<T>(value, this);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000091CC File Offset: 0x000073CC
		IImmutableStack<T> IImmutableStack<!0>.Push(T value)
		{
			return this.Push(value);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000091D5 File Offset: 0x000073D5
		public ImmutableStack<T> Pop()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._tail;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000091F0 File Offset: 0x000073F0
		public ImmutableStack<T> Pop(out T value)
		{
			value = this.Peek();
			return this.Pop();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009204 File Offset: 0x00007404
		IImmutableStack<T> IImmutableStack<!0>.Pop()
		{
			return this.Pop();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000920C File Offset: 0x0000740C
		public ImmutableStack<T>.Enumerator GetEnumerator()
		{
			return new ImmutableStack<T>.Enumerator(this);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009214 File Offset: 0x00007414
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009214 File Offset: 0x00007414
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009224 File Offset: 0x00007424
		internal ImmutableStack<T> Reverse()
		{
			ImmutableStack<T> immutableStack = this.Clear();
			ImmutableStack<T> immutableStack2 = this;
			while (!immutableStack2.IsEmpty)
			{
				immutableStack = immutableStack.Push(immutableStack2.Peek());
				immutableStack2 = immutableStack2.Pop();
			}
			return immutableStack;
		}

		// Token: 0x04000040 RID: 64
		private static readonly ImmutableStack<T> s_EmptyField = new ImmutableStack<T>();

		// Token: 0x04000041 RID: 65
		private readonly T _head;

		// Token: 0x04000042 RID: 66
		private readonly ImmutableStack<T> _tail;

		// Token: 0x0200006D RID: 109
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x060005FD RID: 1533 RVA: 0x00010768 File Offset: 0x0000E968
			internal Enumerator(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
				this._remainingStack = null;
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060005FE RID: 1534 RVA: 0x00010783 File Offset: 0x0000E983
			public T Current
			{
				get
				{
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x000107AC File Offset: 0x0000E9AC
			public bool MoveNext()
			{
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x040000FF RID: 255
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x04000100 RID: 256
			private ImmutableStack<T> _remainingStack;
		}

		// Token: 0x0200006E RID: 110
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000600 RID: 1536 RVA: 0x000107FB File Offset: 0x0000E9FB
			internal EnumeratorObject(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x00010815 File Offset: 0x0000EA15
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000602 RID: 1538 RVA: 0x00010843 File Offset: 0x0000EA43
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x00010850 File Offset: 0x0000EA50
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x000108A5 File Offset: 0x0000EAA5
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingStack = null;
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x000108B4 File Offset: 0x0000EAB4
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x000108BD File Offset: 0x0000EABD
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableStack<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x04000101 RID: 257
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x04000102 RID: 258
			private ImmutableStack<T> _remainingStack;

			// Token: 0x04000103 RID: 259
			private bool _disposed;
		}
	}
}
