using System;
using System.Drawing;

// Token: 0x020002F1 RID: 753
internal class spr\u20EF
{
	// Token: 0x0600295D RID: 10589 RVA: 0x00292368 File Offset: 0x00291368
	internal static spr\u1D3C ᜀ(byte[] A_0, RectangleF A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 17;
			spr\u1D3C spr_u1D3C;
			for (;;)
			{
				spr\u20EF spr_u20EF;
				switch (num)
				{
				case 0:
					goto IL_13D;
				case 1:
					goto IL_D5;
				case 2:
				{
					float num2;
					if (num2 != 0f)
					{
						num = 5;
						continue;
					}
					return spr_u1D3C;
				}
				case 3:
				{
					if (spr_u1D3C.ᜀ() == 0)
					{
						num = 7;
						continue;
					}
					float num2 = spr_u20EF.ᜁ;
					float num3 = spr_u20EF.ᜂ;
					num = 13;
					continue;
				}
				case 4:
				{
					float num3;
					if (num3 == 0f)
					{
						num = 1;
						continue;
					}
					num = 8;
					continue;
				}
				case 5:
					num = 4;
					continue;
				case 6:
					num = 10;
					continue;
				case 7:
					goto IL_139;
				case 8:
					if (A_1 != RectangleF.Empty)
					{
						num = 14;
						continue;
					}
					goto IL_23A;
				case 9:
					goto IL_86;
				case 10:
				{
					float num3;
					if (A_1.Height != num3)
					{
						num = 11;
						continue;
					}
					return spr_u1D3C;
				}
				case 11:
					goto IL_1DD;
				case 12:
				{
					int num4;
					if (num4 >= spr_u1D3C.ᜀ())
					{
						num = 15;
						continue;
					}
					spr\u25FD spr_u25FD;
					spr_u1D3C.ᜀ(num4).ᜀ(spr_u25FD);
					num4++;
					num = 0;
					continue;
				}
				case 13:
				{
					float num2;
					if (A_1.Width == num2)
					{
						num = 6;
						continue;
					}
					goto IL_1DD;
				}
				case 14:
				{
					spr\u25FD spr_u25FD = new spr\u25FD();
					float num2;
					float num3;
					spr_u25FD.ᜁ(A_1.Width / num2, A_1.Height / num3);
					spr_u25FD.ᜀ(A_1.X, A_1.Y);
					int num4 = 0;
					num = 16;
					continue;
				}
				case 15:
					goto IL_15F;
				case 16:
					goto IL_13D;
				}
				if (spr\u2075.ᜂ(A_0))
				{
					num = 9;
					continue;
				}
				spr_u20EF = new spr\u20EF();
				spr_u1D3C = spr_u20EF.ᜀ(A_0);
				num = 3;
				continue;
				IL_13D:
				num = 12;
				continue;
				IL_1DD:
				num = 2;
			}
			IL_86:
			return new spr\u1D3C(new sprᲨ(A_1));
			IL_D5:
			if (true)
			{
			}
			return spr_u1D3C;
			IL_139:
			return new spr\u1D3C();
			IL_15F:
			IL_23A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_D5;
			default:
				if (false)
				{
				}
				return spr_u1D3C;
			}
			break;
		}
		}
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x002925CC File Offset: 0x002915CC
	private spr\u1D3C ᜀ(byte[] A_0)
	{
		spr\u1D3C result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u2091 spr_u = new spr\u2091(A_0);
			try
			{
				spr᠖ a_ = spr_u.ᜏ();
				result = spr\u20EF.ᜀ(a_, spr_u.ᜃ(), spr_u.\u170D());
				this.ᜁ = (float)spr_u.ᜃ();
				this.ᜂ = (float)spr_u.\u170D();
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9A;
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
				IL_9A:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x00292694 File Offset: 0x00291694
	private static spr\u1D3C ᜀ(spr᠖ A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 17;
			for (;;)
			{
				sprᲨ sprᲨ;
				spr\u1D3C spr_u1D3C;
				bool flag;
				int num2;
				int a_;
				int[] array;
				int[] array2;
				switch (num)
				{
				case 0:
					goto IL_18E;
				case 1:
					if (sprᲨ.ᜅ() > 0)
					{
						num = 0;
						continue;
					}
					return spr_u1D3C;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_A5;
				case 4:
					goto IL_201;
				case 5:
					if (flag)
					{
						num = 9;
						continue;
					}
					goto IL_A5;
				case 6:
					a_ = num2 + 1;
					num = 10;
					continue;
				case 7:
					if (!flag)
					{
						num = 6;
						continue;
					}
					goto IL_1A6;
				case 8:
					goto IL_A5;
				case 9:
					flag = false;
					spr\u20EF.ᜀ(sprᲨ, array, num2, a_);
					spr_u1D3C.ᜀ(sprᲨ);
					sprᲨ = new sprᲨ();
					num = 3;
					continue;
				case 10:
					goto IL_1A6;
				case 11:
					goto IL_201;
				case 12:
					spr\u20EF.ᜀ(sprᲨ, new PointF((float)array2[num2], (float)num2));
					num = 7;
					continue;
				case 13:
					return spr_u1D3C;
				case 14:
					if (num2 == 0)
					{
						num = 15;
						continue;
					}
					goto IL_A5;
				case 15:
					spr\u20EF.ᜀ(sprᲨ, array, num2, a_);
					num = 8;
					continue;
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18E;
					default:
						goto IL_9A;
					}
					break;
				case 18:
					if (array2[num2] != -1)
					{
						num = 12;
						continue;
					}
					num = 5;
					continue;
				case 19:
					if (num2 < 0)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					num = 18;
					continue;
				}
				if (!A_0.ᜂ())
				{
					num = 16;
					continue;
				}
				array = new int[A_2];
				array2 = new int[A_2];
				spr\u20EF.ᜀ(A_0, A_1, A_2, array, array2);
				spr_u1D3C = new spr\u1D3C();
				sprᲨ = new sprᲨ();
				a_ = A_2;
				flag = true;
				num2 = A_2 - 1;
				num = 11;
				continue;
				IL_A5:
				num2--;
				num = 4;
				continue;
				IL_18E:
				spr_u1D3C.ᜀ(sprᲨ);
				num = 13;
				continue;
				IL_1A6:
				flag = true;
				num = 14;
				continue;
				IL_201:
				num = 19;
			}
			IL_9A:
			if (false)
			{
			}
			return new spr\u1D3C(new sprᲨ(new PointF[]
			{
				new PointF(0f, 0f),
				new PointF((float)A_1, 0f),
				new PointF((float)A_1, (float)A_2),
				new PointF(0f, (float)A_2)
			}));
		}
		}
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x00292990 File Offset: 0x00291990
	private static void ᜀ(sprᲨ A_0, int[] A_1, int A_2, int A_3)
	{
		for (;;)
		{
			int num = A_2;
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
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						spr\u20EF.ᜀ(A_0, new PointF((float)A_1[num], (float)num));
						num2 = 6;
						continue;
					case 1:
						if (A_1[num] != -1)
						{
							num2 = 0;
							continue;
						}
						goto IL_56;
					case 2:
						if (true)
						{
						}
						if (num >= A_3)
						{
							num2 = 5;
							continue;
						}
						num2 = 1;
						continue;
					case 3:
						goto IL_9D;
					case 4:
						goto IL_9D;
					case 5:
						return;
					case 6:
						goto IL_56;
					}
					break;
					IL_56:
					num++;
					num2 = 4;
					continue;
					IL_9D:
					num2 = 2;
				}
				break;
			}
			}
		}
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x00292A60 File Offset: 0x00291A60
	private static void ᜀ(spr᠖ A_0, int A_1, int A_2, int[] A_3, int[] A_4)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					A_4[num] = spr\u20EF.ᜀ(num, A_1, A_0);
					num2 = 2;
					continue;
				case 1:
					goto IL_34;
				case 2:
					goto IL_34;
				case 3:
					goto IL_A8;
				case 4:
					if (A_3[num] != -1)
					{
						num2 = 0;
						continue;
					}
					A_4[num] = -1;
					num2 = 1;
					continue;
				case 5:
					goto IL_A8;
				case 6:
					return;
				case 7:
					if (num >= A_2)
					{
						num2 = 6;
						continue;
					}
					A_3[num] = spr\u20EF.ᜁ(num, A_1, A_0);
					num2 = 4;
					continue;
				}
				break;
				IL_34:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num++;
					num2 = 5;
					continue;
				}
				IL_A8:
				num2 = 7;
			}
		}
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x00292B50 File Offset: 0x00291B50
	private static int ᜁ(int A_0, int A_1, spr᠖ A_2)
	{
		int num;
		for (;;)
		{
			IL_46:
			num = 0;
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return num;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_8D;
					case 1:
						goto IL_8B;
					case 2:
						goto IL_8D;
					case 3:
						if (A_2.ᜁ()[A_0 * A_1 + num] != 0)
						{
							num2 = 1;
							continue;
						}
						num++;
						num2 = 0;
						continue;
					case 4:
						return -1;
					case 5:
						if (num >= A_1)
						{
							num2 = 4;
							continue;
						}
						num2 = 3;
						continue;
					}
					goto IL_46;
					IL_8D:
					num2 = 5;
					break;
				}
			}
		}
		return num;
		IL_8B:
		return num;
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x00292C08 File Offset: 0x00291C08
	private static int ᜀ(int A_0, int A_1, spr᠖ A_2)
	{
		int num;
		for (;;)
		{
			IL_3C:
			if (true)
			{
			}
			num = A_1 - 1;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return num;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_8F;
					case 1:
						if (num <= -1)
						{
							num2 = 4;
							continue;
						}
						num2 = 5;
						continue;
					case 2:
						goto IL_8F;
					case 3:
						goto IL_8D;
					case 4:
						return -1;
					case 5:
						if (A_2.ᜁ()[A_0 * A_1 + num] != 0)
						{
							num2 = 3;
							continue;
						}
						num--;
						num2 = 0;
						continue;
					}
					goto IL_3C;
					IL_8F:
					num2 = 1;
					break;
				}
			}
		}
		return num;
		IL_8D:
		return num;
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x00292CC4 File Offset: 0x00291CC4
	private static void ᜀ(sprᲨ A_0, PointF A_1)
	{
		int num2;
		for (;;)
		{
			IL_20:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AE:
				num = 3;
				break;
			case 1:
				goto IL_40;
			default:
				goto IL_40;
			}
			PointF pointF;
			PointF pointF2;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
					if (pointF.X == A_1.X)
					{
						num = 0;
						continue;
					}
					goto IL_EE;
				case 2:
					num = 1;
					continue;
				case 3:
					if (pointF2.X == pointF.X)
					{
						num = 2;
						continue;
					}
					goto IL_EE;
				case 4:
					goto IL_61;
				case 5:
					if (num2 < 2)
					{
						num = 4;
						continue;
					}
					goto IL_90;
				}
				goto IL_20;
			}
			IL_90:
			pointF2 = A_0.ᜃ(num2 - 2).ᜁ();
			pointF = A_0.ᜃ(num2 - 1).ᜁ();
			goto IL_AE;
			IL_40:
			if (false)
			{
			}
			num2 = A_0.ᜅ();
			num = 5;
			goto IL_02;
		}
		IL_61:
		A_0.ᜁ(A_1);
		return;
		IL_86:
		if (true)
		{
		}
		A_0.ᜀ(num2 - 1, new spr\u2251(A_1));
		return;
		IL_EE:
		A_0.ᜁ(A_1);
	}

	// Token: 0x040023F3 RID: 9203
	private const int ᜀ = -1;

	// Token: 0x040023F4 RID: 9204
	private float ᜁ;

	// Token: 0x040023F5 RID: 9205
	private float ᜂ;
}
