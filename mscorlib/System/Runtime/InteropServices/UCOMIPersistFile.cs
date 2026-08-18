using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000546 RID: 1350
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IPersistFile instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[ComImport]
	public interface UCOMIPersistFile
	{
		// Token: 0x06003384 RID: 13188
		void GetClassID(out Guid pClassID);

		// Token: 0x06003385 RID: 13189
		[PreserveSig]
		int IsDirty();

		// Token: 0x06003386 RID: 13190
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x06003387 RID: 13191
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x06003388 RID: 13192
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x06003389 RID: 13193
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
