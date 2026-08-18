using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200016A RID: 362
internal class spr\u2008 : IComparer, IComparer<string>
{
	// Token: 0x06000C4E RID: 3150 RVA: 0x000CF8C8 File Offset: 0x000CE8C8
	public int ᜀ(object A_0, object A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			string text;
			string text2;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8C:
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					text = A_0.ToString();
					text2 = A_1.ToString();
					int length = text.Length;
					int length2 = text2.Length;
					num2 = length - length2;
					num = 6;
				}
				break;
			default:
				if (false)
				{
				}
				num = 7;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return 0;
				case 1:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_116;
				case 2:
					return -1;
				case 3:
					num2 = StringComparer.Ordinal.Compare(text, text2);
					num = 9;
					continue;
				case 4:
					return 1;
				case 5:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 10;
					continue;
				case 6:
					if (num2 == 0)
					{
						num = 3;
						continue;
					}
					return num2;
				case 8:
					if (true)
					{
					}
					num = 1;
					continue;
				case 9:
					goto IL_B3;
				case 10:
					goto IL_8C;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				IL_116:
				num = 5;
			}
			return -1;
			IL_B3:
			return num2;
		}
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x000CFA18 File Offset: 0x000CEA18
	public int ᜀ(string A_0, string A_1)
	{
		int num = 8;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return 1;
			case 1:
				goto IL_85;
			case 2:
			{
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				int length = A_0.Length;
				int length2 = A_1.Length;
				num2 = length - length2;
				num = 9;
				continue;
			}
			case 3:
				num = 5;
				continue;
			case 4:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 2;
				continue;
			case 5:
				if (A_1 == null)
				{
					goto IL_BB;
				}
				goto IL_101;
			case 6:
				num2 = StringComparer.Ordinal.Compare(A_0.ToUpper(), A_1.ToUpper());
				num = 7;
				continue;
			case 7:
				goto IL_AB;
			case 9:
				if (num2 == 0)
				{
					num = 6;
					continue;
				}
				return num2;
			case 10:
				return 0;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_BB:
				num = 10;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				break;
			}
			IL_101:
			num = 4;
		}
		IL_85:
		if (true)
		{
		}
		return -1;
		IL_AB:
		return num2;
	}
}
