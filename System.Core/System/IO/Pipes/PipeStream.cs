using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000B6 RID: 182
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class PipeStream : Stream
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		protected PipeStream(PipeDirection direction, int bufferSize)
		{
			if (direction < PipeDirection.In || direction > PipeDirection.InOut)
			{
				throw new ArgumentOutOfRangeException("direction", SR.GetString("ArgumentOutOfRange_DirectionModeInOutOrInOut"));
			}
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.Init(direction, PipeTransmissionMode.Byte, bufferSize);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000F924 File Offset: 0x0000DB24
		protected PipeStream(PipeDirection direction, PipeTransmissionMode transmissionMode, int outBufferSize)
		{
			if (direction < PipeDirection.In || direction > PipeDirection.InOut)
			{
				throw new ArgumentOutOfRangeException("direction", SR.GetString("ArgumentOutOfRange_DirectionModeInOutOrInOut"));
			}
			if (transmissionMode < PipeTransmissionMode.Byte || transmissionMode > PipeTransmissionMode.Message)
			{
				throw new ArgumentOutOfRangeException("transmissionMode", SR.GetString("ArgumentOutOfRange_TransmissionModeByteOrMsg"));
			}
			if (outBufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("outBufferSize", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.Init(direction, transmissionMode, outBufferSize);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000F994 File Offset: 0x0000DB94
		private void Init(PipeDirection direction, PipeTransmissionMode transmissionMode, int outBufferSize)
		{
			this.m_readMode = transmissionMode;
			this.m_transmissionMode = transmissionMode;
			this.m_pipeDirection = direction;
			if ((this.m_pipeDirection & PipeDirection.In) != (PipeDirection)0)
			{
				this.m_canRead = true;
			}
			if ((this.m_pipeDirection & PipeDirection.Out) != (PipeDirection)0)
			{
				this.m_canWrite = true;
			}
			this.m_outBufferSize = outBufferSize;
			this.m_isMessageComplete = true;
			this.m_state = PipeState.WaitingToConnect;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		[SecurityCritical]
		protected void InitializeHandle(SafePipeHandle handle, bool isExposed, bool isAsync)
		{
			isAsync &= PipeStream._canUseAsync;
			if (isAsync)
			{
				bool flag = false;
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					flag = ThreadPool.BindHandle(handle);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (!flag)
				{
					throw new IOException(SR.GetString("IO_IO_BindHandleFailed"));
				}
			}
			this.m_handle = handle;
			this.m_isAsync = isAsync;
			this.m_isHandleExposed = isExposed;
			this.m_isFromExistingHandle = isExposed;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000FA64 File Offset: 0x0000DC64
		[SecurityCritical]
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			this.CheckReadOperations();
			return this.ReadCore(buffer, offset, count);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		[SecurityCritical]
		private int ReadCore(byte[] buffer, int offset, int count)
		{
			if (this.m_isAsync)
			{
				IAsyncResult asyncResult = this.BeginReadCore(buffer, offset, count, null, null);
				return this.EndRead(asyncResult);
			}
			int num = 0;
			int num2 = this.ReadFileNative(this.m_handle, buffer, offset, count, null, out num);
			if (num2 == -1)
			{
				if (num == 109 || num == 233)
				{
					this.State = PipeState.Broken;
					num2 = 0;
				}
				else
				{
					__Error.WinIOError(num, string.Empty);
				}
			}
			this.m_isMessageComplete = (num != 234);
			return num2;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000FB68 File Offset: 0x0000DD68
		[SecurityCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			this.CheckReadOperations();
			if (this.m_isAsync)
			{
				return this.BeginReadCore(buffer, offset, count, callback, state);
			}
			if (this.m_state == PipeState.Broken)
			{
				PipeStreamAsyncResult pipeStreamAsyncResult = new PipeStreamAsyncResult();
				pipeStreamAsyncResult._handle = this.m_handle;
				pipeStreamAsyncResult._userCallback = callback;
				pipeStreamAsyncResult._userStateObject = state;
				pipeStreamAsyncResult._isWrite = false;
				pipeStreamAsyncResult.CallUserCallback();
				return pipeStreamAsyncResult;
			}
			return base.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000FC48 File Offset: 0x0000DE48
		[SecurityCritical]
		private unsafe PipeStreamAsyncResult BeginReadCore(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			PipeStreamAsyncResult pipeStreamAsyncResult = new PipeStreamAsyncResult();
			pipeStreamAsyncResult._handle = this.m_handle;
			pipeStreamAsyncResult._userCallback = callback;
			pipeStreamAsyncResult._userStateObject = state;
			pipeStreamAsyncResult._isWrite = false;
			if (buffer.Length == 0)
			{
				pipeStreamAsyncResult.CallUserCallback();
			}
			else
			{
				ManualResetEvent waitHandle = new ManualResetEvent(false);
				pipeStreamAsyncResult._waitHandle = waitHandle;
				Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, pipeStreamAsyncResult);
				NativeOverlapped* ptr = overlapped.Pack(PipeStream.IOCallback, buffer);
				pipeStreamAsyncResult._overlapped = ptr;
				int num = 0;
				int num2 = this.ReadFileNative(this.m_handle, buffer, offset, count, ptr, out num);
				if (num2 == -1)
				{
					if (num == 109 || num == 233)
					{
						this.State = PipeState.Broken;
						ptr->InternalLow = IntPtr.Zero;
						pipeStreamAsyncResult.CallUserCallback();
					}
					else if (num != 997)
					{
						__Error.WinIOError(num, string.Empty);
					}
				}
			}
			return pipeStreamAsyncResult;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000FD18 File Offset: 0x0000DF18
		[SecurityCritical]
		public unsafe override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.m_isAsync)
			{
				return base.EndRead(asyncResult);
			}
			PipeStreamAsyncResult pipeStreamAsyncResult = asyncResult as PipeStreamAsyncResult;
			if (pipeStreamAsyncResult == null || pipeStreamAsyncResult._isWrite)
			{
				__Error.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref pipeStreamAsyncResult._EndXxxCalled, 1, 0))
			{
				__Error.EndReadCalledTwice();
			}
			WaitHandle waitHandle = pipeStreamAsyncResult._waitHandle;
			if (waitHandle != null)
			{
				try
				{
					waitHandle.WaitOne();
				}
				finally
				{
					waitHandle.Close();
				}
			}
			NativeOverlapped* overlapped = pipeStreamAsyncResult._overlapped;
			if (overlapped != null)
			{
				Overlapped.Free(overlapped);
			}
			if (pipeStreamAsyncResult._errorCode != 0)
			{
				this.WinIOError(pipeStreamAsyncResult._errorCode);
			}
			this.m_isMessageComplete = (this.m_state == PipeState.Broken || pipeStreamAsyncResult._isMessageComplete);
			return pipeStreamAsyncResult._numBytes;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		[SecurityCritical]
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			this.CheckWriteOperations();
			this.WriteCore(buffer, offset, count);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000FE6C File Offset: 0x0000E06C
		[SecurityCritical]
		private void WriteCore(byte[] buffer, int offset, int count)
		{
			if (this.m_isAsync)
			{
				IAsyncResult asyncResult = this.BeginWriteCore(buffer, offset, count, null, null);
				this.EndWrite(asyncResult);
				return;
			}
			int errorCode = 0;
			int num = this.WriteFileNative(this.m_handle, buffer, offset, count, null, out errorCode);
			if (num == -1)
			{
				this.WinIOError(errorCode);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		[SecurityCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			this.CheckWriteOperations();
			if (!this.m_isAsync)
			{
				return base.BeginWrite(buffer, offset, count, callback, state);
			}
			return this.BeginWriteCore(buffer, offset, count, callback, state);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000FF60 File Offset: 0x0000E160
		[SecurityCritical]
		private unsafe PipeStreamAsyncResult BeginWriteCore(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			PipeStreamAsyncResult pipeStreamAsyncResult = new PipeStreamAsyncResult();
			pipeStreamAsyncResult._userCallback = callback;
			pipeStreamAsyncResult._userStateObject = state;
			pipeStreamAsyncResult._isWrite = true;
			pipeStreamAsyncResult._handle = this.m_handle;
			if (buffer.Length == 0)
			{
				pipeStreamAsyncResult.CallUserCallback();
			}
			else
			{
				ManualResetEvent waitHandle = new ManualResetEvent(false);
				pipeStreamAsyncResult._waitHandle = waitHandle;
				Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, pipeStreamAsyncResult);
				NativeOverlapped* ptr = overlapped.Pack(PipeStream.IOCallback, buffer);
				pipeStreamAsyncResult._overlapped = ptr;
				int num = 0;
				int num2 = this.WriteFileNative(this.m_handle, buffer, offset, count, ptr, out num);
				if (num2 == -1 && num != 997)
				{
					if (ptr != null)
					{
						Overlapped.Free(ptr);
					}
					this.WinIOError(num);
				}
			}
			return pipeStreamAsyncResult;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001000C File Offset: 0x0000E20C
		[SecurityCritical]
		public unsafe override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.m_isAsync)
			{
				base.EndWrite(asyncResult);
				return;
			}
			PipeStreamAsyncResult pipeStreamAsyncResult = asyncResult as PipeStreamAsyncResult;
			if (pipeStreamAsyncResult == null || !pipeStreamAsyncResult._isWrite)
			{
				__Error.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref pipeStreamAsyncResult._EndXxxCalled, 1, 0))
			{
				__Error.EndWriteCalledTwice();
			}
			WaitHandle waitHandle = pipeStreamAsyncResult._waitHandle;
			if (waitHandle != null)
			{
				try
				{
					waitHandle.WaitOne();
				}
				finally
				{
					waitHandle.Close();
				}
			}
			NativeOverlapped* overlapped = pipeStreamAsyncResult._overlapped;
			if (overlapped != null)
			{
				Overlapped.Free(overlapped);
			}
			if (pipeStreamAsyncResult._errorCode != 0)
			{
				this.WinIOError(pipeStreamAsyncResult._errorCode);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000100B4 File Offset: 0x0000E2B4
		[SecurityCritical]
		private unsafe int ReadFileNative(SafePipeHandle handle, byte[] buffer, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (buffer.Length == 0)
			{
				hr = 0;
				return 0;
			}
			int result = 0;
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.m_isAsync)
				{
					num = UnsafeNativeMethods.ReadFile(handle, ptr + offset, count, IntPtr.Zero, overlapped);
				}
				else
				{
					num = UnsafeNativeMethods.ReadFile(handle, ptr + offset, count, out result, IntPtr.Zero);
				}
			}
			if (num != 0)
			{
				hr = 0;
				return result;
			}
			hr = Marshal.GetLastWin32Error();
			if (hr == 234)
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00010138 File Offset: 0x0000E338
		[SecurityCritical]
		private unsafe int WriteFileNative(SafePipeHandle handle, byte[] buffer, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (buffer.Length == 0)
			{
				hr = 0;
				return 0;
			}
			int result = 0;
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.m_isAsync)
				{
					num = UnsafeNativeMethods.WriteFile(handle, ptr + offset, count, IntPtr.Zero, overlapped);
				}
				else
				{
					num = UnsafeNativeMethods.WriteFile(handle, ptr + offset, count, out result, IntPtr.Zero);
				}
			}
			if (num == 0)
			{
				hr = Marshal.GetLastWin32Error();
				return -1;
			}
			hr = 0;
			return result;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000101B0 File Offset: 0x0000E3B0
		[SecurityCritical]
		public override int ReadByte()
		{
			this.CheckReadOperations();
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			byte[] array = new byte[1];
			if (this.ReadCore(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000101EC File Offset: 0x0000E3EC
		[SecurityCritical]
		public override void WriteByte(byte value)
		{
			this.CheckWriteOperations();
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			this.WriteCore(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00010220 File Offset: 0x0000E420
		[SecurityCritical]
		public override void Flush()
		{
			this.CheckWriteOperations();
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010235 File Offset: 0x0000E435
		[SecurityCritical]
		public void WaitForPipeDrain()
		{
			this.CheckWriteOperations();
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (!UnsafeNativeMethods.FlushFileBuffers(this.m_handle))
			{
				this.WinIOError(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00010264 File Offset: 0x0000E464
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.m_handle != null && !this.m_handle.IsClosed)
				{
					this.m_handle.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
			this.m_state = PipeState.Closed;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000102B4 File Offset: 0x0000E4B4
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x000102BF File Offset: 0x0000E4BF
		public bool IsConnected
		{
			get
			{
				return this.State == PipeState.Connected;
			}
			protected set
			{
				this.m_state = (value ? PipeState.Connected : PipeState.Disconnected);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000102CE File Offset: 0x0000E4CE
		public bool IsAsync
		{
			get
			{
				return this.m_isAsync;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public bool IsMessageComplete
		{
			[SecurityCritical]
			get
			{
				if (this.m_state == PipeState.WaitingToConnect)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotYetConnected"));
				}
				if (this.m_state == PipeState.Disconnected)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeDisconnected"));
				}
				if (this.m_handle == null)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
				}
				if (this.m_state == PipeState.Closed)
				{
					__Error.PipeNotOpen();
				}
				if (this.m_handle.IsClosed)
				{
					__Error.PipeNotOpen();
				}
				if (this.m_readMode != PipeTransmissionMode.Message)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeReadModeNotMessage"));
				}
				return this.m_isMessageComplete;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00010370 File Offset: 0x0000E570
		public virtual PipeTransmissionMode TransmissionMode
		{
			[SecurityCritical]
			get
			{
				this.CheckPipePropertyOperations();
				if (!this.m_isFromExistingHandle)
				{
					return this.m_transmissionMode;
				}
				int num;
				if (!UnsafeNativeMethods.GetNamedPipeInfo(this.m_handle, out num, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL))
				{
					this.WinIOError(Marshal.GetLastWin32Error());
				}
				if ((num & 4) != 0)
				{
					return PipeTransmissionMode.Message;
				}
				return PipeTransmissionMode.Byte;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000103C4 File Offset: 0x0000E5C4
		public virtual int InBufferSize
		{
			[SecurityCritical]
			get
			{
				this.CheckPipePropertyOperations();
				if (!this.CanRead)
				{
					throw new NotSupportedException(SR.GetString("NotSupported_UnreadableStream"));
				}
				int result;
				if (!UnsafeNativeMethods.GetNamedPipeInfo(this.m_handle, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, out result, UnsafeNativeMethods.NULL))
				{
					this.WinIOError(Marshal.GetLastWin32Error());
				}
				return result;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001041C File Offset: 0x0000E61C
		public virtual int OutBufferSize
		{
			[SecurityCritical]
			get
			{
				this.CheckPipePropertyOperations();
				if (!this.CanWrite)
				{
					throw new NotSupportedException(SR.GetString("NotSupported_UnwritableStream"));
				}
				int outBufferSize;
				if (this.m_pipeDirection == PipeDirection.Out)
				{
					outBufferSize = this.m_outBufferSize;
				}
				else if (!UnsafeNativeMethods.GetNamedPipeInfo(this.m_handle, UnsafeNativeMethods.NULL, out outBufferSize, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL))
				{
					this.WinIOError(Marshal.GetLastWin32Error());
				}
				return outBufferSize;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00010483 File Offset: 0x0000E683
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x000104A8 File Offset: 0x0000E6A8
		public unsafe virtual PipeTransmissionMode ReadMode
		{
			[SecurityCritical]
			get
			{
				this.CheckPipePropertyOperations();
				if (this.m_isFromExistingHandle || this.IsHandleExposed)
				{
					this.UpdateReadMode();
				}
				return this.m_readMode;
			}
			[SecurityCritical]
			set
			{
				this.CheckPipePropertyOperations();
				if (value < PipeTransmissionMode.Byte || value > PipeTransmissionMode.Message)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("ArgumentOutOfRange_TransmissionModeByteOrMsg"));
				}
				int num = (int)((int)value << 1);
				if (!UnsafeNativeMethods.SetNamedPipeHandleState(this.m_handle, &num, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL))
				{
					this.WinIOError(Marshal.GetLastWin32Error());
					return;
				}
				this.m_readMode = value;
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001050C File Offset: 0x0000E70C
		[SecurityCritical]
		private void UpdateReadMode()
		{
			int num;
			if (!UnsafeNativeMethods.GetNamedPipeHandleState(this.SafePipeHandle, out num, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, 0))
			{
				this.WinIOError(Marshal.GetLastWin32Error());
			}
			if ((num & 2) != 0)
			{
				this.m_readMode = PipeTransmissionMode.Message;
				return;
			}
			this.m_readMode = PipeTransmissionMode.Byte;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001055C File Offset: 0x0000E75C
		[SecurityCritical]
		public PipeSecurity GetAccessControl()
		{
			if (this.m_state == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
			if (this.m_handle == null)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
			}
			if (this.m_handle.IsClosed)
			{
				__Error.PipeNotOpen();
			}
			return new PipeSecurity(this.m_handle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000105AE File Offset: 0x0000E7AE
		[SecurityCritical]
		public void SetAccessControl(PipeSecurity pipeSecurity)
		{
			if (pipeSecurity == null)
			{
				throw new ArgumentNullException("pipeSecurity");
			}
			this.CheckPipePropertyOperations();
			pipeSecurity.Persist(this.m_handle);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000105D0 File Offset: 0x0000E7D0
		public SafePipeHandle SafePipeHandle
		{
			[SecurityCritical]
			get
			{
				if (this.m_handle == null)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
				}
				if (this.m_handle.IsClosed)
				{
					__Error.PipeNotOpen();
				}
				this.m_isHandleExposed = true;
				return this.m_handle;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00010609 File Offset: 0x0000E809
		internal SafePipeHandle InternalHandle
		{
			[SecurityCritical]
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00010611 File Offset: 0x0000E811
		protected bool IsHandleExposed
		{
			get
			{
				return this.m_isHandleExposed;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00010619 File Offset: 0x0000E819
		public override bool CanRead
		{
			get
			{
				return this.m_canRead;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00010621 File Offset: 0x0000E821
		public override bool CanWrite
		{
			get
			{
				return this.m_canWrite;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00010629 File Offset: 0x0000E829
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001062C File Offset: 0x0000E82C
		public override long Length
		{
			get
			{
				__Error.SeekNotSupported();
				return 0L;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00010635 File Offset: 0x0000E835
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001063E File Offset: 0x0000E83E
		public override long Position
		{
			get
			{
				__Error.SeekNotSupported();
				return 0L;
			}
			set
			{
				__Error.SeekNotSupported();
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00010645 File Offset: 0x0000E845
		public override void SetLength(long value)
		{
			__Error.SeekNotSupported();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001064C File Offset: 0x0000E84C
		public override long Seek(long offset, SeekOrigin origin)
		{
			__Error.SeekNotSupported();
			return 0L;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00010655 File Offset: 0x0000E855
		[SecurityCritical]
		protected internal virtual void CheckPipePropertyOperations()
		{
			if (this.m_handle == null)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
			}
			if (this.m_state == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
			if (this.m_handle.IsClosed)
			{
				__Error.PipeNotOpen();
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00010690 File Offset: 0x0000E890
		[SecurityCritical]
		protected internal void CheckReadOperations()
		{
			if (this.m_state == PipeState.WaitingToConnect)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotYetConnected"));
			}
			if (this.m_state == PipeState.Disconnected)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeDisconnected"));
			}
			if (this.m_handle == null)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
			}
			if (this.m_state == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
			if (this.m_handle.IsClosed)
			{
				__Error.PipeNotOpen();
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00010708 File Offset: 0x0000E908
		[SecurityCritical]
		protected internal void CheckWriteOperations()
		{
			if (this.m_state == PipeState.WaitingToConnect)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotYetConnected"));
			}
			if (this.m_state == PipeState.Disconnected)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeDisconnected"));
			}
			if (this.m_handle == null)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
			}
			if (this.m_state == PipeState.Broken)
			{
				throw new IOException(SR.GetString("IO_IO_PipeBroken"));
			}
			if (this.m_state == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
			if (this.m_handle.IsClosed)
			{
				__Error.PipeNotOpen();
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010798 File Offset: 0x0000E998
		[SecurityCritical]
		internal void WinIOError(int errorCode)
		{
			if (errorCode == 109 || errorCode == 233 || errorCode == 232)
			{
				this.m_state = PipeState.Broken;
				throw new IOException(SR.GetString("IO_IO_PipeBroken"), UnsafeNativeMethods.MakeHRFromErrorCode(errorCode));
			}
			if (errorCode == 38)
			{
				__Error.EndOfFile();
				return;
			}
			if (errorCode == 6)
			{
				this.m_handle.SetHandleAsInvalid();
				this.m_state = PipeState.Broken;
			}
			__Error.WinIOError(errorCode, string.Empty);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00010803 File Offset: 0x0000EA03
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001080B File Offset: 0x0000EA0B
		internal PipeState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00010814 File Offset: 0x0000EA14
		[SecurityCritical]
		internal unsafe static UnsafeNativeMethods.SECURITY_ATTRIBUTES GetSecAttrs(HandleInheritability inheritability, PipeSecurity pipeSecurity, out object pinningHandle)
		{
			pinningHandle = null;
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if ((inheritability & HandleInheritability.Inheritable) != HandleInheritability.None || pipeSecurity != null)
			{
				security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				if ((inheritability & HandleInheritability.Inheritable) != HandleInheritability.None)
				{
					security_ATTRIBUTES.bInheritHandle = 1;
				}
				if (pipeSecurity != null)
				{
					byte[] securityDescriptorBinaryForm = pipeSecurity.GetSecurityDescriptorBinaryForm();
					pinningHandle = GCHandle.Alloc(securityDescriptorBinaryForm, GCHandleType.Pinned);
					byte[] array;
					byte* pSecurityDescriptor;
					if ((array = securityDescriptorBinaryForm) == null || array.Length == 0)
					{
						pSecurityDescriptor = null;
					}
					else
					{
						pSecurityDescriptor = &array[0];
					}
					security_ATTRIBUTES.pSecurityDescriptor = pSecurityDescriptor;
					array = null;
				}
			}
			return security_ATTRIBUTES;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00010888 File Offset: 0x0000EA88
		[SecurityCritical]
		internal static UnsafeNativeMethods.SECURITY_ATTRIBUTES GetSecAttrs(HandleInheritability inheritability)
		{
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if ((inheritability & HandleInheritability.Inheritable) != HandleInheritability.None)
			{
				security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				security_ATTRIBUTES.bInheritHandle = 1;
			}
			return security_ATTRIBUTES;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000108B8 File Offset: 0x0000EAB8
		[SecurityCritical]
		private unsafe static void AsyncPSCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(pOverlapped);
			PipeStreamAsyncResult pipeStreamAsyncResult = (PipeStreamAsyncResult)overlapped.AsyncResult;
			pipeStreamAsyncResult._numBytes = (int)numBytes;
			if (!pipeStreamAsyncResult._isWrite && (errorCode == 109U || errorCode == 233U || errorCode == 232U))
			{
				errorCode = 0U;
				numBytes = 0U;
			}
			if (errorCode == 234U)
			{
				errorCode = 0U;
				pipeStreamAsyncResult._isMessageComplete = false;
			}
			else
			{
				pipeStreamAsyncResult._isMessageComplete = true;
			}
			pipeStreamAsyncResult._errorCode = (int)errorCode;
			pipeStreamAsyncResult._completedSynchronously = false;
			pipeStreamAsyncResult._isComplete = true;
			ManualResetEvent waitHandle = pipeStreamAsyncResult._waitHandle;
			if (waitHandle != null && !waitHandle.Set())
			{
				__Error.WinIOError();
			}
			AsyncCallback userCallback = pipeStreamAsyncResult._userCallback;
			if (userCallback != null)
			{
				userCallback(pipeStreamAsyncResult);
			}
		}

		// Token: 0x04000569 RID: 1385
		private static readonly bool _canUseAsync = Environment.OSVersion.Platform == PlatformID.Win32NT;

		// Token: 0x0400056A RID: 1386
		[SecurityCritical]
		private static readonly IOCompletionCallback IOCallback = new IOCompletionCallback(PipeStream.AsyncPSCallback);

		// Token: 0x0400056B RID: 1387
		private SafePipeHandle m_handle;

		// Token: 0x0400056C RID: 1388
		private bool m_canRead;

		// Token: 0x0400056D RID: 1389
		private bool m_canWrite;

		// Token: 0x0400056E RID: 1390
		private bool m_isAsync;

		// Token: 0x0400056F RID: 1391
		private bool m_isMessageComplete;

		// Token: 0x04000570 RID: 1392
		private bool m_isFromExistingHandle;

		// Token: 0x04000571 RID: 1393
		private bool m_isHandleExposed;

		// Token: 0x04000572 RID: 1394
		private PipeTransmissionMode m_readMode;

		// Token: 0x04000573 RID: 1395
		private PipeTransmissionMode m_transmissionMode;

		// Token: 0x04000574 RID: 1396
		private PipeDirection m_pipeDirection;

		// Token: 0x04000575 RID: 1397
		private int m_outBufferSize;

		// Token: 0x04000576 RID: 1398
		private PipeState m_state;
	}
}
