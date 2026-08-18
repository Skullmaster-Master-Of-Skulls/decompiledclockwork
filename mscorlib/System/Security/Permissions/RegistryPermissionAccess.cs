using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000663 RID: 1635
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum RegistryPermissionAccess
	{
		// Token: 0x04001E89 RID: 7817
		NoAccess = 0,
		// Token: 0x04001E8A RID: 7818
		Read = 1,
		// Token: 0x04001E8B RID: 7819
		Write = 2,
		// Token: 0x04001E8C RID: 7820
		Create = 4,
		// Token: 0x04001E8D RID: 7821
		AllAccess = 7
	}
}
