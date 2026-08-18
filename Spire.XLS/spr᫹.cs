using System;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000448 RID: 1096
internal class spr\u1AF9
{
	// Token: 0x060041FD RID: 16893 RVA: 0x002505E4 File Offset: 0x0024F5E4
	public short ᜃ()
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

	// Token: 0x060041FE RID: 16894 RVA: 0x00250628 File Offset: 0x0024F628
	public void ᜀ(short A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x060041FF RID: 16895 RVA: 0x0025066C File Offset: 0x0024F66C
	public short ᜂ()
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

	// Token: 0x06004200 RID: 16896 RVA: 0x002506B0 File Offset: 0x0024F6B0
	public void ᜂ(short A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06004201 RID: 16897 RVA: 0x002506F4 File Offset: 0x0024F6F4
	public short ᜁ()
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

	// Token: 0x06004202 RID: 16898 RVA: 0x00250738 File Offset: 0x0024F738
	public void ᜁ(short A_0)
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

	// Token: 0x06004203 RID: 16899 RVA: 0x0025077C File Offset: 0x0024F77C
	public string ᜄ()
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

	// Token: 0x06004204 RID: 16900 RVA: 0x002507C0 File Offset: 0x0024F7C0
	public void ᜀ(string A_0)
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

	// Token: 0x06004205 RID: 16901 RVA: 0x00250804 File Offset: 0x0024F804
	public void ᜀ(DataProvider A_0, int A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = A_1;
			A_0.WriteInt16(A_1, this.ᜀ);
			A_1 += 2;
			A_0.WriteInt16(A_1, this.ᜁ);
			A_1 += 2;
			A_0.WriteInt16(A_1, this.ᜂ);
			A_1 += 2;
			A_0.WriteString16BitUpdateOffset(ref A_1, this.ᜃ);
			int num2 = A_1 - num;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_93;
				case 1:
					if (num2 % 2 != 0)
					{
						num3 = 2;
						continue;
					}
					goto IL_93;
				case 2:
					goto IL_7D;
				}
				break;
				IL_7D:
				A_0.WriteByte(A_1, 10);
				num3 = 0;
				continue;
				IL_93:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7D;
				default:
					goto IL_A9;
				}
			}
		}
		IL_A9:
		if (false)
		{
		}
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x002508CC File Offset: 0x0024F8CC
	public int ᜁ(DataProvider A_0, int A_1)
	{
		for (;;)
		{
			IL_28:
			int num = A_1;
			this.ᜀ = A_0.ReadInt16(A_1);
			A_1 += 2;
			this.ᜁ = A_0.ReadInt16(A_1);
			A_1 += 2;
			for (;;)
			{
				IL_4E:
				int num2 = 2;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						A_1++;
						num2 = 5;
						continue;
					case 1:
						if (A_0.Capacity > A_1 + 2)
						{
							if (true)
							{
							}
							num2 = 7;
							continue;
						}
						goto IL_6B;
					case 2:
						if (A_0.Capacity > A_1 + 2)
						{
							num2 = 3;
							continue;
						}
						goto IL_6B;
					case 3:
						this.ᜂ = A_0.ReadInt16(A_1);
						A_1 += 2;
						num2 = 1;
						continue;
					case 4:
						goto IL_6B;
					case 5:
						return A_1;
					case 6:
						if (num3 % 2 != 0)
						{
							num2 = 0;
							continue;
						}
						return A_1;
					case 7:
						this.ᜃ = A_0.ReadString16BitUpdateOffset(ref A_1);
						num2 = 4;
						continue;
					}
					goto IL_28;
					IL_6B:
					num3 = A_1 - num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4E;
					default:
						if (false)
						{
						}
						num2 = 6;
						break;
					}
				}
			}
		}
		return A_1;
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x002509FC File Offset: 0x0024F9FC
	public int ᜅ()
	{
		int num;
		for (;;)
		{
			IL_44:
			num = 0;
			int num2 = 0;
			for (;;)
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
					switch (num2)
					{
					case 0:
						if (this.ᜃ != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_6E;
					case 1:
						num += this.ᜃ.Length * 2;
						num += 3;
						num2 = 3;
						continue;
					case 2:
						num++;
						num2 = 4;
						continue;
					case 3:
						goto IL_6E;
					case 4:
						return num;
					case 5:
						goto IL_7A;
					}
					goto IL_44;
					IL_6E:
					num += 6;
					num2 = 5;
					continue;
				}
				IL_7A:
				if (num % 2 == 0)
				{
					return num;
				}
				num2 = 2;
			}
		}
		return num;
	}

	// Token: 0x06004208 RID: 16904 RVA: 0x00250AC4 File Offset: 0x0024FAC4
	public spr\u1AF9 ᜀ()
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
		return (spr\u1AF9)base.MemberwiseClone();
	}

	// Token: 0x04001D35 RID: 7477
	private short ᜀ;

	// Token: 0x04001D36 RID: 7478
	private short ᜁ;

	// Token: 0x04001D37 RID: 7479
	private short ᜂ;

	// Token: 0x04001D38 RID: 7480
	private string ᜃ;
}
