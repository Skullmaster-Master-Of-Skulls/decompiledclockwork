using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000351 RID: 849
internal class sprᱥ
{
	// Token: 0x0600339C RID: 13212 RVA: 0x001DC888 File Offset: 0x001DB888
	public sprᱥ(sprᱥ.ᜀ A_0)
	{
		int a_ = 15;
		this.ᜀ = new List<int>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄⹆㍈⹊ੌ⩎═❒ご╖", a_));
		}
		this.ᜁ = A_0;
		this.ᜀ.Add(0);
	}

	// Token: 0x0600339D RID: 13213 RVA: 0x001DC8DC File Offset: 0x001DB8DC
	public int ᜁ(int A_0)
	{
		int num;
		int count;
		int num2;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_B5:
			this.ᜀ.Capacity = Math.Max(this.ᜀ.Capacity, A_0);
			num = 0;
			num = this.ᜀ[count - 1];
			num2 = count;
			num3 = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3C;
		}
		for (;;)
		{
			IL_1E:
			switch (num3)
			{
			case 0:
				goto IL_73;
			case 1:
				goto IL_73;
			case 2:
				if (num2 > A_0)
				{
					num3 = 5;
					continue;
				}
				num += this.ᜁ(num2);
				this.ᜀ.Add(num);
				num2++;
				num3 = 0;
				continue;
			case 3:
				goto IL_71;
			case 4:
				if (count <= A_0)
				{
					num3 = 3;
					continue;
				}
				goto IL_F1;
			case 5:
				goto IL_87;
			}
			goto IL_3C;
			IL_73:
			num3 = 2;
		}
		IL_71:
		goto IL_B5;
		IL_87:
		IL_F1:
		return this.ᜀ[A_0];
		IL_3C:
		if (true)
		{
		}
		count = this.ᜀ.Count;
		num3 = 4;
		goto IL_1E;
	}

	// Token: 0x0600339E RID: 13214 RVA: 0x001DC9E8 File Offset: 0x001DB9E8
	public int ᜀ(int A_0, int A_1)
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
			if (A_0 > A_1)
			{
				return 0;
			}
			break;
		}
		return this.ᜁ(A_1) - this.ᜁ(A_0 - 1);
	}

	// Token: 0x0600339F RID: 13215 RVA: 0x001DCA3C File Offset: 0x001DBA3C
	public int ᜀ(int A_0)
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
		return this.ᜁ(A_0) - this.ᜁ(A_0 - 1);
	}

	// Token: 0x040016DE RID: 5854
	private List<int> ᜀ;

	// Token: 0x040016DF RID: 5855
	private sprᱥ.ᜀ ᜁ;

	// Token: 0x02000352 RID: 850
	// (Invoke) Token: 0x060033A1 RID: 13217
	public delegate int ᜀ(int A_0);
}
