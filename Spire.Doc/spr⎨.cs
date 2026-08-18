using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020001E8 RID: 488
internal class spr\u23A8 : sprᢿ
{
	// Token: 0x0600153F RID: 5439 RVA: 0x0015AEA8 File Offset: 0x00159EA8
	public void ᜀ(spr᪑ A_0, Graphics A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 0;
			GraphicsUnit pageUnit;
			float pageScale;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_14D;
				case 2:
					goto IL_109;
				case 3:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					pageUnit = A_1.PageUnit;
					A_1.PageUnit = GraphicsUnit.Point;
					pageScale = A_1.PageScale;
					A_1.PageScale = 1f;
					this.ᜀ = A_1;
					spr\u2058 spr_u = this.ᜇ = new spr\u2058();
					num = 1;
					continue;
				}
				case 4:
					goto IL_54;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 3;
				}
			}
			IL_54:
			throw new ArgumentNullException(ClipboardData.b("ᵲᩴ፶ᱸ", a_));
			IL_BC:
			throw new ArgumentNullException(ClipboardData.b("ᑲݴᙶॸ፺ᑼ᱾", a_));
			IL_109:
			goto IL_BC;
			IL_14D:
			try
			{
				A_0.ᜀ(this);
				goto IL_152;
			}
			finally
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
					num = 2;
					for (;;)
					{
						spr\u2058 spr_u;
						switch (num)
						{
						case 0:
							((IDisposable)spr_u).Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_B9;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 0;
					}
					IL_B9:
					break;
				}
			}
			goto IL_BC;
			IL_152:
			A_1.PageScale = pageScale;
			A_1.PageUnit = pageUnit;
			return;
		}
		}
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0015B028 File Offset: 0x0015A028
	public void ᜀ(spr᪑ A_0, Graphics A_1, PointF A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 2;
			GraphicsUnit pageUnit;
			float pageScale;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_109;
				case 1:
				{
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					pageUnit = A_1.PageUnit;
					A_1.PageUnit = GraphicsUnit.Point;
					pageScale = A_1.PageScale;
					A_1.PageScale = 1f;
					this.ᜀ = A_1;
					this.ᜁ = A_2;
					spr\u2058 spr_u = this.ᜇ = new spr\u2058();
					num = 3;
					continue;
				}
				case 3:
					goto IL_154;
				case 4:
					goto IL_54;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 4;
				}
				else
				{
					num = 1;
				}
			}
			IL_54:
			throw new ArgumentNullException(ClipboardData.b("०٨ཪ࡬", a_));
			IL_BC:
			throw new ArgumentNullException(ClipboardData.b("f᭨੪ᵬݮᡰၲٴ", a_));
			IL_109:
			goto IL_BC;
			IL_154:
			try
			{
				A_0.ᜀ(this);
				goto IL_159;
			}
			finally
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
					num = 2;
					for (;;)
					{
						spr\u2058 spr_u;
						switch (num)
						{
						case 0:
							goto IL_B9;
						case 1:
							((IDisposable)spr_u).Dispose();
							num = 0;
							continue;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 1;
					}
					IL_B9:
					break;
				}
			}
			goto IL_BC;
			IL_159:
			A_1.PageScale = pageScale;
			A_1.PageUnit = pageUnit;
			return;
		}
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0015B1AC File Offset: 0x0015A1AC
	public SizeF ᜀ(spr᪑ A_0, SizeF A_1, Graphics A_2, float A_3, float A_4, float A_5)
	{
		int a_ = 12;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_67;
				case 1:
					goto IL_49;
				case 2:
					if (true)
					{
					}
					if (A_2 == null)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				case 4:
					if (A_5 <= 0f)
					{
						num = 0;
						continue;
					}
					goto IL_E7;
				case 5:
					goto IL_E5;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			IL_67:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_A7;
			}
		}
		IL_49:
		throw new ArgumentNullException(ClipboardData.b("፱ѳյ", a_));
		IL_A7:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("űᝳ᝵ᑷό", a_));
		IL_E5:
		throw new ArgumentNullException(ClipboardData.b("ᕱٳ᝵ࡷቹᕻᵽ", a_));
		IL_E7:
		Matrix matrix = spr\u23A8.ᜀ(A_2, A_3, A_4);
		matrix.Scale(A_5, A_5, MatrixOrder.Prepend);
		Matrix transform = A_2.Transform;
		A_2.Transform = matrix;
		this.ᜀ(A_0, A_2);
		A_2.Transform = transform;
		PointF pointF = spr\u23A8.ᜀ(A_1.ToPointF(), A_2);
		return new SizeF(pointF.X * A_5, pointF.Y * A_5);
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0015B2FC File Offset: 0x0015A2FC
	public float ᜀ(spr᪑ A_0, SizeF A_1, Graphics A_2, float A_3, float A_4, float A_5, float A_6)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			for (;;)
			{
				IL_17:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
							if (false)
							{
							}
							if (A_5 <= 0f)
							{
								num = 5;
								continue;
							}
							num = 4;
							continue;
						}
						break;
					case 1:
						goto IL_C5;
					case 2:
						if (A_2 == null)
						{
							num = 1;
							continue;
						}
						num = 0;
						continue;
					case 4:
						if (true)
						{
						}
						if (A_6 <= 0f)
						{
							num = 7;
							continue;
						}
						goto IL_138;
					case 5:
						goto IL_A8;
					case 6:
						goto IL_55;
					case 7:
						goto IL_122;
					}
					if (A_0 == null)
					{
						num = 6;
					}
					else
					{
						num = 2;
					}
				}
			}
			IL_55:
			throw new ArgumentNullException(ClipboardData.b("፱ѳյ", a_));
			IL_A8:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ձᵳት౷ቹ", a_));
			IL_C5:
			throw new ArgumentNullException(ClipboardData.b("ᕱٳ᝵ࡷቹᕻᵽ", a_));
			IL_122:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᩱᅳήίቹࡻ", a_));
			IL_138:
			Matrix matrix = spr\u23A8.ᜀ(A_2, A_3, A_4);
			PointF pointF = spr\u23A8.ᜁ(new PointF(A_5, A_6), A_2);
			float val = pointF.X / A_1.Width;
			float val2 = pointF.Y / A_1.Height;
			float num2 = Math.Min(val, val2);
			matrix.Scale(num2, num2, MatrixOrder.Prepend);
			Matrix transform = A_2.Transform;
			A_2.Transform = matrix;
			this.ᜀ(A_0, A_2);
			A_2.Transform = transform;
			return num2;
		}
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x0015B4B4 File Offset: 0x0015A4B4
	private static Matrix ᜀ(Graphics A_0, float A_1, float A_2)
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
		Matrix transform = A_0.Transform;
		PointF pointF = spr\u23A8.ᜁ(new PointF(transform.OffsetX, transform.OffsetY), A_0);
		float[] elements = transform.Elements;
		Matrix matrix = new Matrix(elements[0], elements[1], elements[2], elements[3], pointF.X, pointF.Y);
		PointF pointF2 = spr\u23A8.ᜁ(new PointF(A_1, A_2), A_0);
		matrix.Translate(pointF2.X, pointF2.Y, MatrixOrder.Prepend);
		return matrix;
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0015B55C File Offset: 0x0015A55C
	private static PointF ᜁ(PointF A_0, Graphics A_1)
	{
		int a_ = 16;
		for (;;)
		{
			GraphicsUnit pageUnit = A_1.PageUnit;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_297;
				case 1:
					goto IL_25D;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (pageUnit)
						{
						case GraphicsUnit.World:
						case GraphicsUnit.Point:
							goto IL_2C7;
						case GraphicsUnit.Display:
							break;
						case GraphicsUnit.Pixel:
							A_0.X = (float)spr\u23C4.ᜄ((double)A_0.X, (double)A_1.DpiX);
							A_0.Y = (float)spr\u23C4.ᜄ((double)A_0.Y, (double)A_1.DpiY);
							num = 1;
							continue;
						case GraphicsUnit.Inch:
							A_0.X = (float)spr\u23C4.\u1717((double)A_0.X);
							A_0.Y = (float)spr\u23C4.\u1717((double)A_0.Y);
							num = 9;
							continue;
						case GraphicsUnit.Document:
							A_0.X = (float)spr\u23C4.ᜄ((double)A_0.X, 300.0);
							A_0.Y = (float)spr\u23C4.ᜄ((double)A_0.Y, 300.0);
							num = 3;
							continue;
						case GraphicsUnit.Millimeter:
							A_0.X = (float)spr\u23C4.\u1715((double)A_0.X);
							A_0.Y = (float)spr\u23C4.\u1715((double)A_0.Y);
							num = 0;
							continue;
						default:
							num = 10;
							continue;
						}
						break;
					}
					num = 11;
					continue;
				case 3:
					goto IL_1A5;
				case 4:
					goto IL_E5;
				case 5:
					num = 7;
					continue;
				case 6:
					goto IL_1B5;
				case 7:
					if (A_1.DpiY >= 300f)
					{
						num = 12;
						continue;
					}
					goto IL_A2;
				case 8:
					goto IL_201;
				case 9:
					goto IL_159;
				case 10:
					num = 6;
					continue;
				case 11:
					if (A_1.DpiX >= 300f)
					{
						num = 5;
						continue;
					}
					goto IL_A2;
				case 12:
					A_0.X = (float)spr\u23C4.ᜄ((double)A_0.X, 100.0);
					A_0.Y = (float)spr\u23C4.ᜄ((double)A_0.Y, 100.0);
					num = 8;
					continue;
				}
				break;
				IL_A2:
				A_0.X = (float)spr\u23C4.ᜄ((double)A_0.X, (double)A_1.DpiX);
				A_0.Y = (float)spr\u23C4.ᜄ((double)A_0.Y, (double)A_1.DpiY);
				num = 4;
			}
		}
		IL_E5:
		if (true)
		{
		}
		IL_159:
		IL_1A5:
		goto IL_2C7;
		IL_1B5:
		throw new InvalidOperationException(ClipboardData.b("⍵ᙷᅹቻᅽꒃ慎ﲋ憐뚕躟", a_));
		IL_201:
		IL_25D:
		IL_297:
		IL_2C7:
		return new PointF(A_0.X * A_1.PageScale, A_0.Y * A_1.PageScale);
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0015B854 File Offset: 0x0015A854
	private static PointF ᜀ(PointF A_0, Graphics A_1)
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
		PointF pointF = spr\u23A8.ᜁ(new PointF(1f, 1f), A_1);
		return new PointF(A_0.X / pointF.X, A_0.Y / pointF.Y);
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x0015B8C8 File Offset: 0x0015A8C8
	public override void ᜀ(spr\u23EB A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				PointF pointF;
				switch (num)
				{
				case 0:
					goto IL_22D;
					try
					{
						for (;;)
						{
							IL_22D:
							Font font = spr\u1CC9.ᜀ(A_0.ᜐ(), this.ᜇ);
							try
							{
								Brush brush;
								this.ᜀ.DrawString(A_0.\u1712(), font, brush, pointF, spr\u23A8.ᜆ);
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_297;
									case 2:
										((IDisposable)font).Dispose();
										num = 0;
										continue;
									}
									if (font == null)
									{
										break;
									}
									num = 2;
								}
								IL_297:;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_2B0;
							}
						}
						IL_2B0:
						if (false)
						{
						}
						goto IL_1FB;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							Brush brush;
							switch (num)
							{
							case 0:
								((IDisposable)brush).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_2F6;
							}
							if (brush == null)
							{
								break;
							}
							num = 0;
						}
						IL_2F6:;
					}
					goto IL_2F9;
				case 1:
					try
					{
						Pen pen;
						GraphicsPath graphicsPath;
						this.ᜀ.DrawPath(pen, graphicsPath);
						goto IL_2F9;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							Pen pen;
							switch (num)
							{
							case 0:
								((IDisposable)pen).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_10E;
							}
							if (pen == null)
							{
								break;
							}
							num = 0;
						}
						IL_10E:;
					}
					goto IL_111;
				case 2:
					if (spr\u2262.ᜀ(A_0.\u1714(), spr\u2262.ទ))
					{
						num = 3;
						continue;
					}
					goto IL_1FB;
				case 3:
				{
					spr\u253E a_ = new spr\u253E(A_0.\u1714());
					Brush brush = spr\u23C3.ᜀ(a_);
					num = 0;
					continue;
				}
				case 4:
				{
					GraphicsPath graphicsPath = new GraphicsPath();
					Font font2 = spr\u1CC9.ᜀ(A_0.ᜐ(), this.ᜇ);
					num = 8;
					continue;
				}
				case 5:
					if (spr\u2262.ᜀ(A_0.ᜌ(), spr\u2262.ទ))
					{
						num = 4;
						continue;
					}
					goto IL_2F9;
				case 7:
					return;
				case 8:
				{
					try
					{
						if (true)
						{
						}
						GraphicsPath graphicsPath;
						Font font2;
						graphicsPath.AddString(A_0.\u1712(), font2.FontFamily, (int)font2.Style, A_0.ᜐ().ᜐ(), pointF, spr\u23A8.ᜆ);
						goto IL_1D4;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							Font font2;
							switch (num)
							{
							case 1:
								((IDisposable)font2).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_1D0;
							}
							if (font2 == null)
							{
								break;
							}
							num = 1;
						}
						IL_1D0:;
					}
					return;
					IL_1D4:
					spr\u23F1 a_2 = new spr\u23F1(A_0.ᜌ());
					Pen pen = spr\u24C2.ᜀ(a_2);
					num = 1;
					continue;
				}
				}
				if (A_0.ᜐ().ᜐ() < 0.1f)
				{
					num = 7;
					continue;
				}
				IL_111:
				this.ᜁ(A_0);
				pointF = new PointF(A_0.\u170D(), A_0.ᜉ());
				num = 2;
				continue;
				IL_1FB:
				num = 5;
			}
			return;
			IL_2F9:
			this.ᜀ(A_0);
			return;
		}
		}
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x0015BC0C File Offset: 0x0015AC0C
	public override void ᜀ(spr\u24A6 A_0)
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
		this.ᜁ(A_0);
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x0015BC50 File Offset: 0x0015AC50
	public override void ᜁ(spr\u24A6 A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0015BC94 File Offset: 0x0015AC94
	public override void ᜀ(spr\u1B70 A_0)
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
		this.ᜁ(A_0);
		this.ᜂ.ᜀ(A_0);
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x0015BCE4 File Offset: 0x0015ACE4
	public override void ᜂ(spr\u1B70 A_0)
	{
		Pen pen;
		for (;;)
		{
			this.ᜂ.ᜂ(A_0);
			int num = 3;
			for (;;)
			{
				Brush brush;
				switch (num)
				{
				case 0:
					try
					{
						this.ᜀ.FillPath(brush, this.ᜂ.ᜀ());
						goto IL_6A;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								((IDisposable)brush).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_DA;
							}
							if (brush == null)
							{
								break;
							}
							num = 1;
						}
						IL_DA:;
					}
					goto IL_DD;
				case 1:
					goto IL_DD;
				case 2:
					goto IL_65;
				case 3:
					if (A_0.ᜅ() != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_6A;
				case 4:
					pen = spr\u24C2.ᜀ(A_0.ᜆ());
					num = 2;
					continue;
				case 5:
					if (A_0.ᜆ() != null)
					{
						num = 4;
						continue;
					}
					goto IL_16F;
				}
				break;
				IL_6A:
				num = 5;
				continue;
				IL_DD:
				brush = spr\u23C3.ᜀ(A_0.ᜅ());
				num = 0;
			}
		}
		IL_65:
		try
		{
			this.ᜀ.DrawPath(pen, this.ᜂ.ᜀ());
		}
		finally
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)pen).Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_16C;
					}
					if (pen == null)
					{
						break;
					}
					num = 0;
				}
				IL_16C:
				break;
			}
			}
		}
		IL_16F:
		this.ᜀ(A_0);
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0015BE84 File Offset: 0x0015AE84
	public override void ᜀ(spr\u1926 A_0)
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
		this.ᜂ.ᜀ(A_0);
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x0015BECC File Offset: 0x0015AECC
	public override void ᜁ(spr\u1926 A_0)
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
		this.ᜂ.ᜁ(A_0);
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x0015BF14 File Offset: 0x0015AF14
	public override void ᜀ(sprᴎ A_0)
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
		this.ᜂ.ᜀ(A_0);
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x0015BF5C File Offset: 0x0015AF5C
	public override void ᜀ(spr\u17F0 A_0)
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
		this.ᜂ.ᜀ(A_0);
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x0015BFA4 File Offset: 0x0015AFA4
	public override void ᜀ(spr\u1DB3 A_0)
	{
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜁ(A_0);
		}
		catch
		{
			this.ᜁ(spr\u1DB3.ᜀ(A_0));
		}
		if (true)
		{
		}
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x0015C008 File Offset: 0x0015B008
	private Image ᜀ(Image A_0, float A_1, float A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				Graphics graphics;
				int x;
				int y;
				int num2;
				int num3;
				int x2;
				int y2;
				int width;
				int height;
				Bitmap bitmap;
				switch (num)
				{
				case 0:
					goto IL_157;
				case 1:
					return A_0;
				case 2:
					try
					{
						graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics.DrawImage(A_0, new Rectangle(x, y, num2, num3), new Rectangle(x2, y2, width, height), GraphicsUnit.Pixel);
						graphics.Dispose();
						return bitmap;
					}
					finally
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
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_154;
								case 2:
									((IDisposable)graphics).Dispose();
									num = 1;
									continue;
								}
								if (graphics == null)
								{
									break;
								}
								num = 2;
							}
							IL_154:
							break;
						}
					}
					goto IL_157;
				case 3:
					num3 = 1;
					num = 10;
					continue;
				case 4:
					num2 = 1;
					num = 8;
					continue;
				case 5:
					if (num2 <= 0)
					{
						num = 4;
						continue;
					}
					goto IL_A4;
				case 6:
					if ((float)A_0.Height <= A_2)
					{
						num = 1;
						continue;
					}
					goto IL_1B0;
				case 7:
					if (num3 <= 0)
					{
						num = 3;
						continue;
					}
					goto IL_6F;
				case 8:
					goto IL_A4;
				case 10:
					goto IL_6F;
				}
				if (true)
				{
				}
				if ((float)A_0.Width <= A_1)
				{
					num = 0;
					continue;
				}
				goto IL_1B0;
				IL_6F:
				bitmap = new Bitmap(num2, num3);
				bitmap.SetResolution(A_0.HorizontalResolution, A_0.VerticalResolution);
				graphics = Graphics.FromImage(bitmap);
				num = 2;
				continue;
				IL_A4:
				num = 7;
				continue;
				IL_157:
				num = 6;
				continue;
				IL_1B0:
				width = A_0.Width;
				height = A_0.Height;
				x2 = 0;
				y2 = 0;
				x = 0;
				y = 0;
				num2 = (int)A_1;
				num3 = (int)A_2;
				num = 5;
			}
			return A_0;
		}
		}
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x0015C21C File Offset: 0x0015B21C
	private void ᜁ(spr\u1DB3 A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Image image = Image.FromStream(new MemoryStream(A_0.ᜅ()));
				int num = 8;
				for (;;)
				{
					sprᢕ sprᢕ;
					ImageAttributes imageAttributes;
					RectangleF rectangleF;
					switch (num)
					{
					case 0:
						try
						{
							for (;;)
							{
								IL_E8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_17A:
									goto IL_17C;
								default:
								{
									if (false)
									{
									}
									MemoryStream memoryStream;
									image.Save(memoryStream, ImageFormat.Png);
									memoryStream.Position = 0L;
									image = Image.FromStream(memoryStream);
									num = 1;
									break;
								}
								}
								for (;;)
								{
									IL_D1:
									switch (num)
									{
									case 0:
										image = this.ᜀ(image, (float)((int)((double)image.Width * 0.55)), (float)((int)((double)image.Height * 0.55)));
										num = 2;
										continue;
									case 1:
										if (image.HorizontalResolution > 300f)
										{
											num = 0;
											continue;
										}
										goto IL_17C;
									case 2:
										goto IL_17A;
									case 3:
										goto IL_188;
									}
									goto IL_E8;
								}
								IL_17C:
								num = 3;
								goto IL_D1;
							}
							IL_188:
							goto IL_2EB;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								MemoryStream memoryStream;
								switch (num)
								{
								case 1:
									((IDisposable)memoryStream).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_1C8;
								}
								if (memoryStream == null)
								{
									break;
								}
								num = 1;
							}
							IL_1C8:;
						}
						goto IL_1CB;
					case 1:
						if (!sprᢕ.ᜀ(sprᢕ))
						{
							num = 6;
							continue;
						}
						goto IL_1CB;
					case 2:
						return;
					case 3:
						imageAttributes = new ImageAttributes();
						imageAttributes.SetColorKey(A_0.ᜈ().ᜂ().ᜈ(), A_0.ᜈ().ᜁ().ᜈ());
						num = 4;
						continue;
					case 4:
						goto IL_218;
					case 5:
						image.Dispose();
						num = 2;
						continue;
					case 6:
						rectangleF = sprᢕ.ᜀ(rectangleF);
						num = 11;
						continue;
					case 7:
						if (image == null)
						{
							num = 5;
							continue;
						}
						return;
					case 8:
						if (image is Metafile)
						{
							num = 10;
							continue;
						}
						goto IL_2EB;
					case 9:
						if (A_0.ᜈ() != null)
						{
							num = 3;
							continue;
						}
						goto IL_218;
					case 10:
					{
						MemoryStream memoryStream = new MemoryStream();
						num = 0;
						continue;
					}
					case 11:
						goto IL_1CB;
					}
					break;
					IL_1CB:
					PointF[] destPoints;
					GraphicsUnit srcUnit;
					this.ᜀ.DrawImage(image, destPoints, rectangleF, srcUnit, imageAttributes);
					num = 7;
					continue;
					IL_218:
					if (true)
					{
					}
					srcUnit = GraphicsUnit.Pixel;
					rectangleF = image.GetBounds(ref srcUnit);
					RectangleF rectangleF2 = A_0.ᜉ();
					destPoints = new PointF[]
					{
						rectangleF2.Location,
						new PointF(rectangleF2.X + rectangleF2.Width, rectangleF2.Y),
						new PointF(rectangleF2.X, rectangleF2.Y + rectangleF2.Height)
					};
					num = 1;
					continue;
					IL_2EB:
					sprᢕ = A_0.ᜆ();
					imageAttributes = null;
					num = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x0015C558 File Offset: 0x0015B558
	private Image ᜀ(Image A_0, RectangleF A_1, RectangleF A_2, GraphicsUnit A_3, ImageAttributes A_4)
	{
		switch (0)
		{
		default:
		{
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap((int)A_1.Width, (int)A_1.Height);
				goto IL_F8;
			}
			catch
			{
				bitmap = new Bitmap(A_0);
				goto IL_F8;
			}
			goto IL_130;
			for (;;)
			{
				IL_F8:
				Graphics graphics = Graphics.FromImage(bitmap);
				try
				{
					PointF[] destPoints = new PointF[]
					{
						A_1.Location,
						new PointF(A_1.X + A_1.Width, A_1.Y),
						new PointF(A_1.X, A_1.Y + A_1.Height)
					};
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.DrawImage(A_0, destPoints, A_2, A_3, A_4);
					graphics.Dispose();
					break;
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E4;
						case 1:
							goto IL_F5;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E4;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						}
						if (graphics != null)
						{
							num = 0;
							continue;
						}
						break;
						IL_E4:
						((IDisposable)graphics).Dispose();
						num = 1;
					}
					IL_F5:;
				}
			}
			IL_130:
			if (true)
			{
			}
			return bitmap;
		}
		}
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x0015C6BC File Offset: 0x0015B6BC
	private void ᜁ(spr\u24F9 A_0)
	{
		for (;;)
		{
			this.ᜅ.Push(this.ᜀ.Save());
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜄ.Push(this.ᜀ.Clip.Clone());
					A_0.ᜁ().ᜀ(this.ᜂ);
					this.ᜀ.SetClip(this.ᜂ.ᜀ(), CombineMode.Intersect);
					if (true)
					{
					}
					num = 4;
					continue;
				case 1:
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_1BA;
							case 2:
								goto IL_1D2;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1AD;
								default:
									if (false)
									{
									}
									num = 5;
									continue;
								}
								break;
							case 4:
								this.ᜀ.TranslateTransform(this.ᜁ.X, this.ᜁ.Y, MatrixOrder.Prepend);
								num = 1;
								continue;
							case 5:
								if (this.ᜀ.Transform.IsIdentity)
								{
									goto IL_1AD;
								}
								goto IL_1BA;
							}
							if (!this.ᜁ.IsEmpty)
							{
								num = 3;
								continue;
							}
							goto IL_1BA;
							IL_1AD:
							num = 4;
							continue;
							IL_1BA:
							Matrix matrix;
							this.ᜀ.MultiplyTransform(matrix, MatrixOrder.Prepend);
							num = 2;
						}
						IL_1D2:
						goto IL_5D;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							Matrix matrix;
							switch (num)
							{
							case 0:
								goto IL_20E;
							case 2:
								((IDisposable)matrix).Dispose();
								num = 0;
								continue;
							}
							if (matrix == null)
							{
								break;
							}
							num = 2;
						}
						IL_20E:;
					}
					return;
				case 2:
					if (spr\u2066.ᜀ(A_0))
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					if (spr\u2066.ᜁ(A_0))
					{
						num = 5;
						continue;
					}
					goto IL_5D;
				case 4:
					return;
				case 5:
				{
					this.ᜃ.Push(this.ᜀ.Transform.Clone());
					Matrix matrix = spr\u20AD.ᜁ(A_0.ᜀ());
					num = 1;
					continue;
				}
				}
				break;
				IL_5D:
				num = 2;
			}
		}
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x0015C8EC File Offset: 0x0015B8EC
	private void ᜀ(spr\u24F9 A_0)
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
					goto IL_C5;
				default:
					if (false)
					{
					}
					this.ᜀ.Transform = (Matrix)this.ᜃ.Pop();
					num = 3;
					continue;
				}
				break;
			case 2:
				goto IL_C5;
			case 3:
				goto IL_67;
			case 4:
				this.ᜀ.Clip = (Region)this.ᜄ.Pop();
				num = 2;
				continue;
			case 5:
				if (spr\u2066.ᜁ(A_0))
				{
					num = 0;
					continue;
				}
				goto IL_C7;
			}
			if (spr\u2066.ᜀ(A_0))
			{
				num = 4;
				continue;
			}
			IL_69:
			num = 5;
			continue;
			IL_C5:
			goto IL_69;
		}
		IL_67:
		IL_C7:
		if (true)
		{
		}
		this.ᜀ.Restore((GraphicsState)this.ᜅ.Pop());
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x0015C9E4 File Offset: 0x0015B9E4
	static spr\u23A8()
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
		spr\u23A8.ᜆ = new StringFormat(StringFormat.GenericTypographic);
		spr\u23A8.ᜆ.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
	}

	// Token: 0x040019B4 RID: 6580
	private new Graphics ᜀ;

	// Token: 0x040019B5 RID: 6581
	private new PointF ᜁ;

	// Token: 0x040019B6 RID: 6582
	private new readonly spr\u1CC2 ᜂ = new spr\u1CC2();

	// Token: 0x040019B7 RID: 6583
	private readonly Stack ᜃ = new Stack();

	// Token: 0x040019B8 RID: 6584
	private readonly Stack ᜄ = new Stack();

	// Token: 0x040019B9 RID: 6585
	private readonly Stack ᜅ = new Stack();

	// Token: 0x040019BA RID: 6586
	private static readonly StringFormat ᜆ;

	// Token: 0x040019BB RID: 6587
	private spr\u2058 ᜇ;
}
