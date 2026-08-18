using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.Doc.Fields.Shape;

// Token: 0x0200034D RID: 845
internal class sprᣛ
{
	// Token: 0x06002D1F RID: 11551 RVA: 0x002B4BCC File Offset: 0x002B3BCC
	internal sprᣛ(spr\u25AC A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x002B4BF4 File Offset: 0x002B3BF4
	internal SizeF ᜁ(sprṏ A_0)
	{
		switch (0)
		{
		default:
		{
			float num3;
			float height;
			for (;;)
			{
				spr\u24A6 a_ = new spr\u24A6();
				this.ᜀ(A_0, a_);
				sprᴐ sprᴐ = A_0.ᜉ().ᜧ();
				SizeF size = this.ᜀ.Size;
				float num = sprᣛ.ᜀ(A_0.ᜉ());
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 2;
						continue;
					case 1:
						if (true)
						{
						}
						num2 = 4;
						continue;
					case 2:
						goto IL_A3;
					case 3:
						IL_18F:
						goto IL_B3;
					case 4:
						if (num == -90f)
						{
							num2 = 5;
							continue;
						}
						goto IL_B3;
					case 5:
						goto IL_16E;
					case 6:
						goto IL_169;
					case 7:
						if ((double)num3 <= A_0.ᜉ().\u177D())
						{
							num2 = 0;
							continue;
						}
						num2 = 6;
						continue;
					case 8:
						if (num != 90f)
						{
							num2 = 1;
							continue;
						}
						goto IL_16E;
					}
					break;
					IL_B3:
					num3 = (float)((double)size.Width + sprᴐ.ᜃ() + sprᴐ.ᜂ());
					height = (float)((double)size.Height + sprᴐ.ᜅ() + sprᴐ.ᜁ());
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18F;
					default:
						if (false)
						{
						}
						num2 = 7;
						continue;
					}
					IL_16E:
					size = new SizeF(size.Height, size.Width);
					num2 = 3;
				}
			}
			IL_A3:
			float num4 = (float)A_0.ᜉ().\u177D();
			goto IL_194;
			IL_169:
			num4 = (float)((double)num3);
			IL_194:
			return new SizeF(num4, height);
		}
		}
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x002B4DA0 File Offset: 0x002B3DA0
	internal void ᜀ(sprṏ A_0, spr\u24A6 A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			spr\u24A6 spr_u24A;
			spr\u1937 a_;
			float num2;
			float width;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 3;
					continue;
				case 1:
					goto IL_F4;
				case 2:
					if (spr_u24A != null)
					{
						goto IL_F7;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_85;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 3:
					if (!this.ᜁ.ᜀ(A_0.ᜉ()))
					{
						num = 1;
						continue;
					}
					a_ = A_0.ᜉ();
					num2 = sprᣛ.ᜀ(a_);
					this.ᜀ = sprᣛ.ᜀ(A_0, num2);
					width = this.ᜀ.Width;
					spr_u24A = this.ᜀ(A_0);
					goto IL_85;
				case 4:
					return;
				}
				if (this.ᜁ != null)
				{
					num = 0;
					continue;
				}
				return;
				IL_85:
				num = 2;
			}
			return;
			IL_F4:
			return;
			IL_F7:
			spr_u24A.ᜀ(this.ᜀ(num2));
			sprᝪ sprᝪ = this.ᜁ(a_);
			spr_u24A.ᜀ(spr\u1B70.ᜀ(new RectangleF(-sprᝪ.ᜁ(), 0f, sprᝪ.ᜁ() + sprᝪ.ᜀ() + width, this.ᜀ.Height)));
			A_1.ᜁ(spr_u24A);
			return;
		}
		}
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x002B4EFC File Offset: 0x002B3EFC
	private sprᝪ ᜁ(spr\u1937 A_0)
	{
		switch (0)
		{
		default:
		{
			float num2;
			float num3;
			for (;;)
			{
				sprᴐ sprᴐ = A_0.ᜧ();
				LayoutFlow layoutFlow = sprᴐ.ᜀ();
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_C7;
					case 1:
						num = 2;
						continue;
					case 2:
						if (true)
						{
						}
						num2 = (float)sprᴐ.ᜃ();
						num3 = (float)sprᴐ.ᜂ();
						num = 4;
						continue;
					case 3:
						goto IL_A9;
					case 4:
						goto IL_FB;
					case 5:
						switch (layoutFlow)
						{
						case LayoutFlow.TopToBottomIdeographic:
						case LayoutFlow.TopToBottom:
							num2 = (float)sprᴐ.ᜅ();
							num3 = (float)sprᴐ.ᜁ();
							num = 0;
							continue;
						case LayoutFlow.BottomToTop:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num2 = (float)sprᴐ.ᜁ();
								num3 = (float)sprᴐ.ᜅ();
								num = 3;
								continue;
							}
							break;
						}
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_A9:
			IL_C7:
			IL_FB:
			float num4 = (float)(5.0 + A_0.ᝅ());
			return new sprᝪ(num2 + num4, num3 + num4);
		}
		}
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x002B5024 File Offset: 0x002B4024
	internal bool ᜀ()
	{
		if (this.ᜁ != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_30;
			}
			if (false)
			{
			}
			IL_30:
			if (true)
			{
			}
			return this.ᜁ.ᜃ();
		}
		return false;
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x002B5078 File Offset: 0x002B4078
	private spr\u24A6 ᜀ(sprṏ A_0)
	{
		switch (0)
		{
		default:
		{
			object a_3;
			float y;
			Color a_4;
			for (;;)
			{
				spr\u1937 spr_u = A_0.ᜉ();
				float num = Math.Max(0f, this.ᜀ.Width);
				float a_ = Math.Max(0f, this.ᜀ.Height);
				int num2 = 2;
				for (;;)
				{
					Color color;
					switch (num2)
					{
					case 0:
						color = Color.Empty;
						goto IL_E5;
					case 1:
						if (!this.ᜀ())
						{
							num2 = 8;
							continue;
						}
						goto IL_10C;
					case 2:
					{
						spr\u25AC spr_u25AC = this.ᜁ;
						if (spr_u.ᜧ().ᜇ() != TextBoxWrapMode.None)
						{
							goto IL_158;
						}
						if (!spr_u.ᜧ().ᜆ())
						{
							goto IL_158;
						}
						float a_2 = 0f;
						IL_171:
						spr_u25AC.ᜁ(a_2);
						this.ᜁ.ᜀ(a_);
						a_3 = this.ᜁ.ᜀ(spr_u, new spr\u230C());
						y = this.ᜀ.Y;
						this.ᜀ(A_0.ᜉ().ᜧ().ᜄ());
						num2 = 11;
						continue;
						IL_158:
						a_2 = num;
						goto IL_171;
					}
					case 3:
						color = spr_u.\u1738();
						goto IL_E5;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AB;
						default:
							if (false)
							{
							}
							this.ᜀ = new RectangleF(this.ᜀ.Location, new SizeF(this.ᜁ.ᜂ(), this.ᜀ.Height));
							num2 = 14;
							continue;
						}
						break;
					case 5:
						num2 = 1;
						continue;
					case 6:
						goto IL_AB;
					case 7:
						goto IL_253;
					case 8:
						this.ᜀ = new RectangleF(this.ᜀ.Location, new SizeF(this.ᜁ.ᜂ(), this.ᜁ.ᜀ()));
						num2 = 7;
						continue;
					case 9:
						if (true)
						{
						}
						num2 = 10;
						continue;
					case 10:
						if (spr_u.ᜦ().ᜉ() != FillType.Solid)
						{
							num2 = 6;
							continue;
						}
						num2 = 3;
						continue;
					case 11:
						if (spr_u.ᝆ())
						{
							num2 = 9;
							continue;
						}
						goto IL_AB;
					case 12:
						if (this.ᜁ.ᜂ() > this.ᜀ.Width)
						{
							num2 = 4;
							continue;
						}
						goto IL_2B0;
					case 13:
						if (spr_u.\u171F())
						{
							num2 = 5;
							continue;
						}
						goto IL_10C;
					case 14:
						goto IL_2AE;
					}
					break;
					IL_AB:
					num2 = 0;
					continue;
					IL_E5:
					a_4 = color;
					num2 = 13;
					continue;
					IL_10C:
					num2 = 12;
				}
			}
			IL_253:
			IL_2AE:
			IL_2B0:
			return this.ᜁ.ᜀ(a_3, new SizeF(this.ᜁ.ᜂ(), this.ᜁ.ᜀ()), this.ᜀ.Height - this.ᜀ.Y + y, a_4);
		}
		}
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x002B537C File Offset: 0x002B437C
	private void ᜀ(TextBoxAnchor A_0)
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
		PointF location = sprᣛ.ᜀ(this.ᜀ, new SizeF(this.ᜁ.ᜂ(), this.ᜁ.ᜀ()), A_0);
		this.ᜀ = new RectangleF(location, this.ᜀ.Size);
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x002B53F8 File Offset: 0x002B43F8
	internal static RectangleF ᜀ(sprṏ A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			PointF location;
			SizeF size;
			for (;;)
			{
				RectangleF rectangleF = A_0.ᜑ();
				spr\u1BA8 spr_u1BA = A_0.ᜈ();
				spr\u1937 spr_u = A_0.ᜉ();
				float num = (float)spr_u.ᝅ();
				int num2 = 1;
				for (;;)
				{
					float height;
					float num3;
					float num5;
					float num4;
					float num6;
					float width;
					float num8;
					float num9;
					switch (num2)
					{
					case 0:
						if (height != 0f)
						{
							num2 = 12;
							continue;
						}
						num2 = 14;
						continue;
					case 1:
						if (num > 1f)
						{
							num2 = 5;
							continue;
						}
						goto IL_1F1;
					case 2:
						num3 = 1f;
						goto IL_37B;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33D;
						}
						if (false)
						{
						}
						if (spr\u1BA8.ᜀ(spr_u))
						{
							num2 = 9;
							continue;
						}
						goto IL_3D9;
					case 4:
						num4 = num5 / height;
						goto IL_208;
					case 5:
						goto IL_33D;
					case 6:
						goto IL_AF;
					case 7:
						num3 = num6 / width;
						goto IL_37B;
					case 8:
						goto IL_3D7;
					case 9:
					{
						float num7 = (num8 - num9) * 0.5f;
						location = new PointF(location.X - num7, location.Y + num7);
						num2 = 8;
						continue;
					}
					case 10:
						num2 = 7;
						continue;
					case 11:
						if (!spr_u.\u171D())
						{
							num2 = 13;
							continue;
						}
						goto IL_AF;
					case 12:
						num2 = 4;
						continue;
					case 13:
						goto IL_1F1;
					case 14:
						num4 = 1f;
						goto IL_208;
					case 15:
						if (true)
						{
						}
						if (width != 0f)
						{
							num2 = 10;
							continue;
						}
						num2 = 2;
						continue;
					}
					break;
					IL_AF:
					float num10 = num * 0.5f;
					float num11 = rectangleF.X;
					float num12 = rectangleF.Y;
					num9 = rectangleF.Width;
					num8 = rectangleF.Height;
					float num13 = num11 + num9;
					float num14 = num12 + num8;
					width = A_0.\u170D().Width;
					height = A_0.\u170D().Height;
					float num15 = (float)spr_u.ᜧ().ᜃ() + num10;
					float num16 = (float)spr_u.ᜧ().ᜅ() + num10;
					float num17 = (float)spr_u.ᜧ().ᜂ() + num10;
					float num18 = (float)spr_u.ᜧ().ᜁ() + num10;
					num6 = (float)spr_u.\u1776();
					num5 = (float)spr_u.ឍ();
					float num19 = width - num15 - num17;
					float num20 = height - num16 - num18;
					float num21 = num11 / num6;
					float num22 = num13 / num6;
					float num23 = num12 / num5;
					float num24 = num14 / num5;
					float num25 = num19 * num21 + num15;
					float num26 = num19 * num22 + num15;
					float num27 = num20 * num23 + num16;
					float num28 = num20 * num24 + num16;
					num2 = 15;
					continue;
					IL_1F1:
					num = 0f;
					num2 = 6;
					continue;
					IL_208:
					float num29 = num4;
					float num30;
					num11 = num25 * num30 + num10;
					num13 = num26 * num30 + num10;
					num12 = num27 * num29 - num10;
					num14 = num28 * num29 - num10;
					PointF[] array = new PointF[]
					{
						new PointF(num11, num12),
						new PointF(num13, num14)
					};
					spr_u1BA.ᜀ(array, true);
					num9 = array[1].X - array[0].X;
					num8 = array[1].Y - array[0].Y;
					size = sprᣛ.ᜀ(spr_u, num9, num8, A_1);
					location = array[0];
					num2 = 3;
					continue;
					IL_33D:
					num2 = 11;
					continue;
					IL_37B:
					num30 = num3;
					num2 = 0;
				}
			}
			IL_3D7:
			IL_3D9:
			return new RectangleF(location, size);
		}
		}
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x002B57E8 File Offset: 0x002B47E8
	private static PointF ᜀ(RectangleF A_0, SizeF A_1, TextBoxAnchor A_2)
	{
		PointF location;
		for (;;)
		{
			SizeF size = A_0.Size;
			location = A_0.Location;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (size.Height <= A_1.Height)
						{
							num = 5;
							continue;
						}
						break;
					}
					num = 1;
					continue;
				case 1:
					switch (A_2)
					{
					case TextBoxAnchor.Top:
					case TextBoxAnchor.TopCentered:
					case TextBoxAnchor.MiddleCentered:
					case TextBoxAnchor.BottomCentered:
					case TextBoxAnchor.TopBaseline:
					case TextBoxAnchor.BottomBaseline:
					case TextBoxAnchor.TopCenteredBaseline:
					case TextBoxAnchor.BottomCenteredBaseline:
						return location;
					case TextBoxAnchor.Middle:
						location = new PointF(location.X, location.Y + (size.Height - A_1.Height) * 0.5f);
						num = 4;
						continue;
					case TextBoxAnchor.Bottom:
						location = new PointF(location.X, location.Y + (size.Height - A_1.Height));
						num = 6;
						continue;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					goto IL_B3;
				case 4:
					return location;
				case 5:
					goto IL_70;
				case 6:
					goto IL_A3;
				}
				break;
			}
		}
		IL_70:
		if (true)
		{
		}
		return location;
		IL_A3:
		return location;
		IL_B3:
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x002B5948 File Offset: 0x002B4948
	internal static float ᜀ(spr\u1937 A_0)
	{
		for (;;)
		{
			IL_38:
			LayoutFlow layoutFlow = A_0.ᜧ().ᜀ();
			int num = 0;
			for (;;)
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
					switch (num)
					{
					case 0:
						switch (layoutFlow)
						{
						case LayoutFlow.TopToBottomIdeographic:
						case LayoutFlow.TopToBottom:
							goto IL_7A;
						case LayoutFlow.BottomToTop:
							goto IL_6A;
						default:
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_80;
					case 2:
						goto IL_8B;
					}
					goto IL_38;
				}
				IL_80:
				num = 2;
			}
		}
		IL_6A:
		return -90f;
		IL_7A:
		return 90f;
		IL_8B:
		return 0f;
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x002B59E8 File Offset: 0x002B49E8
	private static SizeF ᜀ(spr\u1937 A_0, float A_1, float A_2, float A_3)
	{
		float width;
		float height;
		for (;;)
		{
			IL_30:
			width = A_1;
			height = A_2;
			int num = 1;
			for (;;)
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
					switch (num)
					{
					case 0:
						goto IL_78;
					case 1:
						goto IL_3C;
					case 2:
						width = A_2;
						height = A_1;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_30;
				}
				IL_3C:
				if (!(A_3 != 0f ^ spr\u1BA8.ᜀ(A_0)))
				{
					goto IL_7A;
				}
				num = 2;
			}
		}
		IL_78:
		IL_7A:
		return new SizeF(width, height);
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x002B5A78 File Offset: 0x002B4A78
	private spr\u25FD ᜀ(float A_0)
	{
		spr\u25FD spr_u25FD;
		for (;;)
		{
			spr_u25FD = new spr\u25FD();
			spr_u25FD.ᜀ(A_0, MatrixOrder.Append);
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0 == -90f)
					{
						num = 2;
						continue;
					}
					goto IL_DB;
				case 1:
					goto IL_79;
				case 2:
					spr_u25FD.ᜀ(0f, this.ᜀ.Width, MatrixOrder.Append);
					num = 1;
					continue;
				case 3:
					if (A_0 == 90f)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 4:
					goto IL_D9;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						spr_u25FD.ᜀ(this.ᜀ.Height, 0f, MatrixOrder.Append);
						num = 4;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_79:
		IL_D9:
		IL_DB:
		spr_u25FD.ᜀ(this.ᜀ.X, this.ᜀ.Y, MatrixOrder.Append);
		return spr_u25FD;
	}

	// Token: 0x04002670 RID: 9840
	private RectangleF ᜀ = RectangleF.Empty;

	// Token: 0x04002671 RID: 9841
	private readonly spr\u25AC ᜁ;
}
