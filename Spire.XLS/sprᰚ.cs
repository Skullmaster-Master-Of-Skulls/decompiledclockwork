using System;
using System.Collections;
using System.Runtime.InteropServices;

// Token: 0x02000415 RID: 1045
[CLSCompliant(false)]
[ComVisible(true)]
internal class sprᰚ : ArrayList, spr\u2229
{
	// Token: 0x06003E9A RID: 16026 RVA: 0x0022B468 File Offset: 0x0022A468
	public uint ᜀ(ref spr\u2229 A_0)
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

	// Token: 0x06003E9B RID: 16027 RVA: 0x0022B4A8 File Offset: 0x0022A4A8
	public uint ᜀ(uint A_0, ref spr\u1FCC A_1, ref uint A_2)
	{
		int num3;
		for (;;)
		{
			IL_00:
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num2 = (int)A_0;
					goto IL_78;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					if (this.ᜀ >= this.Count)
					{
						num = 3;
						continue;
					}
					goto IL_BB;
				case 3:
					return 2147500037U;
				case 4:
					num2 = 1;
					goto IL_78;
				}
				if (A_0 <= 1U)
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
				IL_78:
				num3 = num2;
				this.ᜀ += num3;
				num = 2;
			}
		}
		return 2147500037U;
		IL_BB:
		A_1 = ((spr\u17F2)this[this.ᜀ]).ᜂ();
		A_2 = (uint)num3;
		return 0U;
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x0022B590 File Offset: 0x0022A590
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

	// Token: 0x06003E9D RID: 16029 RVA: 0x0022B5D4 File Offset: 0x0022A5D4
	public uint ᜀ(uint A_0)
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
			if ((long)this.ᜀ + (long)((ulong)A_0) <= (long)this.Count)
			{
				this.ᜀ += (int)A_0;
				return 0U;
			}
			break;
		}
		if (true)
		{
		}
		return 2147500037U;
	}

	// Token: 0x04001AD0 RID: 6864
	private int ᜀ = -1;
}
