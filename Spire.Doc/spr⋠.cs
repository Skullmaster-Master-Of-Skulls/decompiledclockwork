using System;
using System.Collections.Generic;
using System.Data;
using Spire.Doc.Reporting;

// Token: 0x02000251 RID: 593
internal class spr\u22E0 : IRowsEnumerator
{
	// Token: 0x06001DD6 RID: 7638 RVA: 0x001D8B6C File Offset: 0x001D7B6C
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
		return this.ᜂ;
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x001D8BB0 File Offset: 0x001D7BB0
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
		return this.ᜁ.Count;
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x001D8BF8 File Offset: 0x001D7BF8
	public string ᜆ()
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
		return this.ᜀ.GetSchemaTable().TableName;
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x001D8C44 File Offset: 0x001D7C44
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
		return this.ᜂ >= this.ᜂ();
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x001D8C90 File Offset: 0x001D7C90
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
		return this.ᜂ >= this.ᜂ() - 1;
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x001D8CE0 File Offset: 0x001D7CE0
	protected List<string> ᜀ()
	{
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					if (this.ᜁ == null)
					{
						num = 3;
						continue;
					}
					goto IL_4A;
				case 3:
					goto IL_95;
				}
				if (this.ᜂ >= this.ᜂ())
				{
					goto IL_97;
				}
				if (true)
				{
				}
				num = 1;
			}
			IL_95:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_72;
			}
		}
		IL_4A:
		return this.ᜁ[this.ᜂ];
		IL_72:
		if (false)
		{
		}
		return null;
		IL_97:
		return null;
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x001D8D88 File Offset: 0x001D7D88
	public string[] ᜈ()
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

	// Token: 0x06001DDD RID: 7645 RVA: 0x001D8DCC File Offset: 0x001D7DCC
	public spr\u22E0(IDataReader A_0)
	{
		this.ᜀ = A_0;
		this.ᜁ = new List<List<string>>();
		this.ᜃ = new string[this.ᜀ.FieldCount];
		for (int i = 0; i < this.ᜀ.FieldCount; i++)
		{
			this.ᜃ[i] = this.ᜀ.GetName(i);
		}
		while (this.ᜀ.Read())
		{
			List<string> list = new List<string>();
			for (int j = 0; j < this.ᜀ.FieldCount; j++)
			{
				list.Add(this.ᜀ[j].ToString());
			}
			this.ᜁ.Add(list);
		}
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x001D8E94 File Offset: 0x001D7E94
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
		this.ᜂ = -1;
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x001D8ED8 File Offset: 0x001D7ED8
	public bool ᜇ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜂ++;
				num = 1;
				continue;
			case 1:
				goto IL_78;
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
				if (false)
				{
				}
				if (this.ᜂ >= this.ᜂ())
				{
					goto IL_7A;
				}
				break;
			}
			num = 0;
		}
		IL_78:
		IL_7A:
		return !this.ᜄ();
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x001D8F68 File Offset: 0x001D7F68
	public object ᜀ(string A_0)
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
			List<string> list = this.ᜀ();
			if (list != null)
			{
				return list[this.ᜀ.GetOrdinal(A_0)];
			}
			break;
		}
		}
		if (true)
		{
		}
		return null;
	}

	// Token: 0x04001F7C RID: 8060
	private IDataReader ᜀ;

	// Token: 0x04001F7D RID: 8061
	private List<List<string>> ᜁ;

	// Token: 0x04001F7E RID: 8062
	private int ᜂ = -1;

	// Token: 0x04001F7F RID: 8063
	private string[] ᜃ;
}
