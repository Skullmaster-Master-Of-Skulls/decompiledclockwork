using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000409 RID: 1033
internal class spr\u173C
{
	// Token: 0x0600396C RID: 14700 RVA: 0x0035696C File Offset: 0x0035596C
	internal static void ᜃ(sprṏ A_0)
	{
		if (A_0.ᜉ() == null)
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
				throw new ArgumentNullException();
			}
		}
		spr\u1937 spr_u = A_0.ᜉ();
		int[] a_ = spr\u173C.ᜀ(spr_u);
		spr\u2055[] a_2 = spr_u.ᝁ();
		PointF[] a_3 = spr\u173C.ᜀ(a_2, a_);
		A_0.ᜀ(spr\u173C.ᜀ(spr_u, a_));
		spr\u173C.ᜀ(A_0, a_3);
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x003569E8 File Offset: 0x003559E8
	internal static PointF[] ᜀ(spr\u2055[] A_0)
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
		return spr\u173C.ᜀ(A_0, null);
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x00356A2C File Offset: 0x00355A2C
	private static PointF[] ᜀ(spr\u2055[] A_0, int[] A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				int num2;
				spr\u2055 spr_u;
				int num3;
				int num4;
				PointF[] array;
				switch (num)
				{
				case 0:
					goto IL_BF;
				case 1:
					goto IL_6E;
				case 2:
					if (num2 >= A_0.Length)
					{
						num = 9;
						continue;
					}
					spr_u = A_0[num2];
					num3 = spr_u.ᜂ().ᜂ();
					num4 = spr_u.ᜁ().ᜂ();
					num = 4;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15B;
					default:
						if (false)
						{
						}
						if (spr_u.ᜁ().ᜁ())
						{
							num = 10;
							continue;
						}
						goto IL_BF;
					}
					break;
				case 4:
					if (A_1 != null)
					{
						num = 7;
						continue;
					}
					goto IL_BF;
				case 5:
					goto IL_E9;
				case 6:
					goto IL_15B;
				case 7:
					num = 6;
					continue;
				case 8:
					num3 = A_1[spr_u.ᜂ().ᜂ()];
					num = 5;
					continue;
				case 9:
					return array;
				case 10:
					num4 = A_1[spr_u.ᜁ().ᜂ()];
					num = 0;
					continue;
				case 11:
					goto IL_12F;
				case 13:
					goto IL_12F;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				array = new PointF[A_0.Length];
				num2 = 0;
				num = 11;
				continue;
				IL_BF:
				array[num2] = new PointF((float)num3, (float)num4);
				num2++;
				num = 13;
				continue;
				IL_E9:
				num = 3;
				continue;
				IL_15B:
				if (spr_u.ᜂ().ᜁ())
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				goto IL_E9;
				IL_12F:
				num = 2;
			}
			IL_6E:
			return null;
		}
		}
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x00356BF8 File Offset: 0x00355BF8
	private static RectangleF ᜀ(spr\u1937 A_0, int[] A_1)
	{
		spr\u1D34[] array;
		for (;;)
		{
			bool flag = spr\u173C.ᜁ(A_0);
			array = A_0.\u173D();
			int num = array.Length;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_AB;
				case 1:
					goto IL_F5;
				case 2:
					if (!flag)
					{
						num2 = 3;
						continue;
					}
					num2 = 1;
					continue;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B0;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 4:
					switch (num)
					{
					case 1:
						goto IL_68;
					case 2:
						num2 = 2;
						continue;
					case 3:
						goto IL_7B;
					case 4:
					case 5:
						goto IL_101;
					case 6:
						goto IL_72;
					default:
						num2 = 6;
						continue;
					}
					break;
				case 5:
					goto IL_BB;
				case 6:
					goto IL_B0;
				}
				break;
				IL_B0:
				num2 = 5;
			}
		}
		IL_68:
		return spr\u173C.ᜀ(array[0], A_1);
		IL_72:
		return spr\u173C.ᜀ(A_0, array, A_1);
		IL_7B:
		return spr\u173C.ᜀ(A_0, array, A_1);
		IL_AB:
		spr\u1D34 a_ = array[3];
		goto IL_FA;
		IL_BB:
		goto IL_101;
		IL_F5:
		a_ = array[0];
		IL_FA:
		return spr\u173C.ᜀ(a_, A_1);
		IL_101:
		return new RectangleF(0f, 0f, (float)A_0.\u1776(), (float)A_0.ឍ());
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x00356D24 File Offset: 0x00355D24
	private static RectangleF ᜀ(spr\u1937 A_0, spr\u1D34[] A_1, int[] A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			RectangleF a_;
			RectangleF a_2;
			float a_3;
			for (;;)
			{
				num = 0;
				int num2 = 4;
				for (;;)
				{
					float num3;
					float num4;
					sprᥴ sprᥴ;
					float num6;
					switch (num2)
					{
					case 0:
						goto IL_29D;
					case 1:
						num2 = 9;
						continue;
					case 2:
						num2 = 17;
						continue;
					case 3:
						goto IL_206;
					case 4:
						if (A_1.Length == 6)
						{
							num2 = 1;
							continue;
						}
						goto IL_F1;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A9;
						default:
							if (false)
							{
							}
							num2 = 15;
							continue;
						}
						break;
					case 6:
						if (A_0.\u1718() != null)
						{
							num2 = 2;
							continue;
						}
						goto IL_1CC;
					case 7:
					{
						a_ = spr\u173C.ᜀ(A_1[num], A_2);
						a_2 = spr\u173C.ᜀ(A_1[num + 1], A_2);
						int num5;
						a_3 = spr\u173C.ᜁ(num3, num4, (float)num5);
						num2 = 13;
						continue;
					}
					case 8:
						goto IL_F1;
					case 9:
						if (!spr\u173C.ᜁ(A_0))
						{
							if (true)
							{
							}
							num2 = 10;
							continue;
						}
						goto IL_F1;
					case 10:
						num = 3;
						num2 = 8;
						continue;
					case 11:
						num3 = spr\u173C.ᜁ(sprᥴ.ᜋ, A_2);
						num6 = spr\u173C.ᜁ(sprᥴ.ᜌ, A_2);
						num2 = 3;
						continue;
					case 12:
						if (sprᥴ.ᜇ.ᜂ() == HandlePositionType.Adjust)
						{
							num2 = 5;
							continue;
						}
						goto IL_82;
					case 13:
						goto IL_149;
					case 14:
					{
						int num5;
						if ((float)num5 < num4)
						{
							num2 = 7;
							continue;
						}
						a_ = spr\u173C.ᜀ(A_1[num + 1], A_2);
						a_2 = spr\u173C.ᜀ(A_1[num + 2], A_2);
						a_3 = spr\u173C.ᜁ(num4, num6, (float)num5 - num4);
						num2 = 16;
						continue;
					}
					case 15:
						if (sprᥴ.ᜇ.ᜀ() == 0)
						{
							num2 = 11;
							continue;
						}
						goto IL_82;
					case 16:
						goto IL_1C7;
					case 17:
					{
						if (A_0.\u1718().Length == 0)
						{
							num2 = 0;
							continue;
						}
						sprᥴ = A_0.\u1718()[0];
						int num5 = A_0.ᜂ(1);
						num2 = 12;
						continue;
					}
					case 18:
						goto IL_A9;
					}
					break;
					IL_82:
					num3 = spr\u173C.ᜁ(sprᥴ.\u170D, A_2);
					num6 = spr\u173C.ᜁ(sprᥴ.ᜎ, A_2);
					num2 = 18;
					continue;
					IL_F1:
					num2 = 6;
					continue;
					IL_206:
					num4 = num3 + (num6 - num3) / 2f;
					num2 = 14;
					continue;
					IL_A9:
					goto IL_206;
				}
			}
			IL_149:
			IL_1C7:
			goto IL_2A2;
			IL_1CC:
			return spr\u173C.ᜀ(A_1[num], A_2);
			IL_29D:
			goto IL_1CC;
			IL_2A2:
			return spr\u173C.ᜀ(a_, a_2, a_3);
		}
		}
	}

	// Token: 0x06003971 RID: 14705 RVA: 0x00356FE0 File Offset: 0x00355FE0
	private static bool ᜁ(spr\u1937 A_0)
	{
		if (A_0.ᜧ().ᜀ() != LayoutFlow.Horizontal)
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
				return A_0.ᜧ().ᜀ() == LayoutFlow.HorizontalIdeographic;
			}
		}
		return true;
	}

	// Token: 0x06003972 RID: 14706 RVA: 0x0035703C File Offset: 0x0035603C
	private static float ᜁ(float A_0, float A_1, float A_2)
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
		return A_2 / (A_1 - A_0);
	}

	// Token: 0x06003973 RID: 14707 RVA: 0x00357080 File Offset: 0x00356080
	private static RectangleF ᜀ(RectangleF A_0, RectangleF A_1, float A_2)
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
		float num = spr\u173C.ᜀ(A_0.X, A_1.X, A_2);
		float num2 = spr\u173C.ᜀ(A_0.Y, A_1.Y, A_2);
		float width = spr\u173C.ᜀ(A_0.Right, A_1.Right, A_2) - num;
		float height = spr\u173C.ᜀ(A_0.Bottom, A_1.Bottom, A_2) - num2;
		return new RectangleF(num, num2, width, height);
	}

	// Token: 0x06003974 RID: 14708 RVA: 0x0035711C File Offset: 0x0035611C
	private static float ᜀ(float A_0, float A_1, float A_2)
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
		return A_0 + (A_1 - A_0) * A_2;
	}

	// Token: 0x06003975 RID: 14709 RVA: 0x00357160 File Offset: 0x00356160
	private static RectangleF ᜀ(spr\u1D34 A_0, int[] A_1)
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
		float num = spr\u173C.ᜀ(A_0.ᜀ, A_1);
		float num2 = spr\u173C.ᜀ(A_0.ᜁ, A_1);
		float width = spr\u173C.ᜀ(A_0.ᜂ, A_1) - num;
		float height = spr\u173C.ᜀ(A_0.ᜃ, A_1) - num2;
		return new RectangleF(num, num2, width, height);
	}

	// Token: 0x06003976 RID: 14710 RVA: 0x003571DC File Offset: 0x003561DC
	private static void ᜀ(sprṏ A_0, PointF[] A_1)
	{
		int num2;
		Size a_;
		for (;;)
		{
			IL_2C:
			int num;
			ShapeType shapeType;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E1:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				shapeType = A_0.ᜉ().\u1774();
				num = 3;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					if (num2 == 0)
					{
						num = 5;
						continue;
					}
					goto IL_7A;
				case 3:
					switch (shapeType)
					{
					case ShapeType.Rectangle:
						goto IL_109;
					case ShapeType.RoundRectangle:
						goto IL_C8;
					case ShapeType.Ellipse:
						goto IL_BB;
					default:
						num = 0;
						continue;
					}
					break;
				case 4:
					goto IL_95;
				case 5:
					goto IL_104;
				case 6:
					if (shapeType != ShapeType.Line)
					{
						num = 1;
						continue;
					}
					goto IL_97;
				}
				goto IL_2C;
			}
			IL_C8:
			a_ = A_0.ᜉ().ព();
			num2 = A_0.ᜉ().ᜂ(1);
			goto IL_E1;
		}
		IL_7A:
		spr\u173C.ᜀ(A_0, num2, a_);
		return;
		IL_95:
		A_0.ᜀ(A_0.ᜉ().ᝀ());
		A_0.ᜀ(A_1);
		return;
		IL_97:
		spr\u173C.ᜁ(A_0);
		return;
		IL_BB:
		A_0.ᜀ(spr\u173C.ᜀ(A_0));
		return;
		IL_104:
		spr\u173C.ᜂ(A_0);
		return;
		IL_109:
		spr\u173C.ᜂ(A_0);
	}

	// Token: 0x06003977 RID: 14711 RVA: 0x00357314 File Offset: 0x00356314
	private static void ᜂ(sprṏ A_0)
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
		Size size = A_0.ᜉ().ព();
		A_0.ᜀ(new sprỬ[3]);
		A_0.ᜋ()[0] = new sprỬ(PathType.MoveTo, 0);
		A_0.ᜋ()[1] = new sprỬ(PathType.LineTo, 3);
		A_0.ᜀ(new PointF[]
		{
			PointF.Empty,
			PointF.Empty,
			PointF.Empty,
			PointF.Empty
		});
		A_0.ᜆ()[0] = new PointF(0f, 0f);
		A_0.ᜆ()[1] = new PointF((float)size.Width, 0f);
		A_0.ᜆ()[2] = new PointF((float)size.Width, (float)size.Height);
		A_0.ᜆ()[3] = new PointF(0f, (float)size.Height);
		A_0.ᜋ()[2] = new sprỬ(PathType.Close, 0);
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x00357474 File Offset: 0x00356474
	private static void ᜁ(sprṏ A_0)
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
		Size size = A_0.ᜉ().ព();
		A_0.ᜀ(new sprỬ[2]);
		A_0.ᜋ()[0] = new sprỬ(PathType.MoveTo, 0);
		A_0.ᜋ()[1] = new sprỬ(PathType.LineTo, 1);
		A_0.ᜀ(new PointF[]
		{
			PointF.Empty,
			PointF.Empty
		});
		A_0.ᜆ()[1] = new PointF((float)size.Width, (float)size.Height);
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x0035753C File Offset: 0x0035653C
	private static RectangleF ᜀ(sprṏ A_0)
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
		Size a_ = A_0.ᜉ().ព();
		A_0.ᜀ(new sprỬ[2]);
		A_0.ᜋ()[0] = new sprỬ(PathType.AngleEllipse, 3);
		A_0.ᜀ(new PointF[]
		{
			PointF.Empty,
			PointF.Empty,
			PointF.Empty
		});
		float x = (float)a_.Width * 0.5f;
		float y = (float)a_.Height * 0.5f;
		A_0.ᜆ()[0] = new PointF(x, y);
		A_0.ᜆ()[1] = new PointF(x, y);
		A_0.ᜆ()[2] = new PointF(0f, -23592960f);
		A_0.ᜋ()[1] = new sprỬ(PathType.Close, 0);
		return spr\u173C.ᜀ(a_);
	}

	// Token: 0x0600397A RID: 14714 RVA: 0x00357668 File Offset: 0x00356668
	private static RectangleF ᜀ(Size A_0)
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
		float num = (float)A_0.Height / (float)A_0.Width;
		float num2 = (float)A_0.Width * 0.5f;
		float num3 = (float)A_0.Height * 0.5f;
		float num4 = num3 * num3;
		float num5 = num2 * num2;
		float num6 = (float)Math.Sqrt((double)(num5 * num4 / (num4 + num * num * num5)));
		float num7 = num * num6;
		float num8 = -num6;
		float num9 = num * num8;
		PointF location = new PointF(num8 + num2, num9 + num3);
		PointF pointF = new PointF(num6 + num2, num7 + num3);
		SizeF size = new SizeF(pointF.X - location.X, pointF.Y - location.Y);
		return new RectangleF(location, size);
	}

	// Token: 0x0600397B RID: 14715 RVA: 0x00357754 File Offset: 0x00356754
	private static void ᜀ(sprṏ A_0, int A_1, Size A_2)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num2;
			int num3;
			for (;;)
			{
				SizeF sizeF = A_0.ᜌ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5F;
						default:
							goto IL_E7;
						}
						break;
					case 1:
						goto IL_9D;
					case 2:
						if (sizeF.Width > A_0.ᜌ().Height)
						{
							num = 3;
							continue;
						}
						num2 = A_1;
						num3 = (int)(A_0.ᜌ().Width / A_0.ᜌ().Height * (float)A_1);
						num = 1;
						continue;
					case 3:
						goto IL_5F;
					}
					break;
					IL_5F:
					num3 = A_1;
					num2 = (int)(A_0.ᜌ().Height / A_0.ᜌ().Width * (float)A_1);
					num = 0;
				}
			}
			IL_9D:
			goto IL_EF;
			IL_E7:
			if (false)
			{
			}
			IL_EF:
			A_0.ᜀ(new sprỬ[10]);
			A_0.ᜀ(new PointF[]
			{
				PointF.Empty,
				PointF.Empty,
				PointF.Empty,
				PointF.Empty,
				PointF.Empty,
				PointF.Empty,
				PointF.Empty,
				PointF.Empty
			});
			A_0.ᜋ()[0] = new sprỬ(PathType.MoveTo, 0);
			A_0.ᜆ()[0] = new PointF(0f, (float)num3);
			A_0.ᜋ()[1] = new sprỬ(PathType.EllipticalQuadrantY, 1);
			A_0.ᜆ()[1] = new PointF((float)num2, 0f);
			A_0.ᜋ()[2] = new sprỬ(PathType.LineTo, 1);
			A_0.ᜆ()[2] = new PointF((float)(A_2.Width - num2), 0f);
			A_0.ᜋ()[3] = new sprỬ(PathType.EllipticalQuadrantX, 1);
			A_0.ᜆ()[3] = new PointF((float)A_2.Width, (float)num3);
			A_0.ᜋ()[4] = new sprỬ(PathType.LineTo, 1);
			A_0.ᜆ()[4] = new PointF((float)A_2.Width, (float)(A_2.Height - num3));
			A_0.ᜋ()[5] = new sprỬ(PathType.EllipticalQuadrantY, 1);
			A_0.ᜆ()[5] = new PointF((float)(A_2.Width - num2), (float)A_2.Height);
			A_0.ᜋ()[6] = new sprỬ(PathType.LineTo, 1);
			A_0.ᜆ()[6] = new PointF((float)num2, (float)A_2.Height);
			A_0.ᜋ()[7] = new sprỬ(PathType.EllipticalQuadrantX, 1);
			A_0.ᜆ()[7] = new PointF(0f, (float)(A_2.Height - num3));
			A_0.ᜋ()[8] = new sprỬ(PathType.Close, 1);
			A_0.ᜋ()[9] = new sprỬ(PathType.End, 0);
			return;
		}
		}
	}

	// Token: 0x0600397C RID: 14716 RVA: 0x00357AB8 File Offset: 0x00356AB8
	private static float ᜁ(sprṚ A_0, int[] A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 1:
				goto IL_44;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_44;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (!A_0.ᜁ())
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
		}
		IL_44:
		float num2 = (float)A_1[A_0.ᜂ() - 3];
		goto IL_84;
		IL_7C:
		num2 = (float)A_0.ᜂ();
		IL_84:
		return num2;
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x00357B4C File Offset: 0x00356B4C
	private static float ᜀ(sprṚ A_0, int[] A_1)
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
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 3:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				goto IL_64;
			case 4:
				if (!A_0.ᜁ())
				{
					num = 1;
					continue;
				}
				num = 6;
				continue;
			case 5:
				goto IL_54;
			case 6:
				goto IL_C0;
			case 7:
				goto IL_B3;
			}
			IL_30:
			if (A_0.ᜁ())
			{
				num = 0;
				continue;
			}
			goto IL_64;
			goto IL_30;
			IL_64:
			num = 4;
		}
		IL_54:
		if (true)
		{
		}
		float num2 = (float)A_0.ᜂ();
		goto IL_D0;
		IL_B3:
		return 0f;
		IL_C0:
		num2 = (float)A_1[A_0.ᜂ()];
		IL_D0:
		return num2;
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x00357C2C File Offset: 0x00356C2C
	internal static int[] ᜀ(spr\u1937 A_0)
	{
		switch (0)
		{
		default:
		{
			int[] array2;
			for (;;)
			{
				spr\u2528[] array = A_0.\u1734();
				int num = 6;
				for (;;)
				{
					spr\u2528 spr_u;
					int num2;
					int num3;
					int num4;
					int num5;
					int num7;
					int num6;
					int num8;
					int num9;
					int num10;
					Operation ᜀ;
					switch (num)
					{
					case 0:
						goto IL_579;
					case 1:
						num = 38;
						continue;
					case 2:
						goto IL_281;
					case 3:
						if (!spr_u.ᜂ())
						{
							num = 1;
							continue;
						}
						num = 44;
						continue;
					case 4:
						num = 31;
						continue;
					case 5:
						goto IL_579;
					case 6:
						if (array != null)
						{
							num = 16;
							continue;
						}
						goto IL_528;
					case 7:
						goto IL_281;
					case 8:
						num2 = spr\u173C.ᜀ(A_0, array2, spr_u.ᜃ);
						goto IL_21B;
					case 9:
						goto IL_579;
					case 10:
						if (num3 >= array.Length)
						{
							num = 29;
							continue;
						}
						goto IL_4EC;
					case 11:
						goto IL_579;
					case 12:
						goto IL_579;
					case 13:
						goto IL_2F2;
					case 14:
						goto IL_579;
					case 15:
						goto IL_579;
					case 16:
						num = 39;
						continue;
					case 17:
						if (!spr_u.ᜁ())
						{
							num = 34;
							continue;
						}
						num = 22;
						continue;
					case 18:
						goto IL_579;
					case 19:
						goto IL_579;
					case 20:
						num = 27;
						continue;
					case 21:
						goto IL_579;
					case 22:
						num4 = spr\u173C.ᜀ(A_0, array2, spr_u.ᜄ);
						goto IL_40F;
					case 23:
						goto IL_579;
					case 24:
						if (!spr_u.ᜀ())
						{
							num = 20;
							continue;
						}
						num = 8;
						continue;
					case 25:
						if (num5 <= 0)
						{
							num = 4;
							continue;
						}
						num = 40;
						continue;
					case 26:
						goto IL_579;
					case 27:
						num2 = spr_u.ᜃ;
						goto IL_21B;
					case 28:
						goto IL_4E7;
					case 29:
						return array2;
					case 30:
						goto IL_579;
					case 31:
						num6 = num7;
						goto IL_52A;
					case 32:
						if (num7 != 0)
						{
							num = 41;
							continue;
						}
						goto IL_579;
					case 33:
						goto IL_579;
					case 34:
						num = 35;
						continue;
					case 35:
						num4 = spr_u.ᜄ;
						goto IL_40F;
					case 36:
						goto IL_579;
					case 37:
						goto IL_579;
					case 38:
						num8 = spr_u.ᜂ;
						goto IL_2F7;
					case 39:
						if (array.Length == 0)
						{
							num = 13;
							continue;
						}
						array2 = new int[array.Length];
						num3 = 0;
						num = 2;
						continue;
					case 40:
						num6 = num9;
						goto IL_52A;
					case 41:
						num10 /= num7;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4EC;
						default:
							if (false)
							{
							}
							num = 19;
							continue;
						}
						break;
					case 42:
						switch (ᜀ)
						{
						case Operation.Sum:
							num10 = num5 + num9 - num7;
							num = 37;
							continue;
						case Operation.Prod:
							num10 = num5 * num9;
							num = 32;
							continue;
						case Operation.Mid:
							num10 = (num5 + num9) / 2;
							num = 15;
							continue;
						case Operation.Abs:
							num10 = Math.Abs(num5);
							num = 14;
							continue;
						case Operation.Min:
							num10 = Math.Min(num5, num9);
							num = 5;
							continue;
						case Operation.Max:
							num10 = Math.Max(num5, num9);
							num = 0;
							continue;
						case Operation.If:
							num = 25;
							continue;
						case Operation.Mod:
							num10 = (int)Math.Sqrt((double)(num5 * num5 + num9 * num9 + num7 * num7));
							num = 36;
							continue;
						case Operation.Atan2:
							num10 = (int)(spr\u2109.ᜃ(Math.Atan2((double)num9, (double)num5)) * 65536.0);
							num = 12;
							continue;
						case Operation.Sin:
							num10 = (int)((double)num5 * Math.Sin(spr\u2109.ᜄ((double)((float)num9 / 65536f))));
							num = 45;
							continue;
						case Operation.Cos:
							num10 = (int)((double)num5 * Math.Cos(spr\u2109.ᜄ((double)((float)num9 / 65536f))));
							num = 18;
							continue;
						case Operation.CosAtan2:
							num10 = (int)((double)num5 * Math.Cos(Math.Atan2((double)num7, (double)num9)));
							num = 30;
							continue;
						case Operation.SinAtan2:
							num10 = (int)((double)num5 * Math.Sin(Math.Atan2((double)num7, (double)num9)));
							num = 33;
							continue;
						case Operation.Sqrt:
							num10 = (int)Math.Floor(Math.Sqrt((double)num5));
							num = 23;
							continue;
						case Operation.SumAngle:
							num10 = num5 + num9 * 65536 - num7 * 65536;
							num = 21;
							continue;
						case Operation.Ellipse:
							num10 = (int)((double)num7 * Math.Sqrt(1.0 - Math.Pow((double)num5 / (double)num9, 2.0)));
							num = 9;
							continue;
						case Operation.Tan:
							num10 = (int)((double)num5 * Math.Tan(spr\u2109.ᜄ((double)((float)num9 / 65536f))));
							num = 11;
							continue;
						default:
							num = 43;
							continue;
						}
						break;
					case 43:
						num = 28;
						continue;
					case 44:
						num8 = spr\u173C.ᜀ(A_0, array2, spr_u.ᜂ);
						goto IL_2F7;
					case 45:
						if (true)
						{
						}
						goto IL_579;
					}
					break;
					IL_21B:
					num9 = num2;
					num = 17;
					continue;
					IL_281:
					num = 10;
					continue;
					IL_2F7:
					num5 = num8;
					num = 24;
					continue;
					IL_40F:
					num7 = num4;
					ᜀ = spr_u.ᜀ;
					num = 42;
					continue;
					IL_4EC:
					spr_u = array[num3];
					num = 3;
					continue;
					IL_52A:
					num10 = num6;
					num = 26;
					continue;
					IL_579:
					array2[num3] = num10;
					num3++;
					num = 7;
				}
			}
			return array2;
			IL_2F2:
			goto IL_528;
			IL_4E7:
			throw new ArgumentOutOfRangeException();
			IL_528:
			return null;
		}
		}
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x00358260 File Offset: 0x00357260
	private static int ᜀ(spr\u1937 A_0, int[] A_1, int A_2)
	{
		int a_ = 1;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_2B1;
			case 1:
				goto IL_14C;
			case 2:
				return 0;
			case 3:
				if (A_2 != 508)
				{
					num = 11;
					continue;
				}
				num = 6;
				continue;
			case 4:
				switch (A_2)
				{
				case 1271:
					goto IL_264;
				case 1272:
					goto IL_87;
				case 1273:
					goto IL_1B0;
				case 1274:
				case 1275:
					goto IL_2B1;
				case 1276:
					goto IL_101;
				case 1277:
					goto IL_DD;
				case 1278:
					goto IL_29B;
				case 1279:
					goto IL_10D;
				default:
					num = 5;
					continue;
				}
				break;
			case 5:
				num = 0;
				continue;
			case 6:
				if (!A_0.ᜨ())
				{
					num = 2;
					continue;
				}
				return 1;
			case 7:
				if (A_2 <= 1151)
				{
					goto IL_141;
				}
				goto IL_1DD;
			case 8:
				num = 7;
				continue;
			case 10:
				switch (A_2)
				{
				case 320:
					goto IL_77;
				case 321:
					goto IL_F1;
				case 322:
					goto IL_1D6;
				case 323:
					goto IL_1A9;
				case 324:
				case 325:
				case 326:
				case 335:
				case 336:
				case 337:
				case 338:
					goto IL_2B1;
				case 327:
					goto IL_272;
				case 328:
					goto IL_123;
				case 329:
					goto IL_1BF;
				case 330:
					goto IL_1C7;
				case 331:
					goto IL_27A;
				case 332:
					goto IL_E9;
				case 333:
					goto IL_67;
				case 334:
					goto IL_293;
				case 339:
					goto IL_1CF;
				case 340:
					goto IL_28C;
				default:
					num = 12;
					continue;
				}
				break;
			case 11:
				num = 4;
				continue;
			case 12:
				num = 3;
				continue;
			}
			if (A_2 >= 1024)
			{
				num = 8;
				continue;
			}
			goto IL_1DD;
			IL_141:
			num = 1;
			continue;
			IL_2B1:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_141;
			default:
				goto IL_2C7;
			}
			IL_1DD:
			num = 10;
		}
		IL_67:
		if (true)
		{
		}
		return A_0.ᜂ(7);
		IL_77:
		return A_0.ᝍ() + A_0.\u1776() / 2;
		IL_87:
		return (int)spr\u1712.ᜄ(A_0.\u177D());
		IL_DD:
		return spr\u23C4.ᜈ(A_0.ន());
		IL_E9:
		return A_0.ᜂ(6);
		IL_F1:
		return A_0.ឈ() + A_0.ឍ() / 2;
		IL_101:
		return spr\u23C4.ᜈ(A_0.\u177D());
		IL_10D:
		return spr\u23C4.ᜈ(A_0.ន() * 0.5);
		IL_123:
		return A_0.ᜂ(2);
		IL_14C:
		return A_1[A_2 - 1024];
		IL_1A9:
		return A_0.ឍ();
		IL_1B0:
		return (int)spr\u1712.ᜄ(A_0.ន());
		IL_1BF:
		return A_0.ᜂ(3);
		IL_1C7:
		return A_0.ᜂ(4);
		IL_1CF:
		return A_0.\u1717();
		IL_1D6:
		return A_0.\u1776();
		IL_264:
		return (int)spr\u23C4.ᜈ((int)A_0.ᜭ());
		IL_272:
		return A_0.ᜂ(1);
		IL_27A:
		return A_0.ᜂ(5);
		IL_28C:
		return A_0.ᜬ();
		IL_293:
		return A_0.ᜂ(8);
		IL_29B:
		return spr\u23C4.ᜈ(A_0.\u177D() * 0.5);
		IL_2C7:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᝦࡨᥪ౬ɮ", a_), string.Format(ClipboardData.b("Ⅶ٨ᥪlᩮᵰቲ啴ᑶᡸ᝺Ṽ੾ꮊﶌﺚ뾞즠슢횤螦\udfa8쪪솬\udaae풰鎲\udab4톶莸鮺욼达변", a_), A_2));
	}
}
