using System;

// Token: 0x02000018 RID: 24
internal class spr\u1B00
{
	// Token: 0x060000ED RID: 237 RVA: 0x0000A06C File Offset: 0x0000906C
	public spr\u1B00(ushort A_0, ushort A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000A090 File Offset: 0x00009090
	public virtual void ᜀ(byte[] A_0, ref int A_1)
	{
		if (A_0 == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return;
			}
		}
		byte[] bytes = BitConverter.GetBytes(this.ᜀ);
		Array.Copy(bytes, 0, A_0, A_1, bytes.Length);
		A_1 += bytes.Length;
		bytes = BitConverter.GetBytes(this.ᜁ);
		Array.Copy(bytes, 0, A_0, A_1, bytes.Length);
		A_1 += bytes.Length;
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000A114 File Offset: 0x00009114
	public ushort ᜃ()
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
		return this.ᜀ;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000A158 File Offset: 0x00009158
	public ushort ᜄ()
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
		return this.ᜁ;
	}

	// Token: 0x0400002C RID: 44
	private ushort ᜀ;

	// Token: 0x0400002D RID: 45
	private ushort ᜁ;
}
