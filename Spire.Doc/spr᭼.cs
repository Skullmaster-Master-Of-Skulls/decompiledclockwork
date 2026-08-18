using System;
using System.Drawing;

// Token: 0x020002F4 RID: 756
internal class spr\u1B7C
{
	// Token: 0x0600297C RID: 10620 RVA: 0x002947E8 File Offset: 0x002937E8
	internal spr\u1B7C()
	{
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x002947FC File Offset: 0x002937FC
	internal spr\u1B7C(PointF A_0, PointF A_1)
	{
		if (sprὍ.ᜀ(A_0.X, A_1.X))
		{
			this.ᜂ = true;
			this.ᜄ(A_0.X);
			return;
		}
		this.ᜃ((A_0.Y - A_1.Y) / (A_0.X - A_1.X));
		this.ᜄ(A_0.Y - this.ᜄ() * A_0.X);
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x0029487C File Offset: 0x0029387C
	internal float ᜂ(float A_0)
	{
		if (!this.ᜃ())
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
				return this.ᜄ() * A_0 + this.ᜅ();
			}
		}
		return this.ᜅ();
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x002948D8 File Offset: 0x002938D8
	internal bool ᜂ(PointF A_0)
	{
		if (!this.ᜃ())
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
				return this.ᜂ(A_0.X) < A_0.Y;
			}
		}
		return this.ᜅ() < A_0.X;
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x00294944 File Offset: 0x00293944
	internal spr\u1B7C ᜁ(float A_0)
	{
		int num = 0;
		float num2;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num2 = A_0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_98;
			case 3:
				goto IL_65;
			}
			if (this.ᜄ() == 0f)
			{
				num = 1;
			}
			else
			{
				float num3 = (float)Math.Abs(Math.Cos(Math.Atan((double)this.ᜄ())));
				num2 = A_0 / num3;
				num = 3;
			}
		}
		IL_65:
		IL_98:
		spr\u1B7C spr_u1B7C = new spr\u1B7C();
		spr_u1B7C.ᜂ = this.ᜃ();
		spr_u1B7C.ᜃ(this.ᜄ());
		spr_u1B7C.ᜄ(this.ᜅ() + num2);
		return spr_u1B7C;
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x00294A18 File Offset: 0x00293A18
	internal spr\u1B7C ᜃ(PointF A_0)
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
		return this.ᜁ(A_0, false);
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x00294A5C File Offset: 0x00293A5C
	internal spr\u1B7C ᜁ(PointF A_0, bool A_1)
	{
		spr\u1B7C spr_u1B7C;
		for (;;)
		{
			spr_u1B7C = new spr\u1B7C();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!sprὍ.ᜀ(this.ᜂ(A_0.X), A_0.Y))
					{
						num = 14;
						continue;
					}
					goto IL_1EB;
				case 1:
					if (this.ᜃ())
					{
						num = 9;
						continue;
					}
					num = 3;
					continue;
				case 2:
					goto IL_CA;
				case 3:
					if (this.ᜄ() == 0f)
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				case 4:
					if (A_1)
					{
						num = 7;
						continue;
					}
					goto IL_1CB;
				case 5:
					num = 13;
					continue;
				case 6:
					num = 11;
					continue;
				case 7:
					goto IL_187;
				case 8:
					if (A_1)
					{
						num = 15;
						continue;
					}
					goto IL_1EB;
				case 9:
					if (true)
					{
					}
					num = 4;
					continue;
				case 10:
					goto IL_131;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_187;
					default:
						if (false)
						{
						}
						if (!sprὍ.ᜀ(A_0.Y, this.ᜅ()))
						{
							num = 2;
							continue;
						}
						goto IL_6E;
					}
					break;
				case 12:
					if (!sprὍ.ᜀ(A_0.X, this.ᜅ()))
					{
						num = 10;
						continue;
					}
					goto IL_1CB;
				case 13:
					if (A_1)
					{
						num = 6;
						continue;
					}
					goto IL_6E;
				case 14:
					goto IL_102;
				case 15:
					num = 0;
					continue;
				}
				break;
				IL_187:
				num = 12;
			}
		}
		IL_6E:
		spr_u1B7C.ᜂ = true;
		spr_u1B7C.ᜄ(A_0.X);
		return spr_u1B7C;
		IL_CA:
		return null;
		IL_102:
		return null;
		IL_131:
		return null;
		IL_1CB:
		return new spr\u1B7C(A_0, new PointF(A_0.X + 1f, A_0.Y));
		IL_1EB:
		spr_u1B7C.ᜃ(-1f / this.ᜄ());
		spr_u1B7C.ᜄ(A_0.X * (this.ᜄ() - spr_u1B7C.ᜄ()) + this.ᜅ());
		return spr_u1B7C;
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x00294C8C File Offset: 0x00293C8C
	internal spr\u1B7C ᜄ(PointF A_0)
	{
		spr\u1B7C spr_u1B7C = new spr\u1B7C();
		if (this.ᜃ())
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
				spr_u1B7C.ᜂ = true;
				spr_u1B7C.ᜄ(A_0.X);
				return spr_u1B7C;
			}
		}
		spr_u1B7C.ᜃ(this.ᜄ());
		spr_u1B7C.ᜄ(A_0.Y - this.ᜄ() * A_0.X);
		return spr_u1B7C;
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x00294D18 File Offset: 0x00293D18
	internal bool ᜁ(PointF A_0)
	{
		if (this.ᜃ())
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
				return sprὍ.ᜀ(A_0.X, this.ᜅ());
			}
		}
		return sprὍ.ᜀ(A_0.Y, this.ᜂ(A_0.X));
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x00294D8C File Offset: 0x00293D8C
	internal void ᜀ(PointF A_0, float A_1, PointF[] A_2)
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
		this.ᜀ(A_0, A_1, A_2, false);
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x00294DD4 File Offset: 0x00293DD4
	internal bool ᜀ(PointF A_0, float A_1, PointF[] A_2, bool A_3)
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.ᜁ(A_0))
				{
					num = 6;
					continue;
				}
				goto IL_6C;
			case 1:
				A_1 *= -1f;
				num = 4;
				continue;
			case 2:
				goto IL_84;
			case 3:
				if (this.ᜃ())
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num = 5;
				continue;
			case 4:
				goto IL_C5;
			case 5:
				if (this.ᜄ() < 0f)
				{
					num = 1;
					continue;
				}
				goto IL_108;
			case 6:
				return false;
			case 8:
				num = 0;
				continue;
			}
			if (A_3)
			{
				num = 8;
				continue;
			}
			IL_6C:
			num = 3;
		}
		IL_84:
		goto IL_E5;
		IL_C5:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_E5:
			A_2[0] = new PointF(A_0.X, A_0.Y + A_1);
			return true;
		default:
			if (false)
			{
			}
			break;
		}
		IL_108:
		double num2 = Math.Atan((double)this.ᜄ());
		float x = (float)(Math.Cos(num2) * (double)A_1 + (double)A_0.X);
		float y = (float)(Math.Sin(num2) * (double)A_1 + (double)A_0.Y);
		A_2[0] = new PointF(x, y);
		return true;
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x00294F34 File Offset: 0x00293F34
	internal static bool ᜀ(spr\u1B7C A_0, spr\u1B7C A_1)
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9D;
			case 1:
				goto IL_BA;
			case 2:
				num = 9;
				continue;
			case 3:
				num = 5;
				continue;
			case 4:
				num = 0;
				continue;
			case 5:
				if (!A_0.ᜃ())
				{
					num = 2;
					continue;
				}
				return false;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
					goto IL_D2;
				}
				break;
			case 8:
				if (sprὍ.ᜀ(A_0.ᜄ(), A_1.ᜄ()))
				{
					num = 3;
					continue;
				}
				return false;
			case 9:
				if (!A_1.ᜃ())
				{
					num = 6;
					continue;
				}
				return false;
			}
			if (A_0.ᜃ())
			{
				num = 4;
				continue;
			}
			IL_67:
			num = 8;
			continue;
			IL_9D:
			if (!A_1.ᜃ())
			{
				goto IL_67;
			}
			num = 1;
		}
		IL_BA:
		if (true)
		{
		}
		return true;
		IL_D2:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x0029504C File Offset: 0x0029404C
	internal static void ᜁ(spr\u1B7C A_0, spr\u1B7C A_1, PointF[] A_2)
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
		spr\u1B7C.ᜀ(A_0, A_1, A_2, false);
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x00295094 File Offset: 0x00294094
	internal static bool ᜀ(spr\u1B7C A_0, spr\u1B7C A_1, PointF[] A_2, bool A_3)
	{
		float num2;
		float y;
		for (;;)
		{
			IL_00:
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					if (A_0.ᜃ())
					{
						num = 5;
						continue;
					}
					num = 10;
					continue;
				case 2:
					goto IL_12F;
				case 3:
					if (spr\u1B7C.ᜀ(A_0, A_1))
					{
						num = 7;
						continue;
					}
					goto IL_131;
				case 4:
					goto IL_8E;
				case 5:
					if (true)
					{
					}
					num2 = A_0.ᜅ();
					y = A_1.ᜂ(num2);
					num = 4;
					continue;
				case 6:
					goto IL_E8;
				case 7:
					return false;
				case 8:
					num2 = A_1.ᜅ();
					y = A_0.ᜂ(num2);
					num = 2;
					continue;
				case 10:
					if (A_1.ᜃ())
					{
						num = 8;
						continue;
					}
					num2 = (A_1.ᜅ() - A_0.ᜅ()) / (A_0.ᜄ() - A_1.ᜄ());
					y = A_0.ᜄ() * num2 + A_0.ᜅ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				if (A_3)
				{
					num = 0;
					continue;
				}
				IL_131:
				num = 1;
			}
		}
		IL_8E:
		IL_E8:
		IL_12F:
		A_2[0] = new PointF(num2, y);
		return true;
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x0029520C File Offset: 0x0029420C
	internal static bool ᜀ(spr\u1B7C A_0, spr\u1B7C A_1, PointF[] A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_80;
			case 2:
				goto IL_60;
			case 3:
				goto IL_42;
			case 4:
				if (!A_1.ᜃ())
				{
					num = 2;
					continue;
				}
				goto IL_42;
			case 5:
				if (A_0.ᜄ() == A_1.ᜄ())
				{
					num = 3;
					continue;
				}
				goto IL_A7;
			}
			if (A_0.ᜃ())
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			IL_60:
			num = 5;
			continue;
			IL_80:
			num = 4;
			continue;
			IL_42:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_80;
			default:
				goto IL_58;
			}
		}
		IL_58:
		if (false)
		{
		}
		return false;
		IL_A7:
		return spr\u1B7C.ᜀ(A_0, A_1, A_2, false);
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x002952CC File Offset: 0x002942CC
	internal static spr\u1B7C ᜀ(PointF A_0, bool A_1)
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
		return new spr\u1B7C(A_0, new PointF(A_0.X + (float)(A_1 ? 1 : 0), A_0.Y + (float)(A_1 ? 0 : 1)));
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x0029533C File Offset: 0x0029433C
	internal float ᜄ()
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

	// Token: 0x0600298D RID: 10637 RVA: 0x00295380 File Offset: 0x00294380
	internal void ᜃ(float A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x002953C4 File Offset: 0x002943C4
	internal float ᜅ()
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
		return this.ᜁ;
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x00295408 File Offset: 0x00294408
	internal void ᜄ(float A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x0029544C File Offset: 0x0029444C
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
		return this.ᜂ;
	}

	// Token: 0x0400240E RID: 9230
	private float ᜀ;

	// Token: 0x0400240F RID: 9231
	private float ᜁ;

	// Token: 0x04002410 RID: 9232
	private bool ᜂ;
}
