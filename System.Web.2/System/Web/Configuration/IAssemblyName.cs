using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x02000703 RID: 1795
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CD193BC0-B4BC-11d2-9833-00C04FC31D2E")]
	[ComImport]
	internal interface IAssemblyName
	{
		// Token: 0x060056C6 RID: 22214
		[PreserveSig]
		int SetProperty(uint PropertyId, IntPtr pvProperty, uint cbProperty);

		// Token: 0x060056C7 RID: 22215
		[PreserveSig]
		int GetProperty(uint PropertyId, IntPtr pvProperty, ref uint pcbProperty);

		// Token: 0x060056C8 RID: 22216
		[PreserveSig]
		int Finalize();

		// Token: 0x060056C9 RID: 22217
		[PreserveSig]
		int GetDisplayName(IntPtr szDisplayName, ref uint pccDisplayName, uint dwDisplayFlags);

		// Token: 0x060056CA RID: 22218
		[PreserveSig]
		int BindToObject(object refIID, object pAsmBindSink, IApplicationContext pApplicationContext, [MarshalAs(UnmanagedType.LPWStr)] string szCodeBase, long llFlags, int pvReserved, uint cbReserved, out int ppv);

		// Token: 0x060056CB RID: 22219
		[PreserveSig]
		int GetName(out uint lpcwBuffer, out int pwzName);

		// Token: 0x060056CC RID: 22220
		[PreserveSig]
		int GetVersion(out uint pdwVersionHi, out uint pdwVersionLow);

		// Token: 0x060056CD RID: 22221
		[PreserveSig]
		int IsEqual(IAssemblyName pName, uint dwCmpFlags);

		// Token: 0x060056CE RID: 22222
		[PreserveSig]
		int Clone(out IAssemblyName pName);
	}
}
