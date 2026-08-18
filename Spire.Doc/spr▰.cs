using System;
using System.Collections;

// Token: 0x0200022A RID: 554
internal class spr\u25B0 : IComparer
{
	// Token: 0x06001AAA RID: 6826 RVA: 0x001BDAD8 File Offset: 0x001BCAD8
	public int ᜀ(object A_0, object A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return 1;
			case 2:
				if ((float)A_0 > (float)A_1)
				{
					num = 1;
					continue;
				}
				return -1;
			case 3:
				goto IL_60;
			}
			if ((float)A_0 - (float)A_1 < 0.0001f)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return 0;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			else
			{
				num = 2;
			}
		}
		IL_60:
		return 0;
	}

	// Token: 0x04001E1D RID: 7709
	public spr\u25B0 ᜀ = new spr\u25B0();
}
