using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x020002EB RID: 747
internal class sprហ : spr\u23F8
{
	// Token: 0x06002929 RID: 10537 RVA: 0x00290370 File Offset: 0x0028F370
	internal sprហ(int A_0) : this(A_0, true, false)
	{
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x00290388 File Offset: 0x0028F388
	internal sprហ(int A_0, bool A_1, bool A_2)
	{
		this.ᜆ = new int[9];
		this.ᜈ = new spr\u19DC();
		this.ᜄ = A_0;
		this.ᜅ = ~A_0;
		int num = A_2 ? 1 : 9;
		for (int i = 0; i < num; i++)
		{
			this.ᜆ[i] = 4095;
		}
		if (A_2)
		{
			this.ᜇ |= 1;
		}
		if (A_1)
		{
			this.ᜇ |= 16;
		}
		this.ᜉ = "";
	}

	// Token: 0x0600292B RID: 10539 RVA: 0x00290424 File Offset: 0x0028F424
	internal sprហ(Stream A_0)
	{
		this.ᜆ = new int[9];
		this.ᜈ = new spr\u19DC();
		this.ᜀ(A_0);
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x00290458 File Offset: 0x0028F458
	internal spr\u19DC ᜅ()
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
		return this.ᜈ;
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x0029049C File Offset: 0x0028F49C
	internal string ᜁ()
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
		return this.ᜉ;
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x002904E0 File Offset: 0x0028F4E0
	internal void ᜀ(string A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x00290524 File Offset: 0x0028F524
	internal bool ᜆ()
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
		return (this.ᜇ & 2) != 0;
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x00290570 File Offset: 0x0028F570
	internal bool ᜂ()
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
		return (this.ᜇ & 1) != 0;
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x002905BC File Offset: 0x0028F5BC
	internal void ᜀ(bool A_0)
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
		this.ᜇ &= 254;
		this.ᜇ |= (A_0 ? 1 : 0);
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x00290624 File Offset: 0x0028F624
	internal bool ᜀ()
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
		return (this.ᜇ & 16) != 0;
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x00290670 File Offset: 0x0028F670
	internal int ᜄ()
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

	// Token: 0x06002934 RID: 10548 RVA: 0x002906B4 File Offset: 0x0028F6B4
	internal void ᜀ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x002906F8 File Offset: 0x0028F6F8
	internal new void ᜃ(Stream A_0)
	{
		int num = 0;
		for (;;)
		{
			int num2;
			int num3;
			int num4;
			switch (num)
			{
			case 1:
				goto IL_7D;
			case 2:
				if (true)
				{
				}
				num2 = 9;
				goto IL_C5;
			case 3:
				if (num3 >= num4)
				{
					num = 5;
					continue;
				}
				this.ᜈ.Add(new spr\u225B(A_0));
				num3++;
				num = 7;
				continue;
			case 4:
				num = 2;
				continue;
			case 5:
				return;
			case 6:
				num2 = 1;
				goto IL_C5;
			case 7:
				goto IL_7D;
			}
			if (this.ᜂ())
			{
				num = 6;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				num = 4;
				continue;
			}
			IL_7D:
			num = 3;
			continue;
			IL_C5:
			num4 = num2;
			num3 = 0;
			num = 1;
		}
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x002907DC File Offset: 0x0028F7DC
	internal void ᜂ(Stream A_0)
	{
		int num = 4;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_85;
			case 1:
				goto IL_CA;
			case 2:
				goto IL_CA;
			case 3:
				this.ᜀ(true);
				num = 0;
				continue;
			case 4:
				if (true)
				{
				}
				break;
			case 5:
				if (num2 >= this.ᜆ.Length)
				{
					num = 6;
					continue;
				}
				spr\u23F8.ᜀ(A_0, (short)this.ᜆ[num2]);
				num2++;
				num = 1;
				continue;
			case 6:
				goto IL_EB;
			}
			if (this.ᜈ.Count == 1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
			}
			IL_85:
			spr\u23F8.ᜁ(A_0, this.ᜄ);
			spr\u23F8.ᜁ(A_0, this.ᜅ);
			num2 = 0;
			num = 2;
			continue;
			IL_CA:
			num = 5;
		}
		IL_EB:
		spr\u23F8.ᜀ(A_0, (ushort)this.ᜇ);
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x002908E4 File Offset: 0x0028F8E4
	internal void ᜀ(Stream A_0)
	{
		for (;;)
		{
			IL_18:
			this.ᜄ = spr\u23F8.ᜁ(A_0);
			this.ᜅ = spr\u23F8.ᜁ(A_0);
			int num = 0;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				num2 = 1;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= this.ᜆ.Length)
					{
						num2 = 2;
						continue;
					}
					goto IL_7D;
				case 1:
					goto IL_60;
				case 2:
					goto IL_7B;
				case 3:
					goto IL_60;
				}
				goto IL_18;
				IL_60:
				num2 = 0;
			}
			IL_7D:
			this.ᜆ[num] = (int)spr\u23F8.ᜂ(A_0);
			num++;
			goto IL_8F;
		}
		IL_7B:
		this.ᜇ = (int)spr\u23F8.ᜅ(A_0);
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x002909A4 File Offset: 0x0028F9A4
	internal void ᜁ(Stream A_0)
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
			using (List<object>.Enumerator enumerator = this.ᜈ.GetEnumerator())
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_A3;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						spr\u225B spr_u225B = (spr\u225B)enumerator.Current;
						spr_u225B.ᜁ(A_0);
						num = 4;
						continue;
					}
					}
					IL_80:
					num = 2;
					continue;
					goto IL_80;
				}
				IL_A3:;
			}
			break;
		}
	}

	// Token: 0x0400239B RID: 9115
	private new const int ᜀ = 9;

	// Token: 0x0400239C RID: 9116
	private new const int ᜁ = 4095;

	// Token: 0x0400239D RID: 9117
	private new const int ᜂ = 1;

	// Token: 0x0400239E RID: 9118
	private new const int ᜃ = 16;

	// Token: 0x0400239F RID: 9119
	private new int ᜄ;

	// Token: 0x040023A0 RID: 9120
	private new int ᜅ;

	// Token: 0x040023A1 RID: 9121
	private int[] ᜆ;

	// Token: 0x040023A2 RID: 9122
	private int ᜇ;

	// Token: 0x040023A3 RID: 9123
	private spr\u19DC ᜈ;

	// Token: 0x040023A4 RID: 9124
	private string ᜉ;
}
