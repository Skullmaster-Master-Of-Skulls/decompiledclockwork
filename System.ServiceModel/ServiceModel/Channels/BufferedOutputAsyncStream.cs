using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000773 RID: 1907
	internal class BufferedOutputAsyncStream : Stream
	{
		// Token: 0x060048C5 RID: 18629 RVA: 0x0010CAF8 File Offset: 0x0010ACF8
		internal BufferedOutputAsyncStream(Stream stream, int bufferSize, int bufferLimit)
		{
			this.stream = stream;
			this.bufferSize = bufferSize;
			this.bufferLimit = bufferLimit;
			this.buffers = new BufferedOutputAsyncStream.BufferQueue(this.bufferLimit);
			this.buffers.Add(new BufferedOutputAsyncStream.ByteBuffer(this, this.bufferSize, stream));
			this.availableBufferCount = 1;
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x0010CB50 File Offset: 0x0010AD50
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x060048C7 RID: 18631 RVA: 0x0010CB53 File Offset: 0x0010AD53
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x0010CB56 File Offset: 0x0010AD56
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite && !this.closed;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x0010CB70 File Offset: 0x0010AD70
		public override long Length
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ReadNotSupported")));
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x0010CB8B File Offset: 0x0010AD8B
		// (set) Token: 0x060048CB RID: 18635 RVA: 0x0010CBA6 File Offset: 0x0010ADA6
		public override long Position
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x0010CBC1 File Offset: 0x0010ADC1
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				if (TD.BufferedAsyncWriteStartIsEnabled() && this.activity == null)
				{
					this.activity = EventTraceActivity.GetFromThreadOrCreate(false);
				}
				return this.activity;
			}
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x0010CBE4 File Offset: 0x0010ADE4
		private BufferedOutputAsyncStream.ByteBuffer GetCurrentBuffer()
		{
			this.ThrowOnException();
			if (this.currentByteBuffer == null)
			{
				this.currentByteBuffer = this.buffers.CurrentBuffer();
			}
			return this.currentByteBuffer;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0010CC0C File Offset: 0x0010AE0C
		public override void Close()
		{
			try
			{
				if (!this.closed)
				{
					this.FlushPendingBuffer();
					this.stream.Close();
					this.WaitForAllWritesToComplete();
				}
			}
			finally
			{
				this.closed = true;
			}
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0010CC54 File Offset: 0x0010AE54
		public override void Flush()
		{
			this.FlushPendingBuffer();
			this.stream.Flush();
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0010CC68 File Offset: 0x0010AE68
		private void FlushPendingBuffer()
		{
			BufferedOutputAsyncStream.ByteBuffer byteBuffer = this.buffers.CurrentBuffer();
			if (byteBuffer != null)
			{
				this.DequeueAndFlush(byteBuffer, BufferedOutputAsyncStream.onFlushComplete);
			}
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0010CC90 File Offset: 0x0010AE90
		private void IncrementAsyncWriteCount()
		{
			if (Interlocked.Increment(ref this.asyncWriteCount) > 1)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WriterAsyncWritePending")));
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x0010CCBA File Offset: 0x0010AEBA
		private void DecrementAsyncWriteCount()
		{
			if (Interlocked.Decrement(ref this.asyncWriteCount) != 0)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("NoAsyncWritePending")));
			}
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x0010CCE3 File Offset: 0x0010AEE3
		private void EnsureNoAsyncWritePending()
		{
			if (this.asyncWriteCount != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WriterAsyncWritePending")));
			}
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x0010CD07 File Offset: 0x0010AF07
		private void EnsureOpened()
		{
			if (this.closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StreamClosed")));
			}
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x0010CD2B File Offset: 0x0010AF2B
		private BufferedOutputAsyncStream.ByteBuffer NextBuffer()
		{
			if (!this.AdjustBufferSize())
			{
				this.buffers.WaitForAny();
			}
			return this.GetCurrentBuffer();
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x0010CD46 File Offset: 0x0010AF46
		private bool AdjustBufferSize()
		{
			if (this.availableBufferCount < this.bufferLimit)
			{
				this.buffers.Add(new BufferedOutputAsyncStream.ByteBuffer(this, this.bufferSize, this.stream));
				this.availableBufferCount++;
				return true;
			}
			return false;
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x0010CD84 File Offset: 0x0010AF84
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ReadNotSupported")));
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x0010CD9F File Offset: 0x0010AF9F
		public override int ReadByte()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ReadNotSupported")));
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0010CDBA File Offset: 0x0010AFBA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x0010CDD5 File Offset: 0x0010AFD5
		public override void SetLength(long value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0010CDF0 File Offset: 0x0010AFF0
		private void WaitForAllWritesToComplete()
		{
			this.buffers.WaitForAllWritesToComplete();
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0010CE00 File Offset: 0x0010B000
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.EnsureOpened();
			this.EnsureNoAsyncWritePending();
			while (count > 0)
			{
				BufferedOutputAsyncStream.ByteBuffer byteBuffer = this.GetCurrentBuffer();
				if (byteBuffer == null)
				{
					byteBuffer = this.NextBuffer();
				}
				int num = byteBuffer.FreeBytes;
				if (num > 0)
				{
					if (num > count)
					{
						num = count;
					}
					byteBuffer.CopyData(buffer, offset, num);
					offset += num;
					count -= num;
				}
				if (byteBuffer.FreeBytes == 0)
				{
					this.DequeueAndFlush(byteBuffer, BufferedOutputAsyncStream.onFlushComplete);
				}
			}
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0010CE68 File Offset: 0x0010B068
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.EnsureOpened();
			this.IncrementAsyncWriteCount();
			if (BufferedOutputAsyncStream.onWriteCallback == null)
			{
				BufferedOutputAsyncStream.onWriteCallback = new AsyncEventArgsCallback(BufferedOutputAsyncStream.OnWriteCallback);
				BufferedOutputAsyncStream.onAsyncFlushComplete = new AsyncEventArgsCallback(BufferedOutputAsyncStream.OnAsyncFlushComplete);
			}
			if (this.writeState == null)
			{
				this.writeState = new BufferedOutputAsyncStream.WriteAsyncState();
				this.writeArgs = new BufferedOutputAsyncStream.WriteAsyncArgs();
			}
			else
			{
				this.ThrowOnException();
			}
			this.writeArgs.Set(buffer, offset, count, callback, state);
			this.writeState.Set(BufferedOutputAsyncStream.onWriteCallback, this.writeArgs, this);
			if (this.WriteAsync(this.writeState) == AsyncCompletionResult.Completed)
			{
				this.writeState.Complete(true);
				if (callback != null)
				{
					callback(this.writeState.CompletedSynchronouslyAsyncResult);
				}
				return this.writeState.CompletedSynchronouslyAsyncResult;
			}
			return this.writeState.PendingAsyncResult;
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x0010CF41 File Offset: 0x0010B141
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.DecrementAsyncWriteCount();
			this.ThrowOnException();
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x0010CF50 File Offset: 0x0010B150
		public override void WriteByte(byte value)
		{
			this.EnsureNoAsyncWritePending();
			BufferedOutputAsyncStream.ByteBuffer byteBuffer = this.GetCurrentBuffer();
			if (byteBuffer == null)
			{
				byteBuffer = this.NextBuffer();
			}
			byteBuffer.CopyData(value);
			if (byteBuffer.FreeBytes == 0)
			{
				this.DequeueAndFlush(byteBuffer, BufferedOutputAsyncStream.onFlushComplete);
			}
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x0010CF90 File Offset: 0x0010B190
		private void DequeueAndFlush(BufferedOutputAsyncStream.ByteBuffer currentBuffer, AsyncEventArgsCallback callback)
		{
			this.currentByteBuffer = null;
			BufferedOutputAsyncStream.ByteBuffer byteBuffer = this.buffers.Dequeue();
			BufferedOutputAsyncStream.WriteFlushAsyncEventArgs writeFlushAsyncEventArgs = (BufferedOutputAsyncStream.WriteFlushAsyncEventArgs)currentBuffer.FlushAsyncArgs;
			if (writeFlushAsyncEventArgs == null)
			{
				writeFlushAsyncEventArgs = new BufferedOutputAsyncStream.WriteFlushAsyncEventArgs();
				currentBuffer.FlushAsyncArgs = writeFlushAsyncEventArgs;
			}
			writeFlushAsyncEventArgs.Set(callback, null, this);
			if (currentBuffer.FlushAsync() == AsyncCompletionResult.Completed)
			{
				this.buffers.Enqueue(currentBuffer);
				writeFlushAsyncEventArgs.Complete(true);
			}
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0010CFF4 File Offset: 0x0010B1F4
		private static void OnFlushComplete(IAsyncEventArgs state)
		{
			BufferedOutputAsyncStream bufferedOutputAsyncStream = (BufferedOutputAsyncStream)state.AsyncState;
			BufferedOutputAsyncStream.WriteFlushAsyncEventArgs writeFlushAsyncEventArgs = (BufferedOutputAsyncStream.WriteFlushAsyncEventArgs)state;
			BufferedOutputAsyncStream.ByteBuffer result = writeFlushAsyncEventArgs.Result;
			bufferedOutputAsyncStream.buffers.Enqueue(result);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0010D028 File Offset: 0x0010B228
		private AsyncCompletionResult WriteAsync(BufferedOutputAsyncStream.WriteAsyncState state)
		{
			if (state.Arguments.Count == 0)
			{
				return AsyncCompletionResult.Completed;
			}
			byte[] buffer = state.Arguments.Buffer;
			int num = state.Arguments.Offset;
			int i = state.Arguments.Count;
			BufferedOutputAsyncStream.ByteBuffer currentBuffer = this.GetCurrentBuffer();
			while (i > 0)
			{
				if (currentBuffer == null)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WriteAsyncWithoutFreeBuffer")));
				}
				int num2 = currentBuffer.FreeBytes;
				if (num2 > 0)
				{
					if (num2 > i)
					{
						num2 = i;
					}
					currentBuffer.CopyData(buffer, num, num2);
					num += num2;
					i -= num2;
				}
				if (currentBuffer.FreeBytes == 0)
				{
					this.DequeueAndFlush(currentBuffer, BufferedOutputAsyncStream.onAsyncFlushComplete);
					if (i > 0 || this.buffers.Count == 0)
					{
						this.AdjustBufferSize();
					}
				}
				state.Arguments.Offset = num;
				state.Arguments.Count = i;
				currentBuffer = this.GetCurrentBuffer();
				if (currentBuffer == null)
				{
					if (this.buffers.TryUnlock())
					{
						return AsyncCompletionResult.Queued;
					}
					currentBuffer = this.GetCurrentBuffer();
				}
			}
			return AsyncCompletionResult.Completed;
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x0010D128 File Offset: 0x0010B328
		private static void OnAsyncFlushComplete(IAsyncEventArgs state)
		{
			BufferedOutputAsyncStream bufferedOutputAsyncStream = (BufferedOutputAsyncStream)state.AsyncState;
			Exception ex = null;
			bool flag = false;
			try
			{
				BufferedOutputAsyncStream.OnFlushComplete(state);
				if (bufferedOutputAsyncStream.buffers.TryAcquireLock())
				{
					BufferedOutputAsyncStream.WriteFlushAsyncEventArgs writeFlushAsyncEventArgs = (BufferedOutputAsyncStream.WriteFlushAsyncEventArgs)state;
					if (writeFlushAsyncEventArgs.Exception != null)
					{
						flag = true;
						ex = writeFlushAsyncEventArgs.Exception;
					}
					else if (bufferedOutputAsyncStream.WriteAsync(bufferedOutputAsyncStream.writeState) == AsyncCompletionResult.Completed)
					{
						flag = true;
					}
				}
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				if (ex == null)
				{
					ex = ex2;
				}
				flag = true;
			}
			if (flag)
			{
				bufferedOutputAsyncStream.writeState.Complete(false, ex);
			}
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x0010D1BC File Offset: 0x0010B3BC
		private static void OnWriteCallback(IAsyncEventArgs state)
		{
			BufferedOutputAsyncStream bufferedOutputAsyncStream = (BufferedOutputAsyncStream)state.AsyncState;
			IAsyncResult pendingAsyncResult = bufferedOutputAsyncStream.writeState.PendingAsyncResult;
			AsyncCallback callback = bufferedOutputAsyncStream.writeState.Arguments.Callback;
			bufferedOutputAsyncStream.writeState.Arguments.Callback = null;
			if (callback != null)
			{
				callback(pendingAsyncResult);
			}
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0010D20D File Offset: 0x0010B40D
		private void ThrowOnException()
		{
			this.buffers.ThrowOnException();
			if (this.writeState != null)
			{
				this.writeState.ThrowOnException();
			}
		}

		// Token: 0x04002DFE RID: 11774
		private readonly Stream stream;

		// Token: 0x04002DFF RID: 11775
		private readonly int bufferSize;

		// Token: 0x04002E00 RID: 11776
		private readonly int bufferLimit;

		// Token: 0x04002E01 RID: 11777
		private readonly BufferedOutputAsyncStream.BufferQueue buffers;

		// Token: 0x04002E02 RID: 11778
		private BufferedOutputAsyncStream.ByteBuffer currentByteBuffer;

		// Token: 0x04002E03 RID: 11779
		private int availableBufferCount;

		// Token: 0x04002E04 RID: 11780
		private static AsyncEventArgsCallback onFlushComplete = new AsyncEventArgsCallback(BufferedOutputAsyncStream.OnFlushComplete);

		// Token: 0x04002E05 RID: 11781
		private int asyncWriteCount;

		// Token: 0x04002E06 RID: 11782
		private BufferedOutputAsyncStream.WriteAsyncState writeState;

		// Token: 0x04002E07 RID: 11783
		private BufferedOutputAsyncStream.WriteAsyncArgs writeArgs;

		// Token: 0x04002E08 RID: 11784
		private static AsyncEventArgsCallback onAsyncFlushComplete;

		// Token: 0x04002E09 RID: 11785
		private static AsyncEventArgsCallback onWriteCallback;

		// Token: 0x04002E0A RID: 11786
		private EventTraceActivity activity;

		// Token: 0x04002E0B RID: 11787
		private bool closed;

		// Token: 0x02000CE8 RID: 3304
		private class BufferQueue
		{
			// Token: 0x06007A3C RID: 31292 RVA: 0x001C7880 File Offset: 0x001C5A80
			internal BufferQueue(int queueSize)
			{
				this.head = 0;
				this.count = 0;
				this.size = queueSize;
				this.buffers = new BufferedOutputAsyncStream.BufferQueue.Slot[this.size];
				this.refBufferList = new List<BufferedOutputAsyncStream.ByteBuffer>();
				for (int i = 0; i < queueSize; i++)
				{
					BufferedOutputAsyncStream.BufferQueue.Slot slot = new BufferedOutputAsyncStream.BufferQueue.Slot();
					slot.checkedOut = true;
					this.buffers[i] = slot;
				}
			}

			// Token: 0x17001BA8 RID: 7080
			// (get) Token: 0x06007A3D RID: 31293 RVA: 0x001C78E6 File Offset: 0x001C5AE6
			private object ThisLock
			{
				get
				{
					return this.buffers;
				}
			}

			// Token: 0x17001BA9 RID: 7081
			// (get) Token: 0x06007A3E RID: 31294 RVA: 0x001C78F0 File Offset: 0x001C5AF0
			internal int Count
			{
				get
				{
					object thisLock = this.ThisLock;
					int result;
					lock (thisLock)
					{
						result = this.count;
					}
					return result;
				}
			}

			// Token: 0x06007A3F RID: 31295 RVA: 0x001C7934 File Offset: 0x001C5B34
			internal BufferedOutputAsyncStream.ByteBuffer Dequeue()
			{
				object thisLock = this.ThisLock;
				BufferedOutputAsyncStream.ByteBuffer result;
				lock (thisLock)
				{
					if (this.count == 0)
					{
						result = null;
					}
					else
					{
						BufferedOutputAsyncStream.BufferQueue.Slot slot = this.buffers[this.head];
						this.head = (this.head + 1) % this.size;
						this.count--;
						BufferedOutputAsyncStream.ByteBuffer buffer = slot.buffer;
						slot.buffer = null;
						slot.checkedOut = true;
						result = buffer;
					}
				}
				return result;
			}

			// Token: 0x06007A40 RID: 31296 RVA: 0x001C79C8 File Offset: 0x001C5BC8
			internal void Add(BufferedOutputAsyncStream.ByteBuffer buffer)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.refBufferList.Count < this.size)
					{
						this.refBufferList.Add(buffer);
						this.Enqueue(buffer);
					}
				}
			}

			// Token: 0x06007A41 RID: 31297 RVA: 0x001C7A28 File Offset: 0x001C5C28
			internal void Enqueue(BufferedOutputAsyncStream.ByteBuffer buffer)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.completionException = (this.completionException ?? buffer.CompletionException);
					int num = (this.head + this.count) % this.size;
					BufferedOutputAsyncStream.BufferQueue.Slot slot = this.buffers[num];
					this.count++;
					slot.checkedOut = false;
					slot.buffer = buffer;
					if (this.waiting)
					{
						Monitor.Pulse(this.ThisLock);
					}
				}
			}

			// Token: 0x06007A42 RID: 31298 RVA: 0x001C7AC8 File Offset: 0x001C5CC8
			internal BufferedOutputAsyncStream.ByteBuffer CurrentBuffer()
			{
				object thisLock = this.ThisLock;
				BufferedOutputAsyncStream.ByteBuffer buffer;
				lock (thisLock)
				{
					this.ThrowOnException();
					BufferedOutputAsyncStream.BufferQueue.Slot slot = this.buffers[this.head];
					buffer = slot.buffer;
				}
				return buffer;
			}

			// Token: 0x06007A43 RID: 31299 RVA: 0x001C7B20 File Offset: 0x001C5D20
			internal void WaitForAllWritesToComplete()
			{
				for (int i = 0; i < this.refBufferList.Count; i++)
				{
					this.refBufferList[i].WaitForWriteComplete();
				}
			}

			// Token: 0x06007A44 RID: 31300 RVA: 0x001C7B54 File Offset: 0x001C5D54
			internal void WaitForAny()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.count == 0)
					{
						this.waiting = true;
						Monitor.Wait(this.ThisLock);
						this.waiting = false;
					}
				}
				this.ThrowOnException();
			}

			// Token: 0x06007A45 RID: 31301 RVA: 0x001C7BB8 File Offset: 0x001C5DB8
			internal void ThrowOnException()
			{
				if (this.completionException != null)
				{
					throw FxTrace.Exception.AsError(this.completionException);
				}
			}

			// Token: 0x06007A46 RID: 31302 RVA: 0x001C7BD4 File Offset: 0x001C5DD4
			internal bool TryUnlock()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.count == 0)
					{
						this.pendingCompletion = true;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06007A47 RID: 31303 RVA: 0x001C7C24 File Offset: 0x001C5E24
			internal bool TryAcquireLock()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.pendingCompletion && this.count > 0)
					{
						this.pendingCompletion = false;
						return true;
					}
				}
				return false;
			}

			// Token: 0x040045E8 RID: 17896
			private readonly List<BufferedOutputAsyncStream.ByteBuffer> refBufferList;

			// Token: 0x040045E9 RID: 17897
			private readonly int size;

			// Token: 0x040045EA RID: 17898
			private readonly BufferedOutputAsyncStream.BufferQueue.Slot[] buffers;

			// Token: 0x040045EB RID: 17899
			private Exception completionException;

			// Token: 0x040045EC RID: 17900
			private int head;

			// Token: 0x040045ED RID: 17901
			private int count;

			// Token: 0x040045EE RID: 17902
			private bool waiting;

			// Token: 0x040045EF RID: 17903
			private bool pendingCompletion;

			// Token: 0x02000F40 RID: 3904
			private class Slot
			{
				// Token: 0x04004E44 RID: 20036
				internal bool checkedOut;

				// Token: 0x04004E45 RID: 20037
				internal BufferedOutputAsyncStream.ByteBuffer buffer;
			}
		}

		// Token: 0x02000CE9 RID: 3305
		private class WriteFlushAsyncEventArgs : AsyncEventArgs<object, BufferedOutputAsyncStream.ByteBuffer>
		{
		}

		// Token: 0x02000CEA RID: 3306
		private class ByteBuffer
		{
			// Token: 0x06007A49 RID: 31305 RVA: 0x001C7C88 File Offset: 0x001C5E88
			internal ByteBuffer(BufferedOutputAsyncStream parent, int bufferSize, Stream stream)
			{
				this.waiting = false;
				this.writePending = false;
				this.position = 0;
				this.bytes = DiagnosticUtility.Utility.AllocateByteArray(bufferSize);
				this.stream = stream;
				this.parent = parent;
			}

			// Token: 0x17001BAA RID: 7082
			// (get) Token: 0x06007A4A RID: 31306 RVA: 0x001C7CC4 File Offset: 0x001C5EC4
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001BAB RID: 7083
			// (get) Token: 0x06007A4B RID: 31307 RVA: 0x001C7CC7 File Offset: 0x001C5EC7
			internal Exception CompletionException
			{
				get
				{
					return this.completionException;
				}
			}

			// Token: 0x17001BAC RID: 7084
			// (get) Token: 0x06007A4C RID: 31308 RVA: 0x001C7CCF File Offset: 0x001C5ECF
			internal int FreeBytes
			{
				get
				{
					return this.bytes.Length - this.position;
				}
			}

			// Token: 0x17001BAD RID: 7085
			// (get) Token: 0x06007A4D RID: 31309 RVA: 0x001C7CE0 File Offset: 0x001C5EE0
			// (set) Token: 0x06007A4E RID: 31310 RVA: 0x001C7CE8 File Offset: 0x001C5EE8
			internal AsyncEventArgs<object, BufferedOutputAsyncStream.ByteBuffer> FlushAsyncArgs { get; set; }

			// Token: 0x06007A4F RID: 31311 RVA: 0x001C7CF4 File Offset: 0x001C5EF4
			private static void WriteCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				BufferedOutputAsyncStream.ByteBuffer byteBuffer = (BufferedOutputAsyncStream.ByteBuffer)result.AsyncState;
				try
				{
					if (TD.BufferedAsyncWriteStopIsEnabled())
					{
						TD.BufferedAsyncWriteStop(byteBuffer.parent.EventTraceActivity);
					}
					byteBuffer.stream.EndWrite(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					byteBuffer.completionException = exception;
				}
				object thisLock = byteBuffer.ThisLock;
				lock (thisLock)
				{
					byteBuffer.writePending = false;
					if (byteBuffer.waiting)
					{
						Monitor.Pulse(byteBuffer.ThisLock);
					}
				}
			}

			// Token: 0x06007A50 RID: 31312 RVA: 0x001C7DA4 File Offset: 0x001C5FA4
			internal void WaitForWriteComplete()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.writePending)
					{
						this.waiting = true;
						Monitor.Wait(this.ThisLock);
						this.waiting = false;
					}
				}
				if (this.completionException != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.completionException);
				}
			}

			// Token: 0x06007A51 RID: 31313 RVA: 0x001C7E1C File Offset: 0x001C601C
			internal void CopyData(byte[] buffer, int offset, int count)
			{
				Buffer.BlockCopy(buffer, offset, this.bytes, this.position, count);
				this.position += count;
			}

			// Token: 0x06007A52 RID: 31314 RVA: 0x001C7E40 File Offset: 0x001C6040
			internal void CopyData(byte value)
			{
				byte[] array = this.bytes;
				int num = this.position;
				this.position = num + 1;
				array[num] = value;
			}

			// Token: 0x06007A53 RID: 31315 RVA: 0x001C7E68 File Offset: 0x001C6068
			internal AsyncCompletionResult FlushAsync()
			{
				if (this.position <= 0)
				{
					return AsyncCompletionResult.Completed;
				}
				if (BufferedOutputAsyncStream.ByteBuffer.flushCallback == null)
				{
					BufferedOutputAsyncStream.ByteBuffer.flushCallback = new AsyncCallback(BufferedOutputAsyncStream.ByteBuffer.OnAsyncFlush);
				}
				int num = this.position;
				this.SetWritePending();
				this.position = 0;
				if (TD.BufferedAsyncWriteStartIsEnabled())
				{
					TD.BufferedAsyncWriteStart(this.parent.EventTraceActivity, this.GetHashCode(), num);
				}
				IAsyncResult asyncResult = this.stream.BeginWrite(this.bytes, 0, num, BufferedOutputAsyncStream.ByteBuffer.flushCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					if (TD.BufferedAsyncWriteStopIsEnabled())
					{
						TD.BufferedAsyncWriteStop(this.parent.EventTraceActivity);
					}
					this.stream.EndWrite(asyncResult);
					this.ResetWritePending();
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x06007A54 RID: 31316 RVA: 0x001C7F18 File Offset: 0x001C6118
			private static void OnAsyncFlush(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				BufferedOutputAsyncStream.ByteBuffer byteBuffer = (BufferedOutputAsyncStream.ByteBuffer)result.AsyncState;
				AsyncEventArgs<object, BufferedOutputAsyncStream.ByteBuffer> flushAsyncArgs = byteBuffer.FlushAsyncArgs;
				try
				{
					BufferedOutputAsyncStream.ByteBuffer.WriteCallback(result);
					flushAsyncArgs.Result = byteBuffer;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (byteBuffer.completionException == null)
					{
						byteBuffer.completionException = exception;
					}
				}
				flushAsyncArgs.Complete(false, byteBuffer.completionException);
			}

			// Token: 0x06007A55 RID: 31317 RVA: 0x001C7F8C File Offset: 0x001C618C
			private void ResetWritePending()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.writePending = false;
				}
			}

			// Token: 0x06007A56 RID: 31318 RVA: 0x001C7FD0 File Offset: 0x001C61D0
			private void SetWritePending()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.writePending)
					{
						throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("FlushBufferAlreadyInUse")));
					}
					this.writePending = true;
				}
			}

			// Token: 0x040045F0 RID: 17904
			private byte[] bytes;

			// Token: 0x040045F1 RID: 17905
			private int position;

			// Token: 0x040045F2 RID: 17906
			private Stream stream;

			// Token: 0x040045F3 RID: 17907
			private bool writePending;

			// Token: 0x040045F4 RID: 17908
			private bool waiting;

			// Token: 0x040045F5 RID: 17909
			private Exception completionException;

			// Token: 0x040045F6 RID: 17910
			private BufferedOutputAsyncStream parent;

			// Token: 0x040045F7 RID: 17911
			private static AsyncCallback writeCallback = Fx.ThunkCallback(new AsyncCallback(BufferedOutputAsyncStream.ByteBuffer.WriteCallback));

			// Token: 0x040045F8 RID: 17912
			private static AsyncCallback flushCallback;
		}

		// Token: 0x02000CEB RID: 3307
		private class WriteAsyncArgs
		{
			// Token: 0x17001BAE RID: 7086
			// (get) Token: 0x06007A58 RID: 31320 RVA: 0x001C804C File Offset: 0x001C624C
			// (set) Token: 0x06007A59 RID: 31321 RVA: 0x001C8054 File Offset: 0x001C6254
			internal byte[] Buffer { get; set; }

			// Token: 0x17001BAF RID: 7087
			// (get) Token: 0x06007A5A RID: 31322 RVA: 0x001C805D File Offset: 0x001C625D
			// (set) Token: 0x06007A5B RID: 31323 RVA: 0x001C8065 File Offset: 0x001C6265
			internal int Offset { get; set; }

			// Token: 0x17001BB0 RID: 7088
			// (get) Token: 0x06007A5C RID: 31324 RVA: 0x001C806E File Offset: 0x001C626E
			// (set) Token: 0x06007A5D RID: 31325 RVA: 0x001C8076 File Offset: 0x001C6276
			internal int Count { get; set; }

			// Token: 0x17001BB1 RID: 7089
			// (get) Token: 0x06007A5E RID: 31326 RVA: 0x001C807F File Offset: 0x001C627F
			// (set) Token: 0x06007A5F RID: 31327 RVA: 0x001C8087 File Offset: 0x001C6287
			internal AsyncCallback Callback { get; set; }

			// Token: 0x17001BB2 RID: 7090
			// (get) Token: 0x06007A60 RID: 31328 RVA: 0x001C8090 File Offset: 0x001C6290
			// (set) Token: 0x06007A61 RID: 31329 RVA: 0x001C8098 File Offset: 0x001C6298
			internal object AsyncState { get; set; }

			// Token: 0x06007A62 RID: 31330 RVA: 0x001C80A1 File Offset: 0x001C62A1
			internal void Set(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
				this.Callback = callback;
				this.AsyncState = state;
			}
		}

		// Token: 0x02000CEC RID: 3308
		private class WriteAsyncState : AsyncEventArgs<BufferedOutputAsyncStream.WriteAsyncArgs, BufferedOutputAsyncStream>
		{
			// Token: 0x17001BB3 RID: 7091
			// (get) Token: 0x06007A64 RID: 31332 RVA: 0x001C80D0 File Offset: 0x001C62D0
			internal IAsyncResult PendingAsyncResult
			{
				get
				{
					if (this.pooledAsyncResult == null)
					{
						this.pooledAsyncResult = new BufferedOutputAsyncStream.WriteAsyncState.PooledAsyncResult(this, false);
					}
					return this.pooledAsyncResult;
				}
			}

			// Token: 0x17001BB4 RID: 7092
			// (get) Token: 0x06007A65 RID: 31333 RVA: 0x001C80ED File Offset: 0x001C62ED
			internal IAsyncResult CompletedSynchronouslyAsyncResult
			{
				get
				{
					if (this.completedSynchronouslyResult == null)
					{
						this.completedSynchronouslyResult = new BufferedOutputAsyncStream.WriteAsyncState.PooledAsyncResult(this, true);
					}
					return this.completedSynchronouslyResult;
				}
			}

			// Token: 0x06007A66 RID: 31334 RVA: 0x001C810A File Offset: 0x001C630A
			internal void ThrowOnException()
			{
				if (base.Exception != null)
				{
					throw FxTrace.Exception.AsError(base.Exception);
				}
			}

			// Token: 0x040045FF RID: 17919
			private BufferedOutputAsyncStream.WriteAsyncState.PooledAsyncResult pooledAsyncResult;

			// Token: 0x04004600 RID: 17920
			private BufferedOutputAsyncStream.WriteAsyncState.PooledAsyncResult completedSynchronouslyResult;

			// Token: 0x02000F41 RID: 3905
			private class PooledAsyncResult : IAsyncResult
			{
				// Token: 0x060086AE RID: 34478 RVA: 0x001F2F5E File Offset: 0x001F115E
				internal PooledAsyncResult(BufferedOutputAsyncStream.WriteAsyncState parentState, bool completedSynchronously)
				{
					this.writeState = parentState;
					this.completedSynchronously = completedSynchronously;
				}

				// Token: 0x17001D8A RID: 7562
				// (get) Token: 0x060086AF RID: 34479 RVA: 0x001F2F74 File Offset: 0x001F1174
				public object AsyncState
				{
					get
					{
						if (this.writeState.Arguments == null)
						{
							return null;
						}
						return this.writeState.Arguments.AsyncState;
					}
				}

				// Token: 0x17001D8B RID: 7563
				// (get) Token: 0x060086B0 RID: 34480 RVA: 0x001F2F95 File Offset: 0x001F1195
				public WaitHandle AsyncWaitHandle
				{
					get
					{
						throw FxTrace.Exception.AsError(new NotImplementedException());
					}
				}

				// Token: 0x17001D8C RID: 7564
				// (get) Token: 0x060086B1 RID: 34481 RVA: 0x001F2FA6 File Offset: 0x001F11A6
				public bool CompletedSynchronously
				{
					get
					{
						return this.completedSynchronously;
					}
				}

				// Token: 0x17001D8D RID: 7565
				// (get) Token: 0x060086B2 RID: 34482 RVA: 0x001F2FAE File Offset: 0x001F11AE
				public bool IsCompleted
				{
					get
					{
						throw FxTrace.Exception.AsError(new NotImplementedException());
					}
				}

				// Token: 0x04004E46 RID: 20038
				private readonly BufferedOutputAsyncStream.WriteAsyncState writeState;

				// Token: 0x04004E47 RID: 20039
				private readonly bool completedSynchronously;
			}
		}
	}
}
