using System;
using Spire.CompoundFile.Doc;

// Token: 0x02000396 RID: 918
internal class sprᜫ
{
	// Token: 0x060033D4 RID: 13268 RVA: 0x002FA45C File Offset: 0x002F945C
	public int ᜁ()
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

	// Token: 0x060033D5 RID: 13269 RVA: 0x002FA4A0 File Offset: 0x002F94A0
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
		return this.ᜁ;
	}

	// Token: 0x060033D6 RID: 13270 RVA: 0x002FA4E4 File Offset: 0x002F94E4
	public int ᜀ()
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
		return this.ᜂ() - this.ᜁ() + 1;
	}

	// Token: 0x060033D7 RID: 13271 RVA: 0x002FA530 File Offset: 0x002F9530
	private sprᜫ()
	{
	}

	// Token: 0x060033D8 RID: 13272 RVA: 0x002FA544 File Offset: 0x002F9544
	public sprᜫ(int A_0, int A_1)
	{
		int a_ = 2;
		base..ctor();
		if (A_0 < 0)
		{
			throw new ArgumentException(ClipboardData.b("๧ͩṫᵭѯ≱᭳յ", a_));
		}
		if (A_0 > A_1)
		{
			throw new ArgumentException(ClipboardData.b("ѧ୩Ὣᩭ⁯ᵱݳ", a_));
		}
		this.ᜁ = A_1;
		this.ᜀ = A_0;
	}

	// Token: 0x060033D9 RID: 13273 RVA: 0x002FA5A4 File Offset: 0x002F95A4
	public void ᜀ(int A_0)
	{
		int a_ = 13;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (this.ᜁ > A_0)
				{
					goto IL_6D;
				}
				return;
			case 2:
				num = 7;
				continue;
			case 3:
				if (this.ᜁ >= this.ᜀ)
				{
					num = 0;
					continue;
				}
				goto IL_E2;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6D;
				default:
					goto IL_8B;
				}
				break;
			case 5:
				goto IL_D8;
			case 7:
				if (this.ᜀ > A_0)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
			}
			if (this.ᜀ >= 0)
			{
				num = 2;
				continue;
			}
			goto IL_F6;
			IL_6D:
			num = 4;
		}
		IL_8B:
		if (false)
		{
		}
		goto IL_E2;
		IL_D8:
		if (true)
		{
		}
		goto IL_F6;
		IL_E2:
		throw new ArgumentOutOfRangeException(ClipboardData.b("⁲մ᭶ၸེ㑼ᅾꮄ쮆歷\udf8eﺐ", a_));
		IL_F6:
		throw new ArgumentOutOfRangeException(ClipboardData.b("⁲մ᭶ၸེ㑼ᅾꮄ솆力ﺌﮎ손ﲒ", a_));
	}

	// Token: 0x060033DA RID: 13274 RVA: 0x002FA6BC File Offset: 0x002F96BC
	public void ᜀ(sprᜫ A_0)
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
		this.ᜀ += A_0.ᜁ();
		this.ᜁ += A_0.ᜁ();
	}

	// Token: 0x060033DB RID: 13275 RVA: 0x002FA720 File Offset: 0x002F9720
	public sprᜫ ᜁ(int A_0)
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
		return new sprᜫ(this.ᜀ, this.ᜀ + A_0);
	}

	// Token: 0x060033DC RID: 13276 RVA: 0x002FA770 File Offset: 0x002F9770
	public sprᜫ ᜂ(int A_0)
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
		return new sprᜫ(this.ᜀ + A_0, this.ᜁ);
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x002FA7C0 File Offset: 0x002F97C0
	public string ᜀ(string A_0)
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
		return A_0.Substring(this.ᜀ, this.ᜁ - this.ᜀ);
	}

	// Token: 0x04002829 RID: 10281
	private int ᜀ;

	// Token: 0x0400282A RID: 10282
	private int ᜁ;
}
