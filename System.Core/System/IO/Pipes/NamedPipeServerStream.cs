using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000B2 RID: 178
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class NamedPipeServerStream : PipeStream
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName) : this(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, null, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction) : this(pipeName, direction, 1, PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, null, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000E71C File Offset: 0x0000C91C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances) : this(pipeName, direction, maxNumberOfServerInstances, PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, null, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000E73C File Offset: 0x0000C93C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, PipeOptions.None, 0, 0, null, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000E75C File Offset: 0x0000C95C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, 0, 0, null, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000E77C File Offset: 0x0000C97C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, null, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, pipeSecurity, HandleInheritability.None, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, pipeSecurity, inheritability, (PipeAccessRights)0)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability, PipeAccessRights additionalAccessRights) : base(direction, transmissionMode, outBufferSize)
		{
			if (pipeName == null)
			{
				throw new ArgumentNullException("pipeName");
			}
			if (pipeName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_NeedNonemptyPipeName"));
			}
			if ((options & (PipeOptions)1073741823) != PipeOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", SR.GetString("ArgumentOutOfRange_OptionsInvalid"));
			}
			if (inBufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("inBufferSize", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if ((maxNumberOfServerInstances < 1 || maxNumberOfServerInstances > 254) && maxNumberOfServerInstances != -1)
			{
				throw new ArgumentOutOfRangeException("maxNumberOfServerInstances", SR.GetString("ArgumentOutOfRange_MaxNumServerInstances"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", SR.GetString("ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable"));
			}
			if ((additionalAccessRights & ~(PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.AccessSystemSecurity)) != (PipeAccessRights)0)
			{
				throw new ArgumentOutOfRangeException("additionalAccessRights", SR.GetString("ArgumentOutOfRange_AdditionalAccessLimited"));
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				throw new PlatformNotSupportedException(SR.GetString("PlatformNotSupported_NamedPipeServers"));
			}
			string fullPath = Path.GetFullPath("\\\\.\\pipe\\" + pipeName);
			if (string.Compare(fullPath, "\\\\.\\pipe\\anonymous", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentOutOfRangeException("pipeName", SR.GetString("ArgumentOutOfRange_AnonymousReserved"));
			}
			object obj = null;
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(inheritability, pipeSecurity, out obj);
			try
			{
				this.Create(fullPath, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, additionalAccessRights, secAttrs);
			}
			finally
			{
				if (obj != null)
				{
					((GCHandle)obj).Free();
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000E95C File Offset: 0x0000CB5C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeServerStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle) : base(direction, PipeTransmissionMode.Byte, 0)
		{
			if (safePipeHandle == null)
			{
				throw new ArgumentNullException("safePipeHandle");
			}
			if (safePipeHandle.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidHandle"), "safePipeHandle");
			}
			if (UnsafeNativeMethods.GetFileType(safePipeHandle) != 3)
			{
				throw new IOException(SR.GetString("IO_IO_InvalidPipeHandle"));
			}
			base.InitializeHandle(safePipeHandle, true, isAsync);
			if (isConnected)
			{
				base.State = PipeState.Connected;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		~NamedPipeServerStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000EA00 File Offset: 0x0000CC00
		[SecurityCritical]
		private void Create(string fullPipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeAccessRights rights, UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs)
		{
			int openMode = (int)(direction | ((maxNumberOfServerInstances == 1) ? ((PipeDirection)524288) : ((PipeDirection)0)) | (PipeDirection)options | (PipeDirection)rights);
			int pipeMode = (int)((int)transmissionMode << 2 | (int)transmissionMode << 1);
			if (maxNumberOfServerInstances == -1)
			{
				maxNumberOfServerInstances = 255;
			}
			SafePipeHandle safePipeHandle = UnsafeNativeMethods.CreateNamedPipe(fullPipeName, openMode, pipeMode, maxNumberOfServerInstances, outBufferSize, inBufferSize, 0, secAttrs);
			if (safePipeHandle.IsInvalid)
			{
				__Error.WinIOError(Marshal.GetLastWin32Error(), string.Empty);
			}
			base.InitializeHandle(safePipeHandle, false, (options & PipeOptions.Asynchronous) > PipeOptions.None);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000EA74 File Offset: 0x0000CC74
		[SecurityCritical]
		public void WaitForConnection()
		{
			this.CheckConnectOperationsServer();
			if (base.IsAsync)
			{
				IAsyncResult asyncResult = this.BeginWaitForConnection(null, null);
				this.EndWaitForConnection(asyncResult);
				return;
			}
			if (!UnsafeNativeMethods.ConnectNamedPipe(base.InternalHandle, UnsafeNativeMethods.NULL))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 535)
				{
					__Error.WinIOError(lastWin32Error, string.Empty);
				}
				if (lastWin32Error == 535 && base.State == PipeState.Connected)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeAlreadyConnected"));
				}
			}
			base.State = PipeState.Connected;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public Task WaitForConnectionAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			if (!base.IsAsync)
			{
				return Task.Factory.StartNew(new Action(this.WaitForConnection), cancellationToken);
			}
			IOCancellationHelper state = cancellationToken.CanBeCanceled ? new IOCancellationHelper(cancellationToken) : null;
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginWaitForConnection), new Action<IAsyncResult>(this.EndWaitForConnection), state);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000EB67 File Offset: 0x0000CD67
		public Task WaitForConnectionAsync()
		{
			return this.WaitForConnectionAsync(CancellationToken.None);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000EB74 File Offset: 0x0000CD74
		[SecurityCritical]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public unsafe IAsyncResult BeginWaitForConnection(AsyncCallback callback, object state)
		{
			this.CheckConnectOperationsServer();
			if (!base.IsAsync)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotAsync"));
			}
			PipeAsyncResult pipeAsyncResult = new PipeAsyncResult();
			pipeAsyncResult._handle = base.InternalHandle;
			pipeAsyncResult._userCallback = callback;
			pipeAsyncResult._userStateObject = state;
			IOCancellationHelper iocancellationHelper = state as IOCancellationHelper;
			ManualResetEvent waitHandle = new ManualResetEvent(false);
			pipeAsyncResult._waitHandle = waitHandle;
			Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, pipeAsyncResult);
			NativeOverlapped* ptr = overlapped.Pack(NamedPipeServerStream.WaitForConnectionCallback, null);
			pipeAsyncResult._overlapped = ptr;
			if (!UnsafeNativeMethods.ConnectNamedPipe(base.InternalHandle, ptr))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 997)
				{
					if (iocancellationHelper != null)
					{
						iocancellationHelper.AllowCancellation(base.InternalHandle, ptr);
					}
					return pipeAsyncResult;
				}
				Overlapped.Free(ptr);
				pipeAsyncResult._overlapped = null;
				if (lastWin32Error == 535)
				{
					if (base.State == PipeState.Connected)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeAlreadyConnected"));
					}
					pipeAsyncResult.CallUserCallback();
					return pipeAsyncResult;
				}
				else
				{
					__Error.WinIOError(lastWin32Error, string.Empty);
				}
			}
			if (iocancellationHelper != null)
			{
				iocancellationHelper.AllowCancellation(base.InternalHandle, ptr);
			}
			return pipeAsyncResult;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000EC84 File Offset: 0x0000CE84
		[SecurityCritical]
		public void EndWaitForConnection(IAsyncResult asyncResult)
		{
			this.CheckConnectOperationsServer();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!base.IsAsync)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotAsync"));
			}
			PipeAsyncResult pipeAsyncResult = asyncResult as PipeAsyncResult;
			if (pipeAsyncResult == null)
			{
				__Error.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref pipeAsyncResult._EndXxxCalled, 1, 0))
			{
				__Error.EndWaitForConnectionCalledTwice();
			}
			IOCancellationHelper iocancellationHelper = pipeAsyncResult.AsyncState as IOCancellationHelper;
			if (iocancellationHelper != null)
			{
				iocancellationHelper.SetOperationCompleted();
			}
			WaitHandle waitHandle = pipeAsyncResult._waitHandle;
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
			if (pipeAsyncResult._errorCode != 0)
			{
				if (pipeAsyncResult._errorCode == 995 && iocancellationHelper != null)
				{
					iocancellationHelper.ThrowIOOperationAborted();
				}
				__Error.WinIOError(pipeAsyncResult._errorCode, string.Empty);
			}
			base.State = PipeState.Connected;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000ED58 File Offset: 0x0000CF58
		[SecurityCritical]
		public void Disconnect()
		{
			this.CheckDisconnectOperations();
			if (!UnsafeNativeMethods.DisconnectNamedPipe(base.InternalHandle))
			{
				__Error.WinIOError(Marshal.GetLastWin32Error(), string.Empty);
			}
			base.State = PipeState.Disconnected;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000ED84 File Offset: 0x0000CF84
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public void RunAsClient(PipeStreamImpersonationWorker impersonationWorker)
		{
			base.CheckWriteOperations();
			NamedPipeServerStream.ExecuteHelper executeHelper = new NamedPipeServerStream.ExecuteHelper(impersonationWorker, base.InternalHandle);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(NamedPipeServerStream.tryCode, NamedPipeServerStream.cleanupCode, executeHelper);
			if (executeHelper.m_impersonateErrorCode != 0)
			{
				base.WinIOError(executeHelper.m_impersonateErrorCode);
				return;
			}
			if (executeHelper.m_revertImpersonateErrorCode != 0)
			{
				base.WinIOError(executeHelper.m_revertImpersonateErrorCode);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		[SecurityCritical]
		private static void ImpersonateAndTryCode(object helper)
		{
			NamedPipeServerStream.ExecuteHelper executeHelper = (NamedPipeServerStream.ExecuteHelper)helper;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (UnsafeNativeMethods.ImpersonateNamedPipeClient(executeHelper.m_handle))
				{
					executeHelper.m_mustRevert = true;
				}
				else
				{
					executeHelper.m_impersonateErrorCode = Marshal.GetLastWin32Error();
				}
			}
			if (executeHelper.m_mustRevert)
			{
				executeHelper.m_userCode();
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000EE40 File Offset: 0x0000D040
		[SecurityCritical]
		[PrePrepareMethod]
		private static void RevertImpersonationOnBackout(object helper, bool exceptionThrown)
		{
			NamedPipeServerStream.ExecuteHelper executeHelper = (NamedPipeServerStream.ExecuteHelper)helper;
			if (executeHelper.m_mustRevert && !UnsafeNativeMethods.RevertToSelf())
			{
				executeHelper.m_revertImpersonateErrorCode = Marshal.GetLastWin32Error();
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000EE70 File Offset: 0x0000D070
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public string GetImpersonationUserName()
		{
			base.CheckWriteOperations();
			StringBuilder stringBuilder = new StringBuilder(514);
			if (!UnsafeNativeMethods.GetNamedPipeHandleState(base.InternalHandle, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, stringBuilder, stringBuilder.Capacity))
			{
				base.WinIOError(Marshal.GetLastWin32Error());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		[SecurityCritical]
		private unsafe static void AsyncWaitForConnectionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(pOverlapped);
			PipeAsyncResult pipeAsyncResult = (PipeAsyncResult)overlapped.AsyncResult;
			Overlapped.Free(pOverlapped);
			pipeAsyncResult._overlapped = null;
			if (errorCode == 535U)
			{
				errorCode = 0U;
			}
			pipeAsyncResult._errorCode = (int)errorCode;
			pipeAsyncResult._completedSynchronously = false;
			pipeAsyncResult._isComplete = true;
			ManualResetEvent waitHandle = pipeAsyncResult._waitHandle;
			if (waitHandle != null && !waitHandle.Set())
			{
				__Error.WinIOError();
			}
			AsyncCallback userCallback = pipeAsyncResult._userCallback;
			if (userCallback != null)
			{
				userCallback(pipeAsyncResult);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000EF44 File Offset: 0x0000D144
		[SecurityCritical]
		private void CheckConnectOperationsServer()
		{
			if (base.InternalHandle == null)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
			}
			if (base.State == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
			if (base.InternalHandle.IsClosed)
			{
				__Error.PipeNotOpen();
			}
			if (base.State == PipeState.Broken)
			{
				throw new IOException(SR.GetString("IO_IO_PipeBroken"));
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		[SecurityCritical]
		private void CheckDisconnectOperations()
		{
			if (base.State == PipeState.WaitingToConnect)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotYetConnected"));
			}
			if (base.State == PipeState.Disconnected)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeAlreadyDisconnected"));
			}
			if (base.InternalHandle == null)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeHandleNotSet"));
			}
			if (base.State == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
			if (base.InternalHandle.IsClosed)
			{
				__Error.PipeNotOpen();
			}
		}

		// Token: 0x04000550 RID: 1360
		public const int MaxAllowedServerInstances = -1;

		// Token: 0x04000551 RID: 1361
		[SecurityCritical]
		private static readonly IOCompletionCallback WaitForConnectionCallback = new IOCompletionCallback(NamedPipeServerStream.AsyncWaitForConnectionCallback);

		// Token: 0x04000552 RID: 1362
		private static RuntimeHelpers.TryCode tryCode = new RuntimeHelpers.TryCode(NamedPipeServerStream.ImpersonateAndTryCode);

		// Token: 0x04000553 RID: 1363
		private static RuntimeHelpers.CleanupCode cleanupCode = new RuntimeHelpers.CleanupCode(NamedPipeServerStream.RevertImpersonationOnBackout);

		// Token: 0x0200030B RID: 779
		internal class ExecuteHelper
		{
			// Token: 0x06001A81 RID: 6785 RVA: 0x00061152 File Offset: 0x0005F352
			[SecurityCritical]
			internal ExecuteHelper(PipeStreamImpersonationWorker userCode, SafePipeHandle handle)
			{
				this.m_userCode = userCode;
				this.m_handle = handle;
			}

			// Token: 0x04000E28 RID: 3624
			internal PipeStreamImpersonationWorker m_userCode;

			// Token: 0x04000E29 RID: 3625
			internal SafePipeHandle m_handle;

			// Token: 0x04000E2A RID: 3626
			internal bool m_mustRevert;

			// Token: 0x04000E2B RID: 3627
			internal int m_impersonateErrorCode;

			// Token: 0x04000E2C RID: 3628
			internal int m_revertImpersonateErrorCode;
		}
	}
}
