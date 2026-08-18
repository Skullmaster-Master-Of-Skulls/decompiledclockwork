using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public enum TokenTypes
	{
		// Token: 0x040004C0 RID: 1216
		EOL = 10,
		// Token: 0x040004C1 RID: 1217
		EOF = -1,
		// Token: 0x040004C2 RID: 1218
		NUMBER = -2,
		// Token: 0x040004C3 RID: 1219
		WORD = -3,
		// Token: 0x040004C4 RID: 1220
		REAL = -4,
		// Token: 0x040004C5 RID: 1221
		STRING = -5
	}
}
