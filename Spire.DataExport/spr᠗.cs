using System;

// Token: 0x0200001A RID: 26
internal class spr᠗ : spr\u1DEE
{
	// Token: 0x060000F9 RID: 249 RVA: 0x0000A410 File Offset: 0x00009410
	public spr᠗(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
		double value = 0.0;
		this.ᜁ = null;
		if (sprᮌ.ᜁ(A_3, 12) != 65535)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(A_3, 6, bytes, 0, bytes.Length);
			this.ᜁ = BitConverter.ToDouble(bytes, 0);
		}
		else
		{
			switch (A_3[6])
			{
			case 0:
				this.ᜁ = string.Empty;
				break;
			case 1:
				this.ᜁ = (A_3[8] == 1);
				break;
			}
		}
		Array.Clear(A_3, 6, 8);
		A_3[6] = 2;
		sprᮌ.ᜀ(A_3, 12, ushort.MaxValue);
		Array.Clear(A_3, 16, 4);
		int num = 14;
		A_3[num] |= 2;
		this.ᜂ = sprᮌ.ᜁ(A_3, 20);
		if (this.ᜂ > 0)
		{
			this.ᜃ = new byte[(int)this.ᜂ];
			Array.Copy(A_3, 22, this.ᜃ, 0, (int)this.ᜂ);
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000A538 File Offset: 0x00009538
	protected override void ᜀ(bool A_0)
	{
		if (!this.ᜀ)
		{
			if (true)
			{
			}
			try
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AD;
					case 1:
						goto IL_9B;
					case 2:
						this.ᜃ = null;
						num = 1;
						continue;
					case 3:
						if (this.ᜃ != null)
						{
							num = 2;
							continue;
						}
						goto IL_9B;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AD;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					if (A_0)
					{
						num = 4;
						continue;
					}
					IL_9B:
					this.ᜀ = true;
					num = 0;
				}
				IL_AD:;
			}
			finally
			{
				base.ᜀ(A_0);
			}
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000A60C File Offset: 0x0000960C
	protected override bool ᜅ()
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
		return true;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000A648 File Offset: 0x00009648
	protected override object ᜀ()
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

	// Token: 0x060000FD RID: 253 RVA: 0x0000A68C File Offset: 0x0000968C
	protected override void ᜀ(object A_0)
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

	// Token: 0x060000FE RID: 254 RVA: 0x0000A6D0 File Offset: 0x000096D0
	protected override string ᜁ()
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
		return (string)this.ᜀ();
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000A718 File Offset: 0x00009718
	protected override void ᜀ(string A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000A75C File Offset: 0x0000975C
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
		return this.ᜂ;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000A7A0 File Offset: 0x000097A0
	public byte[] ᜈ()
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

	// Token: 0x04000031 RID: 49
	private new bool ᜀ;

	// Token: 0x04000032 RID: 50
	private new object ᜁ;

	// Token: 0x04000033 RID: 51
	private new ushort ᜂ;

	// Token: 0x04000034 RID: 52
	private byte[] ᜃ;
}
