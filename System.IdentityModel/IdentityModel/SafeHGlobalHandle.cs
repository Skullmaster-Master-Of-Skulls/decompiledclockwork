using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.IdentityModel
{
	// Token: 0x0200009A RID: 154
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeHGlobalHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00006319 File Offset: 0x00004519
		private SafeHGlobalHandle() : base(true)
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00006322 File Offset: 0x00004522
		private SafeHGlobalHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001301D File Offset: 0x0001121D
		protected override bool ReleaseHandle()
		{
			Marshal.FreeHGlobal(this.handle);
			return true;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001302B File Offset: 0x0001122B
		public static SafeHGlobalHandle InvalidHandle
		{
			get
			{
				return new SafeHGlobalHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013038 File Offset: 0x00011238
		public static SafeHGlobalHandle AllocHGlobal(string s)
		{
			byte[] bytes = DiagnosticUtility.Utility.AllocateByteArray(checked((s.Length + 1) * 2));
			Encoding.Unicode.GetBytes(s, 0, s.Length, bytes, 0);
			return SafeHGlobalHandle.AllocHGlobal(bytes);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013078 File Offset: 0x00011278
		public static SafeHGlobalHandle AllocHGlobal(byte[] bytes)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(bytes.Length);
			Marshal.Copy(bytes, 0, safeHGlobalHandle.DangerousGetHandle(), bytes.Length);
			return safeHGlobalHandle;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001309F File Offset: 0x0001129F
		public static SafeHGlobalHandle AllocHGlobal(uint cb)
		{
			return SafeHGlobalHandle.AllocHGlobal((int)cb);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000130A8 File Offset: 0x000112A8
		public static SafeHGlobalHandle AllocHGlobal(int cb)
		{
			if (cb < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("cb", SR.GetString("ValueMustBeNonNegative")));
			}
			SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr handle = Marshal.AllocHGlobal(cb);
				safeHGlobalHandle.SetHandle(handle);
			}
			return safeHGlobalHandle;
		}
	}
}
