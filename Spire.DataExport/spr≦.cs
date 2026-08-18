using System;
using Spire.DataExport.CollectionEditors;

// Token: 0x02000139 RID: 313
internal class spr\u2266 : ICloneable
{
	// Token: 0x060007AC RID: 1964 RVA: 0x0004D420 File Offset: 0x0004C420
	public spr\u2266(int A_0, string A_1, string A_2)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0004D460 File Offset: 0x0004C460
	public object ᜅ()
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
		return new spr\u2266(this.ᜃ(), this.ᜀ(), this.ᜁ());
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0004D4B4 File Offset: 0x0004C4B4
	public int ᜃ()
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

	// Token: 0x060007AF RID: 1967 RVA: 0x0004D4F8 File Offset: 0x0004C4F8
	public void ᜀ(int A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_55;
			case 2:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_55:
				this.ᜀ = A_0;
				if (true)
				{
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (this.ᜀ == A_0)
				{
					return;
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0004D574 File Offset: 0x0004C574
	public string ᜀ()
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

	// Token: 0x060007B1 RID: 1969 RVA: 0x0004D5B8 File Offset: 0x0004C5B8
	public void ᜀ(string A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				if (true)
				{
				}
				goto IL_62;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_62:
				this.ᜁ = A_0;
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (!(this.ᜁ != A_0))
				{
					return;
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0004D638 File Offset: 0x0004C638
	public string ᜁ()
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

	// Token: 0x060007B3 RID: 1971 RVA: 0x0004D67C File Offset: 0x0004C67C
	public void ᜁ(string A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_62;
			case 2:
				return;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_62:
				this.ᜂ = A_0;
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (!(this.ᜂ != A_0))
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0004D6FC File Offset: 0x0004C6FC
	public string ᜄ()
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(HyperlinksCollectionEditor.b("嬟夡砣䀥匧ᨩ儫爭嘯䤱Գ䬵ᠷ䄹฻䌽笿㽁㥃", a_), this.ᜀ, this.ᜁ, this.ᜂ);
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0004D76C File Offset: 0x0004C76C
	public int ᜂ()
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
		return this.ᜄ().Length;
	}

	// Token: 0x04000618 RID: 1560
	private int ᜀ;

	// Token: 0x04000619 RID: 1561
	private string ᜁ = string.Empty;

	// Token: 0x0400061A RID: 1562
	private string ᜂ = string.Empty;
}
