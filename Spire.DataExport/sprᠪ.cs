using System;
using System.Collections;
using System.Reflection;

// Token: 0x0200003E RID: 62
[DefaultMember("Item")]
internal class sprᠪ : spr\u2574
{
	// Token: 0x060001FB RID: 507 RVA: 0x00012A14 File Offset: 0x00011A14
	public sprᠪ(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00012A28 File Offset: 0x00011A28
	protected virtual int ᜅ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (true)
					{
					}
					goto IL_2E;
				case 1:
					goto IL_2E;
				case 2:
					if (num2 < base.ᜌ())
					{
						num += this.ᜀ(num2).ᜥ();
						num2++;
						num3 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					break;
				case 3:
					return num;
				}
				break;
				IL_2E:
				num3 = 2;
			}
		}
		return num;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00012AC8 File Offset: 0x00011AC8
	public int ᜀ(spr\u2320 A_0)
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
		return base.ᜁ(A_0);
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00012B0C File Offset: 0x00011B0C
	public void ᜁ(int A_0, spr\u2320 A_1)
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
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00012B50 File Offset: 0x00011B50
	public void ᜀ(sprḗ A_0)
	{
		IEnumerator enumerator = base.ᜇ();
		try
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u2320 spr_u;
					spr_u.ᜀ(A_0);
					num = 1;
					continue;
				}
				case 3:
					num = 6;
					continue;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num = 3;
						continue;
					}
					spr\u2320 spr_u = (spr\u2320)enumerator.Current;
					num = 5;
					continue;
				}
				case 5:
				{
					spr\u2320 spr_u;
					if (spr_u != null)
					{
						num = 0;
						continue;
					}
					break;
				}
				case 6:
					goto IL_8B;
				}
				IL_58:
				num = 4;
				continue;
				goto IL_58;
			}
			IL_8B:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E7;
					case 1:
						if (disposable == null)
						{
							goto IL_E9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DF;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						disposable.Dispose();
						goto IL_DF;
					}
					break;
					IL_DF:
					num = 0;
				}
			}
			IL_E7:
			IL_E9:;
		}
		if (true)
		{
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00012C6C File Offset: 0x00011C6C
	public new spr\u2320 ᜀ(int A_0)
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
		return base.ᜀ(A_0) as spr\u2320;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00012CB4 File Offset: 0x00011CB4
	public void ᜀ(int A_0, spr\u2320 A_1)
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
		base.ᜀ(A_0, A_1);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00012CF8 File Offset: 0x00011CF8
	public int ᜆ()
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
		return this.ᜅ();
	}
}
