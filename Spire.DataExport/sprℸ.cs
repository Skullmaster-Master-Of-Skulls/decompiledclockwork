using System;

// Token: 0x02000019 RID: 25
internal class sprℸ : spr\u1B00
{
	// Token: 0x060000F1 RID: 241 RVA: 0x0000A19C File Offset: 0x0000919C
	public sprℸ(ushort A_0, ushort A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000A1B4 File Offset: 0x000091B4
	public override void ᜀ(byte[] A_0, ref int A_1)
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
		base.ᜀ(A_0, ref A_1);
		byte[] array = BitConverter.GetBytes(this.ᜀ);
		Array.Copy(array, 0, A_0, A_1, array.Length);
		A_1 += array.Length;
		array = BitConverter.GetBytes(this.ᜁ);
		Array.Copy(array, 0, A_0, A_1, array.Length);
		A_1 += array.Length;
		array = BitConverter.GetBytes(this.ᜂ);
		Array.Copy(array, 0, A_0, A_1, array.Length);
		A_1 += array.Length;
		array = new byte[12];
		Array.Copy(array, 0, A_0, A_1, array.Length);
		A_1 += array.Length;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000A278 File Offset: 0x00009278
	public ushort ᜂ()
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

	// Token: 0x060000F4 RID: 244 RVA: 0x0000A2BC File Offset: 0x000092BC
	public void ᜂ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000A300 File Offset: 0x00009300
	public ushort ᜁ()
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

	// Token: 0x060000F6 RID: 246 RVA: 0x0000A344 File Offset: 0x00009344
	public void ᜁ(ushort A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000A388 File Offset: 0x00009388
	public ushort ᜀ()
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

	// Token: 0x060000F8 RID: 248 RVA: 0x0000A3CC File Offset: 0x000093CC
	public void ᜀ(ushort A_0)
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

	// Token: 0x0400002E RID: 46
	private new ushort ᜀ;

	// Token: 0x0400002F RID: 47
	private ushort ᜁ;

	// Token: 0x04000030 RID: 48
	private ushort ᜂ;
}
