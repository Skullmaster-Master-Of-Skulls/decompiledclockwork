using System;
using System.IO;

// Token: 0x020000ED RID: 237
internal class sprỚ
{
	// Token: 0x06000507 RID: 1287 RVA: 0x00031464 File Offset: 0x00030464
	public int ᜁ()
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
		return this.ᜀ.Length + 1 + 1 + 4 + 1 + 1 + this.ᜆ.Length + 1;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x000314BC File Offset: 0x000304BC
	public byte[] ᜀ()
	{
		byte[] buffer;
		for (;;)
		{
			MemoryStream memoryStream = new MemoryStream(this.ᜁ());
			try
			{
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				try
				{
					binaryWriter.Write(this.ᜀ);
					binaryWriter.Write(this.ᜁ);
					binaryWriter.Write(this.ᜂ);
					binaryWriter.Write(this.ᜃ);
					binaryWriter.Write(this.ᜄ);
					binaryWriter.Write(this.ᜅ);
					binaryWriter.Write(this.ᜆ);
					binaryWriter.Write(this.ᜇ);
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_BA;
						case 1:
							((IDisposable)binaryWriter).Dispose();
							num = 0;
							continue;
						}
						if (binaryWriter == null)
						{
							break;
						}
						num = 1;
					}
					IL_BA:;
				}
				buffer = memoryStream.GetBuffer();
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)memoryStream).Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_FD;
					}
					if (memoryStream == null)
					{
						goto IL_107;
					}
					num = 0;
				}
				IL_FD:
				if (true)
				{
				}
				IL_107:;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_11E;
			}
		}
		IL_11E:
		if (false)
		{
		}
		return buffer;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0003160C File Offset: 0x0003060C
	public void ᜂ()
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
		Array.Clear(this.ᜀ, 0, this.ᜀ.Length);
		this.ᜁ = 0;
		this.ᜂ = 0;
		this.ᜃ = 0;
		this.ᜄ = 0;
		this.ᜅ = 0;
		Array.Clear(this.ᜆ, 0, this.ᜆ.Length);
		this.ᜇ = 0;
	}

	// Token: 0x04000555 RID: 1365
	public byte[] ᜀ = new byte[10];

	// Token: 0x04000556 RID: 1366
	public byte ᜁ;

	// Token: 0x04000557 RID: 1367
	public byte ᜂ;

	// Token: 0x04000558 RID: 1368
	public int ᜃ;

	// Token: 0x04000559 RID: 1369
	public byte ᜄ;

	// Token: 0x0400055A RID: 1370
	public byte ᜅ;

	// Token: 0x0400055B RID: 1371
	public byte[] ᜆ = new byte[13];

	// Token: 0x0400055C RID: 1372
	public byte ᜇ;
}
