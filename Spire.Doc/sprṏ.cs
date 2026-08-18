using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x02000378 RID: 888
internal class sprṏ
{
	// Token: 0x060031C3 RID: 12739 RVA: 0x002E0A28 File Offset: 0x002DFA28
	internal sprṏ(spr\u1F9B A_0, sprᾔ A_1, sprά A_2)
	{
		this.ᜑ = A_0;
		this.\u1712 = A_2;
		this.\u1713 = (spr\u1937)A_0.ᜁ();
		this.ᜏ = new spr\u1BA8(this.\u1713, A_0.ᜇ(), A_1);
		this.ᜐ = new spr\u1BA8(this.\u1713, A_0.ᜇ(), A_1, this.\u1713.Owner is sprᢋ);
		if (A_0.ᜇ().IsEmpty)
		{
			this.\u170D = A_0.ᜁ().ᝡ();
			this.ᜎ = A_0.ᜁ().\u1753();
		}
		else
		{
			this.\u170D = A_0.ᜇ();
			this.ᜎ = A_0.ᜇ();
		}
		this.ᜈ = new spr\u24A6();
		spr\u173C.ᜃ(this);
	}

	// Token: 0x060031C4 RID: 12740 RVA: 0x002E0B2C File Offset: 0x002DFB2C
	internal sprṏ(sprṏ A_0, sprỬ[] A_1)
	{
		this.ᜑ = A_0.ᜑ;
		this.\u1712 = A_0.\u1712;
		this.\u1713 = A_0.ᜉ();
		this.ᜏ = A_0.ᜏ;
		this.\u170D = A_0.\u170D;
		this.ᜎ = A_0.ᜎ;
		this.ᜈ = new spr\u24A6();
		spr\u173C.ᜃ(this);
		this.ᜁ = A_1;
	}

