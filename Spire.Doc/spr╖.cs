using System;
using System.Collections;
using System.Runtime.InteropServices;

// Token: 0x0200044D RID: 1101
[ComVisible(true)]
[CLSCompliant(false)]
internal class spr\u2556 : ArrayList, spr\u2318
{
	// Token: 0x06003D0B RID: 15627 RVA: 0x0038D64C File Offset: 0x0038C64C
	public uint ᜀ(ref spr\u2318 A_0)
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
		return 2147483649U;
	}

	// Token: 0x06003D0C RID: 15628 RVA: 0x0038D68C File Offset: 0x0038C68C
	public uint ᜀ(uint A_0, ref sprῙ A_1, ref uint A_2)
	{
		int num = 3;
		int num3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				num2 = 1;
				goto IL_59;
			case 1:
				if (this.ᜀ >= this.Count)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_68;
				default:
					goto IL_AC;
				}
				break;
			case 2:
				num2 = (int)A_0;
				goto IL_59;
			case 4:
				return 2147500037U;
			case 5:
				if (true)
				{
				}
				num = 2;
				continue;
			}
			if (A_0 <= 1U)
			{
				num = 5;
				continue;
			}
			num = 0;
			continue;
			IL_68:
			num = 1;
			continue;
			IL_59:
			num3 = num2;
			this.ᜀ += num3;
			goto IL_68;
		}
		return 2147500037U;
		IL_AC:
		if (false)
		{
		}
		A_1 = ((spr\u1CE6)this[this.ᜀ]).ᜂ();
		A_2 = (uint)num3;
		return 0U;
	}

	// Token: 0x06003D0D RID: 15629 RVA: 0x0038D76C File Offset: 0x0038C76C
	public uint ᜀ()
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
		this.ᜀ = -1;
		return 0U;
	}

	// Token: 0x06003D0E RID: 15630 RVA: 0x0038D7B0 File Offset: 0x0038C7B0
	public uint ᜀ(uint A_0)
	{
		if ((long)this.ᜀ + (long)((ulong)A_0) > (long)this.Count)
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
				break;
			}
			if (true)
			{
			}
			return 2147500037U;
		}
		this.ᜀ += (int)A_0;
		return 0U;
	}

	// Token: 0x04002C1F RID: 11295
	private int ᜀ = -1;
}
