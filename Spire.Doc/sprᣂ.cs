using System;

// Token: 0x02000208 RID: 520
internal class sprᣂ
{
	// Token: 0x0600185F RID: 6239 RVA: 0x00176638 File Offset: 0x00175638
	internal short ᜀ()
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

	// Token: 0x06001860 RID: 6240 RVA: 0x0017667C File Offset: 0x0017567C
	internal void ᜀ(short A_0)
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

	// Token: 0x06001861 RID: 6241 RVA: 0x001766C0 File Offset: 0x001756C0
	internal byte ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x00176704 File Offset: 0x00175704
	internal void ᜁ(byte A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x00176748 File Offset: 0x00175748
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
		return this.ᜄ;
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x0017678C File Offset: 0x0017578C
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
		this.ᜄ = A_0;
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x001767D0 File Offset: 0x001757D0
	internal sprᣂ()
	{
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x001767F0 File Offset: 0x001757F0
	internal void ᜀ(byte[] A_0)
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
		this.ᜂ = BitConverter.ToInt16(A_0, 0);
		this.ᜃ = A_0[2];
		this.ᜄ = A_0[3];
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x0017684C File Offset: 0x0017584C
	internal byte[] ᜃ()
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
		byte[] array = new byte[4];
		BitConverter.GetBytes(this.ᜂ).CopyTo(array, 0);
		array[2] = this.ᜃ;
		array[3] = this.ᜄ;
		return array;
	}

	// Token: 0x04001C9A RID: 7322
	internal const int ᜀ = 4;

	// Token: 0x04001C9B RID: 7323
	internal const int ᜁ = 240;

	// Token: 0x04001C9C RID: 7324
	private short ᜂ;

	// Token: 0x04001C9D RID: 7325
	private byte ᜃ;

	// Token: 0x04001C9E RID: 7326
	private byte ᜄ = 240;
}
