using System;
using System.IO;

// Token: 0x020000EC RID: 236
internal class spr\u205B
{
	// Token: 0x06000503 RID: 1283 RVA: 0x000311CC File Offset: 0x000301CC
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
		return 15 + this.ᜈ.Length + 1 + this.ᜊ.Length;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00031220 File Offset: 0x00030220
	public byte[] ᜀ()
	{
		byte[] buffer;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
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
					binaryWriter.Write(this.ᜈ);
					binaryWriter.Write(this.ᜉ);
					binaryWriter.Write(this.ᜊ);
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_102;
						case 2:
							((IDisposable)binaryWriter).Dispose();
							num = 0;
							continue;
						}
						if (binaryWriter == null)
						{
							break;
						}
						num = 2;
					}
					IL_102:;
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
						goto IL_145;
					case 1:
						((IDisposable)memoryStream).Dispose();
						num = 0;
						continue;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_145:;
			}
			break;
		}
		}
		return buffer;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00031394 File Offset: 0x00030394
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
		this.ᜀ = 0;
		this.ᜁ = 0;
		this.ᜂ = 0;
		this.ᜃ = 0;
		this.ᜄ = 0;
		this.ᜅ = 0;
		this.ᜆ = 0;
		this.ᜇ = 0;
		Array.Clear(this.ᜈ, 0, this.ᜈ.Length);
		this.ᜉ = 0;
		Array.Clear(this.ᜊ, 0, this.ᜊ.Length);
	}

	// Token: 0x0400054A RID: 1354
	public byte ᜀ;

	// Token: 0x0400054B RID: 1355
	public byte ᜁ;

	// Token: 0x0400054C RID: 1356
	public byte ᜂ;

	// Token: 0x0400054D RID: 1357
	public byte ᜃ;

	// Token: 0x0400054E RID: 1358
	public int ᜄ;

	// Token: 0x0400054F RID: 1359
	public ushort ᜅ;

	// Token: 0x04000550 RID: 1360
	public int ᜆ;

	// Token: 0x04000551 RID: 1361
	public byte ᜇ;

	// Token: 0x04000552 RID: 1362
	public byte[] ᜈ = new byte[13];

	// Token: 0x04000553 RID: 1363
	public byte ᜉ;

	// Token: 0x04000554 RID: 1364
	public byte[] ᜊ = new byte[3];
}
