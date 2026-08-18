using System;
using Microsoft.Win32.SafeHandles;

namespace System.Web.Util
{
	// Token: 0x020001C7 RID: 455
	internal class HeapAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600174C RID: 5964 RVA: 0x0004923C File Offset: 0x0004743C
		protected HeapAllocHandle() : base(true)
		{
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00049245 File Offset: 0x00047445
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.HeapFree(HeapAllocHandle.ProcessHeap, 0U, this.handle);
		}

		// Token: 0x04001700 RID: 5888
		private static readonly IntPtr ProcessHeap = UnsafeNativeMethods.GetProcessHeap();
	}
}
