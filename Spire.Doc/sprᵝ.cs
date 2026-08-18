using System;
using System.Drawing;

// Token: 0x02000380 RID: 896
internal class spr\u1D5D
{
	// Token: 0x06003225 RID: 12837 RVA: 0x002E317C File Offset: 0x002E217C
	internal static spr\u1B70 ᜀ(PointF A_0, PointF A_1, float A_2, spr\u2262 A_3)
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
		return spr\u1D5D.ᜀ(A_0, A_1, new spr\u23F1(A_3, A_2));
	}

	// Token: 0x06003226 RID: 12838 RVA: 0x002E31C8 File Offset: 0x002E21C8
	internal static spr\u1B70 ᜀ(PointF A_0, PointF A_1, spr\u23F1 A_2)
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
		spr\u1B70 spr_u1B = spr\u1B70.ᜀ(A_0, A_1);
		spr_u1B.ᜀ(A_2);
		return spr_u1B;
	}

	// Token: 0x06003227 RID: 12839 RVA: 0x002E3214 File Offset: 0x002E2214
	internal static spr\u1B70 ᜂ(PointF A_0, PointF A_1, float A_2, spr\u2262 A_3, bool A_4)
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
		spr\u1B70 spr_u1B = new spr\u1B70(new spr\u23F1(A_3));
		float num = spr\u1D5D.ᜀ(A_0, A_1, A_4);
		spr_u1B.ᜁ(spr\u1D5D.ᜀ(A_0, num, A_2, A_4));
		spr\u1D5D.ᜀ(spr_u1B, A_0, num, A_2 + 1.5f, A_4);
		return spr_u1B;
	}

	// Token: 0x06003228 RID: 12840 RVA: 0x002E3288 File Offset: 0x002E2288
	internal static spr\u1B70 ᜁ(PointF A_0, PointF A_1, float A_2, spr\u2262 A_3, bool A_4)
	{
		switch (0)
		{
		default:
		{
			spr\u1B70 spr_u1B;
			float num;
			PointF a_;
			PointF a_2;
			for (;;)
			{
				spr_u1B = new spr\u1B70(new spr\u23F1(A_3));
				num = spr\u1D5D.ᜀ(A_0, A_1, A_4);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						a_ = new PointF(A_0.X, A_0.Y - A_2 * 0.25f);
						a_2 = new PointF(A_0.X, A_0.Y + A_2 * 0.25f);
						if (true)
						{
						}
						num2 = 1;
						continue;
					case 1:
						goto IL_115;
					case 2:
						if (A_4)
						{
							num2 = 0;
							continue;
						}
						a_ = new PointF(A_0.X - A_2 * 0.25f, A_0.Y);
						a_2 = new PointF(A_0.X + A_2 * 0.25f, A_0.Y);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 3:
						goto IL_C3;
					}
					break;
				}
			}
			IL_C3:
			IL_115:
			spr_u1B.ᜁ(spr\u1D5D.ᜀ(a_, num, A_2 * 0.5f, A_4));
			spr_u1B.ᜁ(spr\u1D5D.ᜀ(a_2, num, A_2 * 0.5f, A_4));
			spr\u1D5D.ᜀ(spr_u1B, A_0, num, A_2 + 1.5f, A_4);
			return spr_u1B;
		}
		}
	}

	// Token: 0x06003229 RID: 12841 RVA: 0x002E33EC File Offset: 0x002E23EC
	private static spr\u1926 ᜀ(PointF A_0, float A_1, float A_2, bool A_3)
	{
		switch (0)
		{
		default:
		{
			spr\u1926 spr_u;
			float[] array;
			for (;;)
			{
				spr_u = new spr\u1926();
				int num = (int)(A_1 / A_2) + 2;
				array = new float[num * 2];
				array[0] = A_0.X;
				array[1] = A_0.Y;
				int num2;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_191:
					if (spr\u1CC6.ᜀ((long)num2))
					{
						num3 = 7;
					}
					else
					{
						array[num2 * 2 + 3] = A_0.Y - A_2 / 2f;
						num3 = 0;
					}
					break;
				default:
					if (false)
					{
					}
					num3 = 2;
					break;
				}
				for (;;)
				{
					int num4;
					switch (num3)
					{
					case 0:
						goto IL_2AD;
					case 1:
						goto IL_2AD;
					case 2:
						if (A_3)
						{
							num3 = 3;
							continue;
						}
						num4 = 0;
						num3 = 6;
						continue;
					case 3:
						num2 = 0;
						num3 = 10;
						continue;
					case 4:
						goto IL_191;
					case 5:
						goto IL_100;
					case 6:
						goto IL_1E3;
					case 7:
						array[num2 * 2 + 3] = A_0.Y + A_2 / 2f;
						num3 = 1;
						continue;
					case 8:
						goto IL_12E;
					case 9:
						goto IL_207;
					case 10:
						goto IL_207;
					case 11:
						goto IL_12E;
					case 12:
						if (spr\u1CC6.ᜀ((long)num4))
						{
							num3 = 18;
							continue;
						}
						array[num4 * 2 + 2] = A_0.X - A_2 / 2f;
						num3 = 8;
						continue;
					case 13:
						if (num4 >= num - 1)
						{
							num3 = 16;
							continue;
						}
						num3 = 12;
						continue;
					case 14:
						if (num2 >= num - 1)
						{
							if (true)
							{
							}
							num3 = 17;
							continue;
						}
						array[num2 * 2 + 2] = A_0.X + (float)num2 * A_2 + A_2 / 2f;
						num3 = 4;
						continue;
					case 15:
						goto IL_1E3;
					case 16:
						goto IL_202;
					case 17:
						num3 = 5;
						continue;
					case 18:
						array[num4 * 2 + 2] = A_0.X + A_2 / 2f;
						num3 = 11;
						continue;
					}
					break;
					IL_12E:
					array[num4 * 2 + 3] = A_0.Y + (float)num4 * A_2 + A_2 / 2f;
					num4++;
					num3 = 15;
					continue;
					IL_1E3:
					num3 = 13;
					continue;
					IL_207:
					num3 = 14;
					continue;
					IL_2AD:
					num2++;
					num3 = 9;
				}
			}
			IL_100:
			IL_202:
			sprᴎ a_ = new sprᴎ(array);
			spr_u.ᜁ(a_);
			return spr_u;
		}
		}
	}

	// Token: 0x0600322A RID: 12842 RVA: 0x002E36D0 File Offset: 0x002E26D0
	internal static spr\u1B70 ᜀ(PointF A_0, PointF A_1, float A_2, spr\u2262 A_3, bool A_4)
	{
		switch (0)
		{
		default:
		{
			spr\u1B70 spr_u1B;
			spr\u1926 spr_u;
			float num11;
			float[] array;
			for (;;)
			{
				spr_u1B = new spr\u1B70(new spr\u23F1(A_3));
				float num = A_2 + 3f;
				int num2 = 28;
				for (;;)
				{
					PointF pointF;
					int num3;
					float num4;
					int num5;
					float num6;
					int num7;
					float num8;
					float num9;
					int num10;
					switch (num2)
					{
					case 0:
						pointF = new PointF(A_0.X - num, A_0.Y + num / 2f);
						num2 = 31;
						continue;
					case 1:
						if ((num3 & 1) == 1)
						{
							num2 = 27;
							continue;
						}
						goto IL_380;
					case 2:
						num4 += spr\u1D5D.ᜀ(ref num5);
						num6 = num4 + num;
						num2 = 33;
						continue;
					case 3:
						if ((num3 & 2) == 2)
						{
							num2 = 11;
							continue;
						}
						num2 = 29;
						continue;
					case 4:
						goto IL_239;
					case 5:
						if ((num7 & 2) == 2)
						{
							num2 = 8;
							continue;
						}
						num2 = 25;
						continue;
					case 6:
						goto IL_222;
					case 7:
						goto IL_288;
					case 8:
						num2 = 21;
						continue;
					case 9:
						num8 += spr\u1D5D.ᜀ(ref num5);
						num9 = num8 + num;
						num2 = 7;
						continue;
					case 10:
						if (num3 >= num10 - 1)
						{
							num2 = 13;
							continue;
						}
						num2 = 3;
						continue;
					case 11:
						num2 = 1;
						continue;
					case 12:
						goto IL_239;
					case 13:
						num2 = 30;
						continue;
					case 14:
						goto IL_4B3;
					case 15:
						goto IL_2BD;
					case 16:
						goto IL_4B3;
					case 17:
						goto IL_160;
					case 18:
						goto IL_222;
					case 19:
						num4 = pointF.X;
						num6 = num4 + num;
						num3 = 0;
						num2 = 4;
						continue;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4FF;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_4CA;
						}
						break;
					case 21:
						if ((num7 & 1) == 1)
						{
							num2 = 26;
							continue;
						}
						goto IL_4CA;
					case 22:
						goto IL_2BD;
					case 23:
						if (A_4)
						{
							num2 = 19;
							continue;
						}
						num8 = pointF.Y;
						num9 = num8 + num;
						num7 = 0;
						num2 = 15;
						continue;
					case 24:
						goto IL_380;
					case 25:
						if ((num7 & 1) == 1)
						{
							num2 = 9;
							continue;
						}
						goto IL_288;
					case 26:
						num9 += spr\u1D5D.ᜀ(ref num5);
						num8 = num9 - num;
						num2 = 20;
						continue;
					case 27:
						num6 += spr\u1D5D.ᜀ(ref num5);
						num4 = num6 - num;
						num2 = 24;
						continue;
					case 28:
						if (A_4)
						{
							num2 = 0;
							continue;
						}
						pointF = new PointF(A_0.X + num / 2f, A_0.Y - num);
						num2 = 17;
						continue;
					case 29:
						if ((num3 & 1) == 1)
						{
							num2 = 2;
							continue;
						}
						goto IL_400;
					case 30:
						goto IL_111;
					case 31:
						goto IL_160;
					case 32:
						goto IL_2E0;
					case 33:
						goto IL_400;
					case 34:
						if (num7 >= num10 - 1)
						{
							num2 = 32;
							continue;
						}
						num2 = 5;
						continue;
					}
					break;
					IL_160:
					spr_u = new spr\u1926();
					spr_u1B.ᜁ(spr_u);
					num11 = spr\u1D5D.ᜀ(A_0, A_1, A_4);
					float num12 = num11 + num * 2f;
					num10 = (int)(num12 * 2f) + 1;
					array = new float[num10 * 2];
					array[0] = pointF.X;
					array[1] = pointF.Y;
					num5 = 0;
					num2 = 23;
					continue;
					IL_222:
					num7++;
					num2 = 22;
					continue;
					IL_239:
					num2 = 10;
					continue;
					IL_288:
					array[num7 * 2 + 2] = A_0.X + num / 2f;
					array[num7 * 2 + 3] = num8;
					num2 = 18;
					continue;
					IL_2BD:
					num2 = 34;
					continue;
					IL_380:
					array[num3 * 2 + 2] = num6;
					array[num3 * 2 + 3] = A_0.Y - num / 2f;
					num2 = 14;
					continue;
					IL_400:
					array[num3 * 2 + 2] = num4;
					array[num3 * 2 + 3] = A_0.Y + num / 2f;
					num2 = 16;
					continue;
					IL_4B3:
					num3++;
					num2 = 12;
					continue;
					IL_4CA:
					array[num7 * 2 + 2] = A_0.X - num / 2f;
					array[num7 * 2 + 3] = num9;
					num2 = 6;
				}
			}
			IL_111:
			IL_2E0:
			IL_4FF:
			sprᴎ a_ = new sprᴎ(array);
			spr_u.ᜁ(a_);
			spr\u1D5D.ᜀ(spr_u1B, A_0, num11, A_2, A_4);
			return spr_u1B;
		}
		}
	}

	// Token: 0x0600322B RID: 12843 RVA: 0x002E3BFC File Offset: 0x002E2BFC
	private static float ᜀ(ref int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_7B:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				if (A_0 % 5 == 0)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			case 1:
				goto IL_75;
			case 2:
				goto IL_83;
			case 3:
				goto IL_62;
			}
			goto IL_3E;
		}
		IL_62:
		return 2.25f;
		IL_75:
		if (A_0 % 6 == 0)
		{
			goto IL_7B;
		}
		return 0.75f;
		IL_83:
		A_0 = 0;
		return 2.25f;
		IL_3E:
		if (true)
		{
		}
		A_0++;
		num = 0;
		goto IL_28;
	}

	// Token: 0x0600322C RID: 12844 RVA: 0x002E3C9C File Offset: 0x002E2C9C
	private static void ᜀ(spr\u1B70 A_0, PointF A_1, float A_2, float A_3, bool A_4)
	{
		int num = 0;
		RectangleF a_;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_B1;
			case 2:
				if (true)
				{
				}
				a_ = new RectangleF(A_1.X, A_1.Y - A_3 / 2f, A_2, A_3);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_56;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 3:
				goto IL_56;
			}
			if (A_4)
			{
				num = 2;
			}
			else
			{
				a_ = new RectangleF(A_1.X - A_3 / 2f, A_1.Y, A_3, A_2);
				num = 3;
			}
		}
		IL_56:
		IL_B1:
		A_0.ᜀ(spr\u1B70.ᜀ(a_));
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x002E3D68 File Offset: 0x002E2D68
	private static float ᜀ(PointF A_0, PointF A_1, bool A_2)
	{
		if (A_2)
		{
			for (;;)
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
					goto IL_2D;
				}
			}
			IL_2D:
			if (false)
			{
			}
			return A_1.X - A_0.X;
		}
		return A_1.Y - A_0.Y;
	}
}
