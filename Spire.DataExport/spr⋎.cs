using System;
using System.Globalization;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x0200011C RID: 284
internal class spr\u22CE
{
	// Token: 0x06000693 RID: 1683 RVA: 0x0003F060 File Offset: 0x0003E060
	public static string ᜁ(int A_0)
	{
		char c = (char)(65 + A_0 % 26);
		A_0 /= 26;
		if (A_0 != 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return ((ushort)(64 + A_0)).ToString() + c.ToString();
			}
		}
		if (true)
		{
		}
		return c.ToString();
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0003F0D0 File Offset: 0x0003E0D0
	public static int ᜃ(string A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return int.Parse(A_0) - 1;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0003F114 File Offset: 0x0003E114
	public static string ᜀ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return (A_0 + 1).ToString();
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0003F15C File Offset: 0x0003E15C
	internal static uint ᜀ(uint A_0, byte A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return A_0 << (int)A_1 | A_0 >> (int)(32 - A_1);
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0003F1B0 File Offset: 0x0003E1B0
	internal static int ᜀ(int A_0, byte A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return spr\u22CE.ᜀ((uint)A_0, A_1).GetHashCode();
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0003F1FC File Offset: 0x0003E1FC
	internal static ushort ᜀ(bool A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_59;
			case 3:
				goto IL_64;
			}
			if (true)
			{
			}
			if (!A_0)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
			}
			num = 0;
		}
		IL_59:
		ushort num2 = 0;
		goto IL_71;
		IL_64:
		num2 = 1;
		IL_71:
		return num2;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0003F27C File Offset: 0x0003E27C
	public static byte[] ᜀ(object[] A_0)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[A_0.Length];
			int num = 0;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_5B;
				case 1:
					goto IL_5B;
				case 2:
					return array;
				case 3:
					if (num >= A_0.Length)
					{
						num2 = 2;
						continue;
					}
					array[num] = (byte)A_0[num];
					num++;
					num2 = 1;
					continue;
				}
				break;
				IL_5B:
				num2 = 3;
			}
		}
		return array;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0003F31C File Offset: 0x0003E31C
	public static object[] ᜁ(byte[] A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		object[] array = new object[A_0.Length];
		Array.Copy(A_0, 0, array, 0, A_0.Length);
		return array;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0003F370 File Offset: 0x0003E370
	public static bool ᜀ(Array A_0, object A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Array.IndexOf(A_0, A_1) != -1;
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0003F3B8 File Offset: 0x0003E3B8
	public static bool ᜀ(uint A_0, uint A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return (A_0 & A_1) != 0U;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0003F3FC File Offset: 0x0003E3FC
	public static bool ᜀ(ushort A_0, ushort A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return (A_0 & A_1) != 0;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0003F440 File Offset: 0x0003E440
	public static ushort ᜀ(ushort A_0, ushort A_1, bool A_2)
	{
		while (A_2)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return A_0 | A_1;
			}
		}
		return A_0 & ~A_1;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0003F48C File Offset: 0x0003E48C
	public static uint ᜀ(uint A_0, uint A_1, uint A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 &= ~A_1;
		A_0 += (A_2 & A_1);
		return A_0;
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0003F4D8 File Offset: 0x0003E4D8
	public static ushort ᜀ(ushort A_0, ushort A_1, ushort A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 &= ~A_1;
		A_0 += (A_2 & A_1);
		return A_0;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0003F528 File Offset: 0x0003E528
	public static byte ᜀ(byte A_0, byte A_1, bool A_2)
	{
		for (;;)
		{
			A_0 &= ~A_1;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return A_0;
				case 1:
					A_0 += A_1;
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_2)
						{
							num = 1;
							continue;
						}
						return A_0;
					}
					break;
				}
				break;
			}
		}
		return A_0;
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0003F5A8 File Offset: 0x0003E5A8
	public static string ᜀ(bool A_0, byte[] A_1, int A_2, int A_3)
	{
		string result;
		for (;;)
		{
			result = string.Empty;
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_5B;
				case 1:
					if (A_0)
					{
						num = 2;
						continue;
					}
					result = Encoding.ASCII.GetString(A_1, A_2, A_3);
					num = 0;
					continue;
				case 2:
					result = Encoding.Unicode.GetString(A_1, A_2, A_3 * 2);
					num = 3;
					continue;
				case 3:
					goto IL_75;
				}
				break;
			}
		}
		IL_5B:
		IL_75:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_75;
		default:
			if (false)
			{
			}
			return result;
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0003F64C File Offset: 0x0003E64C
	public static byte[] ᜂ(string A_0)
	{
		byte[] array;
		for (;;)
		{
			int num = spr\u22CE.ᜁ(A_0);
			array = new byte[num];
			int num2 = 0;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num3 = 1;
				break;
			}
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return array;
				case 1:
					goto IL_58;
				case 2:
					if (num2 >= num)
					{
						num3 = 0;
						continue;
					}
					if (true)
					{
					}
					array[num2] = byte.Parse(A_0.Substring(num2 * 3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					num2++;
					num3 = 3;
					continue;
				case 3:
					goto IL_58;
				}
				break;
				IL_58:
				num3 = 2;
			}
		}
		return array;
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0003F700 File Offset: 0x0003E700
	public static int ᜁ(string A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return A_0.Length / 3 + 1;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0003F748 File Offset: 0x0003E748
	public static string ᜀ(byte[] A_0)
	{
		int a_ = 1;
		StringBuilder stringBuilder;
		for (;;)
		{
			int num = Math.Max(A_0.Length * 3 - 1, 1);
			stringBuilder = new StringBuilder(num, num);
			int num2 = 0;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_D5;
				case 1:
					goto IL_D5;
				case 2:
					if (num2 >= A_0.Length)
					{
						num3 = 3;
						continue;
					}
					num3 = 6;
					continue;
				case 3:
					goto IL_F1;
				case 4:
					goto IL_52;
				case 5:
					stringBuilder.Append(' ');
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BA;
					default:
						if (false)
						{
						}
						num3 = 4;
						continue;
					}
					break;
				case 6:
					goto IL_BA;
				}
				break;
				IL_52:
				stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("昜⼞ᬠ笢ᜤ娦", a_), A_0[num2]);
				num2++;
				num3 = 0;
				continue;
				IL_D5:
				num3 = 2;
				continue;
				IL_BA:
				if (num2 <= 0)
				{
					goto IL_52;
				}
				num3 = 5;
			}
		}
		IL_F1:
		if (true)
		{
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0003F858 File Offset: 0x0003E858
	private static int ᜀ(char A_0)
	{
		int a_ = 10;
		for (;;)
		{
			if (true)
			{
			}
			int num = (int)(char.ToUpper(A_0) - 'A');
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_6F;
				case 1:
					if (num > 25)
					{
						num2 = 3;
						continue;
					}
					return num;
				case 2:
					num2 = 1;
					continue;
				case 3:
					goto IL_BC;
				}
				break;
			}
		}
		IL_6F:
		throw new ArgumentOutOfRangeException(HyperlinksCollectionEditor.b("攥䜧䘩夫䌭帯縱儳䈵䰷弹主", a_), A_0, ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("朥娧䴩弫焭猯崱堳䌵唷吹瀻嬽㐿㙁⅃㑅Ň⑉⽋⅍≏⁑ㅓ㕕ⱗ", a_)));
		IL_BC:
		goto IL_6F;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0003F924 File Offset: 0x0003E924
	public static int ᜀ(string A_0)
	{
		int a_ = 18;
		for (;;)
		{
			int num = -1;
			int num2 = 9;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= 0)
					{
						num2 = 5;
						continue;
					}
					goto IL_ED;
				case 1:
					num = (spr\u22CE.ᜀ(A_0[0]) + 1) * 26 + spr\u22CE.ᜀ(A_0[1]);
					num2 = 2;
					continue;
				case 2:
					goto IL_13E;
				case 3:
					num = spr\u22CE.ᜀ(A_0[0]);
					if (true)
					{
					}
					num2 = 8;
					continue;
				case 4:
					if (A_0.Length == 2)
					{
						num2 = 1;
						continue;
					}
					goto IL_13E;
				case 5:
					num2 = 6;
					continue;
				case 6:
					if (num > 255)
					{
						num2 = 7;
						continue;
					}
					return num;
				case 7:
					goto IL_9D;
				case 8:
					goto IL_13E;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						if (A_0.Length == 1)
						{
							num2 = 3;
							continue;
						}
						num2 = 4;
						continue;
					}
					break;
				}
				break;
				IL_13E:
				num2 = 0;
			}
		}
		IL_9D:
		IL_ED:
		throw new ArgumentOutOfRangeException(HyperlinksCollectionEditor.b("洭弯帱䄳嬵嘷琹崻匽┿", a_), A_0, ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("漭䈯唱䜳椵笷唹倻䬽ⴿⱁ੃❅╇⽉Ջ⁍㍏㵑♓⑕㵗㥙⡛", a_)));
	}
}
