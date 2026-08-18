using System;
using System.Runtime.InteropServices;

namespace System.Net.Mail
{
	// Token: 0x02000690 RID: 1680
	[StructLayout(LayoutKind.Sequential)]
	internal class _METADATA_HANDLE_INFO
	{
		// Token: 0x060033E6 RID: 13286 RVA: 0x000DB5E9 File Offset: 0x000DA5E9
		private _METADATA_HANDLE_INFO()
		{
			this.dwMDPermissions = 0;
			this.dwMDSystemChangeNumber = 0;
		}

		// Token: 0x04002FF0 RID: 12272
		internal int dwMDPermissions;

		// Token: 0x04002FF1 RID: 12273
		internal int dwMDSystemChangeNumber;
	}
}
