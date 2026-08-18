using System;

// Token: 0x0200055A RID: 1370
internal class spr\u23DF : spr\u24A5
{
	// Token: 0x060052BC RID: 21180 RVA: 0x0033B05C File Offset: 0x0033A05C
	public string ᜀ(string A_0)
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 2;
				continue;
			case 1:
				num = 4;
				continue;
			case 2:
				if (A_0.Length > 0)
				{
					num = 1;
					continue;
				}
				return A_0;
			case 3:
				return A_0;
			case 4:
				if (A_0[0] != '/')
				{
					num = 5;
					continue;
				}
				return A_0;
			case 5:
				A_0 = '/' + A_0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 0;
		}
		return A_0;
	}
}
