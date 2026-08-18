using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000682 RID: 1666
	internal sealed class SafeLsaLogonProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C47 RID: 15431 RVA: 0x000CE3CC File Offset: 0x000CD3CC
		private SafeLsaLogonProcessHandle() : base(true)
		{
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x000CE3D5 File Offset: 0x000CD3D5
		internal SafeLsaLogonProcessHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000CE3E5 File Offset: 0x000CD3E5
		internal static SafeLsaLogonProcessHandle InvalidHandle
		{
			get
			{
				return new SafeLsaLogonProcessHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x000CE3F1 File Offset: 0x000CD3F1
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaDeregisterLogonProcess(this.handle) >= 0;
		}
	}
}
