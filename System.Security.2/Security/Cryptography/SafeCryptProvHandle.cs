using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000011 RID: 17
	[SecurityCritical]
	internal sealed class SafeCryptProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000042C2 File Offset: 0x000024C2
		private SafeCryptProvHandle() : base(true)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000042D8 File Offset: 0x000024D8
		internal SafeCryptProvHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000431E File Offset: 0x0000251E
		internal SafeCryptProvHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004330 File Offset: 0x00002530
		internal SafeCryptProvHandle(IntPtr handle, SafeHandle parentHandle) : base(true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = false;
				parentHandle.DangerousAddRef(ref flag);
				if (flag)
				{
					this._parentHandle = parentHandle;
					base.SetHandle(handle);
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004378 File Offset: 0x00002578
		internal static SafeCryptProvHandle InvalidHandle
		{
			get
			{
				SafeCryptProvHandle safeCryptProvHandle = new SafeCryptProvHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCryptProvHandle);
				return safeCryptProvHandle;
			}
		}

		// Token: 0x06000083 RID: 131
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ncrypt.dll", SetLastError = true)]
		private static extern bool NCryptIsKeyHandle(IntPtr hCryptProv);

		// Token: 0x06000084 RID: 132
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool CryptReleaseContext(IntPtr hCryptProv, uint dwFlags);

		// Token: 0x06000085 RID: 133
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ncrypt.dll", SetLastError = true)]
		private static extern int NCryptFreeObject(IntPtr hObject);

		// Token: 0x06000086 RID: 134 RVA: 0x00004398 File Offset: 0x00002598
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			if (this._parentHandle != null)
			{
				this._parentHandle.DangerousRelease();
				this._parentHandle = null;
				base.SetHandle(IntPtr.Zero);
				return true;
			}
			if (SafeCryptProvHandle.NCryptIsKeyHandle(handle))
			{
				int num = SafeCryptProvHandle.NCryptFreeObject(handle);
				return num == 0;
			}
			return SafeCryptProvHandle.CryptReleaseContext(handle, 0U);
		}

		// Token: 0x0400037C RID: 892
		private SafeHandle _parentHandle;
	}
}
