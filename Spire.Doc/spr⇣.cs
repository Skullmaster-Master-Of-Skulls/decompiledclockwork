using System;
using System.Security.Cryptography;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x02000203 RID: 515
[CLSCompliant(false)]
internal class spr\u21E3
{
	// Token: 0x06001839 RID: 6201 RVA: 0x001751E4 File Offset: 0x001741E4
	internal spr\u21E3()
	{
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x001751F8 File Offset: 0x001741F8
	internal byte[] ᜀ(byte[] A_0, uint A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			byte[] array2;
			for (;;)
			{
				byte[] array = new byte[4];
				int num = 0;
				int num2 = 15;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_10B;
					case 1:
					{
						string text = text.Trim().Trim(new char[]
						{
							'﻿'
						});
						text = text.ToUpper();
						byte[] bytes = Encoding.Unicode.GetBytes(text);
						array2 = this.ᜀ(A_0, bytes);
						SHA1 sha = new SHA1Managed();
						array2 = sha.ComputeHash(array2);
						byte[] array3 = new byte[24];
						int num3 = 0;
						num2 = 5;
						continue;
					}
					case 2:
						goto IL_1AE;
					case 3:
					{
						int num4;
						if (num4 >= 4)
						{
							num2 = 9;
							continue;
						}
						byte[] array3;
						int num5;
						array3[array2.Length + num4] = (byte)num5;
						num5 >>= 8;
						num4++;
						num2 = 2;
						continue;
					}
					case 4:
						return array2;
					case 5:
						goto IL_1D0;
					case 6:
					{
						int num3;
						if (num3 >= 100000)
						{
							num2 = 4;
							continue;
						}
						byte[] array3;
						array2.CopyTo(array3, 0);
						int num5 = num3;
						int num4 = 0;
						num2 = 8;
						continue;
					}
					case 7:
						goto IL_1D0;
					case 8:
						goto IL_1AE;
					case 9:
					{
						SHA1 sha;
						byte[] array3;
						array2 = sha.ComputeHash(array3);
						int num3;
						num3++;
						num2 = 7;
						continue;
					}
					case 10:
						goto IL_1F6;
					case 11:
					{
						int num6;
						if (num6 >= 4)
						{
							goto IL_206;
						}
						string text;
						text += array[num6].ToString(ClipboardData.b("⽶䭸", a_));
						num6++;
						num2 = 10;
						continue;
					}
					case 12:
					{
						string text = string.Empty;
						int num6 = 0;
						num2 = 14;
						continue;
					}
					case 13:
						if (true)
						{
						}
						if (num < 4)
						{
							array[num] = Convert.ToByte((uint)((ulong)A_1 & (ulong)(255L << (num * 8 & 31))) >> num * 8);
							num++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_206;
						default:
							if (false)
							{
							}
							num2 = 12;
							continue;
						}
						break;
					case 14:
						goto IL_1F6;
					case 15:
						goto IL_10B;
					}
					break;
					IL_10B:
					num2 = 13;
					continue;
					IL_1AE:
					num2 = 3;
					continue;
					IL_1D0:
					num2 = 6;
					continue;
					IL_1F6:
					num2 = 11;
					continue;
					IL_206:
					num2 = 1;
				}
			}
			return array2;
		}
		}
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x0017548C File Offset: 0x0017448C
	private byte[] ᜀ(byte[] A_0, byte[] A_1)
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
		byte[] array = new byte[A_0.Length + A_1.Length];
		Buffer.BlockCopy(A_0, 0, array, 0, A_0.Length);
		Buffer.BlockCopy(A_1, 0, array, A_0.Length, A_1.Length);
		return array;
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x001754F0 File Offset: 0x001744F0
	internal byte[] ᜀ(int A_0)
	{
		int a_ = 0;
		for (;;)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					byte[] array;
					int num2;
					Random random;
					int maxValue;
					switch (num)
					{
					case 0:
						goto IL_D3;
					case 1:
						return array;
					case 3:
						if (num2 >= A_0)
						{
							num = 1;
							continue;
						}
						array[num2] = (byte)random.Next(maxValue);
						num2++;
						num = 4;
						continue;
					case 4:
						goto IL_D3;
					case 5:
						goto IL_4E;
					}
					if (A_0 <= 0)
					{
						num = 5;
						continue;
					}
					array = new byte[A_0];
					random = new Random((int)DateTime.Now.Ticks);
					maxValue = 256;
					num2 = 0;
					num = 0;
					continue;
					IL_D3:
					if (true)
					{
					}
					num = 3;
				}
				IL_4E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_2;
			}
			}
		}
		Block_2:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("੥൧ѩ୫ᩭᡯ", a_));
	}

	// Token: 0x04001B42 RID: 6978
	internal const int ᜀ = 100000;

	// Token: 0x04001B43 RID: 6979
	internal const string ᜁ = "rsaFull";

	// Token: 0x04001B44 RID: 6980
	internal const string ᜂ = "hash";

	// Token: 0x04001B45 RID: 6981
	internal const string ᜃ = "typeAny";

	// Token: 0x04001B46 RID: 6982
	internal const int ᜄ = 4;
}
