using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000239 RID: 569
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000109-0000-0000-C000-000000000046")]
	internal interface IPersistStream : IPersist
	{
		// Token: 0x060010F3 RID: 4339
		void GetClassID(out Guid pClassID);

		// Token: 0x060010F4 RID: 4340
		[PreserveSig]
		int IsDirty();

		// Token: 0x060010F5 RID: 4341
		void Load([In] IStream pStm);

		// Token: 0x060010F6 RID: 4342
		void Save([In] IStream pStm, [MarshalAs(UnmanagedType.Bool)] [In] bool fClearDirty);

		// Token: 0x060010F7 RID: 4343
		void GetSizeMax(out long pcbSize);
	}
}
