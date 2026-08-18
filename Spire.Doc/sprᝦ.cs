using System;
using System.IO;
using System.Text;

// Token: 0x0200026C RID: 620
internal class sprᝦ
{
	// Token: 0x0600211B RID: 8475 RVA: 0x00229420 File Offset: 0x00228420
	internal short ᜄ()
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

	// Token: 0x0600211C RID: 8476 RVA: 0x00229464 File Offset: 0x00228464
	internal void ᜂ(short A_0)
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

	// Token: 0x0600211D RID: 8477 RVA: 0x002294A8 File Offset: 0x002284A8
	internal string ᜂ()
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

	// Token: 0x0600211E RID: 8478 RVA: 0x002294EC File Offset: 0x002284EC
	internal void ᜀ(string A_0)
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

	// Token: 0x0600211F RID: 8479 RVA: 0x00229530 File Offset: 0x00228530
	internal short ᜃ()
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

	// Token: 0x06002120 RID: 8480 RVA: 0x00229574 File Offset: 0x00228574
	internal void ᜁ(short A_0)
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

	// Token: 0x06002121 RID: 8481 RVA: 0x002295B8 File Offset: 0x002285B8
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
		return this.ᜄ;
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x002295FC File Offset: 0x002285FC
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x00229640 File Offset: 0x00228640
	internal int ᜁ()
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
		return this.ᜅ;
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x00229684 File Offset: 0x00228684
	internal void ᜀ(int A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x002296C8 File Offset: 0x002286C8
	internal sprᝦ(BinaryReader A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x002296E4 File Offset: 0x002286E4
	internal sprᝦ()
	{
		this.ᜀ(-1);
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x00229700 File Offset: 0x00228700
	internal void ᜀ(BinaryReader A_0)
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
		byte[] array = A_0.ReadBytes(20);
		this.ᜁ = Encoding.Unicode.GetString(array).Substring(1, (int)array[0]);
		this.ᜂ = A_0.ReadInt16();
		this.ᜃ = A_0.ReadInt16();
		this.ᜄ = A_0.ReadInt16();
		this.ᜅ = A_0.ReadInt32();
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x00229790 File Offset: 0x00228790
	internal void ᜀ(BinaryWriter A_0)
	{
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				text = string.Empty;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B5;
					case 1:
						goto IL_124;
					case 2:
						if (text.Length < 10)
						{
							num = 7;
							continue;
						}
						goto IL_16E;
					case 3:
						goto IL_B5;
					case 4:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						text += '\0';
						num2++;
						num = 9;
						continue;
					}
					case 5:
						goto IL_105;
					case 6:
						goto IL_4E;
					case 7:
					{
						int num2 = 0;
						int num3 = 10 - text.Length;
						num = 5;
						continue;
					}
					case 8:
						text = '9' + this.ᜁ.Substring(0, 9);
						num = 3;
						continue;
					case 9:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						default:
							if (false)
							{
							}
							goto IL_105;
						}
						break;
					}
					break;
					IL_4E:
					if (this.ᜁ.Length > 9)
					{
						num = 8;
						continue;
					}
					text = (char)this.ᜁ.Length + this.ᜁ;
					num = 0;
					continue;
					IL_B5:
					num = 2;
					continue;
					IL_105:
					num = 4;
				}
			}
			IL_124:
			IL_16E:
			byte[] bytes = Encoding.Unicode.GetBytes(text);
			A_0.Write(bytes, 0, bytes.Length);
			A_0.Write(this.ᜂ);
			A_0.Write(this.ᜃ);
			A_0.Write(this.ᜄ);
			A_0.Write(this.ᜅ);
			return;
		}
		}
	}

	// Token: 0x04002071 RID: 8305
	internal const int ᜀ = 30;

	// Token: 0x04002072 RID: 8306
	private string ᜁ;

	// Token: 0x04002073 RID: 8307
	private short ᜂ;

	// Token: 0x04002074 RID: 8308
	private short ᜃ;

	// Token: 0x04002075 RID: 8309
	private short ᜄ;

	// Token: 0x04002076 RID: 8310
	private int ᜅ;
}
