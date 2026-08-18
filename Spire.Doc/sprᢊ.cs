using System;
using System.IO;

// Token: 0x02000246 RID: 582
internal class sprᢊ
{
	// Token: 0x06001CCE RID: 7374 RVA: 0x001D16BC File Offset: 0x001D06BC
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
		return (this.ᜀ & 1) != 0;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x001D1708 File Offset: 0x001D0708
	internal void ᜂ(bool A_0)
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
		this.ᜀ = ((this.ᜀ & 254) | (A_0 ? 1 : 0));
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x001D1764 File Offset: 0x001D0764
	internal bool ᜅ()
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

	// Token: 0x06001CD1 RID: 7377 RVA: 0x001D17B0 File Offset: 0x001D07B0
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
		this.ᜀ = (byte)((int)(this.ᜀ & 253) | (A_0 ? 1 : 0) << 1);
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x001D180C File Offset: 0x001D080C
	internal byte ᜄ()
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
		return (byte)((this.ᜀ & 12) >> 2);
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x001D1854 File Offset: 0x001D0854
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
		this.ᜀ = (byte)((int)(this.ᜀ & 243) | (int)A_0 << 2);
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x001D18A8 File Offset: 0x001D08A8
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
		return (this.ᜀ & 16) >> 4 != 0;
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x001D18F4 File Offset: 0x001D08F4
	internal void ᜀ(bool A_0)
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
		this.ᜀ = (byte)((int)(this.ᜀ & 239) | (A_0 ? 1 : 0) << 4);
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x001D1950 File Offset: 0x001D0950
	internal ushort ᜁ()
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

	// Token: 0x06001CD7 RID: 7383 RVA: 0x001D1994 File Offset: 0x001D0994
	internal void ᜀ(ushort A_0)
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

	// Token: 0x06001CD8 RID: 7384 RVA: 0x001D19D8 File Offset: 0x001D09D8
	internal uint ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x001D1A1C File Offset: 0x001D0A1C
	internal void ᜁ(uint A_0)
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

	// Token: 0x06001CDA RID: 7386 RVA: 0x001D1A60 File Offset: 0x001D0A60
	internal uint ᜆ()
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

	// Token: 0x06001CDB RID: 7387 RVA: 0x001D1AA4 File Offset: 0x001D0AA4
	internal void ᜀ(uint A_0)
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

	// Token: 0x06001CDC RID: 7388 RVA: 0x001D1AE8 File Offset: 0x001D0AE8
	internal sprᢊ()
	{
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x001D1B04 File Offset: 0x001D0B04
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
		this.ᜀ = (byte)A_0.ReadByte();
		A_0.ReadByte();
		this.ᜁ = spr\u23F8.ᜅ(A_0);
		this.ᜂ = spr\u23F8.ᜃ(A_0);
		this.ᜃ = spr\u23F8.ᜃ(A_0);
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x001D1B78 File Offset: 0x001D0B78
	internal void ᜀ(Stream A_0)
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
		A_0.WriteByte(this.ᜀ);
		A_0.WriteByte(0);
		spr\u23F8.ᜀ(A_0, this.ᜁ);
		spr\u23F8.ᜀ(A_0, this.ᜂ);
		spr\u23F8.ᜀ(A_0, this.ᜃ);
	}

	// Token: 0x04001F54 RID: 8020
	private byte ᜀ;

	// Token: 0x04001F55 RID: 8021
	private ushort ᜁ = 25;

	// Token: 0x04001F56 RID: 8022
	private uint ᜂ;

	// Token: 0x04001F57 RID: 8023
	private uint ᜃ;
}
