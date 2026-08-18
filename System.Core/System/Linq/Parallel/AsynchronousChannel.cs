using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000175 RID: 373
	internal sealed class AsynchronousChannel<T> : IDisposable
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00030C75 File Offset: 0x0002EE75
		internal AsynchronousChannel(int index, int chunkSize, CancellationToken cancellationToken, IntValueEvent consumerEvent) : this(index, 512, chunkSize, cancellationToken, consumerEvent)
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00030C88 File Offset: 0x0002EE88
		internal AsynchronousChannel(int index, int capacity, int chunkSize, CancellationToken cancellationToken, IntValueEvent consumerEvent)
		{
			if (chunkSize == 0)
			{
				chunkSize = Scheduling.GetDefaultChunkSize<T>();
			}
			this.m_index = index;
			this.m_buffer = new T[capacity + 1][];
			this.m_producerBufferIndex = 0;
			this.m_consumerBufferIndex = 0;
			this.m_producerEvent = new ManualResetEventSlim();
			this.m_consumerEvent = consumerEvent;
			this.m_chunkSize = chunkSize;
			this.m_producerChunk = new T[chunkSize];
			this.m_producerChunkIndex = 0;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00030D04 File Offset: 0x0002EF04
		internal bool IsFull
		{
			get
			{
				int producerBufferIndex = this.m_producerBufferIndex;
				int consumerBufferIndex = this.m_consumerBufferIndex;
				return producerBufferIndex == consumerBufferIndex - 1 || (consumerBufferIndex == 0 && producerBufferIndex == this.m_buffer.Length - 1);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00030D3D File Offset: 0x0002EF3D
		internal bool IsChunkBufferEmpty
		{
			get
			{
				return this.m_producerBufferIndex == this.m_consumerBufferIndex;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00030D51 File Offset: 0x0002EF51
		internal bool IsDone
		{
			get
			{
				return this.m_done;
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00030D5B File Offset: 0x0002EF5B
		internal void FlushBuffers()
		{
			this.FlushCachedChunk();
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00030D64 File Offset: 0x0002EF64
		internal void SetDone()
		{
			this.m_done = true;
			lock (this)
			{
				if (this.m_consumerEvent != null)
				{
					this.m_consumerEvent.Set(this.m_index);
				}
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00030DBC File Offset: 0x0002EFBC
		internal void Enqueue(T item)
		{
			int producerChunkIndex = this.m_producerChunkIndex;
			this.m_producerChunk[producerChunkIndex] = item;
			if (producerChunkIndex == this.m_chunkSize - 1)
			{
				this.EnqueueChunk(this.m_producerChunk);
				this.m_producerChunk = new T[this.m_chunkSize];
			}
			this.m_producerChunkIndex = (producerChunkIndex + 1) % this.m_chunkSize;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00030E18 File Offset: 0x0002F018
		private void EnqueueChunk(T[] chunk)
		{
			if (this.IsFull)
			{
				this.WaitUntilNonFull();
			}
			int producerBufferIndex = this.m_producerBufferIndex;
			this.m_buffer[producerBufferIndex] = chunk;
			Interlocked.Exchange(ref this.m_producerBufferIndex, (producerBufferIndex + 1) % this.m_buffer.Length);
			if (this.m_consumerIsWaiting == 1 && !this.IsChunkBufferEmpty)
			{
				this.m_consumerIsWaiting = 0;
				this.m_consumerEvent.Set(this.m_index);
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00030E8C File Offset: 0x0002F08C
		private void WaitUntilNonFull()
		{
			do
			{
				this.m_producerEvent.Reset();
				Interlocked.Exchange(ref this.m_producerIsWaiting, 1);
				if (this.IsFull)
				{
					this.m_producerEvent.Wait(this.m_cancellationToken);
				}
				else
				{
					this.m_producerIsWaiting = 0;
				}
			}
			while (this.IsFull);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00030EE0 File Offset: 0x0002F0E0
		private void FlushCachedChunk()
		{
			if (this.m_producerChunk != null && this.m_producerChunkIndex != 0)
			{
				T[] array = new T[this.m_producerChunkIndex];
				Array.Copy(this.m_producerChunk, array, this.m_producerChunkIndex);
				this.EnqueueChunk(array);
				this.m_producerChunk = null;
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00030F2C File Offset: 0x0002F12C
		internal bool TryDequeue(ref T item)
		{
			if (this.m_consumerChunk == null)
			{
				if (!this.TryDequeueChunk(ref this.m_consumerChunk))
				{
					return false;
				}
				this.m_consumerChunkIndex = 0;
			}
			item = this.m_consumerChunk[this.m_consumerChunkIndex];
			this.m_consumerChunkIndex++;
			if (this.m_consumerChunkIndex == this.m_consumerChunk.Length)
			{
				this.m_consumerChunk = null;
			}
			return true;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00030F95 File Offset: 0x0002F195
		private bool TryDequeueChunk(ref T[] chunk)
		{
			if (this.IsChunkBufferEmpty)
			{
				return false;
			}
			chunk = this.InternalDequeueChunk();
			return true;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00030FAC File Offset: 0x0002F1AC
		internal bool TryDequeue(ref T item, ref bool isDone)
		{
			isDone = false;
			if (this.m_consumerChunk == null)
			{
				if (!this.TryDequeueChunk(ref this.m_consumerChunk, ref isDone))
				{
					return false;
				}
				this.m_consumerChunkIndex = 0;
			}
			item = this.m_consumerChunk[this.m_consumerChunkIndex];
			this.m_consumerChunkIndex++;
			if (this.m_consumerChunkIndex == this.m_consumerChunk.Length)
			{
				this.m_consumerChunk = null;
			}
			return true;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003101C File Offset: 0x0002F21C
		private bool TryDequeueChunk(ref T[] chunk, ref bool isDone)
		{
			isDone = false;
			while (this.IsChunkBufferEmpty)
			{
				if (this.IsDone && this.IsChunkBufferEmpty)
				{
					isDone = true;
					return false;
				}
				Interlocked.Exchange(ref this.m_consumerIsWaiting, 1);
				if (this.IsChunkBufferEmpty && !this.IsDone)
				{
					return false;
				}
				this.m_consumerIsWaiting = 0;
			}
			chunk = this.InternalDequeueChunk();
			return true;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003107C File Offset: 0x0002F27C
		private T[] InternalDequeueChunk()
		{
			int consumerBufferIndex = this.m_consumerBufferIndex;
			T[] result = this.m_buffer[consumerBufferIndex];
			this.m_buffer[consumerBufferIndex] = null;
			Interlocked.Exchange(ref this.m_consumerBufferIndex, (consumerBufferIndex + 1) % this.m_buffer.Length);
			if (this.m_producerIsWaiting == 1 && !this.IsFull)
			{
				this.m_producerIsWaiting = 0;
				this.m_producerEvent.Set();
			}
			return result;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000310E4 File Offset: 0x0002F2E4
		internal void DoneWithDequeueWait()
		{
			this.m_consumerIsWaiting = 0;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000310F0 File Offset: 0x0002F2F0
		public void Dispose()
		{
			lock (this)
			{
				this.m_producerEvent.Dispose();
				this.m_producerEvent = null;
				this.m_consumerEvent = null;
			}
		}

		// Token: 0x04000802 RID: 2050
		private T[][] m_buffer;

		// Token: 0x04000803 RID: 2051
		private readonly int m_index;

		// Token: 0x04000804 RID: 2052
		private volatile int m_producerBufferIndex;

		// Token: 0x04000805 RID: 2053
		private volatile int m_consumerBufferIndex;

		// Token: 0x04000806 RID: 2054
		private volatile bool m_done;

		// Token: 0x04000807 RID: 2055
		private T[] m_producerChunk;

		// Token: 0x04000808 RID: 2056
		private int m_producerChunkIndex;

		// Token: 0x04000809 RID: 2057
		private T[] m_consumerChunk;

		// Token: 0x0400080A RID: 2058
		private int m_consumerChunkIndex;

		// Token: 0x0400080B RID: 2059
		private int m_chunkSize;

		// Token: 0x0400080C RID: 2060
		private ManualResetEventSlim m_producerEvent;

		// Token: 0x0400080D RID: 2061
		private IntValueEvent m_consumerEvent;

		// Token: 0x0400080E RID: 2062
		private volatile int m_producerIsWaiting;

		// Token: 0x0400080F RID: 2063
		private volatile int m_consumerIsWaiting;

		// Token: 0x04000810 RID: 2064
		private CancellationToken m_cancellationToken;
	}
}
