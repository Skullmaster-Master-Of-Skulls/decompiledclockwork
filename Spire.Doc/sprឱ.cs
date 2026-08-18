using System;
using System.Collections;
using System.IO;
using System.Text;
using Spire.Doc.Fields.Shape;

// Token: 0x020003A8 RID: 936
internal class sprឱ
{
	// Token: 0x060034E3 RID: 13539 RVA: 0x0030D1DC File Offset: 0x0030C1DC
	private sprឱ()
	{
	}

	// Token: 0x060034E4 RID: 13540 RVA: 0x0030D1F0 File Offset: 0x0030C1F0
	internal static Encoding ᜀ(bool A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_33;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (A_0)
		{
			return sprឱ.ᜀ;
		}
		IL_33:
		return sprឱ.ᜁ;
	}

	// Token: 0x060034E5 RID: 13541 RVA: 0x0030D23C File Offset: 0x0030C23C
	internal static string ᜀ(BinaryReader A_0, bool A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			string string2;
			for (;;)
			{
				int num2;
				string @string;
				switch (num)
				{
				case 0:
					num2 = (int)(A_0.ReadUInt16() * 2);
					num = 1;
					continue;
				case 1:
					if (!spr\u1CC6.ᜀ(A_0, num2))
					{
						num = 10;
						continue;
					}
					goto IL_113;
				case 2:
					if (A_2)
					{
						num = 6;
						continue;
					}
					return @string;
				case 3:
					goto IL_113;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (!A_2)
						{
							return string2;
						}
						break;
					}
					num = 8;
					continue;
				case 6:
					A_0.ReadByte();
					num = 9;
					continue;
				case 7:
					goto IL_79;
				case 8:
					A_0.ReadUInt16();
					num = 7;
					continue;
				case 9:
					return @string;
				case 10:
					num2 = 0;
					num = 3;
					continue;
				}
				if (A_1)
				{
					num = 0;
					continue;
				}
				int count = (int)A_0.ReadByte();
				byte[] bytes = A_0.ReadBytes(count);
				@string = sprឱ.ᜀ(A_1).GetString(bytes);
				num = 2;
				continue;
				IL_113:
				byte[] bytes2 = A_0.ReadBytes(num2);
				string2 = sprឱ.ᜀ(A_1).GetString(bytes2);
				num = 4;
			}
			return string2;
			IL_79:
			if (true)
			{
			}
			return string2;
		}
		}
	}

	// Token: 0x060034E6 RID: 13542 RVA: 0x0030D3B4 File Offset: 0x0030C3B4
	internal static int ᜀ(string A_0, int A_1, BinaryWriter A_2, bool A_3, bool A_4)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = (int)A_2.BaseStream.Position;
				int num2 = 7;
				for (;;)
				{
					string text;
					switch (num2)
					{
					case 0:
						goto IL_8A;
					case 1:
					{
						if (A_3)
						{
							num2 = 14;
							continue;
						}
						int num3 = (int)((byte)Math.Min(A_0.Length, 255));
						A_2.Write((byte)num3);
						byte[] bytes = sprឱ.ᜀ(false).GetBytes(A_0);
						A_2.Write(bytes, 0, num3);
						num2 = 13;
						continue;
					}
					case 2:
						A_2.Write(0);
						num2 = 5;
						continue;
					case 3:
						goto IL_8F;
					case 4:
						text = A_0;
						goto IL_197;
					case 5:
						goto IL_17A;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8A;
						default:
							if (false)
							{
							}
							if (A_0.Length > A_1)
							{
								num2 = 8;
								continue;
							}
							goto IL_8F;
						}
						break;
					case 7:
						if (A_0 == null)
						{
							num2 = 11;
							continue;
						}
						num2 = 4;
						continue;
					case 8:
						A_0 = A_0.Substring(0, A_1);
						num2 = 3;
						continue;
					case 9:
						A_2.Write(0);
						num2 = 0;
						continue;
					case 10:
						if (A_4)
						{
							num2 = 9;
							continue;
						}
						goto IL_1ED;
					case 11:
						num2 = 12;
						continue;
					case 12:
						text = "";
						goto IL_197;
					case 13:
						if (A_4)
						{
							num2 = 2;
							continue;
						}
						goto IL_1ED;
					case 14:
					{
						A_2.Write((ushort)A_0.Length);
						byte[] bytes2 = sprឱ.ᜀ(true).GetBytes(A_0);
						A_2.Write(bytes2);
						num2 = 10;
						continue;
					}
					}
					break;
					IL_8F:
					num2 = 1;
					continue;
					IL_197:
					A_0 = text;
					if (true)
					{
					}
					num2 = 6;
				}
			}
			IL_8A:
			IL_17A:
			IL_1ED:
			return (int)A_2.BaseStream.Position - num;
		}
		}
	}

	// Token: 0x060034E7 RID: 13543 RVA: 0x0030D5BC File Offset: 0x0030C5BC
	internal static string ᜄ(BinaryReader A_0, int A_1)
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
		int num = (int)A_0.BaseStream.Position;
		int num2 = (int)(A_0.ReadUInt16() * 2);
		num2 = Math.Min(num2, A_1 - 2);
		byte[] bytes = A_0.ReadBytes(num2);
		string @string = sprឱ.ᜀ.GetString(bytes);
		A_0.BaseStream.Position = (long)(num + A_1);
		return @string;
	}

	// Token: 0x060034E8 RID: 13544 RVA: 0x0030D63C File Offset: 0x0030C63C
	internal static void ᜀ(string A_0, BinaryWriter A_1, int A_2)
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
		int val = A_2 / 2 - 1;
		int num = Math.Min(A_0.Length, val);
		A_1.Write((ushort)num);
		byte[] bytes = sprឱ.ᜀ.GetBytes(A_0);
		int num2 = num * 2;
		A_1.Write(bytes, 0, num2);
		int num3 = A_2 - num2 - 2;
		A_1.Write(new byte[num3]);
	}

	// Token: 0x060034E9 RID: 13545 RVA: 0x0030D6C0 File Offset: 0x0030C6C0
	internal static string ᜃ(BinaryReader A_0, int A_1)
	{
		if (A_1 != 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				byte[] bytes = A_0.ReadBytes(A_1);
				return sprឱ.ᜀ.GetString(bytes, 0, A_1 - 2);
			}
			}
		}
		if (true)
		{
		}
		return string.Empty;
	}

	// Token: 0x060034EA RID: 13546 RVA: 0x0030D720 File Offset: 0x0030C720
	internal static string ᜂ(BinaryReader A_0)
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
		int a_ = A_0.ReadInt32();
		return sprឱ.ᜃ(A_0, a_);
	}

	// Token: 0x060034EB RID: 13547 RVA: 0x0030D76C File Offset: 0x0030C76C
	internal static string ᜁ(BinaryReader A_0)
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
		int a_ = A_0.ReadInt32() * 2;
		return sprឱ.ᜃ(A_0, a_);
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x0030D7B8 File Offset: 0x0030C7B8
	internal static int ᜂ(string A_0, BinaryWriter A_1)
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
		byte[] array = new byte[sprឱ.ᜁ(A_0)];
		sprឱ.ᜀ.GetBytes(A_0, 0, A_0.Length, array, 0);
		A_1.Write(array);
		return array.Length;
	}

	// Token: 0x060034ED RID: 13549 RVA: 0x0030D820 File Offset: 0x0030C820
	internal static void ᜁ(string A_0, BinaryWriter A_1)
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
		A_1.Write(sprឱ.ᜀ(A_0));
		sprឱ.ᜂ(A_0, A_1);
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x0030D870 File Offset: 0x0030C870
	internal static void ᜀ(string A_0, BinaryWriter A_1)
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
		A_1.Write(sprឱ.ᜁ(A_0));
		sprឱ.ᜂ(A_0, A_1);
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x0030D8C0 File Offset: 0x0030C8C0
	internal static int ᜁ(string A_0)
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
		return sprឱ.ᜀ(A_0) * 2;
	}

	// Token: 0x060034F0 RID: 13552 RVA: 0x0030D904 File Offset: 0x0030C904
	internal static int ᜀ(string A_0)
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
		return A_0.Length + 1;
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x0030D948 File Offset: 0x0030C948
	internal static string ᜂ(BinaryReader A_0, int A_1)
	{
		if (A_1 != 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				byte[] bytes = A_0.ReadBytes(A_1);
				return sprឱ.ᜁ.GetString(bytes, 0, A_1 - 1);
			}
			}
		}
		if (true)
		{
		}
		return string.Empty;
	}

	// Token: 0x060034F2 RID: 13554 RVA: 0x0030D9A8 File Offset: 0x0030C9A8
	internal static string ᜀ(BinaryReader A_0)
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
		int a_ = A_0.ReadInt32();
		return sprឱ.ᜂ(A_0, a_);
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x0030D9F4 File Offset: 0x0030C9F4
	internal static char[] ᜀ(BinaryReader A_0, bool A_1, int A_2)
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
		byte[] bytes = A_0.ReadBytes(A_1 ? (A_2 * 2) : A_2);
		return sprឱ.ᜀ(A_1).GetChars(bytes);
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x0030DA50 File Offset: 0x0030CA50
	internal static string ᜁ(BinaryReader A_0, int A_1, int A_2)
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
		byte[] bytes = A_0.ReadBytes(A_1 - 1);
		A_0.ReadByte();
		return Encoding.GetEncoding(A_2).GetString(bytes);
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x0030DAA8 File Offset: 0x0030CAA8
	internal static string ᜁ(BinaryReader A_0, int A_1)
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
		return sprឱ.ᜁ(A_0, (int)A_0.ReadInt16(), A_1);
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x0030DAF0 File Offset: 0x0030CAF0
	internal static string ᜀ(BinaryReader A_0, int A_1)
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
		int a_ = A_0.ReadInt32();
		return sprឱ.ᜀ(A_0, a_, A_1);
	}

	// Token: 0x060034F7 RID: 13559 RVA: 0x0030DB3C File Offset: 0x0030CB3C
	internal static string ᜀ(BinaryReader A_0, int A_1, int A_2)
	{
		if (A_1 != 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				byte[] bytes = A_0.ReadBytes(A_1 - 1);
				A_0.ReadByte();
				return Encoding.GetEncoding(A_2).GetString(bytes);
			}
			}
		}
		if (true)
		{
		}
		return string.Empty;
	}

	// Token: 0x060034F8 RID: 13560 RVA: 0x0030DBA0 File Offset: 0x0030CBA0
	internal static void ᜀ(BinaryWriter A_0, string A_1, int A_2, bool A_3)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				A_0.Write((short)(A_1.Length + 1));
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1F;
				default:
					goto IL_68;
				}
				break;
			}
			goto IL_1C;
			IL_1F:
			num = 1;
			continue;
			IL_1C:
			if (A_3)
			{
				goto IL_1F;
			}
			goto IL_70;
		}
		IL_68:
		if (false)
		{
		}
		IL_70:
		A_0.Write(Encoding.GetEncoding(A_2).GetBytes(A_1));
		A_0.Write(0);
	}

	// Token: 0x060034F9 RID: 13561 RVA: 0x0030DC38 File Offset: 0x0030CC38
	internal static Language ᜀ(int A_0)
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
		return (Language)spr\u19FA.ᜀ(sprឱ.ᜂ, A_0, Language.LanguageNotSet);
	}

	// Token: 0x060034FA RID: 13562 RVA: 0x0030DC90 File Offset: 0x0030CC90
	internal static int ᜀ(Language A_0)
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
		return (int)spr\u19FA.ᜀ(sprឱ.ᜃ, A_0, 0);
	}

	// Token: 0x060034FB RID: 13563 RVA: 0x0030DCE8 File Offset: 0x0030CCE8
	static sprឱ()
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
		sprឱ.ᜀ = Encoding.Unicode;
		sprឱ.ᜁ = Encoding.GetEncoding(1252);
		sprឱ.ᜂ = new Hashtable();
		sprឱ.ᜃ = new Hashtable();
		spr\u19FA.ᜁ(new object[]
		{
			0,
			Language.LanguageNotSet,
			1,
			Language.Japanese,
			2,
			Language.ChineseSingapore,
			3,
			Language.Korean,
			4,
			Language.ChineseTaiwan
		}, sprឱ.ᜂ, sprឱ.ᜃ);
	}

	// Token: 0x04002879 RID: 10361
	private static readonly Encoding ᜀ;

	// Token: 0x0400287A RID: 10362
	private static readonly Encoding ᜁ;

	// Token: 0x0400287B RID: 10363
	private static readonly Hashtable ᜂ;

	// Token: 0x0400287C RID: 10364
	private static readonly Hashtable ᜃ;
}
