using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Spire.Doc.Reporting;

// Token: 0x020001DE RID: 478
internal class spr\u1977 : IRowsEnumerator
{
	// Token: 0x060014C6 RID: 5318 RVA: 0x001523D4 File Offset: 0x001513D4
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
		return this.ᜂ;
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x00152418 File Offset: 0x00151418
	public int ᜃ()
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜁ.ItemArray != null)
				{
					num = 1;
					continue;
				}
				goto IL_118;
			case 1:
				num = 5;
				continue;
			case 2:
				if (this.ᜁ != null)
				{
					num = 7;
					continue;
				}
				goto IL_118;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (this.ᜇ != 0)
					{
						goto IL_118;
					}
					break;
				}
				num = 4;
				continue;
			case 4:
				num = 2;
				continue;
			case 5:
				if (this.ᜁ.ItemArray.Length > 0)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				goto IL_118;
			case 7:
				num = 0;
				continue;
			case 8:
				goto IL_48;
			case 9:
				return 1;
			}
			if (this.ᜀ != null)
			{
				num = 8;
			}
			else
			{
				num = 3;
			}
		}
		IL_48:
		return this.ᜀ.Rows.Count;
		IL_118:
		return this.ᜇ;
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x00152544 File Offset: 0x00151544
	public string ᜇ()
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
			if (this.ᜀ == null)
			{
				return this.ᜄ;
			}
			break;
		}
		if (true)
		{
		}
		return this.ᜀ.TableName;
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x0015259C File Offset: 0x0015159C
	public bool ᜅ()
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
		return this.ᜂ >= this.ᜃ();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x001525E8 File Offset: 0x001515E8
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
		return this.ᜂ >= this.ᜃ() - 1;
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x00152638 File Offset: 0x00151638
	protected object ᜁ()
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_105;
			case 1:
				if (this.ᜀ == null)
				{
					goto IL_C4;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EE;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 2:
				num = 3;
				continue;
			case 3:
				if (this.ᜅ != null)
				{
					num = 8;
					continue;
				}
				num = 1;
				continue;
			case 4:
				goto IL_9C;
			case 5:
				goto IL_124;
			case 6:
			{
				int num2;
				if (num2 > this.ᜂ)
				{
					num = 5;
					continue;
				}
				this.ᜅ.MoveNext();
				num2++;
				num = 9;
				continue;
			}
			case 8:
			{
				this.ᜅ.Reset();
				int num2 = 0;
				num = 0;
				continue;
			}
			case 9:
				goto IL_105;
			}
			if (this.ᜂ < this.ᜃ())
			{
				num = 2;
				continue;
			}
			goto IL_146;
			IL_105:
			num = 6;
		}
		IL_9C:
		goto IL_EE;
		IL_C4:
		return this.ᜁ;
		IL_EE:
		return this.ᜀ.Rows[this.ᜂ];
		IL_124:
		return this.ᜅ.Current;
		IL_146:
		return null;
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x0015278C File Offset: 0x0015178C
	public spr\u1977(DataTable A_0)
	{
		this.ᜀ = A_0;
		this.ᜀ(this.ᜀ);
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x001527BC File Offset: 0x001517BC
	public spr\u1977(DataRow A_0)
	{
		this.ᜁ = A_0;
		this.ᜀ(A_0.Table);
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x001527EC File Offset: 0x001517EC
	public void ᜄ()
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

	// Token: 0x060014CF RID: 5327 RVA: 0x00152830 File Offset: 0x00151830
	public bool ᜈ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				IL_3C:
				this.ᜂ++;
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (this.ᜂ < this.ᜃ())
			{
				num = 1;
				continue;
			}
			IL_5E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			default:
				goto IL_74;
			}
		}
		IL_74:
		if (false)
		{
		}
		return !this.ᜅ();
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x001528C0 File Offset: 0x001518C0
	public object ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			PropertyInfo propertyInfo;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_ED;
				case 1:
				{
					PropertyInfo[] properties = this.ᜆ.GetProperties();
					int num2 = 0;
					int num3 = properties.Length;
					num = 0;
					continue;
				}
				case 3:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 5;
						continue;
					}
					PropertyInfo[] properties;
					propertyInfo = properties[num2];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CB;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				}
				case 4:
					goto IL_C9;
				case 5:
					goto IL_109;
				case 6:
					goto IL_68;
				case 7:
					goto IL_ED;
				case 8:
					if (this.ᜁ() != null)
					{
						num = 1;
						continue;
					}
					goto IL_14F;
				case 9:
				{
					if (propertyInfo.Name == A_0)
					{
						num = 4;
						continue;
					}
					int num2;
					num2++;
					num = 7;
					continue;
				}
				}
				if (this.ᜁ() is DataRow)
				{
					num = 6;
					continue;
				}
				IL_CB:
				num = 8;
				continue;
				IL_ED:
				num = 3;
			}
			IL_68:
			return (this.ᜁ() as DataRow)[A_0];
			IL_C9:
			if (true)
			{
			}
			return propertyInfo.GetValue(this.ᜁ(), null);
			IL_109:
			IL_14F:
			return null;
		}
		}
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x00152A20 File Offset: 0x00151A20
	public string[] ᜉ()
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
		return this.ᜃ;
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x00152A64 File Offset: 0x00151A64
	private void ᜀ(DataTable A_0)
	{
		for (;;)
		{
			this.ᜃ = new string[A_0.Columns.Count];
			int num = 0;
			int num2 = 0;
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
					if (num >= this.ᜃ.Length)
					{
						num2 = 3;
						continue;
					}
					this.ᜃ[num] = A_0.Columns[num].ColumnName;
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_42;
				case 3:
					return;
				}
				break;
				IL_42:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				num2 = 1;
			}
		}
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x00152B24 File Offset: 0x00151B24
	public spr\u1977(MailMergeDataTable A_0)
	{
		this.ᜄ = A_0.GroupName;
		A_0.SourceData.Reset();
		A_0.SourceData.MoveNext();
		this.ᜅ = A_0.SourceData;
		try
		{
			this.ᜆ = this.ᜅ.Current.GetType();
			this.ᜀ(this.ᜅ);
		}
		catch
		{
			this.ᜆ = null;
			this.ᜃ = null;
		}
		this.ᜀ();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x00152BBC File Offset: 0x00151BBC
	private void ᜀ(IEnumerator A_0)
	{
		switch (0)
		{
		default:
		{
			List<string> list;
			for (;;)
			{
				int num;
				int num2;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_90:
					PropertyInfo[] properties;
					list.Add(properties[num].Name);
					num++;
					num2 = 2;
					break;
				}
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					list = new List<string>();
					PropertyInfo[] properties = this.ᜆ.GetProperties();
					num = 0;
					num3 = properties.Length;
					num2 = 0;
					break;
				}
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_78;
					case 1:
						goto IL_8E;
					case 2:
						goto IL_78;
					case 3:
						if (num >= num3)
						{
							num2 = 1;
							continue;
						}
						goto IL_90;
					}
					break;
					IL_78:
					num2 = 3;
				}
			}
			IL_8E:
			this.ᜃ = list.ToArray();
			return;
		}
		}
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x00152C88 File Offset: 0x00151C88
	private void ᜀ()
	{
		for (;;)
		{
			this.ᜅ.Reset();
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜅ.MoveNext())
					{
						num = 2;
						continue;
					}
					this.ᜇ++;
					num = 3;
					continue;
				case 1:
					goto IL_35;
				case 2:
					goto IL_78;
				case 3:
					goto IL_35;
				}
				break;
				IL_35:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_95;
				}
				if (false)
				{
				}
				num = 0;
			}
		}
		IL_78:
		IL_95:
		this.ᜅ.Reset();
	}

	// Token: 0x04001935 RID: 6453
	private DataTable ᜀ;

	// Token: 0x04001936 RID: 6454
	private DataRow ᜁ;

	// Token: 0x04001937 RID: 6455
	private int ᜂ = -1;

	// Token: 0x04001938 RID: 6456
	private string[] ᜃ;

	// Token: 0x04001939 RID: 6457
	private string ᜄ;

	// Token: 0x0400193A RID: 6458
	private IEnumerator ᜅ;

	// Token: 0x0400193B RID: 6459
	private Type ᜆ;

	// Token: 0x0400193C RID: 6460
	private int ᜇ;
}
