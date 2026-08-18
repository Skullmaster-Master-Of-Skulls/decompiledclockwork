using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000ED RID: 237
	[CLSCompliant(false)]
	[Serializable]
	public enum CharacterTypes : sbyte
	{
		// Token: 0x04000437 RID: 1079
		WHITESPACE = 1,
		// Token: 0x04000438 RID: 1080
		NUMERIC,
		// Token: 0x04000439 RID: 1081
		ALPHABETIC = 4,
		// Token: 0x0400043A RID: 1082
		STRINGQUOTE = 8,
		// Token: 0x0400043B RID: 1083
		COMMENTCHAR = 16
	}
}
