using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x02000290 RID: 656
internal class spr\u255A
{
	// Token: 0x060022D4 RID: 8916 RVA: 0x00239C6C File Offset: 0x00238C6C
	internal static spr\u24A6 ᜀ(sprṏ A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u23F1 a_;
			sprᤕ a_2;
			SizeF a_3;
			GraphicsPath graphicsPath;
			spr\u1B70 a_5;
			for (;;)
			{
				IL_53:
				spr\u1F8D spr_u1F8D = A_0.ᜉ().\u173A();
				spr\u24A6 spr_u24A = A_0.ᜏ();
				a_ = A_0.ᜇ();
				a_2 = A_0.ᜎ();
				a_3 = A_0.ᜉ().\u1753();
				float height = a_3.Height;
				SizeF a_4;
				graphicsPath = spr\u255A.ᜀ(spr_u1F8D, height, out a_4);
				if (true)
				{
				}
				int num = 5;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_166;
					default:
					{
						if (false)
						{
						}
						spr\u1B70 spr_u1B;
						switch (num)
						{
						case 0:
							try
							{
								a_5 = spr\u255A.ᜀ(graphicsPath, (spr\u1926)spr_u1B.ᜀ(0), (spr\u1926)spr_u1B.ᜀ(1), spr_u1F8D.ᜌ(), height, a_4);
								return spr\u255A.ᜀ(a_5, a_, a_2);
							}
							catch (spr\u1D9C)
							{
								goto IL_166;
							}
							goto IL_111;
						case 1:
							if (spr_u1B.ᜉ() == 2)
							{
								num = 0;
								continue;
							}
							goto IL_166;
						case 2:
							if (spr_u1F8D.ᜆ())
							{
								num = 6;
								continue;
							}
							goto IL_166;
						case 3:
							if (spr_u24A.ᜉ() == 1)
							{
								num = 4;
								continue;
							}
							goto IL_166;
						case 4:
							num = 2;
							continue;
						case 5:
							if (graphicsPath.PointCount == 0)
							{
								num = 7;
								continue;
							}
							num = 3;
							continue;
						case 6:
							goto IL_111;
						case 7:
							goto IL_CC;
						}
						goto IL_53;
						IL_111:
						spr_u1B = (spr\u1B70)spr_u24A.ᜀ(0);
						num = 1;
						break;
					}
					}
				}
			}
			IL_CC:
			return null;
			IL_166:
			a_5 = spr\u255A.ᜀ(A_0, graphicsPath, a_3);
			return spr\u255A.ᜀ(a_5, a_, a_2);
		}
		}
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x00239E30 File Offset: 0x00238E30
	private static spr\u1B70 ᜀ(sprṏ A_0, GraphicsPath A_1, SizeF A_2)
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
		spr\u1926 spr_u = new spr\u1926();
		spr_u.ᜁ(new sprᴎ(new PointF[]
		{
			PointF.Empty,
			new PointF(A_2.Width, 0f)
		}));
		spr\u1926 spr_u2 = new spr\u1926();
		spr_u2.ᜁ(new sprᴎ(new PointF[]
		{
			new PointF(0f, A_2.Height),
			new PointF(A_2.Width, A_2.Height)
		}));
		return spr\u255A.ᜀ(A_1, spr_u, spr_u2, true, A_2.Height, SizeF.Empty);
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x00239F20 File Offset: 0x00238F20
	private static spr\u24A6 ᜀ(spr\u1B70 A_0, spr\u23F1 A_1, sprᤕ A_2)
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
		spr\u24A6 spr_u24A = new spr\u24A6();
		A_0.ᜀ(A_1);
		A_0.ᜀ(A_2);
		spr_u24A.ᜁ(A_0);
		return spr_u24A;
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x00239F78 File Offset: 0x00238F78
	private static GraphicsPath ᜀ(spr\u1F8D A_0, float A_1, out SizeF A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				Font font;
				int num2;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					try
					{
						num2 = (int)font.Style;
						StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
						stringFormat.LineAlignment = StringAlignment.Far;
						GraphicsPath graphicsPath = new GraphicsPath();
						string text = spr\u1AEB.ᜃ(A_0.ᜑ());
						graphicsPath.AddString(text, font.FontFamily, num2, A_1, new PointF(0f, A_1), stringFormat);
						A_2 = spr\u255A.ᜀ(text, font, stringFormat);
						return graphicsPath;
					}
					finally
					{
						for (;;)
						{
							IL_A4:
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_FB;
								case 1:
									((IDisposable)font).Dispose();
									num = 0;
									continue;
								}
								if (font == null)
								{
									goto IL_FD;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A4;
								default:
									if (false)
									{
									}
									num = 1;
									break;
								}
							}
						}
						IL_FB:
						IL_FD:;
					}
					goto IL_FE;
				case 1:
					goto IL_40;
				}
				if (A_1 <= 0f)
				{
					num = 1;
					continue;
				}
				IL_FE:
				num2 = spr\u255A.ᜀ(A_0.ᜋ(), A_0.ᜇ(), A_0.\u1713(), A_0.\u170D());
				font = spr\u1CC9.ᜀ(A_0.ᜈ(), A_1, (FontStyle)num2);
				num = 0;
			}
			IL_40:
			A_2 = new SizeF(0f, 0f);
			return new GraphicsPath();
		}
		}
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x0023A100 File Offset: 0x00239100
	private static int ᜀ(bool A_0, bool A_1, bool A_2, bool A_3)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_46;
				case 1:
					if (A_0)
					{
						num2 = 8;
						continue;
					}
					goto IL_BC;
				case 2:
					if (A_3)
					{
						num2 = 6;
						continue;
					}
					return num;
				case 3:
					goto IL_A1;
				case 4:
					num |= 2;
					num2 = 0;
					continue;
				case 5:
					return num;
				case 6:
					num |= 4;
					num2 = 5;
					continue;
				case 7:
					if (A_1)
					{
						num2 = 4;
						continue;
					}
					goto IL_46;
				case 8:
					num |= 1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				}
				break;
				IL_46:
				num2 = 2;
				continue;
				IL_BC:
				num2 = 7;
				continue;
				IL_A1:
				if (true)
				{
				}
				goto IL_BC;
			}
		}
		return num;
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x0023A1E8 File Offset: 0x002391E8
	private static spr\u1B70 ᜀ(GraphicsPath A_0)
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
		return new spr\u1B70();
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x0023A228 File Offset: 0x00239228
	private static SizeF ᜀ(string A_0, Font A_1, StringFormat A_2)
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
		Bitmap bitmap = new Bitmap(1, 1);
		Graphics graphics = spr\u205F.ᜀ(bitmap);
		PointF empty = PointF.Empty;
		SizeF sizeF = graphics.MeasureString(A_0, A_1, empty, A_2);
		float width = (float)spr\u23C4.\u1717((double)(sizeF.Width / graphics.DpiX));
		float height = (float)spr\u23C4.\u1717((double)(sizeF.Height / graphics.DpiY));
		graphics.Dispose();
		bitmap.Dispose();
		return new SizeF(width, height);
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x0023A2C8 File Offset: 0x002392C8
	private static spr\u1B70 ᜀ(GraphicsPath A_0, spr\u1926 A_1, spr\u1926 A_2, bool A_3, float A_4, SizeF A_5)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			PointF[] pathPoints;
			for (;;)
			{
				int num2;
				float num4;
				float num6;
				float width;
				float num7;
				float num9;
				float num8;
				switch (num)
				{
				case 0:
					goto IL_183;
				case 1:
					goto IL_B8;
				case 3:
				{
					if (num2 >= pathPoints.Length)
					{
						num = 12;
						continue;
					}
					PointF pointF = pathPoints[num2];
					float num3 = pointF.X - num4;
					float num5 = pointF.Y - num6;
					num7 = num3 / width;
					num8 = num5 / num9;
					num = 13;
					continue;
				}
				case 4:
					num7 = 1f;
					num = 7;
					continue;
				case 5:
					goto IL_183;
				case 6:
					goto IL_222;
				case 7:
					goto IL_148;
				case 8:
					goto IL_222;
				case 9:
					if (num8 > 1f)
					{
						num = 10;
						continue;
					}
					goto IL_B8;
				case 10:
					if (true)
					{
					}
					goto IL_69;
				case 11:
				{
					RectangleF bounds = A_0.GetBounds();
					num4 = bounds.Left;
					num6 = bounds.Top;
					width = bounds.Width;
					num9 = bounds.Height;
					num = 6;
					continue;
				}
				case 12:
					goto IL_1A6;
				case 13:
					if (num7 <= 1f)
					{
						goto IL_148;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (A_3)
				{
					num = 11;
					continue;
				}
				num4 = 0f;
				num6 = 0f;
				width = A_5.Width;
				num9 = A_4;
				num = 8;
				continue;
				IL_69:
				num8 = 1f;
				num = 1;
				continue;
				IL_B8:
				spr\u2420 spr_u;
				PointF a_ = spr_u.ᜀ(A_1, num7);
				PointF a_2 = spr_u.ᜀ(A_2, num7);
				float num10 = spr\u2420.ᜀ(a_, a_2);
				PointF pointF2 = spr\u2420.ᜀ(a_, a_2, num10 * num8);
				pathPoints[num2] = pointF2;
				num2++;
				num = 5;
				continue;
				IL_148:
				num = 9;
				continue;
				IL_183:
				num = 3;
				continue;
				IL_222:
				pathPoints = A_0.PathPoints;
				spr_u = new spr\u2420();
				num2 = 0;
				num = 0;
			}
			IL_1A6:
			return spr\u255A.ᜀ(pathPoints, A_0.PathTypes);
		}
		}
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x0023A528 File Offset: 0x00239528
	private static spr\u1B70 ᜀ(PointF[] A_0, byte[] A_1)
	{
		sprᴎ sprᴎ;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_194:
			sprᴎ = null;
			num = 5;
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		int num2;
		spr\u1926 spr_u;
		PointF[] array;
		PointF pointF;
		int num3;
		spr\u1B70 spr_u1B;
		for (;;)
		{
			IL_36:
			float x;
			float y;
			switch (num)
			{
			case 0:
				num2 = 0;
				spr_u.ᜁ(new spr\u17F0(array));
				pointF = array[3];
				num = 23;
				continue;
			case 1:
				goto IL_2B2;
			case 2:
				goto IL_29B;
			case 3:
				goto IL_118;
			case 4:
				if ((A_1[num3] & 3) == 3)
				{
					num = 6;
					continue;
				}
				num = 20;
				continue;
			case 5:
				if (num2 == 0)
				{
					num = 18;
					continue;
				}
				goto IL_392;
			case 6:
				goto IL_194;
			case 7:
				spr_u = new spr\u1926();
				spr_u.ᜀ(true);
				sprᴎ = null;
				pointF = new PointF(x, y);
				num = 9;
				continue;
			case 8:
				if (A_1[num3] == 0)
				{
					num = 7;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
			case 9:
				goto IL_29B;
			case 10:
				goto IL_392;
			case 11:
				goto IL_225;
			case 12:
				if ((A_1[num3] & 128) == 128)
				{
					num = 14;
					continue;
				}
				goto IL_29B;
			case 13:
				num = 17;
				continue;
			case 14:
				spr_u1B.ᜁ(spr_u);
				spr_u = new spr\u1926();
				spr_u.ᜀ(true);
				sprᴎ = null;
				num2 = 0;
				num = 2;
				continue;
			case 15:
				return spr_u1B;
			case 16:
				sprᴎ = new sprᴎ();
				spr_u.ᜁ(sprᴎ);
				sprᴎ.ᜀ().Add(pointF);
				num = 3;
				continue;
			case 17:
				if (sprᴎ == null)
				{
					num = 16;
					continue;
				}
				goto IL_118;
			case 18:
				array = new PointF[]
				{
					PointF.Empty,
					PointF.Empty,
					PointF.Empty,
					PointF.Empty
				};
				array[0] = pointF;
				num = 10;
				continue;
			case 19:
				if (num2 == 3)
				{
					num = 0;
					continue;
				}
				goto IL_2B2;
			case 20:
				if ((A_1[num3] & 1) == 1)
				{
					num = 13;
					continue;
				}
				goto IL_2B2;
			case 21:
				if (num3 >= A_0.Length)
				{
					num = 15;
					continue;
				}
				x = A_0[num3].X;
				y = A_0[num3].Y;
				num = 8;
				continue;
			case 22:
				goto IL_225;
			case 23:
				goto IL_2B2;
			}
			goto IL_9D;
			IL_118:
			pointF = new PointF(x, y);
			sprᴎ.ᜀ().Add(pointF);
			num = 1;
			continue;
			IL_225:
			num = 21;
			continue;
			IL_29B:
			num3++;
			num = 11;
			continue;
			IL_2B2:
			num = 12;
			continue;
			IL_392:
			num2++;
			array[num2] = new PointF(x, y);
			num = 19;
		}
		return spr_u1B;
		IL_20:
		if (false)
		{
		}
		switch (0)
		{
		}
		IL_9D:
		spr_u1B = new spr\u1B70();
		spr_u = null;
		array = new PointF[]
		{
			PointF.Empty,
			PointF.Empty,
			PointF.Empty,
			PointF.Empty
		};
		pointF = PointF.Empty;
		num2 = 0;
		sprᴎ = null;
		num3 = 0;
		num = 22;
		goto IL_36;
	}
}
