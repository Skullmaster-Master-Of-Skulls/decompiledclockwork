using System;
using System.IO;

// Token: 0x020002E3 RID: 739
internal class sprἾ : spr\u23F8
{
	// Token: 0x06002894 RID: 10388 RVA: 0x00286A38 File Offset: 0x00285A38
	public ushort ᜅ()
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

	// Token: 0x06002895 RID: 10389 RVA: 0x00286A7C File Offset: 0x00285A7C
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
		this.ᜀ = A_0;
	}

	// Token: 0x06002896 RID: 10390 RVA: 0x00286AC0 File Offset: 0x00285AC0
	public bool ᜁ()
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
		return this.ᜁ == 1;
	}

	// Token: 0x06002897 RID: 10391 RVA: 0x00286B04 File Offset: 0x00285B04
	public void ᜀ(bool A_0)
	{
		while (A_0)
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
				if (true)
				{
				}
				this.ᜁ = 1;
				return;
			}
		}
		this.ᜁ = 0;
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x00286B54 File Offset: 0x00285B54
	public bool ᜀ()
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
		return this.ᜂ == 1;
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x00286B98 File Offset: 0x00285B98
	public void ᜁ(bool A_0)
	{
		while (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = 1;
				return;
			}
		}
		this.ᜂ = 0;
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x00286BE8 File Offset: 0x00285BE8
	public uint ᜂ()
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

	// Token: 0x0600289B RID: 10395 RVA: 0x00286C2C File Offset: 0x00285C2C
	public void ᜀ(uint A_0)
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

	// Token: 0x0600289C RID: 10396 RVA: 0x00286C70 File Offset: 0x00285C70
	public byte[] ᜄ()
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
		return this.ᜄ;
	}

	// Token: 0x0600289D RID: 10397 RVA: 0x00286CB4 File Offset: 0x00285CB4
	public void ᜀ(byte[] A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x00286CF8 File Offset: 0x00285CF8
	public int ᜁ(Stream A_0)
	{
		int num2;
		for (;;)
		{
			ushort num = spr\u23F8.ᜅ(A_0);
			this.ᜀ = (num & 16383);
			this.ᜁ = (ushort)((num & 16384) >> 14);
			this.ᜂ = (ushort)((num & 32768) >> 15);
			this.ᜃ = spr\u23F8.ᜃ(A_0);
			num2 = 6;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return num2;
			default:
			{
				if (false)
				{
				}
				int num3 = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num3)
					{
					case 0:
						if (this.ᜀ())
						{
							num3 = 1;
							continue;
						}
						return num2;
					case 1:
						num2 += (int)this.ᜃ;
						this.ᜄ = new byte[this.ᜃ];
						num3 = 2;
						continue;
					case 2:
						return num2;
					}
					break;
				}
				break;
			}
			}
		}
		return num2;
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x00286DD8 File Offset: 0x00285DD8
	public void ᜀ(Stream A_0)
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
		short num = (short)this.ᜀ;
		num += (short)(this.ᜁ << 14);
		num += (short)(this.ᜂ << 15);
		spr\u23F8.ᜀ(A_0, (ushort)num);
		spr\u23F8.ᜀ(A_0, this.ᜃ);
	}

	// Token: 0x04002363 RID: 9059
	private new ushort ᜀ;

	// Token: 0x04002364 RID: 9060
	private new ushort ᜁ;

	// Token: 0x04002365 RID: 9061
	private new ushort ᜂ;

	// Token: 0x04002366 RID: 9062
	private new uint ᜃ;

	// Token: 0x04002367 RID: 9063
	private new byte[] ᜄ;
}
