using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200056C RID: 1388
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IConnectionPoint
	{
		// Token: 0x060033CB RID: 13259
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x060033CC RID: 13260
		void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

		// Token: 0x060033CD RID: 13261
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x060033CE RID: 13262
		void Unadvise(int dwCookie);

		// Token: 0x060033CF RID: 13263
		void EnumConnections(out IEnumConnections ppEnum);
	}
}
