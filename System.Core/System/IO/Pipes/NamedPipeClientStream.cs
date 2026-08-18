using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000B3 RID: 179
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class NamedPipeClientStream : PipeStream
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000F01A File Offset: 0x0000D21A
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string pipeName) : this(".", pipeName, PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000F02C File Offset: 0x0000D22C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string serverName, string pipeName) : this(serverName, pipeName, PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000F03A File Offset: 0x0000D23A
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction) : this(serverName, pipeName, direction, PipeOptions.None, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F048 File Offset: 0x0000D248
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options) : this(serverName, pipeName, direction, options, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000F057 File Offset: 0x0000D257
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel) : this(serverName, pipeName, direction, options, impersonationLevel, HandleInheritability.None)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F068 File Offset: 0x0000D268
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability) : base(direction, 0)
		{
			if (pipeName == null)
			{
				throw new ArgumentNullException("pipeName");
			}
			if (serverName == null)
			{
				throw new ArgumentNullException("serverName", SR.GetString("ArgumentNull_ServerName"));
			}
			if (pipeName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_NeedNonemptyPipeName"));
			}
			if (serverName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_EmptyServerName"));
			}
			if ((options & (PipeOptions)1073741823) != PipeOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", SR.GetString("ArgumentOutOfRange_OptionsInvalid"));
			}
			if (impersonationLevel < TokenImpersonationLevel.None || impersonationLevel > TokenImpersonationLevel.Delegation)
			{
				throw new ArgumentOutOfRangeException("impersonationLevel", SR.GetString("ArgumentOutOfRange_ImpersonationInvalid"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", SR.GetString("ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable"));
			}
			this.m_normalizedPipePath = Path.GetFullPath("\\\\" + serverName + "\\pipe\\" + pipeName);
			if (string.Compare(this.m_normalizedPipePath, "\\\\.\\pipe\\anonymous", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentOutOfRangeException("pipeName", SR.GetString("ArgumentOutOfRange_AnonymousReserved"));
			}
			this.m_inheritability = inheritability;
			this.m_impersonationLevel = impersonationLevel;
			this.m_pipeOptions = options;
			if ((PipeDirection.In & direction) != (PipeDirection)0)
			{
				this.m_access |= int.MinValue;
			}
			if ((PipeDirection.Out & direction) != (PipeDirection)0)
			{
				this.m_access |= 1073741824;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability) : base(NamedPipeClientStream.DirectionFromRights(desiredAccessRights), 0)
		{
			if (pipeName == null)
			{
				throw new ArgumentNullException("pipeName");
			}
			if (serverName == null)
			{
				throw new ArgumentNullException("serverName", SR.GetString("ArgumentNull_ServerName"));
			}
			if (pipeName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_NeedNonemptyPipeName"));
			}
			if (serverName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_EmptyServerName"));
			}
			if ((options & (PipeOptions)1073741823) != PipeOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", SR.GetString("ArgumentOutOfRange_OptionsInvalid"));
			}
			if (impersonationLevel < TokenImpersonationLevel.None || impersonationLevel > TokenImpersonationLevel.Delegation)
			{
				throw new ArgumentOutOfRangeException("impersonationLevel", SR.GetString("ArgumentOutOfRange_ImpersonationInvalid"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", SR.GetString("ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable"));
			}
			if ((desiredAccessRights & ~(PipeAccessRights.ReadData | PipeAccessRights.WriteData | PipeAccessRights.ReadAttributes | PipeAccessRights.WriteAttributes | PipeAccessRights.ReadExtendedAttributes | PipeAccessRights.WriteExtendedAttributes | PipeAccessRights.CreateNewInstance | PipeAccessRights.Delete | PipeAccessRights.ReadPermissions | PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.Synchronize | PipeAccessRights.AccessSystemSecurity)) != (PipeAccessRights)0)
			{
				throw new ArgumentOutOfRangeException("desiredAccessRights", SR.GetString("ArgumentOutOfRange_InvalidPipeAccessRights"));
			}
			this.m_normalizedPipePath = Path.GetFullPath("\\\\" + serverName + "\\pipe\\" + pipeName);
			if (string.Compare(this.m_normalizedPipePath, "\\\\.\\pipe\\anonymous", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentOutOfRangeException("pipeName", SR.GetString("ArgumentOutOfRange_AnonymousReserved"));
			}
			this.m_inheritability = inheritability;
			this.m_impersonationLevel = impersonationLevel;
			this.m_pipeOptions = options;
			this.m_access = (int)desiredAccessRights;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000F30C File Offset: 0x0000D50C
		private static PipeDirection DirectionFromRights(PipeAccessRights rights)
		{
			PipeDirection pipeDirection = (PipeDirection)0;
			if ((rights & PipeAccessRights.ReadData) != (PipeAccessRights)0)
			{
				pipeDirection |= PipeDirection.In;
			}
			if ((rights & PipeAccessRights.WriteData) != (PipeAccessRights)0)
			{
				pipeDirection |= PipeDirection.Out;
			}
			return pipeDirection;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000F330 File Offset: 0x0000D530
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public NamedPipeClientStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle) : base(direction, 0)
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

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		~NamedPipeClientStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public void Connect()
		{
			this.Connect(-1);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		[SecurityCritical]
		public void Connect(int timeout)
		{
			this.CheckConnectOperationsClient();
			if (timeout < 0 && timeout != -1)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.GetString("ArgumentOutOfRange_InvalidTimeout"));
			}
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(this.m_inheritability);
			int num = (int)this.m_pipeOptions;
			if (this.m_impersonationLevel != TokenImpersonationLevel.None)
			{
				num |= 1048576;
				num |= this.m_impersonationLevel - TokenImpersonationLevel.Anonymous << 16;
			}
			int tickCount = Environment.TickCount;
			int num2 = 0;
			SpinWait spinWait = default(SpinWait);
			SafePipeHandle safePipeHandle;
			int lastWin32Error2;
			for (;;)
			{
				if (UnsafeNativeMethods.WaitNamedPipe(this.m_normalizedPipePath, timeout - num2))
				{
					goto IL_9C;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					spinWait.SpinOnce();
				}
				else
				{
					if (lastWin32Error != 0)
					{
						__Error.WinIOError(lastWin32Error, string.Empty);
						goto IL_9C;
					}
					goto IL_11C;
				}
				IL_106:
				if (timeout != -1 && (num2 = Environment.TickCount - tickCount) >= timeout)
				{
					goto IL_11C;
				}
				continue;
				IL_9C:
				safePipeHandle = UnsafeNativeMethods.CreateNamedPipeClient(this.m_normalizedPipePath, this.m_access, FileShare.None, secAttrs, FileMode.Open, num, UnsafeNativeMethods.NULL);
				if (!safePipeHandle.IsInvalid)
				{
					goto IL_E6;
				}
				lastWin32Error2 = Marshal.GetLastWin32Error();
				if (lastWin32Error2 == 231)
				{
					spinWait.SpinOnce();
					goto IL_106;
				}
				break;
			}
			__Error.WinIOError(lastWin32Error2, string.Empty);
			IL_E6:
			base.InitializeHandle(safePipeHandle, false, (this.m_pipeOptions & PipeOptions.Asynchronous) > PipeOptions.None);
			base.State = PipeState.Connected;
			return;
			IL_11C:
			throw new TimeoutException();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000F50A File Offset: 0x0000D70A
		public Task ConnectAsync()
		{
			return this.ConnectAsync(-1, CancellationToken.None);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000F518 File Offset: 0x0000D718
		public Task ConnectAsync(int timeout)
		{
			return this.ConnectAsync(timeout, CancellationToken.None);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000F526 File Offset: 0x0000D726
		public Task ConnectAsync(CancellationToken cancellationToken)
		{
			return this.ConnectAsync(-1, cancellationToken);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000F530 File Offset: 0x0000D730
		public Task ConnectAsync(int timeout, CancellationToken cancellationToken)
		{
			this.CheckConnectOperationsClient();
			if (timeout < 0 && timeout != -1)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.GetString("ArgumentOutOfRange_InvalidTimeout"));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			int startTime = Environment.TickCount;
			return Task.Factory.StartNew(delegate()
			{
				this.ConnectInternal(timeout, cancellationToken, startTime);
			}, cancellationToken);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		[SecuritySafeCritical]
		private void ConnectInternal(int timeout, CancellationToken cancellationToken, int startTime)
		{
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(this.m_inheritability);
			int num = (int)this.m_pipeOptions;
			if (this.m_impersonationLevel != TokenImpersonationLevel.None)
			{
				num |= 1048576;
				num |= this.m_impersonationLevel - TokenImpersonationLevel.Anonymous << 16;
			}
			int num2 = 0;
			SpinWait spinWait = default(SpinWait);
			SafePipeHandle safePipeHandle;
			int lastWin32Error2;
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int num3 = timeout - num2;
				int timeout2;
				if (cancellationToken.CanBeCanceled)
				{
					timeout2 = Math.Min(50, num3);
				}
				else
				{
					timeout2 = num3;
				}
				if (UnsafeNativeMethods.WaitNamedPipe(this.m_normalizedPipePath, timeout2))
				{
					goto IL_AD;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					spinWait.SpinOnce();
				}
				else
				{
					if (lastWin32Error != 0)
					{
						__Error.WinIOError(lastWin32Error, string.Empty);
						goto IL_AD;
					}
					if (!cancellationToken.CanBeCanceled)
					{
						goto IL_12D;
					}
					spinWait.SpinOnce();
				}
				IL_117:
				if (timeout != -1 && (num2 = Environment.TickCount - startTime) >= timeout)
				{
					goto IL_12D;
				}
				continue;
				IL_AD:
				safePipeHandle = UnsafeNativeMethods.CreateNamedPipeClient(this.m_normalizedPipePath, this.m_access, FileShare.None, secAttrs, FileMode.Open, num, UnsafeNativeMethods.NULL);
				if (!safePipeHandle.IsInvalid)
				{
					goto IL_F7;
				}
				lastWin32Error2 = Marshal.GetLastWin32Error();
				if (lastWin32Error2 == 231)
				{
					spinWait.SpinOnce();
					goto IL_117;
				}
				break;
			}
			__Error.WinIOError(lastWin32Error2, string.Empty);
			IL_F7:
			base.InitializeHandle(safePipeHandle, false, (this.m_pipeOptions & PipeOptions.Asynchronous) > PipeOptions.None);
			base.State = PipeState.Connected;
			return;
			IL_12D:
			throw new TimeoutException();
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000F708 File Offset: 0x0000D908
		public int NumberOfServerInstances
		{
			[SecurityCritical]
			get
			{
				this.CheckPipePropertyOperations();
				int result;
				if (!UnsafeNativeMethods.GetNamedPipeHandleState(base.InternalHandle, UnsafeNativeMethods.NULL, out result, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, UnsafeNativeMethods.NULL, 0))
				{
					base.WinIOError(Marshal.GetLastWin32Error());
				}
				return result;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F74B File Offset: 0x0000D94B
		[SecurityCritical]
		protected internal override void CheckPipePropertyOperations()
		{
			base.CheckPipePropertyOperations();
			if (base.State == PipeState.WaitingToConnect)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeNotYetConnected"));
			}
			if (base.State == PipeState.Broken)
			{
				throw new IOException(SR.GetString("IO_IO_PipeBroken"));
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F784 File Offset: 0x0000D984
		private void CheckConnectOperationsClient()
		{
			if (base.State == PipeState.Connected)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_PipeAlreadyConnected"));
			}
			if (base.State == PipeState.Closed)
			{
				__Error.PipeNotOpen();
			}
		}

		// Token: 0x04000554 RID: 1364
		private const int CancellationCheckIntervalInMilliseconds = 50;

		// Token: 0x04000555 RID: 1365
		private string m_normalizedPipePath;

		// Token: 0x04000556 RID: 1366
		private TokenImpersonationLevel m_impersonationLevel;

		// Token: 0x04000557 RID: 1367
		private PipeOptions m_pipeOptions;

		// Token: 0x04000558 RID: 1368
		private HandleInheritability m_inheritability;

		// Token: 0x04000559 RID: 1369
		private int m_access;
	}
}
