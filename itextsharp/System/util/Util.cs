using System;
using System.Globalization;

namespace System.util
{
	// Token: 0x020003C2 RID: 962
	public static class Util
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x000CA238 File Offset: 0x000C9238
		public static int USR(int op1, int op2)
		{
			if (op2 < 1)
			{
				return op1;
			}
			return (int)((uint)op1 >> op2);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000CA246 File Offset: 0x000C9246
		public static bool EqualsIgnoreCase(string s1, string s2)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000CA25D File Offset: 0x000C925D
		public static int CompareToIgnoreCase(string s1, string s2)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase);
		}
	}
}
