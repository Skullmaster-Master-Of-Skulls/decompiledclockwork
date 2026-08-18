using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x0200008C RID: 140
	public sealed class SerpentCipher : BlockCipher
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x00019000 File Offset: 0x00017200
		public SerpentCipher(byte[] key, CipherMode mode, CipherPadding padding) : base(key, 16, mode, padding)
		{
			int num = key.Length * 8;
			if (num != 128 && num != 192 && num != 256)
			{
				throw new ArgumentException(string.Format("KeySize '{0}' is not valid for this algorithm.", num));
			}
			this._workingKey = this.MakeWorkingKey(key);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001905C File Offset: 0x0001725C
		public override int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputCount != (int)base.BlockSize)
			{
				throw new ArgumentException("inputCount");
			}
			this._x3 = SerpentCipher.BytesToWord(inputBuffer, inputOffset);
			this._x2 = SerpentCipher.BytesToWord(inputBuffer, inputOffset + 4);
			this._x1 = SerpentCipher.BytesToWord(inputBuffer, inputOffset + 8);
			this._x0 = SerpentCipher.BytesToWord(inputBuffer, inputOffset + 12);
			this.Sb0(this._workingKey[0] ^ this._x0, this._workingKey[1] ^ this._x1, this._workingKey[2] ^ this._x2, this._workingKey[3] ^ this._x3);
			this.LT();
			this.Sb1(this._workingKey[4] ^ this._x0, this._workingKey[5] ^ this._x1, this._workingKey[6] ^ this._x2, this._workingKey[7] ^ this._x3);
			this.LT();
			this.Sb2(this._workingKey[8] ^ this._x0, this._workingKey[9] ^ this._x1, this._workingKey[10] ^ this._x2, this._workingKey[11] ^ this._x3);
			this.LT();
			this.Sb3(this._workingKey[12] ^ this._x0, this._workingKey[13] ^ this._x1, this._workingKey[14] ^ this._x2, this._workingKey[15] ^ this._x3);
			this.LT();
			this.Sb4(this._workingKey[16] ^ this._x0, this._workingKey[17] ^ this._x1, this._workingKey[18] ^ this._x2, this._workingKey[19] ^ this._x3);
			this.LT();
			this.Sb5(this._workingKey[20] ^ this._x0, this._workingKey[21] ^ this._x1, this._workingKey[22] ^ this._x2, this._workingKey[23] ^ this._x3);
			this.LT();
			this.Sb6(this._workingKey[24] ^ this._x0, this._workingKey[25] ^ this._x1, this._workingKey[26] ^ this._x2, this._workingKey[27] ^ this._x3);
			this.LT();
			this.Sb7(this._workingKey[28] ^ this._x0, this._workingKey[29] ^ this._x1, this._workingKey[30] ^ this._x2, this._workingKey[31] ^ this._x3);
			this.LT();
			this.Sb0(this._workingKey[32] ^ this._x0, this._workingKey[33] ^ this._x1, this._workingKey[34] ^ this._x2, this._workingKey[35] ^ this._x3);
			this.LT();
			this.Sb1(this._workingKey[36] ^ this._x0, this._workingKey[37] ^ this._x1, this._workingKey[38] ^ this._x2, this._workingKey[39] ^ this._x3);
			this.LT();
			this.Sb2(this._workingKey[40] ^ this._x0, this._workingKey[41] ^ this._x1, this._workingKey[42] ^ this._x2, this._workingKey[43] ^ this._x3);
			this.LT();
			this.Sb3(this._workingKey[44] ^ this._x0, this._workingKey[45] ^ this._x1, this._workingKey[46] ^ this._x2, this._workingKey[47] ^ this._x3);
			this.LT();
			this.Sb4(this._workingKey[48] ^ this._x0, this._workingKey[49] ^ this._x1, this._workingKey[50] ^ this._x2, this._workingKey[51] ^ this._x3);
			this.LT();
			this.Sb5(this._workingKey[52] ^ this._x0, this._workingKey[53] ^ this._x1, this._workingKey[54] ^ this._x2, this._workingKey[55] ^ this._x3);
			this.LT();
			this.Sb6(this._workingKey[56] ^ this._x0, this._workingKey[57] ^ this._x1, this._workingKey[58] ^ this._x2, this._workingKey[59] ^ this._x3);
			this.LT();
			this.Sb7(this._workingKey[60] ^ this._x0, this._workingKey[61] ^ this._x1, this._workingKey[62] ^ this._x2, this._workingKey[63] ^ this._x3);
			this.LT();
			this.Sb0(this._workingKey[64] ^ this._x0, this._workingKey[65] ^ this._x1, this._workingKey[66] ^ this._x2, this._workingKey[67] ^ this._x3);
			this.LT();
			this.Sb1(this._workingKey[68] ^ this._x0, this._workingKey[69] ^ this._x1, this._workingKey[70] ^ this._x2, this._workingKey[71] ^ this._x3);
			this.LT();
			this.Sb2(this._workingKey[72] ^ this._x0, this._workingKey[73] ^ this._x1, this._workingKey[74] ^ this._x2, this._workingKey[75] ^ this._x3);
			this.LT();
			this.Sb3(this._workingKey[76] ^ this._x0, this._workingKey[77] ^ this._x1, this._workingKey[78] ^ this._x2, this._workingKey[79] ^ this._x3);
			this.LT();
			this.Sb4(this._workingKey[80] ^ this._x0, this._workingKey[81] ^ this._x1, this._workingKey[82] ^ this._x2, this._workingKey[83] ^ this._x3);
			this.LT();
			this.Sb5(this._workingKey[84] ^ this._x0, this._workingKey[85] ^ this._x1, this._workingKey[86] ^ this._x2, this._workingKey[87] ^ this._x3);
			this.LT();
			this.Sb6(this._workingKey[88] ^ this._x0, this._workingKey[89] ^ this._x1, this._workingKey[90] ^ this._x2, this._workingKey[91] ^ this._x3);
			this.LT();
			this.Sb7(this._workingKey[92] ^ this._x0, this._workingKey[93] ^ this._x1, this._workingKey[94] ^ this._x2, this._workingKey[95] ^ this._x3);
			this.LT();
			this.Sb0(this._workingKey[96] ^ this._x0, this._workingKey[97] ^ this._x1, this._workingKey[98] ^ this._x2, this._workingKey[99] ^ this._x3);
			this.LT();
			this.Sb1(this._workingKey[100] ^ this._x0, this._workingKey[101] ^ this._x1, this._workingKey[102] ^ this._x2, this._workingKey[103] ^ this._x3);
			this.LT();
			this.Sb2(this._workingKey[104] ^ this._x0, this._workingKey[105] ^ this._x1, this._workingKey[106] ^ this._x2, this._workingKey[107] ^ this._x3);
			this.LT();
			this.Sb3(this._workingKey[108] ^ this._x0, this._workingKey[109] ^ this._x1, this._workingKey[110] ^ this._x2, this._workingKey[111] ^ this._x3);
			this.LT();
			this.Sb4(this._workingKey[112] ^ this._x0, this._workingKey[113] ^ this._x1, this._workingKey[114] ^ this._x2, this._workingKey[115] ^ this._x3);
			this.LT();
			this.Sb5(this._workingKey[116] ^ this._x0, this._workingKey[117] ^ this._x1, this._workingKey[118] ^ this._x2, this._workingKey[119] ^ this._x3);
			this.LT();
			this.Sb6(this._workingKey[120] ^ this._x0, this._workingKey[121] ^ this._x1, this._workingKey[122] ^ this._x2, this._workingKey[123] ^ this._x3);
			this.LT();
			this.Sb7(this._workingKey[124] ^ this._x0, this._workingKey[125] ^ this._x1, this._workingKey[126] ^ this._x2, this._workingKey[127] ^ this._x3);
			SerpentCipher.WordToBytes(this._workingKey[131] ^ this._x3, outputBuffer, outputOffset);
			SerpentCipher.WordToBytes(this._workingKey[130] ^ this._x2, outputBuffer, outputOffset + 4);
			SerpentCipher.WordToBytes(this._workingKey[129] ^ this._x1, outputBuffer, outputOffset + 8);
			SerpentCipher.WordToBytes(this._workingKey[128] ^ this._x0, outputBuffer, outputOffset + 12);
			return (int)base.BlockSize;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00019AA8 File Offset: 0x00017CA8
		public override int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputCount != (int)base.BlockSize)
			{
				throw new ArgumentException("inputCount");
			}
			this._x3 = (this._workingKey[131] ^ SerpentCipher.BytesToWord(inputBuffer, inputOffset));
			this._x2 = (this._workingKey[130] ^ SerpentCipher.BytesToWord(inputBuffer, inputOffset + 4));
			this._x1 = (this._workingKey[129] ^ SerpentCipher.BytesToWord(inputBuffer, inputOffset + 8));
			this._x0 = (this._workingKey[128] ^ SerpentCipher.BytesToWord(inputBuffer, inputOffset + 12));
			this.Ib7(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[124];
			this._x1 ^= this._workingKey[125];
			this._x2 ^= this._workingKey[126];
			this._x3 ^= this._workingKey[127];
			this.InverseLT();
			this.Ib6(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[120];
			this._x1 ^= this._workingKey[121];
			this._x2 ^= this._workingKey[122];
			this._x3 ^= this._workingKey[123];
			this.InverseLT();
			this.Ib5(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[116];
			this._x1 ^= this._workingKey[117];
			this._x2 ^= this._workingKey[118];
			this._x3 ^= this._workingKey[119];
			this.InverseLT();
			this.Ib4(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[112];
			this._x1 ^= this._workingKey[113];
			this._x2 ^= this._workingKey[114];
			this._x3 ^= this._workingKey[115];
			this.InverseLT();
			this.Ib3(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[108];
			this._x1 ^= this._workingKey[109];
			this._x2 ^= this._workingKey[110];
			this._x3 ^= this._workingKey[111];
			this.InverseLT();
			this.Ib2(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[104];
			this._x1 ^= this._workingKey[105];
			this._x2 ^= this._workingKey[106];
			this._x3 ^= this._workingKey[107];
			this.InverseLT();
			this.Ib1(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[100];
			this._x1 ^= this._workingKey[101];
			this._x2 ^= this._workingKey[102];
			this._x3 ^= this._workingKey[103];
			this.InverseLT();
			this.Ib0(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[96];
			this._x1 ^= this._workingKey[97];
			this._x2 ^= this._workingKey[98];
			this._x3 ^= this._workingKey[99];
			this.InverseLT();
			this.Ib7(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[92];
			this._x1 ^= this._workingKey[93];
			this._x2 ^= this._workingKey[94];
			this._x3 ^= this._workingKey[95];
			this.InverseLT();
			this.Ib6(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[88];
			this._x1 ^= this._workingKey[89];
			this._x2 ^= this._workingKey[90];
			this._x3 ^= this._workingKey[91];
			this.InverseLT();
			this.Ib5(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[84];
			this._x1 ^= this._workingKey[85];
			this._x2 ^= this._workingKey[86];
			this._x3 ^= this._workingKey[87];
			this.InverseLT();
			this.Ib4(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[80];
			this._x1 ^= this._workingKey[81];
			this._x2 ^= this._workingKey[82];
			this._x3 ^= this._workingKey[83];
			this.InverseLT();
			this.Ib3(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[76];
			this._x1 ^= this._workingKey[77];
			this._x2 ^= this._workingKey[78];
			this._x3 ^= this._workingKey[79];
			this.InverseLT();
			this.Ib2(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[72];
			this._x1 ^= this._workingKey[73];
			this._x2 ^= this._workingKey[74];
			this._x3 ^= this._workingKey[75];
			this.InverseLT();
			this.Ib1(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[68];
			this._x1 ^= this._workingKey[69];
			this._x2 ^= this._workingKey[70];
			this._x3 ^= this._workingKey[71];
			this.InverseLT();
			this.Ib0(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[64];
			this._x1 ^= this._workingKey[65];
			this._x2 ^= this._workingKey[66];
			this._x3 ^= this._workingKey[67];
			this.InverseLT();
			this.Ib7(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[60];
			this._x1 ^= this._workingKey[61];
			this._x2 ^= this._workingKey[62];
			this._x3 ^= this._workingKey[63];
			this.InverseLT();
			this.Ib6(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[56];
			this._x1 ^= this._workingKey[57];
			this._x2 ^= this._workingKey[58];
			this._x3 ^= this._workingKey[59];
			this.InverseLT();
			this.Ib5(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[52];
			this._x1 ^= this._workingKey[53];
			this._x2 ^= this._workingKey[54];
			this._x3 ^= this._workingKey[55];
			this.InverseLT();
			this.Ib4(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[48];
			this._x1 ^= this._workingKey[49];
			this._x2 ^= this._workingKey[50];
			this._x3 ^= this._workingKey[51];
			this.InverseLT();
			this.Ib3(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[44];
			this._x1 ^= this._workingKey[45];
			this._x2 ^= this._workingKey[46];
			this._x3 ^= this._workingKey[47];
			this.InverseLT();
			this.Ib2(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[40];
			this._x1 ^= this._workingKey[41];
			this._x2 ^= this._workingKey[42];
			this._x3 ^= this._workingKey[43];
			this.InverseLT();
			this.Ib1(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[36];
			this._x1 ^= this._workingKey[37];
			this._x2 ^= this._workingKey[38];
			this._x3 ^= this._workingKey[39];
			this.InverseLT();
			this.Ib0(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[32];
			this._x1 ^= this._workingKey[33];
			this._x2 ^= this._workingKey[34];
			this._x3 ^= this._workingKey[35];
			this.InverseLT();
			this.Ib7(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[28];
			this._x1 ^= this._workingKey[29];
			this._x2 ^= this._workingKey[30];
			this._x3 ^= this._workingKey[31];
			this.InverseLT();
			this.Ib6(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[24];
			this._x1 ^= this._workingKey[25];
			this._x2 ^= this._workingKey[26];
			this._x3 ^= this._workingKey[27];
			this.InverseLT();
			this.Ib5(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[20];
			this._x1 ^= this._workingKey[21];
			this._x2 ^= this._workingKey[22];
			this._x3 ^= this._workingKey[23];
			this.InverseLT();
			this.Ib4(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[16];
			this._x1 ^= this._workingKey[17];
			this._x2 ^= this._workingKey[18];
			this._x3 ^= this._workingKey[19];
			this.InverseLT();
			this.Ib3(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[12];
			this._x1 ^= this._workingKey[13];
			this._x2 ^= this._workingKey[14];
			this._x3 ^= this._workingKey[15];
			this.InverseLT();
			this.Ib2(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[8];
			this._x1 ^= this._workingKey[9];
			this._x2 ^= this._workingKey[10];
			this._x3 ^= this._workingKey[11];
			this.InverseLT();
			this.Ib1(this._x0, this._x1, this._x2, this._x3);
			this._x0 ^= this._workingKey[4];
			this._x1 ^= this._workingKey[5];
			this._x2 ^= this._workingKey[6];
			this._x3 ^= this._workingKey[7];
			this.InverseLT();
			this.Ib0(this._x0, this._x1, this._x2, this._x3);
			SerpentCipher.WordToBytes(this._x3 ^ this._workingKey[3], outputBuffer, outputOffset);
			SerpentCipher.WordToBytes(this._x2 ^ this._workingKey[2], outputBuffer, outputOffset + 4);
			SerpentCipher.WordToBytes(this._x1 ^ this._workingKey[1], outputBuffer, outputOffset + 8);
			SerpentCipher.WordToBytes(this._x0 ^ this._workingKey[0], outputBuffer, outputOffset + 12);
			return (int)base.BlockSize;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		private int[] MakeWorkingKey(byte[] key)
		{
			int[] array = new int[16];
			int num = 0;
			int i;
			for (i = key.Length - 4; i > 0; i -= 4)
			{
				array[num++] = SerpentCipher.BytesToWord(key, i);
			}
			if (i == 0)
			{
				array[num++] = SerpentCipher.BytesToWord(key, 0);
				if (num < 8)
				{
					array[num] = 1;
				}
				int[] array2 = new int[132];
				for (int j = 8; j < 16; j++)
				{
					array[j] = SerpentCipher.RotateLeft(array[j - 8] ^ array[j - 5] ^ array[j - 3] ^ array[j - 1] ^ -1640531527 ^ j - 8, 11);
				}
				Buffer.BlockCopy(array, 8, array2, 0, 8);
				for (int k = 8; k < 132; k++)
				{
					array2[k] = SerpentCipher.RotateLeft(array2[k - 8] ^ array2[k - 5] ^ array2[k - 3] ^ array2[k - 1] ^ -1640531527 ^ k, 11);
				}
				this.Sb3(array2[0], array2[1], array2[2], array2[3]);
				array2[0] = this._x0;
				array2[1] = this._x1;
				array2[2] = this._x2;
				array2[3] = this._x3;
				this.Sb2(array2[4], array2[5], array2[6], array2[7]);
				array2[4] = this._x0;
				array2[5] = this._x1;
				array2[6] = this._x2;
				array2[7] = this._x3;
				this.Sb1(array2[8], array2[9], array2[10], array2[11]);
				array2[8] = this._x0;
				array2[9] = this._x1;
				array2[10] = this._x2;
				array2[11] = this._x3;
				this.Sb0(array2[12], array2[13], array2[14], array2[15]);
				array2[12] = this._x0;
				array2[13] = this._x1;
				array2[14] = this._x2;
				array2[15] = this._x3;
				this.Sb7(array2[16], array2[17], array2[18], array2[19]);
				array2[16] = this._x0;
				array2[17] = this._x1;
				array2[18] = this._x2;
				array2[19] = this._x3;
				this.Sb6(array2[20], array2[21], array2[22], array2[23]);
				array2[20] = this._x0;
				array2[21] = this._x1;
				array2[22] = this._x2;
				array2[23] = this._x3;
				this.Sb5(array2[24], array2[25], array2[26], array2[27]);
				array2[24] = this._x0;
				array2[25] = this._x1;
				array2[26] = this._x2;
				array2[27] = this._x3;
				this.Sb4(array2[28], array2[29], array2[30], array2[31]);
				array2[28] = this._x0;
				array2[29] = this._x1;
				array2[30] = this._x2;
				array2[31] = this._x3;
				this.Sb3(array2[32], array2[33], array2[34], array2[35]);
				array2[32] = this._x0;
				array2[33] = this._x1;
				array2[34] = this._x2;
				array2[35] = this._x3;
				this.Sb2(array2[36], array2[37], array2[38], array2[39]);
				array2[36] = this._x0;
				array2[37] = this._x1;
				array2[38] = this._x2;
				array2[39] = this._x3;
				this.Sb1(array2[40], array2[41], array2[42], array2[43]);
				array2[40] = this._x0;
				array2[41] = this._x1;
				array2[42] = this._x2;
				array2[43] = this._x3;
				this.Sb0(array2[44], array2[45], array2[46], array2[47]);
				array2[44] = this._x0;
				array2[45] = this._x1;
				array2[46] = this._x2;
				array2[47] = this._x3;
				this.Sb7(array2[48], array2[49], array2[50], array2[51]);
				array2[48] = this._x0;
				array2[49] = this._x1;
				array2[50] = this._x2;
				array2[51] = this._x3;
				this.Sb6(array2[52], array2[53], array2[54], array2[55]);
				array2[52] = this._x0;
				array2[53] = this._x1;
				array2[54] = this._x2;
				array2[55] = this._x3;
				this.Sb5(array2[56], array2[57], array2[58], array2[59]);
				array2[56] = this._x0;
				array2[57] = this._x1;
				array2[58] = this._x2;
				array2[59] = this._x3;
				this.Sb4(array2[60], array2[61], array2[62], array2[63]);
				array2[60] = this._x0;
				array2[61] = this._x1;
				array2[62] = this._x2;
				array2[63] = this._x3;
				this.Sb3(array2[64], array2[65], array2[66], array2[67]);
				array2[64] = this._x0;
				array2[65] = this._x1;
				array2[66] = this._x2;
				array2[67] = this._x3;
				this.Sb2(array2[68], array2[69], array2[70], array2[71]);
				array2[68] = this._x0;
				array2[69] = this._x1;
				array2[70] = this._x2;
				array2[71] = this._x3;
				this.Sb1(array2[72], array2[73], array2[74], array2[75]);
				array2[72] = this._x0;
				array2[73] = this._x1;
				array2[74] = this._x2;
				array2[75] = this._x3;
				this.Sb0(array2[76], array2[77], array2[78], array2[79]);
				array2[76] = this._x0;
				array2[77] = this._x1;
				array2[78] = this._x2;
				array2[79] = this._x3;
				this.Sb7(array2[80], array2[81], array2[82], array2[83]);
				array2[80] = this._x0;
				array2[81] = this._x1;
				array2[82] = this._x2;
				array2[83] = this._x3;
				this.Sb6(array2[84], array2[85], array2[86], array2[87]);
				array2[84] = this._x0;
				array2[85] = this._x1;
				array2[86] = this._x2;
				array2[87] = this._x3;
				this.Sb5(array2[88], array2[89], array2[90], array2[91]);
				array2[88] = this._x0;
				array2[89] = this._x1;
				array2[90] = this._x2;
				array2[91] = this._x3;
				this.Sb4(array2[92], array2[93], array2[94], array2[95]);
				array2[92] = this._x0;
				array2[93] = this._x1;
				array2[94] = this._x2;
				array2[95] = this._x3;
				this.Sb3(array2[96], array2[97], array2[98], array2[99]);
				array2[96] = this._x0;
				array2[97] = this._x1;
				array2[98] = this._x2;
				array2[99] = this._x3;
				this.Sb2(array2[100], array2[101], array2[102], array2[103]);
				array2[100] = this._x0;
				array2[101] = this._x1;
				array2[102] = this._x2;
				array2[103] = this._x3;
				this.Sb1(array2[104], array2[105], array2[106], array2[107]);
				array2[104] = this._x0;
				array2[105] = this._x1;
				array2[106] = this._x2;
				array2[107] = this._x3;
				this.Sb0(array2[108], array2[109], array2[110], array2[111]);
				array2[108] = this._x0;
				array2[109] = this._x1;
				array2[110] = this._x2;
				array2[111] = this._x3;
				this.Sb7(array2[112], array2[113], array2[114], array2[115]);
				array2[112] = this._x0;
				array2[113] = this._x1;
				array2[114] = this._x2;
				array2[115] = this._x3;
				this.Sb6(array2[116], array2[117], array2[118], array2[119]);
				array2[116] = this._x0;
				array2[117] = this._x1;
				array2[118] = this._x2;
				array2[119] = this._x3;
				this.Sb5(array2[120], array2[121], array2[122], array2[123]);
				array2[120] = this._x0;
				array2[121] = this._x1;
				array2[122] = this._x2;
				array2[123] = this._x3;
				this.Sb4(array2[124], array2[125], array2[126], array2[127]);
				array2[124] = this._x0;
				array2[125] = this._x1;
				array2[126] = this._x2;
				array2[127] = this._x3;
				this.Sb3(array2[128], array2[129], array2[130], array2[131]);
				array2[128] = this._x0;
				array2[129] = this._x1;
				array2[130] = this._x2;
				array2[131] = this._x3;
				return array2;
			}
			throw new ArgumentException("key must be a multiple of 4 bytes");
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001B3BE File Offset: 0x000195BE
		private static int RotateLeft(int x, int bits)
		{
			return x << bits | (int)((uint)x >> 32 - bits);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00015BF5 File Offset: 0x00013DF5
		private static int RotateRight(int x, int bits)
		{
			return (int)((uint)x >> bits | (uint)((uint)x << 32 - bits));
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001B3D0 File Offset: 0x000195D0
		private static int BytesToWord(byte[] src, int srcOff)
		{
			return (int)(src[srcOff] & byte.MaxValue) << 24 | (int)(src[srcOff + 1] & byte.MaxValue) << 16 | (int)(src[srcOff + 2] & byte.MaxValue) << 8 | (int)(src[srcOff + 3] & byte.MaxValue);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001B407 File Offset: 0x00019607
		private static void WordToBytes(int word, byte[] dst, int dstOff)
		{
			dst[dstOff + 3] = (byte)word;
			dst[dstOff + 2] = (byte)((uint)word >> 8);
			dst[dstOff + 1] = (byte)((uint)word >> 16);
			dst[dstOff] = (byte)((uint)word >> 24);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001B42C File Offset: 0x0001962C
		private void Sb0(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = c ^ num;
			int num3 = b ^ num2;
			this._x3 = ((a & d) ^ num3);
			int num4 = a ^ (b & num);
			this._x2 = (num3 ^ (c | num4));
			int num5 = this._x3 & (num2 ^ num4);
			this._x1 = (~num2 ^ num5);
			this._x0 = (num5 ^ ~num4);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001B488 File Offset: 0x00019688
		private void Ib0(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = d ^ (num | num2);
			int num4 = c ^ num3;
			this._x2 = (num2 ^ num4);
			int num5 = num ^ (d & num2);
			this._x1 = (num3 ^ (this._x2 & num5));
			this._x3 = ((a & num3) ^ (num4 | this._x1));
			this._x0 = (this._x3 ^ (num4 ^ num5));
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001B4EC File Offset: 0x000196EC
		private void Sb1(int a, int b, int c, int d)
		{
			int num = b ^ ~a;
			int num2 = c ^ (a | num);
			this._x2 = (d ^ num2);
			int num3 = b ^ (d | num);
			int num4 = num ^ this._x2;
			this._x3 = (num4 ^ (num2 & num3));
			int num5 = num2 ^ num3;
			this._x1 = (this._x3 ^ num5);
			this._x0 = (num2 ^ (num4 & num5));
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001B54C File Offset: 0x0001974C
		private void Ib1(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = a ^ (b & num);
			int num3 = num ^ num2;
			this._x3 = (c ^ num3);
			int num4 = b ^ (num & num2);
			int num5 = this._x3 | num4;
			this._x1 = (num2 ^ num5);
			int num6 = ~this._x1;
			int num7 = this._x3 ^ num4;
			this._x0 = (num6 ^ num7);
			this._x2 = (num3 ^ (num6 | num7));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001B5B8 File Offset: 0x000197B8
		private void Sb2(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = b ^ d;
			int num3 = c & num;
			this._x0 = (num2 ^ num3);
			int num4 = c ^ num;
			int num5 = c ^ this._x0;
			int num6 = b & num5;
			this._x3 = (num4 ^ num6);
			this._x2 = (a ^ ((d | num6) & (this._x0 | num4)));
			this._x1 = (num2 ^ this._x3 ^ (this._x2 ^ (d | num)));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001B628 File Offset: 0x00019828
		private void Ib2(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = ~num;
			int num3 = a ^ c;
			int num4 = c ^ num;
			int num5 = b & num4;
			this._x0 = (num3 ^ num5);
			int num6 = a | num2;
			int num7 = d ^ num6;
			int num8 = num3 | num7;
			this._x3 = (num ^ num8);
			int num9 = ~num4;
			int num10 = this._x0 | this._x3;
			this._x1 = (num9 ^ num10);
			this._x2 = ((d & num9) ^ (num3 ^ num10));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001B6A0 File Offset: 0x000198A0
		private void Sb3(int a, int b, int c, int d)
		{
			int num = a ^ b;
			int num2 = a & c;
			int num3 = a | d;
			int num4 = c ^ d;
			int num5 = num & num3;
			int num6 = num2 | num5;
			this._x2 = (num4 ^ num6);
			int num7 = b ^ num3;
			int num8 = num6 ^ num7;
			int num9 = num4 & num8;
			this._x0 = (num ^ num9);
			int num10 = this._x2 & this._x0;
			this._x1 = (num8 ^ num10);
			this._x3 = ((b | d) ^ (num4 ^ num10));
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001B718 File Offset: 0x00019918
		private void Ib3(int a, int b, int c, int d)
		{
			int num = a | b;
			int num2 = b ^ c;
			int num3 = b & num2;
			int num4 = a ^ num3;
			int num5 = c ^ num4;
			int num6 = d | num4;
			this._x0 = (num2 ^ num6);
			int num7 = num2 | num6;
			int num8 = d ^ num7;
			this._x2 = (num5 ^ num8);
			int num9 = num ^ num8;
			int num10 = this._x0 & num9;
			this._x3 = (num4 ^ num10);
			this._x1 = (this._x3 ^ (this._x0 ^ num9));
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001B790 File Offset: 0x00019990
		private void Sb4(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = d & num;
			int num3 = c ^ num2;
			int num4 = b | num3;
			this._x3 = (num ^ num4);
			int num5 = ~b;
			int num6 = num | num5;
			this._x0 = (num3 ^ num6);
			int num7 = a & this._x0;
			int num8 = num ^ num5;
			int num9 = num4 & num8;
			this._x2 = (num7 ^ num9);
			this._x1 = (a ^ num3 ^ (num8 & this._x2));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001B800 File Offset: 0x00019A00
		private void Ib4(int a, int b, int c, int d)
		{
			int num = c | d;
			int num2 = a & num;
			int num3 = b ^ num2;
			int num4 = a & num3;
			int num5 = c ^ num4;
			this._x1 = (d ^ num5);
			int num6 = ~a;
			int num7 = num5 & this._x1;
			this._x3 = (num3 ^ num7);
			int num8 = this._x1 | num6;
			int num9 = d ^ num8;
			this._x0 = (this._x3 ^ num9);
			this._x2 = ((num3 & num9) ^ (this._x1 ^ num6));
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001B87C File Offset: 0x00019A7C
		private void Sb5(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = a ^ d;
			int num4 = c ^ num;
			int num5 = num2 | num3;
			this._x0 = (num4 ^ num5);
			int num6 = d & this._x0;
			int num7 = num2 ^ this._x0;
			this._x1 = (num6 ^ num7);
			int num8 = num | this._x0;
			int num9 = num2 | num6;
			int num10 = num3 ^ num8;
			this._x2 = (num9 ^ num10);
			this._x3 = (b ^ num6 ^ (this._x1 & num10));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001B8FC File Offset: 0x00019AFC
		private void Ib5(int a, int b, int c, int d)
		{
			int num = ~c;
			int num2 = b & num;
			int num3 = d ^ num2;
			int num4 = a & num3;
			int num5 = b ^ num;
			this._x3 = (num4 ^ num5);
			int num6 = b | this._x3;
			int num7 = a & num6;
			this._x1 = (num3 ^ num7);
			int num8 = a | d;
			int num9 = num ^ num6;
			this._x0 = (num8 ^ num9);
			this._x2 = ((b & num8) ^ (num4 | (a ^ c)));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001B96C File Offset: 0x00019B6C
		private void Sb6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ d;
			int num3 = b ^ num2;
			int num4 = num | num2;
			int num5 = c ^ num4;
			this._x1 = (b ^ num5);
			int num6 = num2 | this._x1;
			int num7 = d ^ num6;
			int num8 = num5 & num7;
			this._x2 = (num3 ^ num8);
			int num9 = num5 ^ num7;
			this._x0 = (this._x2 ^ num9);
			this._x3 = (~num5 ^ (num3 & num9));
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		private void Ib6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = c ^ num2;
			int num4 = c | num;
			int num5 = d ^ num4;
			this._x1 = (num3 ^ num5);
			int num6 = num3 & num5;
			int num7 = num2 ^ num6;
			int num8 = b | num7;
			this._x3 = (num5 ^ num8);
			int num9 = b | this._x3;
			this._x0 = (num7 ^ num9);
			this._x2 = ((d & num) ^ (num3 ^ num9));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001BA48 File Offset: 0x00019C48
		private void Sb7(int a, int b, int c, int d)
		{
			int num = b ^ c;
			int num2 = c & num;
			int num3 = d ^ num2;
			int num4 = a ^ num3;
			int num5 = d | num;
			int num6 = num4 & num5;
			this._x1 = (b ^ num6);
			int num7 = num3 | this._x1;
			int num8 = a & num4;
			this._x3 = (num ^ num8);
			int num9 = num4 ^ num7;
			int num10 = this._x3 & num9;
			this._x2 = (num3 ^ num10);
			this._x0 = (~num9 ^ (this._x3 & this._x2));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		private void Ib7(int a, int b, int c, int d)
		{
			int num = c | (a & b);
			int num2 = d & (a | b);
			this._x3 = (num ^ num2);
			int num3 = ~d;
			int num4 = b ^ num2;
			int num5 = num4 | (this._x3 ^ num3);
			this._x1 = (a ^ num5);
			this._x0 = (c ^ num4 ^ (d | this._x1));
			this._x2 = (num ^ this._x1 ^ (this._x0 ^ (a & this._x3)));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001BB3C File Offset: 0x00019D3C
		private void LT()
		{
			int num = SerpentCipher.RotateLeft(this._x0, 13);
			int num2 = SerpentCipher.RotateLeft(this._x2, 3);
			int x = this._x1 ^ num ^ num2;
			int x2 = this._x3 ^ num2 ^ num << 3;
			this._x1 = SerpentCipher.RotateLeft(x, 1);
			this._x3 = SerpentCipher.RotateLeft(x2, 7);
			this._x0 = SerpentCipher.RotateLeft(num ^ this._x1 ^ this._x3, 5);
			this._x2 = SerpentCipher.RotateLeft(num2 ^ this._x3 ^ this._x1 << 7, 22);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		private void InverseLT()
		{
			int num = SerpentCipher.RotateRight(this._x2, 22) ^ this._x3 ^ this._x1 << 7;
			int num2 = SerpentCipher.RotateRight(this._x0, 5) ^ this._x1 ^ this._x3;
			int num3 = SerpentCipher.RotateRight(this._x3, 7);
			int num4 = SerpentCipher.RotateRight(this._x1, 1);
			this._x3 = (num3 ^ num ^ num2 << 3);
			this._x1 = (num4 ^ num2 ^ num);
			this._x2 = SerpentCipher.RotateRight(num, 3);
			this._x0 = SerpentCipher.RotateRight(num2, 13);
		}

		// Token: 0x040002B5 RID: 693
		private const int Rounds = 32;

		// Token: 0x040002B6 RID: 694
		private const int Phi = -1640531527;

		// Token: 0x040002B7 RID: 695
		private readonly int[] _workingKey;

		// Token: 0x040002B8 RID: 696
		private int _x0;

		// Token: 0x040002B9 RID: 697
		private int _x1;

		// Token: 0x040002BA RID: 698
		private int _x2;

		// Token: 0x040002BB RID: 699
		private int _x3;
	}
}
