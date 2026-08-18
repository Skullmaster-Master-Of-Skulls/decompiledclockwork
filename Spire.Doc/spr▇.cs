using System;
using System.Collections;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x0200029B RID: 667
internal class spr\u2587
{
	// Token: 0x06002366 RID: 9062 RVA: 0x0023F130 File Offset: 0x0023E130
	internal spr\u2587()
	{
		this.\u171A();
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x0023F154 File Offset: 0x0023E154
	internal spr\u2587(BorderStyle A_0, int A_1, Color A_2)
	{
		this.ᜄ = A_0;
		this.ᜅ = A_1;
		this.ᜆ = A_2;
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x0023F188 File Offset: 0x0023E188
	internal spr\u2587(sprᢟ A_0, int A_1)
	{
		this.ᜂ = A_0;
		this.ᜃ = A_1;
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x0023F1B4 File Offset: 0x0023E1B4
	public void \u171A()
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
		this.ᜂ = null;
		spr\u2587.ᜁ(new object[]
		{
			this.ᜄ != BorderStyle.None,
			PrId.LineStyle,
			this.ᜄ
		});
		this.ᜄ = BorderStyle.None;
		spr\u2587.ᜁ(new object[]
		{
			this.ᜅ != 0,
			PrId.RawLineWidth,
			this.ᜅ
		});
		this.ᜅ = 0;
		spr\u2587.ᜁ(new object[]
		{
			!this.ᜆ.IsEmpty,
			PrId.Color,
			this.ᜆ
		});
		this.ᜆ = Color.Empty;
		spr\u2587.ᜁ(new object[]
		{
			this.ᜇ != 0,
			PrId.RawDistanceFromText,
			this.ᜇ
		});
		this.ᜇ = 0;
		spr\u2587.ᜁ(new object[]
		{
			this.ᜈ,
			PrId.Shadow,
			this.ᜈ
		});
		this.ᜈ = false;
		spr\u2587.ᜁ(new object[]
		{
			this.ᜉ,
			PrId.Frame,
			this.ᜉ
		});
		this.ᜉ = false;
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
		this.ᜊ = null;
		this.ᜋ = null;
		this.ᜌ = null;
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x0023F3A4 File Offset: 0x0023E3A4
	public BorderStyle ᜈ()
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
			if (this.\u1716())
			{
				return this.ᜄ().ᜈ();
			}
			break;
		}
		return this.ᜄ;
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x0023F3FC File Offset: 0x0023E3FC
	public void ᜁ(BorderStyle A_0)
	{
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_99:
				this.ᜀ(0.0);
				if (true)
				{
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				this.ᜃ();
				spr\u2587.ᜁ(new object[]
				{
					this.ᜄ != A_0,
					PrId.LineStyle,
					this.ᜄ
				});
				this.ᜄ = A_0;
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BD;
				case 1:
					goto IL_99;
				case 2:
					if (A_0 == BorderStyle.None)
					{
						num = 1;
						continue;
					}
					goto IL_BF;
				}
				break;
			}
		}
		IL_BD:
		IL_BF:
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x0023F4DC File Offset: 0x0023E4DC
	public double \u1715()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6C;
			case 1:
				goto IL_4C;
			case 2:
				goto IL_7C;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6C:
				if (this.\u1714())
				{
					goto IL_92;
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (this.\u1716())
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
				break;
			}
		}
		IL_4C:
		return this.ᜄ().\u1715();
		IL_7C:
		if (true)
		{
		}
		return spr\u23C4.\u1713(this.ᜅ);
		IL_92:
		return (double)this.ᜅ;
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x0023F584 File Offset: 0x0023E584
	public void ᜀ(double A_0)
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
		this.ᜁ(A_0, true);
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x0023F5C8 File Offset: 0x0023E5C8
	internal double ᜊ()
	{
		double num;
		for (;;)
		{
			IL_18:
			num = this.\u1715();
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6D:
				num2 = 2;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_5F;
				case 1:
					if (num == 0.0)
					{
						num2 = 0;
						continue;
					}
					return num;
				case 2:
					if (!this.ᜆ())
					{
						num2 = 3;
						continue;
					}
					goto IL_8A;
				case 3:
					goto IL_88;
				}
				goto IL_18;
			}
			IL_5F:
			goto IL_6D;
		}
		return num;
		IL_88:
		return num;
		IL_8A:
		return 0.25;
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x0023F668 File Offset: 0x0023E668
	internal bool \u1714()
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
		return this.ᜈ() >= (BorderStyle)64;
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x0023F6B0 File Offset: 0x0023E6B0
	public bool ᜆ()
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
		return this.ᜈ() != BorderStyle.None;
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x0023F6F8 File Offset: 0x0023E6F8
	internal static bool ᜀ(BorderStyle A_0, float A_1, float A_2)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 <= 0f)
				{
					num = 2;
					continue;
				}
				return true;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				}
				if (false)
				{
				}
				num = 0;
				continue;
			case 2:
				goto IL_7C;
			}
			if (true)
			{
			}
			if (A_0 == BorderStyle.None)
			{
				break;
			}
			num = 1;
		}
		IL_35:
		return A_2 > 0f;
		IL_7C:
		goto IL_35;
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x0023F784 File Offset: 0x0023E784
	internal float \u171E()
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
		return spr\u2587.ᜀ(this.ᜈ(), (float)this.ᜊ());
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x0023F7D4 File Offset: 0x0023E7D4
	internal float ᜐ()
	{
		int num = 3;
		float num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.\u1719())
				{
					num = 2;
					continue;
				}
				goto IL_3E;
			case 1:
				num2 = this.\u171E();
				num = 0;
				continue;
			case 2:
				num2 *= 2f;
				num = 4;
				continue;
			case 3:
				if (true)
				{
				}
				break;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
					goto IL_7A;
				}
				break;
			}
			goto IL_2C;
			IL_34:
			num = 1;
			continue;
			IL_2C:
			if (this.ᜆ())
			{
				goto IL_34;
			}
			goto IL_A6;
		}
		IL_3E:
		num2 += (float)this.ᜏ();
		return num2;
		IL_7A:
		if (false)
		{
		}
		goto IL_3E;
		IL_A6:
		return 0f;
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x0023F88C File Offset: 0x0023E88C
	public Color ᜑ()
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
		return this.\u1712();
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x0023F8D0 File Offset: 0x0023E8D0
	public void ᜁ(Color A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06002376 RID: 9078 RVA: 0x0023F914 File Offset: 0x0023E914
	internal Color \u1712()
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
			if (this.\u1716())
			{
				return this.ᜄ().\u1712();
			}
			break;
		}
		return this.ᜆ;
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x0023F96C File Offset: 0x0023E96C
	internal void ᜀ(Color A_0)
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
		this.ᜃ();
		spr\u2587.ᜁ(new object[]
		{
			this.ᜆ != A_0,
			PrId.Color,
			this.ᜆ
		});
		this.ᜆ = A_0;
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x0023FA00 File Offset: 0x0023EA00
	public double ᜏ()
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
			if (this.\u1716())
			{
				return this.ᜄ().ᜏ();
			}
			break;
		}
		return (double)this.ᜇ;
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x0023FA58 File Offset: 0x0023EA58
	public void ᜂ(double A_0)
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
		this.ᜀ(A_0, true);
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x0023FA9C File Offset: 0x0023EA9C
	public bool \u1719()
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
			if (this.\u1716())
			{
				return this.ᜄ().\u1719();
			}
			break;
		}
		return this.ᜈ;
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x0023FAF4 File Offset: 0x0023EAF4
	public void ᜀ(bool A_0)
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
		this.ᜃ();
		spr\u2587.ᜁ(new object[]
		{
			this.ᜈ != A_0,
			PrId.Shadow,
			this.ᜈ
		});
		this.ᜈ = A_0;
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x0023FB88 File Offset: 0x0023EB88
	internal bool ᜉ()
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
			if (this.\u1716())
			{
				return this.ᜄ().ᜉ();
			}
			break;
		}
		return this.ᜉ;
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x0023FBE0 File Offset: 0x0023EBE0
	internal void ᜁ(bool A_0)
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
		this.ᜃ();
		spr\u2587.ᜁ(new object[]
		{
			this.ᜉ != A_0,
			PrId.Frame,
			this.ᜉ
		});
		this.ᜉ = A_0;
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x0023FC74 File Offset: 0x0023EC74
	internal string ᜌ()
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
		return this.ᜊ;
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x0023FCB8 File Offset: 0x0023ECB8
	internal void ᜀ(string A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x0023FCFC File Offset: 0x0023ECFC
	internal string ᜎ()
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

	// Token: 0x06002381 RID: 9089 RVA: 0x0023FD40 File Offset: 0x0023ED40
	internal void ᜂ(string A_0)
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
		this.ᜋ = A_0;
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x0023FD84 File Offset: 0x0023ED84
	internal string \u171C()
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
		return this.ᜌ;
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x0023FDC8 File Offset: 0x0023EDC8
	internal void ᜁ(string A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x0023FE0C File Offset: 0x0023EE0C
	internal int ᜇ()
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

	// Token: 0x06002385 RID: 9093 RVA: 0x0023FE50 File Offset: 0x0023EE50
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
		spr\u2587.ᜁ(new object[]
		{
			this.ᜅ != A_0,
			PrId.RawLineWidth,
			this.ᜅ
		});
		this.ᜅ = A_0;
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x0023FEDC File Offset: 0x0023EEDC
	internal int \u1718()
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

	// Token: 0x06002387 RID: 9095 RVA: 0x0023FF20 File Offset: 0x0023EF20
	internal void ᜁ(int A_0)
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
		spr\u2587.ᜁ(new object[]
		{
			this.ᜇ != A_0,
			PrId.RawDistanceFromText,
			this.ᜇ
		});
		this.ᜇ = A_0;
		object[] array = new object[2];
		array[0] = this;
		spr\u2587.ᜀ(array);
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x0023FFAC File Offset: 0x0023EFAC
	internal int \u170D()
	{
		float[] array = (float[])spr\u2587.ᜏ[this.ᜈ()];
		if (array == null)
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
				return 1;
			}
		}
		return array.Length;
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x0024000C File Offset: 0x0023F00C
	internal void ᜃ(double A_0)
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
		this.ᜁ(A_0, false);
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x00240050 File Offset: 0x0023F050
	private void ᜁ(double A_0, bool A_1)
	{
		int a_ = 7;
		for (;;)
		{
			double num = spr\u2109.ᜁ(A_0, 0.0, 31.0);
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num != A_0)
					{
						num2 = 1;
						continue;
					}
					goto IL_76;
				case 1:
					num2 = 8;
					continue;
				case 2:
					this.ᜀ(this.\u1714() ? spr\u2109.ᜂ(num) : spr\u23C4.ᜌ(num));
					num2 = 7;
					continue;
				case 3:
					num2 = 4;
					continue;
				case 4:
					if (this.ᜈ() == BorderStyle.None)
					{
						num2 = 6;
						continue;
					}
					return;
				case 5:
					goto IL_9D;
				case 6:
					this.ᜁ(BorderStyle.Single);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 9;
						continue;
					}
					break;
				case 7:
					if (num > 0.0)
					{
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					return;
				case 8:
					if (A_1)
					{
						num2 = 10;
						continue;
					}
					goto IL_76;
				case 9:
					return;
				case 10:
					num2 = 5;
					continue;
				}
				break;
				IL_76:
				this.ᜃ();
				num2 = 2;
			}
		}
		IL_9D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ŭٮὰᙲ≴Ṷᵸེᕼ", a_));
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x002401C0 File Offset: 0x0023F1C0
	internal void ᜁ(double A_0)
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
		this.ᜀ(A_0, false);
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x00240204 File Offset: 0x0023F204
	private void ᜀ(double A_0, bool A_1)
	{
		int a_ = 19;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_EE;
			case 1:
				goto IL_109;
			case 2:
				if (A_0 > 31.0)
				{
					num = 7;
					continue;
				}
				goto IL_137;
			case 4:
				if (A_1)
				{
					num = 1;
					continue;
				}
				A_0 = 0.0;
				num = 5;
				continue;
			case 5:
				goto IL_135;
			case 6:
				num = 4;
				continue;
			case 7:
				num = 9;
				continue;
			case 8:
				goto IL_9B;
			case 9:
				if (A_1)
				{
					num = 8;
					continue;
				}
				A_0 = 31.0;
				num = 0;
				continue;
			}
			IL_41:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_41;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (A_0 < 0.0)
				{
					num = 6;
				}
				else
				{
					num = 2;
				}
				break;
			}
		}
		IL_9B:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᵸቺ๼୾쾈力얐", a_));
		IL_EE:
		goto IL_137;
		IL_109:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᵸቺ๼୾쾈力얐", a_));
		IL_135:
		IL_137:
		this.ᜃ();
		this.ᜁ((int)A_0);
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x00240358 File Offset: 0x0023F358
	internal bool ᜃ(spr\u2587 A_0)
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜈ() == A_0.ᜈ())
				{
					num = 15;
					continue;
				}
				goto IL_230;
			case 1:
				if (object.ReferenceEquals(this, A_0))
				{
					num = 18;
					continue;
				}
				num = 0;
				continue;
			case 2:
				num = 19;
				continue;
			case 3:
				return false;
			case 4:
				if (this.ᜏ() == A_0.ᜏ())
				{
					num = 12;
					continue;
				}
				goto IL_230;
			case 5:
				if (this.ᜌ() == A_0.ᜌ())
				{
					num = 9;
					continue;
				}
				goto IL_230;
			case 6:
				if (true)
				{
				}
				if (this.ᜉ() == A_0.ᜉ())
				{
					num = 2;
					continue;
				}
				goto IL_230;
			case 8:
			{
				Color color;
				if (color.Equals(A_0.\u1712()))
				{
					num = 16;
					continue;
				}
				goto IL_230;
			}
			case 9:
				num = 17;
				continue;
			case 10:
				num = 5;
				continue;
			case 11:
				if (this.\u1715() == A_0.\u1715())
				{
					num = 13;
					continue;
				}
				goto IL_230;
			case 12:
				IL_C9:
				num = 6;
				continue;
			case 13:
			{
				Color color = this.\u1712();
				num = 8;
				continue;
			}
			case 14:
				goto IL_F9;
			case 15:
				num = 11;
				continue;
			case 16:
				num = 4;
				continue;
			case 17:
				if (this.ᜎ() == A_0.ᜎ())
				{
					num = 14;
					continue;
				}
				goto IL_230;
			case 18:
				return true;
			case 19:
				if (this.\u1719() == A_0.\u1719())
				{
					num = 10;
					continue;
				}
				goto IL_230;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_230:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_C9;
			default:
				goto IL_246;
			}
		}
		return false;
		IL_F9:
		return this.\u171C() == A_0.\u171C();
		IL_246:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x002405B4 File Offset: 0x0023F5B4
	public virtual bool ᜀ(object A_0)
	{
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return true;
			case 2:
				return false;
			case 3:
				if (object.ReferenceEquals(this, A_0))
				{
					goto IL_A4;
				}
				num = 5;
				continue;
			case 4:
				return false;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A4;
				default:
					if (false)
					{
					}
					if (A_0.GetType() != typeof(spr\u2587))
					{
						num = 4;
						continue;
					}
					goto IL_B1;
				}
				break;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 2;
				continue;
			}
			num = 3;
			continue;
			IL_A4:
			num = 0;
		}
		return false;
		IL_B1:
		return this.ᜃ((spr\u2587)A_0);
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x00240680 File Offset: 0x0023F680
	public virtual int \u1717()
	{
		int num;
		for (;;)
		{
			num = (int)this.ᜄ;
			num = (num * 397 ^ this.ᜅ);
			num = (num * 397 ^ this.ᜆ.GetHashCode());
			num = (num * 397 ^ this.ᜇ);
			num = (num * 397 ^ this.ᜈ.GetHashCode());
			num = (num * 397 ^ this.ᜉ.GetHashCode());
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num = (num * 397 ^ ((this.ᜊ != null) ? this.ᜊ.GetHashCode() : 0));
					if (true)
					{
					}
					num2 = 1;
					continue;
				case 1:
					for (;;)
					{
						num = (num * 397 ^ ((this.ᜋ != null) ? this.ᜋ.GetHashCode() : 0));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_FF;
						}
					}
					IL_FF:
					if (false)
					{
					}
					num2 = 2;
					continue;
				case 2:
					goto IL_110;
				}
				break;
			}
		}
		IL_110:
		return num * 397 ^ ((this.ᜌ != null) ? this.ᜌ.GetHashCode() : 0);
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x002407C4 File Offset: 0x0023F7C4
	internal bool ᜂ(spr\u2587 A_0)
	{
		if (!this.ᜆ())
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
				return false;
			}
		}
		return this.ᜃ(A_0);
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x00240814 File Offset: 0x0023F814
	internal bool ᜁ(spr\u2587 A_0)
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
		return spr\u2587.ᜀ(this.ᜈ(), A_0.ᜈ());
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x00240860 File Offset: 0x0023F860
	internal static bool ᜀ(BorderStyle A_0, BorderStyle A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return true;
			case 1:
				if ((BorderStyle)spr\u2587.ᜐ[A_0] == A_1)
				{
					num = 0;
					continue;
				}
				goto IL_A7;
			case 2:
				num = 1;
				continue;
			case 3:
				if (spr\u2587.ᜐ.ContainsKey(A_1))
				{
					num = 6;
					continue;
				}
				return false;
			case 5:
				goto IL_9D;
			case 6:
				num = 7;
				continue;
			case 7:
				if ((BorderStyle)spr\u2587.ᜐ[A_1] == A_0)
				{
					num = 5;
					continue;
				}
				return false;
			}
			IL_3A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3A;
			default:
				if (false)
				{
				}
				if (spr\u2587.ᜐ.ContainsKey(A_0))
				{
					num = 2;
					continue;
				}
				break;
			}
			IL_A7:
			num = 3;
		}
		IL_9D:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x00240974 File Offset: 0x0023F974
	internal bool ᜀ(spr\u2587 A_0, out bool A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_69;
			case 1:
				goto IL_97;
			case 3:
				goto IL_EF;
			case 4:
				if (A_1)
				{
					num = 0;
					continue;
				}
				return false;
			case 5:
				if (this.\u1715() == A_0.\u1715())
				{
					num = 1;
					continue;
				}
				return false;
			case 6:
				num = 4;
				continue;
			case 7:
				if (this.ᜈ() != A_0.ᜈ())
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				goto IL_69;
			case 8:
				if (this.\u170D() == 1)
				{
					num = 3;
					continue;
				}
				A_1 = this.ᜁ(A_0);
				num = 7;
				continue;
			case 9:
				goto IL_67;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_67:
				num = 8;
				continue;
			default:
				if (false)
				{
				}
				if (this.ᜆ())
				{
					num = 9;
					continue;
				}
				goto IL_100;
			}
			IL_69:
			num = 5;
		}
		IL_97:
		return this.ᜉ() == A_0.ᜉ();
		IL_EF:
		IL_100:
		A_1 = false;
		return false;
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x00240AA8 File Offset: 0x0023FAA8
	private spr\u2587 ᜄ()
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
		return (spr\u2587)this.ᜂ.ᜁ(this.ᜃ);
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x00240AFC File Offset: 0x0023FAFC
	public bool ᜋ()
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
		return this.\u1716();
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x00240B40 File Offset: 0x0023FB40
	internal bool \u1716()
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
		return this.ᜂ != null;
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x00240B88 File Offset: 0x0023FB88
	internal spr\u2587 \u171D()
	{
		int a_ = 14;
		if (!this.\u1716())
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
				return (spr\u2587)base.MemberwiseClone();
			}
		}
		if (true)
		{
		}
		throw new InvalidOperationException(ClipboardData.b("㝳᝵ᙷᑹ፻੽ꁿ겋ﺏ늑ﶓﾙ풟잡삣蚥즧\udea9\ud8ab\udcad\ud9af킱솳습\uddb7钹", a_));
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x00240BF8 File Offset: 0x0023FBF8
	private void ᜃ()
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
			{
				this.ᜀ(this.ᜄ());
				spr\u2587.ᜁ(new object[]
				{
					this.ᜂ != null,
					PrId.Parent,
					this.ᜂ
				});
				this.ᜂ = null;
				object[] array = new object[2];
				array[0] = this;
				spr\u2587.ᜀ(array);
				goto IL_9B;
			}
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9B;
				default:
					goto IL_BC;
				}
				break;
			}
			if (this.\u1716())
			{
				num = 0;
				continue;
			}
			return;
			IL_9B:
			num = 1;
		}
		IL_BC:
		if (false)
		{
		}
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x00240CCC File Offset: 0x0023FCCC
	private void ᜀ(spr\u2587 A_0)
	{
		int a_ = 12;
		if (A_0 != null)
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
				spr\u2587.ᜁ(new object[]
				{
					this.ᜃ != A_0.ᜃ,
					PrId.Key,
					this.ᜃ
				});
				this.ᜃ = A_0.ᜃ;
				spr\u2587.ᜁ(new object[]
				{
					this.ᜄ != A_0.ᜈ(),
					PrId.LineStyle,
					this.ᜄ
				});
				this.ᜄ = A_0.ᜈ();
				spr\u2587.ᜁ(new object[]
				{
					this.ᜅ != A_0.ᜇ(),
					PrId.RawLineWidth,
					this.ᜅ
				});
				this.ᜅ = A_0.ᜇ();
				spr\u2587.ᜁ(new object[]
				{
					this.ᜆ != A_0.\u1712(),
					PrId.Color,
					this.ᜆ
				});
				this.ᜆ = A_0.\u1712();
				spr\u2587.ᜁ(new object[]
				{
					this.ᜇ != A_0.\u1718(),
					PrId.RawDistanceFromText,
					this.ᜇ
				});
				this.ᜇ = A_0.\u1718();
				spr\u2587.ᜁ(new object[]
				{
					this.ᜈ != A_0.\u1719(),
					PrId.Shadow,
					this.ᜈ
				});
				this.ᜈ = A_0.\u1719();
				spr\u2587.ᜁ(new object[]
				{
					this.ᜉ != A_0.ᜉ(),
					PrId.Frame,
					this.ᜉ
				});
				this.ᜉ = A_0.ᜉ();
				object[] array = new object[2];
				array[0] = this;
				spr\u2587.ᜀ(array);
				this.ᜊ = A_0.ᜌ();
				this.ᜋ = A_0.ᜎ();
				this.ᜌ = A_0.\u171C();
				return;
			}
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("űٳᕵ", a_));
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x00240F78 File Offset: 0x0023FF78
	internal static float[] ᜃ(BorderStyle A_0, float A_1)
	{
		for (;;)
		{
			float[] array = (float[])spr\u2587.\u170D[A_0];
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					float[] array2;
					if (array != null)
					{
						array2 = (float[])array.Clone();
						int num2 = 0;
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return array2;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 1:
					goto IL_B3;
				case 2:
				{
					float[] array2;
					int num2;
					if (num2 >= array2.Length)
					{
						num = 5;
						continue;
					}
					array2[num2] *= A_1;
					num2++;
					num = 1;
					continue;
				}
				case 3:
					goto IL_6F;
				case 4:
					goto IL_B3;
				case 5:
				{
					float[] array2;
					return array2;
				}
				}
				break;
				IL_B3:
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_6F:
		return null;
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x00241060 File Offset: 0x00240060
	internal static float ᜂ(BorderStyle A_0, float A_1)
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
		return (float)spr\u2587.ᜎ[A_0];
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x002410B0 File Offset: 0x002400B0
	internal static float[] ᜁ(BorderStyle A_0, float A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				float[] array = (float[])spr\u2587.ᜏ[A_0];
				int num = 2;
				for (;;)
				{
					int num2;
					float[] array2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							goto IL_114;
						}
						break;
					case 1:
						if (num2 >= array2.Length)
						{
							num = 4;
							continue;
						}
						num = 9;
						continue;
					case 2:
						if (array == null)
						{
							num = 6;
							continue;
						}
						array2 = (float[])array.Clone();
						num2 = 0;
						num = 8;
						continue;
					case 3:
						if (true)
						{
						}
						goto IL_114;
					case 4:
						return array2;
					case 5:
						array2[num2] *= A_1;
						num = 3;
						continue;
					case 6:
						goto IL_6A;
					case 7:
						goto IL_E7;
					case 8:
						goto IL_E7;
					case 9:
						if (array2[num2] >= 0f)
						{
							num = 5;
							continue;
						}
						goto IL_6F;
					}
					break;
					IL_6F:
					array2[num2] = Math.Abs(array2[num2]);
					num = 0;
					continue;
					IL_E7:
					num = 1;
					continue;
					IL_114:
					num2++;
					num = 7;
				}
			}
			IL_6A:
			return new float[]
			{
				A_1
			};
		}
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x00241218 File Offset: 0x00240218
	internal static int ᜀ(BorderStyle A_0)
	{
		float[] array = (float[])spr\u2587.ᜏ[A_0];
		if (array != null)
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
					goto IL_39;
				}
			}
			IL_39:
			if (false)
			{
			}
			return array.Length;
		}
		return 1;
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x00241274 File Offset: 0x00240274
	internal static float ᜀ(BorderStyle A_0, float A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						float num2;
						return num2;
					}
					case 1:
						goto IL_C4;
					case 2:
					{
						int num3;
						float[] array;
						if (num3 >= array.Length)
						{
							num = 0;
							continue;
						}
						float num4 = array[num3];
						float num2;
						num2 += num4;
						num3++;
						num = 4;
						continue;
					}
					case 3:
						switch (A_0)
						{
						case BorderStyle.None:
							goto IL_F0;
						case BorderStyle.Single:
						case BorderStyle.Dot:
						case BorderStyle.DashLargeGap:
						case BorderStyle.DotDash:
						case BorderStyle.DotDotDash:
						case BorderStyle.DashSmallGap:
							goto IL_135;
						case BorderStyle.Thick:
						case BorderStyle.Hairline:
							goto IL_BE;
						case BorderStyle.Double:
						case BorderStyle.Triple:
						case BorderStyle.ThinThickSmallGap:
						case BorderStyle.ThinThinSmallGap:
						case BorderStyle.ThinThickThinSmallGap:
						case BorderStyle.ThinThickMediumGap:
						case BorderStyle.ThickThinMediumGap:
						case BorderStyle.ThickThickThinMediumGap:
						case BorderStyle.ThinThickLargeGap:
						case BorderStyle.ThickThinLargeGap:
						case BorderStyle.ThinThickThinLargeGap:
						case BorderStyle.Emboss3D:
						case BorderStyle.Engrave3D:
						{
							float[] array2 = spr\u2587.ᜁ(A_0, A_1);
							float num2 = 0f;
							float[] array = array2;
							int num3 = 0;
							num = 1;
							continue;
						}
						case (BorderStyle)4:
							goto IL_17A;
						case BorderStyle.Wave:
							goto IL_10C;
						case BorderStyle.DoubleWave:
							goto IL_F6;
						case BorderStyle.DashDotStroker:
						case BorderStyle.Outset:
							return A_1;
						default:
							num = 5;
							continue;
						}
						break;
					case 4:
						goto IL_C4;
					case 5:
						num = 6;
						continue;
					case 6:
						goto IL_10A;
					}
					break;
					IL_C4:
					num = 2;
				}
			}
			IL_BE:
			return 0.75f;
			IL_F0:
			return 0f;
			IL_F6:
			return 6.75f * A_1;
			IL_10A:
			goto IL_17A;
			IL_10C:
			return 2.5f * A_1;
			IL_135:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return A_1;
			default:
				if (false)
				{
				}
				return A_1;
			}
			IL_17A:
			if (true)
			{
			}
			return A_1;
		}
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x00241404 File Offset: 0x00240404
	internal int \u1713()
	{
		object obj = spr\u2587.ᜑ[this.ᜈ()];
		if (obj == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_31;
				}
			}
			IL_31:
			if (true)
			{
			}
			if (false)
			{
			}
			return 0;
		}
		return (int)obj;
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x00241464 File Offset: 0x00240464
	internal int ᜅ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 2:
				goto IL_63;
			case 3:
				if (this.ᜈ() == BorderStyle.DashLargeGap)
				{
					num = 0;
					continue;
				}
				goto IL_7E;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_63:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (this.ᜈ() == BorderStyle.Dot)
				{
					return 1;
				}
				num = 2;
				break;
			}
		}
		return 1;
		IL_7C:
		return 1;
		IL_7E:
		return this.\u1713() * this.ᜇ();
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x002414FC File Offset: 0x002404FC
	internal static spr\u2587 ᜀ(spr\u2587 A_0, spr\u2587 A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return A_1;
				case 1:
				{
					int num2;
					int num3;
					if (num2 != num3)
					{
						num = 2;
						continue;
					}
					int num4 = A_0.ᜂ();
					int num5 = A_1.ᜂ();
					num = 6;
					continue;
				}
				case 2:
					num = 5;
					continue;
				case 3:
					num = 12;
					continue;
				case 4:
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					num = 8;
					continue;
				case 5:
				{
					int num2;
					int num3;
					if (num2 <= num3)
					{
						num = 18;
						continue;
					}
					return A_0;
				}
				case 6:
				{
					int num4;
					int num5;
					if (num4 != num5)
					{
						num = 16;
						continue;
					}
					num4 = A_0.ᜁ();
					num5 = A_1.ᜁ();
					num = 17;
					continue;
				}
				case 7:
					if (A_0.ᜀ() <= A_1.ᜀ())
					{
						return A_0;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_220;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 8:
				{
					if (A_0.ᜅ() != A_1.ᜅ())
					{
						num = 20;
						continue;
					}
					int num2 = A_0.\u1713();
					int num3 = A_1.\u1713();
					num = 1;
					continue;
				}
				case 9:
				{
					int num4;
					int num5;
					if (num4 >= num5)
					{
						num = 0;
						continue;
					}
					return A_0;
				}
				case 10:
					return A_1;
				case 11:
					return A_0;
				case 12:
				{
					int num4;
					int num5;
					if (num4 >= num5)
					{
						num = 15;
						continue;
					}
					return A_0;
				}
				case 13:
					if (A_0.ᜅ() <= A_1.ᜅ())
					{
						if (true)
						{
						}
						num = 14;
						continue;
					}
					return A_0;
				case 14:
					return A_1;
				case 15:
					return A_1;
				case 16:
					num = 9;
					continue;
				case 17:
				{
					int num4;
					int num5;
					if (num4 != num5)
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				}
				case 18:
					return A_1;
				case 19:
					return A_1;
				case 20:
					num = 13;
					continue;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				IL_220:
				num = 4;
			}
			return A_1;
		}
		}
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x00241770 File Offset: 0x00240770
	private int ᜂ()
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
		return (int)(this.ᜑ().R + this.ᜑ().B + 2 * this.ᜑ().G);
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x002417DC File Offset: 0x002407DC
	private int ᜁ()
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
		return (int)(this.ᜑ().B + 2 * this.ᜑ().G);
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x00241838 File Offset: 0x00240838
	private int ᜀ()
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
		return (int)this.ᜑ().G;
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x00241884 File Offset: 0x00240884
	static spr\u2587()
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
		spr\u2587.\u1712 = new spr\u2587();
		spr\u2587.\u170D = new Hashtable();
		spr\u2587.\u170D.Add(BorderStyle.Dot, new float[]
		{
			1f,
			1f
		});
		spr\u2587.\u170D.Add(BorderStyle.DashSmallGap, new float[]
		{
			4f,
			1f
		});
		spr\u2587.\u170D.Add(BorderStyle.DashLargeGap, new float[]
		{
			4f,
			4f
		});
		spr\u2587.\u170D.Add(BorderStyle.DotDash, new float[]
		{
			7f,
			3f,
			3f,
			3f
		});
		spr\u2587.\u170D.Add(BorderStyle.DotDotDash, new float[]
		{
			6f,
			2f,
			2f,
			2f,
			2f,
			2f
		});
		spr\u2587.ᜎ = new Hashtable();
		spr\u2587.ᜎ.Add(BorderStyle.Dot, 2f);
		spr\u2587.ᜎ.Add(BorderStyle.DashSmallGap, 5f);
		spr\u2587.ᜎ.Add(BorderStyle.DashLargeGap, 8f);
		spr\u2587.ᜎ.Add(BorderStyle.DotDash, 16f);
		spr\u2587.ᜎ.Add(BorderStyle.DotDotDash, 16f);
		spr\u2587.ᜏ = new Hashtable();
		spr\u2587.ᜏ.Add(BorderStyle.Double, new float[]
		{
			1f,
			1f,
			1f
		});
		spr\u2587.ᜏ.Add(BorderStyle.Triple, new float[]
		{
			1f,
			1f,
			1f,
			1f,
			1f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThinThickSmallGap, new float[]
		{
			1f,
			-0.75f,
			-0.75f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThinThinSmallGap, new float[]
		{
			-0.75f,
			-0.75f,
			1f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThinThickMediumGap, new float[]
		{
			1f,
			0.5f,
			0.5f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThickThinMediumGap, new float[]
		{
			0.5f,
			0.5f,
			1f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThinThickLargeGap, new float[]
		{
			-1.5f,
			1f,
			-0.75f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThickThinLargeGap, new float[]
		{
			-0.75f,
			1f,
			-1.5f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThinThickThinSmallGap, new float[]
		{
			-0.75f,
			-0.75f,
			1f,
			-0.75f,
			-0.75f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThickThickThinMediumGap, new float[]
		{
			0.5f,
			0.5f,
			1f,
			0.5f,
			0.5f
		});
		spr\u2587.ᜏ.Add(BorderStyle.ThinThickThinLargeGap, new float[]
		{
			-0.75f,
			1f,
			-1.5f,
			1f,
			-0.75f
		});
		spr\u2587.ᜏ.Add(BorderStyle.Emboss3D, new float[]
		{
			0.25f,
			0f,
			1f,
			0f,
			0.25f
		});
		spr\u2587.ᜏ.Add(BorderStyle.Engrave3D, new float[]
		{
			0.25f,
			0f,
			1f,
			0f,
			0.25f
		});
		spr\u2587.ᜐ = new Hashtable();
		spr\u2587.ᜐ.Add(BorderStyle.ThinThickSmallGap, BorderStyle.ThinThinSmallGap);
		spr\u2587.ᜐ.Add(BorderStyle.ThinThickMediumGap, BorderStyle.ThickThinMediumGap);
		spr\u2587.ᜐ.Add(BorderStyle.ThinThickLargeGap, BorderStyle.ThickThinLargeGap);
		spr\u2587.ᜑ = new Hashtable(27);
		spr\u2587.ᜑ.Add(BorderStyle.Single, 1);
		spr\u2587.ᜑ.Add(BorderStyle.Thick, 2);
		spr\u2587.ᜑ.Add(BorderStyle.Double, 3);
		spr\u2587.ᜑ.Add(BorderStyle.Dot, 4);
		spr\u2587.ᜑ.Add(BorderStyle.DashLargeGap, 5);
		spr\u2587.ᜑ.Add(BorderStyle.DotDash, 8);
		spr\u2587.ᜑ.Add(BorderStyle.DotDotDash, 9);
		spr\u2587.ᜑ.Add(BorderStyle.Triple, 10);
		spr\u2587.ᜑ.Add(BorderStyle.ThinThickSmallGap, 11);
		spr\u2587.ᜑ.Add(BorderStyle.ThinThinSmallGap, 12);
		spr\u2587.ᜑ.Add(BorderStyle.ThinThickThinSmallGap, 13);
		spr\u2587.ᜑ.Add(BorderStyle.ThinThickMediumGap, 14);
		spr\u2587.ᜑ.Add(BorderStyle.ThickThinMediumGap, 15);
		spr\u2587.ᜑ.Add(BorderStyle.ThickThickThinMediumGap, 16);
		spr\u2587.ᜑ.Add(BorderStyle.ThinThickLargeGap, 17);
		spr\u2587.ᜑ.Add(BorderStyle.ThickThinLargeGap, 18);
		spr\u2587.ᜑ.Add(BorderStyle.ThinThickThinLargeGap, 19);
		spr\u2587.ᜑ.Add(BorderStyle.Wave, 20);
		spr\u2587.ᜑ.Add(BorderStyle.DoubleWave, 21);
		spr\u2587.ᜑ.Add(BorderStyle.DashSmallGap, 22);
		spr\u2587.ᜑ.Add(BorderStyle.DashDotStroker, 23);
		spr\u2587.ᜑ.Add(BorderStyle.Emboss3D, 24);
		spr\u2587.ᜑ.Add(BorderStyle.Engrave3D, 25);
		spr\u2587.ᜑ.Add(BorderStyle.Outset, 26);
		spr\u2587.ᜑ.Add(BorderStyle.Inset, 27);
		spr\u2587.ᜑ.Add(BorderStyle.None, 0);
		spr\u2587.ᜑ.Add(BorderStyle.Hairline, 1);
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x00241EB8 File Offset: 0x00240EB8
	internal bool \u171B()
	{
		if (this.ᜈ() >= BorderStyle.None)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_21;
				}
			}
			IL_21:
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜈ() <= BorderStyle.Inset;
		}
		return false;
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x00241F10 File Offset: 0x00240F10
	internal static void ᜁ(params object[] A_0)
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

	// Token: 0x060023A8 RID: 9128 RVA: 0x00241F4C File Offset: 0x00240F4C
	internal static void ᜀ(params object[] A_0)
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
	}

	// Token: 0x04002147 RID: 8519
	private const int ᜀ = 31;

	// Token: 0x04002148 RID: 8520
	private const int ᜁ = 31;

	// Token: 0x04002149 RID: 8521
	private sprᢟ ᜂ;

	// Token: 0x0400214A RID: 8522
	private int ᜃ;

	// Token: 0x0400214B RID: 8523
	private BorderStyle ᜄ;

	// Token: 0x0400214C RID: 8524
	private int ᜅ;

	// Token: 0x0400214D RID: 8525
	private Color ᜆ = Color.Empty;

	// Token: 0x0400214E RID: 8526
	private int ᜇ;

	// Token: 0x0400214F RID: 8527
	private bool ᜈ;

	// Token: 0x04002150 RID: 8528
	private bool ᜉ;

	// Token: 0x04002151 RID: 8529
	private string ᜊ;

	// Token: 0x04002152 RID: 8530
	private string ᜋ;

	// Token: 0x04002153 RID: 8531
	private string ᜌ;

	// Token: 0x04002154 RID: 8532
	private static readonly Hashtable \u170D;

	// Token: 0x04002155 RID: 8533
	private static readonly Hashtable ᜎ;

	// Token: 0x04002156 RID: 8534
	private static readonly Hashtable ᜏ;

	// Token: 0x04002157 RID: 8535
	private static readonly Hashtable ᜐ;

	// Token: 0x04002158 RID: 8536
	private static readonly Hashtable ᜑ;

	// Token: 0x04002159 RID: 8537
	internal static readonly spr\u2587 \u1712;
}
