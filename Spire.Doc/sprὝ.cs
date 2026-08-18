using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x020003A2 RID: 930
internal class sprὝ : sprᢿ
{
	// Token: 0x0600348E RID: 13454 RVA: 0x00304430 File Offset: 0x00303430
	internal static spr\u1B70[] ᜃ(spr\u1B70 A_0)
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
		sprὝ sprὝ = new sprὝ();
		return sprὝ.ᜁ(A_0);
	}

	// Token: 0x0600348F RID: 13455 RVA: 0x00304478 File Offset: 0x00303478
	internal static spr\u1B70 ᜀ(spr\u1B70 A_0, float A_1)
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
		sprὝ sprὝ = new sprὝ();
		return sprὝ.ᜀ(A_0, new sprᢉ(1f, A_1));
	}

	// Token: 0x06003490 RID: 13456 RVA: 0x003044CC File Offset: 0x003034CC
	private spr\u1B70[] ᜁ(spr\u1B70 A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				ArrayList arrayList;
				switch (num)
				{
				case 0:
					goto IL_22C;
				case 1:
					if (A_0.ᜆ().ᜊ().Length == 2)
					{
						num = 25;
						continue;
					}
					goto IL_B4;
				case 2:
					if (A_0.ᜆ().ᜊ() != sprὝ.ᜂ)
					{
						num = 12;
						continue;
					}
					goto IL_31D;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_2B2;
				case 5:
					num = 17;
					continue;
				case 6:
					if (A_0.ᜆ().ᜊ().Length != 0)
					{
						num = 3;
						continue;
					}
					goto IL_31D;
				case 7:
					num = 6;
					continue;
				case 8:
					if (A_0.ᜆ().ᜆ() == PenAlignment.Inset)
					{
						num = 4;
						continue;
					}
					num = 18;
					continue;
				case 9:
					goto IL_22C;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_358;
					default:
					{
						if (false)
						{
						}
						if (!sprὝ.ᜀ(A_0.ᜆ(), arrayList))
						{
							num = 20;
							continue;
						}
						this.ᜁ = new spr\u1B70[arrayList.Count];
						int num2 = 0;
						num = 9;
						continue;
					}
					}
					break;
				case 11:
					goto IL_250;
				case 12:
					num = 1;
					continue;
				case 13:
					goto IL_353;
				case 14:
					num = 8;
					continue;
				case 15:
				{
					int num2;
					if (num2 >= arrayList.Count)
					{
						num = 11;
						continue;
					}
					this.ᜁ[num2] = this.ᜀ(A_0, (sprᢉ)arrayList[num2]);
					num2++;
					num = 0;
					continue;
				}
				case 17:
					if (A_0.ᜆ().ᜊ()[1] == 1f)
					{
						num = 24;
						continue;
					}
					goto IL_B4;
				case 18:
					if (A_0.ᜆ().ᜊ() != null)
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					goto IL_31D;
				case 19:
					if (A_0.ᜆ().ᜇ() == DashCap.Round)
					{
						num = 13;
						continue;
					}
					goto IL_13F;
				case 20:
					goto IL_187;
				case 21:
					if (A_0.ᜆ().ᜊ()[0] == 0f)
					{
						num = 5;
						continue;
					}
					goto IL_B4;
				case 22:
					num = 19;
					continue;
				case 23:
					if (A_0.ᜆ().\u170D() == DashStyle.Dot)
					{
						num = 22;
						continue;
					}
					goto IL_13F;
				case 24:
					goto IL_1E8;
				case 25:
					num = 21;
					continue;
				}
				if (A_0.ᜆ() != null)
				{
					num = 14;
					continue;
				}
				break;
				IL_B4:
				num = 23;
				continue;
				IL_13F:
				arrayList = new ArrayList();
				num = 10;
				continue;
				IL_22C:
				num = 15;
			}
			IL_132:
			return new spr\u1B70[]
			{
				A_0
			};
			IL_187:
			return new spr\u1B70[]
			{
				A_0
			};
			IL_1E8:
			goto IL_31D;
			IL_250:
			goto IL_358;
			IL_2B2:
			goto IL_132;
			IL_31D:
			return new spr\u1B70[]
			{
				A_0
			};
			IL_353:
			return new spr\u1B70[]
			{
				A_0
			};
			IL_358:
			return this.ᜁ;
		}
		}
	}

	// Token: 0x06003491 RID: 13457 RVA: 0x00304838 File Offset: 0x00303838
	private spr\u1B70 ᜀ(spr\u1B70 A_0, sprᢉ A_1)
	{
		spr\u1B70 result;
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ = new spr\u211F(A_0, A_1);
			A_0.ᜀ(this);
			result = this.ᜀ.ᜂ();
		}
		catch (Exception)
		{
			result = new spr\u1B70();
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06003492 RID: 13458 RVA: 0x003048B0 File Offset: 0x003038B0
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
		this.ᜀ.ᜁ(A_0);
	}

	// Token: 0x06003493 RID: 13459 RVA: 0x003048F8 File Offset: 0x003038F8
	public override void ᜁ(spr\u1926 A_0)
	{
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_7A;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (!A_0.ᜁ())
				{
					num = 0;
					continue;
				}
				goto IL_7C;
			case 3:
				num = 2;
				continue;
			}
			if (A_0.ᜉ() <= 1)
			{
				break;
			}
			num = 3;
		}
		return;
		IL_7A:
		return;
		IL_7C:
		this.ᜆ();
	}

	// Token: 0x06003494 RID: 13460 RVA: 0x00304988 File Offset: 0x00303988
	public override void ᜀ(sprᴎ A_0)
	{
		for (;;)
		{
			IL_42:
			spr\u211F spr_u211F = this.ᜀ;
			spr_u211F.ᜀ(spr_u211F.ᜋ() + 1);
			sprᴎ sprᴎ = this.ᜁ(A_0, this.ᜀ.ᜌ().ᜀ());
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_42;
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
						if (sprᴎ != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						this.ᜀ.ᜁ().ᜁ(sprᴎ);
						num = 2;
						continue;
					case 2:
						return;
					}
					goto IL_42;
				}
			}
		}
	}

	// Token: 0x06003495 RID: 13461 RVA: 0x00304A38 File Offset: 0x00303A38
	public override void ᜀ(spr\u17F0 A_0)
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
		spr\u211F spr_u211F = this.ᜀ;
		spr_u211F.ᜀ(spr_u211F.ᜋ() + 1);
		spr\u17F0[] a_ = this.ᜀ(A_0, this.ᜀ.ᜌ().ᜀ());
		this.ᜀ.ᜁ().ᜀ(a_);
	}

	// Token: 0x06003496 RID: 13462 RVA: 0x00304AB0 File Offset: 0x00303AB0
	private void ᜆ()
	{
		while (this.ᜀ.ᜆ())
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
				this.ᜅ();
				return;
			}
		}
		this.ᜄ();
	}

	// Token: 0x06003497 RID: 13463 RVA: 0x00304B08 File Offset: 0x00303B08
	private void ᜅ()
	{
		while (this.ᜀ.ᜉ())
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
				this.ᜀ();
				return;
			}
		}
		this.ᜁ();
	}

	// Token: 0x06003498 RID: 13464 RVA: 0x00304B60 File Offset: 0x00303B60
	private void ᜄ()
	{
		for (;;)
		{
			if (true)
			{
			}
			if (!this.ᜀ.ᜉ())
			{
				goto IL_3A;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2B;
			}
		}
		IL_2B:
		if (false)
		{
		}
		this.ᜂ();
		return;
		IL_3A:
		this.ᜃ();
	}

	// Token: 0x06003499 RID: 13465 RVA: 0x00304BB8 File Offset: 0x00303BB8
	private void ᜃ()
	{
		spr\u2415 spr_u;
		for (;;)
		{
			spr_u = spr\u24F7.ᜀ(this.ᜀ.ᜊ(), this.ᜀ.ᜀ(), true);
			if (!spr_u.ᜅ())
			{
				goto IL_76;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_3B;
			}
		}
		IL_3B:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ.ᜁ().ᜂ(new PointF[]
		{
			spr_u.ᜁ()
		});
		return;
		IL_76:
		spr\u187D[] a_ = spr_u.ᜄ();
		spr\u187D[] a_2 = spr_u.ᜃ();
		this.ᜀ.ᜁ(a_);
		this.ᜀ.ᜀ(a_2);
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x00304C6C File Offset: 0x00303C6C
	private void ᜂ()
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
		spr\u187D[] a_ = spr\u24F7.ᜀ((sprᴎ)this.ᜀ.ᜅ(), this.ᜀ.ᜊ(), false);
		this.ᜀ.ᜁ(a_);
	}

	// Token: 0x0600349B RID: 13467 RVA: 0x00304CD8 File Offset: 0x00303CD8
	private void ᜁ()
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
		spr\u187D[] a_ = spr\u24F7.ᜀ(this.ᜀ.ᜃ(), this.ᜀ.ᜀ(), true);
		this.ᜀ.ᜀ(a_);
	}

	// Token: 0x0600349C RID: 13468 RVA: 0x00304D3C File Offset: 0x00303D3C
	private void ᜀ()
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
				{
					if (((sprᴎ)this.ᜀ.ᜅ()).ᜀ().Count < 2)
					{
						num = 2;
						continue;
					}
					sprᴎ sprᴎ = (sprᴎ)this.ᜀ.ᜅ();
					spr\u1B7C a_ = new spr\u1B7C((PointF)this.ᜀ.ᜃ().ᜀ()[0], (PointF)this.ᜀ.ᜃ().ᜀ()[1]);
					spr\u1B7C a_2 = new spr\u1B7C((PointF)sprᴎ.ᜀ()[sprᴎ.ᜀ().Count - 2], (PointF)sprᴎ.ᜀ()[sprᴎ.ᜀ().Count - 1]);
					PointF[] array = new PointF[]
					{
						PointF.Empty
					};
					bool flag = spr\u1B7C.ᜀ(a_, a_2, array, true);
					goto IL_11A;
				}
				case 1:
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11A;
					default:
						goto IL_19C;
					}
					break;
				case 3:
					return;
				case 4:
				{
					PointF[] array;
					this.ᜀ.ᜃ().ᜀ()[0] = array[0];
					sprᴎ sprᴎ;
					((sprᴎ)this.ᜀ.ᜅ()).ᜀ()[sprᴎ.ᜀ().Count - 1] = array[0];
					num = 3;
					continue;
				}
				case 5:
				{
					bool flag;
					if (flag)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return;
				}
				}
				if (this.ᜀ.ᜃ().ᜀ().Count >= 2)
				{
					num = 1;
					continue;
				}
				break;
				IL_11A:
				num = 5;
			}
			return;
			IL_19C:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x0600349D RID: 13469 RVA: 0x00304F5C File Offset: 0x00303F5C
	private sprᴎ ᜁ(sprᴎ A_0, float A_1)
	{
		sprᴎ sprᴎ;
		for (;;)
		{
			int count = A_0.ᜀ().Count;
			int num = 7;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_EE;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_89;
				case 3:
					return A_0;
				case 4:
					goto IL_74;
				case 5:
				{
					int num2;
					switch (num2)
					{
					case 1:
						sprᴎ = this.ᜁ(A_0);
						num = 0;
						continue;
					case 2:
						sprᴎ = this.ᜀ(A_0, A_1);
						num = 6;
						continue;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 6:
					goto IL_63;
				case 7:
					if (count < 1)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_74;
					default:
					{
						if (false)
						{
						}
						int num2 = count;
						num = 5;
						continue;
					}
					}
					break;
				}
				break;
				IL_74:
				sprᴎ = this.ᜀ(A_0, count, A_1);
				num = 2;
			}
		}
		return A_0;
		IL_63:
		IL_89:
		IL_EE:
		this.ᜀ(A_0, sprᴎ);
		return sprᴎ;
	}

	// Token: 0x0600349E RID: 13470 RVA: 0x00305064 File Offset: 0x00304064
	private sprᴎ ᜁ(sprᴎ A_0)
	{
		switch (0)
		{
		default:
		{
			PointF pointF;
			PointF pointF2;
			for (;;)
			{
				pointF = PointF.Empty;
				pointF2 = PointF.Empty;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						sprᴎ sprᴎ = (sprᴎ)this.ᜀ.ᜅ();
						pointF2 = (PointF)sprᴎ.ᜀ()[sprᴎ.ᜀ().Count - 1];
						if (true)
						{
						}
						num = 8;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_102;
						default:
							if (false)
							{
							}
							pointF = ((spr\u17F0)this.ᜀ.ᜇ()).ᜀ().ᜀ();
							num = 3;
							continue;
						}
						break;
					case 2:
						if (this.ᜀ.ᜅ() is sprᴎ)
						{
							num = 0;
							continue;
						}
						goto IL_1F0;
					case 3:
						if (this.ᜀ.ᜅ() is spr\u17F0)
						{
							num = 5;
							continue;
						}
						goto IL_1F0;
					case 4:
						goto IL_1EB;
					case 5:
						pointF2 = ((spr\u17F0)this.ᜀ.ᜅ()).ᜀ().ᜀ();
						num = 4;
						continue;
					case 6:
						goto IL_102;
					case 7:
						if (this.ᜀ.ᜇ() is sprᴎ)
						{
							num = 6;
							continue;
						}
						goto IL_1F0;
					case 8:
						goto IL_1BE;
					case 9:
						if (this.ᜀ.ᜇ() is spr\u17F0)
						{
							num = 1;
							continue;
						}
						num = 7;
						continue;
					}
					break;
					IL_102:
					sprᴎ sprᴎ2 = (sprᴎ)this.ᜀ.ᜇ();
					pointF = (PointF)sprᴎ2.ᜀ()[sprᴎ2.ᜀ().Count - 1];
					num = 2;
				}
			}
			IL_1BE:
			IL_1EB:
			IL_1F0:
			float num2 = pointF2.X - pointF.X;
			float num3 = pointF2.Y - pointF.Y;
			PointF pointF3 = (PointF)A_0.ᜀ()[0];
			PointF pointF4 = new PointF(pointF3.X + num2, pointF3.Y + num3);
			return new sprᴎ(new PointF[]
			{
				pointF4
			});
		}
		}
	}

	// Token: 0x0600349F RID: 13471 RVA: 0x003052D0 File Offset: 0x003042D0
	private sprᴎ ᜀ(sprᴎ A_0, float A_1)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			spr\u1B7C spr_u1B7C;
			PointF[] array2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				for (;;)
				{
					IL_36:
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						A_1 *= -1f;
						num = 2;
						continue;
					case 1:
						if (this.ᜀ((PointF)A_0.ᜀ()[0], (PointF)A_0.ᜀ()[1]))
						{
							num = 0;
							continue;
						}
						goto IL_EE;
					case 2:
						goto IL_EC;
					}
					goto IL_51;
				}
				IL_EC:
				IL_EE:
				spr\u1B7C a_ = spr_u1B7C.ᜁ(A_1);
				spr\u1B7C a_2 = spr_u1B7C.ᜃ((PointF)A_0.ᜀ()[0]);
				spr\u1B7C a_3 = spr_u1B7C.ᜃ((PointF)A_0.ᜀ()[1]);
				PointF[] array = new PointF[]
				{
					PointF.Empty,
					PointF.Empty
				};
				spr\u1B7C.ᜁ(a_, a_2, array2);
				array[0] = array2[0];
				spr\u1B7C.ᜁ(a_, a_3, array2);
				array[1] = array2[0];
				return new sprᴎ(array);
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_51:
			array2 = new PointF[]
			{
				PointF.Empty
			};
			spr_u1B7C = new spr\u1B7C((PointF)A_0.ᜀ()[0], (PointF)A_0.ᜀ()[1]);
			num = 1;
			goto IL_36;
		}
		}
	}

	// Token: 0x060034A0 RID: 13472 RVA: 0x00305480 File Offset: 0x00304480
	private sprᴎ ᜀ(sprᴎ A_0, int A_1, float A_2)
	{
		bool a_;
		for (;;)
		{
			IL_3A:
			a_ = this.ᜀ.ᜄ();
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						A_1--;
						a_ = true;
						num = 1;
						continue;
					case 1:
						goto IL_95;
					case 2:
						if (sprὍ.ᜀ((PointF)A_0.ᜀ()[0], (PointF)A_0.ᜀ()[A_1 - 1]))
						{
							num = 0;
							continue;
						}
						goto IL_97;
					}
					goto IL_3A;
				}
			}
		}
		IL_95:
		IL_97:
		if (true)
		{
		}
		ArrayList arrayList = new ArrayList(A_1);
		this.ᜀ(A_0, arrayList, A_1, A_2);
		sprᴎ result = new sprᴎ(this.ᜀ(A_0, (spr\u1B7C[])arrayList.ToArray(typeof(spr\u1B7C)), A_2, A_1));
		this.ᜀ.ᜀ(a_);
		return result;
	}

	// Token: 0x060034A1 RID: 13473 RVA: 0x00305570 File Offset: 0x00304570
	private void ᜀ(sprᴎ A_0, ArrayList A_1, int A_2, float A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_3B:
				int num = 0;
				for (;;)
				{
					IL_3D:
					if (true)
					{
					}
					int num2 = 5;
					for (;;)
					{
						float num3;
						spr\u1B7C spr_u1B7C;
						switch (num2)
						{
						case 0:
						{
							bool flag2;
							bool flag = this.ᜀ((PointF)A_0.ᜀ()[num], (PointF)A_0.ᜀ()[flag2 ? 0 : (num + 1)]);
							num3 = A_3;
							num2 = 4;
							continue;
						}
						case 1:
						{
							if (num >= A_2)
							{
								num2 = 3;
								continue;
							}
							bool flag2 = num == A_2 - 1;
							num2 = 8;
							continue;
						}
						case 2:
							goto IL_137;
						case 3:
							return;
						case 4:
						{
							bool flag;
							if (flag)
							{
								num2 = 6;
								continue;
							}
							goto IL_137;
						}
						case 5:
							goto IL_158;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3D;
							default:
								if (false)
								{
								}
								num3 *= -1f;
								num2 = 2;
								continue;
							}
							break;
						case 7:
							goto IL_158;
						case 8:
						{
							bool flag2;
							spr_u1B7C = new spr\u1B7C((PointF)A_0.ᜀ()[num], (PointF)A_0.ᜀ()[flag2 ? 0 : (num + 1)]);
							num2 = 0;
							continue;
						}
						}
						goto IL_3B;
						IL_137:
						A_1.Add(spr_u1B7C.ᜁ(num3));
						num++;
						num2 = 7;
						continue;
						IL_158:
						num2 = 1;
					}
				}
			}
			return;
		}
	}

	// Token: 0x060034A2 RID: 13474 RVA: 0x003056F8 File Offset: 0x003046F8
	private PointF[] ᜀ(sprᴎ A_0, spr\u1B7C[] A_1, float A_2, int A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_F7:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_87;
			}
			break;
		}
		int num2;
		PointF[] array;
		PointF[] array2;
		for (;;)
		{
			IL_2C:
			bool flag;
			bool flag5;
			switch (num)
			{
			case 0:
				if (flag)
				{
					num = 13;
					continue;
				}
				num = 3;
				continue;
			case 1:
				goto IL_271;
			case 2:
			{
				bool flag2;
				if (!flag2)
				{
					num = 14;
					continue;
				}
				num = 20;
				continue;
			}
			case 3:
			{
				bool flag2;
				bool flag3 = spr\u1B7C.ᜀ(A_1[num2], A_1[flag2 ? 0 : (num2 + 1)], array, true);
				num = 18;
				continue;
			}
			case 4:
			{
				bool flag2;
				array2[flag2 ? 0 : (num2 + 1)] = array[0];
				num2++;
				num = 15;
				continue;
			}
			case 5:
			{
				bool flag2;
				spr\u1B7C a_ = new spr\u1B7C((PointF)A_0.ᜀ()[flag2 ? 0 : (A_3 - 2)], (PointF)A_0.ᜀ()[flag2 ? 1 : (A_3 - 1)]).ᜃ((PointF)A_0.ᜀ()[flag2 ? 0 : (A_3 - 1)]);
				num = 16;
				continue;
			}
			case 6:
				if (!this.ᜀ.ᜄ())
				{
					num = 8;
					continue;
				}
				num = 12;
				continue;
			case 7:
				goto IL_2B3;
			case 8:
				goto IL_F7;
			case 9:
				this.ᜀ(A_0, A_2, A_1, num2, array);
				num = 1;
				continue;
			case 10:
			{
				if (num2 >= A_1.Length)
				{
					num = 19;
					continue;
				}
				bool flag2 = num2 == A_1.Length - 1;
				bool flag4 = num2 == A_1.Length - 2;
				num = 6;
				continue;
			}
			case 11:
			{
				bool flag4;
				flag5 = flag4;
				goto IL_144;
			}
			case 12:
				flag5 = false;
				goto IL_144;
			case 13:
				num = 5;
				continue;
			case 14:
				num = 11;
				continue;
			case 15:
				goto IL_2B3;
			case 16:
			{
				bool flag2;
				spr\u1B7C a_;
				spr\u1B7C.ᜁ(A_1[flag2 ? 0 : (A_3 - 2)], a_, array);
				if (true)
				{
				}
				num = 17;
				continue;
			}
			case 17:
				goto IL_271;
			case 18:
			{
				bool flag3;
				if (!flag3)
				{
					num = 9;
					continue;
				}
				goto IL_271;
			}
			case 19:
				return array2;
			case 20:
				flag5 = true;
				goto IL_144;
			}
			goto IL_87;
			IL_144:
			flag = flag5;
			num = 0;
			continue;
			IL_271:
			num = 4;
			continue;
			IL_2B3:
			num = 10;
		}
		return array2;
		IL_87:
		array = new PointF[]
		{
			PointF.Empty
		};
		array2 = new PointF[A_3];
		num2 = 0;
		num = 7;
		goto IL_2C;
	}

	// Token: 0x060034A3 RID: 13475 RVA: 0x003059E8 File Offset: 0x003049E8
	private void ᜀ(sprᴎ A_0, float A_1, spr\u1B7C[] A_2, int A_3, PointF[] A_4)
	{
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
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = A_3 == A_0.ᜀ().Count - 1;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							bool flag2;
							if (!flag2)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_D9;
						}
						case 1:
							num2 *= -1f;
							num = 2;
							continue;
						case 2:
							goto IL_93;
						case 3:
						{
							bool flag2 = this.ᜀ((PointF)A_0.ᜀ()[A_3], (PointF)A_0.ᜀ()[flag ? 0 : (A_3 + 1)]);
							num2 = A_1;
							num = 0;
							continue;
						}
						}
						break;
					}
				}
				IL_93:
				break;
			}
			break;
		}
		IL_D9:
		spr\u1B7C spr_u1B7C = A_2[A_3].ᜁ(num2);
		spr\u1B7C spr_u1B7C2 = spr_u1B7C.ᜃ((PointF)A_0.ᜀ()[A_3]);
		spr\u1B7C.ᜁ(A_2[A_3], spr_u1B7C2.ᜁ(num2), A_4);
	}

	// Token: 0x060034A4 RID: 13476 RVA: 0x00305B08 File Offset: 0x00304B08
	private spr\u17F0[] ᜀ(spr\u17F0 A_0, float A_1)
	{
		spr\u187D[] array2;
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
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u187D[] array = A_0.ᜀ().ᜁ();
					array2 = new spr\u187D[array.Length];
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_70;
						case 1:
							if (num >= array.Length)
							{
								num2 = 3;
								continue;
							}
							array2[num] = this.ᜀ(array[num], A_1);
							num++;
							num2 = 2;
							continue;
						case 2:
							goto IL_70;
						case 3:
							goto IL_92;
						}
						break;
						IL_70:
						num2 = 1;
					}
				}
				IL_92:
				break;
			}
			break;
		}
		spr\u24F7.ᜀ(array2);
		array2 = this.ᜂ(array2);
		return this.ᜀ(A_0, array2);
	}

	// Token: 0x060034A5 RID: 13477 RVA: 0x00305BF0 File Offset: 0x00304BF0
	private spr\u187D ᜀ(spr\u187D A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			float num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_164:
				num *= -1f;
				num2 = 1;
				break;
			case 1:
				goto IL_2E;
			default:
				goto IL_2E;
			}
			spr\u1B7C spr_u1B7C;
			PointF[] array;
			PointF pointF;
			for (;;)
			{
				IL_36:
				switch (num2)
				{
				case 0:
					goto IL_10B;
				case 1:
					goto IL_109;
				case 2:
					num *= -1f;
					num2 = 0;
					continue;
				case 3:
					if (this.ᜀ(A_0.ᜂ(), A_0.ᜀ()))
					{
						if (true)
						{
						}
						num2 = 5;
						continue;
					}
					goto IL_17E;
				case 4:
					if (this.ᜀ(A_0.ᜁ(), A_0.ᜂ()))
					{
						num2 = 2;
						continue;
					}
					goto IL_10B;
				case 5:
					goto IL_164;
				}
				goto IL_55;
				IL_10B:
				spr_u1B7C.ᜀ(A_0.ᜁ(), num, array);
				pointF = array[0];
				num = A_1;
				num2 = 3;
			}
			IL_109:
			IL_17E:
			spr\u1B7C spr_u1B7C2;
			spr_u1B7C2.ᜀ(A_0.ᜀ(), num, array);
			PointF pointF2 = array[0];
			spr\u1B7C spr_u1B7C3;
			spr\u1B7C spr_u1B7C4;
			PointF a_ = sprὝ.ᜀ(A_0, spr_u1B7C3, spr_u1B7C4, pointF, pointF2);
			spr\u187D result;
			result.ᜀ(pointF);
			result.ᜂ(pointF2);
			result.ᜁ(a_);
			return result;
			IL_2E:
			if (false)
			{
			}
			IL_55:
			result = default(spr\u187D);
			spr_u1B7C3 = new spr\u1B7C(A_0.ᜁ(), A_0.ᜂ());
			spr_u1B7C4 = new spr\u1B7C(A_0.ᜀ(), A_0.ᜂ());
			spr_u1B7C = spr_u1B7C3.ᜃ(A_0.ᜁ());
			spr_u1B7C2 = spr_u1B7C4.ᜃ(A_0.ᜀ());
			array = new PointF[]
			{
				PointF.Empty
			};
			num = A_1;
			num2 = 4;
			goto IL_36;
		}
		}
	}

	// Token: 0x060034A6 RID: 13478 RVA: 0x00305DC8 File Offset: 0x00304DC8
	private static PointF ᜀ(spr\u187D A_0, spr\u1B7C A_1, spr\u1B7C A_2, PointF A_3, PointF A_4)
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
			switch (0)
			{
			}
			break;
		}
		PointF[] array;
		for (;;)
		{
			array = new PointF[]
			{
				PointF.Empty
			};
			bool flag = spr\u1B7C.ᜀ(A_1, A_2);
			spr\u1B7C a_ = A_1.ᜄ(A_3);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u1B7C a_2 = A_1.ᜃ(A_0.ᜂ());
					spr\u1B7C.ᜁ(a_, a_2, array);
					num = 1;
					continue;
				}
				case 1:
					goto IL_D4;
				case 2:
					goto IL_B0;
				case 3:
				{
					if (flag)
					{
						num = 0;
						continue;
					}
					spr\u1B7C a_3 = A_2.ᜄ(A_4);
					spr\u1B7C.ᜁ(a_, a_3, array);
					num = 2;
					continue;
				}
				}
				break;
			}
		}
		IL_B0:
		IL_D4:
		if (true)
		{
		}
		return array[0];
	}

	// Token: 0x060034A7 RID: 13479 RVA: 0x00305EC0 File Offset: 0x00304EC0
	private spr\u187D[] ᜂ(spr\u187D[] A_0)
	{
		int num = 6;
		for (;;)
		{
			spr\u187D[] array;
			switch (num)
			{
			case 0:
				if (!this.ᜀ.ᜉ())
				{
					goto IL_96;
				}
				num = 3;
				continue;
			case 1:
				array = this.ᜁ(A_0);
				goto IL_A0;
			case 2:
				num = 1;
				continue;
			case 3:
				array = this.ᜀ(A_0);
				goto IL_A0;
			case 4:
				num = 0;
				continue;
			case 5:
				return A_0;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_96;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (this.ᜀ.ᜅ() != null)
			{
				num = 4;
				continue;
			}
			break;
			IL_96:
			num = 2;
			continue;
			IL_A0:
			A_0 = array;
			if (true)
			{
			}
			num = 5;
		}
		return A_0;
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x00305F90 File Offset: 0x00304F90
	private spr\u187D[] ᜁ(spr\u187D[] A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2415 spr_u = spr\u24F7.ᜀ(this.ᜀ.ᜀ(), A_0, false);
				int num = 7;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						spr\u187D[] array;
						if (num2 >= array.Length)
						{
							num = 5;
							continue;
						}
						if (true)
						{
						}
						A_0[num2] = array[array.Length - 1 - num2];
						num2++;
						num = 6;
						continue;
					}
					case 1:
					{
						spr\u187D[] array;
						if (array.Length != A_0.Length)
						{
							num = 2;
							continue;
						}
						goto IL_185;
					}
					case 2:
					{
						spr\u187D[] array;
						A_0 = new spr\u187D[array.Length];
						num = 3;
						continue;
					}
					case 3:
						goto IL_185;
					case 4:
						goto IL_E7;
					case 5:
						return A_0;
					case 6:
						goto IL_E7;
					case 7:
						if (spr_u.ᜅ())
						{
							num = 9;
							continue;
						}
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
							spr\u187D[] array2 = spr_u.ᜄ();
							spr\u187D[] array = spr_u.ᜃ();
							array2[array2.Length - 1].ᜂ(array[array.Length - 1].ᜁ());
							this.ᜀ.ᜀ(array2);
							num = 1;
							continue;
						}
						}
						break;
					case 8:
						return A_0;
					case 9:
						this.ᜀ(spr_u, A_0);
						num = 8;
						continue;
					}
					break;
					IL_E7:
					num = 0;
					continue;
					IL_185:
					num2 = 0;
					num = 4;
				}
			}
			return A_0;
		}
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x00306138 File Offset: 0x00305138
	private void ᜀ(spr\u2415 A_0, spr\u187D[] A_1)
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
		PointF pointF = spr\u24F7.ᜀ(A_1, false, new PointF[]
		{
			A_1[A_1.Length - 1].ᜂ(),
			A_1[A_1.Length - 1].ᜁ()
		}, A_0.ᜁ());
		this.ᜀ.ᜁ().ᜂ(new PointF[]
		{
			pointF
		});
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x003061E8 File Offset: 0x003051E8
	private spr\u187D[] ᜀ(spr\u187D[] A_0)
	{
		for (;;)
		{
			spr\u187D[] array = spr\u24F7.ᜀ((sprᴎ)this.ᜀ.ᜅ(), A_0, false);
			int num = 1;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (num2 >= array.Length)
					{
						num = 2;
						continue;
					}
					A_0[num2] = array[array.Length - 1 - num2];
					num2++;
					num = 3;
					continue;
				case 1:
					if (array.Length != A_0.Length)
					{
						num = 5;
						continue;
					}
					goto IL_8F;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						goto IL_F1;
					}
					break;
				case 3:
					goto IL_B5;
				case 4:
					goto IL_B5;
				case 5:
					A_0 = new spr\u187D[array.Length];
					num = 6;
					continue;
				case 6:
					goto IL_8F;
				}
				break;
				IL_91:
				num = 4;
				continue;
				IL_8F:
				num2 = 0;
				goto IL_91;
				IL_B5:
				num = 0;
			}
		}
		IL_F1:
		if (false)
		{
		}
		return A_0;
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x003062F0 File Offset: 0x003052F0
	private void ᜀ(sprᴎ A_0, sprᴎ A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_AD;
			case 1:
				spr\u24F7.ᜀ(A_1, (sprᴎ)this.ᜀ.ᜅ());
				num = 3;
				continue;
			case 2:
			{
				if (this.ᜀ.ᜉ())
				{
					num = 1;
					continue;
				}
				spr\u187D[] array = spr\u24F7.ᜀ(A_1, this.ᜀ.ᜀ(), true);
				this.ᜀ.ᜀ(array);
				this.ᜀ.ᜀ(array[array.Length - 1]);
				num = 0;
				continue;
			}
			case 3:
				goto IL_D0;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
				goto IL_D2;
			}
			if (this.ᜀ.ᜅ() != null)
			{
				num = 5;
				continue;
			}
			break;
			IL_D2:
			num = 2;
		}
		IL_AD:
		IL_D0:
		this.ᜀ.ᜀ(A_1, A_0);
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x00306410 File Offset: 0x00305410
	private spr\u17F0[] ᜀ(spr\u17F0 A_0, spr\u187D[] A_1)
	{
		if (true)
		{
		}
		spr\u17F0[] array;
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_73:
			array[num] = new spr\u17F0(A_1[num].ᜁ(), A_1[num].ᜂ(), A_1[num].ᜀ());
			num++;
			num2 = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		for (;;)
		{
			IL_30:
			switch (num2)
			{
			case 0:
				if (num >= A_1.Length)
				{
					num2 = 3;
					continue;
				}
				goto IL_73;
			case 1:
				goto IL_5B;
			case 2:
				goto IL_5B;
			case 3:
				goto IL_71;
			}
			goto IL_46;
			IL_5B:
			num2 = 0;
		}
		IL_71:
		this.ᜀ.ᜀ(A_1, array, A_0);
		return array;
		IL_46:
		array = new spr\u17F0[A_1.Length];
		num = 0;
		num2 = 1;
		goto IL_30;
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x003064DC File Offset: 0x003054DC
	private bool ᜀ(PointF A_0, PointF A_1)
	{
		float num;
		float num2;
		for (;;)
		{
			num = A_1.X - A_0.X;
			num2 = A_1.Y - A_0.Y;
			int num3 = 4;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_80;
				case 1:
					num *= -1f;
					num2 *= -1f;
					num3 = 0;
					continue;
				case 2:
					if (num != 0f)
					{
						num3 = 3;
						continue;
					}
					goto IL_CF;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						goto IL_71;
					}
					break;
				case 4:
					if (!this.ᜀ.ᜈ())
					{
						num3 = 1;
						continue;
					}
					goto IL_80;
				}
				break;
				IL_80:
				if (true)
				{
				}
				num3 = 2;
			}
		}
		IL_71:
		if (false)
		{
		}
		return num > 0f;
		IL_CF:
		return num2 < 0f;
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x003065C0 File Offset: 0x003055C0
	private static bool ᜀ(spr\u23F1 A_0, ArrayList A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num;
				float num2;
				float num3;
				bool flag;
				int num4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B7:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num2 = 0f;
					num3 = A_0.ᜀ() / 2f;
					flag = false;
					num4 = 0;
					num = 6;
					break;
				}
				for (;;)
				{
					float num5;
					switch (num)
					{
					case 0:
					{
						float a_ = (num5 - num2) * A_0.ᜀ();
						float num6 = A_0.ᜀ() * (num5 + num2) / 2f;
						float a_2 = num6 - num3;
						A_1.Add(new sprᢉ(a_, a_2));
						flag = false;
						num = 4;
						continue;
					}
					case 1:
						num = 10;
						continue;
					case 2:
						goto IL_17C;
					case 3:
						if (num5 >= 0f)
						{
							num = 1;
							continue;
						}
						return false;
					case 4:
						goto IL_EE;
					case 5:
						if (flag)
						{
							goto IL_B7;
						}
						flag = true;
						if (true)
						{
						}
						num = 9;
						continue;
					case 6:
						goto IL_17C;
					case 7:
						if (num4 >= A_0.ᜊ().Length)
						{
							num = 11;
							continue;
						}
						num5 = A_0.ᜊ()[num4];
						num = 3;
						continue;
					case 8:
						if (num5 < num2)
						{
							num = 13;
							continue;
						}
						num = 5;
						continue;
					case 9:
						goto IL_EE;
					case 10:
						if (num5 <= 1f)
						{
							num = 12;
							continue;
						}
						return false;
					case 11:
						return true;
					case 12:
						num = 8;
						continue;
					case 13:
						goto IL_1D0;
					}
					break;
					IL_EE:
					num2 = num5;
					num4++;
					num = 2;
					continue;
					IL_17C:
					num = 7;
				}
			}
			return true;
			IL_1D0:
			return false;
		}
	}

	// Token: 0x060034B0 RID: 13488 RVA: 0x003067B4 File Offset: 0x003057B4
	// Note: this type is marked as 'beforefieldinit'.
	static sprὝ()
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
		sprὝ.ᜂ = new float[0];
	}

	// Token: 0x0400285B RID: 10331
	private new spr\u211F ᜀ;

	// Token: 0x0400285C RID: 10332
	private new spr\u1B70[] ᜁ;

	// Token: 0x0400285D RID: 10333
	internal new static readonly float[] ᜂ;
}
