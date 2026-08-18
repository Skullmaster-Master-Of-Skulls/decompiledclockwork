using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000538 RID: 1336
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPointContainer instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[ComImport]
	public interface UCOMIConnectionPointContainer
	{
		// Token: 0x06003341 RID: 13121
		void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

		// Token: 0x06003342 RID: 13122
		void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
	}
}
