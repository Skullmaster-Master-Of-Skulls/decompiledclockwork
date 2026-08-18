using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000269 RID: 617
	[Guid("C7CD7379-F3F2-4634-811B-703281D73E08")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServiceSxsConfig
	{
		// Token: 0x060011A3 RID: 4515
		void SxsConfig(CSC_SxsConfig sxsConfig);

		// Token: 0x060011A4 RID: 4516
		void SxsName([MarshalAs(UnmanagedType.LPWStr)] string szSxsName);

		// Token: 0x060011A5 RID: 4517
		void SxsDirectory([MarshalAs(UnmanagedType.LPWStr)] string szSxsDirectory);
	}
}
