using System;
using System.ComponentModel;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000295 RID: 661
internal class spr\u1C3B : spr\u21AE
{
	// Token: 0x06002316 RID: 8982 RVA: 0x0023C9AC File Offset: 0x0023B9AC
	internal spr\u1C3B()
	{
		this.ᜋ();
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x0023C9C8 File Offset: 0x0023B9C8
	internal spr\u1C3B(spr\u22C8 A_0, int A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x0023C9EC File Offset: 0x0023B9EC
	public void ᜋ()
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
		spr\u1C3B.ᜀ(new object[]
		{
			this.ᜀ != null,
			PrId.Parent,
			this.ᜀ
		});
		this.ᜀ = null;
		spr\u1C3B.ᜀ(new object[]
		{
			this.ᜂ != TextureStyle.TextureNone,
			PrId.Texture,
			this.ᜂ
		});
		this.ᜂ = TextureStyle.TextureNone;
		spr\u1C3B.ᜀ(new object[]
		{
			!this.ᜃ.ᜇ(),
			PrId.ForegroundPatternColor,
			this.ᜃ
		});
		this.ᜃ = spr\u2262.ទ;
		spr\u1C3B.ᜀ(new object[]
		{
			!this.ᜄ.ᜇ(),
			PrId.BackgroundPatternColor,
			this.ᜄ
		});
		this.ᜄ = spr\u2262.ទ;
		this.ᜅ = null;
		this.ᜆ = null;
		this.ᜇ = null;
		this.ᜈ = null;
		this.ᜉ = null;
		this.ᜊ = null;
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x0023CB50 File Offset: 0x0023BB50
	internal bool ᜁ(spr\u1C3B A_0)
	{
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (this.\u1712() == A_0.\u1712())
				{
					num = 6;
					continue;
				}
				return false;
			case 2:
				if (this.\u1713() == A_0.\u1713())
				{
					num = 19;
					continue;
				}
				return false;
			case 3:
				if (this.ᜈ() == A_0.ᜈ())
				{
					num = 0;
					continue;
				}
				return false;
			case 4:
				if (spr\u2262.ᜁ(this.ᜑ(), A_0.ᜑ()))
				{
					num = 11;
					continue;
				}
				return false;
			case 5:
				if (spr\u2262.ᜁ(this.ᜄ(), A_0.ᜄ()))
				{
					num = 18;
					continue;
				}
				return false;
			case 6:
				goto IL_A2;
			case 7:
				return false;
			case 8:
				num = 17;
				continue;
			case 10:
				if (object.ReferenceEquals(this, A_0))
				{
					num = 14;
					continue;
				}
				num = 3;
				continue;
			case 11:
				num = 5;
				continue;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A2;
				default:
					goto IL_124;
				}
				break;
			case 13:
				if (this.ᜎ() == A_0.ᜎ())
				{
					num = 12;
					continue;
				}
				return false;
			case 14:
				return true;
			case 15:
				num = 1;
				continue;
			case 16:
				if (this.ᜂ() == A_0.ᜂ())
				{
					num = 8;
					continue;
				}
				return false;
			case 17:
				if (this.ᜉ() == A_0.ᜉ())
				{
					num = 15;
					continue;
				}
				return false;
			case 18:
				num = 16;
				continue;
			case 19:
				if (true)
				{
				}
				num = 13;
				continue;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 7;
				continue;
			}
			num = 10;
			continue;
			IL_A2:
			num = 2;
		}
		return false;
		IL_124:
		if (false)
		{
		}
		return this.ᜆ() == A_0.ᜆ();
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x0023CDB8 File Offset: 0x0023BDB8
	public virtual bool ᜀ(object A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return true;
			case 1:
				if (A_0.GetType() != typeof(spr\u1C3B))
				{
					num = 4;
					continue;
				}
				goto IL_B1;
			case 3:
				return false;
			case 4:
				return false;
			case 5:
				if (!object.ReferenceEquals(this, A_0))
				{
					num = 1;
					continue;
				}
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
					num = 0;
					continue;
				}
				break;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 3;
			}
			else
			{
				num = 5;
			}
		}
		return false;
		IL_B1:
		return this.ᜁ((spr\u1C3B)A_0);
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x0023CE84 File Offset: 0x0023BE84
	public virtual int ᜇ()
	{
		int num;
		for (;;)
		{
			num = (int)this.ᜂ;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_53:
				num = (num * 397 ^ (spr\u2262.ᜀ(this.ᜃ, null) ? this.ᜃ.GetHashCode() : 0));
				num2 = 5;
				break;
			default:
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
					num = (num * 397 ^ ((this.ᜇ != null) ? this.ᜇ.GetHashCode() : 0));
					num2 = 4;
					continue;
				case 1:
					goto IL_53;
				case 2:
					num = (num * 397 ^ ((this.ᜅ != null) ? this.ᜅ.GetHashCode() : 0));
					num2 = 7;
					continue;
				case 3:
					num = (num * 397 ^ ((this.ᜉ != null) ? this.ᜉ.GetHashCode() : 0));
					if (true)
					{
					}
					num2 = 6;
					continue;
				case 4:
					num = (num * 397 ^ ((this.ᜈ != null) ? this.ᜈ.GetHashCode() : 0));
					num2 = 3;
					continue;
				case 5:
					num = (num * 397 ^ (spr\u2262.ᜀ(this.ᜄ, null) ? this.ᜄ.GetHashCode() : 0));
					num2 = 2;
					continue;
				case 6:
					goto IL_1B3;
				case 7:
					num = (num * 397 ^ ((this.ᜆ != null) ? this.ᜆ.GetHashCode() : 0));
					num2 = 0;
					continue;
				}
				break;
			}
		}
		IL_1B3:
		return num * 397 ^ ((this.ᜊ != null) ? this.ᜊ.GetHashCode() : 0);
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x0023D068 File Offset: 0x0023C068
	private void ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5C;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (true)
				{
				}
				goto IL_5C;
			}
			if (this.ᜏ())
			{
				num = 2;
				continue;
			}
			break;
			IL_5C:
			this.ᜀ(this.ᜀ());
			spr\u1C3B.ᜀ(new object[]
			{
				this.ᜀ != null,
				PrId.Parent,
				this.ᜀ
			});
			this.ᜀ = null;
			num = 0;
		}
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x0023D128 File Offset: 0x0023C128
	private void ᜀ(spr\u1C3B A_0)
	{
		int a_ = 0;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ᕥᩧ३", a_));
		}
		IL_50:
		this.ᜀ = A_0.ᜀ;
		spr\u1C3B.ᜀ(new object[]
		{
			this.ᜁ != A_0.ᜁ,
			PrId.Key,
			this.ᜁ
		});
		this.ᜁ = A_0.ᜁ;
		spr\u1C3B.ᜀ(new object[]
		{
			this.ᜂ != A_0.ᜈ(),
			PrId.Texture,
			this.ᜂ
		});
		this.ᜂ = A_0.ᜈ();
		spr\u1C3B.ᜀ(new object[]
		{
			spr\u2262.ᜀ(this.ᜃ, A_0.ᜑ()),
			PrId.ForegroundPatternColor,
			this.ᜃ
		});
		this.ᜃ = A_0.ᜑ();
		spr\u1C3B.ᜀ(new object[]
		{
			spr\u2262.ᜀ(this.ᜄ, A_0.ᜄ()),
			PrId.BackgroundPatternColor,
			this.ᜄ
		});
		this.ᜄ = A_0.ᜄ();
		this.ᜅ = A_0.ᜂ();
		this.ᜆ = A_0.ᜉ();
		this.ᜇ = A_0.\u1712();
		this.ᜈ = A_0.\u1713();
		this.ᜉ = A_0.ᜎ();
		this.ᜊ = A_0.ᜆ();
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x0023D2F8 File Offset: 0x0023C2F8
	public bool ᜃ()
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
		return this.ᜏ();
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x0023D33C File Offset: 0x0023C33C
	[EditorBrowsable(EditorBrowsableState.Never)]
	spr\u21AE spr\u21AE.\u170D()
	{
		int a_ = 5;
		if (this.ᜏ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_55;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new InvalidOperationException(ClipboardData.b("⡪౬ŮὰᱲŴ坶᩸᝺ቼᅾꎂꦈﲔﲘﾚ붜ﺞ햠힢힤캦쮨\udeaa\ud9ac쪮龰", a_));
		}
		IL_55:
		return (spr\u1C3B)base.MemberwiseClone();
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x0023D3AC File Offset: 0x0023C3AC
	internal bool ᜏ()
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
		return this.ᜀ != null;
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x0023D3F4 File Offset: 0x0023C3F4
	public Color ᜌ()
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
		return this.ᜄ().ᜈ();
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x0023D43C File Offset: 0x0023C43C
	public void ᜁ(Color A_0)
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
		this.ᜀ(spr\u2262.ᜀ(A_0));
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x0023D484 File Offset: 0x0023C484
	internal spr\u2262 ᜄ()
	{
		if (!this.ᜏ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3F;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜄ;
		}
		IL_3F:
		return this.ᜀ().ᜄ();
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x0023D4DC File Offset: 0x0023C4DC
	internal void ᜀ(spr\u2262 A_0)
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
		this.ᜁ();
		spr\u1C3B.ᜀ(new object[]
		{
			spr\u2262.ᜀ(this.ᜄ, A_0),
			PrId.BackgroundPatternColor,
			this.ᜄ
		});
		this.ᜄ = A_0;
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x0023D558 File Offset: 0x0023C558
	public Color ᜅ()
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
		return this.ᜑ().ᜈ();
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x0023D5A0 File Offset: 0x0023C5A0
	public void ᜀ(Color A_0)
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
		this.ᜁ(spr\u2262.ᜀ(A_0));
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x0023D5E8 File Offset: 0x0023C5E8
	internal spr\u2262 ᜑ()
	{
		if (true)
		{
		}
		if (!this.ᜏ())
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
				return this.ᜃ;
			}
		}
		return this.ᜀ().ᜑ();
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x0023D640 File Offset: 0x0023C640
	internal void ᜁ(spr\u2262 A_0)
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
		this.ᜁ();
		spr\u1C3B.ᜀ(new object[]
		{
			spr\u2262.ᜀ(this.ᜃ, A_0),
			PrId.ForegroundPatternColor,
			this.ᜃ
		});
		this.ᜃ = A_0;
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x0023D6BC File Offset: 0x0023C6BC
	public TextureStyle ᜈ()
	{
		if (!this.ᜏ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3F;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜂ;
		}
		IL_3F:
		return this.ᜀ().ᜈ();
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x0023D714 File Offset: 0x0023C714
	public void ᜀ(TextureStyle A_0)
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
		this.ᜁ();
		spr\u1C3B.ᜀ(new object[]
		{
			this.ᜂ != A_0,
			PrId.Texture,
			this.ᜂ
		});
		this.ᜂ = A_0;
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x0023D798 File Offset: 0x0023C798
	internal string ᜂ()
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
		return this.ᜅ;
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x0023D7DC File Offset: 0x0023C7DC
	internal void ᜂ(string A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x0023D820 File Offset: 0x0023C820
	internal string ᜉ()
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

	// Token: 0x0600232E RID: 9006 RVA: 0x0023D864 File Offset: 0x0023C864
	internal void ᜄ(string A_0)
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

	// Token: 0x0600232F RID: 9007 RVA: 0x0023D8A8 File Offset: 0x0023C8A8
	internal string \u1712()
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

	// Token: 0x06002330 RID: 9008 RVA: 0x0023D8EC File Offset: 0x0023C8EC
	internal void ᜃ(string A_0)
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

	// Token: 0x06002331 RID: 9009 RVA: 0x0023D930 File Offset: 0x0023C930
	internal string \u1713()
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
		return this.ᜈ;
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x0023D974 File Offset: 0x0023C974
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
		this.ᜈ = A_0;
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x0023D9B8 File Offset: 0x0023C9B8
	internal string ᜎ()
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
		return this.ᜉ;
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x0023D9FC File Offset: 0x0023C9FC
	internal void ᜀ(string A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x06002335 RID: 9013 RVA: 0x0023DA40 File Offset: 0x0023CA40
	internal string ᜆ()
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

	// Token: 0x06002336 RID: 9014 RVA: 0x0023DA84 File Offset: 0x0023CA84
	internal void ᜅ(string A_0)
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

	// Token: 0x06002337 RID: 9015 RVA: 0x0023DAC8 File Offset: 0x0023CAC8
	internal bool ᜐ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜈ() != TextureStyle.TextureNil)
				{
					return true;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_32;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 1:
				num = 0;
				continue;
			case 3:
				goto IL_8F;
			}
			if (this.ᜈ() == TextureStyle.TextureNone)
			{
				break;
			}
			num = 1;
		}
		IL_32:
		return !this.ᜄ().ᜇ();
		IL_8F:
		goto IL_32;
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x0023DB68 File Offset: 0x0023CB68
	internal spr\u1C3B ᜊ()
	{
		int a_ = 18;
		if (this.ᜏ())
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
				throw new InvalidOperationException(ClipboardData.b("㭷᭹ቻၽꒃ낏望뚕ﮝ튟쮡킣쎥첧誩춫\udaad쒯삱\uddb3풵춷캹\ud9bb邽", a_));
			}
		}
		return (spr\u1C3B)base.MemberwiseClone();
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x0023DBD8 File Offset: 0x0023CBD8
	private spr\u1C3B ᜀ()
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
		return (spr\u1C3B)this.ᜀ.ᜀ(this.ᜁ);
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x0023DC2C File Offset: 0x0023CC2C
	internal static void ᜀ(params object[] A_0)
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

	// Token: 0x0400213A RID: 8506
	private spr\u22C8 ᜀ;

	// Token: 0x0400213B RID: 8507
	private int ᜁ;

	// Token: 0x0400213C RID: 8508
	private TextureStyle ᜂ;

	// Token: 0x0400213D RID: 8509
	private spr\u2262 ᜃ;

	// Token: 0x0400213E RID: 8510
	private spr\u2262 ᜄ;

	// Token: 0x0400213F RID: 8511
	private string ᜅ;

	// Token: 0x04002140 RID: 8512
	private string ᜆ;

	// Token: 0x04002141 RID: 8513
	private string ᜇ;

	// Token: 0x04002142 RID: 8514
	private string ᜈ;

	// Token: 0x04002143 RID: 8515
	private string ᜉ;

	// Token: 0x04002144 RID: 8516
	private string ᜊ;
}