	// Token: 0x060031C5 RID: 12741 RVA: 0x002E0BCC File Offset: 0x002DFBCC
	private void ᜄ()
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
		this.ᜃ = new spr\u1B70();
		this.ᜃ.ᜀ(this.ᜇ());
		this.ᜃ.ᜀ(this.ᜎ());
		this.ᜈ.ᜁ(this.ᜃ);
	}

	// Token: 0x060031C6 RID: 12742 RVA: 0x002E0C48 File Offset: 0x002DFC48
	private void ᜃ()
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
		this.ᜅ = new spr\u1926();
		this.ᜅ.ᜀ(false);
		this.ᜊ().ᜁ(this.ᜅ);
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x002E0CAC File Offset: 0x002DFCAC
	private void ᜂ()
	{
		switch (0)
		{
		default:
		{
			spr\u2554 spr_u;
			for (;;)
			{
				spr_u = this.\u1713.\u1736();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!spr_u.ᜂ())
						{
							num = 2;
							continue;
						}
						LineFillType lineFillType = spr_u.ᜄ();
						num = 5;
						continue;
					}
					case 1:
						goto IL_80;
					case 2:
						goto IL_5D;
					case 3:
						num = 4;
						continue;
					case 4:
					{
						sprᤕ a_ = spr\u21C8.ᜀ(spr\u2262.ᜀ(spr_u.ᜊ()), spr_u.ᜇ());
						this.ᜊ = new spr\u23F1(a_);
						num = 7;
						continue;
					}
					case 5:
					{
						LineFillType lineFillType;
						switch (lineFillType)
						{
						case LineFillType.Pattern:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_90;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								sprᤕ a_2 = spr\u21C8.ᜀ(spr_u.ᜏ(), null, spr\u1777.ᜀ(spr_u.ᜊ()), spr\u1777.ᜀ(spr_u.ᜑ()));
								this.ᜊ = new spr\u23F1(a_2);
								num = 6;
								continue;
							}
							}
							break;
						case LineFillType.Texture:
						case LineFillType.Picture:
						{
							spr\u1BE7 a_3 = new spr\u1BE7(spr_u.ᜏ());
							this.ᜊ = new spr\u23F1(a_3);
							num = 1;
							continue;
						}
						default:
							num = 3;
							continue;
						}
						break;
					}
					case 6:
						goto IL_16C;
					case 7:
						goto IL_C7;
					}
					break;
				}
			}
			IL_5D:
			goto IL_90;
			IL_80:
			goto IL_16E;
			IL_90:
			this.ᜊ = null;
			return;
			IL_C7:
			IL_16C:
			IL_16E:
			this.ᜊ.ᜀ(spr\u1D53.ᜀ(spr_u.ᜆ()));
			this.ᜊ.ᜂ(this.\u1712());
			this.ᜁ(spr_u);
			this.ᜀ(spr_u);
			LineCap a_4 = spr\u1D53.ᜁ(spr_u.\u1713());
			this.ᜊ.ᜀ(spr\u1D53.ᜀ(spr_u.\u1713()));
			this.ᜊ.ᜀ(a_4);
			this.ᜊ.ᜁ(a_4);
			return;
		}
		}
	}

	// Token: 0x060031C8 RID: 12744 RVA: 0x002E0E9C File Offset: 0x002DFE9C
	private void ᜁ(spr\u2554 A_0)
	{
		switch (A_0.ᜉ())
		{
		case ShapeLineStyle.Single:
			this.ᜊ.ᜀ(new float[]
			{
				0f,
				1f
			});
			return;
		case ShapeLineStyle.Double:
			break;
		case ShapeLineStyle.ThickThin:
			this.ᜊ.ᜀ(new float[]
			{
				0f,
				0.6f,
				0.8f,
				1f
			});
			return;
		case ShapeLineStyle.ThinThick:
			this.ᜊ.ᜀ(new float[]
			{
				0f,
				0.2f,
				0.4f,
				1f
			});
			return;
		case ShapeLineStyle.Triple:
			this.ᜊ.ᜀ(new float[]
			{
				0f,
				0.16666667f,
				0.33333334f,
				0.6666667f,
				0.8333333f,
				1f
			});
			return;
		default:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return;
			}
			break;
		}
		if (true)
		{
		}
		this.ᜊ.ᜀ(new float[]
		{
			0f,
			0.33333334f,
			0.6666667f,
			1f
		});
	}

	// Token: 0x060031C9 RID: 12745 RVA: 0x002E0F8C File Offset: 0x002DFF8C
	private void ᜀ(spr\u2554 A_0)
	{
		switch (A_0.ᜐ())
		{
		case LineDashing.Solid:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜊ.ᜀ(DashStyle.Solid);
				return;
			}
			break;
		case LineDashing.Dash:
			this.ᜊ.ᜀ(DashStyle.Dash);
			return;
		case LineDashing.Dot:
			this.ᜊ.ᜀ(DashStyle.Dot);
			return;
		case LineDashing.DashDot:
			break;
		case LineDashing.DashDotDot:
			this.ᜊ.ᜀ(DashStyle.DashDotDot);
			return;
		case LineDashing.DotGEL:
			this.ᜊ.ᜁ(new float[]
			{
				1f,
				3f
			});
			return;
		case LineDashing.DashGEL:
			this.ᜊ.ᜁ(new float[]
			{
				4f,
				3f
			});
			return;
		case LineDashing.LongDashGEL:
			this.ᜊ.ᜁ(new float[]
			{
				8f,
				3f
			});
			return;
		case LineDashing.DashDotGEL:
			this.ᜊ.ᜁ(new float[]
			{
				4f,
				3f,
				1f,
				3f
			});
			return;
		case LineDashing.LongDashDotGEL:
			this.ᜊ.ᜁ(new float[]
			{
				8f,
				3f,
				1f,
				3f
			});
			return;
		case LineDashing.LongDashDotDotGEL:
			if (true)
			{
			}
			this.ᜊ.ᜁ(new float[]
			{
				8f,
				3f,
				1f,
				3f,
				1f,
				3f
			});
			return;
		default:
			return;
		}
		this.ᜊ.ᜀ(DashStyle.DashDot);
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x002E1108 File Offset: 0x002E0108
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			sprᤖ sprᤖ;
			for (;;)
			{
				sprᤖ = this.ᜀ();
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D1;
					}
					if (false)
					{
					}
					float num2;
					FillType fillType;
					float a_;
					switch (num)
					{
					case 0:
						num2 = (float)this.\u1713.ម();
						goto IL_10E;
					case 1:
						if (!sprᤖ.ᜂ())
						{
							num = 7;
							continue;
						}
						num = 4;
						continue;
					case 2:
						num = 5;
						continue;
					case 3:
						goto IL_9E;
					case 4:
						if (!this.\u1713.\u1719())
						{
							num = 9;
							continue;
						}
						num = 0;
						continue;
					case 5:
						goto IL_215;
					case 6:
						num2 = 0f;
						goto IL_10E;
					case 7:
						goto IL_8D;
					case 8:
						return;
					case 9:
						num = 6;
						continue;
					case 10:
						switch (fillType)
						{
						case FillType.Solid:
						case FillType.Background:
							goto IL_280;
						case FillType.Pattern:
							goto IL_1D6;
						case FillType.Texture:
							this.ᜋ = spr\u21C8.ᜀ(sprᤖ, this.ᜑ.ᜉ(), a_);
							goto IL_D1;
						case FillType.Picture:
							goto IL_1A0;
						case FillType.Shade:
						case FillType.ShadeScale:
						case FillType.ShadeTitle:
							goto IL_236;
						case FillType.ShadeCenter:
							goto IL_1BE;
						case FillType.ShadeShape:
							num = 3;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					}
					break;
					IL_10E:
					a_ = num2;
					FillType fillType2 = sprᤖ.ᜉ();
					fillType = fillType2;
					num = 10;
					continue;
					IL_D1:
					num = 8;
				}
			}
			IL_8D:
			this.ᜋ = null;
			return;
			IL_9E:
			if (true)
			{
			}
			this.ᜋ = (this.ᜉ().ᝉ() ? spr\u21C8.ᜁ(this.\u1713, this.ᜎ) : spr\u21C8.ᜀ(this.ᜃ, sprᤖ, new PointF(this.ᜎ.Width * 0.5f, this.ᜎ.Height * 0.5f)));
			return;
			IL_1A0:
			this.ᜋ = spr\u21C8.ᜀ(sprᤖ, this.ᜑ.ᜉ(), this.ᜏ);
			return;
			IL_1BE:
			this.ᜋ = spr\u21C8.ᜁ(this.\u1713, this.ᜎ);
			return;
			IL_1D6:
			this.ᜋ = spr\u21C8.ᜀ(sprᤖ.ᜆ(), this.ᜑ.ᜉ(), spr\u2262.ᜀ(sprᤖ.ᜇ()), spr\u2262.ᜀ(sprᤖ.ᜋ()));
			return;
			IL_215:
			goto IL_280;
			IL_236:
			this.ᜋ = spr\u21C8.ᜀ(this.\u1713, this.ᜎ);
			return;
			IL_280:
			this.ᜋ = spr\u21C8.ᜀ(spr\u2262.ᜀ(sprᤖ.ᜇ()), sprᤖ.ᜅ());
			return;
		}
		}
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x002E13B4 File Offset: 0x002E03B4
	internal void \u1715()
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
		this.ᜊ = null;
		this.ᜉ = null;
		this.\u1717();
	}

	// Token: 0x060031CC RID: 12748 RVA: 0x002E140C File Offset: 0x002E040C
	internal void \u1717()
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
		this.ᜅ = null;
		this.ᜆ = PointF.Empty;
		this.ᜇ = false;
		this.ᜄ = null;
	}

	// Token: 0x060031CD RID: 12749 RVA: 0x002E1468 File Offset: 0x002E0468
	internal spr\u1BA8 ᜈ()
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

	// Token: 0x060031CE RID: 12750 RVA: 0x002E14AC File Offset: 0x002E04AC
	internal spr\u1BA8 ᜐ()
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
		return this.ᜐ;
	}

	// Token: 0x060031CF RID: 12751 RVA: 0x002E14F0 File Offset: 0x002E04F0
	internal spr\u24A6 ᜏ()
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
		return this.ᜈ;
	}

	// Token: 0x060031D0 RID: 12752 RVA: 0x002E1534 File Offset: 0x002E0534
	internal void ᜀ(spr\u24A6 A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x060031D1 RID: 12753 RVA: 0x002E1578 File Offset: 0x002E0578
	internal sprỬ[] ᜋ()
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

	// Token: 0x060031D2 RID: 12754 RVA: 0x002E15BC File Offset: 0x002E05BC
	internal void ᜀ(sprỬ[] A_0)
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

	// Token: 0x060031D3 RID: 12755 RVA: 0x002E1600 File Offset: 0x002E0600
	internal PointF[] ᜆ()
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

	// Token: 0x060031D4 RID: 12756 RVA: 0x002E1644 File Offset: 0x002E0644
	internal void ᜀ(PointF[] A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060031D5 RID: 12757 RVA: 0x002E1688 File Offset: 0x002E0688
	internal spr\u1B70 ᜊ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_60;
			case 2:
				this.ᜄ();
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (this.ᜃ != null)
			{
				break;
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
				num = 2;
				break;
			}
		}
		IL_60:
		return this.ᜃ;
	}

	// Token: 0x060031D6 RID: 12758 RVA: 0x002E1708 File Offset: 0x002E0708
	internal spr\u1926 ᜅ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜃ();
				num = 1;
				continue;
			case 1:
				goto IL_60;
			}
			if (true)
			{
			}
			if (this.ᜅ != null)
			{
				break;
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
				num = 0;
				break;
			}
		}
		IL_60:
		return this.ᜅ;
	}

	// Token: 0x060031D7 RID: 12759 RVA: 0x002E1788 File Offset: 0x002E0788
	internal spr\u23F1 ᜇ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜂ();
				num = 2;
				continue;
			case 2:
				goto IL_58;
			}
			if (this.ᜊ != null)
			{
				break;
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
				num = 1;
				break;
			}
		}
		IL_58:
		if (true)
		{
		}
		return this.ᜊ;
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x002E1808 File Offset: 0x002E0808
	internal sprᤕ ᜎ()
	{
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜁ();
				num = 1;
				continue;
			case 1:
				goto IL_60;
			}
			if (this.ᜋ != null)
			{
				break;
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
				num = 0;
				break;
			}
		}
		IL_60:
		return this.ᜋ;
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x002E1888 File Offset: 0x002E0888
	private sprᤖ ᜀ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_6B;
			case 2:
				this.ᜌ = new sprᤖ(this.\u1713);
				num = 1;
				continue;
			}
			if (this.ᜌ != null)
			{
				break;
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
				if (true)
				{
				}
				num = 2;
				break;
			}
		}
		IL_6B:
		return this.ᜌ;
	}

	// Token: 0x060031DA RID: 12762 RVA: 0x002E1914 File Offset: 0x002E0914
	internal PointF \u1713()
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
		return this.ᜆ;
	}

	// Token: 0x060031DB RID: 12763 RVA: 0x002E1958 File Offset: 0x002E0958
	internal void ᜀ(PointF A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060031DC RID: 12764 RVA: 0x002E199C File Offset: 0x002E099C
	internal bool \u1718()
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

	// Token: 0x060031DD RID: 12765 RVA: 0x002E19E0 File Offset: 0x002E09E0
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
		this.ᜇ = A_0;
	}

	// Token: 0x060031DE RID: 12766 RVA: 0x002E1A24 File Offset: 0x002E0A24
	internal spr᪑ \u1716()
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

	// Token: 0x060031DF RID: 12767 RVA: 0x002E1A68 File Offset: 0x002E0A68
	internal void ᜀ(spr᪑ A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x002E1AAC File Offset: 0x002E0AAC
	internal float \u1712()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_70;
			case 2:
				if (true)
				{
				}
				this.ᜉ = this.\u1713.ᜭ();
				num = 1;
				continue;
			}
			if (this.ᜉ != null)
			{
				break;
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
				num = 2;
				break;
			}
		}
		IL_70:
		return (float)this.ᜉ;
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x002E1B40 File Offset: 0x002E0B40
	internal spr\u1937 ᜉ()
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
		return this.\u1713;
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x002E1B84 File Offset: 0x002E0B84
	internal void ᜀ(spr\u1937 A_0)
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
		this.\u1713 = A_0;
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x002E1BC8 File Offset: 0x002E0BC8
	internal RectangleF ᜑ()
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
		return this.ᜀ;
	}

	// Token: 0x060031E4 RID: 12772 RVA: 0x002E1C0C File Offset: 0x002E0C0C
	internal void ᜀ(RectangleF A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x002E1C50 File Offset: 0x002E0C50
	internal SizeF ᜌ()
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
		return this.\u170D;
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x002E1C94 File Offset: 0x002E0C94
	internal SizeF \u170D()
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
		return this.ᜎ;
	}

	// Token: 0x060031E7 RID: 12775 RVA: 0x002E1CD8 File Offset: 0x002E0CD8
	internal sprά \u1714()
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
		return this.\u1712;
	}

	// Token: 0x04002720 RID: 10016
	private RectangleF ᜀ = RectangleF.Empty;

	// Token: 0x04002721 RID: 10017
	private sprỬ[] ᜁ;

	// Token: 0x04002722 RID: 10018
	private PointF[] ᜂ;

	// Token: 0x04002723 RID: 10019
	private spr\u1B70 ᜃ;

	// Token: 0x04002724 RID: 10020
	private spr᪑ ᜄ;

	// Token: 0x04002725 RID: 10021
	private spr\u1926 ᜅ;

	// Token: 0x04002726 RID: 10022
	private PointF ᜆ = PointF.Empty;

	// Token: 0x04002727 RID: 10023
	private bool ᜇ;

	// Token: 0x04002728 RID: 10024
	private spr\u24A6 ᜈ;

	// Token: 0x04002729 RID: 10025
	private object ᜉ;

	// Token: 0x0400272A RID: 10026
	private spr\u23F1 ᜊ;

	// Token: 0x0400272B RID: 10027
	private sprᤕ ᜋ;

	// Token: 0x0400272C RID: 10028
	private sprᤖ ᜌ;

	// Token: 0x0400272D RID: 10029
	private readonly SizeF \u170D = SizeF.Empty;

	// Token: 0x0400272E RID: 10030
	private readonly SizeF ᜎ = SizeF.Empty;

	// Token: 0x0400272F RID: 10031
	private readonly spr\u1BA8 ᜏ;

	// Token: 0x04002730 RID: 10032
	private readonly spr\u1BA8 ᜐ;

	// Token: 0x04002731 RID: 10033
	private spr\u1F9B ᜑ;

	// Token: 0x04002732 RID: 10034
	private readonly sprά \u1712;

	// Token: 0x04002733 RID: 10035
	private spr\u1937 \u1713;
}
