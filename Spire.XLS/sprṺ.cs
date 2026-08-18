using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002B9 RID: 697
internal class sprṺ
{
	// Token: 0x06002A36 RID: 10806 RVA: 0x0017B3F4 File Offset: 0x0017A3F4
	public byte[] ᜂ()
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

	// Token: 0x06002A37 RID: 10807 RVA: 0x0017B438 File Offset: 0x0017A438
	public byte[] ᜁ()
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

	// Token: 0x06002A38 RID: 10808 RVA: 0x0017B47C File Offset: 0x0017A47C
	public byte[] ᜀ()
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

	// Token: 0x06002A39 RID: 10809 RVA: 0x0017B4C0 File Offset: 0x0017A4C0
	public void ᜀ(DataProvider A_0, int A_1, int A_2)
	{
		int a_ = 15;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㕄㕆♈㵊⑌⭎㑐⅒", a_));
			}
		}
		A_1 = A_0.ReadArray(A_1, this.ᜁ);
		A_1 = A_0.ReadArray(A_1, this.ᜂ);
		A_1 = A_0.ReadArray(A_1, this.ᜃ);
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x0017B54C File Offset: 0x0017A54C
	public void ᜁ(DataProvider A_0, int A_1, int A_2)
	{
		int a_ = 14;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㑃㑅❇㱉╋⩍㕏⁑", a_));
			}
		}
		int num = this.ᜁ.Length;
		A_0.WriteBytes(A_1, this.ᜁ, 0, num);
		A_1 += num;
		num = this.ᜂ.Length;
		A_0.WriteBytes(A_1, this.ᜂ, 0, num);
		A_1 += num;
		num = this.ᜃ.Length;
		A_0.WriteBytes(A_1, this.ᜃ, 0, num);
		A_1 += num;
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x0017B600 File Offset: 0x0017A600
	public static int ᜀ(ExcelVersion A_0)
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
		return 48;
	}

	// Token: 0x04001408 RID: 5128
	public const int ᜀ = 48;

	// Token: 0x04001409 RID: 5129
	private byte[] ᜁ = new byte[16];

	// Token: 0x0400140A RID: 5130
	private byte[] ᜂ = new byte[16];

	// Token: 0x0400140B RID: 5131
	private byte[] ᜃ = new byte[16];
}
