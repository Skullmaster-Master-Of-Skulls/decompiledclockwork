using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000019 RID: 25
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("09829352-87C2-418D-8D79-4133969A489D")]
	[ComImport]
	internal interface IAppHostChangeHandler
	{
		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnSectionChanges([MarshalAs(UnmanagedType.BStr)] [In] string bstrSectionName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrConfigPath);
	}
}
