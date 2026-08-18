using System;
using System.Globalization;
using Spire.CompoundFile.Doc;

// Token: 0x020001F8 RID: 504
internal class sprḦ : spr\u19E7
{
	// Token: 0x06001617 RID: 5655 RVA: 0x00165488 File Offset: 0x00164488
	internal override int ᜀ(byte[] A_0, int A_1, int A_2, char[] A_3, int A_4)
	{
		int a_ = 15;
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
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num4 >= 55296U)
						{
							num3 = 3;
							continue;
						}
						goto IL_70;
					case 1:
						goto IL_191;
					case 2:
						goto IL_DA;
					case 3:
						num3 = 9;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D3;
						default:
							goto IL_154;
						}
						break;
					case 5:
						goto IL_1B2;
					case 6:
						goto IL_A6;
					case 7:
						goto IL_191;
					case 8:
						if (num4 > 1114111U)
						{
							num3 = 4;
							continue;
						}
						goto IL_1D3;
					case 9:
						if (num4 <= 57343U)
						{
							if (true)
							{
							}
							num3 = 6;
							continue;
						}
						goto IL_70;
					case 10:
						if (num4 > 65535U)
						{
							num3 = 12;
							continue;
						}
						num3 = 0;
						continue;
					case 11:
						goto IL_DA;
					case 12:
						A_3[num2] = spr\u19E7.ᜀ(num4);
						num2++;
						num3 = 2;
						continue;
					case 13:
						if (num + 3 >= A_2)
						{
							num3 = 5;
							continue;
						}
						num4 = (uint)((int)A_0[num] << 24 | (int)A_0[num + 1] << 16 | (int)A_0[num + 2] << 8 | (int)A_0[num + 3]);
						num3 = 8;
						continue;
					}
					break;
					IL_70:
					A_3[num2] = (char)num4;
					num3 = 11;
					continue;
					IL_DA:
					num2++;
					num += 4;
					num3 = 1;
					continue;
					IL_191:
					num3 = 13;
					continue;
					IL_1D3:
					num3 = 10;
				}
			}
			IL_A6:
			throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("㱴᥶ླྀ᩺ᅼᙾꎂ力랖ꦘ꾞鮠\udba2\ud8a4螦삨얪趬쪮\udfb0킲\udab4펶킸햺\udabc", a_), new object[]
			{
				num4
			}));
			IL_154:
			if (false)
			{
			}
			throw new spr\u1FA8(string.Format(CultureInfo.CurrentUICulture, ClipboardData.b("㱴᥶ླྀ᩺ᅼᙾꎂ力랖ꦘ꾞鮠\udba2\ud8a4螦삨얪趬쪮\udfb0킲\udab4펶킸햺\udabc", a_), new object[]
			{
				num4
			}));
			IL_1B2:
			return num2 - A_4;
		}
		}
	}
}
