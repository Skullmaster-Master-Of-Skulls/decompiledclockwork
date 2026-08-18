using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000224 RID: 548
	[Guid("A7549A29-A7C4-42e1-8DC1-7E3D748DC24A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IContextSecurityPerimeter
	{
		// Token: 0x06001092 RID: 4242
		[return: MarshalAs(UnmanagedType.Bool)]
		bool GetPerimeterFlag();

		// Token: 0x06001093 RID: 4243
		void SetPerimeterFlag([MarshalAs(UnmanagedType.Bool)] bool flag);
	}
}
