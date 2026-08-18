using System;

// Token: 0x0200042C RID: 1068
[CLSCompliant(false)]
internal class spr\u2227
{
	// Token: 0x06003B67 RID: 15207 RVA: 0x00371780 File Offset: 0x00370780
	internal spr\u2227(spr\u1CC1 A_0, spr\u1CC1 A_1)
	{
		this.ᜂ = A_0;
		this.ᜃ = A_1;
		this.ᜁ(1000);
		this.ᜀ(720);
	}

	// Token: 0x06003B68 RID: 15208 RVA: 0x003717B8 File Offset: 0x003707B8
	internal spr\u2227()
	{
		this.ᜂ = new spr\u1CC1(61955);
		this.ᜂ.ᜁ(new byte[3]);
		this.ᜃ = new spr\u1CC1(61956);
		this.ᜃ.ᜁ(new byte[3]);
		this.ᜁ(1000);
		this.ᜀ(720);
	}

	// Token: 0x06003B69 RID: 15209 RVA: 0x00371824 File Offset: 0x00370824
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
		byte[] value = this.ᜂ.ᜅ();
		return BitConverter.ToUInt16(value, 1);
	}

	// Token: 0x06003B6A RID: 15210 RVA: 0x00371874 File Offset: 0x00370874
	internal void ᜁ(ushort A_0)
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
		byte[] bytes = BitConverter.GetBytes(A_0);
		this.ᜂ.ᜅ()[1] = bytes[0];
		this.ᜂ.ᜅ()[2] = bytes[1];
	}

	// Token: 0x06003B6B RID: 15211 RVA: 0x003718D8 File Offset: 0x003708D8
	internal ushort ᜀ()
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
		byte[] value = this.ᜃ.ᜅ();
		return BitConverter.ToUInt16(value, 1);
	}

	// Token: 0x06003B6C RID: 15212 RVA: 0x00371928 File Offset: 0x00370928
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
		byte[] bytes = BitConverter.GetBytes(A_0);
		this.ᜃ.ᜅ()[1] = bytes[0];
		this.ᜃ.ᜅ()[2] = bytes[1];
	}

	// Token: 0x04002B9D RID: 11165
	private const ushort ᜀ = 1000;

	// Token: 0x04002B9E RID: 11166
	private const ushort ᜁ = 720;

	// Token: 0x04002B9F RID: 11167
	private spr\u1CC1 ᜂ;

	// Token: 0x04002BA0 RID: 11168
	private spr\u1CC1 ᜃ;
}
