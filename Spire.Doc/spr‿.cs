using System;
using System.IO;

// Token: 0x02000245 RID: 581
internal class spr\u203F
{
	// Token: 0x06001CC0 RID: 7360 RVA: 0x001D11C0 File Offset: 0x001D01C0
	internal bool ᜁ()
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
		return (this.ᜀ & 1) != 0;
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x001D120C File Offset: 0x001D020C
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
		this.ᜀ = ((this.ᜀ & 65534) | (A_0 ? 1 : 0));
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x001D1268 File Offset: 0x001D0268
	internal bool ᜀ()
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
		return (this.ᜀ & 2) >> 1 != 0;
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x001D12B4 File Offset: 0x001D02B4
	internal void ᜃ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ = (ushort)((int)(this.ᜀ & 65533) | (A_0 ? 1 : 0) << 1);
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x001D1310 File Offset: 0x001D0310
	internal byte ᜂ()
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
		return (byte)((this.ᜀ & 480) >> 5);
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x001D135C File Offset: 0x001D035C
	internal void ᜀ(byte A_0)
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
		this.ᜀ = (ushort)((int)(this.ᜀ & 65055) | (int)A_0 << 5);
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x001D13B0 File Offset: 0x001D03B0
	internal bool ᜃ()
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
		return (this.ᜀ & 512) >> 9 != 0;
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x001D1400 File Offset: 0x001D0400
	internal void ᜁ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ = (ushort)((int)(this.ᜀ & 65023) | (A_0 ? 1 : 0) << 9);
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x001D1460 File Offset: 0x001D0460
	internal bool ᜅ()
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
		return (this.ᜀ & 1024) >> 10 != 0;
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x001D14B0 File Offset: 0x001D04B0
	internal void ᜂ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ = (ushort)((int)(this.ᜀ & 64511) | (A_0 ? 1 : 0) << 10);
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x001D1510 File Offset: 0x001D0510
	internal sprḻ ᜄ()
	{
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_50;
			case 2:
				goto IL_6F;
			}
			if (this.ᜁ == null)
			{
				num = 1;
				continue;
			}
			break;
			IL_50:
			this.ᜁ = new sprḻ();
			num = 2;
		}
		IL_6F:
		return this.ᜁ;
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x001D1594 File Offset: 0x001D0594
	internal spr\u203F(spr\u202E A_0)
	{
		this.ᜂ = A_0;
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x001D15BC File Offset: 0x001D05BC
	internal void ᜁ(Stream A_0)
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
		spr\u23F8.ᜃ(A_0);
		this.ᜀ = spr\u23F8.ᜅ(A_0);
		spr\u23F8.ᜅ(A_0);
		spr\u23F8.ᜃ(A_0);
		spr\u23F8.ᜃ(A_0);
		spr\u23F8.ᜃ(A_0);
		spr\u23F8.ᜃ(A_0);
		this.ᜄ().ᜁ(A_0);
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x001D163C File Offset: 0x001D063C
	internal void ᜀ(Stream A_0)
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
		spr\u23F8.ᜀ(A_0, 0U);
		spr\u23F8.ᜀ(A_0, this.ᜀ);
		spr\u23F8.ᜀ(A_0, 0);
		spr\u23F8.ᜀ(A_0, 0U);
		spr\u23F8.ᜀ(A_0, 0U);
		spr\u23F8.ᜀ(A_0, 0U);
		spr\u23F8.ᜀ(A_0, 0U);
		this.ᜄ().ᜀ(A_0);
	}

	// Token: 0x04001F51 RID: 8017
	private ushort ᜀ = 1059;

	// Token: 0x04001F52 RID: 8018
	private sprḻ ᜁ;

	// Token: 0x04001F53 RID: 8019
	private spr\u202E ᜂ;
}
