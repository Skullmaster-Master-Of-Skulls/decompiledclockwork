using System;
using System.Collections;

// Token: 0x0200015B RID: 347
internal abstract class spr\u170D : sprᢿ
{
	// Token: 0x0600099F RID: 2463 RVA: 0x0008137C File Offset: 0x0008037C
	protected void ᜃ(spr\u24A6 A_0)
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
		A_0.ᜀ(this);
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x000813C0 File Offset: 0x000803C0
	public override void ᜀ(spr\u24A6 A_0)
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
		spr\u24B5 value = new spr\u24B5(A_0, this.ᜁ());
		this.ᜁ.Add(value);
		this.ᜀ = value;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0008141C File Offset: 0x0008041C
	public override void ᜁ(spr\u24A6 A_0)
	{
		for (;;)
		{
			IL_14:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_34;
			default:
				goto IL_34;
			}
			int num;
			int count;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					this.ᜀ = (spr\u24B5)this.ᜁ[count - 2];
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					if (count - 1 > 0)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					return;
				}
				goto IL_14;
			}
			IL_34:
			if (false)
			{
			}
			this.ᜀ.ᜀ(this.ᜀ());
			count = this.ᜁ.Count;
			this.ᜁ.RemoveAt(count - 1);
			num = 1;
			goto IL_02;
		}
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x000814D4 File Offset: 0x000804D4
	public override void ᜀ(spr\u1B70 A_0)
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
		spr\u1B70[] a_ = this.ᜁ(A_0);
		this.ᜀ.ᜀ(A_0, a_);
	}

	// Token: 0x060009A3 RID: 2467
	protected abstract spr\u1B70[] ᜁ(spr\u1B70 A_0);

	// Token: 0x060009A4 RID: 2468 RVA: 0x00081524 File Offset: 0x00080524
	protected virtual int ᜁ()
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
		return 1;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00081560 File Offset: 0x00080560
	protected virtual bool ᜀ()
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
		return false;
	}

	// Token: 0x04001390 RID: 5008
	private new spr\u24B5 ᜀ;

	// Token: 0x04001391 RID: 5009
	private new readonly ArrayList ᜁ = new ArrayList();
}
