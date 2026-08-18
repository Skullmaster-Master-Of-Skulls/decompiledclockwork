using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000509 RID: 1289
	public class SerpentEngine : IBlockCipher
	{
		// Token: 0x06002BF5 RID: 11253 RVA: 0x00109AEC File Offset: 0x00108AEC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to Serpent init - " + parameters.GetType().ToString());
			}
			this.encrypting = forEncryption;
			this.wKey = this.MakeWorkingKey(((KeyParameter)parameters).GetKey());
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x00109B3A File Offset: 0x00108B3A
		public string AlgorithmName
		{
			get
			{
				return "Serpent";
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x00109B41 File Offset: 0x00108B41
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00109B44 File Offset: 0x00108B44
		public int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x00109B48 File Offset: 0x00108B48
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.wKey == null)
			{
				throw new InvalidOperationException("Serpent not initialised");
			}
			if (inOff + 16 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 16 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (this.encrypting)
			{
				this.EncryptBlock(input, inOff, output, outOff);
			}
			else
			{
				this.DecryptBlock(input, inOff, output, outOff);
			}
			return 16;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x00109BB3 File Offset: 0x00108BB3
		public void Reset()
		{
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x00109BB8 File Offset: 0x00108BB8
		private int[] MakeWorkingKey(byte[] key)
		{
			int[] array = new int[16];
			int num = 0;
			int i;
			for (i = key.Length - 4; i > 0; i -= 4)
			{
				array[num++] = this.BytesToWord(key, i);
			}
			if (i == 0)
			{
				array[num++] = this.BytesToWord(key, 0);
				if (num < 8)
				{
					array[num] = 1;
				}
				int num2 = (SerpentEngine.ROUNDS + 1) * 4;
				int[] array2 = new int[num2];
				for (int j = 8; j < 16; j++)
				{
					array[j] = this.RotateLeft(array[j - 8] ^ array[j - 5] ^ array[j - 3] ^ array[j - 1] ^ SerpentEngine.PHI ^ j - 8, 11);
				}
				Array.Copy(array, 8, array2, 0, 8);
				for (int k = 8; k < num2; k++)
				{
					array2[k] = this.RotateLeft(array2[k - 8] ^ array2[k - 5] ^ array2[k - 3] ^ array2[k - 1] ^ SerpentEngine.PHI ^ k, 11);
				}
				this.Sb3(array2[0], array2[1], array2[2], array2[3]);
				array2[0] = this.X0;
				array2[1] = this.X1;
				array2[2] = this.X2;
				array2[3] = this.X3;
				this.Sb2(array2[4], array2[5], array2[6], array2[7]);
				array2[4] = this.X0;
				array2[5] = this.X1;
				array2[6] = this.X2;
				array2[7] = this.X3;
				this.Sb1(array2[8], array2[9], array2[10], array2[11]);
				array2[8] = this.X0;
				array2[9] = this.X1;
				array2[10] = this.X2;
				array2[11] = this.X3;
				this.Sb0(array2[12], array2[13], array2[14], array2[15]);
				array2[12] = this.X0;
				array2[13] = this.X1;
				array2[14] = this.X2;
				array2[15] = this.X3;
				this.Sb7(array2[16], array2[17], array2[18], array2[19]);
				array2[16] = this.X0;
				array2[17] = this.X1;
				array2[18] = this.X2;
				array2[19] = this.X3;
				this.Sb6(array2[20], array2[21], array2[22], array2[23]);
				array2[20] = this.X0;
				array2[21] = this.X1;
				array2[22] = this.X2;
				array2[23] = this.X3;
				this.Sb5(array2[24], array2[25], array2[26], array2[27]);
				array2[24] = this.X0;
				array2[25] = this.X1;
				array2[26] = this.X2;
				array2[27] = this.X3;
				this.Sb4(array2[28], array2[29], array2[30], array2[31]);
				array2[28] = this.X0;
				array2[29] = this.X1;
				array2[30] = this.X2;
				array2[31] = this.X3;
				this.Sb3(array2[32], array2[33], array2[34], array2[35]);
				array2[32] = this.X0;
				array2[33] = this.X1;
				array2[34] = this.X2;
				array2[35] = this.X3;
				this.Sb2(array2[36], array2[37], array2[38], array2[39]);
				array2[36] = this.X0;
				array2[37] = this.X1;
				array2[38] = this.X2;
				array2[39] = this.X3;
				this.Sb1(array2[40], array2[41], array2[42], array2[43]);
				array2[40] = this.X0;
				array2[41] = this.X1;
				array2[42] = this.X2;
				array2[43] = this.X3;
				this.Sb0(array2[44], array2[45], array2[46], array2[47]);
				array2[44] = this.X0;
				array2[45] = this.X1;
				array2[46] = this.X2;
				array2[47] = this.X3;
				this.Sb7(array2[48], array2[49], array2[50], array2[51]);
				array2[48] = this.X0;
				array2[49] = this.X1;
				array2[50] = this.X2;
				array2[51] = this.X3;
				this.Sb6(array2[52], array2[53], array2[54], array2[55]);
				array2[52] = this.X0;
				array2[53] = this.X1;
				array2[54] = this.X2;
				array2[55] = this.X3;
				this.Sb5(array2[56], array2[57], array2[58], array2[59]);
				array2[56] = this.X0;
				array2[57] = this.X1;
				array2[58] = this.X2;
				array2[59] = this.X3;
				this.Sb4(array2[60], array2[61], array2[62], array2[63]);
				array2[60] = this.X0;
				array2[61] = this.X1;
				array2[62] = this.X2;
				array2[63] = this.X3;
				this.Sb3(array2[64], array2[65], array2[66], array2[67]);
				array2[64] = this.X0;
				array2[65] = this.X1;
				array2[66] = this.X2;
				array2[67] = this.X3;
				this.Sb2(array2[68], array2[69], array2[70], array2[71]);
				array2[68] = this.X0;
				array2[69] = this.X1;
				array2[70] = this.X2;
				array2[71] = this.X3;
				this.Sb1(array2[72], array2[73], array2[74], array2[75]);
				array2[72] = this.X0;
				array2[73] = this.X1;
				array2[74] = this.X2;
				array2[75] = this.X3;
				this.Sb0(array2[76], array2[77], array2[78], array2[79]);
				array2[76] = this.X0;
				array2[77] = this.X1;
				array2[78] = this.X2;
				array2[79] = this.X3;
				this.Sb7(array2[80], array2[81], array2[82], array2[83]);
				array2[80] = this.X0;
				array2[81] = this.X1;
				array2[82] = this.X2;
				array2[83] = this.X3;
				this.Sb6(array2[84], array2[85], array2[86], array2[87]);
				array2[84] = this.X0;
				array2[85] = this.X1;
				array2[86] = this.X2;
				array2[87] = this.X3;
				this.Sb5(array2[88], array2[89], array2[90], array2[91]);
				array2[88] = this.X0;
				array2[89] = this.X1;
				array2[90] = this.X2;
				array2[91] = this.X3;
				this.Sb4(array2[92], array2[93], array2[94], array2[95]);
				array2[92] = this.X0;
				array2[93] = this.X1;
				array2[94] = this.X2;
				array2[95] = this.X3;
				this.Sb3(array2[96], array2[97], array2[98], array2[99]);
				array2[96] = this.X0;
				array2[97] = this.X1;
				array2[98] = this.X2;
				array2[99] = this.X3;
				this.Sb2(array2[100], array2[101], array2[102], array2[103]);
				array2[100] = this.X0;
				array2[101] = this.X1;
				array2[102] = this.X2;
				array2[103] = this.X3;
				this.Sb1(array2[104], array2[105], array2[106], array2[107]);
				array2[104] = this.X0;
				array2[105] = this.X1;
				array2[106] = this.X2;
				array2[107] = this.X3;
				this.Sb0(array2[108], array2[109], array2[110], array2[111]);
				array2[108] = this.X0;
				array2[109] = this.X1;
				array2[110] = this.X2;
				array2[111] = this.X3;
				this.Sb7(array2[112], array2[113], array2[114], array2[115]);
				array2[112] = this.X0;
				array2[113] = this.X1;
				array2[114] = this.X2;
				array2[115] = this.X3;
				this.Sb6(array2[116], array2[117], array2[118], array2[119]);
				array2[116] = this.X0;
				array2[117] = this.X1;
				array2[118] = this.X2;
				array2[119] = this.X3;
				this.Sb5(array2[120], array2[121], array2[122], array2[123]);
				array2[120] = this.X0;
				array2[121] = this.X1;
				array2[122] = this.X2;
				array2[123] = this.X3;
				this.Sb4(array2[124], array2[125], array2[126], array2[127]);
				array2[124] = this.X0;
				array2[125] = this.X1;
				array2[126] = this.X2;
				array2[127] = this.X3;
				this.Sb3(array2[128], array2[129], array2[130], array2[131]);
				array2[128] = this.X0;
				array2[129] = this.X1;
				array2[130] = this.X2;
				array2[131] = this.X3;
				return array2;
			}
			throw new ArgumentException("key must be a multiple of 4 bytes");
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x0010A5CA File Offset: 0x001095CA
		private int RotateLeft(int x, int bits)
		{
			return x << bits | (int)((uint)x >> 32 - bits);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x0010A5DC File Offset: 0x001095DC
		private int RotateRight(int x, int bits)
		{
			return (int)((uint)x >> bits | (uint)((uint)x << 32 - bits));
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x0010A5EE File Offset: 0x001095EE
		private int BytesToWord(byte[] src, int srcOff)
		{
			return (int)(src[srcOff] & byte.MaxValue) << 24 | (int)(src[srcOff + 1] & byte.MaxValue) << 16 | (int)(src[srcOff + 2] & byte.MaxValue) << 8 | (int)(src[srcOff + 3] & byte.MaxValue);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x0010A625 File Offset: 0x00109625
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			dst[dstOff + 3] = (byte)word;
			dst[dstOff + 2] = (byte)((uint)word >> 8);
			dst[dstOff + 1] = (byte)((uint)word >> 16);
			dst[dstOff] = (byte)((uint)word >> 24);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0010A64C File Offset: 0x0010964C
		private void EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			this.X3 = this.BytesToWord(input, inOff);
			this.X2 = this.BytesToWord(input, inOff + 4);
			this.X1 = this.BytesToWord(input, inOff + 8);
			this.X0 = this.BytesToWord(input, inOff + 12);
			this.Sb0(this.wKey[0] ^ this.X0, this.wKey[1] ^ this.X1, this.wKey[2] ^ this.X2, this.wKey[3] ^ this.X3);
			this.LT();
			this.Sb1(this.wKey[4] ^ this.X0, this.wKey[5] ^ this.X1, this.wKey[6] ^ this.X2, this.wKey[7] ^ this.X3);
			this.LT();
			this.Sb2(this.wKey[8] ^ this.X0, this.wKey[9] ^ this.X1, this.wKey[10] ^ this.X2, this.wKey[11] ^ this.X3);
			this.LT();
			this.Sb3(this.wKey[12] ^ this.X0, this.wKey[13] ^ this.X1, this.wKey[14] ^ this.X2, this.wKey[15] ^ this.X3);
			this.LT();
			this.Sb4(this.wKey[16] ^ this.X0, this.wKey[17] ^ this.X1, this.wKey[18] ^ this.X2, this.wKey[19] ^ this.X3);
			this.LT();
			this.Sb5(this.wKey[20] ^ this.X0, this.wKey[21] ^ this.X1, this.wKey[22] ^ this.X2, this.wKey[23] ^ this.X3);
			this.LT();
			this.Sb6(this.wKey[24] ^ this.X0, this.wKey[25] ^ this.X1, this.wKey[26] ^ this.X2, this.wKey[27] ^ this.X3);
			this.LT();
			this.Sb7(this.wKey[28] ^ this.X0, this.wKey[29] ^ this.X1, this.wKey[30] ^ this.X2, this.wKey[31] ^ this.X3);
			this.LT();
			this.Sb0(this.wKey[32] ^ this.X0, this.wKey[33] ^ this.X1, this.wKey[34] ^ this.X2, this.wKey[35] ^ this.X3);
			this.LT();
			this.Sb1(this.wKey[36] ^ this.X0, this.wKey[37] ^ this.X1, this.wKey[38] ^ this.X2, this.wKey[39] ^ this.X3);
			this.LT();
			this.Sb2(this.wKey[40] ^ this.X0, this.wKey[41] ^ this.X1, this.wKey[42] ^ this.X2, this.wKey[43] ^ this.X3);
			this.LT();
			this.Sb3(this.wKey[44] ^ this.X0, this.wKey[45] ^ this.X1, this.wKey[46] ^ this.X2, this.wKey[47] ^ this.X3);
			this.LT();
			this.Sb4(this.wKey[48] ^ this.X0, this.wKey[49] ^ this.X1, this.wKey[50] ^ this.X2, this.wKey[51] ^ this.X3);
			this.LT();
			this.Sb5(this.wKey[52] ^ this.X0, this.wKey[53] ^ this.X1, this.wKey[54] ^ this.X2, this.wKey[55] ^ this.X3);
			this.LT();
			this.Sb6(this.wKey[56] ^ this.X0, this.wKey[57] ^ this.X1, this.wKey[58] ^ this.X2, this.wKey[59] ^ this.X3);
			this.LT();
			this.Sb7(this.wKey[60] ^ this.X0, this.wKey[61] ^ this.X1, this.wKey[62] ^ this.X2, this.wKey[63] ^ this.X3);
			this.LT();
			this.Sb0(this.wKey[64] ^ this.X0, this.wKey[65] ^ this.X1, this.wKey[66] ^ this.X2, this.wKey[67] ^ this.X3);
			this.LT();
			this.Sb1(this.wKey[68] ^ this.X0, this.wKey[69] ^ this.X1, this.wKey[70] ^ this.X2, this.wKey[71] ^ this.X3);
			this.LT();
			this.Sb2(this.wKey[72] ^ this.X0, this.wKey[73] ^ this.X1, this.wKey[74] ^ this.X2, this.wKey[75] ^ this.X3);
			this.LT();
			this.Sb3(this.wKey[76] ^ this.X0, this.wKey[77] ^ this.X1, this.wKey[78] ^ this.X2, this.wKey[79] ^ this.X3);
			this.LT();
			this.Sb4(this.wKey[80] ^ this.X0, this.wKey[81] ^ this.X1, this.wKey[82] ^ this.X2, this.wKey[83] ^ this.X3);
			this.LT();
			this.Sb5(this.wKey[84] ^ this.X0, this.wKey[85] ^ this.X1, this.wKey[86] ^ this.X2, this.wKey[87] ^ this.X3);
			this.LT();
			this.Sb6(this.wKey[88] ^ this.X0, this.wKey[89] ^ this.X1, this.wKey[90] ^ this.X2, this.wKey[91] ^ this.X3);
			this.LT();
			this.Sb7(this.wKey[92] ^ this.X0, this.wKey[93] ^ this.X1, this.wKey[94] ^ this.X2, this.wKey[95] ^ this.X3);
			this.LT();
			this.Sb0(this.wKey[96] ^ this.X0, this.wKey[97] ^ this.X1, this.wKey[98] ^ this.X2, this.wKey[99] ^ this.X3);
			this.LT();
			this.Sb1(this.wKey[100] ^ this.X0, this.wKey[101] ^ this.X1, this.wKey[102] ^ this.X2, this.wKey[103] ^ this.X3);
			this.LT();
			this.Sb2(this.wKey[104] ^ this.X0, this.wKey[105] ^ this.X1, this.wKey[106] ^ this.X2, this.wKey[107] ^ this.X3);
			this.LT();
			this.Sb3(this.wKey[108] ^ this.X0, this.wKey[109] ^ this.X1, this.wKey[110] ^ this.X2, this.wKey[111] ^ this.X3);
			this.LT();
			this.Sb4(this.wKey[112] ^ this.X0, this.wKey[113] ^ this.X1, this.wKey[114] ^ this.X2, this.wKey[115] ^ this.X3);
			this.LT();
			this.Sb5(this.wKey[116] ^ this.X0, this.wKey[117] ^ this.X1, this.wKey[118] ^ this.X2, this.wKey[119] ^ this.X3);
			this.LT();
			this.Sb6(this.wKey[120] ^ this.X0, this.wKey[121] ^ this.X1, this.wKey[122] ^ this.X2, this.wKey[123] ^ this.X3);
			this.LT();
			this.Sb7(this.wKey[124] ^ this.X0, this.wKey[125] ^ this.X1, this.wKey[126] ^ this.X2, this.wKey[127] ^ this.X3);
			this.WordToBytes(this.wKey[131] ^ this.X3, outBytes, outOff);
			this.WordToBytes(this.wKey[130] ^ this.X2, outBytes, outOff + 4);
			this.WordToBytes(this.wKey[129] ^ this.X1, outBytes, outOff + 8);
			this.WordToBytes(this.wKey[128] ^ this.X0, outBytes, outOff + 12);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0010B080 File Offset: 0x0010A080
		private void DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			this.X3 = (this.wKey[131] ^ this.BytesToWord(input, inOff));
			this.X2 = (this.wKey[130] ^ this.BytesToWord(input, inOff + 4));
			this.X1 = (this.wKey[129] ^ this.BytesToWord(input, inOff + 8));
			this.X0 = (this.wKey[128] ^ this.BytesToWord(input, inOff + 12));
			this.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[124];
			this.X1 ^= this.wKey[125];
			this.X2 ^= this.wKey[126];
			this.X3 ^= this.wKey[127];
			this.InverseLT();
			this.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[120];
			this.X1 ^= this.wKey[121];
			this.X2 ^= this.wKey[122];
			this.X3 ^= this.wKey[123];
			this.InverseLT();
			this.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[116];
			this.X1 ^= this.wKey[117];
			this.X2 ^= this.wKey[118];
			this.X3 ^= this.wKey[119];
			this.InverseLT();
			this.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[112];
			this.X1 ^= this.wKey[113];
			this.X2 ^= this.wKey[114];
			this.X3 ^= this.wKey[115];
			this.InverseLT();
			this.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[108];
			this.X1 ^= this.wKey[109];
			this.X2 ^= this.wKey[110];
			this.X3 ^= this.wKey[111];
			this.InverseLT();
			this.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[104];
			this.X1 ^= this.wKey[105];
			this.X2 ^= this.wKey[106];
			this.X3 ^= this.wKey[107];
			this.InverseLT();
			this.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[100];
			this.X1 ^= this.wKey[101];
			this.X2 ^= this.wKey[102];
			this.X3 ^= this.wKey[103];
			this.InverseLT();
			this.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[96];
			this.X1 ^= this.wKey[97];
			this.X2 ^= this.wKey[98];
			this.X3 ^= this.wKey[99];
			this.InverseLT();
			this.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[92];
			this.X1 ^= this.wKey[93];
			this.X2 ^= this.wKey[94];
			this.X3 ^= this.wKey[95];
			this.InverseLT();
			this.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[88];
			this.X1 ^= this.wKey[89];
			this.X2 ^= this.wKey[90];
			this.X3 ^= this.wKey[91];
			this.InverseLT();
			this.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[84];
			this.X1 ^= this.wKey[85];
			this.X2 ^= this.wKey[86];
			this.X3 ^= this.wKey[87];
			this.InverseLT();
			this.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[80];
			this.X1 ^= this.wKey[81];
			this.X2 ^= this.wKey[82];
			this.X3 ^= this.wKey[83];
			this.InverseLT();
			this.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[76];
			this.X1 ^= this.wKey[77];
			this.X2 ^= this.wKey[78];
			this.X3 ^= this.wKey[79];
			this.InverseLT();
			this.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[72];
			this.X1 ^= this.wKey[73];
			this.X2 ^= this.wKey[74];
			this.X3 ^= this.wKey[75];
			this.InverseLT();
			this.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[68];
			this.X1 ^= this.wKey[69];
			this.X2 ^= this.wKey[70];
			this.X3 ^= this.wKey[71];
			this.InverseLT();
			this.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[64];
			this.X1 ^= this.wKey[65];
			this.X2 ^= this.wKey[66];
			this.X3 ^= this.wKey[67];
			this.InverseLT();
			this.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[60];
			this.X1 ^= this.wKey[61];
			this.X2 ^= this.wKey[62];
			this.X3 ^= this.wKey[63];
			this.InverseLT();
			this.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[56];
			this.X1 ^= this.wKey[57];
			this.X2 ^= this.wKey[58];
			this.X3 ^= this.wKey[59];
			this.InverseLT();
			this.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[52];
			this.X1 ^= this.wKey[53];
			this.X2 ^= this.wKey[54];
			this.X3 ^= this.wKey[55];
			this.InverseLT();
			this.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[48];
			this.X1 ^= this.wKey[49];
			this.X2 ^= this.wKey[50];
			this.X3 ^= this.wKey[51];
			this.InverseLT();
			this.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[44];
			this.X1 ^= this.wKey[45];
			this.X2 ^= this.wKey[46];
			this.X3 ^= this.wKey[47];
			this.InverseLT();
			this.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[40];
			this.X1 ^= this.wKey[41];
			this.X2 ^= this.wKey[42];
			this.X3 ^= this.wKey[43];
			this.InverseLT();
			this.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[36];
			this.X1 ^= this.wKey[37];
			this.X2 ^= this.wKey[38];
			this.X3 ^= this.wKey[39];
			this.InverseLT();
			this.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[32];
			this.X1 ^= this.wKey[33];
			this.X2 ^= this.wKey[34];
			this.X3 ^= this.wKey[35];
			this.InverseLT();
			this.Ib7(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[28];
			this.X1 ^= this.wKey[29];
			this.X2 ^= this.wKey[30];
			this.X3 ^= this.wKey[31];
			this.InverseLT();
			this.Ib6(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[24];
			this.X1 ^= this.wKey[25];
			this.X2 ^= this.wKey[26];
			this.X3 ^= this.wKey[27];
			this.InverseLT();
			this.Ib5(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[20];
			this.X1 ^= this.wKey[21];
			this.X2 ^= this.wKey[22];
			this.X3 ^= this.wKey[23];
			this.InverseLT();
			this.Ib4(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[16];
			this.X1 ^= this.wKey[17];
			this.X2 ^= this.wKey[18];
			this.X3 ^= this.wKey[19];
			this.InverseLT();
			this.Ib3(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[12];
			this.X1 ^= this.wKey[13];
			this.X2 ^= this.wKey[14];
			this.X3 ^= this.wKey[15];
			this.InverseLT();
			this.Ib2(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[8];
			this.X1 ^= this.wKey[9];
			this.X2 ^= this.wKey[10];
			this.X3 ^= this.wKey[11];
			this.InverseLT();
			this.Ib1(this.X0, this.X1, this.X2, this.X3);
			this.X0 ^= this.wKey[4];
			this.X1 ^= this.wKey[5];
			this.X2 ^= this.wKey[6];
			this.X3 ^= this.wKey[7];
			this.InverseLT();
			this.Ib0(this.X0, this.X1, this.X2, this.X3);
			this.WordToBytes(this.X3 ^ this.wKey[3], outBytes, outOff);
			this.WordToBytes(this.X2 ^ this.wKey[2], outBytes, outOff + 4);
			this.WordToBytes(this.X1 ^ this.wKey[1], outBytes, outOff + 8);
			this.WordToBytes(this.X0 ^ this.wKey[0], outBytes, outOff + 12);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0010C084 File Offset: 0x0010B084
		private void Sb0(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = c ^ num;
			int num3 = b ^ num2;
			this.X3 = ((a & d) ^ num3);
			int num4 = a ^ (b & num);
			this.X2 = (num3 ^ (c | num4));
			int num5 = this.X3 & (num2 ^ num4);
			this.X1 = (~num2 ^ num5);
			this.X0 = (num5 ^ ~num4);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0010C0E0 File Offset: 0x0010B0E0
		private void Ib0(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = d ^ (num | num2);
			int num4 = c ^ num3;
			this.X2 = (num2 ^ num4);
			int num5 = num ^ (d & num2);
			this.X1 = (num3 ^ (this.X2 & num5));
			this.X3 = ((a & num3) ^ (num4 | this.X1));
			this.X0 = (this.X3 ^ (num4 ^ num5));
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x0010C144 File Offset: 0x0010B144
		private void Sb1(int a, int b, int c, int d)
		{
			int num = b ^ ~a;
			int num2 = c ^ (a | num);
			this.X2 = (d ^ num2);
			int num3 = b ^ (d | num);
			int num4 = num ^ this.X2;
			this.X3 = (num4 ^ (num2 & num3));
			int num5 = num2 ^ num3;
			this.X1 = (this.X3 ^ num5);
			this.X0 = (num2 ^ (num4 & num5));
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0010C1A4 File Offset: 0x0010B1A4
		private void Ib1(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = a ^ (b & num);
			int num3 = num ^ num2;
			this.X3 = (c ^ num3);
			int num4 = b ^ (num & num2);
			int num5 = this.X3 | num4;
			this.X1 = (num2 ^ num5);
			int num6 = ~this.X1;
			int num7 = this.X3 ^ num4;
			this.X0 = (num6 ^ num7);
			this.X2 = (num3 ^ (num6 | num7));
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0010C210 File Offset: 0x0010B210
		private void Sb2(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = b ^ d;
			int num3 = c & num;
			this.X0 = (num2 ^ num3);
			int num4 = c ^ num;
			int num5 = c ^ this.X0;
			int num6 = b & num5;
			this.X3 = (num4 ^ num6);
			this.X2 = (a ^ ((d | num6) & (this.X0 | num4)));
			this.X1 = (num2 ^ this.X3 ^ (this.X2 ^ (d | num)));
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x0010C280 File Offset: 0x0010B280
		private void Ib2(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = ~num;
			int num3 = a ^ c;
			int num4 = c ^ num;
			int num5 = b & num4;
			this.X0 = (num3 ^ num5);
			int num6 = a | num2;
			int num7 = d ^ num6;
			int num8 = num3 | num7;
			this.X3 = (num ^ num8);
			int num9 = ~num4;
			int num10 = this.X0 | this.X3;
			this.X1 = (num9 ^ num10);
			this.X2 = ((d & num9) ^ (num3 ^ num10));
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x0010C2F8 File Offset: 0x0010B2F8
		private void Sb3(int a, int b, int c, int d)
		{
			int num = a ^ b;
			int num2 = a & c;
			int num3 = a | d;
			int num4 = c ^ d;
			int num5 = num & num3;
			int num6 = num2 | num5;
			this.X2 = (num4 ^ num6);
			int num7 = b ^ num3;
			int num8 = num6 ^ num7;
			int num9 = num4 & num8;
			this.X0 = (num ^ num9);
			int num10 = this.X2 & this.X0;
			this.X1 = (num8 ^ num10);
			this.X3 = ((b | d) ^ (num4 ^ num10));
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x0010C374 File Offset: 0x0010B374
		private void Ib3(int a, int b, int c, int d)
		{
			int num = a | b;
			int num2 = b ^ c;
			int num3 = b & num2;
			int num4 = a ^ num3;
			int num5 = c ^ num4;
			int num6 = d | num4;
			this.X0 = (num2 ^ num6);
			int num7 = num2 | num6;
			int num8 = d ^ num7;
			this.X2 = (num5 ^ num8);
			int num9 = num ^ num8;
			int num10 = this.X0 & num9;
			this.X3 = (num4 ^ num10);
			this.X1 = (this.X3 ^ (this.X0 ^ num9));
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x0010C3F0 File Offset: 0x0010B3F0
		private void Sb4(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = d & num;
			int num3 = c ^ num2;
			int num4 = b | num3;
			this.X3 = (num ^ num4);
			int num5 = ~b;
			int num6 = num | num5;
			this.X0 = (num3 ^ num6);
			int num7 = a & this.X0;
			int num8 = num ^ num5;
			int num9 = num4 & num8;
			this.X2 = (num7 ^ num9);
			this.X1 = (a ^ num3 ^ (num8 & this.X2));
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x0010C460 File Offset: 0x0010B460
		private void Ib4(int a, int b, int c, int d)
		{
			int num = c | d;
			int num2 = a & num;
			int num3 = b ^ num2;
			int num4 = a & num3;
			int num5 = c ^ num4;
			this.X1 = (d ^ num5);
			int num6 = ~a;
			int num7 = num5 & this.X1;
			this.X3 = (num3 ^ num7);
			int num8 = this.X1 | num6;
			int num9 = d ^ num8;
			this.X0 = (this.X3 ^ num9);
			this.X2 = ((num3 & num9) ^ (this.X1 ^ num6));
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x0010C4DC File Offset: 0x0010B4DC
		private void Sb5(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = a ^ d;
			int num4 = c ^ num;
			int num5 = num2 | num3;
			this.X0 = (num4 ^ num5);
			int num6 = d & this.X0;
			int num7 = num2 ^ this.X0;
			this.X1 = (num6 ^ num7);
			int num8 = num | this.X0;
			int num9 = num2 | num6;
			int num10 = num3 ^ num8;
			this.X2 = (num9 ^ num10);
			this.X3 = (b ^ num6 ^ (this.X1 & num10));
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0010C560 File Offset: 0x0010B560
		private void Ib5(int a, int b, int c, int d)
		{
			int num = ~c;
			int num2 = b & num;
			int num3 = d ^ num2;
			int num4 = a & num3;
			int num5 = b ^ num;
			this.X3 = (num4 ^ num5);
			int num6 = b | this.X3;
			int num7 = a & num6;
			this.X1 = (num3 ^ num7);
			int num8 = a | d;
			int num9 = num ^ num6;
			this.X0 = (num8 ^ num9);
			this.X2 = ((b & num8) ^ (num4 | (a ^ c)));
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0010C5D0 File Offset: 0x0010B5D0
		private void Sb6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ d;
			int num3 = b ^ num2;
			int num4 = num | num2;
			int num5 = c ^ num4;
			this.X1 = (b ^ num5);
			int num6 = num2 | this.X1;
			int num7 = d ^ num6;
			int num8 = num5 & num7;
			this.X2 = (num3 ^ num8);
			int num9 = num5 ^ num7;
			this.X0 = (this.X2 ^ num9);
			this.X3 = (~num5 ^ (num3 & num9));
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x0010C644 File Offset: 0x0010B644
		private void Ib6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = c ^ num2;
			int num4 = c | num;
			int num5 = d ^ num4;
			this.X1 = (num3 ^ num5);
			int num6 = num3 & num5;
			int num7 = num2 ^ num6;
			int num8 = b | num7;
			this.X3 = (num5 ^ num8);
			int num9 = b | this.X3;
			this.X0 = (num7 ^ num9);
			this.X2 = ((d & num) ^ (num3 ^ num9));
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0010C6B4 File Offset: 0x0010B6B4
		private void Sb7(int a, int b, int c, int d)
		{
			int num = b ^ c;
			int num2 = c & num;
			int num3 = d ^ num2;
			int num4 = a ^ num3;
			int num5 = d | num;
			int num6 = num4 & num5;
			this.X1 = (b ^ num6);
			int num7 = num3 | this.X1;
			int num8 = a & num4;
			this.X3 = (num ^ num8);
			int num9 = num4 ^ num7;
			int num10 = this.X3 & num9;
			this.X2 = (num3 ^ num10);
			this.X0 = (~num9 ^ (this.X3 & this.X2));
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0010C734 File Offset: 0x0010B734
		private void Ib7(int a, int b, int c, int d)
		{
			int num = c | (a & b);
			int num2 = d & (a | b);
			this.X3 = (num ^ num2);
			int num3 = ~d;
			int num4 = b ^ num2;
			int num5 = num4 | (this.X3 ^ num3);
			this.X1 = (a ^ num5);
			this.X0 = (c ^ num4 ^ (d | this.X1));
			this.X2 = (num ^ this.X1 ^ (this.X0 ^ (a & this.X3)));
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x0010C7A8 File Offset: 0x0010B7A8
		private void LT()
		{
			int num = this.RotateLeft(this.X0, 13);
			int num2 = this.RotateLeft(this.X2, 3);
			int x = this.X1 ^ num ^ num2;
			int x2 = this.X3 ^ num2 ^ num << 3;
			this.X1 = this.RotateLeft(x, 1);
			this.X3 = this.RotateLeft(x2, 7);
			this.X0 = this.RotateLeft(num ^ this.X1 ^ this.X3, 5);
			this.X2 = this.RotateLeft(num2 ^ this.X3 ^ this.X1 << 7, 22);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x0010C844 File Offset: 0x0010B844
		private void InverseLT()
		{
			int num = this.RotateRight(this.X2, 22) ^ this.X3 ^ this.X1 << 7;
			int num2 = this.RotateRight(this.X0, 5) ^ this.X1 ^ this.X3;
			int num3 = this.RotateRight(this.X3, 7);
			int num4 = this.RotateRight(this.X1, 1);
			this.X3 = (num3 ^ num ^ num2 << 3);
			this.X1 = (num4 ^ num2 ^ num);
			this.X2 = this.RotateRight(num, 3);
			this.X0 = this.RotateRight(num2, 13);
		}

		// Token: 0x04001E59 RID: 7769
		private const int BLOCK_SIZE = 16;

		// Token: 0x04001E5A RID: 7770
		private static readonly int ROUNDS = 32;

		// Token: 0x04001E5B RID: 7771
		private static readonly int PHI = -1640531527;

		// Token: 0x04001E5C RID: 7772
		private bool encrypting;

		// Token: 0x04001E5D RID: 7773
		private int[] wKey;

		// Token: 0x04001E5E RID: 7774
		private int X0;

		// Token: 0x04001E5F RID: 7775
		private int X1;

		// Token: 0x04001E60 RID: 7776
		private int X2;

		// Token: 0x04001E61 RID: 7777
		private int X3;
	}
}
