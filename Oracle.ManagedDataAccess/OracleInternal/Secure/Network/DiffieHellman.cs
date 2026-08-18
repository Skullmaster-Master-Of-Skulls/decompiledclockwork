using System;
using \u0002;
using \u0006;

namespace OracleInternal.Secure.Network
{
	// Token: 0x0200034C RID: 844
	internal class DiffieHellman
	{
		// Token: 0x06001DC8 RID: 7624 RVA: 0x00123DA0 File Offset: 0x00121FA0
		private DiffieHellman(int _requested_size)
		{
			this.\u0001(null, null, _requested_size);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00123DD4 File Offset: 0x00121FD4
		public DiffieHellman(byte[] _base_ora, byte[] _modulus_ora, int _requested_size)
		{
			this.\u0001(_base_ora, _modulus_ora, _requested_size);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00123E08 File Offset: 0x00122008
		public DiffieHellman(byte[] _base_ora, byte[] _modulus_ora, ushort _ebits_ora, ushort _mbits_ora)
		{
			if (_base_ora != null && _modulus_ora != null)
			{
				this.\u0006 = _base_ora;
				this.\u0007 = _modulus_ora;
				this.\u000E = _mbits_ora;
				this.\u0008 = _ebits_ora;
				return;
			}
			this.\u0001(_base_ora, _modulus_ora, 40);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00123E6C File Offset: 0x0012206C
		private void \u0001(byte[] \u0002, byte[] \u0003, int \u0004)
		{
			this.\u0002 = \u0002;
			if (\u0002 != null)
			{
				this.\u0003 = \u0002.Length;
			}
			else
			{
				this.\u0003 = 0;
			}
			this.\u0004 = \u0003;
			if (\u0003 != null)
			{
				this.\u0005 = \u0003.Length;
			}
			else
			{
				this.\u0005 = 0;
			}
			this.\u0001(\u0004);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00123EB8 File Offset: 0x001220B8
		public byte[] getPublicKey()
		{
			this.\u0001();
			return this.\u0011;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00123EC8 File Offset: 0x001220C8
		public byte[] getSessionKey(byte[] pkey_data, int pkey_size)
		{
			this.\u0002(pkey_data, pkey_size);
			return this.\u0015;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00123ED8 File Offset: 0x001220D8
		private void \u0001(byte[] \u0002, int \u0003)
		{
			new \u0002.\u0001().\u0001(\u0002, \u0003);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00123EE8 File Offset: 0x001220E8
		private void \u0001(int \u0002)
		{
			int i = 0;
			while (i < DiffieHellman.\u0016.Length)
			{
				if (\u0002 >= (int)DiffieHellman.\u0016[i] && \u0002 <= (int)DiffieHellman.\u0017[i])
				{
					this.\u0008 = DiffieHellman.\u0018[i];
					this.\u000E = DiffieHellman.\u0019[i];
					this.\u0006 = new byte[(int)((this.\u000E + 7) / 8)];
					this.\u0007 = new byte[(int)((this.\u000E + 7) / 8)];
					if (this.\u0003 * 8 >= (int)this.\u000E && this.\u0005 * 8 >= (int)this.\u000E)
					{
						Array.Copy(this.\u0002, 0, this.\u0006, 0, this.\u0006.Length);
						Array.Copy(this.\u0004, 0, this.\u0007, 0, this.\u0007.Length);
						break;
					}
					Array.Copy(DiffieHellman.\u001E[i], 0, this.\u0006, 0, this.\u0006.Length);
					Array.Copy(DiffieHellman.\u001F[i], 0, this.\u0007, 0, this.\u0007.Length);
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.\u0006 != null)
			{
				byte[] u = this.\u0007;
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0012400C File Offset: 0x0012220C
		private void \u0001()
		{
			ushort[] array = new ushort[257];
			ushort[] array2 = new ushort[257];
			byte[] array3 = new byte[512];
			int num = this.\u0008 + 7 >> 3;
			int num2 = this.\u000E + 7 >> 3;
			this.\u0012 = (int)((ushort)num2);
			this.\u0013 = (int)(this.\u000E / 16 + 1);
			this.\u0011 = new byte[this.\u0012];
			this.\u0001(array3, num);
			byte[] array4 = array3;
			int num3 = 0;
			array4[num3] &= (byte)(255 >> num - (int)(8 * this.\u0008));
			global::\u0006.\u0001.\u0001(array, this.\u0013, this.\u0006, num2);
			global::\u0006.\u0001.\u0001(this.\u0010, this.\u0013, array3, num);
			global::\u0006.\u0001.\u0001(this.\u000F, this.\u0013, this.\u0007, num2);
			global::\u0006.\u0001.\u0001(array2, array, this.\u0010, this.\u000F, this.\u0013);
			global::\u0006.\u0001.\u0001(this.\u0011, this.\u0012, array2, this.\u0013);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00124120 File Offset: 0x00122320
		private void \u0002(byte[] \u0002, int \u0003)
		{
			ushort[] array = new ushort[DiffieHellman.\u0001];
			ushort[] array2 = new ushort[DiffieHellman.\u0001];
			this.\u0014 = this.\u0012;
			this.\u0015 = new byte[this.\u0014];
			global::\u0006.\u0001.\u0001(array, this.\u0013, \u0002, \u0003);
			global::\u0006.\u0001.\u0001(array2, array, this.\u0010, this.\u000F, this.\u0013);
			global::\u0006.\u0001.\u0001(this.\u0015, this.\u0014, array2, this.\u0013);
		}

		// Token: 0x04002017 RID: 8215
		private static readonly int \u0001 = 257;

		// Token: 0x04002018 RID: 8216
		private byte[] \u0002;

		// Token: 0x04002019 RID: 8217
		private int \u0003;

		// Token: 0x0400201A RID: 8218
		private byte[] \u0004;

		// Token: 0x0400201B RID: 8219
		private int \u0005;

		// Token: 0x0400201C RID: 8220
		private byte[] \u0006;

		// Token: 0x0400201D RID: 8221
		private byte[] \u0007;

		// Token: 0x0400201E RID: 8222
		private ushort \u0008;

		// Token: 0x0400201F RID: 8223
		private ushort \u000E;

		// Token: 0x04002020 RID: 8224
		private ushort[] \u000F = new ushort[DiffieHellman.\u0001];

		// Token: 0x04002021 RID: 8225
		private ushort[] \u0010 = new ushort[DiffieHellman.\u0001];

		// Token: 0x04002022 RID: 8226
		private byte[] \u0011;

		// Token: 0x04002023 RID: 8227
		private int \u0012;

		// Token: 0x04002024 RID: 8228
		private int \u0013;

		// Token: 0x04002025 RID: 8229
		private int \u0014;

		// Token: 0x04002026 RID: 8230
		private byte[] \u0015;

		// Token: 0x04002027 RID: 8231
		private static readonly ushort[] \u0016 = new ushort[]
		{
			40,
			41,
			56,
			128,
			256
		};

		// Token: 0x04002028 RID: 8232
		private static readonly ushort[] \u0017 = new ushort[]
		{
			40,
			64,
			56,
			128,
			256
		};

		// Token: 0x04002029 RID: 8233
		private static readonly ushort[] \u0018 = new ushort[]
		{
			80,
			112,
			112,
			512,
			512
		};

		// Token: 0x0400202A RID: 8234
		private static readonly ushort[] \u0019 = new ushort[]
		{
			300,
			512,
			512,
			512,
			512
		};

		// Token: 0x0400202B RID: 8235
		private static readonly byte[] \u001A = new byte[]
		{
			2,
			83,
			179,
			242,
			166,
			141,
			61,
			187,
			106,
			195,
			153,
			9,
			192,
			215,
			4,
			5,
			242,
			91,
			130,
			97,
			107,
			122,
			232,
			220,
			29,
			123,
			3,
			150,
			53,
			226,
			219,
			239,
			67,
			102,
			250,
			208,
			76,
			193
		};

		// Token: 0x0400202C RID: 8236
		private static readonly byte[] \u001B = new byte[]
		{
			12,
			54,
			129,
			183,
			4,
			71,
			3,
			160,
			120,
			96,
			81,
			38,
			140,
			234,
			155,
			188,
			163,
			62,
			124,
			1,
			171,
			54,
			139,
			34,
			117,
			152,
			119,
			102,
			53,
			197,
			128,
			213,
			36,
			210,
			80,
			99,
			184,
			243
		};

		// Token: 0x0400202D RID: 8237
		private static readonly byte[] \u001C = new byte[]
		{
			130,
			152,
			222,
			73,
			222,
			247,
			9,
			229,
			224,
			13,
			176,
			160,
			165,
			156,
			169,
			242,
			61,
			246,
			198,
			167,
			233,
			74,
			68,
			163,
			225,
			135,
			46,
			245,
			76,
			31,
			161,
			122,
			223,
			92,
			242,
			117,
			129,
			237,
			81,
			195,
			38,
			238,
			139,
			225,
			4,
			3,
			30,
			103,
			80,
			83,
			181,
			124,
			75,
			69,
			111,
			21,
			74,
			23,
			86,
			11,
			90,
			21,
			149,
			165
		};

		// Token: 0x0400202E RID: 8238
		private static readonly byte[] \u001D = new byte[]
		{
			220,
			142,
			163,
			27,
			8,
			96,
			105,
			138,
			204,
			246,
			209,
			158,
			135,
			14,
			52,
			252,
			103,
			197,
			89,
			11,
			78,
			166,
			177,
			60,
			213,
			253,
			239,
			21,
			172,
			157,
			95,
			63,
			33,
			76,
			220,
			7,
			204,
			135,
			74,
			179,
			1,
			215,
			127,
			44,
			67,
			51,
			81,
			60,
			222,
			11,
			30,
			206,
			100,
			71,
			118,
			87,
			92,
			81,
			204,
			152,
			179,
			254,
			231,
			239
		};

		// Token: 0x0400202F RID: 8239
		private static readonly byte[][] \u001E = new byte[][]
		{
			DiffieHellman.\u001A,
			DiffieHellman.\u001C,
			DiffieHellman.\u001C,
			DiffieHellman.\u001C,
			DiffieHellman.\u001C
		};

		// Token: 0x04002030 RID: 8240
		private static readonly byte[][] \u001F = new byte[][]
		{
			DiffieHellman.\u001B,
			DiffieHellman.\u001D,
			DiffieHellman.\u001D,
			DiffieHellman.\u001D,
			DiffieHellman.\u001D
		};
	}
}
