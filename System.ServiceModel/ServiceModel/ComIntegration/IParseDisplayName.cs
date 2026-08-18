using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000227 RID: 551
	[SuppressUnmanagedCodeSecurity]
	[Guid("0000011a-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IParseDisplayName
	{
		// Token: 0x060010A9 RID: 4265
		void ParseDisplayName(IBindCtx pbc, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, IntPtr pchEaten, IntPtr ppmkOut);
	}
}
