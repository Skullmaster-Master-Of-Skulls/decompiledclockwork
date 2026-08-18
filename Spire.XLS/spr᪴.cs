using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000440 RID: 1088
internal class spr\u1AB4
{
	// Token: 0x0600416C RID: 16748 RVA: 0x0024BCA4 File Offset: 0x0024ACA4
	public List<spr\u2340> ᜀ()
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

	// Token: 0x0600416D RID: 16749 RVA: 0x0024BCE8 File Offset: 0x0024ACE8
	public spr\u1AB4()
	{
		this.ᜁ = 8;
		this.ᜂ = new List<spr\u2340>();
		base..ctor();
	}

	// Token: 0x0600416E RID: 16750 RVA: 0x0024BD10 File Offset: 0x0024AD10
	public spr\u1AB4(Stream A_0)
	{
		int a_ = 1;
		this.ᜁ = 8;
		this.ᜂ = new List<spr\u2340>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䐶䴸䤺堼帾ⱀ", a_));
		}
		byte[] a_2 = new byte[4];
		this.ᜁ = sprṯ.ᜀ(A_0, a_2);
		int num = sprṯ.ᜀ(A_0, a_2);
		if (this.ᜂ.Capacity < num)
		{
			this.ᜂ.Capacity = num;
		}
		if (this.ᜁ != 8)
		{
			A_0.Position += (long)(this.ᜁ - 8);
		}
		for (int i = 0; i < num; i++)
		{
			spr\u2340 item = new spr\u2340(A_0);
			this.ᜂ.Add(item);
		}
	}

	// Token: 0x0600416F RID: 16751 RVA: 0x0024BDE0 File Offset: 0x0024ADE0
	public void ᜀ(Stream A_0)
	{
		int a_ = 0;
		int num = 2;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 0:
				goto IL_C3;
			case 1:
				goto IL_C3;
			case 3:
				goto IL_3C;
			case 4:
			{
				if (num2 >= count)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				spr\u2340 spr_u = this.ᜂ[num2];
				spr_u.ᜀ(A_0);
				num2++;
				num = 0;
				continue;
			}
			case 5:
				return;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			sprṯ.ᜀ(A_0, this.ᜁ);
			count = this.ᜂ.Count;
			sprṯ.ᜀ(A_0, count);
			num2 = 0;
			num = 1;
			continue;
			IL_C3:
			num = 4;
		}
		IL_3C:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䠹夻弽ⴿ", a_));
		}
	}

	// Token: 0x04001D19 RID: 7449
	private const int ᜀ = 8;

	// Token: 0x04001D1A RID: 7450
	private int ᜁ;

	// Token: 0x04001D1B RID: 7451
	private List<spr\u2340> ᜂ;
}
