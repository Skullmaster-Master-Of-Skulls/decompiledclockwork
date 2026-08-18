using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000541 RID: 1345
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumVARIANT instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[ComImport]
	public interface UCOMIEnumVARIANT
	{
		// Token: 0x0600335C RID: 13148
		[PreserveSig]
		int Next(int celt, int rgvar, int pceltFetched);

		// Token: 0x0600335D RID: 13149
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600335E RID: 13150
		[PreserveSig]
		int Reset();

		// Token: 0x0600335F RID: 13151
		void Clone(int ppenum);
	}
}
