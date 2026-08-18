using System;
using Spire.Doc.Core;

// Token: 0x020002AD RID: 685
[CLSCompliant(false)]
internal class sprᤜ : sprᳱ
{
	// Token: 0x06002509 RID: 9481 RVA: 0x002559CC File Offset: 0x002549CC
	public sprᤜ(sprᬛ A_0) : base(A_0)
	{
		this.ᜄ();
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x002559F0 File Offset: 0x002549F0
	public new bool ᜈ()
	{
		bool result;
		for (;;)
		{
			IL_24:
			result = false;
			bool flag = this.ᜃ();
			for (;;)
			{
				int num = 3;
				for (;;)
				{
					bool flag2;
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						flag2 = false;
						goto IL_91;
					case 2:
						flag2 = flag;
						goto IL_91;
					case 3:
						if (flag)
						{
							if (true)
							{
							}
							num = 5;
							continue;
						}
						return result;
					case 4:
						goto IL_B4;
					case 5:
						num = 6;
						continue;
					case 6:
						if (this.ᜄ.ᜃ().Position != (long)this.ᜀ)
						{
							num = 0;
							continue;
						}
						num = 1;
						continue;
					}
					goto IL_24;
					IL_91:
					result = flag2;
					this.ᜀ = (int)this.ᜄ.ᜃ().Position;
					num = 4;
				}
				IL_B4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_CA;
				}
			}
		}
		IL_CA:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x00255ADC File Offset: 0x00254ADC
	protected override void ᜁ()
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
		this.ᜅ();
		base.ᜁ();
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x00255B24 File Offset: 0x00254B24
	protected virtual bool ᜃ()
	{
		int num = base.ᜄ(base.ᜊ().ᜇ(), this.ᜇ.Length);
		if (!this.ᜅ.ᜄ().ᜀ().ᜡ().ᜉ(num))
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
				return false;
			}
		}
		return num != 0;
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x00255BA4 File Offset: 0x00254BA4
	protected new virtual void ᜄ()
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
		this.ᜋ = WordSubdocument.Footnote;
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x00255BE8 File Offset: 0x00254BE8
	protected new virtual void ᜅ()
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
		this.ᜊ = new spr\u2351(this.ᜅ.ᜄ());
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x00255C3C File Offset: 0x00254C3C
	protected new virtual bool ᜂ()
	{
		if (this.ᜅ.ᜄ().ᜀ().ᜡ().ᜇ() == this.ᜂ + 1)
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
				return true;
			}
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x00255CA0 File Offset: 0x00254CA0
	public override WordChunkType ᜆ()
	{
		WordChunkType result;
		for (;;)
		{
			result = base.ᜆ();
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					result = WordChunkType.DocumentEnd;
					num = 0;
					continue;
				case 2:
					if (!this.ᜂ())
					{
						return result;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x040021D8 RID: 8664
	protected new int ᜀ = -1;
}
