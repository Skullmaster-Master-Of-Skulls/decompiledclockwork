using System;
using System.Text;

// Token: 0x020001F6 RID: 502
internal abstract class spr\u19E7 : Decoder
{
	// Token: 0x06001610 RID: 5648 RVA: 0x00164F9C File Offset: 0x00163F9C
	public virtual int ᜀ(byte[] A_0, int A_1, int A_2)
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
		return (A_2 + this.ᜁ) / 4;
	}

	// Token: 0x06001611 RID: 5649
	internal abstract int ᜀ(byte[] A_0, int A_1, int A_2, char[] A_3, int A_4);

	// Token: 0x06001612 RID: 5650 RVA: 0x00164FE4 File Offset: 0x00163FE4
	public virtual int ᜁ(byte[] A_0, int A_1, int A_2, char[] A_3, int A_4)
	{
		int num;
		for (;;)
		{
			IL_5C:
			num = this.ᜁ;
			int num2 = 12;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						if (A_1 >= 0)
						{
							num2 = 8;
							continue;
						}
						return num;
					case 1:
						if (num >= 4)
						{
							num2 = 13;
							continue;
						}
						this.ᜀ[num] = A_0[A_1];
						A_1++;
						A_2--;
						num++;
						num2 = 6;
						continue;
					case 2:
						goto IL_11B;
					case 3:
						return num;
					case 4:
						goto IL_166;
					case 5:
						if (A_1 >= A_2)
						{
							goto IL_178;
						}
						this.ᜀ[this.ᜁ] = A_0[A_1];
						this.ᜁ++;
						A_1++;
						num2 = 4;
						continue;
					case 6:
						goto IL_1B0;
					case 7:
						goto IL_166;
					case 8:
						num2 = 7;
						continue;
					case 9:
						goto IL_11B;
					case 10:
						num2 = 11;
						continue;
					case 11:
						goto IL_1B0;
					case 12:
						if (this.ᜁ > 0)
						{
							num2 = 10;
							continue;
						}
						if (true)
						{
						}
						num = 0;
						num2 = 9;
						continue;
					case 13:
						num = 1;
						this.ᜀ(this.ᜀ, 0, 4, A_3, A_4);
						A_4++;
						num2 = 2;
						continue;
					}
					goto IL_5C;
					IL_11B:
					num = this.ᜀ(A_0, A_1, A_2, A_3, A_4) + num;
					int num3 = (this.ᜁ + A_2) % 4;
					A_2 += A_1;
					A_1 = A_2 - num3;
					this.ᜁ = 0;
					num2 = 0;
					continue;
					IL_166:
					num2 = 5;
					continue;
					IL_1B0:
					num2 = 1;
					continue;
				}
				}
				IL_178:
				num2 = 3;
			}
		}
		return num;
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x001651C4 File Offset: 0x001641C4
	internal static char ᜀ(uint A_0)
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
		byte b = (byte)(55232U + (A_0 >> 10));
		byte b2 = (byte)(56320U | (A_0 & 1023U));
		return (char)((int)b2 << 8 | (int)b);
	}

	// Token: 0x04001A05 RID: 6661
	internal byte[] ᜀ = new byte[4];

	// Token: 0x04001A06 RID: 6662
	internal int ᜁ;
}
