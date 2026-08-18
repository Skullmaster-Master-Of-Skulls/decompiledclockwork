using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

// Token: 0x02000143 RID: 323
internal abstract class spr\u2573
{
	// Token: 0x06000864 RID: 2148 RVA: 0x0005D284 File Offset: 0x0005C284
	public spr\u1AB8 \u1716()
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
		return this.ᜁ;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0005D2C8 File Offset: 0x0005C2C8
	public LayoutState \u1717()
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
		return this.ᜀ;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0005D30C File Offset: 0x0005C30C
	public spr\u1D30 \u171A()
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
		return this.ᜂ.ᜀ();
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0005D354 File Offset: 0x0005C354
	public spr\u25FC \u1719()
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
		return this.ᜅ;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0005D398 File Offset: 0x0005C398
	public spr\u19E0 \u171E()
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
		return this.ᜆ.ᜀ();
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0005D3E0 File Offset: 0x0005C3E0
	internal sprᦰ \u171B()
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
		return this.ᜃ;
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0005D424 File Offset: 0x0005C424
	public double \u1715()
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
		return this.ᜂ.ᜀ().ᜊ().ᜂ() + this.ᜂ.ᜀ().ᜋ().ᜂ();
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0005D48C File Offset: 0x0005C48C
	public double \u171D()
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
		return this.ᜂ.ᜀ().ᜊ().ᜀ() + this.ᜂ.ᜀ().ᜋ().ᜀ();
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0005D4F4 File Offset: 0x0005C4F4
	public spr\u1AB8 \u1718()
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
		return this.ᜂ;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0005D538 File Offset: 0x0005C538
	public bool \u171C()
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
		return this.ᜇ;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0005D57C File Offset: 0x0005C57C
	public spr\u2573(spr\u1AB8 A_0, sprᴉ A_1)
	{
		this.ᜂ = A_0;
		this.ᜁ = A_0;
		this.ᜆ = A_1;
	}

	// Token: 0x0600086F RID: 2159
	public abstract sprᦰ ᜀ(RectangleF A_0);

