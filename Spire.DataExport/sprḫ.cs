using System;
using System.IO;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x02000084 RID: 132
internal class sprḫ
{
	// Token: 0x06000404 RID: 1028 RVA: 0x000275A8 File Offset: 0x000265A8
	public sprḫ()
	{
		this.ᜂ = new spr\u219E(this);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x000275D4 File Offset: 0x000265D4
	private void ᜀ(bool A_0)
	{
		int a_ = 13;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_94;
			case 1:
				if (!File.Exists(this.ᜀ))
				{
					num = 5;
					continue;
				}
				goto IL_7E;
			case 2:
				if (A_0)
				{
					num = 3;
					continue;
				}
				goto IL_7E;
			case 3:
				num = 1;
				continue;
			case 4:
				if (A_0)
				{
					num = 0;
					continue;
				}
				goto IL_12C;
			case 5:
				goto IL_B6;
			case 6:
				goto IL_7C;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D6;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (true)
			{
			}
			if (this.ᜀ.Length == 0)
			{
				num = 6;
				continue;
			}
			num = 2;
			continue;
			IL_7E:
			num = 4;
		}
		IL_7C:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("栨太䨬尮渰甲尴嬶尸町尼刾⑀ൂ⩄㍆ൈ⹊⭌♎㽐㙒ㅔ", a_)));
		IL_94:
		goto IL_D6;
		IL_B6:
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("怨䔪嬬丮崰娲儴砶䤸帺似帾㕀⩂⩄⥆ᙈൊ⑌⍎㑐ᵒ㩔⍖᱘⍚㑜ⱞᕠ", a_)), this.ᜀ));
		IL_D6:
		this.ᜁ = new sprᾀ(this.ᜀ);
		return;
		IL_12C:
		this.ᜁ = new sprấ(this.ᜀ);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00027720 File Offset: 0x00026720
	private void ᜀ()
	{
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜁ.Close();
					this.ᜁ = null;
					num = 2;
					continue;
				case 2:
					goto IL_5A;
				}
				if (true)
				{
				}
				if (this.ᜁ == null)
				{
					break;
				}
				num = 1;
			}
			IL_5C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_72;
			}
			IL_5A:
			goto IL_5C;
		}
		IL_72:
		if (false)
		{
		}
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x000277A8 File Offset: 0x000267A8
	public void ᜆ()
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
		this.ᜂ.ᜄ();
		this.ᜄ = false;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x000277F8 File Offset: 0x000267F8
	public void ᜄ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		}
		if (false)
		{
		}
		this.ᜃ = true;
		try
		{
			this.ᜀ(true);
			try
			{
				this.ᜂ.ᜁ(this.ᜁ);
				this.ᜄ = true;
			}
			finally
			{
				this.ᜀ();
			}
		}
		finally
		{
			if (true)
			{
			}
			this.ᜃ = false;
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0002788C File Offset: 0x0002688C
	public void ᜃ()
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
			this.ᜀ(false);
			try
			{
				this.ᜂ.ᜀ(this.ᜁ);
				this.ᜄ = true;
			}
			finally
			{
				this.ᜀ();
			}
			break;
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00027904 File Offset: 0x00026904
	public bool ᜇ()
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

	// Token: 0x0600040B RID: 1035 RVA: 0x00027948 File Offset: 0x00026948
	public bool ᜅ()
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
		return this.ᜄ;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0002798C File Offset: 0x0002698C
	public string ᜂ()
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

	// Token: 0x0600040D RID: 1037 RVA: 0x000279D0 File Offset: 0x000269D0
	public void ᜀ(string A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00027A14 File Offset: 0x00026A14
	public spr\u219E ᜁ()
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

	// Token: 0x0600040F RID: 1039 RVA: 0x00027A58 File Offset: 0x00026A58
	public sprᲷ ᜈ()
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

	// Token: 0x06000410 RID: 1040 RVA: 0x00027A9C File Offset: 0x00026A9C
	public void ᜀ(sprᲷ A_0)
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

	// Token: 0x0400028C RID: 652
	private string ᜀ = string.Empty;

	// Token: 0x0400028D RID: 653
	private sprḗ ᜁ;

	// Token: 0x0400028E RID: 654
	private spr\u219E ᜂ;

	// Token: 0x0400028F RID: 655
	private bool ᜃ;

	// Token: 0x04000290 RID: 656
	private bool ᜄ;

	// Token: 0x04000291 RID: 657
	private sprᲷ ᜅ;
}
