using System;
using System.Drawing;
using Spire.Doc.Fields.Shape.Ps;

// Token: 0x0200040A RID: 1034
internal class spr᧑
{
	// Token: 0x06003981 RID: 14721 RVA: 0x0035857C File Offset: 0x0035757C
	internal spr᧑(BorderGridType A_0)
	{
		this.ᜁ = A_0;
		this.ᜂ = new spr\u2515();
	}

	// Token: 0x06003982 RID: 14722 RVA: 0x003585A4 File Offset: 0x003575A4
	internal void ᜄ()
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
			if (this.ᜄ != null)
			{
				this.ᜄ = this.ᜇ;
				this.ᜅ = this.ᜄ.\u1713();
				this.ᜆ = null;
				return;
			}
			break;
		}
	}

	// Token: 0x06003983 RID: 14723 RVA: 0x00358610 File Offset: 0x00357610
	internal void ᜁ(RectangleF A_0, spr\u2587 A_1, spr\u2587 A_2, spr\u2587 A_3, spr\u2587 A_4)
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
		RectangleF rectangleF = spr᧑.ᜀ(A_0, A_1, A_2, A_3, A_4);
		PointF a_ = new PointF(rectangleF.X, rectangleF.Y);
		this.ᜁ(a_);
		this.ᜀ(a_);
		this.ᜃ(rectangleF.Right, A_3);
		this.ᜂ(rectangleF.X, A_3);
		this.ᜁ(rectangleF.Right, A_3);
		this.ᜀ(rectangleF.Height, A_1);
		spr᠐ spr᠐ = this.ᜀ(A_4, A_2);
		this.ᜄ = this.ᜅ;
		this.ᜅ = this.ᜄ.\u1713();
		this.ᜆ = spr᠐;
	}

	// Token: 0x06003984 RID: 14724 RVA: 0x003586E4 File Offset: 0x003576E4
	private static RectangleF ᜀ(RectangleF A_0, spr\u2587 A_1, spr\u2587 A_2, spr\u2587 A_3, spr\u2587 A_4)
	{
		switch (0)
		{
		default:
		{
			float num;
			float num2;
			float num3;
			float num4;
			for (;;)
			{
				num = A_0.X;
				num2 = A_0.Y;
				num3 = A_0.Width;
				num4 = A_0.Height;
				int num5 = 0;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						if (A_1.\u1719())
						{
							num5 = 8;
							continue;
						}
						goto IL_CE;
					case 1:
						if (true)
						{
						}
						if (A_4.\u1719())
						{
							num5 = 3;
							continue;
						}
						goto IL_19F;
					case 2:
						goto IL_157;
					case 3:
						num4 -= A_4.\u171E();
						goto IL_FD;
					case 4:
						goto IL_A9;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FD;
						}
						if (false)
						{
						}
						goto IL_CE;
					case 6:
						num3 -= A_2.\u171E();
						num5 = 4;
						continue;
					case 7:
						if (A_2.\u1719())
						{
							num5 = 6;
							continue;
						}
						goto IL_A9;
					case 8:
						num += A_1.\u171E();
						num3 -= A_1.\u171E();
						num5 = 5;
						continue;
					case 9:
						num2 += A_3.\u171E();
						num4 -= A_3.\u171E();
						num5 = 2;
						continue;
					case 10:
						if (A_3.\u1719())
						{
							num5 = 9;
							continue;
						}
						goto IL_157;
					case 11:
						goto IL_109;
					}
					break;
					IL_A9:
					num5 = 10;
					continue;
					IL_CE:
					num5 = 7;
					continue;
					IL_FD:
					num5 = 11;
					continue;
					IL_157:
					num5 = 1;
				}
			}
			IL_109:
			IL_19F:
			return new RectangleF(num, num2, num3, num4);
		}
		}
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x0035889C File Offset: 0x0035789C
	private void ᜁ(PointF A_0)
	{
		int num = 2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				this.ᜄ = new spr᠐();
				this.ᜄ.ᜀ(A_0);
				this.ᜃ = this.ᜄ;
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				return;
			}
			while (this.ᜄ == null)
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
					goto IL_0A;
				}
			}
			break;
		}
	}

	// Token: 0x06003986 RID: 14726 RVA: 0x00358934 File Offset: 0x00357934
	private void ᜀ(PointF A_0)
	{
		for (;;)
		{
			IL_30:
			float num = A_0.Y - this.ᜄ.\u1712().Y;
			bool flag = num > 0.005f;
			int num2 = 1;
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
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						this.ᜇ = new spr᠐();
						this.ᜇ.ᜀ(A_0);
						this.ᜄ.ᜂ(this.ᜇ);
						this.ᜇ.ᜃ(this.ᜄ);
						this.ᜇ.ᜀ(spr\u2587.\u1712);
						this.ᜄ();
						num2 = 2;
						continue;
					case 1:
						if (flag)
						{
							goto IL_6A;
						}
						return;
					case 2:
						return;
					}
					goto IL_30;
				}
				IL_6A:
				num2 = 0;
			}
		}
	}

	// Token: 0x06003987 RID: 14727 RVA: 0x00358A1C File Offset: 0x00357A1C
	private void ᜃ(float A_0, spr\u2587 A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜅ = new spr᠐();
				this.ᜅ.ᜀ(new PointF(A_0, this.ᜄ.\u1712().Y));
				this.ᜅ.ᜁ(A_1);
				this.ᜄ.ᜁ(this.ᜅ);
				this.ᜅ.ᜀ(this.ᜄ);
				num = 2;
				continue;
			case 2:
				return;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (this.ᜅ == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				return;
			}
			IL_4F:
			if (false)
			{
			}
			num = 1;
			continue;
			goto IL_4F;
		}
	}

	// Token: 0x06003988 RID: 14728 RVA: 0x00358AF4 File Offset: 0x00357AF4
	private void ᜂ(float A_0, spr\u2587 A_1)
	{
		switch (0)
		{
		default:
		{
			spr᠐ spr᠐;
			spr᠐ spr᠐2;
			for (;;)
			{
				spr᠐ = this.ᜄ;
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						spr᠐2 = spr᠐.ᜀ(A_0);
						spr᠐.ᜁ(spr\u2587.\u1712);
						num = 6;
						continue;
					case 1:
						if (spr᠐.ᜊ())
						{
							num = 0;
							continue;
						}
						spr᠐2 = spr᠐.ᜀ(A_0);
						spr᠐2.ᜁ(spr᠐.ᜉ());
						num = 5;
						continue;
					case 2:
						spr᠐ = spr᠐.\u1713();
						goto IL_D0;
					case 3:
						if (A_0 > spr᠐.\u1712().X)
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						num = 1;
						continue;
					case 4:
						goto IL_1AB;
					case 5:
						goto IL_16D;
					case 6:
						goto IL_100;
					case 7:
						spr᠐2 = spr᠐;
						spr᠐ = spr᠐.\u1713();
						num = 10;
						continue;
					case 8:
						spr᠐2 = spr᠐.ᜁ(A_0);
						spr᠐2.ᜁ(spr\u2587.\u1712);
						spr᠐ = null;
						num = 12;
						continue;
					case 9:
						if (!spr᠐.ᜇ())
						{
							num = 2;
							continue;
						}
						goto IL_172;
					case 10:
						goto IL_213;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D0;
						default:
							if (false)
							{
							}
							goto IL_172;
						}
						break;
					case 12:
						goto IL_148;
					case 13:
						goto IL_1AB;
					case 14:
						if (A_0 - spr᠐.\u1712().X <= 0.005f)
						{
							num = 11;
							continue;
						}
						num = 9;
						continue;
					case 15:
						if (Math.Abs(A_0 - spr᠐.\u1712().X) < 0.005f)
						{
							num = 7;
							continue;
						}
						num = 3;
						continue;
					}
					break;
					IL_D0:
					num = 4;
					continue;
					IL_172:
					num = 15;
					continue;
					IL_1AB:
					num = 14;
				}
			}
			IL_100:
			IL_148:
			IL_16D:
			IL_213:
			this.ᜄ = spr᠐2;
			this.ᜅ = spr᠐;
			return;
		}
		}
	}

	// Token: 0x06003989 RID: 14729 RVA: 0x00358D24 File Offset: 0x00357D24
	private void ᜁ(float A_0, spr\u2587 A_1)
	{
		switch (0)
		{
		default:
		{
			spr᠐ spr᠐3;
			for (;;)
			{
				spr᠐ spr᠐ = this.ᜅ;
				spr᠐ spr᠐2 = this.ᜄ;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_FA:
					spr᠐3 = spr᠐2.ᜁ(A_0);
					spr᠐3.ᜁ(spr\u2587.ᜀ(spr᠐.ᜉ(), A_1));
					num = 8;
					break;
				default:
					if (false)
					{
					}
					num = 6;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D2;
					case 1:
						if (spr᠐ != null)
						{
							num = 12;
							continue;
						}
						goto IL_182;
					case 2:
						goto IL_D7;
					case 3:
						goto IL_182;
					case 4:
						spr᠐3 = spr᠐2.ᜁ(A_0);
						spr᠐3.ᜁ(A_1);
						num = 0;
						continue;
					case 5:
						goto IL_180;
					case 6:
						goto IL_D7;
					case 7:
						if (spr᠐ == null)
						{
							num = 4;
							continue;
						}
						num = 10;
						continue;
					case 8:
						goto IL_120;
					case 9:
						spr᠐3 = spr᠐;
						spr᠐.ᜁ(spr\u2587.ᜀ(spr᠐.ᜉ(), A_1));
						num = 5;
						continue;
					case 10:
						if (Math.Abs(A_0 - spr᠐.\u1712().X) < 0.005f)
						{
							num = 9;
							continue;
						}
						goto IL_FA;
					case 11:
						if (A_0 - spr᠐.\u1712().X <= 0.005f)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						spr᠐.ᜁ(spr\u2587.ᜀ(spr᠐.ᜉ(), A_1));
						spr᠐2 = spr᠐;
						spr᠐ = spr᠐.\u1713();
						num = 2;
						continue;
					case 12:
						num = 11;
						continue;
					}
					break;
					IL_D7:
					num = 1;
					continue;
					IL_182:
					num = 7;
				}
			}
			IL_D2:
			IL_120:
			IL_180:
			this.ᜅ = spr᠐3;
			return;
		}
		}
	}

	// Token: 0x0600398A RID: 14730 RVA: 0x00358F14 File Offset: 0x00357F14
	private void ᜀ(float A_0, spr\u2587 A_1)
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
			if (this.ᜆ != null)
			{
				this.ᜆ.ᜀ(spr\u2587.ᜀ(this.ᜆ.ᜎ(), A_1));
				return;
			}
			break;
		}
		this.ᜆ = new spr᠐();
		this.ᜆ.ᜀ(new PointF(this.ᜄ.\u1712().X, this.ᜄ.\u1712().Y + A_0));
		this.ᜆ.ᜀ(A_1);
		this.ᜄ.ᜂ(this.ᜆ);
		this.ᜆ.ᜃ(this.ᜄ);
		this.ᜇ = this.ᜆ;
	}

	// Token: 0x0600398B RID: 14731 RVA: 0x00358FF8 File Offset: 0x00357FF8
	private spr᠐ ᜀ(spr\u2587 A_0, spr\u2587 A_1)
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
		spr᠐ spr᠐ = new spr᠐();
		spr᠐.ᜀ(new PointF(this.ᜅ.\u1712().X, this.ᜆ.\u1712().Y));
		spr᠐.ᜁ(A_0);
		spr᠐.ᜀ(A_1);
		this.ᜅ.ᜂ(spr᠐);
		spr᠐.ᜃ(this.ᜅ);
		this.ᜆ.ᜁ(spr᠐);
		spr᠐.ᜀ(this.ᜆ);
		return spr᠐;
	}

	// Token: 0x0600398C RID: 14732 RVA: 0x003590AC File Offset: 0x003580AC
	internal void ᜀ(spr\u24A6 A_0)
	{
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				return;
			case 2:
			{
				sprẜ a_ = new sprẜ(A_0);
				this.ᜂ();
				this.ᜁ(a_);
				this.ᜀ(a_);
				this.ᜀ();
				num = 0;
				continue;
			}
			}
			while (this.ᜃ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				num = 2;
				goto IL_12;
			}
			break;
		}
	}

	// Token: 0x0600398D RID: 14733 RVA: 0x00359140 File Offset: 0x00358140
	internal void ᜂ(sprẜ A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				this.ᜂ();
				this.ᜁ(A_0);
				this.ᜀ(A_0);
				this.ᜀ();
				num = 0;
				continue;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (this.ᜃ())
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				return;
			}
			IL_4C:
			if (false)
			{
			}
			num = 2;
			continue;
			goto IL_4C;
		}
	}

	// Token: 0x0600398E RID: 14734 RVA: 0x003591D0 File Offset: 0x003581D0
	internal spr\u24A6 ᜆ()
	{
		spr\u24A6 spr_u24A;
		for (;;)
		{
			IL_38:
			spr_u24A = null;
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return spr_u24A;
					case 1:
						if (this.ᜃ())
						{
							goto IL_54;
						}
						return spr_u24A;
					case 2:
					{
						spr_u24A = new spr\u24A6();
						sprẜ a_ = new sprẜ(spr_u24A);
						this.ᜂ();
						this.ᜁ(a_);
						this.ᜀ(a_);
						this.ᜀ();
						num = 0;
						continue;
					}
					}
					goto IL_38;
				}
				IL_54:
				num = 2;
			}
		}
		return spr_u24A;
	}

	// Token: 0x0600398F RID: 14735 RVA: 0x00359270 File Offset: 0x00358270
	internal void ᜀ(float A_0, float A_1)
	{
		for (;;)
		{
			IL_28:
			spr᠐ spr᠐ = this.ᜃ;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_76:
				goto IL_9F;
			default:
				if (false)
				{
				}
				num = 6;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_78;
				case 1:
					spr᠐ = spr᧑.ᜀ(spr᠐);
					num = 2;
					continue;
				case 2:
					goto IL_76;
				case 3:
				{
					if (spr᠐ == null)
					{
						num = 5;
						continue;
					}
					spr᠐ spr᠐2 = spr᠐;
					num = 4;
					continue;
				}
				case 4:
					goto IL_78;
				case 5:
					return;
				case 6:
					goto IL_5D;
				case 7:
				{
					spr᠐ spr᠐2;
					if (spr᠐2 == null)
					{
						num = 1;
						continue;
					}
					spr᠐2.ᜀ(A_0, A_1);
					spr᠐2 = spr᠐2.\u1713();
					num = 0;
					continue;
				}
				}
				goto IL_28;
				IL_78:
				num = 7;
			}
			IL_5D:
			if (true)
			{
			}
			IL_9F:
			num = 3;
			goto IL_02;
		}
	}

	// Token: 0x06003990 RID: 14736 RVA: 0x00359354 File Offset: 0x00358354
	private static spr᠐ ᜀ(spr᠐ A_0)
	{
		spr᠐ spr᠐2;
		for (;;)
		{
			spr᠐ spr᠐ = A_0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8E;
				case 1:
					num = 5;
					continue;
				case 2:
					if (spr᠐2 == null)
					{
						num = 1;
						continue;
					}
					goto IL_30;
				case 3:
					if (spr᠐2 != null)
					{
						num = 6;
						continue;
					}
					goto IL_BE;
				case 4:
					goto IL_30;
				case 5:
					if (spr᠐ == null)
					{
						goto IL_77;
					}
					goto IL_8E;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						goto IL_5C;
					}
					break;
				}
				break;
				IL_30:
				num = 3;
				continue;
				IL_77:
				num = 4;
				continue;
				IL_8E:
				spr᠐2 = spr᠐.ᜐ();
				spr᠐ = spr᠐.\u1713();
				num = 2;
			}
		}
		IL_5C:
		if (true)
		{
		}
		if (false)
		{
		}
		return spr᠐2.ᜈ();
		IL_BE:
		return null;
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x00359420 File Offset: 0x00358420
	private void ᜂ()
	{
		for (;;)
		{
			IL_3A:
			this.ᜁ();
			spr᠐ spr᠐ = this.ᜃ;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7C:
				goto IL_A8;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				IL_02:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					spr᠐ spr᠐2;
					if (spr᠐2 == null)
					{
						num = 7;
						continue;
					}
					spr᠐2.\u1717();
					spr᠐2 = spr᠐2.\u1713();
					num = 5;
					continue;
				}
				case 2:
					goto IL_7E;
				case 3:
					goto IL_6B;
				case 4:
				{
					if (spr᠐ == null)
					{
						num = 0;
						continue;
					}
					spr᠐ spr᠐2 = spr᠐;
					num = 2;
					continue;
				}
				case 5:
					goto IL_7E;
				case 6:
					goto IL_7C;
				case 7:
					spr᠐ = spr᧑.ᜀ(spr᠐);
					num = 6;
					continue;
				}
				goto IL_3A;
				IL_7E:
				num = 1;
			}
			IL_6B:
			IL_A8:
			num = 4;
			goto IL_02;
		}
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x0035950C File Offset: 0x0035850C
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					goto IL_C8;
				case 2:
				{
					spr᠐ spr᠐ = this.ᜃ;
					num = 1;
					continue;
				}
				case 3:
				{
					spr᠐ spr᠐;
					if (spr᠐ == null)
					{
						num = 5;
						continue;
					}
					float num2;
					spr᠐.ᜀ(new PointF(num2, spr᠐.\u1712().Y));
					float num3;
					spr᠐.\u1713().ᜀ(new PointF(num2 + num3, spr᠐.\u1712().Y));
					spr᠐ = spr᠐.ᜐ();
					if (true)
					{
					}
					num = 8;
					continue;
				}
				case 4:
				{
					spr᠐ spr᠐2;
					if (spr᠐2 == null)
					{
						num = 2;
						continue;
					}
					float num2 = Math.Min(num2, spr᠐2.\u1712().X);
					float num3 = Math.Max(num3, spr᠐2.\u1713().\u1712().X - num2);
					spr᠐2 = spr᠐2.ᜐ();
					num = 0;
					continue;
				}
				case 5:
					return;
				case 7:
					goto IL_71;
				case 8:
					goto IL_C8;
				case 9:
					return;
				}
				if (this.ᜁ != BorderGridType.Paragraph)
				{
					num = 9;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					float num2 = 2.1474836E+09f;
					float num3 = -2.1474836E+09f;
					spr᠐ spr᠐2 = this.ᜃ;
					num = 7;
					continue;
				}
				}
				IL_71:
				num = 4;
				continue;
				IL_C8:
				num = 3;
			}
			return;
		}
		}
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x003596B4 File Offset: 0x003586B4
	private void ᜀ()
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
		this.ᜃ = null;
		this.ᜄ = null;
		this.ᜅ = null;
		this.ᜆ = null;
		this.ᜇ = null;
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x00359714 File Offset: 0x00358714
	private void ᜁ(sprẜ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr᠐ spr᠐ = this.ᜃ;
				int num = 16;
				for (;;)
				{
					spr᠐ spr᠐2;
					spr᠐ spr᠐4;
					switch (num)
					{
					case 0:
						if (spr᠐ == null)
						{
							num = 10;
							continue;
						}
						spr᠐2 = spr᠐;
						num = 12;
						continue;
					case 1:
					{
						spr᠐ spr᠐3 = spr᠐2;
						spr᠐4 = spr᠐2.ᜐ();
						num = 14;
						continue;
					}
					case 2:
						if (spr᠐4 == null)
						{
							num = 8;
							continue;
						}
						num = 18;
						continue;
					case 3:
						goto IL_1EF;
					case 4:
					{
						if (spr᠐2 == null)
						{
							num = 5;
							continue;
						}
						bool flag = !spr᠐2.ᜌ();
						goto IL_F5;
					}
					case 5:
						spr᠐ = spr᧑.ᜀ(spr᠐);
						num = 11;
						continue;
					case 6:
						if (!spr᠐4.ᜅ())
						{
							num = 17;
							continue;
						}
						goto IL_1EF;
					case 7:
					{
						bool flag;
						if (!flag)
						{
							num = 1;
							continue;
						}
						goto IL_7C;
					}
					case 8:
						goto IL_7C;
					case 9:
						goto IL_1CC;
					case 10:
						goto IL_15E;
					case 11:
						goto IL_140;
					case 12:
						goto IL_1CC;
					case 13:
						goto IL_1EF;
					case 14:
						goto IL_115;
					case 15:
						goto IL_115;
					case 16:
						goto IL_140;
					case 17:
					{
						spr᠐ spr᠐3;
						this.ᜂ.ᜀ(spr᠐3, spr᠐4, this.ᜁ == BorderGridType.Table, true, A_0);
						spr᠐3 = spr᠐4;
						num = 3;
						continue;
					}
					case 18:
					{
						spr᠐ spr᠐3;
						if (spr᠐3.ᜏ().ᜆ())
						{
							num = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F5;
						default:
							if (false)
							{
							}
							num = 19;
							continue;
						}
						break;
					}
					case 19:
					{
						spr᠐ spr᠐3 = spr᠐4;
						num = 13;
						continue;
					}
					}
					break;
					IL_7C:
					spr᠐2 = spr᠐2.\u1713();
					num = 9;
					continue;
					IL_F5:
					num = 7;
					continue;
					IL_115:
					num = 2;
					continue;
					IL_140:
					num = 0;
					continue;
					IL_1CC:
					num = 4;
					continue;
					IL_1EF:
					spr᠐4 = spr᠐4.ᜐ();
					num = 15;
				}
			}
			IL_15E:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x00359954 File Offset: 0x00358954
	private void ᜀ(sprẜ A_0)
	{
		for (;;)
		{
			spr᠐ spr᠐ = this.ᜃ;
			int num = 3;
			for (;;)
			{
				spr᠐ spr᠐3;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_104;
					default:
						if (false)
						{
						}
						spr᠐ = spr᧑.ᜀ(spr᠐);
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_120;
				case 2:
				{
					spr᠐ spr᠐2;
					this.ᜂ.ᜀ(spr᠐2, spr᠐3, this.ᜁ == BorderGridType.Table, false, A_0);
					spr᠐2 = spr᠐3;
					num = 11;
					continue;
				}
				case 3:
					goto IL_120;
				case 4:
				{
					if (spr᠐ == null)
					{
						num = 12;
						continue;
					}
					spr᠐ spr᠐2 = spr᠐;
					spr᠐3 = spr᠐2.\u1713();
					num = 9;
					continue;
				}
				case 5:
					goto IL_109;
				case 6:
				{
					spr᠐ spr᠐2 = spr᠐3;
					num = 5;
					continue;
				}
				case 7:
					goto IL_54;
				case 8:
					if (spr᠐3 == null)
					{
						num = 0;
						continue;
					}
					num = 13;
					continue;
				case 9:
					goto IL_104;
				case 10:
					if (!spr᠐3.ᜁ())
					{
						num = 2;
						continue;
					}
					goto IL_109;
				case 11:
					goto IL_109;
				case 12:
					return;
				case 13:
				{
					spr᠐ spr᠐2;
					if (!spr᠐2.\u1716().ᜆ())
					{
						num = 6;
						continue;
					}
					num = 10;
					continue;
				}
				}
				break;
				IL_54:
				if (true)
				{
				}
				num = 8;
				continue;
				IL_104:
				goto IL_54;
				IL_109:
				spr᠐3 = spr᠐3.\u1713();
				num = 7;
				continue;
				IL_120:
				num = 4;
			}
		}
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x00359AD8 File Offset: 0x00358AD8
	internal spr᠐ ᜅ()
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

	// Token: 0x06003997 RID: 14743 RVA: 0x00359B1C File Offset: 0x00358B1C
	internal bool ᜃ()
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
		return this.ᜃ != null;
	}

	// Token: 0x04002AB9 RID: 10937
	private const float ᜀ = 0.005f;

	// Token: 0x04002ABA RID: 10938
	private readonly BorderGridType ᜁ;

	// Token: 0x04002ABB RID: 10939
	private readonly spr\u2515 ᜂ;

	// Token: 0x04002ABC RID: 10940
	private spr᠐ ᜃ;

	// Token: 0x04002ABD RID: 10941
	private spr᠐ ᜄ;

	// Token: 0x04002ABE RID: 10942
	private spr᠐ ᜅ;

	// Token: 0x04002ABF RID: 10943
	private spr᠐ ᜆ;

	// Token: 0x04002AC0 RID: 10944
	private spr᠐ ᜇ;
}
