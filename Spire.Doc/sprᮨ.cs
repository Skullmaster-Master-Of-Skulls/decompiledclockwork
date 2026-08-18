using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Fields.Shape.Ps;

// Token: 0x02000216 RID: 534
internal class spr\u1BA8
{
	// Token: 0x0600191D RID: 6429 RVA: 0x001881B0 File Offset: 0x001871B0
	internal spr\u1BA8(spr\u1937 A_0, SizeF A_1, sprᾔ A_2)
	{
		this.ᜀ = A_0;
		this.ᜋ = (A_1.IsEmpty ? this.ᜀ.ᝡ() : A_1);
		this.ᜁ = A_2.ᜂ();
		SizeF sizeF = spr\u1BA8.ᜀ(A_0, A_2.ᜃ(), A_2.ᜁ());
		this.ᜂ = sizeF.Width;
		this.ᜃ = sizeF.Height;
		this.ᜄ = A_2.ᜄ();
		this.ᜇ = LimoStretchType.None;
		this.ᜈ = 0f;
		this.ᜉ = null;
		this.ᜅ = A_0.\u1717();
		this.ᜆ = A_0.ᜬ();
		this.ᜀ(false);
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x00188274 File Offset: 0x00187274
	internal spr\u1BA8(spr\u1937 A_0, SizeF A_1, sprᾔ A_2, bool A_3)
	{
		this.ᜀ = A_0;
		this.ᜋ = (A_1.IsEmpty ? this.ᜀ.ᝡ() : A_1);
		this.ᜁ = A_2.ᜂ();
		SizeF sizeF = spr\u1BA8.ᜀ(A_0, A_2.ᜃ(), A_2.ᜁ());
		this.ᜂ = sizeF.Width;
		this.ᜃ = sizeF.Height;
		this.ᜄ = A_2.ᜄ();
		this.ᜇ = LimoStretchType.None;
		this.ᜈ = 0f;
		this.ᜉ = null;
		this.ᜅ = A_0.\u1717();
		this.ᜆ = A_0.ᜬ();
		this.ᜀ(A_3);
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x00188338 File Offset: 0x00187338
	internal static SizeF ᜀ(sprᩍ A_0, float A_1, float A_2)
	{
		if (spr\u1BA8.ᜀ(A_0))
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
				return new SizeF(A_2, A_1);
			}
		}
		if (true)
		{
		}
		return new SizeF(A_1, A_2);
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x0018838C File Offset: 0x0018738C
	internal static bool ᜀ(sprᩍ A_0)
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
		return spr\u1BA8.ᜁ((float)A_0.ម());
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x001883D4 File Offset: 0x001873D4
	internal static bool ᜁ(float A_0)
	{
		for (;;)
		{
			A_0 = spr\u1BA8.ᜀ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					if (A_0 >= 135f)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					return true;
				case 2:
					if (A_0 >= 45f)
					{
						num = 5;
						continue;
					}
					goto IL_71;
				case 3:
					if (A_0 >= 225f)
					{
						num = 4;
						continue;
					}
					return false;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_62;
					}
					break;
				case 5:
					num = 1;
					continue;
				}
				break;
				IL_71:
				num = 3;
			}
		}
		IL_62:
		if (false)
		{
		}
		return A_0 < 315f;
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x0018849C File Offset: 0x0018749C
	internal static float ᜀ(float A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 < 0f)
				{
					num = 5;
					continue;
				}
				return A_0;
			case 1:
				return A_0;
			case 2:
				if (true)
				{
				}
				A_0 %= 360f;
				num = 4;
				continue;
			case 4:
				goto IL_A4;
			case 5:
				A_0 += 360f;
				num = 1;
				continue;
			}
			if (Math.Abs(A_0) > 360f)
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
					continue;
				}
			}
			IL_6E:
			num = 0;
			continue;
			IL_A4:
			goto IL_6E;
		}
		return A_0;
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x0018855C File Offset: 0x0018755C
	internal PointF ᜁ()
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
		return spr\u1BA8.ᜀ(this.ᜀ, this.ᜋ, this.ᜂ, this.ᜃ);
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x001885B4 File Offset: 0x001875B4
	private static PointF ᜀ(sprᩍ A_0, SizeF A_1, float A_2, float A_3)
	{
		switch (0)
		{
		default:
		{
			float num3;
			float num4;
			for (;;)
			{
				int num = A_0.\u1776();
				int num2 = A_0.ឍ();
				num3 = A_1.Width / (float)num;
				num4 = A_1.Height / (float)num2;
				int num5 = 5;
				for (;;)
				{
					switch (num5)
					{
					case 0:
					{
						spr\u1937 spr_u;
						if (spr_u.\u1717() == 0)
						{
							num5 = 1;
							continue;
						}
						goto IL_123;
					}
					case 1:
						num5 = 3;
						continue;
					case 2:
						goto IL_AF;
					case 3:
					{
						spr\u1937 spr_u;
						if (spr_u.ᜬ() != 0)
						{
							num5 = 9;
							continue;
						}
						goto IL_AF;
					}
					case 4:
						goto IL_F0;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_181;
						default:
							if (false)
							{
							}
							if (A_0 is spr\u1937)
							{
								num5 = 10;
								continue;
							}
							goto IL_AF;
						}
						break;
					case 6:
						if (num3 != 0f)
						{
							num5 = 7;
							continue;
						}
						num5 = 8;
						continue;
					case 7:
						num5 = 4;
						continue;
					case 8:
						goto IL_181;
					case 9:
						goto IL_123;
					case 10:
					{
						spr\u1937 spr_u = (spr\u1937)A_0;
						num5 = 0;
						continue;
					}
					}
					break;
					IL_AF:
					if (true)
					{
					}
					num3 *= A_2;
					num4 *= A_3;
					num5 = 6;
					continue;
					IL_123:
					float num6 = Math.Min(num3, num4);
					num3 = num6;
					num4 = num6;
					num5 = 2;
				}
			}
			IL_F0:
			float x = num3;
			IL_F3:
			return new PointF(x, (num4 == 0f) ? 1f : num4);
			IL_181:
			x = 1f;
			goto IL_F3;
		}
		}
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x00188754 File Offset: 0x00187754
	internal static SizeF ᜀ(SizeF A_0, sprᩍ A_1, float A_2, float A_3)
	{
		if (!A_0.IsEmpty)
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
				PointF pointF = spr\u1BA8.ᜀ(A_1, A_1.ᝡ(), A_2, A_3);
				return new SizeF(A_0.Width / pointF.X, A_0.Height / pointF.Y);
			}
			}
		}
		if (true)
		{
		}
		return A_0;
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x001887D0 File Offset: 0x001877D0
	internal void ᜀ(PointF[] A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				float num2;
				int num3;
				float num4;
				switch (num)
				{
				case 0:
					if (num2 > (float)this.ᜆ)
					{
						num = 12;
						continue;
					}
					goto IL_157;
				case 1:
					if (this.ᜇ == LimoStretchType.AlongX)
					{
						num = 11;
						continue;
					}
					num = 0;
					continue;
				case 2:
				{
					if (num3 >= A_0.Length)
					{
						num = 10;
						continue;
					}
					PointF pointF = A_0[num3];
					num4 = pointF.X;
					num2 = pointF.Y;
					num = 1;
					continue;
				}
				case 3:
					if (A_1)
					{
						goto IL_112;
					}
					goto IL_1F1;
				case 4:
					goto IL_157;
				case 5:
					goto IL_157;
				case 6:
					goto IL_83;
				case 7:
					num4 += this.ᜈ;
					num = 5;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_112;
					default:
						if (false)
						{
						}
						num3 = 0;
						if (true)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 9:
					goto IL_83;
				case 10:
					goto IL_100;
				case 11:
					num = 15;
					continue;
				case 12:
					num2 += this.ᜈ;
					num = 4;
					continue;
				case 13:
					goto IL_11E;
				case 15:
					if (num4 > (float)this.ᜅ)
					{
						num = 7;
						continue;
					}
					goto IL_157;
				}
				if (this.ᜇ != LimoStretchType.None)
				{
					num = 8;
					continue;
				}
				goto IL_100;
				IL_83:
				num = 2;
				continue;
				IL_100:
				num = 3;
				continue;
				IL_112:
				num = 13;
				continue;
				IL_157:
				A_0[num3] = new PointF(num4, num2);
				num3++;
				num = 6;
			}
			IL_11E:
			this.ᜊ.ᜀ(A_0);
			return;
			IL_1F1:
			this.ᜉ.ᜀ(A_0);
			return;
		}
		}
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x001889DC File Offset: 0x001879DC
	internal spr\u25FD ᜀ(Size A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			float x;
			float y;
			spr\u25FD spr_u25FD;
			for (;;)
			{
				PointF[] array = new PointF[]
				{
					PointF.Empty,
					new PointF((float)this.ᜀ.\u1776(), 0f),
					new PointF((float)this.ᜀ.\u1776(), (float)this.ᜀ.ឍ()),
					new PointF(0f, (float)this.ᜀ.ឍ())
				};
				this.ᜀ(array, false);
				RectangleF rectangleF = spr\u1BA8.ᜀ(array);
				x = rectangleF.X;
				y = rectangleF.Y;
				float width = rectangleF.Width;
				float height = rectangleF.Height;
				int num = 12;
				for (;;)
				{
					float num2;
					float num3;
					double num4;
					switch (num)
					{
					case 0:
					{
						RectangleF rectangleF2 = this.ᜀ.ᜀ(new RectangleF(0f, 0f, this.ᜋ.Width, this.ᜋ.Height));
						width = rectangleF2.Width;
						height = rectangleF2.Height;
						num = 8;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D1;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 2:
						if (num2 > num3)
						{
							num = 10;
							continue;
						}
						num2 = num3;
						num = 7;
						continue;
					case 3:
						goto IL_2D1;
					case 4:
						if (num4 != 0.0)
						{
							num = 11;
							continue;
						}
						goto IL_2F9;
					case 5:
						if (A_1)
						{
							num = 9;
							continue;
						}
						goto IL_144;
					case 6:
						if (true)
						{
						}
						if (this.ᜀ.\u1719())
						{
							num = 1;
							continue;
						}
						goto IL_2F9;
					case 7:
						goto IL_144;
					case 8:
						goto IL_1B3;
					case 9:
						num = 2;
						continue;
					case 10:
						num3 = num2;
						num = 3;
						continue;
					case 11:
						x = array[0].X;
						y = array[0].Y;
						spr_u25FD.ᜀ((float)num4, MatrixOrder.Append);
						num = 13;
						continue;
					case 12:
						if (this.ᜀ.\u1719())
						{
							num = 0;
							continue;
						}
						goto IL_1B3;
					case 13:
						goto IL_221;
					}
					break;
					IL_144:
					spr_u25FD = new spr\u25FD();
					spr_u25FD.ᜁ(num2, num3, MatrixOrder.Prepend);
					num4 = (double)this.ᜄ + this.ᜀ.ម();
					num = 6;
					continue;
					IL_2D1:
					goto IL_144;
					IL_1B3:
					num2 = width / (float)A_0.Width;
					num3 = height / (float)A_0.Height;
					num = 5;
				}
			}
			IL_221:
			IL_2F9:
			spr_u25FD.ᜀ(x, y, MatrixOrder.Append);
			return spr_u25FD;
		}
		}
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x00188CF0 File Offset: 0x00187CF0
	internal static RectangleF ᜀ(PointF[] A_0)
	{
		switch (0)
		{
		default:
		{
			float x;
			float num;
			float y;
			float num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				for (;;)
				{
					x = A_0[0].X;
					num = x;
					y = A_0[0].Y;
					num2 = y;
					int num3 = 0;
					int num4 = 10;
					for (;;)
					{
						switch (num4)
						{
						case 0:
						{
							PointF pointF;
							if (pointF.Y > num2)
							{
								num4 = 5;
								continue;
							}
							num4 = 4;
							continue;
						}
						case 1:
						{
							PointF pointF;
							if (pointF.X < x)
							{
								num4 = 7;
								continue;
							}
							goto IL_1C2;
						}
						case 2:
						{
							PointF pointF;
							if (pointF.X > num)
							{
								num4 = 8;
								continue;
							}
							if (true)
							{
							}
							num4 = 1;
							continue;
						}
						case 3:
							goto IL_F1;
						case 4:
						{
							PointF pointF;
							if (pointF.Y < y)
							{
								num4 = 15;
								continue;
							}
							goto IL_F1;
						}
						case 5:
						{
							PointF pointF;
							num2 = pointF.Y;
							num4 = 3;
							continue;
						}
						case 6:
							goto IL_F1;
						case 7:
						{
							PointF pointF;
							x = pointF.X;
							num4 = 12;
							continue;
						}
						case 8:
						{
							PointF pointF;
							num = pointF.X;
							num4 = 14;
							continue;
						}
						case 9:
						{
							if (num3 >= A_0.Length)
							{
								num4 = 13;
								continue;
							}
							PointF pointF = A_0[num3];
							num4 = 2;
							continue;
						}
						case 10:
							goto IL_15F;
						case 11:
							goto IL_15F;
						case 12:
							goto IL_1C2;
						case 13:
							goto IL_17E;
						case 14:
							goto IL_1C2;
						case 15:
						{
							PointF pointF;
							y = pointF.Y;
							num4 = 6;
							continue;
						}
						}
						break;
						IL_F1:
						num3++;
						num4 = 11;
						continue;
						IL_15F:
						num4 = 9;
						continue;
						IL_1C2:
						num4 = 0;
					}
				}
				IL_17E:
				break;
			}
			return new RectangleF(x, y, num - x, num2 - y);
		}
		}
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x00188EF8 File Offset: 0x00187EF8
	private void ᜀ(bool A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			float num2;
			float num3;
			float num5;
			float num9;
			int num11;
			int num12;
			float a_;
			for (;;)
			{
				if (true)
				{
				}
				int num4;
				int num6;
				int num7;
				int num8;
				switch (num)
				{
				case 0:
					if (num2 > num3)
					{
						num = 11;
						continue;
					}
					this.ᜇ = LimoStretchType.AlongY;
					this.ᜈ = (float)num4 * (num3 / num2 - 1f);
					num5 += this.ᜈ * 0.5f;
					num = 3;
					continue;
				case 2:
					num = 16;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_104;
					default:
						if (false)
						{
						}
						goto IL_104;
					}
					break;
				case 4:
					goto IL_104;
				case 5:
					num6 = this.ᜀ.ᝍ();
					goto IL_141;
				case 6:
					num6 = 0;
					goto IL_141;
				case 7:
					num7 = this.ᜀ.ឈ();
					goto IL_194;
				case 8:
					num = 7;
					continue;
				case 9:
					goto IL_15F;
				case 10:
					goto IL_123;
				case 11:
					this.ᜇ = LimoStretchType.AlongX;
					this.ᜈ = (float)num8 * (num2 / num3 - 1f);
					num9 += this.ᜈ * 0.5f;
					num = 4;
					continue;
				case 12:
					if (!A_0)
					{
						num = 8;
						continue;
					}
					num = 14;
					continue;
				case 13:
					if (this.ᜅ == 0)
					{
						num = 2;
						continue;
					}
					goto IL_15F;
				case 14:
					num7 = 0;
					goto IL_194;
				case 15:
					num = 5;
					continue;
				case 16:
					if (this.ᜆ != 0)
					{
						num = 9;
						continue;
					}
					goto IL_278;
				}
				if (!A_0)
				{
					num = 15;
					continue;
				}
				num = 6;
				continue;
				IL_104:
				float num10 = Math.Min(num2, num3);
				num2 = num10;
				num3 = num10;
				num = 10;
				continue;
				IL_141:
				num11 = num6;
				num = 12;
				continue;
				IL_15F:
				num = 0;
				continue;
				IL_194:
				num12 = num7;
				num8 = this.ᜀ.\u1776();
				num4 = this.ᜀ.ឍ();
				num9 = (float)num8 * 0.5f;
				num5 = (float)num4 * 0.5f;
				num2 = this.ᜋ.Width / (float)num8;
				num3 = this.ᜋ.Height / (float)num4;
				a_ = (float)this.ᜀ.ម();
				num = 13;
			}
			IL_123:
			IL_278:
			this.ᜀ(num2, num3, num9, num5, (float)num11, (float)num12);
			this.ᜉ = new spr\u25FD();
			this.ᜉ.ᜀ((float)(-(float)num11), (float)(-(float)num12), MatrixOrder.Append);
			this.ᜉ.ᜀ(-num9, -num5, MatrixOrder.Append);
			num2 *= this.ᜂ;
			num3 *= this.ᜃ;
			this.ᜊ = this.ᜉ.ᜎ();
			this.ᜉ.ᜁ(num2, num3, MatrixOrder.Append);
			this.ᜊ.ᜁ(num2, num3, MatrixOrder.Append);
			this.ᜉ.ᜀ(a_, MatrixOrder.Append);
			spr\u1BA8.ᜀ(this.ᜀ, this.ᜉ);
			this.ᜉ.ᜀ(this.ᜄ, MatrixOrder.Append);
			this.ᜉ.ᜀ(num9 * num2, num5 * num3, MatrixOrder.Append);
			this.ᜊ.ᜀ(num9 * num2, num5 * num3, MatrixOrder.Append);
			return;
		}
		}
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x00189264 File Offset: 0x00188264
	private void ᜀ(float A_0, float A_1, float A_2, float A_3, float A_4, float A_5)
	{
		PointF[] array;
		spr\u25FD spr_u25FD;
		for (;;)
		{
			IL_14:
			array = new PointF[]
			{
				new PointF(A_2, A_3)
			};
			spr_u25FD = new spr\u25FD();
			spr_u25FD.ᜀ(-A_4, -A_5, MatrixOrder.Append);
			spr_u25FD.ᜁ(A_0, A_1, MatrixOrder.Append);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 1:
					if (true)
					{
					}
					if (!this.ᜀ.ᝏ())
					{
						num = 2;
						continue;
					}
					goto IL_AA;
				case 2:
					spr_u25FD.ᜀ((float)this.ᜀ.\u177A(), (float)this.ᜀ.ᝣ(), MatrixOrder.Append);
					num = 0;
					continue;
				}
				goto IL_14;
			}
			IL_AA:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_C0;
			}
			IL_9E:
			goto IL_AA;
		}
		IL_C0:
		if (false)
		{
		}
		spr_u25FD.ᜀ(this.ᜁ, MatrixOrder.Append);
		spr_u25FD.ᜀ(-A_2 * A_0 * this.ᜂ, -A_3 * A_1 * this.ᜃ, MatrixOrder.Append);
		spr_u25FD.ᜀ(array);
		this.ᜌ = array[0];
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x00189380 File Offset: 0x00188380
	private static void ᜀ(sprᩍ A_0, spr\u25FD A_1)
	{
		for (;;)
		{
			for (;;)
			{
				FlipOrientation flipOrientation = A_0.ᝑ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch (flipOrientation)
						{
						case FlipOrientation.None:
							return;
						case FlipOrientation.Horizontal:
							goto IL_55;
						case FlipOrientation.Vertical:
							A_1.ᜁ(1f, -1f, MatrixOrder.Append);
							num = 2;
							continue;
						case FlipOrientation.Both:
							goto IL_67;
						default:
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_53;
					case 2:
						return;
					}
					break;
				}
			}
			IL_53:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8F;
			}
		}
		IL_55:
		A_1.ᜁ(-1f, 1f, MatrixOrder.Append);
		return;
		IL_67:
		A_1.ᜁ(-1f, -1f, MatrixOrder.Append);
		return;
		IL_8F:
		if (false)
		{
		}
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x00189444 File Offset: 0x00188444
	internal static spr\u25FD ᜀ(sprᩍ A_0, sprᾔ A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u25FD spr_u25FD;
			float num9;
			float num10;
			for (;;)
			{
				int num = A_0.ᝍ();
				int num2 = A_0.ឈ();
				float num3 = (float)A_0.\u177D();
				float num4 = (float)A_0.ន();
				float num5 = num3 * 0.5f;
				float num6 = num4 * 0.5f;
				spr_u25FD = new spr\u25FD();
				spr_u25FD.ᜀ((float)(-(float)num), (float)(-(float)num2), MatrixOrder.Append);
				spr_u25FD.ᜀ(-num5, -num6, MatrixOrder.Append);
				FlipOrientation flipOrientation = A_0.ᝑ();
				int num7 = 10;
				for (;;)
				{
					float num8;
					bool flag;
					bool flag2;
					switch (num7)
					{
					case 0:
						goto IL_1CB;
					case 1:
						if (Math.Abs(num8 % 180f) == 90f)
						{
							num7 = 6;
							continue;
						}
						goto IL_E6;
					case 2:
						goto IL_175;
					case 3:
						flag = false;
						goto IL_188;
					case 4:
						if (A_0.វ())
						{
							goto IL_E6;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_153;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num7 = 8;
							continue;
						}
						break;
					case 5:
						goto IL_1CB;
					case 6:
						num7 = 4;
						continue;
					case 7:
						flag = !(A_0.ParentObject is sprᢋ);
						goto IL_188;
					case 8:
						num7 = 7;
						continue;
					case 9:
						num7 = 14;
						continue;
					case 10:
						switch (flipOrientation)
						{
						case FlipOrientation.None:
							goto IL_1CB;
						case FlipOrientation.Horizontal:
							spr_u25FD.ᜁ(-1f, 1f, MatrixOrder.Append);
							num7 = 11;
							continue;
						case FlipOrientation.Vertical:
							spr_u25FD.ᜁ(1f, -1f, MatrixOrder.Append);
							num7 = 0;
							continue;
						case FlipOrientation.Both:
							spr_u25FD.ᜁ(-1f, -1f, MatrixOrder.Append);
							goto IL_153;
						default:
							num7 = 9;
							continue;
						}
						break;
					case 11:
						goto IL_1CB;
					case 12:
						num9 = num6;
						num10 = num5;
						num7 = 2;
						continue;
					case 13:
						if (flag2)
						{
							num7 = 12;
							continue;
						}
						num9 = num5;
						num10 = num6;
						num7 = 15;
						continue;
					case 14:
						goto IL_1CB;
					case 15:
						goto IL_1C6;
					}
					break;
					IL_E6:
					num7 = 3;
					continue;
					IL_153:
					num7 = 5;
					continue;
					IL_188:
					flag2 = flag;
					num7 = 13;
					continue;
					IL_1CB:
					num8 = (float)A_0.ម() + A_1.ᜄ();
					spr_u25FD.ᜁ(A_1.ᜃ(), A_1.ᜁ(), MatrixOrder.Append);
					spr_u25FD.ᜀ(num8, MatrixOrder.Append);
					num7 = 1;
				}
			}
			IL_175:
			IL_1C6:
			spr_u25FD.ᜀ(num9 * A_1.ᜃ(), num10 * A_1.ᜁ(), MatrixOrder.Append);
			return spr_u25FD;
		}
		}
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x00189704 File Offset: 0x00188704
	internal static spr\u25FD ᜀ(sprᩍ A_0, SizeF A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u25FD spr_u25FD;
			for (;;)
			{
				int num = A_0.ᝍ();
				int num2 = A_0.ឈ();
				int num3 = A_0.\u1776();
				int num4 = A_0.ឍ();
				float num5 = (float)num3 * 0.5f;
				float num6 = (float)num4 * 0.5f;
				int num7 = 8;
				for (;;)
				{
					float num8;
					float num9;
					FlipOrientation flipOrientation;
					switch (num7)
					{
					case 0:
						if (!A_0.ᝏ())
						{
							num7 = 13;
							continue;
						}
						return spr_u25FD;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B8;
						default:
							if (false)
							{
							}
							goto IL_14A;
						}
						break;
					case 2:
						goto IL_1FF;
					case 3:
						if (true)
						{
						}
						num8 = A_1.Height / (float)num4;
						num9 = A_1.Width / (float)num3;
						num7 = 2;
						continue;
					case 4:
						switch (flipOrientation)
						{
						case FlipOrientation.None:
							goto IL_14A;
						case FlipOrientation.Horizontal:
							spr_u25FD.ᜁ(-1f, 1f, MatrixOrder.Append);
							num7 = 12;
							continue;
						case FlipOrientation.Vertical:
							spr_u25FD.ᜁ(1f, -1f, MatrixOrder.Append);
							goto IL_B8;
						case FlipOrientation.Both:
							spr_u25FD.ᜁ(-1f, -1f, MatrixOrder.Append);
							num7 = 1;
							continue;
						default:
							num7 = 14;
							continue;
						}
						break;
					case 5:
						if (A_2)
						{
							num7 = 3;
							continue;
						}
						goto IL_1FF;
					case 6:
						goto IL_105;
					case 7:
						goto IL_14A;
					case 8:
						if (A_1.IsEmpty)
						{
							num7 = 9;
							continue;
						}
						goto IL_105;
					case 9:
						A_1 = A_0.ᝡ();
						num7 = 6;
						continue;
					case 10:
						return spr_u25FD;
					case 11:
						goto IL_14A;
					case 12:
						goto IL_14A;
					case 13:
						spr_u25FD.ᜀ((float)A_0.\u177A(), (float)A_0.ᝣ(), MatrixOrder.Append);
						num7 = 10;
						continue;
					case 14:
						num7 = 7;
						continue;
					}
					break;
					IL_B8:
					num7 = 11;
					continue;
					IL_105:
					num8 = A_1.Width / (float)num3;
					num9 = A_1.Height / (float)num4;
					num7 = 5;
					continue;
					IL_14A:
					spr_u25FD.ᜁ(num8, num9, MatrixOrder.Append);
					spr_u25FD.ᜀ((float)A_0.ម(), MatrixOrder.Append);
					spr_u25FD.ᜀ(num5 * num8, num6 * num9, MatrixOrder.Append);
					num7 = 0;
					continue;
					IL_1FF:
					spr_u25FD = new spr\u25FD();
					spr_u25FD.ᜀ((float)(-(float)num), (float)(-(float)num2), MatrixOrder.Append);
					spr_u25FD.ᜀ(-num5, -num6, MatrixOrder.Append);
					flipOrientation = A_0.ᝑ();
					num7 = 4;
				}
			}
			return spr_u25FD;
		}
		}
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x001899B4 File Offset: 0x001889B4
	internal PointF ᜀ()
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
		return this.ᜌ;
	}

	// Token: 0x04001CDD RID: 7389
	private readonly spr\u1937 ᜀ;

	// Token: 0x04001CDE RID: 7390
	private readonly spr\u25FD ᜁ;

	// Token: 0x04001CDF RID: 7391
	private readonly float ᜂ;

	// Token: 0x04001CE0 RID: 7392
	private readonly float ᜃ;

	// Token: 0x04001CE1 RID: 7393
	private readonly float ᜄ;

	// Token: 0x04001CE2 RID: 7394
	private readonly int ᜅ;

	// Token: 0x04001CE3 RID: 7395
	private readonly int ᜆ;

	// Token: 0x04001CE4 RID: 7396
	private LimoStretchType ᜇ;

	// Token: 0x04001CE5 RID: 7397
	private float ᜈ;

	// Token: 0x04001CE6 RID: 7398
	private spr\u25FD ᜉ;

	// Token: 0x04001CE7 RID: 7399
	private spr\u25FD ᜊ;

	// Token: 0x04001CE8 RID: 7400
	private SizeF ᜋ = SizeF.Empty;

	// Token: 0x04001CE9 RID: 7401
	private PointF ᜌ;
}
