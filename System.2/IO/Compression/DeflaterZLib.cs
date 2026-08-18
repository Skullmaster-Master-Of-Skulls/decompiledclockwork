using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000422 RID: 1058
	internal class DeflaterZLib : IDeflater, IDisposable
	{
		// Token: 0x06002792 RID: 10130 RVA: 0x000B61F9 File Offset: 0x000B43F9
		internal DeflaterZLib() : this(CompressionLevel.Optimal)
		{
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000B6204 File Offset: 0x000B4404
		internal DeflaterZLib(CompressionLevel compressionLevel)
		{
			ZLibNative.CompressionLevel compressionLevel2;
			int windowBits;
			int memLevel;
			ZLibNative.CompressionStrategy strategy;
			switch (compressionLevel)
			{
			case CompressionLevel.Optimal:
				compressionLevel2 = (ZLibNative.CompressionLevel)6;
				windowBits = -15;
				memLevel = 8;
				strategy = ZLibNative.CompressionStrategy.DefaultStrategy;
				break;
			case CompressionLevel.Fastest:
				compressionLevel2 = ZLibNative.CompressionLevel.BestSpeed;
				windowBits = -15;
				memLevel = 8;
				strategy = ZLibNative.CompressionStrategy.DefaultStrategy;
				break;
			case CompressionLevel.NoCompression:
				compressionLevel2 = ZLibNative.CompressionLevel.NoCompression;
				windowBits = -15;
				memLevel = 7;
				strategy = ZLibNative.CompressionStrategy.DefaultStrategy;
				break;
			default:
				throw new ArgumentOutOfRangeException("compressionLevel");
			}
			this._isDisposed = false;
			this.DeflateInit(compressionLevel2, windowBits, memLevel, strategy);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000B6274 File Offset: 0x000B4474
		~DeflaterZLib()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000B62AC File Offset: 0x000B44AC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000B62BB File Offset: 0x000B44BB
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (!this._isDisposed)
			{
				if (disposing)
				{
					this._zlibStream.Dispose();
				}
				if (this._inputBufferHandle.IsAllocated)
				{
					this.DeallocateInputBufferHandle();
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000B62ED File Offset: 0x000B44ED
		private bool NeedsInput()
		{
			return ((IDeflater)this).NeedsInput();
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000B62F5 File Offset: 0x000B44F5
		[SecuritySafeCritical]
		bool IDeflater.NeedsInput()
		{
			return this._zlibStream.AvailIn == 0U;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000B6308 File Offset: 0x000B4508
		[SecuritySafeCritical]
		void IDeflater.SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			if (count == 0)
			{
				return;
			}
			object obj = this.syncLock;
			lock (obj)
			{
				this._inputBufferHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
				this._isValid = 1;
				this._zlibStream.NextIn = this._inputBufferHandle.AddrOfPinnedObject() + startIndex;
				this._zlibStream.AvailIn = (uint)count;
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000B6384 File Offset: 0x000B4584
		[SecuritySafeCritical]
		int IDeflater.GetDeflateOutput(byte[] outputBuffer)
		{
			int result;
			try
			{
				int num;
				this.ReadDeflateOutput(outputBuffer, ZLibNative.FlushCode.NoFlush, out num);
				result = num;
			}
			finally
			{
				if (this._zlibStream.AvailIn == 0U && this._inputBufferHandle.IsAllocated)
				{
					this.DeallocateInputBufferHandle();
				}
			}
			return result;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000B63D4 File Offset: 0x000B45D4
		private unsafe ZLibNative.ErrorCode ReadDeflateOutput(byte[] outputBuffer, ZLibNative.FlushCode flushCode, out int bytesRead)
		{
			object obj = this.syncLock;
			ZLibNative.ErrorCode result;
			lock (obj)
			{
				byte* value;
				if (outputBuffer == null || outputBuffer.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &outputBuffer[0];
				}
				this._zlibStream.NextOut = (IntPtr)((void*)value);
				this._zlibStream.AvailOut = (uint)outputBuffer.Length;
				ZLibNative.ErrorCode errorCode = this.Deflate(flushCode);
				bytesRead = outputBuffer.Length - (int)this._zlibStream.AvailOut;
				result = errorCode;
			}
			return result;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000B6468 File Offset: 0x000B4668
		bool IDeflater.Finish(byte[] outputBuffer, out int bytesRead)
		{
			ZLibNative.ErrorCode errorCode = this.ReadDeflateOutput(outputBuffer, ZLibNative.FlushCode.Finish, out bytesRead);
			return errorCode == ZLibNative.ErrorCode.StreamEnd;
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000B6484 File Offset: 0x000B4684
		private void DeallocateInputBufferHandle()
		{
			object obj = this.syncLock;
			lock (obj)
			{
				this._zlibStream.AvailIn = 0U;
				this._zlibStream.NextIn = ZLibNative.ZNullPtr;
				if (Interlocked.Exchange(ref this._isValid, 0) != 0)
				{
					this._inputBufferHandle.Free();
				}
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000B64F4 File Offset: 0x000B46F4
		[SecuritySafeCritical]
		private void DeflateInit(ZLibNative.CompressionLevel compressionLevel, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy)
		{
			ZLibNative.ErrorCode zlibErrorCode;
			try
			{
				zlibErrorCode = ZLibNative.CreateZLibStreamForDeflate(out this._zlibStream, compressionLevel, windowBits, memLevel, strategy);
			}
			catch (Exception inner)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), inner);
			}
			switch (zlibErrorCode)
			{
			case ZLibNative.ErrorCode.VersionError:
				throw new ZLibException(SR.GetString("ZLibErrorVersionMismatch"), "deflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.MemError:
				throw new ZLibException(SR.GetString("ZLibErrorNotEnoughMemory"), "deflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorIncorrectInitParameters"), "deflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
				return;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "deflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000B65E4 File Offset: 0x000B47E4
		[SecuritySafeCritical]
		private ZLibNative.ErrorCode Deflate(ZLibNative.FlushCode flushCode)
		{
			ZLibNative.ErrorCode errorCode;
			try
			{
				errorCode = this._zlibStream.Deflate(flushCode);
			}
			catch (Exception inner)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), inner);
			}
			switch (errorCode)
			{
			case ZLibNative.ErrorCode.BufError:
				return errorCode;
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorInconsistentStream"), "deflate", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
			case ZLibNative.ErrorCode.StreamEnd:
				return errorCode;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "deflate", (int)errorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x04002181 RID: 8577
		private ZLibNative.ZLibStreamHandle _zlibStream;

		// Token: 0x04002182 RID: 8578
		private GCHandle _inputBufferHandle;

		// Token: 0x04002183 RID: 8579
		private bool _isDisposed;

		// Token: 0x04002184 RID: 8580
		private int _isValid;

		// Token: 0x04002185 RID: 8581
		private readonly object syncLock = new object();
	}
}
