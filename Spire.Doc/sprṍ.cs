using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;
using Spire.Pdf.General.Paper.Drawing.Ps;

// Token: 0x0200039C RID: 924
internal class sprṍ
{
	// Token: 0x0600341C RID: 13340 RVA: 0x002FF2C8 File Offset: 0x002FE2C8
	internal static string ᜅ(int A_0)
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
		return sprṍ.ᜁ(spr\u23C4.ᜋ(A_0));
	}

	// Token: 0x0600341D RID: 13341 RVA: 0x002FF310 File Offset: 0x002FE310
	internal static string ᜄ(int A_0)
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
		return sprṍ.ᜀ(spr\u23C4.ᜇ(A_0));
	}

	// Token: 0x0600341E RID: 13342 RVA: 0x002FF358 File Offset: 0x002FE358
	internal static string ᜀ(spr᮸ A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return A_0.ᜁ(false).Replace(ClipboardData.b("䵵塷", a_), ClipboardData.b("䵵", a_));
	}

	// Token: 0x0600341F RID: 13343 RVA: 0x002FF3C4 File Offset: 0x002FE3C4
	internal static string ᜁ(sprᩍ A_0)
	{
		int a_ = 16;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("⥵w䩹䱻乽끿\udd81ﾃ뚅붋", a_), A_0.វ() ? ClipboardData.b("ή", a_) : ClipboardData.b("յ", a_), sprᜌ.ᜉ(A_0.\u175A()));
	}

	// Token: 0x06003420 RID: 13344 RVA: 0x002FF450 File Offset: 0x002FE450
	internal static string ᜀ(sprᩍ A_0)
	{
		while (!spr\u1CC6.ᜋ(A_0.អ()))
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
				return sprṍ.ᜁ(A_0);
			}
		}
		return A_0.អ();
	}

	// Token: 0x06003421 RID: 13345 RVA: 0x002FF4A8 File Offset: 0x002FE4A8
	internal static string ᜀ(Hashtable A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder();
			int num = 129;
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_BB;
				case 1:
					goto IL_3D;
				case 2:
					goto IL_F5;
				case 3:
					stringBuilder.Append(sprṍ.ᜁ(spr\u23C4.ᜋ((int)A_0[num])));
					num2 = 1;
					continue;
				case 4:
					if (A_0[num] != null)
					{
						num2 = 3;
						continue;
					}
					goto IL_3D;
				case 5:
					if (num > 132)
					{
						num2 = 2;
						continue;
					}
					num2 = 4;
					continue;
				case 6:
					goto IL_BB;
				}
				break;
				IL_3D:
				stringBuilder.Append(',');
				num++;
				if (true)
				{
				}
				num2 = 0;
				continue;
				IL_BB:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num2 = 5;
					break;
				}
			}
		}
		IL_F5:
		return stringBuilder.ToString().TrimEnd(new char[]
		{
			','
		});
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x002FF5C4 File Offset: 0x002FE5C4
	internal static string ᜁ(object A_0)
	{
		while (A_0 == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return null;
			}
		}
		int a_ = (int)A_0;
		return sprṍ.ᜃ(a_);
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x002FF614 File Offset: 0x002FE614
	internal static string ᜃ(int A_0)
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8F;
			case 1:
				if (A_0 % 16384 != 0)
				{
					goto IL_A0;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				goto IL_34;
			}
			if (A_0 == 0)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_34:
		return ClipboardData.b("䍲", a_);
		IL_8F:
		return sprᜌ.ᜁ((double)A_0 / 65536.0);
		IL_A0:
		return A_0.ToString() + ClipboardData.b("ᕲ", a_);
	}

	// Token: 0x06003424 RID: 13348 RVA: 0x002FF6DC File Offset: 0x002FE6DC
	internal static string ᜁ(object A_0, bool A_1)
	{
		bool flag;
		for (;;)
		{
			flag = (bool)A_0;
			if (flag == A_1)
			{
				goto IL_38;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2B;
			}
		}
		IL_2B:
		if (false)
		{
		}
		return sprṍ.ᜀ(flag);
		IL_38:
		return null;
	}

	// Token: 0x06003425 RID: 13349 RVA: 0x002FF72C File Offset: 0x002FE72C
	internal static string ᜀ(object A_0)
	{
		while (A_0 == null)
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
				return null;
			}
		}
		bool a_ = (bool)A_0;
		return sprṍ.ᜀ(a_);
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x002FF77C File Offset: 0x002FE77C
	internal static string ᜀ(bool A_0)
	{
		int a_ = 18;
		for (;;)
		{
			if (true)
			{
			}
			if (A_0)
			{
				goto IL_41;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2C;
			}
		}
		IL_2C:
		if (false)
		{
		}
		return ClipboardData.b("ṷ", a_);
		IL_41:
		return ClipboardData.b("౷", a_);
	}

	// Token: 0x06003427 RID: 13351 RVA: 0x002FF7E4 File Offset: 0x002FE7E4
	internal static string ᜂ(int A_0)
	{
		int a_ = 13;
		double num;
		for (;;)
		{
			num = spr\u23C4.ᜀ(A_0);
			if (num != Math.Round(num))
			{
				goto IL_47;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_39;
			}
		}
		IL_39:
		if (false)
		{
		}
		return sprᜌ.\u170D((int)num);
		IL_47:
		return sprᜌ.\u170D(A_0) + ClipboardData.b("ᕲᅴ", a_);
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x002FF85C File Offset: 0x002FE85C
	internal static string ᜀ(spr\u2143[] A_0)
	{
		int a_ = 3;
		StringBuilder stringBuilder;
		for (;;)
		{
			IL_45:
			stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				IL_4D:
				int num2 = 11;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_166;
					case 1:
						if (stringBuilder.Length > 0)
						{
							num2 = 6;
							continue;
						}
						goto IL_1E7;
					case 2:
						if (num == 0)
						{
							num2 = 5;
							continue;
						}
						num2 = 4;
						continue;
					case 3:
						stringBuilder.Append(string.Format(ClipboardData.b("ቨ孪ၬ佮ੰ䉲ࡴ䱶", a_), 1, spr\u23B0.ᜁ(A_0[num].ᜀ)));
						num2 = 9;
						continue;
					case 4:
						if (num == A_0.Length - 1)
						{
							num2 = 3;
							continue;
						}
						if (true)
						{
						}
						stringBuilder.Append(string.Format(ClipboardData.b("ቨ孪ၬ佮ੰ䉲ࡴ䱶", a_), sprṍ.ᜃ(A_0[num].ᜁ), spr\u23B0.ᜁ(A_0[num].ᜀ)));
						num2 = 0;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						default:
							if (false)
							{
							}
							stringBuilder.Append(string.Format(ClipboardData.b("ቨ孪ၬ佮ੰ䉲ࡴ䱶", a_), 0, spr\u23B0.ᜁ(A_0[num].ᜀ)));
							num2 = 12;
							continue;
						}
						break;
					case 6:
						goto IL_146;
					case 7:
						if (num >= A_0.Length)
						{
							num2 = 10;
							continue;
						}
						num2 = 2;
						continue;
					case 8:
						goto IL_64;
					case 9:
						goto IL_166;
					case 10:
						num2 = 1;
						continue;
					case 11:
						goto IL_64;
					case 12:
						goto IL_166;
					}
					goto IL_45;
					IL_64:
					num2 = 7;
					continue;
					IL_166:
					num++;
					num2 = 8;
				}
			}
		}
		IL_146:
		return stringBuilder.ToString(0, stringBuilder.Length - 1);
		IL_1E7:
		return "";
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x002FFA58 File Offset: 0x002FEA58
	internal static string ᜀ(Point A_0)
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("ၪ嵬ቮ嵰ࡲ䑴੶", a_), sprᜌ.\u170D(A_0.X), sprᜌ.\u170D(A_0.Y));
	}

	// Token: 0x0600342A RID: 13354 RVA: 0x002FFAC8 File Offset: 0x002FEAC8
	internal static string ᜀ(double A_0, double A_1, bool A_2)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("๴䝶Ѹ坺ټ乾ﲀ", a_), sprṍ.ᜀ(A_0, A_2), sprṍ.ᜀ(A_1, A_2));
	}

	// Token: 0x0600342B RID: 13355 RVA: 0x002FFB30 File Offset: 0x002FEB30
	internal static string ᜀ(object A_0, bool A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_2B;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_23;
			}
		}
		IL_23:
		if (false)
		{
		}
		return null;
		IL_2B:
		double a_ = (double)A_0;
		return sprṍ.ᜀ(a_, A_1);
	}

	// Token: 0x0600342C RID: 13356 RVA: 0x002FFB80 File Offset: 0x002FEB80
	internal static string ᜀ(double A_0, bool A_1)
	{
		while (A_1)
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
				return sprṍ.ᜁ(A_0);
			}
		}
		return sprᜌ.\u170D(spr\u2109.ᜂ(A_0));
	}

	// Token: 0x0600342D RID: 13357 RVA: 0x002FFBD4 File Offset: 0x002FEBD4
	internal static string ᜁ(double A_0)
	{
		int a_ = 10;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_BA;
			case 2:
				goto IL_3D;
			case 3:
				if (A_0 % 72.0 != 0.0)
				{
					goto IL_CB;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (A_0 == 0.0)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_3D:
		if (true)
		{
		}
		return ClipboardData.b("䁯", a_);
		IL_BA:
		return sprᜌ.ᜁ(A_0 / 72.0) + ClipboardData.b("᥯ᱱ", a_);
		IL_CB:
		return sprᜌ.ᜁ(A_0) + ClipboardData.b("oٱ", a_);
	}

	// Token: 0x0600342E RID: 13358 RVA: 0x002FFCC8 File Offset: 0x002FECC8
	internal static string ᜀ(double A_0)
	{
		int a_ = 5;
		if (A_0 != 0.0)
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
				return sprᜌ.ᜀ(A_0) + ClipboardData.b("٪l", a_);
			}
		}
		return ClipboardData.b("孪", a_);
	}

	// Token: 0x0600342F RID: 13359 RVA: 0x002FFD44 File Offset: 0x002FED44
	internal static string ᜁ(int A_0)
	{
		int a_ = 8;
		if (A_0 == 0)
		{
			if (true)
			{
			}
		}
		else
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
				return sprᜌ.\u170D(A_0) + ClipboardData.b("䭭", a_);
			}
		}
		return ClipboardData.b("幭", a_);
	}

	// Token: 0x06003430 RID: 13360 RVA: 0x002FFDB8 File Offset: 0x002FEDB8
	internal static string ᜀ(spr\u2587 A_0)
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
				goto IL_81;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				if (A_0.ᜆ())
				{
					num = 1;
					continue;
				}
				goto IL_83;
			}
			if (A_0 == null)
			{
				goto IL_83;
			}
			num = 0;
		}
		IL_2D:
		Color a_ = A_0.\u1712();
		return spr\u23B0.ᜁ(a_);
		IL_81:
		goto IL_2D;
		IL_83:
		return "";
	}

	// Token: 0x06003431 RID: 13361 RVA: 0x002FFE50 File Offset: 0x002FEE50
	internal static string ᜀ(int A_0)
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
		double a_ = (double)A_0 / 16777216.0;
		return sprᜌ.ᜂ(a_);
	}

	// Token: 0x06003432 RID: 13362 RVA: 0x002FFEA0 File Offset: 0x002FEEA0
	internal static int ᜁ(string A_0)
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
		return spr\u2109.ᜂ(sprᜌ.ᜏ(A_0) * 16777216.0);
	}

	// Token: 0x06003433 RID: 13363 RVA: 0x002FFEF0 File Offset: 0x002FEEF0
	internal static int ᜀ(string A_0)
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
		byte[] bytes = Encoding.Unicode.GetBytes(A_0.Trim(new char[]
		{
			'#'
		}));
		CrcMaker crcMaker = new CrcMaker();
		return crcMaker.MakeCRC(bytes);
	}

	// Token: 0x06003434 RID: 13364 RVA: 0x002FFF58 File Offset: 0x002FEF58
	internal static byte[] ᜄ(byte[] A_0)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			BinaryWriter binaryWriter;
			for (;;)
			{
				memoryStream = new MemoryStream();
				binaryWriter = new BinaryWriter(memoryStream);
				string text = ClipboardData.b("╧㥩⍫⡭㙯㭱㝳㍵䅷呹䱻", a_);
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_16B;
					case 1:
						goto IL_129;
					case 2:
					{
						binaryWriter.Write((uint)A_0.Length);
						spr\u2481 spr_u = spr\u2075.ᜑ(A_0);
						binaryWriter.Write(spr_u.ᜌ());
						binaryWriter.Write(spr_u.ᜉ());
						binaryWriter.Write(spr_u.ᜐ());
						binaryWriter.Write(spr_u.ᜂ());
						binaryWriter.Write(spr_u.\u170D());
						binaryWriter.Write(spr_u.ᜋ());
						binaryWriter.Write(0U);
						binaryWriter.Write(65024);
						int num3 = 0;
						num2 = 1;
						continue;
					}
					case 3:
					{
						int num3;
						if (num3 >= 467)
						{
							num2 = 4;
							continue;
						}
						binaryWriter.Write(0);
						num3++;
						num2 = 5;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_127;
						default:
							goto IL_163;
						}
						break;
					case 5:
						goto IL_129;
					case 6:
						if (num >= text.Length)
						{
							num2 = 2;
							continue;
						}
						binaryWriter.Write((byte)text[num]);
						num++;
						num2 = 7;
						continue;
					case 7:
						goto IL_127;
					}
					break;
					IL_129:
					num2 = 3;
					continue;
					IL_16B:
					num2 = 6;
					continue;
					IL_127:
					goto IL_16B;
				}
			}
			IL_163:
			if (false)
			{
			}
			if (true)
			{
			}
			binaryWriter.Write(A_0);
			return sprṍ.ᜂ(memoryStream.ToArray());
		}
		}
	}

	// Token: 0x06003435 RID: 13365 RVA: 0x00300120 File Offset: 0x002FF120
	internal static byte[] ᜃ(byte[] A_0)
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
		byte[] array = sprṍ.ᜁ(A_0);
		byte[] array2 = new byte[array.Length - 512];
		Array.Copy(array, 512, array2, 0, array2.Length);
		return array2;
	}

	// Token: 0x06003436 RID: 13366 RVA: 0x00300184 File Offset: 0x002FF184
	internal static byte[] ᜂ(byte[] A_0)
	{
		byte[] array;
		for (;;)
		{
			IL_18:
			A_0 = spr\u258F.ᜀ(A_0, PsZipMethod.Deflate);
			array = new byte[A_0.Length + sprṍ.ᜁ.Length];
			A_0.CopyTo(array, sprṍ.ᜁ.Length);
			int num = 0;
			for (;;)
			{
				IL_42:
				if (true)
				{
				}
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_54;
					case 1:
						return array;
					case 2:
						goto IL_54;
					case 3:
						if (num < sprṍ.ᜁ.Length)
						{
							array[num] = sprṍ.ᜁ[num];
							num++;
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					}
					goto IL_18;
					IL_54:
					num2 = 3;
				}
			}
		}
		return array;
	}

	// Token: 0x06003437 RID: 13367 RVA: 0x00300248 File Offset: 0x002FF248
	internal static byte[] ᜁ(byte[] A_0)
	{
		if (!sprṍ.ᜀ(A_0))
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
				return A_0;
			}
		}
		MemoryStream a_ = new MemoryStream(A_0, 10, A_0.Length - 10);
		return spr\u258F.ᜀ(a_, 0, PsZipMethod.Deflate);
	}

	// Token: 0x06003438 RID: 13368 RVA: 0x003002A8 File Offset: 0x002FF2A8
	internal static string ᜀ(spr\u2055[] A_0, char A_1, char A_2)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			IL_18:
			stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				IL_20:
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_5C;
					case 1:
						goto IL_2A;
					case 2:
						goto IL_2A;
					case 3:
						if (num < A_0.Length)
						{
							spr\u2055 spr_u = A_0[num];
							stringBuilder.Append(sprṍ.ᜀ(spr_u.ᜂ()));
							stringBuilder.Append(A_1);
							stringBuilder.Append(sprṍ.ᜀ(spr_u.ᜁ()));
							stringBuilder.Append(A_2);
							num++;
							if (true)
							{
							}
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					goto IL_18;
					IL_2A:
					num2 = 3;
				}
			}
		}
		IL_5C:
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		return stringBuilder.ToString();
	}

	// Token: 0x06003439 RID: 13369 RVA: 0x00300388 File Offset: 0x002FF388
	internal static string ᜀ(sprṚ A_0)
	{
		int a_ = 18;
		if (A_0.ᜁ())
		{
			if (true)
			{
			}
		}
		else
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
				return sprᜌ.\u170D(A_0.ᜂ());
			}
		}
		return ClipboardData.b("㡷", a_) + sprᜌ.\u170D(A_0.ᜂ());
	}

	// Token: 0x0600343A RID: 13370 RVA: 0x00300400 File Offset: 0x002FF400
	private static bool ᜀ(byte[] A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					return true;
				case 1:
					if (num >= 4)
					{
						num2 = 0;
						continue;
					}
					num2 = 4;
					continue;
				case 2:
					goto IL_7E;
				case 3:
					goto IL_7E;
				case 4:
					if (A_0[num] == sprṍ.ᜁ[num])
					{
						num++;
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
						if (false)
						{
						}
						num2 = 5;
						continue;
					}
					break;
				case 5:
					goto IL_7C;
				}
				break;
				IL_7E:
				num2 = 1;
			}
		}
		return false;
		IL_7C:
		return false;
	}

	// Token: 0x0600343C RID: 13372 RVA: 0x003004C8 File Offset: 0x002FF4C8
	// Note: this type is marked as 'beforefieldinit'.
	static sprṍ()
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
		sprṍ.ᜁ = new byte[]
		{
			31,
			139,
			8,
			0,
			0,
			0,
			0,
			0,
			2,
			11
		};
	}

	// Token: 0x04002841 RID: 10305
	private const double ᜀ = 16777216.0;

	// Token: 0x04002842 RID: 10306
	private static readonly byte[] ᜁ;
}
