using System;

namespace TechnoPro.Common.Public.Entities.Login
{
	// Token: 0x02000193 RID: 403
	public enum eLoginMethod
	{
		// Token: 0x04000793 RID: 1939
		ClockWorkLogin,
		// Token: 0x04000794 RID: 1940
		WindowsLogin,
		// Token: 0x04000795 RID: 1941
		Ldap,
		// Token: 0x04000796 RID: 1942
		ActiveDirectory = 4,
		// Token: 0x04000797 RID: 1943
		Shiboleth = 8
	}
}
