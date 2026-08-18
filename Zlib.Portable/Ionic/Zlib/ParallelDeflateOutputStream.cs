using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x0200000F RID: 15
	public class ParallelDeflateOutputStream : Stream
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000877E File Offset: 0x0000697E
		public ParallelDeflateOutputStream(Stream stream) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000878A File Offset: 0x0000698A
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level) : this(stream, level, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00008796 File Offset: 0x00006996
		public ParallelDeflateOutputStream(Stream stream, bool leaveOpen) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000087A2 File Offset: 0x000069A2
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000087B0 File Offset: 0x000069B0
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
		{
			this._outStream = stream;
			this._compressLevel = level;
			this.Strategy = strategy;
			this._leaveOpen = leaveOpen;
			this.MaxBufferPairs = 16;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000881F File Offset: 0x00006A1F
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00008827 File Offset: 0x00006A27
		public CompressionStrategy Strategy { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00008830 File Offset: 0x00006A30
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00008838 File Offset: 0x00006A38
		public int MaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00008855 File Offset: 0x00006A55
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000885D File Offset: 0x00006A5D
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than 1024 bytes");
				}
				this._bufferSize = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000887E File Offset: 0x00006A7E
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00008886 File Offset: 0x00006A86
		public long BytesProcessed
		{
			get
			{
				return this._totalBytesProcessed;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008890 File Offset: 0x00006A90
		private void _InitializePoolOfWorkItems()
		{
			this._toWrite = new Queue<int>();
			this._toFill = new Queue<int>();
			this._pool = new List<WorkItem>();
			int num = ParallelDeflateOutputStream.BufferPairsPerCore * 4;
			num = Math.Min(num, this._maxBufferPairs);
			for (int i = 0; i < num; i++)
			{
				this._pool.Add(new WorkItem(this._bufferSize, this._compressLevel, this.Strategy, i));
				this._toFill.Enqueue(i);
			}
			this._newlyCompressedBlob = new AutoResetEvent(false);
			this._runningCrc = new CRC32();
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008944 File Offset: 0x00006B44
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool mustWait = false;
			if (this._isClosed)
			{
				throw new InvalidOperationException();
			}
			if (this._pendingException != null)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			if (count == 0)
			{
				return;
			}
			if (!this._firstWriteDone)
			{
				this._InitializePoolOfWorkItems();
				this._firstWriteDone = true;
			}
			for (;;)
			{
				this.EmitPendingBuffers(false, mustWait);
				mustWait = false;
				int num;
				if (this._currentlyFilling >= 0)
				{
					num = this._currentlyFilling;
					goto IL_98;
				}
				if (this._toFill.Count != 0)
				{
					num = this._toFill.Dequeue();
					this._lastFilled++;
					goto IL_98;
				}
				mustWait = true;
				IL_145:
				if (count <= 0)
				{
					return;
				}
				continue;
				IL_98:
				WorkItem workItem = this._pool[num];
				int num2 = (workItem.buffer.Length - workItem.inputBytesAvailable > count) ? count : (workItem.buffer.Length - workItem.inputBytesAvailable);
				workItem.ordinal = this._lastFilled;
				Buffer.BlockCopy(buffer, offset, workItem.buffer, workItem.inputBytesAvailable, num2);
				count -= num2;
				offset += num2;
				workItem.inputBytesAvailable += num2;
				if (workItem.inputBytesAvailable == workItem.buffer.Length)
				{
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this._DeflateOne), workItem))
					{
						break;
					}
					this._currentlyFilling = -1;
				}
				else
				{
					this._currentlyFilling = num;
				}
				goto IL_145;
			}
			throw new Exception("Cannot enqueue workitem");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008AA0 File Offset: 0x00006CA0
		private void _FlushFinish()
		{
			byte[] array = new byte[128];
			ZlibCodec zlibCodec = new ZlibCodec();
			int num = zlibCodec.InitializeDeflate(this._compressLevel, false);
			zlibCodec.InputBuffer = null;
			zlibCodec.NextIn = 0;
			zlibCodec.AvailableBytesIn = 0;
			zlibCodec.OutputBuffer = array;
			zlibCodec.NextOut = 0;
			zlibCodec.AvailableBytesOut = array.Length;
			num = zlibCodec.Deflate(FlushType.Finish);
			if (num != 1 && num != 0)
			{
				throw new Exception("deflating: " + zlibCodec.Message);
			}
			if (array.Length - zlibCodec.AvailableBytesOut > 0)
			{
				this._outStream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
			}
			zlibCodec.EndDeflate();
			this._Crc32 = this._runningCrc.Crc32Result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008B5C File Offset: 0x00006D5C
		private void _Flush(bool lastInput)
		{
			if (this._isClosed)
			{
				throw new InvalidOperationException();
			}
			if (this.emitting)
			{
				return;
			}
			if (this._currentlyFilling >= 0)
			{
				WorkItem wi = this._pool[this._currentlyFilling];
				this._DeflateOne(wi);
				this._currentlyFilling = -1;
			}
			if (lastInput)
			{
				this.EmitPendingBuffers(true, false);
				this._FlushFinish();
				return;
			}
			this.EmitPendingBuffers(false, false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008BC3 File Offset: 0x00006DC3
		public override void Flush()
		{
			if (this._pendingException != null)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			if (this._handlingException)
			{
				return;
			}
			this._Flush(false);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public void Close()
		{
			if (this._pendingException != null)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			if (this._handlingException)
			{
				return;
			}
			if (this._isClosed)
			{
				return;
			}
			this._Flush(true);
			if (!this._leaveOpen)
			{
				this._outStream.Dispose();
			}
			this._isClosed = true;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008C5B File Offset: 0x00006E5B
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.Close();
			this._pool = null;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008C74 File Offset: 0x00006E74
		public void Reset(Stream stream)
		{
			if (!this._firstWriteDone)
			{
				return;
			}
			this._toWrite.Clear();
			this._toFill.Clear();
			foreach (WorkItem workItem in this._pool)
			{
				this._toFill.Enqueue(workItem.index);
				workItem.ordinal = -1;
			}
			this._firstWriteDone = false;
			this._totalBytesProcessed = 0L;
			this._runningCrc = new CRC32();
			this._isClosed = false;
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
			this._outStream = stream;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008D3C File Offset: 0x00006F3C
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (this.emitting)
			{
				return;
			}
			this.emitting = true;
			if (doAll || mustWait)
			{
				this._newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = doAll ? 200 : (mustWait ? -1 : 0);
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(this._toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (this._toWrite.Count > 0)
							{
								num3 = this._toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(this._toWrite);
						}
						if (num3 >= 0)
						{
							WorkItem workItem = this._pool[num3];
							if (workItem.ordinal != this._lastWritten + 1)
							{
								Queue<int> toWrite = this._toWrite;
								lock (toWrite)
								{
									this._toWrite.Enqueue(num3);
								}
								if (num == num3)
								{
									this._newlyCompressedBlob.WaitOne();
									num = -1;
								}
								else if (num == -1)
								{
									num = num3;
								}
							}
							else
							{
								num = -1;
								this._outStream.Write(workItem.compressed, 0, workItem.compressedBytesAvailable);
								this._runningCrc.Combine(workItem.crc, workItem.inputBytesAvailable);
								this._totalBytesProcessed += (long)workItem.inputBytesAvailable;
								workItem.inputBytesAvailable = 0;
								this._lastWritten = workItem.ordinal;
								this._toFill.Enqueue(workItem.index);
								if (num2 == -1)
								{
									num2 = 0;
								}
							}
						}
					}
					else
					{
						num3 = -1;
					}
				}
				while (num3 >= 0);
			}
			while (doAll && this._lastWritten != this._latestCompressed);
			this.emitting = false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008EDC File Offset: 0x000070DC
		private void _DeflateOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				int index = workItem.index;
				CRC32 crc = new CRC32();
				crc.SlurpBlock(workItem.buffer, 0, workItem.inputBytesAvailable);
				this.DeflateOneSegment(workItem);
				workItem.crc = crc.Crc32Result;
				object obj = this._latestLock;
				lock (obj)
				{
					if (workItem.ordinal > this._latestCompressed)
					{
						this._latestCompressed = workItem.ordinal;
					}
				}
				Queue<int> toWrite = this._toWrite;
				lock (toWrite)
				{
					this._toWrite.Enqueue(workItem.index);
				}
				this._newlyCompressedBlob.Set();
			}
			catch (Exception pendingException)
			{
				object obj = this._eLock;
				lock (obj)
				{
					if (this._pendingException != null)
					{
						this._pendingException = pendingException;
					}
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00009004 File Offset: 0x00007204
		private bool DeflateOneSegment(WorkItem workitem)
		{
			ZlibCodec compressor = workitem.compressor;
			compressor.ResetDeflate();
			compressor.NextIn = 0;
			compressor.AvailableBytesIn = workitem.inputBytesAvailable;
			compressor.NextOut = 0;
			compressor.AvailableBytesOut = workitem.compressed.Length;
			do
			{
				compressor.Deflate(FlushType.None);
			}
			while (compressor.AvailableBytesIn > 0 || compressor.AvailableBytesOut == 0);
			compressor.Deflate(FlushType.Sync);
			workitem.compressedBytesAvailable = (int)compressor.TotalBytesOut;
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009078 File Offset: 0x00007278
		[Conditional("Trace")]
		private void TraceOutput(ParallelDeflateOutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this._DesiredTrace) != ParallelDeflateOutputStream.TraceBits.None)
			{
				object outputLock = this._outputLock;
				lock (outputLock)
				{
					Thread.CurrentThread.GetHashCode();
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004975 File Offset: 0x00002B75
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004975 File Offset: 0x00002B75
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000090CE File Offset: 0x000072CE
		public override bool CanWrite
		{
			get
			{
				return this._outStream.CanWrite;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000090E2 File Offset: 0x000072E2
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Position
		{
			get
			{
				return this._outStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000090DB File Offset: 0x000072DB
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000090DB File Offset: 0x000072DB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000090DB File Offset: 0x000072DB
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040000CC RID: 204
		private static readonly int IO_BUFFER_SIZE_DEFAULT = 65536;

		// Token: 0x040000CD RID: 205
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x040000CE RID: 206
		private List<WorkItem> _pool;

		// Token: 0x040000CF RID: 207
		private bool _leaveOpen;

		// Token: 0x040000D0 RID: 208
		private bool emitting;

		// Token: 0x040000D1 RID: 209
		private Stream _outStream;

		// Token: 0x040000D2 RID: 210
		private int _maxBufferPairs;

		// Token: 0x040000D3 RID: 211
		private int _bufferSize = ParallelDeflateOutputStream.IO_BUFFER_SIZE_DEFAULT;

		// Token: 0x040000D4 RID: 212
		private AutoResetEvent _newlyCompressedBlob;

		// Token: 0x040000D5 RID: 213
		private object _outputLock = new object();

		// Token: 0x040000D6 RID: 214
		private bool _isClosed;

		// Token: 0x040000D7 RID: 215
		private bool _firstWriteDone;

		// Token: 0x040000D8 RID: 216
		private int _currentlyFilling;

		// Token: 0x040000D9 RID: 217
		private int _lastFilled;

		// Token: 0x040000DA RID: 218
		private int _lastWritten;

		// Token: 0x040000DB RID: 219
		private int _latestCompressed;

		// Token: 0x040000DC RID: 220
		private int _Crc32;

		// Token: 0x040000DD RID: 221
		private CRC32 _runningCrc;

		// Token: 0x040000DE RID: 222
		private object _latestLock = new object();

		// Token: 0x040000DF RID: 223
		private Queue<int> _toWrite;

		// Token: 0x040000E0 RID: 224
		private Queue<int> _toFill;

		// Token: 0x040000E1 RID: 225
		private long _totalBytesProcessed;

		// Token: 0x040000E2 RID: 226
		private CompressionLevel _compressLevel;

		// Token: 0x040000E3 RID: 227
		private volatile Exception _pendingException;

		// Token: 0x040000E4 RID: 228
		private bool _handlingException;

		// Token: 0x040000E5 RID: 229
		private object _eLock = new object();

		// Token: 0x040000E6 RID: 230
		private ParallelDeflateOutputStream.TraceBits _DesiredTrace = ParallelDeflateOutputStream.TraceBits.EmitLock | ParallelDeflateOutputStream.TraceBits.EmitEnter | ParallelDeflateOutputStream.TraceBits.EmitBegin | ParallelDeflateOutputStream.TraceBits.EmitDone | ParallelDeflateOutputStream.TraceBits.EmitSkip | ParallelDeflateOutputStream.TraceBits.Session | ParallelDeflateOutputStream.TraceBits.Compress | ParallelDeflateOutputStream.TraceBits.WriteEnter | ParallelDeflateOutputStream.TraceBits.WriteTake;

		// Token: 0x02000026 RID: 38
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x04000199 RID: 409
			None = 0U,
			// Token: 0x0400019A RID: 410
			NotUsed1 = 1U,
			// Token: 0x0400019B RID: 411
			EmitLock = 2U,
			// Token: 0x0400019C RID: 412
			EmitEnter = 4U,
			// Token: 0x0400019D RID: 413
			EmitBegin = 8U,
			// Token: 0x0400019E RID: 414
			EmitDone = 16U,
			// Token: 0x0400019F RID: 415
			EmitSkip = 32U,
			// Token: 0x040001A0 RID: 416
			EmitAll = 58U,
			// Token: 0x040001A1 RID: 417
			Flush = 64U,
			// Token: 0x040001A2 RID: 418
			Lifecycle = 128U,
			// Token: 0x040001A3 RID: 419
			Session = 256U,
			// Token: 0x040001A4 RID: 420
			Synch = 512U,
			// Token: 0x040001A5 RID: 421
			Instance = 1024U,
			// Token: 0x040001A6 RID: 422
			Compress = 2048U,
			// Token: 0x040001A7 RID: 423
			Write = 4096U,
			// Token: 0x040001A8 RID: 424
			WriteEnter = 8192U,
			// Token: 0x040001A9 RID: 425
			WriteTake = 16384U,
			// Token: 0x040001AA RID: 426
			All = 4294967295U
		}
	}
}
