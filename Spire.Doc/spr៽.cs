using System;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x02000340 RID: 832
internal class spr\u17FD : spr\u2562
{
	// Token: 0x06002C7E RID: 11390 RVA: 0x002AEC58 File Offset: 0x002ADC58
	internal override int ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_84;
			case 1:
				num = 2;
				continue;
			case 2:
				if (this.ᜄ != null)
				{
					num = 0;
					continue;
				}
				return 0;
			case 3:
				if (true)
				{
				}
				break;
			}
			if (this.ᜃ == null)
			{
				return 0;
			}
			num = 1;
		}
		IL_84:
		return this.ᜃ.Length + this.ᜄ.Length + 4 + 8;
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x002AECF8 File Offset: 0x002ADCF8
	internal spr\u17FD(spr\u2578 A_0)
	{
		byte[] array = new byte[A_0.Length];
		A_0.Read(array, 0, array.Length);
		this.ᜁ(array, 0);
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x002AED2C File Offset: 0x002ADD2C
	internal spr\u17FD(string A_0)
	{
		this.ᜅ = A_0;
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
		this.ᜃ = asciiencoding.GetBytes(this.ᜅ);
		this.ᜄ = unicodeEncoding.GetBytes(this.ᜅ);
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x002AED78 File Offset: 0x002ADD78
	internal override void ᜁ(byte[] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				for (;;)
				{
					num = 0;
					int num2 = 0;
					int num3 = A_0.Length;
					int num4 = 8;
					for (;;)
					{
						int num5;
						switch (num4)
						{
						case 0:
							if (true)
							{
							}
							goto IL_D1;
						case 1:
							num -= 3;
							num4 = 3;
							continue;
						case 2:
							if (num2 >= num3)
							{
								num4 = 4;
								continue;
							}
							num4 = 5;
							continue;
						case 3:
							goto IL_6F;
						case 4:
							goto IL_ED;
						case 5:
							if (num > 0)
							{
								num4 = 1;
								continue;
							}
							goto IL_6F;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num2++;
								num4 = 0;
								continue;
							}
							break;
						case 7:
							if (num5 != -858997829)
							{
								num4 = 6;
								continue;
							}
							goto IL_104;
						case 8:
							goto IL_D1;
						}
						break;
						IL_6F:
						num5 = spr\u2562.ᜃ(A_0, ref num);
						num4 = 7;
						continue;
						IL_D1:
						num4 = 2;
					}
				}
			}
			IL_ED:
			IL_104:
			byte[] array = new byte[num - 4];
			int num6 = 0;
			this.ᜃ = spr\u2562.ᜀ(A_0, num - 4, ref num6);
			num6 += 4;
			int a_ = A_0.Length - array.Length - 4;
			this.ᜄ = spr\u2562.ᜀ(A_0, a_, ref num6);
			return;
		}
		}
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x002AEECC File Offset: 0x002ADECC
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotImplementedException(ClipboardData.b("㝸ᑺॼ彾", a_));
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x002AEF24 File Offset: 0x002ADF24
	internal void ᜀ(sprᤘ A_0)
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
		byte[] array = new byte[this.ᜀ()];
		int num = 0;
		array[num] = (byte)this.ᜃ.Length;
		num += 2;
		spr\u2562.ᜀ(array, ref num, this.ᜃ);
		num += 2;
		spr\u2562.ᜀ(array, ref num, BitConverter.GetBytes(-858997829));
		array[num] = (byte)this.ᜃ.Length;
		num += 2;
		spr\u2562.ᜀ(array, ref num, this.ᜄ);
		A_0.Write(array, 0, array.Length);
	}

	// Token: 0x0400263A RID: 9786
	private new const int ᜀ = 0;

	// Token: 0x0400263B RID: 9787
	private new const int ᜁ = -858997829;

	// Token: 0x0400263C RID: 9788
	private new const int ᜂ = 4;

	// Token: 0x0400263D RID: 9789
	private new byte[] ᜃ;

	// Token: 0x0400263E RID: 9790
	private new byte[] ᜄ;

	// Token: 0x0400263F RID: 9791
	private string ᜅ;
}
