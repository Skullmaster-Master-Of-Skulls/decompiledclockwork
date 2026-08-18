using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x02000708 RID: 1800
	internal interface IGac
	{
		// Token: 0x060056EB RID: 22251
		[DispId(13)]
		void GacInstall([MarshalAs(UnmanagedType.BStr)] string assemblyPath);
	}
}
