using System;
using System.Runtime.InteropServices;
using Telerik.Web.UI.ExcelBiff;

namespace Telerik.Web.UI.Common.Export.ExcelBiff
{
	// Token: 0x02000A1C RID: 2588
	internal sealed class SafeNativeMethods
	{
		// Token: 0x060061F3 RID: 25075
		[DllImport("ole32.dll")]
		internal static extern int CreateILockBytesOnHGlobal(IntPtr hGlobal, [MarshalAs(UnmanagedType.Bool)] bool fDeleteOnRelease, out OLEStructuredStorage.UCOMILockBytes lockBytes);

		// Token: 0x060061F4 RID: 25076
		[DllImport("Ole32.dll")]
		internal static extern int StgCreateDocfile([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved, out OLEStructuredStorage.UCOMIStorage storage);

		// Token: 0x060061F5 RID: 25077
		[DllImport("ole32.dll")]
		internal static extern int StgCreateDocfileOnILockBytes(OLEStructuredStorage.UCOMILockBytes plkbyt, int grfMode, int reserved, out OLEStructuredStorage.UCOMIStorage storage);

		// Token: 0x060061F6 RID: 25078
		[DllImport("ole32.dll")]
		internal static extern int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int stgfmt, int grfAttr, IntPtr StgOptions, IntPtr reserved2, ref Guid refiid, out OLEStructuredStorage.UCOMIStorage storage);

		// Token: 0x060061F7 RID: 25079
		[DllImport("ole32.dll")]
		internal static extern int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, IntPtr stgPriority, int grfMode, IntPtr snbExclude, int reserved, out OLEStructuredStorage.UCOMIStorage storage);
	}
}
