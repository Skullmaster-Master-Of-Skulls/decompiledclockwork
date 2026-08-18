using System;
using System.Data;
using Spire.Doc.Reporting;

// Token: 0x02000183 RID: 387
internal class sprᲯ : IRowsEnumerator
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x000E2DCC File Offset: 0x000E1DCC
	public sprᲯ(DataView A_0)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_0.Table;
		this.ᜀ(this.ᜁ);
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x000E2E08 File Offset: 0x000E1E08
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
		return this.ᜃ;
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x000E2E4C File Offset: 0x000E1E4C
	public int ᜂ()
	{
		while (this.ᜀ == null)
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
				return 1;
			}
		}
		return this.ᜀ.Count;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x000E2EA0 File Offset: 0x000E1EA0
	public string ᜆ()
	{
		while (this.ᜀ == null)
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
				return "";
			}
		}
		return this.ᜀ.Table.TableName;
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x000E2EFC File Offset: 0x000E1EFC
	public bool ᜄ()
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
		return this.ᜃ >= this.ᜂ();
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x000E2F48 File Offset: 0x000E1F48
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
		return this.ᜃ >= this.ᜂ() - 1;
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x000E2F98 File Offset: 0x000E1F98
	protected DataRow ᜀ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9F;
			case 1:
				if (this.ᜀ == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				goto IL_40;
			case 2:
				goto IL_5E;
			}
			if (this.ᜃ < this.ᜂ())
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			goto IL_A1;
			IL_5E:
			num = 1;
		}
		IL_40:
		return this.ᜀ[this.ᜃ].Row;
		IL_9F:
		return this.ᜂ;
		IL_A1:
		return null;
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x000E3048 File Offset: 0x000E2048
	public void ᜃ()
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
		this.ᜃ = -1;
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x000E308C File Offset: 0x000E208C
	public bool ᜇ()
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
					goto IL_78;
				case 2:
					this.ᜃ++;
					num = 1;
					continue;
				}
				if (this.ᜃ >= this.ᜂ())
				{
					goto IL_7A;
				}
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
					num = 2;
					break;
				}
			}
		}
		IL_78:
		IL_7A:
		return !this.ᜄ();
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x000E311C File Offset: 0x000E211C
	public object ᜀ(string A_0)
	{
		DataRow dataRow;
		for (;;)
		{
			dataRow = this.ᜀ();
			if (dataRow != null)
			{
				goto IL_3C;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2A;
			}
		}
		IL_2A:
		if (false)
		{
		}
		return null;
		IL_3C:
		return dataRow[A_0];
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x000E316C File Offset: 0x000E216C
	public string[] ᜈ()
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

	// Token: 0x06000DA3 RID: 3491 RVA: 0x000E31B0 File Offset: 0x000E21B0
	private void ᜀ(DataTable A_0)
	{
		for (;;)
		{
			this.ᜄ = new string[A_0.Columns.Count];
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_42;
				case 1:
					if (num >= this.ᜄ.Length)
					{
						num2 = 2;
						continue;
					}
					this.ᜄ[num] = this.ᜁ.Columns[num].ColumnName;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					num2 = 0;
					continue;
				case 2:
					return;
				case 3:
					goto IL_42;
				}
				break;
				IL_42:
				num2 = 1;
			}
		}
	}

	// Token: 0x04001715 RID: 5909
	private DataView ᜀ;

	// Token: 0x04001716 RID: 5910
	private DataTable ᜁ;

	// Token: 0x04001717 RID: 5911
	private DataRow ᜂ;

	// Token: 0x04001718 RID: 5912
	private int ᜃ = -1;

	// Token: 0x04001719 RID: 5913
	private string[] ᜄ;
}
