using System;
using System.Runtime.InteropServices;

namespace System.Net.Mail
{
	// Token: 0x02000261 RID: 609
	[StructLayout(LayoutKind.Sequential)]
	internal class _METADATA_HANDLE_INFO
	{
		// Token: 0x06001704 RID: 5892 RVA: 0x0007659A File Offset: 0x0007479A
		private _METADATA_HANDLE_INFO()
		{
			this.dwMDPermissions = 0;
			this.dwMDSystemChangeNumber = 0;
		}

		// Token: 0x0400179B RID: 6043
		internal int dwMDPermissions;

		// Token: 0x0400179C RID: 6044
		internal int dwMDSystemChangeNumber;
	}
}
