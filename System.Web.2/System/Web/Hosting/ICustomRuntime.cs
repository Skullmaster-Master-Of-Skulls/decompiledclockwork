using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000792 RID: 1938
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("692D0723-C338-4D09-9057-C71F0F47DA87")]
	[ComImport]
	internal interface ICustomRuntime
	{
		// Token: 0x06005C8D RID: 23693
		void Start([In] IntPtr reserved0, [In] int reserved1);

		// Token: 0x06005C8E RID: 23694
		void ResolveModules([In] IntPtr pResolveModuleData, [In] int resolveModuleDataSize);

		// Token: 0x06005C8F RID: 23695
		void Stop([In] IntPtr reserved0, [In] int reserved1);
	}
}
