using System;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x020001CC RID: 460
internal static class sprữ
{
	// Token: 0x060013CE RID: 5070 RVA: 0x00149358 File Offset: 0x00148358
	public static short ᜂ(Stream A_0, byte[] A_1)
	{
		int a_ = 13;
		if (A_0.Read(A_1, 0, 2) != 2)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_45;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_45:
			throw new Exception(ClipboardData.b("㩲᭴Ŷᡸ᝺ᑼ᭾ꆀ잂", a_));
		}
		return BitConverter.ToInt16(A_1, 0);
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x001493C8 File Offset: 0x001483C8
	public static int ᜁ(Stream A_0, byte[] A_1)
	{
		int a_ = 15;
		if (A_0.Read(A_1, 0, 4) != 4)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_45;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_45:
			throw new Exception(ClipboardData.b("㱴᥶ླྀ᩺ᅼᙾꎂﶈ", a_));
		}
		return BitConverter.ToInt32(A_1, 0);
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x00149438 File Offset: 0x00148438
	public static double ᜀ(Stream A_0, byte[] A_1)
	{
		int a_ = 5;
		if (A_0.Read(A_1, 0, 8) != 8)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3D;
			}
			if (false)
			{
			}
			IL_3D:
			if (true)
			{
			}
			throw new Exception(ClipboardData.b("≪ͬ᥮ၰὲᱴ፶奸ὺᱼ୾", a_));
		}
		return BitConverter.ToDouble(A_1, 0);
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x001494A8 File Offset: 0x001484A8
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

	// Token: 0x060013D2 RID: 5074 RVA: 0x001494F4 File Offset: 0x001484F4
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

	// Token: 0x060013D3 RID: 5075 RVA: 0x00149540 File Offset: 0x00148540
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

	// Token: 0x060013D4 RID: 5076 RVA: 0x0014958C File Offset: 0x0014858C
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
				int num = sprữ.ᜁ(A_0, a_);
				array = new byte[num];
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_DC;
					case 1:
					{
						int num3;
						A_1 = num3;
						num2 = 7;
						continue;
					}
					case 2:
					{
						int num3;
						if (num3 >= num)
						{
							num2 = 6;
							continue;
						}
						num2 = 4;
						continue;
					}
					case 3:
						if (A_0.Read(array, 0, num) == num)
						{
							int num3 = 0;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 8;
							continue;
						}
						break;
					case 4:
					{
						int num3;
						if (array[num3] == 0)
						{
							num2 = 1;
							continue;
						}
						num3++;
						num2 = 5;
						continue;
					}
					case 5:
						goto IL_DC;
					case 6:
						goto IL_F8;
					case 7:
						goto IL_119;
					case 8:
						goto IL_99;
					}
					break;
					IL_DC:
					num2 = 2;
				}
			}
			IL_99:
			throw new IOException();
			IL_F8:
			IL_119:
			IL_11B:
			Encoding @default = Encoding.Default;
			string @string = @default.GetString(array, 0, A_1);
			return sprữ.ᜀ(@string);
		}
		}
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x001496D0 File Offset: 0x001486D0
	public static string ᜀ(Stream A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			byte[] array;
			for (;;)
			{
				byte[] a_ = new byte[4];
				num = sprữ.ᜁ(A_0, a_) * 2;
				array = new byte[num];
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						A_0.Position += (long)num3;
						goto IL_7C;
					}
					case 1:
						goto IL_6B;
					case 2:
					{
						if (A_0.Read(array, 0, num) != num)
						{
							num2 = 1;
							continue;
						}
						int num3 = 4 - num % 4;
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					case 3:
					{
						int num3;
						if (num3 != 4)
						{
							num2 = 0;
							continue;
						}
						goto IL_B9;
					}
					case 4:
						goto IL_B9;
					}
					break;
					IL_7C:
					num2 = 4;
					continue;
					IL_B9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						goto IL_CF;
					}
				}
			}
			IL_6B:
			throw new IOException();
			IL_CF:
			if (false)
			{
			}
			Encoding unicode = Encoding.Unicode;
			string @string = unicode.GetString(array, 0, num);
			return sprữ.ᜀ(@string);
		}
		}
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x001497D0 File Offset: 0x001487D0
	public static int ᜁ(Stream A_0, string A_1)
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
		return sprữ.ᜀ(A_0, A_1, @default, false);
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x0014981C File Offset: 0x0014881C
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
		return sprữ.ᜀ(A_0, A_1, Encoding.Unicode, true);
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x00149864 File Offset: 0x00148864
	public static int ᜀ(Stream A_0, string A_1, Encoding A_2, bool A_3 = false)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 1;
			int num3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_152;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					A_1 += ClipboardData.b("橩", a_);
					num = 0;
					continue;
				case 3:
					A_1 = ClipboardData.b("橩", a_);
					num = 8;
					continue;
				case 4:
				{
					int num2;
					byte[] array = new byte[num2];
					A_0.Write(array, 0, array.Length);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_95;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				}
				case 5:
					if (A_3)
					{
						num = 6;
						continue;
					}
					goto IL_1A0;
				case 6:
				{
					int num2 = 4 - num3 % 4;
					num = 10;
					continue;
				}
				case 7:
					if (A_1[A_1.Length - 1] != '\0')
					{
						num = 2;
						continue;
					}
					goto IL_152;
				case 8:
					goto IL_152;
				case 9:
					goto IL_123;
				case 10:
				{
					int num2;
					if (num2 < 4)
					{
						goto IL_95;
					}
					goto IL_1A0;
				}
				}
				if (string.IsNullOrEmpty(A_1))
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
				IL_95:
				num = 4;
				continue;
				IL_152:
				byte[] bytes = A_2.GetBytes(A_1);
				num3 = bytes.Length;
				int length = A_1.Length;
				byte[] bytes2 = BitConverter.GetBytes(length);
				A_0.Write(bytes2, 0, bytes2.Length);
				A_0.Write(bytes, 0, num3);
				num = 5;
			}
			IL_123:
			IL_1A0:
			return 4 + num3;
		}
		}
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x00149A14 File Offset: 0x00148A14
	public static void ᜀ(Stream A_0, ref int A_1)
	{
		for (;;)
		{
			int num = A_1 % 4;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_4C;
				case 1:
					if (num != 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_90;
				case 2:
				{
					if (true)
					{
					}
					int num3 = 0;
					int num4 = 4 - num;
					num2 = 0;
					continue;
				}
				case 3:
				{
					int num3;
					int num4;
					if (num3 >= num4)
					{
						goto IL_58;
					}
					A_0.WriteByte(0);
					num3++;
					A_1++;
					num2 = 5;
					continue;
				}
				case 4:
					goto IL_90;
				case 5:
					goto IL_4C;
				}
				break;
				IL_4C:
				num2 = 3;
				continue;
				IL_58:
				num2 = 4;
				continue;
				IL_90:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
					goto IL_A6;
				}
			}
		}
		IL_A6:
		if (false)
		{
		}
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x00149AD0 File Offset: 0x00148AD0
	private static string ᜀ(string A_0)
	{
		int num = 4;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				return A_0;
			case 1:
				num2 = A_0.Length;
				goto IL_C6;
			case 2:
				num = 3;
				continue;
			case 3:
				num2 = 0;
				goto IL_C6;
			case 5:
				if (A_0[num3 - 1] == '\0')
				{
					num = 8;
					continue;
				}
				return A_0;
			case 6:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8C;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 7:
				if (num3 > 0)
				{
					num = 6;
					continue;
				}
				return A_0;
			case 8:
				goto IL_8C;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 1;
			continue;
			IL_8C:
			A_0 = A_0.Substring(0, num3 - 1);
			num = 0;
			continue;
			IL_C6:
			num3 = num2;
			num = 7;
		}
		return A_0;
	}

	// Token: 0x040018E6 RID: 6374
	public const int ᜀ = 4;

	// Token: 0x040018E7 RID: 6375
	private const int ᜁ = 2;

	// Token: 0x040018E8 RID: 6376
	private const int ᜂ = 8;
}
