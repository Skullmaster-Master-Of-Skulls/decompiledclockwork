using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000579 RID: 1401
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IPersistFile
	{
		// Token: 0x0600340C RID: 13324
		void GetClassID(out Guid pClassID);

		// Token: 0x0600340D RID: 13325
		[PreserveSig]
		int IsDirty();

		// Token: 0x0600340E RID: 13326
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x0600340F RID: 13327
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x06003410 RID: 13328
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x06003411 RID: 13329
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
