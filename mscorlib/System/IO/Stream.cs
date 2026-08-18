using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200059E RID: 1438
	[ComVisible(true)]
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003477 RID: 13431
		public abstract bool CanRead { get; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003478 RID: 13432
		public abstract bool CanSeek { get; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000ADD4A File Offset: 0x000ACD4A
		[ComVisible(false)]
		public virtual bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600347A RID: 13434
		public abstract bool CanWrite { get; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600347B RID: 13435
		public abstract long Length { get; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600347C RID: 13436
		// (set) Token: 0x0600347D RID: 13437
		public abstract long Position { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000ADD4D File Offset: 0x000ACD4D
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000ADD5E File Offset: 0x000ACD5E
		[ComVisible(false)]
		public virtual int ReadTimeout
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000ADD6F File Offset: 0x000ACD6F
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000ADD80 File Offset: 0x000ACD80
		[ComVisible(false)]
		public virtual int WriteTimeout
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000ADD91 File Offset: 0x000ACD91
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000ADDA0 File Offset: 0x000ACDA0
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000ADDA8 File Offset: 0x000ACDA8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._asyncActiveEvent != null)
			{
				this._CloseAsyncActiveEvent(Interlocked.Decrement(ref this._asyncActiveCount));
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000ADDC6 File Offset: 0x000ACDC6
		private void _CloseAsyncActiveEvent(int asyncActiveCount)
		{
			if (this._asyncActiveEvent != null && asyncActiveCount == 0)
			{
				this._asyncActiveEvent.Close();
				this._asyncActiveEvent = null;
			}
		}

		// Token: 0x06003486 RID: 13446
		public abstract void Flush();

		// Token: 0x06003487 RID: 13447 RVA: 0x000ADDE5 File Offset: 0x000ACDE5
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000ADDF0 File Offset: 0x000ACDF0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			Interlocked.Increment(ref this._asyncActiveCount);
			Stream.ReadDelegate readDelegate = new Stream.ReadDelegate(this.Read);
			if (this._asyncActiveEvent == null)
			{
				lock (this)
				{
					if (this._asyncActiveEvent == null)
					{
						this._asyncActiveEvent = new AutoResetEvent(true);
					}
				}
			}
			this._asyncActiveEvent.WaitOne();
			this._readDelegate = readDelegate;
			return readDelegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000ADE84 File Offset: 0x000ACE84
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this._readDelegate == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			int result = -1;
			try
			{
				result = this._readDelegate.EndInvoke(asyncResult);
			}
			finally
			{
				this._readDelegate = null;
				this._asyncActiveEvent.Set();
				this._CloseAsyncActiveEvent(Interlocked.Decrement(ref this._asyncActiveCount));
			}
			return result;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000ADF00 File Offset: 0x000ACF00
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			Interlocked.Increment(ref this._asyncActiveCount);
			Stream.WriteDelegate writeDelegate = new Stream.WriteDelegate(this.Write);
			if (this._asyncActiveEvent == null)
			{
				lock (this)
				{
					if (this._asyncActiveEvent == null)
					{
						this._asyncActiveEvent = new AutoResetEvent(true);
					}
				}
			}
			this._asyncActiveEvent.WaitOne();
			this._writeDelegate = writeDelegate;
			return writeDelegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000ADF94 File Offset: 0x000ACF94
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this._writeDelegate == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			try
			{
				this._writeDelegate.EndInvoke(asyncResult);
			}
			finally
			{
				this._writeDelegate = null;
				this._asyncActiveEvent.Set();
				this._CloseAsyncActiveEvent(Interlocked.Decrement(ref this._asyncActiveCount));
			}
		}

		// Token: 0x0600348C RID: 13452
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x0600348D RID: 13453
		public abstract void SetLength(long value);

		// Token: 0x0600348E RID: 13454
		public abstract int Read([In] [Out] byte[] buffer, int offset, int count);

		// Token: 0x0600348F RID: 13455 RVA: 0x000AE00C File Offset: 0x000AD00C
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06003490 RID: 13456
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x06003491 RID: 13457 RVA: 0x000AE034 File Offset: 0x000AD034
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000AE055 File Offset: 0x000AD055
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		// Token: 0x04001BE0 RID: 7136
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x04001BE1 RID: 7137
		[NonSerialized]
		private Stream.ReadDelegate _readDelegate;

		// Token: 0x04001BE2 RID: 7138
		[NonSerialized]
		private Stream.WriteDelegate _writeDelegate;

		// Token: 0x04001BE3 RID: 7139
		[NonSerialized]
		private AutoResetEvent _asyncActiveEvent;

		// Token: 0x04001BE4 RID: 7140
		[NonSerialized]
		private int _asyncActiveCount = 1;

		// Token: 0x0200059F RID: 1439
		// (Invoke) Token: 0x06003496 RID: 13462
		private delegate int ReadDelegate([In] [Out] byte[] bytes, int index, int offset);

		// Token: 0x020005A0 RID: 1440
		// (Invoke) Token: 0x0600349A RID: 13466
		private delegate void WriteDelegate(byte[] bytes, int index, int offset);

		// Token: 0x020005A1 RID: 1441
		[Serializable]
		private sealed class NullStream : Stream
		{
			// Token: 0x0600349D RID: 13469 RVA: 0x000AE090 File Offset: 0x000AD090
			internal NullStream()
			{
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x0600349E RID: 13470 RVA: 0x000AE098 File Offset: 0x000AD098
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x0600349F RID: 13471 RVA: 0x000AE09B File Offset: 0x000AD09B
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000AE09E File Offset: 0x000AD09E
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000AE0A1 File Offset: 0x000AD0A1
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x060034A2 RID: 13474 RVA: 0x000AE0A5 File Offset: 0x000AD0A5
			// (set) Token: 0x060034A3 RID: 13475 RVA: 0x000AE0A9 File Offset: 0x000AD0A9
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x060034A4 RID: 13476 RVA: 0x000AE0AB File Offset: 0x000AD0AB
			public override void Flush()
			{
			}

			// Token: 0x060034A5 RID: 13477 RVA: 0x000AE0AD File Offset: 0x000AD0AD
			public override int Read([In] [Out] byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x060034A6 RID: 13478 RVA: 0x000AE0B0 File Offset: 0x000AD0B0
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x060034A7 RID: 13479 RVA: 0x000AE0B3 File Offset: 0x000AD0B3
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x060034A8 RID: 13480 RVA: 0x000AE0B5 File Offset: 0x000AD0B5
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x060034A9 RID: 13481 RVA: 0x000AE0B7 File Offset: 0x000AD0B7
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x060034AA RID: 13482 RVA: 0x000AE0BB File Offset: 0x000AD0BB
			public override void SetLength(long length)
			{
			}
		}

		// Token: 0x020005A2 RID: 1442
		[Serializable]
		internal sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x060034AB RID: 13483 RVA: 0x000AE0BD File Offset: 0x000AD0BD
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x060034AC RID: 13484 RVA: 0x000AE0DA File Offset: 0x000AD0DA
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x060034AD RID: 13485 RVA: 0x000AE0E7 File Offset: 0x000AD0E7
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x060034AE RID: 13486 RVA: 0x000AE0F4 File Offset: 0x000AD0F4
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x060034AF RID: 13487 RVA: 0x000AE101 File Offset: 0x000AD101
			[ComVisible(false)]
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000AE110 File Offset: 0x000AD110
			public override long Length
			{
				get
				{
					long length;
					lock (this._stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000AE150 File Offset: 0x000AD150
			// (set) Token: 0x060034B2 RID: 13490 RVA: 0x000AE190 File Offset: 0x000AD190
			public override long Position
			{
				get
				{
					long position;
					lock (this._stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					lock (this._stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000AE1D0 File Offset: 0x000AD1D0
			// (set) Token: 0x060034B4 RID: 13492 RVA: 0x000AE1DD File Offset: 0x000AD1DD
			[ComVisible(false)]
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000AE1EB File Offset: 0x000AD1EB
			// (set) Token: 0x060034B6 RID: 13494 RVA: 0x000AE1F8 File Offset: 0x000AD1F8
			[ComVisible(false)]
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x060034B7 RID: 13495 RVA: 0x000AE208 File Offset: 0x000AD208
			public override void Close()
			{
				lock (this._stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x060034B8 RID: 13496 RVA: 0x000AE25C File Offset: 0x000AD25C
			protected override void Dispose(bool disposing)
			{
				lock (this._stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x060034B9 RID: 13497 RVA: 0x000AE2B4 File Offset: 0x000AD2B4
			public override void Flush()
			{
				lock (this._stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x060034BA RID: 13498 RVA: 0x000AE2F4 File Offset: 0x000AD2F4
			public override int Read([In] [Out] byte[] bytes, int offset, int count)
			{
				int result;
				lock (this._stream)
				{
					result = this._stream.Read(bytes, offset, count);
				}
				return result;
			}

			// Token: 0x060034BB RID: 13499 RVA: 0x000AE338 File Offset: 0x000AD338
			public override int ReadByte()
			{
				int result;
				lock (this._stream)
				{
					result = this._stream.ReadByte();
				}
				return result;
			}

			// Token: 0x060034BC RID: 13500 RVA: 0x000AE378 File Offset: 0x000AD378
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				lock (this._stream)
				{
					result = this._stream.BeginRead(buffer, offset, count, callback, state);
				}
				return result;
			}

			// Token: 0x060034BD RID: 13501 RVA: 0x000AE3C0 File Offset: 0x000AD3C0
			public override int EndRead(IAsyncResult asyncResult)
			{
				int result;
				lock (this._stream)
				{
					result = this._stream.EndRead(asyncResult);
				}
				return result;
			}

			// Token: 0x060034BE RID: 13502 RVA: 0x000AE404 File Offset: 0x000AD404
			public override long Seek(long offset, SeekOrigin origin)
			{
				long result;
				lock (this._stream)
				{
					result = this._stream.Seek(offset, origin);
				}
				return result;
			}

			// Token: 0x060034BF RID: 13503 RVA: 0x000AE448 File Offset: 0x000AD448
			public override void SetLength(long length)
			{
				lock (this._stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x060034C0 RID: 13504 RVA: 0x000AE488 File Offset: 0x000AD488
			public override void Write(byte[] bytes, int offset, int count)
			{
				lock (this._stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x060034C1 RID: 13505 RVA: 0x000AE4CC File Offset: 0x000AD4CC
			public override void WriteByte(byte b)
			{
				lock (this._stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x060034C2 RID: 13506 RVA: 0x000AE50C File Offset: 0x000AD50C
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				lock (this._stream)
				{
					result = this._stream.BeginWrite(buffer, offset, count, callback, state);
				}
				return result;
			}

			// Token: 0x060034C3 RID: 13507 RVA: 0x000AE554 File Offset: 0x000AD554
			public override void EndWrite(IAsyncResult asyncResult)
			{
				lock (this._stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x04001BE5 RID: 7141
			private Stream _stream;
		}
	}
}
