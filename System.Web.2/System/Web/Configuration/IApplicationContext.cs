using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x02000700 RID: 1792
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7c23ff90-33af-11d3-95da-00a024a85b51")]
	[ComImport]
	internal interface IApplicationContext
	{
		// Token: 0x060056B8 RID: 22200
		void SetContextNameObject(IAssemblyName pName);

		// Token: 0x060056B9 RID: 22201
		void GetContextNameObject(out IAssemblyName ppName);

		// Token: 0x060056BA RID: 22202
		void Set([MarshalAs(UnmanagedType.LPWStr)] string szName, int pvValue, uint cbValue, uint dwFlags);

		// Token: 0x060056BB RID: 22203
		void Get([MarshalAs(UnmanagedType.LPWStr)] string szName, out int pvValue, ref uint pcbValue, uint dwFlags);

		// Token: 0x060056BC RID: 22204
		void GetDynamicDirectory(out int wzDynamicDir, ref uint pdwSize);
	}
}
