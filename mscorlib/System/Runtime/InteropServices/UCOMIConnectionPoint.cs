using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000539 RID: 1337
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPoint instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[ComImport]
	public interface UCOMIConnectionPoint
	{
		// Token: 0x06003343 RID: 13123
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x06003344 RID: 13124
		void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

		// Token: 0x06003345 RID: 13125
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x06003346 RID: 13126
		void Unadvise(int dwCookie);

		// Token: 0x06003347 RID: 13127
		void EnumConnections(out UCOMIEnumConnections ppEnum);
	}
}
