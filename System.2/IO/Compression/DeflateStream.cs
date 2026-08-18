using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000426 RID: 1062
	[__DynamicallyInvokable]
	public class DeflateStream : Stream
	{
		// Token: 0x060027BE RID: 10174 RVA: 0x000B6C69 File Offset: 0x000B4E69
		[__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionMode mode) : this(stream, mode, false)
		{
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000B6C74 File Offset: 0x000B4E74
		internal DeflateStream(Stream stream, bool leaveOpen, IFileFormatReader reader)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(SR.GetString("NotReadableStream"), "stream");
			}
			this.inflater = DeflateStream.CreateInflater(reader);
			this.m_CallBack = new AsyncCallback(this.ReadCallback);
			this._stream = stream;
			this._mode = CompressionMode.Decompress;
			this._leaveOpen = leaveOpen;
			this.buffer = new byte[8192];
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000B6CF8 File Offset: 0x000B4EF8
		[__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (CompressionMode.Compress != mode && mode != CompressionMode.Decompress)
			{
				throw new ArgumentException(SR.GetString("ArgumentOutOfRange_Enum"), "mode");
			}
			this._stream = stream;
			this._mode = mode;
			this._leaveOpen = leaveOpen;
			CompressionMode mode2 = this._mode;
			if (mode2 != CompressionMode.Decompress)
			{
				if (mode2 == CompressionMode.Compress)
				{
					if (!this._stream.CanWrite)
					{
						throw new ArgumentException(SR.GetString("NotWriteableStream"), "stream");
					}
					this.deflater = DeflateStream.CreateDeflater(null);
					this.m_AsyncWriterDelegate = new DeflateStream.AsyncWriteDelegate(this.InternalWrite);
					this.m_CallBack = new AsyncCallback(this.WriteCallback);
				}
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(SR.GetString("NotReadableStream"), "stream");
				}
				this.inflater = DeflateStream.CreateInflater(null);
				this.m_CallBack = new AsyncCallback(this.ReadCallback);
			}
			this.buffer = new byte[8192];
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000B6E09 File Offset: 0x000B5009
		[__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionLevel compressionLevel) : this(stream, compressionLevel, false)
		{
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000B6E14 File Offset: 0x000B5014
		[__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("NotWriteableStream"), "stream");
			}
			this._stream = stream;
			this._mode = CompressionMode.Compress;
			this._leaveOpen = leaveOpen;
			this.deflater = DeflateStream.CreateDeflater(new CompressionLevel?(compressionLevel));
			this.m_AsyncWriterDelegate = new DeflateStream.AsyncWriteDelegate(this.InternalWrite);
			this.m_CallBack = new AsyncCallback(this.WriteCallback);
			this.buffer = new byte[8192];
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000B6EAC File Offset: 0x000B50AC
		private static IDeflater CreateDeflater(CompressionLevel? compressionLevel)
		{
			DeflateStream.WorkerType workerType = DeflateStream.GetDeflaterType();
			if (workerType == DeflateStream.WorkerType.Managed)
			{
				return new DeflaterManaged();
			}
			if (workerType != DeflateStream.WorkerType.ZLib)
			{
				throw new SystemException("Program entered an unexpected state.");
			}
			if (compressionLevel != null)
			{
				return new DeflaterZLib(compressionLevel.Value);
			}
			return new DeflaterZLib();
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000B6EF4 File Offset: 0x000B50F4
		private static IInflater CreateInflater(IFileFormatReader reader = null)
		{
			DeflateStream.WorkerType workerType = DeflateStream.GetInflaterType();
			if (workerType == DeflateStream.WorkerType.Managed)
			{
				return new Inflater(reader);
			}
			if (workerType != DeflateStream.WorkerType.ZLib)
			{
				throw new SystemException("Program entered an unexpected state.");
			}
			if (reader == null)
			{
				return new InflaterZlib(-15);
			}
			return new InflaterZlib(47);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000B6F34 File Offset: 0x000B5134
		[SecuritySafeCritical]
		private static DeflateStream.WorkerType GetDeflaterType()
		{
			if (DeflateStream.WorkerType.Unknown != DeflateStream.deflaterType)
			{
				return DeflateStream.deflaterType;
			}
			if (CLRConfig.CheckLegacyManagedDeflateStream())
			{
				return DeflateStream.deflaterType = DeflateStream.WorkerType.Managed;
			}
			if (!CompatibilitySwitches.IsNetFx45LegacyManagedDeflateStream)
			{
				return DeflateStream.deflaterType = DeflateStream.WorkerType.ZLib;
			}
			return DeflateStream.deflaterType = DeflateStream.WorkerType.Managed;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000B6F73 File Offset: 0x000B5173
		[SecuritySafeCritical]
		private static DeflateStream.WorkerType GetInflaterType()
		{
			if (DeflateStream.WorkerType.Unknown != DeflateStream.inflaterType)
			{
				return DeflateStream.inflaterType;
			}
			if (!LocalAppContextSwitches.DoNotUseNativeZipLibraryForDecompression)
			{
				return DeflateStream.inflaterType = DeflateStream.WorkerType.ZLib;
			}
			return DeflateStream.inflaterType = DeflateStream.WorkerType.Managed;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000B6FA1 File Offset: 0x000B51A1
		internal void SetFileFormatWriter(IFileFormatWriter writer)
		{
			if (writer != null)
			{
				this.formatWriter = writer;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000B6FAD File Offset: 0x000B51AD
		[__DynamicallyInvokable]
		public Stream BaseStream
		{
			[__DynamicallyInvokable]
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000B6FB5 File Offset: 0x000B51B5
		[__DynamicallyInvokable]
		public override bool CanRead
		{
			[__DynamicallyInvokable]
			get
			{
				return this._stream != null && this._mode == CompressionMode.Decompress && this._stream.CanRead;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000B6FD6 File Offset: 0x000B51D6
		[__DynamicallyInvokable]
		public override bool CanWrite
		{
			[__DynamicallyInvokable]
			get
			{
				return this._stream != null && this._mode == CompressionMode.Compress && this._stream.CanWrite;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000B6FF8 File Offset: 0x000B51F8
		[__DynamicallyInvokable]
		public override bool CanSeek
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000B6FFB File Offset: 0x000B51FB
		[__DynamicallyInvokable]
		public override long Length
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000B700C File Offset: 0x000B520C
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000B701D File Offset: 0x000B521D
		[__DynamicallyInvokable]
		public override long Position
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
			[__DynamicallyInvokable]
			set
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000B702E File Offset: 0x000B522E
		[__DynamicallyInvokable]
		public override void Flush()
		{
			this.EnsureNotDisposed();
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000B7036 File Offset: 0x000B5236
		[__DynamicallyInvokable]
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000B7047 File Offset: 0x000B5247
		[__DynamicallyInvokable]
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000B7058 File Offset: 0x000B5258
		[__DynamicallyInvokable]
		public override int Read(byte[] array, int offset, int count)
		{
			this.EnsureDecompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			int num = offset;
			int num2 = count;
			for (;;)
			{
				int num3 = this.inflater.Inflate(array, num, num2);
				num += num3;
				num2 -= num3;
				if (num2 == 0 || this.inflater.Finished())
				{
					break;
				}
				int num4 = this._stream.Read(this.buffer, 0, this.buffer.Length);
				if (num4 == 0)
				{
					break;
				}
				this.inflater.SetInput(this.buffer, 0, num4);
			}
			return count - num2;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000B70DC File Offset: 0x000B52DC
		private void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("InvalidArgumentOffsetCount"));
			}
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000B712D File Offset: 0x000B532D
		private void EnsureNotDisposed()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000B7148 File Offset: 0x000B5348
		private void EnsureDecompressionMode()
		{
			if (this._mode != CompressionMode.Decompress)
			{
				throw new InvalidOperationException(SR.GetString("CannotReadFromDeflateStream"));
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000B7162 File Offset: 0x000B5362
		private void EnsureCompressionMode()
		{
			if (this._mode != CompressionMode.Compress)
			{
				throw new InvalidOperationException(SR.GetString("CannotWriteToDeflateStream"));
			}
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000B7180 File Offset: 0x000B5380
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this.EnsureDecompressionMode();
			if (this.asyncOperations != 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidBeginCall"));
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			Interlocked.Increment(ref this.asyncOperations);
			IAsyncResult result;
			try
			{
				DeflateStreamAsyncResult deflateStreamAsyncResult = new DeflateStreamAsyncResult(this, asyncState, asyncCallback, array, offset, count);
				deflateStreamAsyncResult.isWrite = false;
				int num = this.inflater.Inflate(array, offset, count);
				if (num != 0)
				{
					deflateStreamAsyncResult.InvokeCallback(true, num);
					result = deflateStreamAsyncResult;
				}
				else if (this.inflater.Finished())
				{
					deflateStreamAsyncResult.InvokeCallback(true, 0);
					result = deflateStreamAsyncResult;
				}
				else
				{
					this._stream.BeginRead(this.buffer, 0, this.buffer.Length, this.m_CallBack, deflateStreamAsyncResult);
					deflateStreamAsyncResult.m_CompletedSynchronously &= deflateStreamAsyncResult.IsCompleted;
					result = deflateStreamAsyncResult;
				}
			}
			catch
			{
				Interlocked.Decrement(ref this.asyncOperations);
				throw;
			}
			return result;
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000B7278 File Offset: 0x000B5478
		private void ReadCallback(IAsyncResult baseStreamResult)
		{
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)baseStreamResult.AsyncState;
			deflateStreamAsyncResult.m_CompletedSynchronously &= baseStreamResult.CompletedSynchronously;
			try
			{
				this.EnsureNotDisposed();
				int num = this._stream.EndRead(baseStreamResult);
				if (num <= 0)
				{
					deflateStreamAsyncResult.InvokeCallback(0);
				}
				else
				{
					this.inflater.SetInput(this.buffer, 0, num);
					num = this.inflater.Inflate(deflateStreamAsyncResult.buffer, deflateStreamAsyncResult.offset, deflateStreamAsyncResult.count);
					if (num == 0 && !this.inflater.Finished())
					{
						this._stream.BeginRead(this.buffer, 0, this.buffer.Length, this.m_CallBack, deflateStreamAsyncResult);
					}
					else
					{
						deflateStreamAsyncResult.InvokeCallback(num);
					}
				}
			}
			catch (Exception result)
			{
				deflateStreamAsyncResult.InvokeCallback(result);
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000B7358 File Offset: 0x000B5558
		[__DynamicallyInvokable]
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.EnsureDecompressionMode();
			this.CheckEndXxxxLegalStateAndParams(asyncResult);
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)asyncResult;
			this.AwaitAsyncResultCompletion(deflateStreamAsyncResult);
			Exception ex = deflateStreamAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw ex;
			}
			return (int)deflateStreamAsyncResult.Result;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000B739C File Offset: 0x000B559C
		[__DynamicallyInvokable]
		public override void Write(byte[] array, int offset, int count)
		{
			this.EnsureCompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			this.InternalWrite(array, offset, count, false);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000B73BD File Offset: 0x000B55BD
		internal void InternalWrite(byte[] array, int offset, int count, bool isAsync)
		{
			this.DoMaintenance(array, offset, count);
			this.WriteDeflaterOutput(isAsync);
			this.deflater.SetInput(array, offset, count);
			this.WriteDeflaterOutput(isAsync);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000B73E8 File Offset: 0x000B55E8
		private void WriteDeflaterOutput(bool isAsync)
		{
			while (!this.deflater.NeedsInput())
			{
				int deflateOutput = this.deflater.GetDeflateOutput(this.buffer);
				if (deflateOutput > 0)
				{
					this.DoWrite(this.buffer, 0, deflateOutput, isAsync);
				}
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000B742C File Offset: 0x000B562C
		private void DoWrite(byte[] array, int offset, int count, bool isAsync)
		{
			if (isAsync)
			{
				IAsyncResult asyncResult = this._stream.BeginWrite(array, offset, count, null, null);
				this._stream.EndWrite(asyncResult);
				return;
			}
			this._stream.Write(array, offset, count);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000B746C File Offset: 0x000B566C
		private void DoMaintenance(byte[] array, int offset, int count)
		{
			if (count <= 0)
			{
				return;
			}
			this.wroteBytes = true;
			if (this.formatWriter == null)
			{
				return;
			}
			if (!this.wroteHeader)
			{
				byte[] header = this.formatWriter.GetHeader();
				this._stream.Write(header, 0, header.Length);
				this.wroteHeader = true;
			}
			this.formatWriter.UpdateWithBytesRead(array, offset, count);
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000B74C8 File Offset: 0x000B56C8
		private void PurgeBuffers(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this._stream == null)
			{
				return;
			}
			this.Flush();
			if (this._mode != CompressionMode.Compress)
			{
				return;
			}
			if (this.wroteBytes)
			{
				this.WriteDeflaterOutput(false);
				bool flag;
				do
				{
					int num;
					flag = this.deflater.Finish(this.buffer, out num);
					if (num > 0)
					{
						this.DoWrite(this.buffer, 0, num, false);
					}
				}
				while (!flag);
			}
			if (this.formatWriter != null && this.wroteHeader)
			{
				byte[] footer = this.formatWriter.GetFooter();
				this._stream.Write(footer, 0, footer.Length);
			}
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000B7558 File Offset: 0x000B5758
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.PurgeBuffers(disposing);
			}
			finally
			{
				try
				{
					if (disposing && !this._leaveOpen && this._stream != null)
					{
						this._stream.Close();
					}
				}
				finally
				{
					this._stream = null;
					try
					{
						if (this.deflater != null)
						{
							this.deflater.Dispose();
						}
						if (this.inflater != null)
						{
							this.inflater.Dispose();
						}
					}
					finally
					{
						this.inflater = null;
						this.deflater = null;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000B7600 File Offset: 0x000B5800
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this.EnsureCompressionMode();
			if (this.asyncOperations != 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidBeginCall"));
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			Interlocked.Increment(ref this.asyncOperations);
			IAsyncResult result;
			try
			{
				DeflateStreamAsyncResult deflateStreamAsyncResult = new DeflateStreamAsyncResult(this, asyncState, asyncCallback, array, offset, count);
				deflateStreamAsyncResult.isWrite = true;
				this.m_AsyncWriterDelegate.BeginInvoke(array, offset, count, true, this.m_CallBack, deflateStreamAsyncResult);
				deflateStreamAsyncResult.m_CompletedSynchronously &= deflateStreamAsyncResult.IsCompleted;
				result = deflateStreamAsyncResult;
			}
			catch
			{
				Interlocked.Decrement(ref this.asyncOperations);
				throw;
			}
			return result;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000B76AC File Offset: 0x000B58AC
		private void WriteCallback(IAsyncResult asyncResult)
		{
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)asyncResult.AsyncState;
			deflateStreamAsyncResult.m_CompletedSynchronously &= asyncResult.CompletedSynchronously;
			try
			{
				this.m_AsyncWriterDelegate.EndInvoke(asyncResult);
			}
			catch (Exception result)
			{
				deflateStreamAsyncResult.InvokeCallback(result);
				return;
			}
			deflateStreamAsyncResult.InvokeCallback(null);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000B7708 File Offset: 0x000B5908
		[__DynamicallyInvokable]
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.EnsureCompressionMode();
			this.CheckEndXxxxLegalStateAndParams(asyncResult);
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)asyncResult;
			this.AwaitAsyncResultCompletion(deflateStreamAsyncResult);
			Exception ex = deflateStreamAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000B7744 File Offset: 0x000B5944
		private void CheckEndXxxxLegalStateAndParams(IAsyncResult asyncResult)
		{
			if (this.asyncOperations != 1)
			{
				throw new InvalidOperationException(SR.GetString("InvalidEndCall"));
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			this.EnsureNotDisposed();
			if (!(asyncResult is DeflateStreamAsyncResult))
			{
				throw new ArgumentNullException("asyncResult");
			}
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000B7794 File Offset: 0x000B5994
		private void AwaitAsyncResultCompletion(DeflateStreamAsyncResult asyncResult)
		{
			try
			{
				if (!asyncResult.IsCompleted)
				{
					asyncResult.AsyncWaitHandle.WaitOne();
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.asyncOperations);
				asyncResult.Close();
			}
		}

		// Token: 0x04002195 RID: 8597
		internal const int DefaultBufferSize = 8192;

		// Token: 0x04002196 RID: 8598
		private const int WindowSizeUpperBound = 47;

		// Token: 0x04002197 RID: 8599
		private Stream _stream;

		// Token: 0x04002198 RID: 8600
		private CompressionMode _mode;

		// Token: 0x04002199 RID: 8601
		private bool _leaveOpen;

		// Token: 0x0400219A RID: 8602
		private IInflater inflater;

		// Token: 0x0400219B RID: 8603
		private IDeflater deflater;

		// Token: 0x0400219C RID: 8604
		private byte[] buffer;

		// Token: 0x0400219D RID: 8605
		private int asyncOperations;

		// Token: 0x0400219E RID: 8606
		private readonly AsyncCallback m_CallBack;

		// Token: 0x0400219F RID: 8607
		private readonly DeflateStream.AsyncWriteDelegate m_AsyncWriterDelegate;

		// Token: 0x040021A0 RID: 8608
		private IFileFormatWriter formatWriter;

		// Token: 0x040021A1 RID: 8609
		private bool wroteHeader;

		// Token: 0x040021A2 RID: 8610
		private bool wroteBytes;

		// Token: 0x040021A3 RID: 8611
		private static volatile DeflateStream.WorkerType deflaterType = DeflateStream.WorkerType.Unknown;

		// Token: 0x040021A4 RID: 8612
		private static volatile DeflateStream.WorkerType inflaterType = DeflateStream.WorkerType.Unknown;

		// Token: 0x02000828 RID: 2088
		// (Invoke) Token: 0x0600454D RID: 17741
		internal delegate void AsyncWriteDelegate(byte[] array, int offset, int count, bool isAsync);

		// Token: 0x02000829 RID: 2089
		private enum WorkerType : byte
		{
			// Token: 0x040035D4 RID: 13780
			Managed,
			// Token: 0x040035D5 RID: 13781
			ZLib,
			// Token: 0x040035D6 RID: 13782
			Unknown
		}
	}
}
