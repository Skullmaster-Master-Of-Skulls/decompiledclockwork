using System;
using System.Collections;
using System.Reflection;
using Spire.DataExport.XLS;

// Token: 0x0200014D RID: 333
[DefaultMember("Item")]
internal class sprᱨ : IEnumerable
{
	// Token: 0x06000828 RID: 2088 RVA: 0x0005204C File Offset: 0x0005104C
	public IEnumerator ᜁ()
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
		return this.ᜀ.GetEnumerator();
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00052094 File Offset: 0x00051094
	public int ᜀ(spr\u25F6 A_0)
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
		return this.ᜀ.Add(A_0);
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000520DC File Offset: 0x000510DC
	public void ᜀ(int A_0)
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
		this.ᜀ.RemoveAt(A_0);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00052124 File Offset: 0x00051124
	public void ᜂ()
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
		this.ᜀ.Clear();
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0005216C File Offset: 0x0005116C
	public void ᜀ(string A_0, int A_1, int A_2)
	{
		for (;;)
		{
			int num = this.ᜀ(A_0);
			int num2 = 12;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜁ(num).ᜄ() == -1)
					{
						num2 = 15;
						continue;
					}
					goto IL_27F;
				case 1:
					return;
				case 2:
					goto IL_19F;
				case 3:
					if (A_2 <= this.ᜁ(num).ᜂ())
					{
						num2 = 4;
						continue;
					}
					goto IL_19F;
				case 4:
					num2 = 21;
					continue;
				case 5:
					if (A_2 >= this.ᜁ(num).ᜁ())
					{
						num2 = 10;
						continue;
					}
					goto IL_8B;
				case 6:
					goto IL_11F;
				case 7:
					goto IL_27F;
				case 8:
					goto IL_A8;
				case 9:
					num2 = 13;
					continue;
				case 10:
					num2 = 22;
					continue;
				case 11:
					if (A_1 >= this.ᜁ(num).ᜄ())
					{
						num2 = 14;
						continue;
					}
					goto IL_1E6;
				case 12:
					if (num == -1)
					{
						num2 = 19;
						continue;
					}
					goto IL_11F;
				case 13:
					goto IL_1C7;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C7;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 15:
					goto IL_1E6;
				case 16:
					goto IL_208;
				case 17:
					if (A_1 <= this.ᜁ(num).ᜀ())
					{
						num2 = 9;
						continue;
					}
					goto IL_A8;
				case 18:
					goto IL_235;
				case 19:
					num = this.ᜀ(new spr\u25F6(A_0));
					num2 = 6;
					continue;
				case 20:
					goto IL_8B;
				case 21:
					if (this.ᜁ(num).ᜂ() == -1)
					{
						num2 = 2;
						continue;
					}
					return;
				case 22:
					if (this.ᜁ(num).ᜁ() == -1)
					{
						num2 = 20;
						continue;
					}
					goto IL_208;
				}
				break;
				IL_8B:
				this.ᜁ(num).ᜁ(A_2);
				num2 = 16;
				continue;
				IL_A8:
				this.ᜁ(num).ᜂ(A_1);
				num2 = 18;
				continue;
				IL_11F:
				num2 = 11;
				continue;
				IL_19F:
				this.ᜁ(num).ᜃ(A_2);
				num2 = 1;
				continue;
				IL_1C7:
				if (this.ᜁ(num).ᜀ() == -1)
				{
					num2 = 8;
					continue;
				}
				goto IL_235;
				IL_1E6:
				if (true)
				{
				}
				this.ᜁ(num).ᜀ(A_1);
				num2 = 7;
				continue;
				IL_208:
				num2 = 3;
				continue;
				IL_235:
				num2 = 5;
				continue;
				IL_27F:
				num2 = 17;
			}
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00052428 File Offset: 0x00051428
	public int ᜀ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (string.Compare(this.ᜁ(num).ᜃ(), A_0, true) == 0)
					{
						num2 = 2;
						continue;
					}
					num++;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5C;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 1:
					goto IL_8A;
				case 2:
					return num;
				case 3:
					goto IL_5C;
				case 4:
					return -1;
				case 5:
					if (num >= this.ᜀ())
					{
						num2 = 4;
						continue;
					}
					num2 = 0;
					continue;
				}
				break;
				IL_8A:
				num2 = 5;
				continue;
				IL_5C:
				goto IL_8A;
			}
		}
		return num;
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000524EC File Offset: 0x000514EC
	public void ᜀ(string A_0, DataRange A_1)
	{
		int num = 0;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_33;
			case 2:
				goto IL_5C;
			case 3:
				if (num2 == -1)
				{
					num = 2;
					continue;
				}
				goto IL_7B;
			}
			if (A_1 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num2 = this.ᜀ(A_0);
				num = 3;
			}
		}
		IL_33:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			return;
		}
		IL_5C:
		return;
		IL_7B:
		A_1.ColX = (byte)(this.ᜁ(num2).ᜁ() + 1);
		A_1.RowX = (int)((byte)(this.ᜁ(num2).ᜄ() + 1));
		A_1.ColY = (byte)(this.ᜁ(num2).ᜂ() + 1);
		A_1.RowY = (int)((byte)(this.ᜁ(num2).ᜀ() + 1));
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x000525C8 File Offset: 0x000515C8
	public spr\u25F6 ᜁ(int A_0)
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
		return this.ᜀ[A_0] as spr\u25F6;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00052614 File Offset: 0x00051614
	public void ᜀ(int A_0, spr\u25F6 A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0005265C File Offset: 0x0005165C
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
		return this.ᜀ.Count;
	}

	// Token: 0x04000636 RID: 1590
	private ArrayList ᜀ = new ArrayList();
}
