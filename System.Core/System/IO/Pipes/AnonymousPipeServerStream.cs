using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000AF RID: 175
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AnonymousPipeServerStream : PipeStream
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeServerStream() : this(PipeDirection.Out, HandleInheritability.None, 0, null)
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeServerStream(PipeDirection direction) : this(direction, HandleInheritability.None, 0)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000E1C7 File Offset: 0x0000C3C7
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability) : this(direction, inheritability, 0)
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize) : base(direction, bufferSize)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException(SR.GetString("NotSupported_AnonymousPipeUnidirectional"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", SR.GetString("ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable"));
			}
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(inheritability);
			this.Create(direction, secAttrs, bufferSize);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000E22C File Offset: 0x0000C42C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity) : base(direction, bufferSize)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException(SR.GetString("NotSupported_AnonymousPipeUnidirectional"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", SR.GetString("ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable"));
			}
			object obj;
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(inheritability, pipeSecurity, out obj);
			try
			{
				this.Create(direction, secAttrs, bufferSize);
			}
			finally
			{
				if (obj != null)
				{
					((GCHandle)obj).Free();
				}
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		~AnonymousPipeServerStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeServerStream(PipeDirection direction, SafePipeHandle serverSafePipeHandle, SafePipeHandle clientSafePipeHandle) : base(direction, 0)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException(SR.GetString("NotSupported_AnonymousPipeUnidirectional"));
			}
			if (serverSafePipeHandle == null)
			{
				throw new ArgumentNullException("serverSafePipeHandle");
			}
			if (clientSafePipeHandle == null)
			{
				throw new ArgumentNullException("clientSafePipeHandle");
			}
			if (serverSafePipeHandle.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidHandle"), "serverSafePipeHandle");
			}
			if (clientSafePipeHandle.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidHandle"), "clientSafePipeHandle");
			}
			if (UnsafeNativeMethods.GetFileType(serverSafePipeHandle) != 3)
			{
				throw new IOException(SR.GetString("IO_IO_InvalidPipeHandle"));
			}
			if (UnsafeNativeMethods.GetFileType(clientSafePipeHandle) != 3)
			{
				throw new IOException(SR.GetString("IO_IO_InvalidPipeHandle"));
			}
			base.InitializeHandle(serverSafePipeHandle, true, false);
			this.m_clientHandle = clientSafePipeHandle;
			this.m_clientHandleExposed = true;
			base.State = PipeState.Connected;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		[SecurityCritical]
		public string GetClientHandleAsString()
		{
			this.m_clientHandleExposed = true;
			return this.m_clientHandle.DangerousGetHandle().ToString();
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000E3D3 File Offset: 0x0000C5D3
		public SafePipeHandle ClientSafePipeHandle
		{
			[SecurityCritical]
			get
			{
				this.m_clientHandleExposed = true;
				return this.m_clientHandle;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		[SecurityCritical]
		public void DisposeLocalCopyOfClientHandle()
		{
			if (this.m_clientHandle != null && !this.m_clientHandle.IsClosed)
			{
				this.m_clientHandle.Dispose();
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000E404 File Offset: 0x0000C604
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.m_clientHandleExposed && this.m_clientHandle != null && !this.m_clientHandle.IsClosed)
				{
					this.m_clientHandle.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000E454 File Offset: 0x0000C654
		[SecurityCritical]
		private void Create(PipeDirection direction, UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs, int bufferSize)
		{
			SafePipeHandle safePipeHandle;
			bool flag;
			if (direction == PipeDirection.In)
			{
				flag = UnsafeNativeMethods.CreatePipe(out safePipeHandle, out this.m_clientHandle, secAttrs, bufferSize);
			}
			else
			{
				flag = UnsafeNativeMethods.CreatePipe(out this.m_clientHandle, out safePipeHandle, secAttrs, bufferSize);
			}
			if (!flag)
			{
				__Error.WinIOError(Marshal.GetLastWin32Error(), string.Empty);
			}
			SafePipeHandle handle;
			if (!UnsafeNativeMethods.DuplicateHandle(UnsafeNativeMethods.GetCurrentProcess(), safePipeHandle, UnsafeNativeMethods.GetCurrentProcess(), out handle, 0U, false, 2U))
			{
				__Error.WinIOError(Marshal.GetLastWin32Error(), string.Empty);
			}
			safePipeHandle.Dispose();
			base.InitializeHandle(handle, false, false);
			base.State = PipeState.Connected;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000E4D7 File Offset: 0x0000C6D7
		public override PipeTransmissionMode TransmissionMode
		{
			[SecurityCritical]
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		// Token: 0x17000106 RID: 262
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000E4DA File Offset: 0x0000C6DA
		public override PipeTransmissionMode ReadMode
		{
			[SecurityCritical]
			set
			{
				this.CheckPipePropertyOperations();
				if (value < PipeTransmissionMode.Byte || value > PipeTransmissionMode.Message)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("ArgumentOutOfRange_TransmissionModeByteOrMsg"));
				}
				if (value == PipeTransmissionMode.Message)
				{
					throw new NotSupportedException(SR.GetString("NotSupported_AnonymousPipeMessagesNotSupported"));
				}
			}
		}

		// Token: 0x0400054E RID: 1358
		private SafePipeHandle m_clientHandle;

		// Token: 0x0400054F RID: 1359
		private bool m_clientHandleExposed;
	}
}
