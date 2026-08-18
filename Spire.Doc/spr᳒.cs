using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x0200034C RID: 844
internal class spr\u1CD2
{
	// Token: 0x06002D1C RID: 11548 RVA: 0x002B4740 File Offset: 0x002B3740
	internal static spr\u24A6 ᜀ(sprṏ A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u24A6 spr_u24A;
			spr\u23EB spr_u23EB;
			for (;;)
			{
				spr\u1F8D spr_u1F8D = A_0.ᜉ().\u173A();
				spr_u24A = new spr\u24A6();
				int num = 3;
				for (;;)
				{
					spr\u253E spr_u253E;
					string text;
					spr\u253E spr_u253E3;
					spr\u2262 spr_u;
					float num2;
					spr\u2262 spr_u2;
					float width;
					float height;
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 1:
						spr_u253E = null;
						goto IL_290;
					case 2:
					{
						if (true)
						{
						}
						spr\u23F1 spr_u23F;
						spr_u253E = (spr_u23F.ᜌ() as spr\u253E);
						goto IL_290;
					}
					case 3:
					{
						if (!spr\u1CC6.ᜋ(spr_u1F8D.ᜑ()))
						{
							goto IL_96;
						}
						text = spr\u1AEB.ᜃ(spr_u1F8D.ᜑ());
						spr\u23F1 spr_u23F = A_0.ᜇ();
						spr\u253E spr_u253E2 = A_0.ᜎ() as spr\u253E;
						num = 20;
						continue;
					}
					case 4:
						num = 15;
						continue;
					case 5:
						spr_u = spr_u253E3.ᜀ();
						goto IL_1E6;
					case 6:
						if (num2 != 0f)
						{
							num = 19;
							continue;
						}
						goto IL_385;
					case 7:
					{
						spr\u253E spr_u253E2;
						if (spr_u253E2 != null)
						{
							num = 16;
							continue;
						}
						goto IL_DD;
					}
					case 8:
						if (!spr\u2262.ᜁ(spr_u253E3.ᜀ(), null))
						{
							num = 0;
							continue;
						}
						goto IL_117;
					case 9:
						num = 8;
						continue;
					case 10:
						spr_u = spr\u2262.ទ;
						goto IL_1E6;
					case 11:
						if (spr_u253E3 != null)
						{
							num = 9;
							continue;
						}
						goto IL_117;
					case 12:
						return spr_u24A;
					case 13:
						num = 2;
						continue;
					case 14:
						goto IL_1B7;
					case 15:
					{
						spr\u253E spr_u253E2;
						spr_u2 = spr_u253E2.ᜀ();
						goto IL_26A;
					}
					case 16:
						num = 18;
						continue;
					case 17:
						spr_u2 = spr\u2262.ទ;
						goto IL_26A;
					case 18:
					{
						spr\u253E spr_u253E2;
						if (!spr\u2262.ᜁ(spr_u253E2.ᜀ(), null))
						{
							num = 4;
							continue;
						}
						goto IL_DD;
					}
					case 19:
					{
						float x = width * 0.5f;
						float y = height * 0.5f;
						spr_u23EB.ᜃ().ᜀ(num2, new PointF(x, y), MatrixOrder.Append);
						num = 14;
						continue;
					}
					case 20:
					{
						spr\u23F1 spr_u23F;
						if (spr_u23F != null)
						{
							num = 13;
							continue;
						}
						num = 1;
						continue;
					}
					}
					break;
					IL_96:
					num = 12;
					continue;
					IL_DD:
					num = 17;
					continue;
					IL_117:
					num = 10;
					continue;
					IL_1E6:
					spr\u2262 a_ = spr_u;
					sprᝊ sprᝊ;
					spr\u2262 a_2;
					PointF a_3;
					spr_u23EB = new spr\u23EB(sprᝊ, a_2, a_, a_3, text, SizeF.Empty, (float)spr_u1F8D.ᜎ());
					float a_4 = width / sprᝊ.ᜃ(text).Width;
					spr_u23EB.ᜀ(new spr\u25FD(a_4, 0f, 0f, 1f, 0f, 0f));
					num = 6;
					continue;
					IL_26A:
					a_2 = spr_u2;
					num = 11;
					continue;
					IL_290:
					spr_u253E3 = spr_u253E;
					SizeF sizeF = A_0.ᜉ().\u1753();
					width = sizeF.Width;
					height = sizeF.Height;
					num2 = (float)A_0.ᜉ().ម();
					int a_5 = spr\u1CD2.ᜀ(spr_u1F8D.ᜋ(), spr_u1F8D.ᜇ(), spr_u1F8D.\u170D());
					sprᝊ = spr\u1ABE.ᜁ().ᜀ(spr_u1F8D.ᜈ(), height, (FontStyle)a_5, spr_u1F8D.ᜈ());
					a_3 = new PointF(0f, sprᝊ.ᜐ() - (sprᝊ.ᜎ() - sprᝊ.ᜃ()));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						num = 7;
						break;
					}
				}
			}
			return spr_u24A;
			IL_1B7:
			IL_385:
			spr_u24A.ᜁ(spr_u23EB);
			return spr_u24A;
		}
		}
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x002B4ADC File Offset: 0x002B3ADC
	private static int ᜀ(bool A_0, bool A_1, bool A_2)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 8;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_97;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						if (false)
						{
						}
						if (A_1)
						{
							num2 = 6;
							continue;
						}
						goto IL_43;
					}
					break;
				case 2:
					goto IL_43;
				case 3:
					if (true)
					{
					}
					if (A_2)
					{
						num2 = 5;
						continue;
					}
					return num;
				case 4:
					num |= 1;
					num2 = 0;
					continue;
				case 5:
					goto IL_5E;
				case 6:
					num |= 2;
					num2 = 2;
					continue;
				case 7:
					return num;
				case 8:
					if (A_0)
					{
						num2 = 4;
						continue;
					}
					goto IL_97;
				}
				break;
				IL_43:
				num2 = 3;
				continue;
				IL_5E:
				num |= 4;
				num2 = 7;
				continue;
				IL_97:
				num2 = 1;
			}
		}
		return num;
	}
}
