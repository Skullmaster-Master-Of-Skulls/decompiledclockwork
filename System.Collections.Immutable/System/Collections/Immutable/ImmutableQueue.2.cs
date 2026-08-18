using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200002A RID: 42
	[DebuggerDisplay("IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(ImmutableQueueDebuggerProxy<>))]
	public sealed class ImmutableQueue<T> : IImmutableQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000288 RID: 648 RVA: 0x000079A2 File Offset: 0x00005BA2
		private ImmutableQueue(ImmutableStack<T> forward, ImmutableStack<T> backward)
		{
			Requires.NotNull<ImmutableStack<T>>(forward, "forward");
			Requires.NotNull<ImmutableStack<T>>(backward, "backward");
			this._forwards = forward;
			this._backwards = backward;
			this._backwardsReversed = null;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000079D5 File Offset: 0x00005BD5
		public ImmutableQueue<T> Clear()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000079DC File Offset: 0x00005BDC
		public bool IsEmpty
		{
			get
			{
				return this._forwards.IsEmpty && this._backwards.IsEmpty;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000079F8 File Offset: 0x00005BF8
		public static ImmutableQueue<T> Empty
		{
			get
			{
				return ImmutableQueue<T>.s_EmptyField;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000079FF File Offset: 0x00005BFF
		IImmutableQueue<T> IImmutableQueue<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00007A07 File Offset: 0x00005C07
		private ImmutableStack<T> BackwardsReversed
		{
			get
			{
				if (this._backwardsReversed == null)
				{
					this._backwardsReversed = this._backwards.Reverse();
				}
				return this._backwardsReversed;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007A28 File Offset: 0x00005C28
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.Peek();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007A48 File Offset: 0x00005C48
		public ImmutableQueue<T> Enqueue(T value)
		{
			if (this.IsEmpty)
			{
				return new ImmutableQueue<T>(ImmutableStack<T>.Empty.Push(value), ImmutableStack<T>.Empty);
			}
			return new ImmutableQueue<T>(this._forwards, this._backwards.Push(value));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007A7F File Offset: 0x00005C7F
		IImmutableQueue<T> IImmutableQueue<!0>.Enqueue(T value)
		{
			return this.Enqueue(value);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007A88 File Offset: 0x00005C88
		public ImmutableQueue<T> Dequeue()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			ImmutableStack<T> immutableStack = this._forwards.Pop();
			if (!immutableStack.IsEmpty)
			{
				return new ImmutableQueue<T>(immutableStack, this._backwards);
			}
			if (this._backwards.IsEmpty)
			{
				return ImmutableQueue<T>.Empty;
			}
			return new ImmutableQueue<T>(this.BackwardsReversed, ImmutableStack<T>.Empty);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007AEC File Offset: 0x00005CEC
		public ImmutableQueue<T> Dequeue(out T value)
		{
			value = this.Peek();
			return this.Dequeue();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007B00 File Offset: 0x00005D00
		IImmutableQueue<T> IImmutableQueue<!0>.Dequeue()
		{
			return this.Dequeue();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007B08 File Offset: 0x00005D08
		public ImmutableQueue<T>.Enumerator GetEnumerator()
		{
			return new ImmutableQueue<T>.Enumerator(this);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007B10 File Offset: 0x00005D10
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007B10 File Offset: 0x00005D10
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x04000029 RID: 41
		private static readonly ImmutableQueue<T> s_EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);

		// Token: 0x0400002A RID: 42
		private readonly ImmutableStack<T> _backwards;

		// Token: 0x0400002B RID: 43
		private readonly ImmutableStack<T> _forwards;

		// Token: 0x0400002C RID: 44
		private ImmutableStack<T> _backwardsReversed;

		// Token: 0x02000063 RID: 99
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x0600052F RID: 1327 RVA: 0x0000E1D2 File Offset: 0x0000C3D2
			internal Enumerator(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
				this._remainingForwardsStack = null;
				this._remainingBackwardsStack = null;
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000E1EC File Offset: 0x0000C3EC
			public T Current
			{
				get
				{
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06000531 RID: 1329 RVA: 0x0000E240 File Offset: 0x0000C440
			public bool MoveNext()
			{
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x040000CC RID: 204
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x040000CD RID: 205
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x040000CE RID: 206
			private ImmutableStack<T> _remainingBackwardsStack;
		}

		// Token: 0x02000064 RID: 100
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000532 RID: 1330 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
			internal EnumeratorObject(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000E33C File Offset: 0x0000C53C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000535 RID: 1333 RVA: 0x0000E34C File Offset: 0x0000C54C
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x06000536 RID: 1334 RVA: 0x0000E3E6 File Offset: 0x0000C5E6
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingBackwardsStack = null;
				this._remainingForwardsStack = null;
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x0000E3FC File Offset: 0x0000C5FC
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x0000E405 File Offset: 0x0000C605
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableQueue<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x040000CF RID: 207
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x040000D0 RID: 208
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x040000D1 RID: 209
			private ImmutableStack<T> _remainingBackwardsStack;

			// Token: 0x040000D2 RID: 210
			private bool _disposed;
		}
	}
}
