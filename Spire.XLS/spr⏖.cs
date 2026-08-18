using System;
using System.IO;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000348 RID: 840
internal static class spr\u23D6
{
	// Token: 0x06003322 RID: 13090 RVA: 0x001D4A5C File Offset: 0x001D3A5C
	public static short ᜂ(Stream A_0, byte[] A_1)
	{
		if (A_0.Read(A_1, 0, 2) != 2)
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
				throw new InvalidDataException();
			}
		}
		return BitConverter.ToInt16(A_1, 0);
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x001D4AB4 File Offset: 0x001D3AB4
	public static int ᜁ(Stream A_0, byte[] A_1)
	{
		if (A_0.Read(A_1, 0, 4) != 4)
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
				throw new InvalidDataException();
			}
		}
		return BitConverter.ToInt32(A_1, 0);
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x001D4B0C File Offset: 0x001D3B0C
	public static double ᜀ(Stream A_0, byte[] A_1)
	{
		if (A_0.Read(A_1, 0, 8) != 8)
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
				if (true)
				{
				}
				throw new InvalidDataException();
			}
		}
		return BitConverter.ToDouble(A_1, 0);
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x001D4B64 File Offset: 0x001D3B64
	public static int ᜀ(Stream A_0, short A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, 2);
		return 2;
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x001D4BB0 File Offset: 0x001D3BB0
	public static int ᜂ(Stream A_0, int A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, 4);
		return 4;
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x001D4BFC File Offset: 0x001D3BFC
	public static int ᜀ(Stream A_0, double A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, 8);
		return 8;
	}

	// Token: 0x06003328 RID: 13096 RVA: 0x001D4C48 File Offset: 0x001D3C48
	public static string ᜁ(Stream A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				byte[] a_ = new byte[4];
				int num = spr\u23D6.ᜁ(A_0, a_);
				array = new byte[num];
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_F2;
					case 1:
						goto IL_D6;
					case 2:
					{
						if (A_0.Read(array, 0, num) != num)
						{
							num2 = 5;
							continue;
						}
						int num3 = 0;
						num2 = 6;
						continue;
					}
					case 3:
					{
						int num3;
						if (array[num3] != 0)
						{
							if (true)
							{
							}
							num3++;
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8B;
						default:
							if (false)
							{
							}
							num2 = 8;
							continue;
						}
						break;
					}
					case 4:
						goto IL_113;
					case 5:
						goto IL_72;
					case 6:
						goto IL_D6;
					case 7:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 0;
							continue;
						}
						goto IL_8B;
					}
					case 8:
					{
						int num3;
						A_1 = num3;
						num2 = 4;
						continue;
					}
					}
					break;
					IL_8B:
					num2 = 3;
					continue;
					IL_D6:
					num2 = 7;
				}
			}
			IL_72:
			throw new IOException();
			IL_F2:
			IL_113:
			Encoding @default = Encoding.Default;
			string @string = @default.GetString(array, 0, A_1);
			return spr\u23D6.ᜀ(@string);
		}
		}
	}

	// Token: 0x06003329 RID: 13097 RVA: 0x001D4D84 File Offset: 0x001D3D84
	public static string ᜀ(Stream A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				IL_2B:
				byte[] a_ = new byte[4];
				int num = spr\u23D6.ᜁ(A_0, a_) * 2;
				byte[] array = new byte[num];
				for (;;)
				{
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_7E;
						case 1:
						{
							int num3;
							if (num3 != 0)
							{
								num2 = 4;
								continue;
							}
							return text;
						}
						case 2:
							goto IL_61;
						case 3:
						{
							if (A_0.Read(array, 0, num) != num)
							{
								num2 = 2;
								continue;
							}
							Encoding unicode = Encoding.Unicode;
							text = unicode.GetString(array, 0, num);
							text = spr\u23D6.ᜀ(text);
							int num3 = num % 4;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						case 4:
						{
							int num3;
							A_0.Position += (long)(4 - num3);
							num2 = 0;
							continue;
						}
						}
						goto IL_2B;
					}
					IL_61:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_E5;
					}
				}
			}
			IL_7E:
			return text;
			IL_E5:
			if (false)
			{
			}
			throw new IOException();
		}
		}
	}

	// Token: 0x0600332A RID: 13098 RVA: 0x001D4E84 File Offset: 0x001D3E84
	public static int ᜀ(Stream A_0, string A_1, bool A_2)
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
		Encoding @default = Encoding.Default;
		return spr\u23D6.ᜀ(A_0, A_1, @default, A_2);
	}

	// Token: 0x0600332B RID: 13099 RVA: 0x001D4ED0 File Offset: 0x001D3ED0
	public static int ᜀ(Stream A_0, string A_1)
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
		return spr\u23D6.ᜀ(A_0, A_1, Encoding.Unicode, true);
	}

	// Token: 0x0600332C RID: 13100 RVA: 0x001D4F18 File Offset: 0x001D3F18
	public static int ᜀ(Stream A_0, string A_1, Encoding A_2, bool A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 10;
			int num5;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_111;
				case 1:
				{
					int num2 = 0;
					int num4;
					int num3 = 4 - num4;
					num = 13;
					continue;
				}
				case 2:
					if (A_3)
					{
						num = 5;
						continue;
					}
					return num5;
				case 3:
				{
					int num4;
					if (num4 != 0)
					{
						num = 1;
						continue;
					}
					return num5;
				}
				case 4:
					goto IL_18A;
				case 5:
				{
					int num4 = num5 % 4;
					num = 3;
					continue;
				}
				case 6:
					A_1 = RecordTableEnumerator.b("㘵", a_);
					num = 4;
					continue;
				case 7:
					if (A_1[A_1.Length - 1] == '\0')
					{
						goto IL_18A;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_171;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				case 8:
					return num5;
				case 9:
					goto IL_18A;
				case 11:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 12;
						continue;
					}
					A_0.WriteByte(0);
					num2++;
					num = 0;
					continue;
				}
				case 12:
				{
					int num4;
					num5 += 4 - num4;
					num = 8;
					continue;
				}
				case 13:
					goto IL_171;
				case 14:
					A_1 += RecordTableEnumerator.b("㘵", a_);
					num = 9;
					continue;
				}
				if (string.IsNullOrEmpty(A_1))
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
				IL_111:
				num = 11;
				continue;
				IL_171:
				goto IL_111;
				IL_18A:
				byte[] bytes = A_2.GetBytes(A_1);
				int num6 = bytes.Length;
				int length = A_1.Length;
				byte[] bytes2 = BitConverter.GetBytes(length);
				A_0.Write(bytes2, 0, bytes2.Length);
				A_0.Write(bytes, 0, num6);
				num5 = 4 + num6;
				num = 2;
			}
			return num5;
		}
		}
	}

	// Token: 0x0600332D RID: 13101 RVA: 0x001D5128 File Offset: 0x001D4128
	public static void ᜀ(Stream A_0, ref int A_1)
	{
		for (;;)
		{
			int num = A_1 % 4;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num != 0)
					{
						num2 = 1;
						continue;
					}
					return;
				case 1:
				{
					if (true)
					{
					}
					int num3 = 0;
					int num4 = 4 - num;
					num2 = 4;
					continue;
				}
				case 2:
					goto IL_45;
				case 3:
					return;
				case 4:
					goto IL_45;
				case 5:
				{
					int num3;
					int num4;
					if (num3 < num4)
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
							A_0.WriteByte(0);
							num3++;
							A_1++;
							num2 = 2;
							continue;
						}
					}
					num2 = 3;
					continue;
				}
				}
				break;
				IL_45:
				num2 = 5;
			}
		}
	}

	// Token: 0x0600332E RID: 13102 RVA: 0x001D51E8 File Offset: 0x001D41E8
	private static string ᜀ(string A_0)
	{
		int num = 7;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				num2 = 0;
				goto IL_9D;
			case 1:
				num = 0;
				continue;
			case 2:
				if (A_0[num3 - 1] == '\0')
				{
					num = 3;
					continue;
				}
				return A_0;
			case 3:
				A_0 = A_0.Substring(0, num3 - 1);
				num = 5;
				continue;
			case 4:
				if (num3 <= 0)
				{
					return A_0;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7F;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 5:
				return A_0;
			case 6:
				if (true)
				{
				}
				num = 2;
				continue;
			case 8:
				num2 = A_0.Length;
				goto IL_9D;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_7F:
			num = 8;
			continue;
			IL_9D:
			num3 = num2;
			num = 4;
		}
		return A_0;
	}

	// Token: 0x0400164E RID: 5710
	public const int ᜀ = 4;

	// Token: 0x0400164F RID: 5711
	private const int ᜁ = 2;

	// Token: 0x04001650 RID: 5712
	private const int ᜂ = 8;
}
