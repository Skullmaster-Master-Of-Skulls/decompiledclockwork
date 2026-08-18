using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200057A RID: 1402
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000010-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IRunningObjectTable
	{
		// Token: 0x06003412 RID: 13330
		int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

		// Token: 0x06003413 RID: 13331
		void Revoke(int dwRegister);

		// Token: 0x06003414 RID: 13332
		[PreserveSig]
		int IsRunning(IMoniker pmkObjectName);

		// Token: 0x06003415 RID: 13333
		[PreserveSig]
		int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x06003416 RID: 13334
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x06003417 RID: 13335
		[PreserveSig]
		int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x06003418 RID: 13336
		void EnumRunning(out IEnumMoniker ppenumMoniker);
	}
}
