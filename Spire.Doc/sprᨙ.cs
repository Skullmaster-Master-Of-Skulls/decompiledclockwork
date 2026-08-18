using System;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x0200021A RID: 538
internal class spr\u1A19 : spr\u23AC
{
	// Token: 0x0600193B RID: 6459 RVA: 0x00189D14 File Offset: 0x00188D14
	internal spr\u1A19(string A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x00189D30 File Offset: 0x00188D30
	internal spr\u1A19(spr\u1B02 A_0)
	{
		int a_ = 4;
		base..ctor();
		MemoryStream memoryStream = A_0.ᜃ(ClipboardData.b("楩⍫⵭⡯㱱㕳㭵㵷", a_));
		if (memoryStream != null)
		{
			BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.Unicode);
			StringBuilder stringBuilder = new StringBuilder();
			while (spr\u1CC6.ᜀ(binaryReader, 2))
			{
				char c = binaryReader.ReadChar();
				if (c == '\0')
				{
					IL_52:
					this.ᜀ = stringBuilder.ToString();
					return;
				}
				stringBuilder.Append(c);
			}
			goto IL_52;
		}
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x00189DAC File Offset: 0x00188DAC
	void spr\u23AC.ᜀ(BinaryWriter A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (num2 >= this.ᜀ.Length)
				{
					goto IL_67;
				}
				char c = this.ᜀ[num2];
				A_0.Write((byte)c);
				A_0.Write(0);
				num2++;
				num = 2;
				continue;
			}
			case 2:
				goto IL_51;
			case 3:
			{
				if (true)
				{
				}
				int num2 = 0;
				num = 5;
				continue;
			}
			case 4:
				goto IL_AD;
			case 5:
				goto IL_51;
			}
			if (spr\u1CC6.ᜋ(this.ᜀ))
			{
				num = 3;
				continue;
			}
			goto IL_AD;
			IL_51:
			num = 0;
			continue;
			IL_67:
			num = 4;
			continue;
			IL_AD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_67;
			default:
				goto IL_C3;
			}
		}
		IL_C3:
		if (false)
		{
		}
		A_0.Write(0);
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x00189E8C File Offset: 0x00188E8C
	string spr\u23AC.ᜁ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return ClipboardData.b("筷㕹㽻♽칿쎁즃쎅", a_);
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x00189EE0 File Offset: 0x00188EE0
	internal string ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x04001CEC RID: 7404
	private readonly string ᜀ;
}
