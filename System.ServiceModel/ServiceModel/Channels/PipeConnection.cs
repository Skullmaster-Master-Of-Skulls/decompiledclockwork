using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000846 RID: 2118
	internal sealed class PipeConnection : IConnection
	{
		// Token: 0x06004F1B RID: 20251 RVA: 0x0011FCCC File Offset: 0x0011DECC
		public PipeConnection(PipeHandle pipe, int connectionBufferSize, bool isBoundToCompletionPort, bool autoBindToCompletionPort)
		{
			if (pipe == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("pipe");
			}
			if (pipe.IsInvalid)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("pipe");
			}
			this.closeState = PipeConnection.CloseState.Open;
			this.exceptionEventType = TraceEventType.Error;
			this.isBoundToCompletionPort = isBoundToCompletionPort;
			this.autoBindToCompletionPort = autoBindToCompletionPort;
			this.pipe = pipe;
			this.readBufferSize = connectionBufferSize;
			this.writeBufferSize = connectionBufferSize;
			this.readOverlapped = new OverlappedContext();
			this.asyncReadBuffer = DiagnosticUtility.Utility.AllocateByteArray(connectionBufferSize);
			this.writeOverlapped = new OverlappedContext();
			this.atEOFEvent = new ManualResetEvent(false);
			this.onAsyncReadComplete = new OverlappedIOCompleteCallback(this.OnAsyncReadComplete);
			this.onAsyncWriteComplete = new OverlappedIOCompleteCallback(this.OnAsyncWriteComplete);
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0011FDA9 File Offset: 0x0011DFA9
		public int AsyncReadBufferSize
		{
			get
			{
				return this.readBufferSize;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x0011FDB1 File Offset: 0x0011DFB1
		public byte[] AsyncReadBuffer
		{
			get
			{
				return this.asyncReadBuffer;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x0011FDB9 File Offset: 0x0011DFB9
		private static byte[] ZeroBuffer
		{
			get
			{
				if (PipeConnection.zeroBuffer == null)
				{
					PipeConnection.zeroBuffer = new byte[1];
				}
				return PipeConnection.zeroBuffer;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0011FDD2 File Offset: 0x0011DFD2
		// (set) Token: 0x06004F20 RID: 20256 RVA: 0x0011FDDA File Offset: 0x0011DFDA
		public TraceEventType ExceptionEventType
		{
			get
			{
				return this.exceptionEventType;
			}
			set
			{
				this.exceptionEventType = value;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0011FDE3 File Offset: 0x0011DFE3
		public IPEndPoint RemoteIPEndPoint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x0011FDE6 File Offset: 0x0011DFE6
		private IOThreadTimer ReadTimer
		{
			get
			{
				if (this.readTimer == null)
				{
					if (PipeConnection.onReadTimeout == null)
					{
						PipeConnection.onReadTimeout = new Action<object>(PipeConnection.OnReadTimeout);
					}
					this.readTimer = new IOThreadTimer(PipeConnection.onReadTimeout, this, false);
				}
				return this.readTimer;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x0011FE20 File Offset: 0x0011E020
		private IOThreadTimer WriteTimer
		{
			get
			{
				if (this.writeTimer == null)
				{
					if (PipeConnection.onWriteTimeout == null)
					{
						PipeConnection.onWriteTimeout = new Action<object>(PipeConnection.OnWriteTimeout);
					}
					this.writeTimer = new IOThreadTimer(PipeConnection.onWriteTimeout, this, false);
				}
				return this.writeTimer;
			}
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x0011FE5C File Offset: 0x0011E05C
		private static void OnReadTimeout(object state)
		{
			PipeConnection pipeConnection = (PipeConnection)state;
			pipeConnection.Abort(SR.GetString("PipeConnectionAbortedReadTimedOut", new object[]
			{
				pipeConnection.readTimeout
			}), PipeConnection.TransferOperation.Read);
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0011FE98 File Offset: 0x0011E098
		private static void OnWriteTimeout(object state)
		{
			PipeConnection pipeConnection = (PipeConnection)state;
			pipeConnection.Abort(SR.GetString("PipeConnectionAbortedWriteTimedOut", new object[]
			{
				pipeConnection.writeTimeout
			}), PipeConnection.TransferOperation.Write);
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0011FED1 File Offset: 0x0011E0D1
		public void Abort()
		{
			this.Abort(null, PipeConnection.TransferOperation.Undefined);
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0011FEDB File Offset: 0x0011E0DB
		private void Abort(string timeoutErrorString, PipeConnection.TransferOperation transferOperation)
		{
			this.CloseHandle(true, timeoutErrorString, transferOperation);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0011FEE6 File Offset: 0x0011E0E6
		private Exception ConvertPipeException(PipeException pipeException, PipeConnection.TransferOperation transferOperation)
		{
			return this.ConvertPipeException(pipeException.Message, pipeException, transferOperation);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0011FEF8 File Offset: 0x0011E0F8
		private Exception ConvertPipeException(string exceptionMessage, PipeException pipeException, PipeConnection.TransferOperation transferOperation)
		{
			if (this.timeoutErrorString != null)
			{
				if (transferOperation == this.timeoutErrorTransferOperation)
				{
					return new TimeoutException(this.timeoutErrorString, pipeException);
				}
				return new CommunicationException(this.timeoutErrorString, pipeException);
			}
			else
			{
				if (this.aborted)
				{
					return new CommunicationObjectAbortedException(exceptionMessage, pipeException);
				}
				return new CommunicationException(exceptionMessage, pipeException);
			}
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0011FF48 File Offset: 0x0011E148
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			ConnectionUtilities.ValidateBufferBounds(this.AsyncReadBuffer, offset, size);
			object obj = this.readLock;
			AsyncCompletionResult result;
			lock (obj)
			{
				try
				{
					this.ValidateEnterReadingState(true);
					if (this.isAtEOF)
					{
						this.asyncBytesRead = 0;
						this.asyncReadException = null;
						result = AsyncCompletionResult.Completed;
					}
					else
					{
						if (this.autoBindToCompletionPort && !this.isBoundToCompletionPort)
						{
							object obj2 = this.writeLock;
							lock (obj2)
							{
								this.EnsureBoundToCompletionPort();
							}
						}
						if (this.isReadOutstanding)
						{
							throw Fx.AssertAndThrow("Read I/O already pending when BeginRead called.");
						}
						try
						{
							this.readTimeout = timeout;
							if (this.readTimeout != TimeSpan.MaxValue)
							{
								this.ReadTimer.Set(this.readTimeout);
							}
							this.asyncReadCallback = callback;
							this.asyncReadCallbackState = state;
							this.isReadOutstanding = true;
							this.readOverlapped.StartAsyncOperation(this.AsyncReadBuffer, this.onAsyncReadComplete, this.isBoundToCompletionPort);
							if (UnsafeNativeMethods.ReadFile(this.pipe.DangerousGetHandle(), this.readOverlapped.BufferPtr + offset, size, IntPtr.Zero, this.readOverlapped.NativeOverlapped) == 0)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								if (lastWin32Error != 997 && lastWin32Error != 234)
								{
									this.isReadOutstanding = false;
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeConnection.Exceptions.CreateReadException(lastWin32Error));
								}
							}
						}
						finally
						{
							if (!this.isReadOutstanding)
							{
								this.readOverlapped.CancelAsyncOperation();
								this.asyncReadCallback = null;
								this.asyncReadCallbackState = null;
								this.ReadTimer.Cancel();
							}
						}
						if (!this.isReadOutstanding)
						{
							int num;
							Exception overlappedReadException = PipeConnection.Exceptions.GetOverlappedReadException(this.pipe, this.readOverlapped.NativeOverlapped, out num);
							if (overlappedReadException != null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(overlappedReadException);
							}
							this.asyncBytesRead = num;
							this.HandleReadComplete(this.asyncBytesRead);
						}
						else
						{
							this.EnterReadingState();
						}
						result = (this.isReadOutstanding ? AsyncCompletionResult.Queued : AsyncCompletionResult.Completed);
					}
				}
				catch (PipeException pipeException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Read), this.ExceptionEventType);
				}
			}
			return result;
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x001201BC File Offset: 0x0011E3BC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.FinishPendingWrite(timeout);
			ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
			if (this.autoBindToCompletionPort && !this.isBoundToCompletionPort)
			{
				object obj = this.readLock;
				lock (obj)
				{
					object obj2 = this.writeLock;
					lock (obj2)
					{
						this.ValidateEnterWritingState(true);
						this.EnsureBoundToCompletionPort();
					}
				}
			}
			object obj3 = this.writeLock;
			AsyncCompletionResult result;
			lock (obj3)
			{
				try
				{
					this.ValidateEnterWritingState(true);
					if (this.isWriteOutstanding)
					{
						throw Fx.AssertAndThrow("Write I/O already pending when BeginWrite called.");
					}
					try
					{
						this.writeTimeout = timeout;
						this.WriteTimer.Set(timeoutHelper.RemainingTime());
						this.asyncBytesToWrite = size;
						this.asyncWriteException = null;
						this.asyncWriteCallback = callback;
						this.asyncWriteCallbackState = state;
						this.isWriteOutstanding = true;
						this.writeOverlapped.StartAsyncOperation(buffer, this.onAsyncWriteComplete, this.isBoundToCompletionPort);
						if (UnsafeNativeMethods.WriteFile(this.pipe.DangerousGetHandle(), this.writeOverlapped.BufferPtr + offset, size, IntPtr.Zero, this.writeOverlapped.NativeOverlapped) == 0)
						{
							int lastWin32Error = Marshal.GetLastWin32Error();
							if (lastWin32Error != 997)
							{
								this.isWriteOutstanding = false;
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeConnection.Exceptions.CreateWriteException(lastWin32Error));
							}
						}
					}
					finally
					{
						if (!this.isWriteOutstanding)
						{
							this.writeOverlapped.CancelAsyncOperation();
							this.ResetWriteState();
							this.WriteTimer.Cancel();
						}
					}
					if (!this.isWriteOutstanding)
					{
						int num;
						Exception ex = PipeConnection.Exceptions.GetOverlappedWriteException(this.pipe, this.writeOverlapped.NativeOverlapped, out num);
						if (ex == null && num != size)
						{
							ex = new PipeException(SR.GetString("PipeWriteIncomplete"));
						}
						if (ex != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
						}
					}
					else
					{
						this.EnterWritingState();
					}
					result = (this.isWriteOutstanding ? AsyncCompletionResult.Queued : AsyncCompletionResult.Completed);
				}
				catch (PipeException pipeException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Write), this.ExceptionEventType);
				}
			}
			return result;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0012044C File Offset: 0x0011E64C
		public void Close(TimeSpan timeout, bool asyncAndLinger)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.FinishPendingWrite(timeout);
			bool flag = false;
			try
			{
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				object obj = this.readLock;
				lock (obj)
				{
					object obj2 = this.writeLock;
					lock (obj2)
					{
						if (!this.isShutdownWritten && this.inWritingState)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeCantCloseWithPendingWrite")), this.ExceptionEventType);
						}
						if (this.closeState == PipeConnection.CloseState.Closing || this.closeState == PipeConnection.CloseState.HandleClosed)
						{
							return;
						}
						this.closeState = PipeConnection.CloseState.Closing;
						flag = true;
						if (!this.isAtEOF)
						{
							if (this.inReadingState)
							{
								flag2 = true;
							}
							else
							{
								flag3 = true;
							}
						}
						if (!this.isShutdownWritten)
						{
							flag4 = true;
							this.isShutdownWritten = true;
						}
					}
				}
				if (flag4)
				{
					this.StartWriteZero(timeoutHelper.RemainingTime());
				}
				if (flag3)
				{
					this.StartReadZero();
				}
				try
				{
					this.WaitForWriteZero(timeoutHelper.RemainingTime(), true);
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("PipeShutdownWriteError"), innerException), this.ExceptionEventType);
				}
				if (flag3)
				{
					try
					{
						this.WaitForReadZero(timeoutHelper.RemainingTime(), true);
						goto IL_191;
					}
					catch (TimeoutException innerException2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("PipeShutdownReadError"), innerException2), this.ExceptionEventType);
					}
				}
				if (flag2 && !TimeoutHelper.WaitOne(this.atEOFEvent, timeoutHelper.RemainingTime()))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("PipeShutdownReadError")), this.ExceptionEventType);
				}
				IL_191:
				try
				{
					this.StartWriteZero(timeoutHelper.RemainingTime());
					this.StartReadZero();
					this.WaitForWriteZero(timeoutHelper.RemainingTime(), false);
					this.WaitForReadZero(timeoutHelper.RemainingTime(), false);
				}
				catch (PipeException ex)
				{
					if (!this.IsBrokenPipeError(ex.ErrorCode))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
			}
			catch (TimeoutException innerException3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("PipeCloseFailed"), innerException3), this.ExceptionEventType);
			}
			catch (PipeException pipeException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(SR.GetString("PipeCloseFailed"), pipeException, PipeConnection.TransferOperation.Undefined), this.ExceptionEventType);
			}
			finally
			{
				if (flag)
				{
					this.CloseHandle(false, null, PipeConnection.TransferOperation.Undefined);
				}
			}
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x001207A0 File Offset: 0x0011E9A0
		private void CloseHandle(bool abort, string timeoutErrorString, PipeConnection.TransferOperation transferOperation)
		{
			object obj = this.readLock;
			lock (obj)
			{
				object obj2 = this.writeLock;
				lock (obj2)
				{
					if (this.closeState == PipeConnection.CloseState.HandleClosed)
					{
						return;
					}
					this.timeoutErrorString = timeoutErrorString;
					this.timeoutErrorTransferOperation = transferOperation;
					this.aborted = abort;
					this.closeState = PipeConnection.CloseState.HandleClosed;
					this.pipe.Close();
					this.readOverlapped.FreeOrDefer();
					this.writeOverlapped.FreeOrDefer();
					if (this.atEOFEvent != null)
					{
						this.atEOFEvent.Close();
					}
					try
					{
						this.FinishPendingWrite(TimeSpan.Zero);
					}
					catch (TimeoutException ex)
					{
						if (TD.CloseTimeoutIsEnabled())
						{
							TD.CloseTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
				}
			}
			if (abort)
			{
				TraceEventType traceEventType = TraceEventType.Warning;
				if (this.ExceptionEventType == TraceEventType.Information)
				{
					traceEventType = this.ExceptionEventType;
				}
				if (DiagnosticUtility.ShouldTrace(traceEventType))
				{
					TraceUtility.TraceEvent(traceEventType, 262173, SR.GetString("TraceCodePipeConnectionAbort"), this);
				}
			}
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x001208E8 File Offset: 0x0011EAE8
		private CommunicationException CreatePipeDuplicationFailedException(int win32Error)
		{
			Exception ex = new PipeException(SR.GetString("PipeDuplicationFailed"), win32Error);
			return new CommunicationException(ex.Message, ex);
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x00120914 File Offset: 0x0011EB14
		public object DuplicateAndClose(int targetProcessId)
		{
			SafeCloseHandle safeCloseHandle = ListenerUnsafeNativeMethods.OpenProcess(64, false, targetProcessId);
			if (safeCloseHandle.IsInvalid)
			{
				safeCloseHandle.SetHandleAsInvalid();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.CreatePipeDuplicationFailedException(Marshal.GetLastWin32Error()), this.ExceptionEventType);
			}
			object result;
			try
			{
				IntPtr currentProcess = ListenerUnsafeNativeMethods.GetCurrentProcess();
				if (currentProcess == IntPtr.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.CreatePipeDuplicationFailedException(Marshal.GetLastWin32Error()), this.ExceptionEventType);
				}
				IntPtr intPtr;
				if (!UnsafeNativeMethods.DuplicateHandle(currentProcess, this.pipe, safeCloseHandle, out intPtr, 0, false, 2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.CreatePipeDuplicationFailedException(Marshal.GetLastWin32Error()), this.ExceptionEventType);
				}
				this.Abort();
				result = intPtr;
			}
			finally
			{
				safeCloseHandle.Close();
			}
			return result;
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001209E0 File Offset: 0x0011EBE0
		public object GetCoreTransport()
		{
			return this.pipe;
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x001209E8 File Offset: 0x0011EBE8
		private void EnsureBoundToCompletionPort()
		{
			if (!this.isBoundToCompletionPort)
			{
				ThreadPool.BindHandle(this.pipe);
				this.isBoundToCompletionPort = true;
			}
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x00120A08 File Offset: 0x0011EC08
		public int EndRead()
		{
			if (this.asyncReadException != null)
			{
				Exception exception = this.asyncReadException;
				this.asyncReadException = null;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(exception, this.ExceptionEventType);
			}
			return this.asyncBytesRead;
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x00120A44 File Offset: 0x0011EC44
		public void EndWrite()
		{
			if (this.asyncWriteException != null)
			{
				Exception exception = this.asyncWriteException;
				this.asyncWriteException = null;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(exception, this.ExceptionEventType);
			}
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00120A79 File Offset: 0x0011EC79
		private void EnterReadingState()
		{
			this.inReadingState = true;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00120A82 File Offset: 0x0011EC82
		private void EnterWritingState()
		{
			this.inWritingState = true;
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x00120A8B File Offset: 0x0011EC8B
		private void ExitReadingState()
		{
			this.inReadingState = false;
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x00120A94 File Offset: 0x0011EC94
		private void ExitWritingState()
		{
			this.inWritingState = false;
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00120A9D File Offset: 0x0011EC9D
		private void ReadIOCompleted()
		{
			this.readOverlapped.FreeIfDeferred();
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00120AAB File Offset: 0x0011ECAB
		private void WriteIOCompleted()
		{
			this.writeOverlapped.FreeIfDeferred();
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00120ABC File Offset: 0x0011ECBC
		private void FinishPendingWrite(TimeSpan timeout)
		{
			if (this.pendingWriteBuffer == null)
			{
				return;
			}
			object obj = this.writeLock;
			byte[] buffer;
			BufferManager bufferManager;
			lock (obj)
			{
				if (this.pendingWriteBuffer == null)
				{
					return;
				}
				buffer = this.pendingWriteBuffer;
				this.pendingWriteBuffer = null;
				bufferManager = this.pendingWriteBufferManager;
				this.pendingWriteBufferManager = null;
			}
			try
			{
				bool flag2 = false;
				try
				{
					this.WaitForSyncWrite(timeout, true);
					flag2 = true;
				}
				finally
				{
					object obj2 = this.writeLock;
					lock (obj2)
					{
						try
						{
							if (flag2)
							{
								this.FinishSyncWrite(true);
							}
						}
						finally
						{
							this.ExitWritingState();
							if (!this.isWriteOutstanding)
							{
								bufferManager.ReturnBuffer(buffer);
								this.WriteIOCompleted();
							}
						}
					}
				}
			}
			catch (PipeException pipeException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Write), this.ExceptionEventType);
			}
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x00120BD4 File Offset: 0x0011EDD4
		private void HandleReadComplete(int bytesRead)
		{
			if (bytesRead == 0)
			{
				this.isAtEOF = true;
				this.atEOFEvent.Set();
			}
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x00120BEC File Offset: 0x0011EDEC
		private bool IsBrokenPipeError(int error)
		{
			return error == 232 || error == 109;
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x00120BFD File Offset: 0x0011EDFD
		private Exception CreatePipeClosedException(PipeConnection.TransferOperation transferOperation)
		{
			return this.ConvertPipeException(new PipeException(SR.GetString("PipeClosed")), transferOperation);
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x00120C18 File Offset: 0x0011EE18
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void OnAsyncReadComplete(bool haveResult, int error, int numBytes)
		{
			object obj = this.readLock;
			WaitCallback waitCallback;
			object state;
			lock (obj)
			{
				try
				{
					try
					{
						if (this.readTimeout != TimeSpan.MaxValue && !this.ReadTimer.Cancel())
						{
							this.Abort(SR.GetString("PipeConnectionAbortedReadTimedOut", new object[]
							{
								this.readTimeout
							}), PipeConnection.TransferOperation.Read);
						}
						if (this.closeState == PipeConnection.CloseState.HandleClosed)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreatePipeClosedException(PipeConnection.TransferOperation.Read));
						}
						if (!haveResult)
						{
							if (UnsafeNativeMethods.GetOverlappedResult(this.pipe.DangerousGetHandle(), this.readOverlapped.NativeOverlapped, out numBytes, 0) == 0)
							{
								error = Marshal.GetLastWin32Error();
							}
							else
							{
								error = 0;
							}
						}
						if (error != 0 && error != 234)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeConnection.Exceptions.CreateReadException(error));
						}
						this.asyncBytesRead = numBytes;
						this.HandleReadComplete(this.asyncBytesRead);
					}
					catch (PipeException pipeException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Read));
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.asyncReadException = exception;
				}
				finally
				{
					this.isReadOutstanding = false;
					this.ReadIOCompleted();
					this.ExitReadingState();
					waitCallback = this.asyncReadCallback;
					this.asyncReadCallback = null;
					state = this.asyncReadCallbackState;
					this.asyncReadCallbackState = null;
				}
			}
			waitCallback(state);
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x00120DCC File Offset: 0x0011EFCC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void OnAsyncWriteComplete(bool haveResult, int error, int numBytes)
		{
			Exception ex = null;
			this.WriteTimer.Cancel();
			object obj = this.writeLock;
			WaitCallback waitCallback;
			object state;
			lock (obj)
			{
				try
				{
					try
					{
						if (this.closeState == PipeConnection.CloseState.HandleClosed)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreatePipeClosedException(PipeConnection.TransferOperation.Write));
						}
						if (!haveResult)
						{
							if (UnsafeNativeMethods.GetOverlappedResult(this.pipe.DangerousGetHandle(), this.writeOverlapped.NativeOverlapped, out numBytes, 0) == 0)
							{
								error = Marshal.GetLastWin32Error();
							}
							else
							{
								error = 0;
							}
						}
						if (error != 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeConnection.Exceptions.CreateWriteException(error));
						}
						if (numBytes != this.asyncBytesToWrite)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PipeException(SR.GetString("PipeWriteIncomplete")));
						}
					}
					catch (PipeException pipeException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Write), this.ExceptionEventType);
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				finally
				{
					this.isWriteOutstanding = false;
					this.WriteIOCompleted();
					this.ExitWritingState();
					this.asyncWriteException = ex;
					waitCallback = this.asyncWriteCallback;
					state = this.asyncWriteCallbackState;
					this.ResetWriteState();
				}
			}
			if (waitCallback != null)
			{
				waitCallback(state);
			}
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00120F24 File Offset: 0x0011F124
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
			int result;
			try
			{
				object obj = this.readLock;
				lock (obj)
				{
					this.ValidateEnterReadingState(true);
					if (this.isAtEOF)
					{
						return 0;
					}
					this.StartSyncRead(buffer, offset, size);
					this.EnterReadingState();
				}
				int num = -1;
				bool flag2 = false;
				try
				{
					this.WaitForSyncRead(timeout, true);
					flag2 = true;
				}
				finally
				{
					object obj2 = this.readLock;
					lock (obj2)
					{
						try
						{
							if (flag2)
							{
								num = this.FinishSyncRead(true);
								this.HandleReadComplete(num);
							}
						}
						finally
						{
							this.ExitReadingState();
							if (!this.isReadOutstanding)
							{
								this.ReadIOCompleted();
							}
						}
					}
				}
				result = num;
			}
			catch (PipeException pipeException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Read), this.ExceptionEventType);
			}
			return result;
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0012103C File Offset: 0x0011F23C
		public void Shutdown(TimeSpan timeout)
		{
			try
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.FinishPendingWrite(timeoutHelper.RemainingTime());
				object obj = this.writeLock;
				lock (obj)
				{
					this.ValidateEnterWritingState(true);
					this.StartWriteZero(timeoutHelper.RemainingTime());
					this.isShutdownWritten = true;
				}
			}
			catch (PipeException pipeException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Undefined), this.ExceptionEventType);
			}
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x001210D0 File Offset: 0x0011F2D0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void StartReadZero()
		{
			object obj = this.readLock;
			lock (obj)
			{
				this.ValidateEnterReadingState(false);
				this.StartSyncRead(PipeConnection.ZeroBuffer, 0, 1);
				this.EnterReadingState();
			}
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x00121124 File Offset: 0x0011F324
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void StartWriteZero(TimeSpan timeout)
		{
			this.FinishPendingWrite(timeout);
			object obj = this.writeLock;
			lock (obj)
			{
				this.ValidateEnterWritingState(false);
				this.StartSyncWrite(PipeConnection.ZeroBuffer, 0, 0);
				this.EnterWritingState();
			}
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x00121180 File Offset: 0x0011F380
		private void ResetWriteState()
		{
			this.asyncBytesToWrite = -1;
			this.asyncWriteCallback = null;
			this.asyncWriteCallbackState = null;
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x00121197 File Offset: 0x0011F397
		public IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<bool>(true, callback, state);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x001211A1 File Offset: 0x0011F3A1
		public bool EndValidate(IAsyncResult result)
		{
			return CompletedAsyncResult<bool>.End(result);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x001211AC File Offset: 0x0011F3AC
		private void WaitForReadZero(TimeSpan timeout, bool traceExceptionsAsErrors)
		{
			bool flag = false;
			try
			{
				this.WaitForSyncRead(timeout, traceExceptionsAsErrors);
				flag = true;
			}
			finally
			{
				object obj = this.readLock;
				lock (obj)
				{
					try
					{
						if (flag && this.FinishSyncRead(traceExceptionsAsErrors) != 0)
						{
							Exception exception = this.ConvertPipeException(new PipeException(SR.GetString("PipeSignalExpected")), PipeConnection.TransferOperation.Read);
							TraceEventType eventType = TraceEventType.Information;
							if (traceExceptionsAsErrors)
							{
								eventType = TraceEventType.Error;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelper(exception, eventType);
						}
					}
					finally
					{
						this.ExitReadingState();
						if (!this.isReadOutstanding)
						{
							this.ReadIOCompleted();
						}
					}
				}
			}
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0012125C File Offset: 0x0011F45C
		private void WaitForWriteZero(TimeSpan timeout, bool traceExceptionsAsErrors)
		{
			bool flag = false;
			try
			{
				this.WaitForSyncWrite(timeout, traceExceptionsAsErrors);
				flag = true;
			}
			finally
			{
				object obj = this.writeLock;
				lock (obj)
				{
					try
					{
						if (flag)
						{
							this.FinishSyncWrite(traceExceptionsAsErrors);
						}
					}
					finally
					{
						this.ExitWritingState();
						if (!this.isWriteOutstanding)
						{
							this.WriteIOCompleted();
						}
					}
				}
			}
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x001212DC File Offset: 0x0011F4DC
		public void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			this.WriteHelper(buffer, offset, size, immediate, timeout, ref this.writeOverlapped.Holder[0]);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x001212FC File Offset: 0x0011F4FC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void WriteHelper(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, ref object holder)
		{
			try
			{
				this.FinishPendingWrite(timeout);
				ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
				int i = size;
				if (size > this.writeBufferSize)
				{
					size = this.writeBufferSize;
				}
				while (i > 0)
				{
					object obj = this.writeLock;
					lock (obj)
					{
						this.ValidateEnterWritingState(true);
						this.StartSyncWrite(buffer, offset, size, ref holder);
						this.EnterWritingState();
					}
					bool flag2 = false;
					try
					{
						this.WaitForSyncWrite(timeout, true, ref holder);
						flag2 = true;
					}
					finally
					{
						object obj2 = this.writeLock;
						lock (obj2)
						{
							try
							{
								if (flag2)
								{
									this.FinishSyncWrite(true);
								}
							}
							finally
							{
								this.ExitWritingState();
								if (!this.isWriteOutstanding)
								{
									this.WriteIOCompleted();
								}
							}
						}
					}
					i -= size;
					offset += size;
					if (size > i)
					{
						size = i;
					}
				}
			}
			catch (PipeException pipeException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Write), this.ExceptionEventType);
			}
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x00121434 File Offset: 0x0011F634
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			bool flag = true;
			try
			{
				if (size > this.writeBufferSize)
				{
					this.WriteHelper(buffer, offset, size, immediate, timeout, ref this.writeOverlapped.Holder[0]);
				}
				else
				{
					this.FinishPendingWrite(timeout);
					ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
					object obj = this.writeLock;
					lock (obj)
					{
						this.ValidateEnterWritingState(true);
						bool flag3 = false;
						try
						{
							flag = false;
							this.StartSyncWrite(buffer, offset, size);
							flag3 = true;
						}
						finally
						{
							if (!this.isWriteOutstanding)
							{
								flag = true;
							}
							else if (flag3)
							{
								this.EnterWritingState();
								this.pendingWriteBuffer = buffer;
								this.pendingWriteBufferManager = bufferManager;
							}
						}
					}
				}
			}
			catch (PipeException pipeException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertPipeException(pipeException, PipeConnection.TransferOperation.Write), this.ExceptionEventType);
			}
			finally
			{
				if (flag)
				{
					bufferManager.ReturnBuffer(buffer);
				}
			}
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x00121538 File Offset: 0x0011F738
		private void ValidateEnterReadingState(bool checkEOF)
		{
			if (checkEOF && this.closeState == PipeConnection.CloseState.Closing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeAlreadyClosing")), this.ExceptionEventType);
			}
			if (this.inReadingState)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeReadPending")), this.ExceptionEventType);
			}
			if (this.closeState == PipeConnection.CloseState.HandleClosed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeClosed")), this.ExceptionEventType);
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x001215C4 File Offset: 0x0011F7C4
		private void ValidateEnterWritingState(bool checkShutdown)
		{
			if (checkShutdown)
			{
				if (this.isShutdownWritten)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeAlreadyShuttingDown")), this.ExceptionEventType);
				}
				if (this.closeState == PipeConnection.CloseState.Closing)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeAlreadyClosing")), this.ExceptionEventType);
				}
			}
			if (this.inWritingState)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeWritePending")), this.ExceptionEventType);
			}
			if (this.closeState == PipeConnection.CloseState.HandleClosed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new PipeException(SR.GetString("PipeClosed")), this.ExceptionEventType);
			}
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x00121676 File Offset: 0x0011F876
		private void StartSyncRead(byte[] buffer, int offset, int size)
		{
			this.StartSyncRead(buffer, offset, size, ref this.readOverlapped.Holder[0]);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00121694 File Offset: 0x0011F894
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void StartSyncRead(byte[] buffer, int offset, int size, ref object holder)
		{
			if (this.isReadOutstanding)
			{
				throw Fx.AssertAndThrow("StartSyncRead called when read I/O was already pending.");
			}
			try
			{
				this.isReadOutstanding = true;
				this.readOverlapped.StartSyncOperation(buffer, ref holder);
				if (UnsafeNativeMethods.ReadFile(this.pipe.DangerousGetHandle(), this.readOverlapped.BufferPtr + offset, size, IntPtr.Zero, this.readOverlapped.NativeOverlapped) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 997)
					{
						this.isReadOutstanding = false;
						if (lastWin32Error != 234)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeConnection.Exceptions.CreateReadException(lastWin32Error));
						}
					}
				}
				else
				{
					this.isReadOutstanding = false;
				}
			}
			finally
			{
				if (!this.isReadOutstanding)
				{
					this.readOverlapped.CancelSyncOperation(ref holder);
				}
			}
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x00121758 File Offset: 0x0011F958
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void WaitForSyncRead(TimeSpan timeout, bool traceExceptionsAsErrors)
		{
			if (this.isReadOutstanding)
			{
				if (!this.readOverlapped.WaitForSyncOperation(timeout))
				{
					this.Abort(SR.GetString("PipeConnectionAbortedReadTimedOut", new object[]
					{
						this.readTimeout
					}), PipeConnection.TransferOperation.Read);
					Exception exception = new TimeoutException(SR.GetString("PipeReadTimedOut", new object[]
					{
						timeout
					}));
					TraceEventType eventType = TraceEventType.Information;
					if (traceExceptionsAsErrors)
					{
						eventType = TraceEventType.Error;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(exception, eventType);
				}
				this.isReadOutstanding = false;
			}
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x001217DC File Offset: 0x0011F9DC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private int FinishSyncRead(bool traceExceptionsAsErrors)
		{
			int result = -1;
			Exception ex;
			if (this.closeState == PipeConnection.CloseState.HandleClosed)
			{
				ex = this.CreatePipeClosedException(PipeConnection.TransferOperation.Read);
			}
			else
			{
				ex = PipeConnection.Exceptions.GetOverlappedReadException(this.pipe, this.readOverlapped.NativeOverlapped, out result);
			}
			if (ex != null)
			{
				TraceEventType eventType = TraceEventType.Information;
				if (traceExceptionsAsErrors)
				{
					eventType = TraceEventType.Error;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex, eventType);
			}
			return result;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0012182F File Offset: 0x0011FA2F
		private void StartSyncWrite(byte[] buffer, int offset, int size)
		{
			this.StartSyncWrite(buffer, offset, size, ref this.writeOverlapped.Holder[0]);
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x0012184C File Offset: 0x0011FA4C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void StartSyncWrite(byte[] buffer, int offset, int size, ref object holder)
		{
			if (this.isWriteOutstanding)
			{
				throw Fx.AssertAndThrow("StartSyncWrite called when write I/O was already pending.");
			}
			try
			{
				this.syncWriteSize = size;
				this.isWriteOutstanding = true;
				this.writeOverlapped.StartSyncOperation(buffer, ref holder);
				if (UnsafeNativeMethods.WriteFile(this.pipe.DangerousGetHandle(), this.writeOverlapped.BufferPtr + offset, size, IntPtr.Zero, this.writeOverlapped.NativeOverlapped) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 997)
					{
						this.isWriteOutstanding = false;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeConnection.Exceptions.CreateWriteException(lastWin32Error));
					}
				}
				else
				{
					this.isWriteOutstanding = false;
				}
			}
			finally
			{
				if (!this.isWriteOutstanding)
				{
					this.writeOverlapped.CancelSyncOperation(ref holder);
				}
			}
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x00121910 File Offset: 0x0011FB10
		private void WaitForSyncWrite(TimeSpan timeout, bool traceExceptionsAsErrors)
		{
			this.WaitForSyncWrite(timeout, traceExceptionsAsErrors, ref this.writeOverlapped.Holder[0]);
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x0012192C File Offset: 0x0011FB2C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void WaitForSyncWrite(TimeSpan timeout, bool traceExceptionsAsErrors, ref object holder)
		{
			if (this.isWriteOutstanding)
			{
				if (!this.writeOverlapped.WaitForSyncOperation(timeout, ref holder))
				{
					this.Abort(SR.GetString("PipeConnectionAbortedWriteTimedOut", new object[]
					{
						this.writeTimeout
					}), PipeConnection.TransferOperation.Write);
					Exception exception = new TimeoutException(SR.GetString("PipeWriteTimedOut", new object[]
					{
						timeout
					}));
					TraceEventType eventType = TraceEventType.Information;
					if (traceExceptionsAsErrors)
					{
						eventType = TraceEventType.Error;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(exception, eventType);
				}
				this.isWriteOutstanding = false;
			}
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x001219B0 File Offset: 0x0011FBB0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void FinishSyncWrite(bool traceExceptionsAsErrors)
		{
			Exception ex;
			if (this.closeState == PipeConnection.CloseState.HandleClosed)
			{
				ex = this.CreatePipeClosedException(PipeConnection.TransferOperation.Write);
			}
			else
			{
				int num;
				ex = PipeConnection.Exceptions.GetOverlappedWriteException(this.pipe, this.writeOverlapped.NativeOverlapped, out num);
				if (ex == null && num != this.syncWriteSize)
				{
					ex = new PipeException(SR.GetString("PipeWriteIncomplete"));
				}
			}
			if (ex != null)
			{
				TraceEventType eventType = TraceEventType.Information;
				if (traceExceptionsAsErrors)
				{
					eventType = TraceEventType.Error;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex, eventType);
			}
		}

		// Token: 0x04003115 RID: 12565
		private PipeHandle pipe;

		// Token: 0x04003116 RID: 12566
		private PipeConnection.CloseState closeState;

		// Token: 0x04003117 RID: 12567
		private bool aborted;

		// Token: 0x04003118 RID: 12568
		private bool isBoundToCompletionPort;

		// Token: 0x04003119 RID: 12569
		private bool autoBindToCompletionPort;

		// Token: 0x0400311A RID: 12570
		private TraceEventType exceptionEventType;

		// Token: 0x0400311B RID: 12571
		private static byte[] zeroBuffer;

		// Token: 0x0400311C RID: 12572
		private object readLock = new object();

		// Token: 0x0400311D RID: 12573
		private bool inReadingState;

		// Token: 0x0400311E RID: 12574
		private bool isReadOutstanding;

		// Token: 0x0400311F RID: 12575
		private OverlappedContext readOverlapped;

		// Token: 0x04003120 RID: 12576
		private byte[] asyncReadBuffer;

		// Token: 0x04003121 RID: 12577
		private int readBufferSize;

		// Token: 0x04003122 RID: 12578
		private ManualResetEvent atEOFEvent;

		// Token: 0x04003123 RID: 12579
		private bool isAtEOF;

		// Token: 0x04003124 RID: 12580
		private OverlappedIOCompleteCallback onAsyncReadComplete;

		// Token: 0x04003125 RID: 12581
		private Exception asyncReadException;

		// Token: 0x04003126 RID: 12582
		private WaitCallback asyncReadCallback;

		// Token: 0x04003127 RID: 12583
		private object asyncReadCallbackState;

		// Token: 0x04003128 RID: 12584
		private int asyncBytesRead;

		// Token: 0x04003129 RID: 12585
		private object writeLock = new object();

		// Token: 0x0400312A RID: 12586
		private bool inWritingState;

		// Token: 0x0400312B RID: 12587
		private bool isWriteOutstanding;

		// Token: 0x0400312C RID: 12588
		private OverlappedContext writeOverlapped;

		// Token: 0x0400312D RID: 12589
		private Exception asyncWriteException;

		// Token: 0x0400312E RID: 12590
		private WaitCallback asyncWriteCallback;

		// Token: 0x0400312F RID: 12591
		private object asyncWriteCallbackState;

		// Token: 0x04003130 RID: 12592
		private int asyncBytesToWrite;

		// Token: 0x04003131 RID: 12593
		private bool isShutdownWritten;

		// Token: 0x04003132 RID: 12594
		private int syncWriteSize;

		// Token: 0x04003133 RID: 12595
		private byte[] pendingWriteBuffer;

		// Token: 0x04003134 RID: 12596
		private BufferManager pendingWriteBufferManager;

		// Token: 0x04003135 RID: 12597
		private OverlappedIOCompleteCallback onAsyncWriteComplete;

		// Token: 0x04003136 RID: 12598
		private int writeBufferSize;

		// Token: 0x04003137 RID: 12599
		private TimeSpan readTimeout;

		// Token: 0x04003138 RID: 12600
		private IOThreadTimer readTimer;

		// Token: 0x04003139 RID: 12601
		private static Action<object> onReadTimeout;

		// Token: 0x0400313A RID: 12602
		private string timeoutErrorString;

		// Token: 0x0400313B RID: 12603
		private PipeConnection.TransferOperation timeoutErrorTransferOperation;

		// Token: 0x0400313C RID: 12604
		private TimeSpan writeTimeout;

		// Token: 0x0400313D RID: 12605
		private IOThreadTimer writeTimer;

		// Token: 0x0400313E RID: 12606
		private static Action<object> onWriteTimeout;

		// Token: 0x02000D33 RID: 3379
		private enum CloseState
		{
			// Token: 0x04004747 RID: 18247
			Open,
			// Token: 0x04004748 RID: 18248
			Closing,
			// Token: 0x04004749 RID: 18249
			HandleClosed
		}

		// Token: 0x02000D34 RID: 3380
		private enum TransferOperation
		{
			// Token: 0x0400474B RID: 18251
			Write,
			// Token: 0x0400474C RID: 18252
			Read,
			// Token: 0x0400474D RID: 18253
			Undefined
		}

		// Token: 0x02000D35 RID: 3381
		private static class Exceptions
		{
			// Token: 0x06007C28 RID: 31784 RVA: 0x001CFE81 File Offset: 0x001CE081
			private static PipeException CreateException(string resourceString, int error)
			{
				return new PipeException(SR.GetString(resourceString, new object[]
				{
					PipeError.GetErrorString(error)
				}), error);
			}

			// Token: 0x06007C29 RID: 31785 RVA: 0x001CFE9E File Offset: 0x001CE09E
			public static PipeException CreateReadException(int error)
			{
				return PipeConnection.Exceptions.CreateException("PipeReadError", error);
			}

			// Token: 0x06007C2A RID: 31786 RVA: 0x001CFEAB File Offset: 0x001CE0AB
			public static PipeException CreateWriteException(int error)
			{
				return PipeConnection.Exceptions.CreateException("PipeWriteError", error);
			}

			// Token: 0x06007C2B RID: 31787 RVA: 0x001CFEB8 File Offset: 0x001CE0B8
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			public unsafe static PipeException GetOverlappedWriteException(PipeHandle pipe, NativeOverlapped* nativeOverlapped, out int bytesWritten)
			{
				if (UnsafeNativeMethods.GetOverlappedResult(pipe.DangerousGetHandle(), nativeOverlapped, out bytesWritten, 0) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					return PipeConnection.Exceptions.CreateWriteException(lastWin32Error);
				}
				return null;
			}

			// Token: 0x06007C2C RID: 31788 RVA: 0x001CFEE4 File Offset: 0x001CE0E4
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			public unsafe static PipeException GetOverlappedReadException(PipeHandle pipe, NativeOverlapped* nativeOverlapped, out int bytesRead)
			{
				if (UnsafeNativeMethods.GetOverlappedResult(pipe.DangerousGetHandle(), nativeOverlapped, out bytesRead, 0) != 0)
				{
					return null;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 234)
				{
					return null;
				}
				return PipeConnection.Exceptions.CreateReadException(lastWin32Error);
			}
		}
	}
}
