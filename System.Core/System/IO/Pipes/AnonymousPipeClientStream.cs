using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000B0 RID: 176
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AnonymousPipeClientStream : PipeStream
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000E513 File Offset: 0x0000C713
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeClientStream(string pipeHandleAsString) : this(PipeDirection.In, pipeHandleAsString)
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000E520 File Offset: 0x0000C720
		[SecurityCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeClientStream(PipeDirection direction, string pipeHandleAsString) : base(direction, 0)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException(SR.GetString("NotSupported_AnonymousPipeUnidirectional"));
			}
			if (pipeHandleAsString == null)
			{
				throw new ArgumentNullException("pipeHandleAsString");
			}
			long value = 0L;
			if (!long.TryParse(pipeHandleAsString, out value))
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidHandle"), "pipeHandleAsString");
			}
			SafePipeHandle safePipeHandle = new SafePipeHandle((IntPtr)value, true);
			if (safePipeHandle.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidHandle"), "pipeHandleAsString");
			}
			this.Init(direction, safePipeHandle);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		[SecurityCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public AnonymousPipeClientStream(PipeDirection direction, SafePipeHandle safePipeHandle) : base(direction, 0)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException(SR.GetString("NotSupported_AnonymousPipeUnidirectional"));
			}
			if (safePipeHandle == null)
			{
				throw new ArgumentNullException("safePipeHandle");
			}
			if (safePipeHandle.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidHandle"), "safePipeHandle");
			}
			this.Init(direction, safePipeHandle);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000E60C File Offset: 0x0000C80C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void Init(PipeDirection direction, SafePipeHandle safePipeHandle)
		{
			if (UnsafeNativeMethods.GetFileType(safePipeHandle) != 3)
			{
				throw new IOException(SR.GetString("IO_IO_InvalidPipeHandle"));
			}
			base.InitializeHandle(safePipeHandle, true, false);
			base.State = PipeState.Connected;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000E638 File Offset: 0x0000C838
		~AnonymousPipeClientStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000E668 File Offset: 0x0000C868
		public override PipeTransmissionMode TransmissionMode
		{
			[SecurityCritical]
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		// Token: 0x17000108 RID: 264
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000E66B File Offset: 0x0000C86B
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
	}
}
