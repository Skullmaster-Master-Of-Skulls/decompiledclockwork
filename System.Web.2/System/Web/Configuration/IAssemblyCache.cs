using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x02000701 RID: 1793
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
	[ComImport]
	internal interface IAssemblyCache
	{
		// Token: 0x060056BD RID: 22205
		[PreserveSig]
		int UninstallAssembly(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] string pszAssemblyName, IntPtr pvReserved, out uint pulDisposition);

		// Token: 0x060056BE RID: 22206
		[PreserveSig]
		int QueryAssemblyInfo(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] string pszAssemblyName, IntPtr pAsmInfo);

		// Token: 0x060056BF RID: 22207
		[PreserveSig]
		int CreateAssemblyCacheItem(uint dwFlags, IntPtr pvReserved, out IAssemblyCacheItem ppAsmItem, [MarshalAs(UnmanagedType.LPWStr)] string pszAssemblyName);

		// Token: 0x060056C0 RID: 22208
		[PreserveSig]
		int CreateAssemblyScavenger(out object ppAsmScavenger);

		// Token: 0x060056C1 RID: 22209
		[PreserveSig]
		int InstallAssembly(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] string pszManifestFilePath, IntPtr pvReserved);
	}
}
