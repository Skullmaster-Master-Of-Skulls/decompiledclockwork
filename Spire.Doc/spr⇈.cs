using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.Doc.Fields.Shape;

// Token: 0x020003D1 RID: 977
internal class spr\u21C8
{
	// Token: 0x060036FD RID: 14077 RVA: 0x00338910 File Offset: 0x00337910
	private spr\u21C8()
	{
	}

	// Token: 0x060036FE RID: 14078 RVA: 0x00338924 File Offset: 0x00337924
	private static spr\u2262 ᜁ(spr\u2262 A_0, double A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_77;
				}
				break;
			case 2:
				goto IL_51;
			case 3:
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (A_0.ᜁ() == 255)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_51:
		int num2 = A_0.ᜁ();
		goto IL_8B;
		IL_77:
		if (false)
		{
		}
		num2 = (int)(255.0 * A_1);
		IL_8B:
		int a_ = num2;
		return new spr\u2262(a_, A_0);
	}

	// Token: 0x060036FF RID: 14079 RVA: 0x003389C4 File Offset: 0x003379C4
	internal static sprᤕ ᜀ(spr\u2262 A_0, double A_1)
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
		return new spr\u253E(spr\u21C8.ᜁ(A_0, A_1));
	}

	// Token: 0x06003700 RID: 14080 RVA: 0x00338A0C File Offset: 0x00337A0C
	internal static sprᤕ ᜀ(byte[] A_0, byte[] A_1, spr\u2262 A_2, spr\u2262 A_3)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4B;
			case 2:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_94;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4B;
				default:
					goto IL_74;
				}
				break;
			case 4:
				if (true)
				{
				}
				A_1 = A_0;
				num = 0;
				continue;
			}
			if (A_1 == null)
			{
				num = 4;
				continue;
			}
			IL_4B:
			num = 2;
		}
		IL_74:
		if (false)
		{
		}
		return spr\u21C8.ᜀ(A_2, 1.0);
		IL_94:
		spr\u1BE7 spr_u1BE = new spr\u1BE7(A_1);
		spr_u1BE.ᜀ(new spr\u2262[]
		{
			spr\u2262.ឌ,
			A_2,
			spr\u2262.ᜋ,
			A_3
		});
		spr_u1BE.ᜉ().ᜁ(0.75f, 0.75f, MatrixOrder.Prepend);
		return spr_u1BE;
	}

	// Token: 0x06003701 RID: 14081 RVA: 0x00338AF4 File Offset: 0x00337AF4
	internal static sprᤕ ᜀ(sprᤖ A_0, byte[] A_1, spr\u1BA8 A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A0;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_52;
				default:
					goto IL_7B;
				}
				break;
			case 3:
				goto IL_52;
			case 4:
				A_1 = A_0.ᜆ();
				if (true)
				{
				}
				num = 3;
				continue;
			}
			if (A_1 == null)
			{
				num = 4;
				continue;
			}
			IL_52:
			num = 0;
		}
		IL_7B:
		if (false)
		{
		}
		return spr\u21C8.ᜀ(spr\u2262.ᜀ(A_0.ᜇ()), A_0.ᜅ());
		IL_A0:
		spr\u1BE7 spr_u1BE = new spr\u1BE7(A_1, WrapMode.Clamp);
		spr_u1BE.ᜀ((float)A_0.ᜅ());
		spr\u2481 spr_u = spr\u2075.\u171A(A_1);
		spr_u1BE.ᜀ(A_2.ᜀ(spr_u.ᜑ(), A_0.ᜈ()));
		return spr_u1BE;
	}

	// Token: 0x06003702 RID: 14082 RVA: 0x00338BD8 File Offset: 0x00337BD8
	private static bool ᜂ(sprᤖ A_0)
	{
		for (;;)
		{
			bool flag = spr\u21C8.ᜁ(A_0);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_27;
				case 1:
					goto IL_A2;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_27;
					default:
					{
						if (false)
						{
						}
						if (true)
						{
						}
						Color color = A_0.ᜇ();
						num = 3;
						continue;
					}
					}
					break;
				case 3:
				{
					Color color;
					if (color.ToArgb() == A_0.ᜋ().ToArgb())
					{
						num = 1;
						continue;
					}
					return false;
				}
				}
				break;
				IL_27:
				if (!flag)
				{
					return false;
				}
				num = 2;
			}
		}
		IL_A2:
		return A_0.ᜅ() == A_0.ᜎ();
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x00338C8C File Offset: 0x00337C8C
	private static bool ᜁ(sprᤖ A_0)
	{
		if (A_0.ᜄ() == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0A;
			}
			if (false)
			{
			}
			return true;
		}
		IL_0A:
		if (true)
		{
		}
		return A_0.ᜄ().Length == 0;
	}

	// Token: 0x06003704 RID: 14084 RVA: 0x00338CE0 File Offset: 0x00337CE0
	internal static sprᤕ ᜀ(spr\u1B70 A_0, sprᤖ A_1, PointF A_2)
	{
		if (!spr\u21C8.ᜂ(A_1))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0A;
			}
			if (false)
			{
			}
			spr\u1D5C spr_u1D5C = new spr\u1D5C(A_0, A_2);
			spr\u21C8.ᜂ(A_1, spr_u1D5C);
			return spr_u1D5C;
		}
		IL_0A:
		if (true)
		{
		}
		return spr\u21C8.ᜀ(spr\u2262.ᜀ(A_1.ᜇ()), A_1.ᜅ());
	}

	// Token: 0x06003705 RID: 14085 RVA: 0x00338D4C File Offset: 0x00337D4C
	private static void ᜂ(sprᤖ A_0, spr\u1F58 A_1)
	{
		if (!spr\u21C8.ᜁ(A_0))
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
				spr\u21C8.ᜀ(A_0, A_1);
				return;
			}
		}
		spr\u21C8.ᜁ(A_0, A_1);
	}

	// Token: 0x06003706 RID: 14086 RVA: 0x00338DA0 File Offset: 0x00337DA0
	private static void ᜁ(sprᤖ A_0, spr\u1F58 A_1)
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
		spr\u2262 spr_u = spr\u2262.ᜀ(A_0.ᜇ());
		spr\u2262 spr_u2 = sprầ.ᜀ(spr\u2262.ᜀ(A_0.ᜋ()), spr_u);
		spr_u = spr\u21C8.ᜁ(spr_u, A_0.ᜅ());
		spr_u2 = spr\u21C8.ᜁ(spr_u2, A_0.ᜎ());
		sprᨂ[] a_ = new sprᨂ[]
		{
			new sprᨂ(spr_u, 0f),
			new sprᨂ(spr\u21C8.ᜀ(spr_u, spr_u2, 0.2f), 0.2f),
			new sprᨂ(spr\u21C8.ᜀ(spr_u, spr_u2, 0.4f), 0.4f),
			new sprᨂ(spr\u21C8.ᜀ(spr_u, spr_u2, 0.6f), 0.6f),
			new sprᨂ(spr\u21C8.ᜀ(spr_u, spr_u2, 0.8f), 0.8f),
			new sprᨂ(spr_u2, 1f)
		};
		A_1.ᜀ(spr\u21C8.ᜀ(a_, A_0));
	}

	// Token: 0x06003707 RID: 14087 RVA: 0x00338EAC File Offset: 0x00337EAC
	private static spr\u2262 ᜀ(spr\u2262 A_0, spr\u2262 A_1, float A_2)
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
		A_2 = spr\u2109.ᜀ(A_2, 0f, 1f);
		return new spr\u2262(spr\u21C8.ᜀ(A_0.ᜁ(), A_1.ᜁ(), A_2), spr\u21C8.ᜀ(A_0.ᜃ(), A_1.ᜃ(), A_2), spr\u21C8.ᜀ(A_0.ᜆ(), A_1.ᜆ(), A_2), spr\u21C8.ᜀ(A_0.ᜄ(), A_1.ᜄ(), A_2));
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x00338F48 File Offset: 0x00337F48
	private static int ᜀ(int A_0, int A_1, float A_2)
	{
		if (true)
		{
		}
		if (A_1 != A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return A_0;
			}
			if (false)
			{
			}
			return (int)((float)A_0 + (float)(A_1 - A_0) * (A_2 + (float)Math.Sign(A_2 - (0.5f + (float)(A_0 - A_1) / 765f)) * 0.1f));
		}
		return A_0;
	}

	// Token: 0x06003709 RID: 14089 RVA: 0x00338FB4 File Offset: 0x00337FB4
	private static void ᜀ(sprᤖ A_0, spr\u1F58 A_1)
	{
		switch (0)
		{
		default:
		{
			sprᨂ[] array3;
			for (;;)
			{
				spr\u2143[] array = A_0.ᜄ();
				int num = array.Length;
				sprᨂ[] array2 = new sprᨂ[num];
				int num2 = 0;
				int num3 = 14;
				for (;;)
				{
					int num6;
					switch (num3)
					{
					case 0:
					{
						IL_21F:
						int num4;
						num4++;
						num3 = 4;
						continue;
					}
					case 1:
					{
						int num4;
						if (num4 > array2.Length)
						{
							num3 = 10;
							continue;
						}
						goto IL_290;
					}
					case 2:
						goto IL_290;
					case 3:
						if (array2[0].ᜁ() > 0f)
						{
							num3 = 13;
							continue;
						}
						goto IL_F6;
					case 4:
						goto IL_252;
					case 5:
						if (array2[array2.Length - 1].ᜁ() < 1f)
						{
							num3 = 0;
							continue;
						}
						goto IL_252;
					case 6:
						goto IL_CD;
					case 7:
						if (array2[array2.Length - 1].ᜁ() < 1f)
						{
							num3 = 17;
							continue;
						}
						goto IL_290;
					case 8:
					{
						if (num2 >= num)
						{
							num3 = 11;
							continue;
						}
						double num5 = (double)((float)spr\u23C4.ᜀ(array[num2].ᜁ));
						double a_ = A_0.ᜎ() + (A_0.ᜅ() - A_0.ᜎ()) * num5;
						spr\u2262 a_2 = spr\u21C8.ᜁ(spr\u2262.ᜀ(array[num2].ᜀ), a_);
						array2[num2] = new sprᨂ(a_2, (float)num5);
						num2++;
						num3 = 6;
						continue;
					}
					case 9:
					{
						int num4;
						num4++;
						num3 = 16;
						continue;
					}
					case 10:
					{
						int num4;
						array3 = new sprᨂ[num4];
						num6 = 0;
						num3 = 3;
						continue;
					}
					case 11:
					{
						if (true)
						{
						}
						int num4 = array2.Length;
						num3 = 12;
						continue;
					}
					case 12:
						if (array2[0].ᜁ() > 0f)
						{
							num3 = 9;
							continue;
						}
						goto IL_1F4;
					case 13:
						array3[0] = new sprᨂ(array2[0].ᜀ(), 0f);
						num6++;
						num3 = 15;
						continue;
					case 14:
						goto IL_CD;
					case 15:
						goto IL_F6;
					case 16:
						goto IL_1F4;
					case 17:
					{
						int num4;
						array3[num4 - 1] = new sprᨂ(array2[array2.Length - 1].ᜀ(), 1f);
						num3 = 2;
						continue;
					}
					}
					break;
					IL_CD:
					num3 = 8;
					continue;
					IL_F6:
					Array.Copy(array2, 0, array3, num6, array2.Length);
					num3 = 7;
					continue;
					IL_1F4:
					num3 = 5;
					continue;
					IL_290:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21F;
					default:
						goto IL_2A6;
					}
					IL_252:
					array3 = array2;
					num3 = 1;
				}
			}
			IL_2A6:
			if (false)
			{
			}
			A_1.ᜀ(spr\u21C8.ᜀ(array3, A_0));
			return;
		}
		}
	}

	// Token: 0x0600370A RID: 14090 RVA: 0x00339288 File Offset: 0x00338288
	private static sprᨂ[] ᜀ(sprᨂ[] A_0, sprᤖ A_1)
	{
		switch (0)
		{
		default:
		{
			sprᨂ[] array;
			for (;;)
			{
				float num = (float)A_1.ᜃ();
				int num2 = 22;
				for (;;)
				{
					int num3;
					int num4;
					sprᨂ sprᨂ;
					float num5;
					float num7;
					float num8;
					float num9;
					switch (num2)
					{
					case 0:
						num2 = 12;
						continue;
					case 1:
						return array;
					case 2:
						goto IL_2B6;
					case 3:
						if (num3 >= num4)
						{
							num2 = 1;
							continue;
						}
						num2 = 4;
						continue;
					case 4:
						sprᨂ = A_0[(num > 0f) ? num3 : (num4 - 1 - num3)];
						num2 = 18;
						continue;
					case 5:
						num5 = num - 100f;
						goto IL_1BD;
					case 6:
					{
						int num6;
						if (num6 < num4)
						{
							array[num4 - 1 - num6] = new sprᨂ(A_0[num6].ᜀ(), 1f - A_0[num6].ᜁ());
							num6++;
							num2 = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_300;
						default:
							if (false)
							{
							}
							num2 = 19;
							continue;
						}
						break;
					}
					case 7:
						goto IL_241;
					case 8:
						num7 = 1f - sprᨂ.ᜁ();
						goto IL_DF;
					case 9:
						goto IL_241;
					case 10:
						goto IL_21C;
					case 11:
						if (num == 100f)
						{
							num2 = 21;
							continue;
						}
						num *= 0.01f;
						num2 = 27;
						continue;
					case 12:
						num5 = num + 100f;
						goto IL_1BD;
					case 13:
						if (num == 0f)
						{
							num2 = 25;
							continue;
						}
						num4 = A_0.Length;
						num2 = 11;
						continue;
					case 14:
						num2 = 23;
						continue;
					case 15:
						num2 = 8;
						continue;
					case 16:
						goto IL_21C;
					case 17:
						goto IL_300;
					case 18:
						if (num <= 0f)
						{
							num2 = 15;
							continue;
						}
						num2 = 20;
						continue;
					case 19:
						return array;
					case 20:
						num7 = sprᨂ.ᜁ();
						goto IL_DF;
					case 21:
					{
						array = new sprᨂ[A_0.Length];
						int num6 = 0;
						num2 = 7;
						continue;
					}
					case 22:
						if (spr\u21C8.ᜀ(A_1))
						{
							num2 = 14;
							continue;
						}
						goto IL_2B6;
					case 23:
						if (num <= 0f)
						{
							num2 = 0;
							continue;
						}
						num2 = 5;
						continue;
					case 24:
						goto IL_27E;
					case 25:
						return A_0;
					case 26:
						goto IL_27E;
					case 27:
						if (num > 0f)
						{
							num2 = 17;
							continue;
						}
						num8 = -num;
						num9 = 1f + num;
						if (true)
						{
						}
						num2 = 24;
						continue;
					}
					break;
					IL_DF:
					float num10 = num7;
					array[num3] = new sprᨂ(sprᨂ.ᜀ(), num10 * num8);
					array[array.Length - 1 - num3] = new sprᨂ(sprᨂ.ᜀ(), 1f - num10 * num9);
					num3++;
					num2 = 16;
					continue;
					IL_1BD:
					num = num5;
					num2 = 2;
					continue;
					IL_21C:
					num2 = 3;
					continue;
					IL_241:
					num2 = 6;
					continue;
					IL_27E:
					array = new sprᨂ[num4 * 2];
					num3 = 0;
					num2 = 10;
					continue;
					IL_2B6:
					num2 = 13;
					continue;
					IL_300:
					num8 = 1f - num;
					num9 = num;
					num2 = 26;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x0033961C File Offset: 0x0033861C
	internal static sprᤕ ᜁ(spr\u1937 A_0, SizeF A_1)
	{
		switch (0)
		{
		default:
		{
			sprᤖ sprᤖ;
			spr\u1B70 spr_u1B;
			PointF pointF;
			for (;;)
			{
				sprᤖ = A_0.ᜦ();
				spr_u1B = new spr\u1B70();
				spr\u1926 spr_u = new spr\u1926();
				spr_u.ᜀ(true);
				spr_u1B.ᜁ(spr_u);
				RectangleF rectangleF = new RectangleF(0f, 0f, A_1.Width, A_1.Height);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_AC:
					pointF = sprὍ.ᜀ((float)sprᤖ.\u170D(), (float)sprᤖ.ᜊ(), rectangleF);
					pointF = sprὍ.ᜁ(pointF, sprὍ.ᜁ(rectangleF), (float)A_0.ម());
					PointF[] a_ = sprὍ.ᜀ(rectangleF, (float)A_0.ម());
					spr_u.ᜂ(a_);
					num = 1;
					break;
				}
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F2;
					case 1:
						goto IL_143;
					case 2:
						goto IL_AC;
					case 3:
					{
						if (A_0.\u1719())
						{
							num = 2;
							continue;
						}
						RectangleF rectangleF2 = sprὍ.ᜁ(rectangleF, (float)A_0.ម());
						pointF = sprὍ.ᜀ((float)sprᤖ.\u170D(), (float)sprᤖ.ᜊ(), rectangleF2);
						spr_u.ᜁ(rectangleF2);
						num = 0;
						continue;
					}
					}
					break;
				}
			}
			IL_F2:
			IL_143:
			return spr\u21C8.ᜀ(spr_u1B, sprᤖ, pointF);
		}
		}
	}

	// Token: 0x0600370C RID: 14092 RVA: 0x00339778 File Offset: 0x00338778
	internal static sprᤕ ᜀ(spr\u1937 A_0, SizeF A_1)
	{
		switch (0)
		{
		default:
		{
			sprᤖ sprᤖ;
			spr\u25FD spr_u25FD;
			RectangleF a_2;
			for (;;)
			{
				sprᤖ = A_0.ᜦ();
				int num = 2;
				for (;;)
				{
					float num2;
					float num3;
					RectangleF a_;
					switch (num)
					{
					case 0:
						goto IL_5B;
					case 1:
						goto IL_80;
					case 2:
						if (spr\u1D53.ᜀ(A_1))
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					case 3:
						spr_u25FD.ᜀ(num2 + num3, sprὍ.ᜁ(a_));
						a_2 = sprὍ.ᜁ(a_, -num3);
						num = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_105;
						default:
						{
							if (false)
							{
							}
							if (A_0.\u1719())
							{
								num = 3;
								continue;
							}
							if (true)
							{
							}
							spr_u25FD.ᜀ(num3, sprὍ.ᜁ(a_));
							RectangleF a_3 = sprὍ.ᜁ(a_, num2);
							a_2 = sprὍ.ᜁ(a_3, -num3);
							num = 7;
							continue;
						}
						}
						break;
					case 5:
						goto IL_103;
					case 6:
						if (spr\u21C8.ᜂ(sprᤖ))
						{
							num = 5;
							continue;
						}
						goto IL_105;
					case 7:
						goto IL_D4;
					}
					break;
					IL_105:
					a_ = new RectangleF(0f, 0f, A_1.Width, A_1.Height);
					num3 = -(float)sprᤖ.ᜁ() - 90f;
					num2 = (float)A_0.ខ();
					a_2 = RectangleF.Empty;
					spr_u25FD = new spr\u25FD();
					num = 4;
				}
			}
			IL_5B:
			return null;
			IL_80:
			IL_D4:
			goto IL_18D;
			IL_103:
			return spr\u21C8.ᜀ(spr\u2262.ᜀ(sprᤖ.ᜇ()), sprᤖ.ᜅ());
			IL_18D:
			spr\u201C spr_u201C = new spr\u201C(a_2);
			spr_u201C.ᜀ(spr_u25FD);
			spr\u21C8.ᜂ(sprᤖ, spr_u201C);
			return spr_u201C;
		}
		}
	}

	// Token: 0x0600370D RID: 14093 RVA: 0x00339930 File Offset: 0x00338930
	internal static sprᤕ ᜀ(sprᤖ A_0, byte[] A_1, float A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_89;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A0;
			case 3:
				goto IL_6C;
			case 4:
				A_1 = A_0.ᜆ();
				num = 3;
				continue;
			}
			if (A_1 == null)
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
					num = 4;
					continue;
				}
			}
			IL_6C:
			num = 2;
		}
		IL_89:
		return spr\u21C8.ᜀ(spr\u2262.ᜀ(A_0.ᜇ()), A_0.ᜅ());
		IL_A0:
		spr\u1BE7 spr_u1BE = new spr\u1BE7(A_1);
		spr_u1BE.ᜉ().ᜀ(A_2, MatrixOrder.Prepend);
		return spr_u1BE;
	}

	// Token: 0x0600370E RID: 14094 RVA: 0x003399F4 File Offset: 0x003389F4
	internal static bool ᜀ(sprᤖ A_0)
	{
		for (;;)
		{
			FillType fillType = A_0.ᜉ();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5B;
				case 1:
				{
					int num2;
					if (num2 == -45)
					{
						num = 6;
						continue;
					}
					return false;
				}
				case 2:
				{
					int num2;
					if (num2 != -135)
					{
						num = 7;
						continue;
					}
					return true;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					switch (fillType)
					{
					case FillType.Shade:
					case FillType.ShadeScale:
					case FillType.ShadeTitle:
					{
						int num2 = (int)A_0.ᜁ();
						if (true)
						{
						}
						num = 2;
						continue;
					}
					case FillType.ShadeCenter:
					case FillType.ShadeShape:
						return false;
					default:
						num = 0;
						continue;
					}
					break;
				case 5:
				{
					int num2;
					if (num2 != -90)
					{
						num = 3;
						continue;
					}
					return true;
				}
				case 6:
					goto IL_75;
				case 7:
					num = 5;
					continue;
				}
				break;
			}
		}
		IL_5B:
		return false;
		IL_75:
		return true;
	}
}
