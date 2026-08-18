using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200052D RID: 1325
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FA1F3615-ACB9-486d-9EAC-1BEF87E36B09")]
	[ComVisible(true)]
	public interface ITypeLibExporterNameProvider
	{
		// Token: 0x060032F9 RID: 13049
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		string[] GetNames();
	}
}
