using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Web.Management.Utility
{
	// Token: 0x0200008A RID: 138
	internal sealed class SafeGlobalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000A02B File Offset: 0x0000902B
		public static SafeGlobalAllocHandle Empty
		{
			get
			{
				return new SafeGlobalAllocHandle();
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A032 File Offset: 0x00009032
		private SafeGlobalAllocHandle() : base(false)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000A046 File Offset: 0x00009046
		public SafeGlobalAllocHandle(string str) : base(true)
		{
			base.SetHandle(Marshal.StringToHGlobalUni(str));
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000A05B File Offset: 0x0000905B
		public SafeGlobalAllocHandle(int cb) : base(true)
		{
			base.SetHandle(Marshal.AllocHGlobal(cb));
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A070 File Offset: 0x00009070
		internal void Copy(byte[] permissionBytes, int startIndex, int cbBytes)
		{
			Marshal.Copy(this.handle, permissionBytes, startIndex, cbBytes);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A080 File Offset: 0x00009080
		internal void MarshalStructure(object structure, bool deleteOld)
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException();
			}
			Marshal.StructureToPtr(structure, this.handle, deleteOld);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000A09D File Offset: 0x0000909D
		internal T MarshalToStructure<T>()
		{
			return (T)((object)Marshal.PtrToStructure(this.handle, typeof(T)));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000A0B9 File Offset: 0x000090B9
		protected override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				Marshal.FreeHGlobal(this.handle);
				base.SetHandleAsInvalid();
			}
			return true;
		}
	}
}
