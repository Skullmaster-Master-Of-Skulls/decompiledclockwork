using System;
using System.Collections;

namespace a.c
{
	// Token: 0x0200022E RID: 558
	internal class i : IComparer
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x00053850 File Offset: 0x00052850
		int IComparer.a(object A_0, object A_1)
		{
			int num = (int)((Hashtable)A_0)["specifity"];
			int num2 = (int)((Hashtable)A_1)["specifity"];
			if (num > num2)
			{
				return 1;
			}
			if (num < num2)
			{
				return -1;
			}
			return 0;
		}
	}
}
