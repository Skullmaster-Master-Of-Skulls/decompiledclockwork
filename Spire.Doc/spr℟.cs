using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x020002C3 RID: 707
internal class spr\u211F
{
	// Token: 0x0600266A RID: 9834 RVA: 0x002607A8 File Offset: 0x0025F7A8
	internal spr\u211F(spr\u1B70 A_0, sprᢉ A_1)
	{
		this.ᜁ = A_1;
		this.ᜂ = new spr\u1B70();
		this.ᜂ().ᜀ(A_0.ᜆ().ᜈ());
		this.ᜂ().ᜆ().ᜀ(sprὝ.ᜂ);
		this.ᜂ().ᜆ().ᜂ((this.ᜌ().ᜁ() > 1f) ? this.ᜌ().ᜁ() : 1f);
		if (this.ᜂ().ᜆ().\u170D() != DashStyle.Solid)
		{
			this.ᜂ().ᜆ().ᜁ(spr\u211F.ᜀ(A_0.ᜆ().ᜋ(), A_0.ᜆ().ᜀ(), this.ᜂ().ᜆ().ᜀ()));
		}
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x00260884 File Offset: 0x0025F884
	internal void ᜁ(spr\u1926 A_0)
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
		this.ᜃ = new spr\u1926();
		this.ᜁ().ᜀ(A_0.ᜁ());
		this.ᜀ(A_0.ᜁ());
		this.ᜂ().ᜁ(this.ᜁ());
		this.ᜄ = null;
		this.ᜅ = null;
		this.ᜉ = null;
		this.ᜆ = false;
		this.ᜀ(0);
		ArrayList a_ = spr\u211F.ᜀ(A_0);
		this.ᜏ = sprὍ.ᜀ(a_);
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x00260930 File Offset: 0x0025F930
	internal void ᜀ(spr\u187D[] A_0)
	{
		for (;;)
		{
			IL_3C:
			PointF pointF;
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_177:
				goto IL_A0;
			default:
				if (false)
				{
				}
				pointF = PointF.Empty;
				num = 0;
				num2 = 9;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (A_0.Length > num)
					{
						num2 = 12;
						continue;
					}
					this.ᜌ[num].ᜀ(new spr\u1A68(pointF, pointF, pointF));
					num2 = 10;
					continue;
				case 2:
					num2 = 11;
					continue;
				case 3:
					this.ᜋ = A_0;
					num2 = 0;
					continue;
				case 4:
					goto IL_177;
				case 5:
					if (true)
					{
					}
					num2 = 6;
					continue;
				case 6:
					if (!this.ᜆ())
					{
						num2 = 3;
						continue;
					}
					return;
				case 7:
					goto IL_168;
				case 8:
					if (num >= this.ᜌ.Length)
					{
						num2 = 2;
						continue;
					}
					num2 = 1;
					continue;
				case 9:
					goto IL_68;
				case 10:
					goto IL_168;
				case 11:
					if (this.ᜋ() == 2)
					{
						num2 = 5;
						continue;
					}
					return;
				case 12:
					this.ᜌ[num].ᜀ(new spr\u1A68(A_0[num].ᜁ(), A_0[num].ᜂ(), A_0[num].ᜀ()));
					pointF = A_0[num].ᜀ();
					num2 = 7;
					continue;
				}
				goto IL_3C;
				IL_168:
				num++;
				num2 = 4;
			}
			IL_68:
			IL_A0:
			num2 = 8;
			goto IL_02;
		}
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x00260ADC File Offset: 0x0025FADC
	internal void ᜁ(spr\u187D[] A_0)
	{
		for (;;)
		{
			PointF pointF = PointF.Empty;
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_3D;
				case 1:
					goto IL_BC;
				case 2:
					goto IL_E0;
				case 3:
					if (num >= this.\u170D.Length)
					{
						num2 = 2;
						continue;
					}
					num2 = 5;
					continue;
				case 4:
					this.\u170D[this.\u170D.Length - 1 - num].ᜀ(new spr\u1A68(A_0[num].ᜁ(), A_0[num].ᜂ(), A_0[num].ᜀ()));
					pointF = A_0[num].ᜀ();
					num2 = 0;
					continue;
				case 5:
					if (A_0.Length > num)
					{
						num2 = 4;
						continue;
					}
					this.\u170D[this.\u170D.Length - 1 - num].ᜀ(new spr\u1A68(pointF, pointF, pointF));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 7;
						continue;
					}
					break;
				case 6:
					goto IL_BC;
				case 7:
					goto IL_3D;
				}
				break;
				IL_3D:
				num++;
				num2 = 6;
				continue;
				IL_BC:
				num2 = 3;
			}
		}
		IL_E0:
		if (true)
		{
		}
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x00260C34 File Offset: 0x0025FC34
	internal void ᜀ(sprᴎ A_0, sprᴎ A_1)
	{
		this.ᜄ = A_1;
		this.ᜅ = A_0;
		this.ᜇ = true;
		if (!this.ᜆ)
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
				this.ᜆ = true;
				this.ᜉ = A_0;
				this.ᜈ = true;
				return;
			}
		}
	}

	// Token: 0x0600266F RID: 9839 RVA: 0x00260CA4 File Offset: 0x0025FCA4
	internal void ᜀ(spr\u187D[] A_0, spr\u17F0[] A_1, spr\u17F0 A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_85:
			this.ᜆ = true;
			this.ᜈ = false;
			this.ᜋ = A_0;
			this.\u170D = A_1;
			num = 0;
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_AC;
			case 1:
				if (!this.ᜆ)
				{
					num = 2;
					continue;
				}
				goto IL_AE;
			case 2:
				goto IL_7B;
			}
			goto IL_3A;
		}
		IL_7B:
		if (true)
		{
		}
		goto IL_85;
		IL_AC:
		IL_AE:
		this.ᜄ = A_2;
		this.ᜇ = false;
		return;
		IL_20:
		if (false)
		{
		}
		IL_3A:
		this.ᜅ = A_0[A_0.Length - 1];
		this.ᜊ = A_0;
		this.ᜌ = A_1;
		num = 1;
		goto IL_28;
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x00260D70 File Offset: 0x0025FD70
	private static ArrayList ᜀ(spr\u1926 A_0)
	{
		switch (0)
		{
		default:
		{
			ArrayList arrayList;
			for (;;)
			{
				arrayList = new ArrayList();
				bool a_ = false;
				spr᪑ a_2 = null;
				int num = 0;
				int num2 = 12;
				for (;;)
				{
					spr᪑ spr᪑;
					switch (num2)
					{
					case 0:
						goto IL_8B;
					case 1:
						return arrayList;
					case 2:
					{
						bool flag;
						if (flag)
						{
							num2 = 6;
							continue;
						}
						goto IL_6C;
					}
					case 3:
						goto IL_106;
					case 4:
						goto IL_B4;
					case 5:
					{
						bool flag = spr\u211F.ᜀ((spr\u17F0)spr᪑, A_0, num, arrayList, a_2, a_);
						num2 = 2;
						continue;
					}
					case 6:
						num++;
						num2 = 9;
						continue;
					case 7:
						if (!(spr᪑ is spr\u17F0))
						{
							goto IL_106;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8B;
						default:
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						break;
					case 8:
						if (spr᪑ is sprᴎ)
						{
							num2 = 0;
							continue;
						}
						num2 = 7;
						continue;
					case 9:
						goto IL_6C;
					case 10:
						goto IL_106;
					case 11:
						if (num >= A_0.ᜉ())
						{
							num2 = 1;
							continue;
						}
						spr᪑ = A_0.ᜀ(num);
						num2 = 8;
						continue;
					case 12:
						goto IL_B4;
					}
					break;
					IL_6C:
					a_ = false;
					num2 = 3;
					continue;
					IL_8B:
					if (true)
					{
					}
					spr\u211F.ᜀ((sprᴎ)spr᪑, A_0, num, arrayList, a_2, a_);
					a_ = true;
					num2 = 10;
					continue;
					IL_B4:
					num2 = 11;
					continue;
					IL_106:
					a_2 = spr᪑;
					num++;
					num2 = 4;
				}
			}
			return arrayList;
		}
		}
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x00260F10 File Offset: 0x0025FF10
	private static bool ᜀ(spr\u17F0 A_0, spr\u1926 A_1, int A_2, ArrayList A_3, spr᪑ A_4, bool A_5)
	{
		bool result;
		for (;;)
		{
			result = false;
			A_3.Add(A_0.ᜀ().ᜂ());
			A_3.Add(A_0.ᜀ().ᜀ());
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_121:
				num = 7;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					result = true;
					num = 1;
					continue;
				case 1:
					return result;
				case 2:
					if (A_4 != null)
					{
						num = 6;
						continue;
					}
					num = 10;
					continue;
				case 3:
					if (spr\u211F.ᜀ(A_0, A_4, false, A_5, A_1, A_2))
					{
						num = 0;
						continue;
					}
					return result;
				case 4:
					result = true;
					num = 11;
					continue;
				case 5:
					if (spr\u211F.ᜀ(A_0, A_1.ᜀ(A_1.ᜉ() - 1), false, A_1.ᜀ(A_1.ᜉ() - 1) is sprᴎ, A_1, A_2))
					{
						num = 4;
						continue;
					}
					return result;
				case 6:
					if (true)
					{
					}
					num = 3;
					continue;
				case 7:
					if (A_1.ᜁ())
					{
						num = 9;
						continue;
					}
					return result;
				case 8:
					goto IL_121;
				case 9:
					num = 5;
					continue;
				case 10:
					if (A_1.ᜉ() != 1)
					{
						num = 8;
						continue;
					}
					return result;
				case 11:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x002610AC File Offset: 0x002600AC
	private static void ᜀ(sprᴎ A_0, spr\u1926 A_1, int A_2, ArrayList A_3, spr᪑ A_4, bool A_5)
	{
		for (;;)
		{
			A_3.AddRange(A_0.ᜀ());
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 1:
					spr\u211F.ᜀ(A_0, A_1.ᜀ(A_1.ᜉ() - 1), true, A_1.ᜀ(A_1.ᜉ() - 1) is sprᴎ, A_1, A_2);
					num = 4;
					continue;
				case 2:
					if (A_4 != null)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 3:
					num = 5;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					}
					goto Block_4;
				case 5:
					if (A_1.ᜁ())
					{
						num = 1;
						continue;
					}
					return;
				case 6:
					goto IL_44;
				}
				break;
				IL_6B:
				if (A_1.ᜉ() == 1)
				{
					return;
				}
				if (true)
				{
				}
				num = 3;
			}
		}
		IL_44:
		spr\u211F.ᜀ(A_0, A_4, true, A_5, A_1, A_2);
		return;
		Block_4:
		if (false)
		{
		}
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x002611B8 File Offset: 0x002601B8
	private static bool ᜀ(spr᪑ A_0, spr᪑ A_1, bool A_2, bool A_3, spr\u1926 A_4, int A_5)
	{
		if (A_2)
		{
			for (;;)
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
					goto IL_23;
				}
			}
			IL_23:
			if (false)
			{
			}
			spr\u211F.ᜀ(A_0, A_1, A_3);
			return false;
		}
		return spr\u211F.ᜀ(A_0, A_1, A_3, A_4, A_5);
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x00261210 File Offset: 0x00260210
	private static bool ᜀ(spr᪑ A_0, spr᪑ A_1, bool A_2, spr\u1926 A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			spr\u17F0 spr_u17F;
			spr\u17F0 spr_u17F2;
			for (;;)
			{
				spr_u17F = (spr\u17F0)A_0;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F2;
					case 1:
					{
						spr\u1A68 spr_u1A;
						if (!sprὍ.ᜀ(spr_u1A.ᜀ(), spr_u17F.ᜀ().ᜂ(), 0.1f))
						{
							num = 0;
							continue;
						}
						return false;
					}
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_51;
						default:
							goto IL_135;
						}
						break;
					case 3:
					{
						if (A_2)
						{
							num = 4;
							continue;
						}
						spr_u17F2 = (spr\u17F0)A_1;
						spr\u1A68 spr_u1A = spr_u17F2.ᜀ();
						num = 1;
						continue;
					}
					case 4:
						goto IL_51;
					}
					break;
					IL_51:
					spr\u211F.ᜀ(A_1, spr_u17F.ᜀ().ᜂ());
					num = 2;
				}
			}
			IL_F2:
			sprᴎ a_ = new sprᴎ(new PointF[]
			{
				spr_u17F2.ᜀ().ᜀ(),
				spr_u17F.ᜀ().ᜂ()
			});
			A_3.ᜀ(A_4, a_);
			return true;
			IL_135:
			if (false)
			{
			}
			return false;
		}
		}
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x00261360 File Offset: 0x00260360
	private static void ᜀ(spr᪑ A_0, spr᪑ A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			sprᴎ sprᴎ;
			for (;;)
			{
				sprᴎ = (sprᴎ)A_0;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u1A68 spr_u1A;
						if (!sprὍ.ᜀ(spr_u1A.ᜀ(), (PointF)sprᴎ.ᜀ()[0], 0.1f))
						{
							num = 2;
							continue;
						}
						return;
					}
					case 1:
					{
						if (A_2)
						{
							num = 3;
							continue;
						}
						spr\u17F0 spr_u17F = (spr\u17F0)A_1;
						spr\u1A68 spr_u1A = spr_u17F.ᜀ();
						num = 0;
						continue;
					}
					case 2:
					{
						spr\u17F0 spr_u17F;
						sprᴎ.ᜀ().Insert(0, spr_u17F.ᜀ().ᜀ());
						num = 4;
						continue;
					}
					case 3:
						goto IL_59;
					case 4:
						return;
					}
					break;
				}
			}
			for (;;)
			{
				IL_59:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_E4;
				}
			}
			IL_E4:
			if (false)
			{
			}
			spr\u211F.ᜀ(A_1, (PointF)sprᴎ.ᜀ()[0]);
			return;
		}
		}
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x00261470 File Offset: 0x00260470
	private static void ᜀ(spr᪑ A_0, PointF A_1)
	{
		for (;;)
		{
			sprᴎ sprᴎ = (sprᴎ)A_0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_79;
				case 1:
					sprᴎ.ᜀ().Add(A_1);
					num = 0;
					continue;
				case 2:
					if (true)
					{
					}
					if (!sprὍ.ᜀ((PointF)sprᴎ.ᜀ()[sprᴎ.ᜀ().Count - 1], A_1, 0.1f))
					{
						num = 1;
						continue;
					}
					goto IL_7B;
				}
				break;
			}
		}
		IL_79:
		IL_7B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_79;
		default:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06002677 RID: 9847 RVA: 0x00261520 File Offset: 0x00260520
	private static float[] ᜀ(float[] A_0, float A_1, float A_2)
	{
		float[] array;
		for (;;)
		{
			array = new float[A_0.Length];
			int num = 0;
			if (true)
			{
			}
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return array;
				case 1:
					if (num >= A_0.Length)
					{
						num2 = 0;
						continue;
					}
					array[num] = A_0[num] * A_1 / A_2;
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_8C;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						if (false)
						{
						}
						goto IL_51;
					}
					break;
				}
				break;
				IL_51:
				num2 = 1;
				continue;
				IL_8C:
				goto IL_51;
			}
		}
		return array;
	}

	// Token: 0x06002678 RID: 9848 RVA: 0x002615BC File Offset: 0x002605BC
	internal spr\u1926 ᜁ()
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

	// Token: 0x06002679 RID: 9849 RVA: 0x00261600 File Offset: 0x00260600
	internal sprᢉ ᜌ()
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

	// Token: 0x0600267A RID: 9850 RVA: 0x00261644 File Offset: 0x00260644
	internal spr\u1B70 ᜂ()
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
		return this.ᜂ;
	}

	// Token: 0x0600267B RID: 9851 RVA: 0x00261688 File Offset: 0x00260688
	internal spr᪑ ᜇ()
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
		return this.ᜄ;
	}

	// Token: 0x0600267C RID: 9852 RVA: 0x002616CC File Offset: 0x002606CC
	internal object ᜅ()
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
		return this.ᜅ;
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x00261710 File Offset: 0x00260710
	internal void ᜀ(object A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x00261754 File Offset: 0x00260754
	internal bool ᜉ()
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
		return this.ᜇ;
	}

	// Token: 0x0600267F RID: 9855 RVA: 0x00261798 File Offset: 0x00260798
	internal bool ᜆ()
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
		return this.ᜈ;
	}

	// Token: 0x06002680 RID: 9856 RVA: 0x002617DC File Offset: 0x002607DC
	internal sprᴎ ᜃ()
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
		return this.ᜉ;
	}

	// Token: 0x06002681 RID: 9857 RVA: 0x00261820 File Offset: 0x00260820
	internal spr\u187D[] ᜀ()
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
		return this.ᜊ;
	}

	// Token: 0x06002682 RID: 9858 RVA: 0x00261864 File Offset: 0x00260864
	internal spr\u187D[] ᜊ()
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
		return this.ᜋ;
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x002618A8 File Offset: 0x002608A8
	internal bool ᜄ()
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
		return this.ᜎ;
	}

	// Token: 0x06002684 RID: 9860 RVA: 0x002618EC File Offset: 0x002608EC
	internal void ᜀ(bool A_0)
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
		this.ᜎ = A_0;
	}

	// Token: 0x06002685 RID: 9861 RVA: 0x00261930 File Offset: 0x00260930
	internal bool ᜈ()
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
		return this.ᜏ;
	}

	// Token: 0x06002686 RID: 9862 RVA: 0x00261974 File Offset: 0x00260974
	internal int ᜋ()
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
		return this.ᜐ;
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x002619B8 File Offset: 0x002609B8
	internal void ᜀ(int A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x04002251 RID: 8785
	private const float ᜀ = 0.1f;

	// Token: 0x04002252 RID: 8786
	private readonly sprᢉ ᜁ;

	// Token: 0x04002253 RID: 8787
	private readonly spr\u1B70 ᜂ;

	// Token: 0x04002254 RID: 8788
	private spr\u1926 ᜃ;

	// Token: 0x04002255 RID: 8789
	private spr᪑ ᜄ;

	// Token: 0x04002256 RID: 8790
	private object ᜅ;

	// Token: 0x04002257 RID: 8791
	private bool ᜆ;

	// Token: 0x04002258 RID: 8792
	private bool ᜇ;

	// Token: 0x04002259 RID: 8793
	private bool ᜈ;

	// Token: 0x0400225A RID: 8794
	private sprᴎ ᜉ;

	// Token: 0x0400225B RID: 8795
	private spr\u187D[] ᜊ;

	// Token: 0x0400225C RID: 8796
	private spr\u187D[] ᜋ;

	// Token: 0x0400225D RID: 8797
	private spr\u17F0[] ᜌ;

	// Token: 0x0400225E RID: 8798
	private spr\u17F0[] \u170D;

	// Token: 0x0400225F RID: 8799
	private bool ᜎ;

	// Token: 0x04002260 RID: 8800
	private bool ᜏ;

	// Token: 0x04002261 RID: 8801
	private int ᜐ;
}
