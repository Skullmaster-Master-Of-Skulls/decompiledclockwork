using System;

namespace a.b
{
	// Token: 0x0200030D RID: 781
	internal class ff
	{
		// Token: 0x06001BE5 RID: 7141 RVA: 0x0007AABC File Offset: 0x00079ABC
		public static int a(char A_0)
		{
			if (A_0 >= '0' && A_0 <= '9')
			{
				return (int)A_0;
			}
			if (A_0 >= 'A' && A_0 <= 'Z')
			{
				return (int)(A_0 - '7');
			}
			if (A_0 >= 'a' && A_0 <= 'z')
			{
				return (int)(A_0 - 'W');
			}
			return -1;
		}
	}
}
