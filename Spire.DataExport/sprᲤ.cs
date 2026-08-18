using System;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS;

// Token: 0x0200002A RID: 42
internal abstract class sprᲤ : DisposabledObject
{
	// Token: 0x0600014E RID: 334 RVA: 0x0000BEE4 File Offset: 0x0000AEE4
	public sprᲤ(spr\u219E A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000BF00 File Offset: 0x0000AF00
	protected virtual void ᜀ(bool A_0)
	{
		if (!this.ᜀ)
		{
			if (true)
			{
			}
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_70:
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
						goto IL_7A;
					case 2:
						goto IL_5C;
					case 3:
						goto IL_89;
					}
					if (A_0)
					{
						num = 2;
						continue;
					}
					IL_7A:
					this.ᜀ = true;
					num = 3;
				}
				IL_5C:
				this.ᜀ(this, new EventArgs());
				this.ᜂ();
				goto IL_70;
				IL_89:;
			}
			finally
			{
				base.Dispose(A_0);
			}
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000BFBC File Offset: 0x0000AFBC
	private void ᜀ(object A_0, EventArgs A_1)
	{
		int a_ = 1;
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
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜄ != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					this.ᜄ(A_0, A_1);
					num = 3;
					continue;
				case 3:
					return;
				case 4:
					goto IL_5C;
				}
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
			}
			IL_5C:
			break;
		}
		}
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("လᔞ礠伢嘤琦䰨䠪夬䘮帰崲༴ശ欸娺吼䰾⑀݂⁄㑆㵈㥊≌㙎結╒㑔╖捘㹚", a_));
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000C078 File Offset: 0x0000B078
	public void ᜂ()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.ᜃ != null)
					{
						num = 3;
						continue;
					}
					return;
				case 2:
					goto IL_60;
				case 3:
					this.ᜃ.Close();
					this.ᜃ = null;
					num = 5;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜂ.Close();
						this.ᜂ = null;
						num = 2;
						continue;
					}
					break;
				case 5:
					return;
				}
				if (this.ᜂ != null)
				{
					num = 4;
					continue;
				}
				IL_60:
				num = 1;
			}
		}
	}

	// Token: 0x06000152 RID: 338
	public abstract void ᜀ(sprḗ A_0, spr\u1F46 A_1);

	// Token: 0x06000153 RID: 339
	public abstract void ᜀ(sprḗ A_0);

	// Token: 0x06000154 RID: 340 RVA: 0x0000C144 File Offset: 0x0000B144
	public spr\u219E \u1712()
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

	// Token: 0x06000155 RID: 341 RVA: 0x0000C188 File Offset: 0x0000B188
	public spr\u1F46 \u1714()
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

	// Token: 0x06000156 RID: 342 RVA: 0x0000C1CC File Offset: 0x0000B1CC
	public void ᜀ(spr\u1F46 A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000C210 File Offset: 0x0000B210
	public spr\u1809 \u1715()
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

	// Token: 0x06000158 RID: 344 RVA: 0x0000C254 File Offset: 0x0000B254
	public void ᜀ(spr\u1809 A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000C298 File Offset: 0x0000B298
	public virtual int ᜁ()
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
		return this.ᜂ.ᜥ() + this.ᜃ.ᜥ();
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000C2EC File Offset: 0x0000B2EC
	public EventHandler \u1713()
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

	// Token: 0x0600015B RID: 347 RVA: 0x0000C330 File Offset: 0x0000B330
	public void ᜀ(EventHandler A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x04000070 RID: 112
	private bool ᜀ;

	// Token: 0x04000071 RID: 113
	private spr\u219E ᜁ;

	// Token: 0x04000072 RID: 114
	private spr\u1F46 ᜂ;

	// Token: 0x04000073 RID: 115
	private spr\u1809 ᜃ;

	// Token: 0x04000074 RID: 116
	private EventHandler ᜄ;
}
