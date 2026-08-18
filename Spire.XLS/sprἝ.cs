using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E9 RID: 1257
internal class sprἝ : IComparer, IComparer<string>
{
	// Token: 0x06004D03 RID: 19715 RVA: 0x002EF574 File Offset: 0x002EE574
	public int ᜀ(object A_0, object A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C0;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_103;
					default:
						goto IL_94;
					}
					break;
				case 3:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 9;
					continue;
				case 4:
					return 1;
				case 5:
					return -1;
				case 6:
				{
					string text;
					string text2;
					num2 = StringComparer.Ordinal.Compare(text, text2);
					num = 0;
					continue;
				}
				case 7:
					num = 10;
					continue;
				case 8:
					if (num2 == 0)
					{
						num = 6;
						continue;
					}
					return num2;
				case 9:
				{
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					string text = A_0.ToString();
					string text2 = A_1.ToString();
					int length = text.Length;
					int length2 = text2.Length;
					num2 = length - length2;
					goto IL_103;
				}
				case 10:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					goto IL_123;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				goto IL_123;
				IL_103:
				num = 8;
				continue;
				IL_123:
				num = 3;
			}
			return -1;
			IL_94:
			if (false)
			{
			}
			return 0;
			IL_C0:
			return num2;
		}
		}
	}

	// Token: 0x06004D04 RID: 19716 RVA: 0x002EF6C8 File Offset: 0x002EE6C8
	public int ᜀ(string A_0, string A_1)
	{
		int num = 10;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					num = 6;
					continue;
				}
				goto IL_107;
			case 1:
				return -1;
			case 2:
				num = 0;
				continue;
			case 3:
				return 1;
			case 4:
				num2 = StringComparer.Ordinal.Compare(A_0.ToUpper(), A_1.ToUpper());
				num = 5;
				continue;
			case 5:
				goto IL_8F;
			case 6:
				return 0;
			case 7:
			{
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				int length = A_0.Length;
				int length2 = A_1.Length;
				num2 = length - length2;
				num = 9;
				continue;
			}
			case 8:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
			case 9:
				if (num2 == 0)
				{
					num = 4;
					continue;
				}
				return num2;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_107:
			num = 8;
		}
		return -1;
		IL_8F:
		return num2;
	}
}
