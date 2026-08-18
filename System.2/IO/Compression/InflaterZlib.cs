using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000423 RID: 1059
	internal class InflaterZlib : IInflater, IDisposable
	{
		// Token: 0x060027A0 RID: 10144 RVA: 0x000B6690 File Offset: 0x000B4890
		internal InflaterZlib(int windowBits)
		{
			this._finished = false;
			this._isDisposed = false;
			this.InflateInit(windowBits);
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000B66B8 File Offset: 0x000B48B8
		public int AvailableOutput
		{
			get
			{
				return (int)this._zlibStream.AvailOut;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000B66C5 File Offset: 0x000B48C5
		public bool Finished()
		{
			return this._finished;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000B66D0 File Offset: 0x000B48D0
		public int Inflate(byte[] bytes, int offset, int length)
		{
			if (length == 0)
			{
				return 0;
			}
			int result;
			try
			{
				int num;
				ZLibNative.ErrorCode errorCode = this.ReadInflateOutput(bytes, offset, length, ZLibNative.FlushCode.NoFlush, out num);
				if (errorCode == ZLibNative.ErrorCode.StreamEnd)
				{
					this._finished = true;
				}
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

		// Token: 0x060027A4 RID: 10148 RVA: 0x000B6734 File Offset: 0x000B4934
		public bool NeedsInput()
		{
			return this._zlibStream.AvailIn == 0U;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000B6744 File Offset: 0x000B4944
		public void SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			if (count == 0)
			{
				return;
			}
			object syncLock = this._syncLock;
			lock (syncLock)
			{
				this._inputBufferHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
				this._isValid = 1;
				this._zlibStream.NextIn = this._inputBufferHandle.AddrOfPinnedObject() + startIndex;
				this._zlibStream.AvailIn = (uint)count;
				this._finished = false;
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000B67C8 File Offset: 0x000B49C8
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

		// Token: 0x060027A7 RID: 10151 RVA: 0x000B67FA File Offset: 0x000B49FA
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000B680C File Offset: 0x000B4A0C
		~InflaterZlib()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000B6844 File Offset: 0x000B4A44
		[SecuritySafeCritical]
		private void InflateInit(int windowBits)
		{
			ZLibNative.ErrorCode zlibErrorCode;
			try
			{
				zlibErrorCode = ZLibNative.CreateZLibStreamForInflate(out this._zlibStream, windowBits);
			}
			catch (Exception inner)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), inner);
			}
			switch (zlibErrorCode)
			{
			case ZLibNative.ErrorCode.VersionError:
				throw new ZLibException(SR.GetString("ZLibErrorVersionMismatch"), "inflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.MemError:
				throw new ZLibException(SR.GetString("ZLibErrorNotEnoughMemory"), "inflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorIncorrectInitParameters"), "inflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
				return;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "inflateInit2_", (int)zlibErrorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000B6930 File Offset: 0x000B4B30
		private unsafe ZLibNative.ErrorCode ReadInflateOutput(byte[] outputBuffer, int offset, int length, ZLibNative.FlushCode flushCode, out int bytesRead)
		{
			object syncLock = this._syncLock;
			ZLibNative.ErrorCode result;
			lock (syncLock)
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
				this._zlibStream.NextOut = (IntPtr)((void*)value) + offset;
				this._zlibStream.AvailOut = (uint)length;
				ZLibNative.ErrorCode errorCode = this.Inflate(flushCode);
				bytesRead = length - (int)this._zlibStream.AvailOut;
				result = errorCode;
			}
			return result;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000B69C8 File Offset: 0x000B4BC8
		[SecuritySafeCritical]
		private ZLibNative.ErrorCode Inflate(ZLibNative.FlushCode flushCode)
		{
			ZLibNative.ErrorCode errorCode;
			try
			{
				errorCode = this._zlibStream.Inflate(flushCode);
			}
			catch (Exception inner)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), inner);
			}
			switch (errorCode)
			{
			case ZLibNative.ErrorCode.BufError:
				return errorCode;
			case ZLibNative.ErrorCode.MemError:
				throw new ZLibException(SR.GetString("ZLibErrorNotEnoughMemory"), "inflate_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.DataError:
				throw new InvalidDataException(SR.GetString("GenericInvalidData"));
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorInconsistentStream"), "inflate_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
			case ZLibNative.ErrorCode.StreamEnd:
				return errorCode;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "inflate_", (int)errorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000B6AA4 File Offset: 0x000B4CA4
		private void DeallocateInputBufferHandle()
		{
			object syncLock = this._syncLock;
			lock (syncLock)
			{
				this._zlibStream.AvailIn = 0U;
				this._zlibStream.NextIn = ZLibNative.ZNullPtr;
				if (Interlocked.Exchange(ref this._isValid, 0) != 0)
				{
					this._inputBufferHandle.Free();
				}
			}
		}

		// Token: 0x04002186 RID: 8582
		private bool _finished;

		// Token: 0x04002187 RID: 8583
		private bool _isDisposed;

		// Token: 0x04002188 RID: 8584
		private ZLibNative.ZLibStreamHandle _zlibStream;

		// Token: 0x04002189 RID: 8585
		private GCHandle _inputBufferHandle;

		// Token: 0x0400218A RID: 8586
		private readonly object _syncLock = new object();

		// Token: 0x0400218B RID: 8587
		private int _isValid;
	}
}