	// Token: 0x06000870 RID: 2160 RVA: 0x0005D5A4 File Offset: 0x0005C5A4
	public bool \u1714()
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
			if (this.\u1717() != LayoutState.Splitted)
			{
				return false;
			}
			break;
		}
		return this.\u1716() != null;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0005D5F8 File Offset: 0x0005C5F8
	protected virtual void ᜈ()
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
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0005D634 File Offset: 0x0005C634
	protected void ᜃ(RectangleF A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_38;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (!this.ᜄ)
		{
			this.ᜅ = new spr\u25FC(A_0, this.\u171A(), this.ᜂ);
			return;
		}
		IL_38:
		this.ᜅ = new spr\u25FC(A_0);
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0005D6A0 File Offset: 0x0005C6A0
	protected void ᜀ(RectangleF A_0, bool A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_38;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (!this.ᜄ)
		{
			this.ᜅ = new spr\u25FC(A_0, this.\u171A(), this.ᜂ, A_1);
			return;
		}
		IL_38:
		this.ᜅ = new spr\u25FC(A_0, A_1);
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0005D70C File Offset: 0x0005C70C
	protected void ᜀ(RectangleF A_0, Paddings A_1)
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
		this.\u171A().ᜊ().ᜂ((double)A_1.Left);
		this.\u171A().ᜊ().ᜃ((double)A_1.Right);
		this.\u171A().ᜊ().ᜁ((double)A_1.Top);
		this.\u171A().ᜊ().ᜀ((double)A_1.Bottom);
		this.ᜅ = new spr\u25FC(A_0, this.\u171A(), this.ᜂ);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0005D7BC File Offset: 0x0005C7BC
	protected void ᜂ(RectangleF A_0)
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
		this.ᜅ = new spr\u25FC(A_0, this.\u171A());
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0005D80C File Offset: 0x0005C80C
	protected void ᜀ(PointF A_0, bool A_1)
	{
		RectangleF a_;
		for (;;)
		{
			this.ᜃ = new sprᦰ(this.ᜂ);
			a_ = this.ᜃ.ᜁ();
			A_0.X += (float)this.\u171A().ᜋ().ᜃ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.Y += (float)this.\u171A().ᜋ().ᜁ();
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_C0;
				case 2:
					if (!A_1)
					{
						goto IL_C2;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C0;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_C0:
		IL_C2:
		a_.Location = A_0;
		this.ᜃ.ᜀ(a_);
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0005D8F0 File Offset: 0x0005C8F0
	protected void ᜀ(PointF A_0)
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
		this.ᜃ = new sprᦰ(this.ᜂ);
		RectangleF a_ = this.ᜃ.ᜁ();
		A_0.X += (float)this.\u171A().ᜋ().ᜃ();
		A_0.Y += (float)this.\u171A().ᜋ().ᜁ();
		a_.Location = A_0;
		this.ᜃ.ᜀ(a_);
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0005D99C File Offset: 0x0005C99C
	public static spr\u2573 ᜀ(spr\u1AB8 A_0, sprᴉ A_1, float A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			spr\u17C8 spr_u17C;
			spr\u1AE4 spr_u1AE;
			spr\u2297 spr_u;
			sprᲲ sprᲲ;
			for (;;)
			{
				spr_u17C = (A_0 as spr\u17C8);
				int num = 19;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_305;
					case 1:
						spr_u1AE = (A_0 as TextBox).ᜀ();
						num = 24;
						continue;
					case 2:
						if (A_0 is GroupedShapeObject)
						{
							num = 25;
							continue;
						}
						goto IL_305;
					case 3:
						if (A_0 is spr\u1AE7)
						{
							num = 6;
							continue;
						}
						goto IL_12D;
					case 4:
						if (spr_u != null)
						{
							num = 12;
							continue;
						}
						spr_u1AE = null;
						num = 21;
						continue;
					case 5:
						if (A_0 is Field)
						{
							num = 10;
							continue;
						}
						goto IL_261;
					case 6:
						if (true)
						{
						}
						goto IL_2CF;
					case 7:
						if (!spr_u17C.ᜀ().ᜃ())
						{
							goto IL_14D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2CF;
						default:
							if (false)
							{
							}
							num = 26;
							continue;
						}
						break;
					case 8:
						spr_u = (A_0 as spr\u248F).ᜑ();
						num = 29;
						continue;
					case 9:
						spr_u = (A_0 as Field).\u1714();
						num = 14;
						continue;
					case 10:
						num = 32;
						continue;
					case 11:
						if (A_0 is spr\u24D5)
						{
							num = 16;
							continue;
						}
						goto IL_1A0;
					case 12:
						goto IL_1BE;
					case 13:
						if (spr_u1AE != null)
						{
							num = 15;
							continue;
						}
						sprᲲ = (A_0 as sprᲲ);
						num = 17;
						continue;
					case 14:
						goto IL_261;
					case 15:
						goto IL_148;
					case 16:
						spr_u = (A_0 as spr\u24D5).ᜀ();
						num = 28;
						continue;
					case 17:
						if (sprᲲ != null)
						{
							num = 30;
							continue;
						}
						goto IL_3C0;
					case 18:
						goto IL_12D;
					case 19:
						if (spr_u17C != null)
						{
							num = 23;
							continue;
						}
						spr_u = (A_0 as spr\u2297);
						num = 5;
						continue;
					case 20:
						if (A_0 is spr\u248F)
						{
							num = 8;
							continue;
						}
						goto IL_1F0;
					case 21:
						if (A_0 is spr\u1AE4)
						{
							num = 27;
							continue;
						}
						num = 31;
						continue;
					case 22:
						goto IL_12D;
					case 23:
						num = 7;
						continue;
					case 24:
						goto IL_12D;
					case 25:
						spr_u = (A_0 as GroupedShapeObject).ᜀ();
						num = 0;
						continue;
					case 26:
						goto IL_25C;
					case 27:
						spr_u1AE = (A_0 as spr\u1AE4);
						num = 18;
						continue;
					case 28:
						goto IL_1A0;
					case 29:
						goto IL_1F0;
					case 30:
						goto IL_19E;
					case 31:
						if (A_0 is TextBox)
						{
							num = 1;
							continue;
						}
						num = 3;
						continue;
					case 32:
						if ((A_0 as Field).Type == FieldType.FieldSymbol)
						{
							num = 9;
							continue;
						}
						goto IL_261;
					}
					break;
					IL_12D:
					num = 13;
					continue;
					IL_1A0:
					num = 4;
					continue;
					IL_1F0:
					num = 2;
					continue;
					IL_261:
					num = 20;
					continue;
					IL_2CF:
					spr_u1AE = (A_0 as spr\u1AE7).ᜀ(A_2);
					num = 22;
					continue;
					IL_305:
					num = 11;
				}
			}
			IL_148:
			return new spr\u257C(spr_u1AE, A_1);
			IL_14D:
			return new spr\u25E5(spr_u17C, A_1);
			IL_19E:
			return new spr\u257C(sprᲲ, A_1);
			IL_1BE:
			return new spr\u249D(spr_u, A_1);
			IL_25C:
			return new spr᱆(spr_u17C, A_1);
			IL_3C0:
			throw new ArgumentException(ClipboardData.b("⑬ŮݰቲᥴṶᵸ孺੼ᙾꦈﾊﾎꦒ떔", a_) + A_0.GetType());
		}
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0005DD88 File Offset: 0x0005CD88
	internal RectangleF ᜀ(DocumentObject A_0, RectangleF A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_183:
				num = 17;
				break;
			default:
				if (false)
				{
				}
				goto IL_D2;
			}
			Paragraph paragraph;
			for (;;)
			{
				IL_2F:
				float num2;
				float num3;
				float num4;
				float num5;
				RectangleF rectangleF3;
				PointF pointF;
				switch (num)
				{
				case 0:
					return A_1;
				case 1:
				{
					ParagraphFormat format;
					if (format.FrameWidth == 0)
					{
						num = 9;
						continue;
					}
					goto IL_162;
				}
				case 2:
				{
					RectangleF rectangleF;
					if (Math.Round((double)rectangleF.Bottom, 2) == Math.Round((double)A_1.Bottom, 2))
					{
						num = 14;
						continue;
					}
					goto IL_390;
				}
				case 3:
					paragraph = (A_0 as Table).Rows[0].Cells[0].Paragraphs[0];
					num = 26;
					continue;
				case 4:
					goto IL_2B2;
				case 5:
					paragraph = (A_0 as Paragraph);
					num = 35;
					continue;
				case 6:
					(this.ᜆ as spr\u1DA4).ᜀ(A_1.Location);
					num = 0;
					continue;
				case 7:
					goto IL_162;
				case 8:
				{
					ParagraphFormat format = paragraph.Format;
					Section section = this.ᜀ(paragraph) as Section;
					num2 = A_1.X;
					num3 = A_1.Y;
					num4 = A_1.Width;
					num = 38;
					continue;
				}
				case 9:
					num4 = A_1.Width - Math.Abs(num2);
					num = 7;
					continue;
				case 10:
				{
					RectangleF rectangleF2;
					if (Math.Round((double)rectangleF2.X, 2) == Math.Round((double)A_1.X, 2))
					{
						num = 12;
						continue;
					}
					goto IL_390;
				}
				case 11:
					goto IL_FC;
				case 12:
					num = 29;
					continue;
				case 13:
					if (paragraph != null)
					{
						num = 8;
						continue;
					}
					goto IL_59D;
				case 14:
				{
					RectangleF rectangleF2 = (this.ᜆ as spr\u1DA4).ᜃ();
					num = 10;
					continue;
				}
				case 15:
					if (A_0 is Paragraph)
					{
						num = 5;
						continue;
					}
					num = 20;
					continue;
				case 16:
					goto IL_20C;
				case 17:
					if (A_1.Height - Math.Abs(num3) < 0f)
					{
						num = 32;
						continue;
					}
					num5 = A_1.Height - Math.Abs(num3);
					num = 25;
					continue;
				case 18:
				{
					ParagraphFormat format;
					ushort num6 = (ushort)format.FrameHeight;
					bool flag = (num6 & 32768) != 0;
					num = 31;
					continue;
				}
				case 19:
				{
					ParagraphFormat format;
					num4 = (float)format.FrameWidth / 20f;
					num = 4;
					continue;
				}
				case 20:
					if (A_0 is Table)
					{
						num = 3;
						continue;
					}
					goto IL_34B;
				case 21:
					goto IL_465;
				case 22:
				{
					ParagraphFormat format;
					if (format.FrameHeight != 0)
					{
						num = 18;
						continue;
					}
					goto IL_2F3;
				}
				case 23:
				{
					RectangleF rectangleF = (this.ᜆ as spr\u1DA4).ᜃ();
					num = 2;
					continue;
				}
				case 24:
					if (Math.Round((double)rectangleF3.Width, 2) == Math.Round((double)A_1.Width, 2))
					{
						if (true)
						{
						}
						num = 23;
						continue;
					}
					goto IL_390;
				case 25:
					goto IL_FC;
				case 26:
					goto IL_34B;
				case 27:
					goto IL_2F3;
				case 28:
					if (!pointF.Equals(A_1.Location))
					{
						num = 6;
						continue;
					}
					return A_1;
				case 29:
					if (this.ᜀ(paragraph))
					{
						num = 16;
						continue;
					}
					goto IL_390;
				case 30:
				{
					ParagraphFormat format;
					Section section;
					num2 = this.ᜁ(format, section, A_1, num4);
					num3 = this.ᜀ(format, section, A_1, num5);
					num = 21;
					continue;
				}
				case 31:
				{
					bool flag;
					if (!flag)
					{
						num = 37;
						continue;
					}
					goto IL_2F3;
				}
				case 32:
					num5 -= A_1.Height - Math.Abs(num3);
					num = 11;
					continue;
				case 33:
				{
					ParagraphFormat format;
					if (format.FrameHeightRule != FrameSizeRule.Exact)
					{
						num = 36;
						continue;
					}
					goto IL_FC;
				}
				case 34:
				{
					Section section;
					if (section != null)
					{
						num = 30;
						continue;
					}
					goto IL_465;
				}
				case 35:
					goto IL_34B;
				case 36:
					goto IL_183;
				case 37:
				{
					ushort num6;
					num5 = (float)(num6 & 32767) / 20f;
					num = 27;
					continue;
				}
				case 38:
				{
					ParagraphFormat format;
					if (format.FrameWidth != 0)
					{
						num = 19;
						continue;
					}
					goto IL_2B2;
				}
				}
				goto IL_D2;
				IL_FC:
				A_1 = new RectangleF(num2, num3, num4, num5);
				rectangleF3 = (this.ᜆ as spr\u1DA4).ᜃ();
				num = 24;
				continue;
				IL_162:
				num = 33;
				continue;
				IL_2B2:
				num5 = A_1.Height;
				num = 22;
				continue;
				IL_2F3:
				num = 34;
				continue;
				IL_34B:
				num = 13;
				continue;
				IL_390:
				(this.ᜆ as spr\u1DA4).ᜁ(A_1);
				pointF = (this.ᜆ as spr\u1DA4).ᜆ();
				num = 28;
				continue;
				IL_465:
				num = 1;
			}
			IL_20C:
			return (this.ᜆ as spr\u1DA4).ᜃ();
			IL_59D:
			return default(RectangleF);
			IL_D2:
			paragraph = null;
			num = 15;
			goto IL_2F;
		}
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0005E33C File Offset: 0x0005D33C
	private bool ᜀ(Paragraph A_0)
	{
		int num = 2;
		IDocumentObject documentObject;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 12;
				continue;
			case 1:
				if (A_0.Format.FrameY == (documentObject as Table).Rows[0].Cells[0].Paragraphs[0].Format.FrameY)
				{
					if (true)
					{
					}
					num = 13;
					continue;
				}
				return false;
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
					documentObject = null;
					num = 4;
					continue;
				}
				break;
			case 4:
				if (A_0.IsInCell)
				{
					num = 7;
					continue;
				}
				documentObject = A_0.PreviousSibling;
				num = 5;
				continue;
			case 5:
				goto IL_1F3;
			case 6:
				goto IL_1F3;
			case 7:
				documentObject = A_0.Owner.Owner.Owner.PreviousSibling;
				num = 6;
				continue;
			case 8:
				if (documentObject is Paragraph)
				{
					num = 0;
					continue;
				}
				num = 9;
				continue;
			case 9:
				if (documentObject is Table)
				{
					num = 10;
					continue;
				}
				return false;
			case 10:
				num = 1;
				continue;
			case 11:
				goto IL_1D8;
			case 12:
				if (A_0.Format.FrameY == (documentObject as Paragraph).Format.FrameY)
				{
					num = 11;
					continue;
				}
				return false;
			case 13:
				goto IL_19E;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return false;
			IL_1F3:
			num = 8;
		}
		IL_19E:
		return A_0.Format.FrameX == (documentObject as Table).Rows[0].Cells[0].Paragraphs[0].Format.FrameX;
		IL_1D8:
		return A_0.Format.FrameX == (documentObject as Paragraph).Format.FrameX;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0005E56C File Offset: 0x0005D56C
	internal RectangleF ᜁ(Paragraph A_0, RectangleF A_1)
	{
		switch (0)
		{
		default:
		{
			float x;
			float y;
			float num;
			float num4;
			for (;;)
			{
				ParagraphFormat format = A_0.Format;
				Section section = this.ᜀ(A_0) as Section;
				x = A_1.X;
				y = A_1.Y;
				num = A_1.Width;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						ushort num3 = (ushort)format.FrameHeight;
						num4 = (float)(num3 & 32767) / 20f;
						num2 = 8;
						continue;
					}
					case 1:
						goto IL_136;
					case 2:
						num = (float)format.FrameWidth / 20f;
						num2 = 5;
						continue;
					case 3:
						x = this.ᜁ(format, section, A_1, num);
						y = this.ᜀ(format, section, A_1, num4);
						num2 = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_138;
						default:
							if (false)
							{
							}
							if (section != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_171;
						}
						break;
					case 5:
						goto IL_138;
					case 6:
						if (format.FrameWidth != 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_138;
					case 7:
						if (format.FrameHeight != 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_87;
					case 8:
						goto IL_87;
					}
					break;
					IL_87:
					num2 = 4;
					continue;
					IL_138:
					if (true)
					{
					}
					num4 = A_1.Height;
					num2 = 7;
				}
			}
			IL_136:
			IL_171:
			return new RectangleF(x, y, num, num4);
		}
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0005E6F8 File Offset: 0x0005D6F8
	private DocumentObject ᜀ(DocumentObject A_0)
	{
		DocumentObject documentObject;
		for (;;)
		{
			if (true)
			{
			}
			documentObject = A_0;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_50;
				case 1:
					if ((documentObject as Table).\u1712.OwnerBase is TextBox)
					{
						num = 12;
						continue;
					}
					goto IL_50;
				case 2:
					if ((documentObject as Table).IsTextBox)
					{
						num = 11;
						continue;
					}
					goto IL_50;
				case 3:
					if (documentObject is Table)
					{
						num = 8;
						continue;
					}
					goto IL_50;
				case 4:
					if (documentObject.Owner != null)
					{
						num = 6;
						continue;
					}
					return documentObject;
				case 5:
					goto IL_139;
				case 6:
					documentObject = documentObject.Owner;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_139;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 7:
					if (documentObject is Section)
					{
						num = 9;
						continue;
					}
					num = 3;
					continue;
				case 8:
					num = 2;
					continue;
				case 9:
					return documentObject;
				case 10:
					goto IL_BB;
				case 11:
					num = 1;
					continue;
				case 12:
					documentObject = ((documentObject as Table).\u1712.OwnerBase as DocumentObject);
					num = 0;
					continue;
				}
				break;
				IL_50:
				num = 4;
				continue;
				IL_BB:
				num = 7;
				continue;
				IL_139:
				goto IL_BB;
			}
		}
		return documentObject;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0005E878 File Offset: 0x0005D878
	private float ᜁ(ParagraphFormat A_0, Section A_1, RectangleF A_2, float A_3)
	{
		switch (0)
		{
		default:
		{
			float result;
			for (;;)
			{
				result = 0f;
				short num = A_0.FrameX;
				int num2 = 22;
				for (;;)
				{
					byte b;
					byte b3;
					switch (num2)
					{
					case 0:
						return result;
					case 1:
						num2 = 29;
						continue;
					case 2:
						switch (b)
						{
						case 0:
							result = (this.ᜆ as spr\u1DA4).ᜈ().X;
							goto IL_2A3;
						case 1:
							if (true)
							{
							}
							result = A_1.PageSetup.Margins.Left;
							num2 = 8;
							continue;
						case 2:
							result = 0f;
							num2 = 0;
							continue;
						default:
							num2 = 13;
							continue;
						}
						break;
					case 3:
						return result;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A3;
						default:
							if (false)
							{
							}
							num2 = 15;
							continue;
						}
						break;
					case 5:
						return result;
					case 6:
						return result;
					case 7:
						num2 = 16;
						continue;
					case 8:
						return result;
					case 9:
						num2 = 17;
						continue;
					case 10:
						if (num != -8)
						{
							num2 = 9;
							continue;
						}
						goto IL_353;
					case 11:
						return result;
					case 12:
						num2 = 30;
						continue;
					case 13:
						num2 = 3;
						continue;
					case 14:
						return result;
					case 15:
						if (num != -12)
						{
							num2 = 1;
							continue;
						}
						goto IL_2FE;
					case 16:
						if (num != -16)
						{
							num2 = 4;
							continue;
						}
						goto IL_353;
					case 17:
					{
						if (num != -4)
						{
							num2 = 12;
							continue;
						}
						byte b2 = A_0.FrameHorizontalPos;
						num2 = 19;
						continue;
					}
					case 18:
						return result;
					case 19:
					{
						byte b2;
						switch (b2)
						{
						case 0:
							result = (this.ᜆ as spr\u1DA4).ᜈ().Left + (this.ᜆ as spr\u1DA4).ᜈ().Width / 2f;
							num2 = 25;
							continue;
						case 1:
							result = A_1.PageSetup.Margins.Left + (A_1.PageSetup.PageSize.Width - A_1.PageSetup.Margins.Right - A_1.PageSetup.Margins.Left) / 2f;
							num2 = 5;
							continue;
						case 2:
							result = A_1.PageSetup.PageSize.Width / 2f;
							num2 = 18;
							continue;
						default:
							num2 = 24;
							continue;
						}
						break;
					}
					case 20:
						goto IL_2FE;
					case 21:
						return result;
					case 22:
						if (num <= -12)
						{
							num2 = 7;
							continue;
						}
						num2 = 10;
						continue;
					case 23:
						return result;
					case 24:
						num2 = 28;
						continue;
					case 25:
						return result;
					case 26:
						switch (b3)
						{
						case 0:
							result = (this.ᜆ as spr\u1DA4).ᜈ().Width;
							num2 = 11;
							continue;
						case 1:
							result = A_1.PageSetup.PageSize.Width - A_1.PageSetup.Margins.Right;
							num2 = 6;
							continue;
						case 2:
							result = A_1.PageSetup.PageSize.Width;
							num2 = 14;
							continue;
						default:
							num2 = 31;
							continue;
						}
						break;
					case 27:
						return result;
					case 28:
						return result;
					case 29:
						goto IL_26B;
					case 30:
						if (num == 0)
						{
							num2 = 20;
							continue;
						}
						goto IL_26B;
					case 31:
						num2 = 21;
						continue;
					}
					break;
					IL_26B:
					result = (float)A_0.FrameX / 20f;
					num2 = 23;
					continue;
					IL_2A3:
					num2 = 27;
					continue;
					IL_2FE:
					b = A_0.FrameHorizontalPos;
					num2 = 2;
					continue;
					IL_353:
					b3 = A_0.FrameHorizontalPos;
					num2 = 26;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0005ECFC File Offset: 0x0005DCFC
	private float ᜀ(ParagraphFormat A_0, Section A_1, RectangleF A_2, float A_3)
	{
		switch (0)
		{
		default:
		{
			float result;
			for (;;)
			{
				result = 0f;
				float top = A_1.PageSetup.Margins.Top;
				float bottom = A_1.PageSetup.Margins.Bottom;
				float num = A_1.PageSetup.PageSize.Height - top - bottom;
				short num2 = A_0.FrameY;
				int num3 = 31;
				for (;;)
				{
					byte b;
					byte b3;
					switch (num3)
					{
					case 0:
						return result;
					case 1:
						num3 = 7;
						continue;
					case 2:
						num3 = 4;
						continue;
					case 3:
						num3 = 34;
						continue;
					case 4:
						return result;
					case 5:
						goto IL_42A;
					case 6:
						goto IL_2B0;
					case 7:
						return result;
					case 8:
						num3 = 20;
						continue;
					case 9:
						return result;
					case 10:
						num3 = 13;
						continue;
					case 11:
						num3 = 33;
						continue;
					case 12:
						num3 = 24;
						continue;
					case 13:
						goto IL_416;
					case 14:
						if (num2 != -12)
						{
							num3 = 30;
							continue;
						}
						goto IL_385;
					case 15:
						return result;
					case 16:
						return result;
					case 17:
						return result;
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42A;
						default:
							if (false)
							{
							}
							switch (b)
							{
							case 0:
								result = top + num;
								num3 = 32;
								continue;
							case 1:
								result = A_1.PageSetup.PageSize.Height;
								num3 = 16;
								continue;
							default:
								num3 = 2;
								continue;
							}
							break;
						}
						break;
					case 19:
					{
						byte b2;
						switch (b2)
						{
						case 0:
							result = top + num / 2f;
							num3 = 15;
							continue;
						case 1:
							result = A_1.PageSetup.PageSize.Height / 2f;
							num3 = 17;
							continue;
						default:
							num3 = 1;
							continue;
						}
						break;
					}
					case 20:
						if (num2 != -20)
						{
							num3 = 27;
							continue;
						}
						goto IL_385;
					case 21:
						if (num2 != -16)
						{
							num3 = 10;
							continue;
						}
						goto IL_2B0;
					case 22:
					{
						if (num2 != -8)
						{
							num3 = 3;
							continue;
						}
						byte b2 = A_0.FrameVerticalPos;
						num3 = 19;
						continue;
					}
					case 23:
						return result;
					case 24:
						return result;
					case 25:
						switch (b3)
						{
						case 0:
							result = top;
							num3 = 29;
							continue;
						case 1:
							result = 0f;
							num3 = 26;
							continue;
						default:
							num3 = 12;
							continue;
						}
						break;
					case 26:
						return result;
					case 27:
						num3 = 21;
						continue;
					case 28:
						return result;
					case 29:
						return result;
					case 30:
						num3 = 22;
						continue;
					case 31:
						if (num2 <= -16)
						{
							num3 = 8;
							continue;
						}
						num3 = 14;
						continue;
					case 32:
						return result;
					case 33:
						result = (float)A_0.FrameY / 20f;
						num3 = 0;
						continue;
					case 34:
						if (num2 == -4)
						{
							num3 = 6;
							continue;
						}
						goto IL_416;
					}
					break;
					IL_42A:
					if (true)
					{
					}
					byte b4;
					switch (b4)
					{
					case 0:
						result = top + (float)A_0.FrameY / 20f;
						num3 = 9;
						continue;
					case 1:
						result = (float)A_0.FrameY / 20f;
						num3 = 28;
						continue;
					case 2:
						result = A_2.Y + (float)A_0.FrameY / 20f;
						num3 = 23;
						continue;
					default:
						num3 = 11;
						continue;
					}
					IL_2B0:
					b3 = A_0.FrameVerticalPos;
					num3 = 25;
					continue;
					IL_385:
					b = A_0.FrameVerticalPos;
					num3 = 18;
					continue;
					IL_416:
					b4 = A_0.FrameVerticalPos;
					num3 = 5;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0005F160 File Offset: 0x0005E160
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2573()
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
	}

	// Token: 0x04001334 RID: 4916
	protected LayoutState ᜀ;

	// Token: 0x04001335 RID: 4917
	protected spr\u1AB8 ᜁ;

	// Token: 0x04001336 RID: 4918
	protected spr\u1AB8 ᜂ;

	// Token: 0x04001337 RID: 4919
	protected sprᦰ ᜃ;

	// Token: 0x04001338 RID: 4920
	protected bool ᜄ;

	// Token: 0x04001339 RID: 4921
	protected spr\u25FC ᜅ;

	// Token: 0x0400133A RID: 4922
	protected sprᴉ ᜆ;

	// Token: 0x0400133B RID: 4923
	protected bool ᜇ;

	// Token: 0x0400133C RID: 4924
	internal static bool ᜈ;
}
