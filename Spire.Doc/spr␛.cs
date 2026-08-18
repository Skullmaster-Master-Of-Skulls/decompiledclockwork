using System;
using System.Globalization;
using Spire.CompoundFile.Doc;

// Token: 0x020001F7 RID: 503
internal class spr\u241B : spr\u19E7
{
	// Token: 0x06001615 RID: 5653 RVA: 0x00165240 File Offset: 0x00164240
	internal override int ᜀ(byte[] A_0, int A_1, int A_2, char[] A_3, int A_4)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num2;
			uint num4;
			for (;;)
			{
				A_2 += A_1;
				int num = A_1;
				num2 = A_4;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_175;
					case 1:
						if (num + 3 >= A_2)
						{
							num3 = 4;
							continue;
						}
						num4 = (uint)((int)A_0[num + 3] << 24 | (int)A_0[num + 2] << 16 | (int)A_0[num + 1] << 8 | (int)A_0[num]);
						num3 = 11;
						continue;
					case 2:
						num3 = 3;
						continue;
					case 3:
						if (num4 <= 57343U)
						{
							if (true)
							{
							}
							num3 = 10;
							continue;
						}
						goto IL_70;
					case 4:
						goto IL_196;
					case 5:
						goto IL_19B;
					case 6:
						goto IL_DA;
					case 7:
						goto IL_DA;
					case 8:
						if (num4 >= 55296U)
						{
							num3 = 2;
							continue;
						}
						goto IL_70;
					case 9:
						goto IL_175;
					case 10:
						goto IL_A6;
					case 11:
						if (num4 > 1114111U)
						{
							num3 = 12;
							continue;
						}
						num3 = 13;
						continue;
					case 12:
						goto IL_13E;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_19B;
						default:
							if (false)
							{
							}
							if (num4 > 65535U)
							{
								num3 = 5;
								continue;
							}
							num3 = 8;
							continue;
						}
						break;
					}
					break;
					IL_70:
					A_3[num2] = (char)num4;
					num3 = 6;
					continue;
					IL_DA:
					num2++;
					num += 4;
					num3 = 9;
					continue;
					IL_175:
					num3 = 1;
					continue;
					IL_19B:
					A_3[num2] = spr\u19E7.ᜀ(num4);
					num2++;
					num3 = 7;
				}
			}
			IL_A6:
			throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("⍩ɫᡭᅯṱᵳት塷᥹ᑻώ겋뺍꒓겕벛캟芡솣좥쮧얩좫잭\udeaf햱", a_), new object[]
			{
				num4
			}));
			IL_13E:
			throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("⍩ɫᡭᅯṱᵳት塷᥹ᑻώ겋뺍꒓겕벛캟芡솣좥쮧얩좫잭\udeaf햱", a_), new object[]
			{
				num4
			}));
			IL_196:
			return num2 - A_4;
		}
		}
	}
}
