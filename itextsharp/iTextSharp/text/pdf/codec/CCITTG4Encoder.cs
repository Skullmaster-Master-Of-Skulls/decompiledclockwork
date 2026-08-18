using System;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x02000060 RID: 96
	public class CCITTG4Encoder
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000EAD4 File Offset: 0x0000DAD4
		public CCITTG4Encoder(int width)
		{
			int[][] array = new int[109][];
			int[][] array2 = array;
			int num = 0;
			int[] array3 = new int[3];
			array3[0] = 8;
			array3[1] = 53;
			array2[num] = array3;
			array[1] = new int[]
			{
				6,
				7,
				1
			};
			array[2] = new int[]
			{
				4,
				7,
				2
			};
			array[3] = new int[]
			{
				4,
				8,
				3
			};
			array[4] = new int[]
			{
				4,
				11,
				4
			};
			array[5] = new int[]
			{
				4,
				12,
				5
			};
			array[6] = new int[]
			{
				4,
				14,
				6
			};
			array[7] = new int[]
			{
				4,
				15,
				7
			};
			array[8] = new int[]
			{
				5,
				19,
				8
			};
			array[9] = new int[]
			{
				5,
				20,
				9
			};
			array[10] = new int[]
			{
				5,
				7,
				10
			};
			array[11] = new int[]
			{
				5,
				8,
				11
			};
			array[12] = new int[]
			{
				6,
				8,
				12
			};
			array[13] = new int[]
			{
				6,
				3,
				13
			};
			array[14] = new int[]
			{
				6,
				52,
				14
			};
			array[15] = new int[]
			{
				6,
				53,
				15
			};
			array[16] = new int[]
			{
				6,
				42,
				16
			};
			array[17] = new int[]
			{
				6,
				43,
				17
			};
			array[18] = new int[]
			{
				7,
				39,
				18
			};
			array[19] = new int[]
			{
				7,
				12,
				19
			};
			array[20] = new int[]
			{
				7,
				8,
				20
			};
			array[21] = new int[]
			{
				7,
				23,
				21
			};
			array[22] = new int[]
			{
				7,
				3,
				22
			};
			array[23] = new int[]
			{
				7,
				4,
				23
			};
			array[24] = new int[]
			{
				7,
				40,
				24
			};
			array[25] = new int[]
			{
				7,
				43,
				25
			};
			array[26] = new int[]
			{
				7,
				19,
				26
			};
			array[27] = new int[]
			{
				7,
				36,
				27
			};
			array[28] = new int[]
			{
				7,
				24,
				28
			};
			array[29] = new int[]
			{
				8,
				2,
				29
			};
			array[30] = new int[]
			{
				8,
				3,
				30
			};
			array[31] = new int[]
			{
				8,
				26,
				31
			};
			array[32] = new int[]
			{
				8,
				27,
				32
			};
			array[33] = new int[]
			{
				8,
				18,
				33
			};
			array[34] = new int[]
			{
				8,
				19,
				34
			};
			array[35] = new int[]
			{
				8,
				20,
				35
			};
			array[36] = new int[]
			{
				8,
				21,
				36
			};
			array[37] = new int[]
			{
				8,
				22,
				37
			};
			array[38] = new int[]
			{
				8,
				23,
				38
			};
			array[39] = new int[]
			{
				8,
				40,
				39
			};
			array[40] = new int[]
			{
				8,
				41,
				40
			};
			array[41] = new int[]
			{
				8,
				42,
				41
			};
			array[42] = new int[]
			{
				8,
				43,
				42
			};
			array[43] = new int[]
			{
				8,
				44,
				43
			};
			array[44] = new int[]
			{
				8,
				45,
				44
			};
			array[45] = new int[]
			{
				8,
				4,
				45
			};
			array[46] = new int[]
			{
				8,
				5,
				46
			};
			array[47] = new int[]
			{
				8,
				10,
				47
			};
			array[48] = new int[]
			{
				8,
				11,
				48
			};
			array[49] = new int[]
			{
				8,
				82,
				49
			};
			array[50] = new int[]
			{
				8,
				83,
				50
			};
			array[51] = new int[]
			{
				8,
				84,
				51
			};
			array[52] = new int[]
			{
				8,
				85,
				52
			};
			array[53] = new int[]
			{
				8,
				36,
				53
			};
			array[54] = new int[]
			{
				8,
				37,
				54
			};
			array[55] = new int[]
			{
				8,
				88,
				55
			};
			array[56] = new int[]
			{
				8,
				89,
				56
			};
			array[57] = new int[]
			{
				8,
				90,
				57
			};
			array[58] = new int[]
			{
				8,
				91,
				58
			};
			array[59] = new int[]
			{
				8,
				74,
				59
			};
			array[60] = new int[]
			{
				8,
				75,
				60
			};
			array[61] = new int[]
			{
				8,
				50,
				61
			};
			array[62] = new int[]
			{
				8,
				51,
				62
			};
			array[63] = new int[]
			{
				8,
				52,
				63
			};
			array[64] = new int[]
			{
				5,
				27,
				64
			};
			array[65] = new int[]
			{
				5,
				18,
				128
			};
			array[66] = new int[]
			{
				6,
				23,
				192
			};
			array[67] = new int[]
			{
				7,
				55,
				256
			};
			array[68] = new int[]
			{
				8,
				54,
				320
			};
			array[69] = new int[]
			{
				8,
				55,
				384
			};
			array[70] = new int[]
			{
				8,
				100,
				448
			};
			array[71] = new int[]
			{
				8,
				101,
				512
			};
			array[72] = new int[]
			{
				8,
				104,
				576
			};
			array[73] = new int[]
			{
				8,
				103,
				640
			};
			array[74] = new int[]
			{
				9,
				204,
				704
			};
			array[75] = new int[]
			{
				9,
				205,
				768
			};
			array[76] = new int[]
			{
				9,
				210,
				832
			};
			array[77] = new int[]
			{
				9,
				211,
				896
			};
			array[78] = new int[]
			{
				9,
				212,
				960
			};
			array[79] = new int[]
			{
				9,
				213,
				1024
			};
			array[80] = new int[]
			{
				9,
				214,
				1088
			};
			array[81] = new int[]
			{
				9,
				215,
				1152
			};
			array[82] = new int[]
			{
				9,
				216,
				1216
			};
			array[83] = new int[]
			{
				9,
				217,
				1280
			};
			array[84] = new int[]
			{
				9,
				218,
				1344
			};
			array[85] = new int[]
			{
				9,
				219,
				1408
			};
			array[86] = new int[]
			{
				9,
				152,
				1472
			};
			array[87] = new int[]
			{
				9,
				153,
				1536
			};
			array[88] = new int[]
			{
				9,
				154,
				1600
			};
			array[89] = new int[]
			{
				6,
				24,
				1664
			};
			array[90] = new int[]
			{
				9,
				155,
				1728
			};
			array[91] = new int[]
			{
				11,
				8,
				1792
			};
			array[92] = new int[]
			{
				11,
				12,
				1856
			};
			array[93] = new int[]
			{
				11,
				13,
				1920
			};
			array[94] = new int[]
			{
				12,
				18,
				1984
			};
			array[95] = new int[]
			{
				12,
				19,
				2048
			};
			array[96] = new int[]
			{
				12,
				20,
				2112
			};
			array[97] = new int[]
			{
				12,
				21,
				2176
			};
			array[98] = new int[]
			{
				12,
				22,
				2240
			};
			array[99] = new int[]
			{
				12,
				23,
				2304
			};
			array[100] = new int[]
			{
				12,
				28,
				2368
			};
			array[101] = new int[]
			{
				12,
				29,
				2432
			};
			array[102] = new int[]
			{
				12,
				30,
				2496
			};
			array[103] = new int[]
			{
				12,
				31,
				2560
			};
			array[104] = new int[]
			{
				12,
				1,
				-1
			};
			array[105] = new int[]
			{
				9,
				1,
				-2
			};
			array[106] = new int[]
			{
				10,
				1,
				-2
			};
			array[107] = new int[]
			{
				11,
				1,
				-2
			};
			array[108] = new int[]
			{
				12,
				0,
				-2
			};
			this.TIFFFaxWhiteCodes = array;
			int[][] array4 = new int[109][];
			int[][] array5 = array4;
			int num2 = 0;
			int[] array6 = new int[3];
			array6[0] = 10;
			array6[1] = 55;
			array5[num2] = array6;
			array4[1] = new int[]
			{
				3,
				2,
				1
			};
			array4[2] = new int[]
			{
				2,
				3,
				2
			};
			array4[3] = new int[]
			{
				2,
				2,
				3
			};
			array4[4] = new int[]
			{
				3,
				3,
				4
			};
			array4[5] = new int[]
			{
				4,
				3,
				5
			};
			array4[6] = new int[]
			{
				4,
				2,
				6
			};
			array4[7] = new int[]
			{
				5,
				3,
				7
			};
			array4[8] = new int[]
			{
				6,
				5,
				8
			};
			array4[9] = new int[]
			{
				6,
				4,
				9
			};
			array4[10] = new int[]
			{
				7,
				4,
				10
			};
			array4[11] = new int[]
			{
				7,
				5,
				11
			};
			array4[12] = new int[]
			{
				7,
				7,
				12
			};
			array4[13] = new int[]
			{
				8,
				4,
				13
			};
			array4[14] = new int[]
			{
				8,
				7,
				14
			};
			array4[15] = new int[]
			{
				9,
				24,
				15
			};
			array4[16] = new int[]
			{
				10,
				23,
				16
			};
			array4[17] = new int[]
			{
				10,
				24,
				17
			};
			array4[18] = new int[]
			{
				10,
				8,
				18
			};
			array4[19] = new int[]
			{
				11,
				103,
				19
			};
			array4[20] = new int[]
			{
				11,
				104,
				20
			};
			array4[21] = new int[]
			{
				11,
				108,
				21
			};
			array4[22] = new int[]
			{
				11,
				55,
				22
			};
			array4[23] = new int[]
			{
				11,
				40,
				23
			};
			array4[24] = new int[]
			{
				11,
				23,
				24
			};
			array4[25] = new int[]
			{
				11,
				24,
				25
			};
			array4[26] = new int[]
			{
				12,
				202,
				26
			};
			array4[27] = new int[]
			{
				12,
				203,
				27
			};
			array4[28] = new int[]
			{
				12,
				204,
				28
			};
			array4[29] = new int[]
			{
				12,
				205,
				29
			};
			array4[30] = new int[]
			{
				12,
				104,
				30
			};
			array4[31] = new int[]
			{
				12,
				105,
				31
			};
			array4[32] = new int[]
			{
				12,
				106,
				32
			};
			array4[33] = new int[]
			{
				12,
				107,
				33
			};
			array4[34] = new int[]
			{
				12,
				210,
				34
			};
			array4[35] = new int[]
			{
				12,
				211,
				35
			};
			array4[36] = new int[]
			{
				12,
				212,
				36
			};
			array4[37] = new int[]
			{
				12,
				213,
				37
			};
			array4[38] = new int[]
			{
				12,
				214,
				38
			};
			array4[39] = new int[]
			{
				12,
				215,
				39
			};
			array4[40] = new int[]
			{
				12,
				108,
				40
			};
			array4[41] = new int[]
			{
				12,
				109,
				41
			};
			array4[42] = new int[]
			{
				12,
				218,
				42
			};
			array4[43] = new int[]
			{
				12,
				219,
				43
			};
			array4[44] = new int[]
			{
				12,
				84,
				44
			};
			array4[45] = new int[]
			{
				12,
				85,
				45
			};
			array4[46] = new int[]
			{
				12,
				86,
				46
			};
			array4[47] = new int[]
			{
				12,
				87,
				47
			};
			array4[48] = new int[]
			{
				12,
				100,
				48
			};
			array4[49] = new int[]
			{
				12,
				101,
				49
			};
			array4[50] = new int[]
			{
				12,
				82,
				50
			};
			array4[51] = new int[]
			{
				12,
				83,
				51
			};
			array4[52] = new int[]
			{
				12,
				36,
				52
			};
			array4[53] = new int[]
			{
				12,
				55,
				53
			};
			array4[54] = new int[]
			{
				12,
				56,
				54
			};
			array4[55] = new int[]
			{
				12,
				39,
				55
			};
			array4[56] = new int[]
			{
				12,
				40,
				56
			};
			array4[57] = new int[]
			{
				12,
				88,
				57
			};
			array4[58] = new int[]
			{
				12,
				89,
				58
			};
			array4[59] = new int[]
			{
				12,
				43,
				59
			};
			array4[60] = new int[]
			{
				12,
				44,
				60
			};
			array4[61] = new int[]
			{
				12,
				90,
				61
			};
			array4[62] = new int[]
			{
				12,
				102,
				62
			};
			array4[63] = new int[]
			{
				12,
				103,
				63
			};
			array4[64] = new int[]
			{
				10,
				15,
				64
			};
			array4[65] = new int[]
			{
				12,
				200,
				128
			};
			array4[66] = new int[]
			{
				12,
				201,
				192
			};
			array4[67] = new int[]
			{
				12,
				91,
				256
			};
			array4[68] = new int[]
			{
				12,
				51,
				320
			};
			array4[69] = new int[]
			{
				12,
				52,
				384
			};
			array4[70] = new int[]
			{
				12,
				53,
				448
			};
			array4[71] = new int[]
			{
				13,
				108,
				512
			};
			array4[72] = new int[]
			{
				13,
				109,
				576
			};
			array4[73] = new int[]
			{
				13,
				74,
				640
			};
			array4[74] = new int[]
			{
				13,
				75,
				704
			};
			array4[75] = new int[]
			{
				13,
				76,
				768
			};
			array4[76] = new int[]
			{
				13,
				77,
				832
			};
			array4[77] = new int[]
			{
				13,
				114,
				896
			};
			array4[78] = new int[]
			{
				13,
				115,
				960
			};
			array4[79] = new int[]
			{
				13,
				116,
				1024
			};
			array4[80] = new int[]
			{
				13,
				117,
				1088
			};
			array4[81] = new int[]
			{
				13,
				118,
				1152
			};
			array4[82] = new int[]
			{
				13,
				119,
				1216
			};
			array4[83] = new int[]
			{
				13,
				82,
				1280
			};
			array4[84] = new int[]
			{
				13,
				83,
				1344
			};
			array4[85] = new int[]
			{
				13,
				84,
				1408
			};
			array4[86] = new int[]
			{
				13,
				85,
				1472
			};
			array4[87] = new int[]
			{
				13,
				90,
				1536
			};
			array4[88] = new int[]
			{
				13,
				91,
				1600
			};
			array4[89] = new int[]
			{
				13,
				100,
				1664
			};
			array4[90] = new int[]
			{
				13,
				101,
				1728
			};
			array4[91] = new int[]
			{
				11,
				8,
				1792
			};
			array4[92] = new int[]
			{
				11,
				12,
				1856
			};
			array4[93] = new int[]
			{
				11,
				13,
				1920
			};
			array4[94] = new int[]
			{
				12,
				18,
				1984
			};
			array4[95] = new int[]
			{
				12,
				19,
				2048
			};
			array4[96] = new int[]
			{
				12,
				20,
				2112
			};
			array4[97] = new int[]
			{
				12,
				21,
				2176
			};
			array4[98] = new int[]
			{
				12,
				22,
				2240
			};
			array4[99] = new int[]
			{
				12,
				23,
				2304
			};
			array4[100] = new int[]
			{
				12,
				28,
				2368
			};
			array4[101] = new int[]
			{
				12,
				29,
				2432
			};
			array4[102] = new int[]
			{
				12,
				30,
				2496
			};
			array4[103] = new int[]
			{
				12,
				31,
				2560
			};
			array4[104] = new int[]
			{
				12,
				1,
				-1
			};
			array4[105] = new int[]
			{
				9,
				1,
				-2
			};
			array4[106] = new int[]
			{
				10,
				1,
				-2
			};
			array4[107] = new int[]
			{
				11,
				1,
				-2
			};
			array4[108] = new int[]
			{
				12,
				0,
				-2
			};
			this.TIFFFaxBlackCodes = array4;
			int[] array7 = new int[3];
			array7[0] = 3;
			array7[1] = 1;
			this.horizcode = array7;
			int[] array8 = new int[3];
			array8[0] = 4;
			array8[1] = 1;
			this.passcode = array8;
			int[][] array9 = new int[7][];
			int[][] array10 = array9;
			int num3 = 0;
			int[] array11 = new int[3];
			array11[0] = 7;
			array11[1] = 3;
			array10[num3] = array11;
			int[][] array12 = array9;
			int num4 = 1;
			int[] array13 = new int[3];
			array13[0] = 6;
			array13[1] = 3;
			array12[num4] = array13;
			int[][] array14 = array9;
			int num5 = 2;
			int[] array15 = new int[3];
			array15[0] = 3;
			array15[1] = 3;
			array14[num5] = array15;
			int[][] array16 = array9;
			int num6 = 3;
			int[] array17 = new int[3];
			array17[0] = 1;
			array17[1] = 1;
			array16[num6] = array17;
			int[][] array18 = array9;
			int num7 = 4;
			int[] array19 = new int[3];
			array19[0] = 3;
			array19[1] = 2;
			array18[num7] = array19;
			int[][] array20 = array9;
			int num8 = 5;
			int[] array21 = new int[3];
			array21[0] = 6;
			array21[1] = 2;
			array20[num8] = array21;
			int[][] array22 = array9;
			int num9 = 6;
			int[] array23 = new int[3];
			array23[0] = 7;
			array23[1] = 2;
			array22[num9] = array23;
			this.vcodes = array9;
			this.msbmask = new int[]
			{
				0,
				1,
				3,
				7,
				15,
				31,
				63,
				127,
				255
			};
			base..ctor();
			this.rowpixels = width;
			this.rowbytes = (this.rowpixels + 7) / 8;
			this.refline = new byte[this.rowbytes];
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FE24 File Offset: 0x0000EE24
		public void Fax4Encode(byte[] data, int offset, int size)
		{
			this.dataBp = data;
			this.offsetData = offset;
			this.sizeData = size;
			while (this.sizeData > 0)
			{
				this.Fax3Encode2DRow();
				Array.Copy(this.dataBp, this.offsetData, this.refline, 0, this.rowbytes);
				this.offsetData += this.rowbytes;
				this.sizeData -= this.rowbytes;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000FE9C File Offset: 0x0000EE9C
		public static byte[] Compress(byte[] data, int width, int height)
		{
			CCITTG4Encoder ccittg4Encoder = new CCITTG4Encoder(width);
			ccittg4Encoder.Fax4Encode(data, 0, ccittg4Encoder.rowbytes * height);
			return ccittg4Encoder.Close();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000FEC6 File Offset: 0x0000EEC6
		public void Fax4Encode(byte[] data, int height)
		{
			this.Fax4Encode(data, 0, this.rowbytes * height);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000FED8 File Offset: 0x0000EED8
		private void Putcode(int[] table)
		{
			this.PutBits(table[1], table[0]);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000FEE8 File Offset: 0x0000EEE8
		private void Putspan(int span, int[][] tab)
		{
			int bits;
			int length;
			while (span >= 2624)
			{
				int[] array = tab[103];
				bits = array[1];
				length = array[0];
				this.PutBits(bits, length);
				span -= array[2];
			}
			if (span >= 64)
			{
				int[] array2 = tab[63 + (span >> 6)];
				bits = array2[1];
				length = array2[0];
				this.PutBits(bits, length);
				span -= array2[2];
			}
			bits = tab[span][1];
			length = tab[span][0];
			this.PutBits(bits, length);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000FF54 File Offset: 0x0000EF54
		private void PutBits(int bits, int length)
		{
			while (length > this.bit)
			{
				this.data |= bits >> length - this.bit;
				length -= this.bit;
				this.outBuf.Append((byte)this.data);
				this.data = 0;
				this.bit = 8;
			}
			this.data |= (bits & this.msbmask[length]) << this.bit - length;
			this.bit -= length;
			if (this.bit == 0)
			{
				this.outBuf.Append((byte)this.data);
				this.data = 0;
				this.bit = 8;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001000C File Offset: 0x0000F00C
		private void Fax3Encode2DRow()
		{
			int num = 0;
			int num2 = (this.Pixel(this.dataBp, this.offsetData, 0) != 0) ? 0 : CCITTG4Encoder.Finddiff(this.dataBp, this.offsetData, 0, this.rowpixels, 0);
			int num3 = (this.Pixel(this.refline, 0, 0) != 0) ? 0 : CCITTG4Encoder.Finddiff(this.refline, 0, 0, this.rowpixels, 0);
			for (;;)
			{
				int num4 = CCITTG4Encoder.Finddiff2(this.refline, 0, num3, this.rowpixels, this.Pixel(this.refline, 0, num3));
				if (num4 >= num2)
				{
					int num5 = num3 - num2;
					if (-3 > num5 || num5 > 3)
					{
						int num6 = CCITTG4Encoder.Finddiff2(this.dataBp, this.offsetData, num2, this.rowpixels, this.Pixel(this.dataBp, this.offsetData, num2));
						this.Putcode(this.horizcode);
						if (num + num2 == 0 || this.Pixel(this.dataBp, this.offsetData, num) == 0)
						{
							this.Putspan(num2 - num, this.TIFFFaxWhiteCodes);
							this.Putspan(num6 - num2, this.TIFFFaxBlackCodes);
						}
						else
						{
							this.Putspan(num2 - num, this.TIFFFaxBlackCodes);
							this.Putspan(num6 - num2, this.TIFFFaxWhiteCodes);
						}
						num = num6;
					}
					else
					{
						this.Putcode(this.vcodes[num5 + 3]);
						num = num2;
					}
				}
				else
				{
					this.Putcode(this.passcode);
					num = num4;
				}
				if (num >= this.rowpixels)
				{
					break;
				}
				num2 = CCITTG4Encoder.Finddiff(this.dataBp, this.offsetData, num, this.rowpixels, this.Pixel(this.dataBp, this.offsetData, num));
				num3 = CCITTG4Encoder.Finddiff(this.refline, 0, num, this.rowpixels, this.Pixel(this.dataBp, this.offsetData, num) ^ 1);
				num3 = CCITTG4Encoder.Finddiff(this.refline, 0, num3, this.rowpixels, this.Pixel(this.dataBp, this.offsetData, num));
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000101F5 File Offset: 0x0000F1F5
		private void Fax4PostEncode()
		{
			this.PutBits(1, 12);
			this.PutBits(1, 12);
			if (this.bit != 8)
			{
				this.outBuf.Append((byte)this.data);
				this.data = 0;
				this.bit = 8;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010233 File Offset: 0x0000F233
		public byte[] Close()
		{
			this.Fax4PostEncode();
			return this.outBuf.ToByteArray();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010246 File Offset: 0x0000F246
		private int Pixel(byte[] data, int offset, int bit)
		{
			if (bit >= this.rowpixels)
			{
				return 0;
			}
			return (data[offset + (bit >> 3)] & byte.MaxValue) >> 7 - (bit & 7) & 1;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001026C File Offset: 0x0000F26C
		private static int Find1span(byte[] bp, int offset, int bs, int be)
		{
			int i = be - bs;
			int num = offset + (bs >> 3);
			int num2;
			int num3;
			if (i > 0 && (num2 = (bs & 7)) != 0)
			{
				num3 = (int)CCITTG4Encoder.oneruns[(int)bp[num] << num2 & 255];
				if (num3 > 8 - num2)
				{
					num3 = 8 - num2;
				}
				if (num3 > i)
				{
					num3 = i;
				}
				if (num2 + num3 < 8)
				{
					return num3;
				}
				i -= num3;
				num++;
			}
			else
			{
				num3 = 0;
			}
			while (i >= 8)
			{
				if (bp[num] != 255)
				{
					return num3 + (int)CCITTG4Encoder.oneruns[(int)(bp[num] & byte.MaxValue)];
				}
				num3 += 8;
				i -= 8;
				num++;
			}
			if (i > 0)
			{
				num2 = (int)CCITTG4Encoder.oneruns[(int)(bp[num] & byte.MaxValue)];
				num3 += ((num2 > i) ? i : num2);
			}
			return num3;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00010318 File Offset: 0x0000F318
		private static int Find0span(byte[] bp, int offset, int bs, int be)
		{
			int i = be - bs;
			int num = offset + (bs >> 3);
			int num2;
			int num3;
			if (i > 0 && (num2 = (bs & 7)) != 0)
			{
				num3 = (int)CCITTG4Encoder.zeroruns[(int)bp[num] << num2 & 255];
				if (num3 > 8 - num2)
				{
					num3 = 8 - num2;
				}
				if (num3 > i)
				{
					num3 = i;
				}
				if (num2 + num3 < 8)
				{
					return num3;
				}
				i -= num3;
				num++;
			}
			else
			{
				num3 = 0;
			}
			while (i >= 8)
			{
				if (bp[num] != 0)
				{
					return num3 + (int)CCITTG4Encoder.zeroruns[(int)(bp[num] & byte.MaxValue)];
				}
				num3 += 8;
				i -= 8;
				num++;
			}
			if (i > 0)
			{
				num2 = (int)CCITTG4Encoder.zeroruns[(int)(bp[num] & byte.MaxValue)];
				num3 += ((num2 > i) ? i : num2);
			}
			return num3;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000103BC File Offset: 0x0000F3BC
		private static int Finddiff(byte[] bp, int offset, int bs, int be, int color)
		{
			return bs + ((color != 0) ? CCITTG4Encoder.Find1span(bp, offset, bs, be) : CCITTG4Encoder.Find0span(bp, offset, bs, be));
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000103D8 File Offset: 0x0000F3D8
		private static int Finddiff2(byte[] bp, int offset, int bs, int be, int color)
		{
			if (bs >= be)
			{
				return be;
			}
			return CCITTG4Encoder.Finddiff(bp, offset, bs, be, color);
		}

		// Token: 0x04000174 RID: 372
		private const int LENGTH = 0;

		// Token: 0x04000175 RID: 373
		private const int CODE = 1;

		// Token: 0x04000176 RID: 374
		private const int RUNLEN = 2;

		// Token: 0x04000177 RID: 375
		private const int EOL = 1;

		// Token: 0x04000178 RID: 376
		private const int G3CODE_EOL = -1;

		// Token: 0x04000179 RID: 377
		private const int G3CODE_INVALID = -2;

		// Token: 0x0400017A RID: 378
		private const int G3CODE_EOF = -3;

		// Token: 0x0400017B RID: 379
		private const int G3CODE_INCOMP = -4;

		// Token: 0x0400017C RID: 380
		private int rowbytes;

		// Token: 0x0400017D RID: 381
		private int rowpixels;

		// Token: 0x0400017E RID: 382
		private int bit = 8;

		// Token: 0x0400017F RID: 383
		private int data;

		// Token: 0x04000180 RID: 384
		private byte[] refline;

		// Token: 0x04000181 RID: 385
		private ByteBuffer outBuf = new ByteBuffer(1024);

		// Token: 0x04000182 RID: 386
		private byte[] dataBp;

		// Token: 0x04000183 RID: 387
		private int offsetData;

		// Token: 0x04000184 RID: 388
		private int sizeData;

		// Token: 0x04000185 RID: 389
		private static byte[] zeroruns = new byte[]
		{
			8,
			7,
			6,
			6,
			5,
			5,
			5,
			5,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x04000186 RID: 390
		private static byte[] oneruns = new byte[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			6,
			6,
			7,
			8
		};

		// Token: 0x04000187 RID: 391
		private int[][] TIFFFaxWhiteCodes;

		// Token: 0x04000188 RID: 392
		private int[][] TIFFFaxBlackCodes;

		// Token: 0x04000189 RID: 393
		private int[] horizcode;

		// Token: 0x0400018A RID: 394
		private int[] passcode;

		// Token: 0x0400018B RID: 395
		private int[][] vcodes;

		// Token: 0x0400018C RID: 396
		private int[] msbmask;
	}
}
