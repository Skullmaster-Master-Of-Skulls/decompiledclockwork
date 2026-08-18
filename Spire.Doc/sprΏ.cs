using System;
using System.Drawing.Imaging;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x02000264 RID: 612
internal class sprΏ : spr\u2192
{
	// Token: 0x06002011 RID: 8209 RVA: 0x0021F898 File Offset: 0x0021E898
	internal sprΏ(Document A_0) : base(MSOFBT.msofbtBSE, 2, A_0)
	{
		this.ᜀ = new sprắ();
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x0021F8C0 File Offset: 0x0021E8C0
	protected override void ᜁ(Stream A_0)
	{
		for (;;)
		{
			for (;;)
			{
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
					this.ᜀ.ᜁ(A_0);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (base.\u1717().ᜇ() > 36)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							this.ᜂ = true;
							this.ᜁ = (spr\u1D2F.ᜀ(A_0, this.ᜁ) as sprᢘ);
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x0021F968 File Offset: 0x0021E968
	protected override void ᜀ(Stream A_0)
	{
		int num;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					if (this.ᜂ)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					num = 4;
					continue;
				case 3:
				{
					this.ᜀ.ᜆ = this.ᜁ.ᜆ(A_0);
					int num2 = Convert.ToInt32(A_0.Position);
					A_0.Position = (long)num3;
					this.ᜀ.ᜀ(A_0);
					A_0.Position = (long)num2;
					num = 1;
					continue;
				}
				case 4:
					if (this.ᜁ != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				goto IL_38;
			}
			return;
		default:
			if (false)
			{
			}
			break;
		}
		IL_38:
		if (true)
		{
		}
		num3 = Convert.ToInt32(A_0.Position);
		this.ᜀ.ᜀ(A_0);
		num = 0;
		goto IL_1E;
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x0021FA60 File Offset: 0x0021EA60
	internal void ᜂ(Stream A_0)
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
		long position = A_0.Position;
		A_0.Position = (long)this.ᜀ.ᜈ;
		this.ᜁ = (spr\u1D2F.ᜀ(A_0, this.ᜁ) as sprᢘ);
		A_0.Position = position;
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x0021FAD4 File Offset: 0x0021EAD4
	internal new void ᜃ(Stream A_0)
	{
		for (;;)
		{
			for (;;)
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
					this.ᜀ.ᜈ = (int)A_0.Position;
					this.ᜀ.ᜆ = 0;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜀ.ᜆ = this.ᜁ.ᜆ(A_0);
							if (true)
							{
							}
							num = 2;
							continue;
						case 1:
							if (this.ᜁ != null)
							{
								num = 0;
								continue;
							}
							return;
						case 2:
							return;
						}
						break;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x0021FB80 File Offset: 0x0021EB80
	internal void ᜀ(sprᠾ A_0)
	{
		sprᢘ sprᢘ;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4C:
			sprᢘ = new sprᲱ(A_0, this.ᜁ);
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_9A;
			case 2:
				goto IL_4C;
			case 3:
				goto IL_79;
			}
			if (A_0.ᜄ())
			{
				num = 2;
			}
			else
			{
				bool a_ = this.ᜀ(A_0.ᜂ());
				sprᢘ = new sprᱪ(A_0, a_, this.ᜁ);
				if (true)
				{
				}
				num = 3;
			}
		}
		IL_79:
		IL_9A:
		base.\u1717().ᜁ((int)sprᢘ.ᜌ());
		this.ᜁ().ᜂ = (int)sprᢘ.ᜌ();
		this.ᜁ().ᜃ = (int)sprᢘ.ᜌ();
		this.ᜁ().ᜄ = sprᢘ.ᜈ().ToByteArray();
		this.ᜁ().ᜅ = 255;
		this.ᜁ().ᜇ = 1;
		this.ᜀ(sprᢘ);
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x0021FC98 File Offset: 0x0021EC98
	internal sprᢘ ᜄ()
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

	// Token: 0x06002018 RID: 8216 RVA: 0x0021FCDC File Offset: 0x0021ECDC
	internal void ᜀ(sprᢘ A_0)
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

	// Token: 0x06002019 RID: 8217 RVA: 0x0021FD20 File Offset: 0x0021ED20
	internal new sprắ ᜁ()
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

	// Token: 0x0600201A RID: 8218 RVA: 0x0021FD64 File Offset: 0x0021ED64
	internal bool ᜂ()
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

	// Token: 0x0600201B RID: 8219 RVA: 0x0021FDA8 File Offset: 0x0021EDA8
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
		this.ᜂ = A_0;
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x0021FDEC File Offset: 0x0021EDEC
	internal virtual spr\u2192 ᜀ()
	{
		sprΏ sprΏ;
		for (;;)
		{
			for (;;)
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
					sprΏ = new sprΏ(this.ᜁ);
					sprΏ.ᜂ = this.ᜂ;
					sprΏ.ᜀ = this.ᜀ.ᜀ();
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							sprΏ.ᜁ = (sprᢘ)this.ᜁ.ᜃ();
							num = 1;
							continue;
						case 1:
							goto IL_A6;
						case 2:
							if (this.ᜁ != null)
							{
								num = 0;
								continue;
							}
							goto IL_A8;
						}
						break;
					}
					break;
				}
				}
			}
		}
		IL_A6:
		IL_A8:
		sprΏ.ᜀ(base.\u1717().ᜆ());
		sprΏ.ᜁ = this.ᜁ;
		return sprΏ;
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x0021FEC0 File Offset: 0x0021EEC0
	private new bool ᜁ(ImageFormat A_0)
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
			if (!A_0.Equals(ImageFormat.Emf))
			{
				return A_0.Equals(ImageFormat.Wmf);
			}
			break;
		}
		return true;
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x0021FF18 File Offset: 0x0021EF18
	private bool ᜀ(ImageFormat A_0)
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
			if (!A_0.Equals(ImageFormat.Png))
			{
				return A_0.Equals(ImageFormat.Bmp);
			}
			break;
		}
		return true;
	}

	// Token: 0x04002017 RID: 8215
	private new sprắ ᜀ;

	// Token: 0x04002018 RID: 8216
	private new sprᢘ ᜁ;

	// Token: 0x04002019 RID: 8217
	private new bool ᜂ;
}
