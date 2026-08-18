using System;
using \u0005;

namespace OracleInternal.Secure.Network
{
	// Token: 0x02000348 RID: 840
	public class DES168 : EncryptionAlgorithm
	{
		// Token: 0x06001D90 RID: 7568 RVA: 0x00122420 File Offset: 0x00120620
		public override void init(byte[] key, byte[] iv)
		{
			if (key == null && iv == null)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(528));
			}
			if (key.Length < 24)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(528));
			}
			Array.Copy(key, 0, this.\u0006, 0, 8);
			Array.Copy(key, 8, this.\u0007, 0, 8);
			Array.Copy(key, 16, this.\u0008, 0, 8);
			this.\u0015 = true;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00122490 File Offset: 0x00120690
		public override void setSessionKey(byte[] key, byte[] iv)
		{
			this.\u0015 = true;
			if (key == null && iv == null)
			{
				if (this.\u0006 == null)
				{
					throw new Exception(global::\u0005.\u0001.\u0001(528));
				}
				return;
			}
			else
			{
				if (key.Length < 24)
				{
					throw new Exception(global::\u0005.\u0001.\u0001(528));
				}
				Array.Copy(key, 0, this.\u0006, 0, 8);
				Array.Copy(key, 8, this.\u0007, 0, 8);
				Array.Copy(key, 16, this.\u0008, 0, 8);
				return;
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00122508 File Offset: 0x00120708
		public override byte[] decrypt(byte[] ebuffer)
		{
			return this.decrypt(ebuffer, ebuffer.Length);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00122514 File Offset: 0x00120714
		public override byte[] decrypt(byte[] ebuffer, int length)
		{
			byte b = ebuffer[length - 1];
			if (b < 0 || b > 8)
			{
				return null;
			}
			int num = length - (int)b;
			byte[] array = new byte[length - 1];
			int num2 = length - 1;
			if (this.\u0015)
			{
				this.\u0001(this.\u0008, this.\u0007, this.\u0006, 1);
			}
			for (int i = 0; i < num2; i += 8)
			{
				byte[] array2 = new byte[8];
				Array.Copy(ebuffer, i, array2, 0, 8);
				byte[] sourceArray = this.\u0001(array2, 1);
				Array.Copy(sourceArray, 0, array, i, 8);
			}
			byte[] array3 = new byte[num];
			Array.Copy(array, 0, array3, 0, num);
			return array3;
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x001225B8 File Offset: 0x001207B8
		public override byte[] encrypt(byte[] buffer)
		{
			return this.encrypt(buffer, buffer.Length);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x001225C4 File Offset: 0x001207C4
		public override byte[] encrypt(byte[] buffer, int len)
		{
			byte b = (byte)((len % 8 == 0) ? 0 : (8 - len % 8));
			int num = len + (int)b;
			byte[] array = new byte[num + 1];
			if (this.\u0015)
			{
				this.\u0001(this.\u0006, this.\u0007, this.\u0008, 0);
			}
			for (int i = 0; i < len; i += 8)
			{
				byte[] array2 = new byte[8];
				if (i <= len - 8)
				{
					Array.Copy(buffer, i, array2, 0, 8);
				}
				else
				{
					Array.Copy(buffer, i, array2, 0, len & 7);
				}
				byte[] sourceArray = this.\u0001(array2, 0);
				Array.Copy(sourceArray, 0, array, i, 8);
			}
			array[num] = b + 1;
			return array;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0012266C File Offset: 0x0012086C
		public override int maxDelta()
		{
			return 8;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x00122670 File Offset: 0x00120870
		internal new void \u0001(byte[] \u0002, byte[] \u0003, byte[] \u0004, byte \u0005)
		{
			this.\u000E = this.\u0001(\u0002, \u0005);
			this.\u000F = this.\u0001(\u0003, (\u0005 == 0) ? 1 : 0);
			this.\u0010 = this.\u0001(\u0004, \u0005);
			this.\u0011 = this.\u0001(\u0002, \u0005);
			this.\u0012 = this.\u0001(\u0003, (\u0005 == 0) ? 1 : 0);
			this.\u0013 = this.\u0001(\u0004, \u0005);
			Array.Copy(DES168.\u001C, 0, this.\u0014, 0, 8);
			this.\u0015 = false;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00122700 File Offset: 0x00120900
		internal new uint[] \u0001(byte[] \u0002, byte \u0003)
		{
			byte[] array = new byte[56];
			byte[] array2 = new byte[56];
			uint[] array3 = new uint[32];
			for (int i = 0; i < 56; i++)
			{
				int num = (int)DES168.\u0019[i];
				int num2 = num & 7;
				array[i] = ((((uint)\u0002[num >> 3] & DES168.\u0017[16 + num2]) != 0U) ? 1 : 0);
			}
			for (int j = 0; j < 16; j++)
			{
				int num2;
				if (\u0003 == 1)
				{
					num2 = 15 - j << 1;
				}
				else
				{
					num2 = j << 1;
				}
				int num3 = num2 + 1;
				array3[num2] = (array3[num3] = 0U);
				for (int i = 0; i < 28; i++)
				{
					int num = i + (int)DES168.\u001A[j];
					if (num < 28)
					{
						array2[i] = array[num];
					}
					else
					{
						array2[i] = array[num - 28];
					}
				}
				for (int i = 28; i < 56; i++)
				{
					int num = i + (int)DES168.\u001A[j];
					if (num < 56)
					{
						array2[i] = array[num];
					}
					else
					{
						array2[i] = array[num - 28];
					}
				}
				for (int i = 0; i < 24; i++)
				{
					if (array2[(int)DES168.\u001B[i]] != 0)
					{
						array3[num2] |= DES168.\u0018[i];
					}
					if (array2[(int)DES168.\u001B[i + 24]] != 0)
					{
						array3[num3] |= DES168.\u0018[i];
					}
				}
			}
			return this.\u0001(array3);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00122860 File Offset: 0x00120A60
		private new uint[] \u0001(uint[] \u0002)
		{
			uint[] array = new uint[32];
			uint[] array2 = array;
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 16)
			{
				int num3 = num++;
				array2[num2] = (\u0002[num3] & 16515072U) << 6;
				array2[num2] |= (\u0002[num3] & 4032U) << 10;
				array2[num2] |= (\u0002[num] & 16515072U) >> 10;
				array2[num2] |= (\u0002[num] & 4032U) >> 6;
				num2++;
				array2[num2] = (\u0002[num3] & 258048U) << 12;
				array2[num2] |= (\u0002[num3] & 63U) << 16;
				array2[num2] |= (\u0002[num] & 258048U) >> 4;
				array2[num2] |= (\u0002[num] & 63U);
				num2++;
				i++;
				num++;
			}
			return array2;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00122990 File Offset: 0x00120B90
		private new byte[] \u0001(byte[] \u0002, byte \u0003)
		{
			byte[] array = new byte[8];
			if (\u0003 == 0)
			{
				this.\u0001(array, this.\u0014, \u0002, 2);
				this.\u0001(array, this.\u000E);
				this.\u0001(array, this.\u000F);
				this.\u0001(array, this.\u0010);
				Array.Copy(array, 0, this.\u0014, 0, 8);
			}
			else
			{
				byte[] array2 = new byte[8];
				Array.Copy(\u0002, 0, array2, 0, 8);
				this.\u0001(\u0002, this.\u0011);
				this.\u0001(\u0002, this.\u0012);
				this.\u0001(\u0002, this.\u0013);
				this.\u0001(array, this.\u0014, \u0002, 2);
				Array.Copy(array2, 0, this.\u0014, 0, 8);
			}
			return array;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00122A48 File Offset: 0x00120C48
		internal new void \u0001(byte[] \u0002, byte[] \u0003, byte[] \u0004, int \u0005)
		{
			if (\u0005 == 1)
			{
				for (int i = 0; i < 8; i++)
				{
					\u0002[i] = (\u0003[i] & \u0004[i]);
				}
				return;
			}
			if (\u0005 == 2)
			{
				for (int i = 0; i < 8; i++)
				{
					\u0002[i] = (\u0003[i] ^ \u0004[i]);
				}
			}
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00122A90 File Offset: 0x00120C90
		internal new void \u0001(byte[] \u0002, uint[] \u0003)
		{
			uint[] array = new uint[2];
			this.\u0002(\u0002, array);
			this.\u0001(array, \u0003);
			this.\u0001(array, \u0002);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00122ABC File Offset: 0x00120CBC
		internal void \u0002(byte[] \u0002, uint[] \u0003)
		{
			int num = 0;
			\u0003[0] = (uint)((uint)(\u0002[num] & byte.MaxValue) << 24);
			num++;
			\u0003[0] |= (uint)((uint)(\u0002[num] & byte.MaxValue) << 16);
			num++;
			\u0003[0] |= (uint)((uint)(\u0002[num] & byte.MaxValue) << 8);
			num++;
			\u0003[0] |= (uint)(\u0002[num] & byte.MaxValue);
			num++;
			\u0003[1] = (uint)((uint)(\u0002[num] & byte.MaxValue) << 24);
			num++;
			\u0003[1] |= (uint)((uint)(\u0002[num] & byte.MaxValue) << 16);
			num++;
			\u0003[1] |= (uint)((uint)(\u0002[num] & byte.MaxValue) << 8);
			num++;
			\u0003[1] |= (uint)(\u0002[num] & byte.MaxValue);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00122BB8 File Offset: 0x00120DB8
		internal new void \u0001(uint[] \u0002, byte[] \u0003)
		{
			int num = 0;
			\u0003[num] = (byte)(\u0002[0] >> 24 & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[0] >> 16 & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[0] >> 8 & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[0] & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[1] >> 24 & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[1] >> 16 & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[1] >> 8 & 255U);
			num++;
			\u0003[num] = (byte)(\u0002[1] & 255U);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00122C5C File Offset: 0x00120E5C
		internal new void \u0001(uint[] \u0002, uint[] \u0003)
		{
			int num = 0;
			uint num2 = \u0002[0];
			uint num3 = \u0002[1];
			uint num4 = (num2 >> 4 ^ num3) & 252645135U;
			num3 ^= num4;
			num2 ^= num4 << 4;
			num4 = ((num2 >> 16 ^ num3) & 65535U);
			num3 ^= num4;
			num2 ^= num4 << 16;
			num4 = ((num3 >> 2 ^ num2) & 858993459U);
			num2 ^= num4;
			num3 ^= num4 << 2;
			num4 = ((num3 >> 8 ^ num2) & 16711935U);
			num2 ^= num4;
			num3 ^= num4 << 8;
			num3 = ((num3 << 1 | (num3 >> 31 & 1U)) & uint.MaxValue);
			num4 = ((num2 ^ num3) & 2863311530U);
			num2 ^= num4;
			num3 ^= num4;
			num2 = ((num2 << 1 | (num2 >> 31 & 1U)) & uint.MaxValue);
			for (int i = 0; i < 8; i++)
			{
				num4 = (num3 << 28 | num3 >> 4);
				uint num5 = \u0003[num];
				num4 ^= \u0003[num];
				num++;
				uint num6 = DES168.\u0082[(int)((UIntPtr)(num4 & 63U))];
				num6 |= DES168.\u0080[(int)((UIntPtr)(num4 >> 8 & 63U))];
				num6 |= DES168.\u001F[(int)((UIntPtr)(num4 >> 16 & 63U))];
				num6 |= DES168.\u001D[(int)((UIntPtr)(num4 >> 24 & 63U))];
				num4 = (num3 ^ \u0003[num]);
				num++;
				num6 |= DES168.\u0083[(int)((UIntPtr)(num4 & 63U))];
				num6 |= DES168.\u0081[(int)((UIntPtr)(num4 >> 8 & 63U))];
				num6 |= DES168.\u007F[(int)((UIntPtr)(num4 >> 16 & 63U))];
				num6 |= DES168.\u001E[(int)((UIntPtr)(num4 >> 24 & 63U))];
				num2 ^= num6;
				num4 = (num2 << 28 | num2 >> 4);
				num4 ^= \u0003[num];
				num++;
				num6 = DES168.\u0082[(int)((UIntPtr)(num4 & 63U))];
				num6 |= DES168.\u0080[(int)((UIntPtr)(num4 >> 8 & 63U))];
				num6 |= DES168.\u001F[(int)((UIntPtr)(num4 >> 16 & 63U))];
				num6 |= DES168.\u001D[(int)((UIntPtr)(num4 >> 24 & 63U))];
				num4 = (num2 ^ \u0003[num]);
				num++;
				num6 |= DES168.\u0083[(int)((UIntPtr)(num4 & 63U))];
				num6 |= DES168.\u0081[(int)((UIntPtr)(num4 >> 8 & 63U))];
				num6 |= DES168.\u007F[(int)((UIntPtr)(num4 >> 16 & 63U))];
				num6 |= DES168.\u001E[(int)((UIntPtr)(num4 >> 24 & 63U))];
				num3 ^= num6;
			}
			num3 = (num3 << 31 | num3 >> 1);
			num4 = ((num2 ^ num3) & 2863311530U);
			num2 ^= num4;
			num3 ^= num4;
			num2 = (num2 << 31 | num2 >> 1);
			num4 = ((num2 >> 8 ^ num3) & 16711935U);
			num3 ^= num4;
			num2 ^= num4 << 8;
			num4 = ((num2 >> 2 ^ num3) & 858993459U);
			num3 ^= num4;
			num2 ^= num4 << 2;
			num4 = ((num3 >> 16 ^ num2) & 65535U);
			num2 ^= num4;
			num3 ^= num4 << 16;
			num4 = ((num3 >> 4 ^ num2) & 252645135U);
			num2 ^= num4;
			num3 ^= num4 << 4;
			\u0002[0] = num3;
			\u0002[1] = num2;
		}

		// Token: 0x04001FF2 RID: 8178
		internal new const int \u0001 = 8;

		// Token: 0x04001FF3 RID: 8179
		internal const int \u0002 = 1;

		// Token: 0x04001FF4 RID: 8180
		internal const int \u0003 = 2;

		// Token: 0x04001FF5 RID: 8181
		internal const byte \u0004 = 0;

		// Token: 0x04001FF6 RID: 8182
		internal const byte \u0005 = 1;

		// Token: 0x04001FF7 RID: 8183
		internal byte[] \u0006 = new byte[8];

		// Token: 0x04001FF8 RID: 8184
		internal byte[] \u0007 = new byte[8];

		// Token: 0x04001FF9 RID: 8185
		internal byte[] \u0008 = new byte[8];

		// Token: 0x04001FFA RID: 8186
		private uint[] \u000E;

		// Token: 0x04001FFB RID: 8187
		private uint[] \u000F;

		// Token: 0x04001FFC RID: 8188
		private uint[] \u0010;

		// Token: 0x04001FFD RID: 8189
		private uint[] \u0011;

		// Token: 0x04001FFE RID: 8190
		private uint[] \u0012;

		// Token: 0x04001FFF RID: 8191
		private uint[] \u0013;

		// Token: 0x04002000 RID: 8192
		private byte[] \u0014 = new byte[8];

		// Token: 0x04002001 RID: 8193
		internal bool \u0015;

		// Token: 0x04002002 RID: 8194
		private static short[] \u0016 = new short[]
		{
			200,
			100,
			40,
			20,
			10,
			4,
			2,
			1
		};

		// Token: 0x04002003 RID: 8195
		private static uint[] \u0017 = new uint[]
		{
			8388608U,
			4194304U,
			2097152U,
			1048576U,
			524288U,
			262144U,
			131072U,
			65536U,
			32768U,
			16384U,
			8192U,
			4096U,
			2048U,
			1024U,
			512U,
			256U,
			128U,
			64U,
			32U,
			16U,
			8U,
			4U,
			2U,
			1U
		};

		// Token: 0x04002004 RID: 8196
		private static uint[] \u0018 = new uint[]
		{
			8388608U,
			4194304U,
			2097152U,
			1048576U,
			524288U,
			262144U,
			131072U,
			65536U,
			32768U,
			16384U,
			8192U,
			4096U,
			2048U,
			1024U,
			512U,
			256U,
			128U,
			64U,
			32U,
			16U,
			8U,
			4U,
			2U,
			1U
		};

		// Token: 0x04002005 RID: 8197
		private static byte[] \u0019 = new byte[]
		{
			56,
			48,
			40,
			32,
			24,
			16,
			8,
			0,
			57,
			49,
			41,
			33,
			25,
			17,
			9,
			1,
			58,
			50,
			42,
			34,
			26,
			18,
			10,
			2,
			59,
			51,
			43,
			35,
			62,
			54,
			46,
			38,
			30,
			22,
			14,
			6,
			61,
			53,
			45,
			37,
			29,
			21,
			13,
			5,
			60,
			52,
			44,
			36,
			28,
			20,
			12,
			4,
			27,
			19,
			11,
			3
		};

		// Token: 0x04002006 RID: 8198
		private static byte[] \u001A = new byte[]
		{
			1,
			2,
			4,
			6,
			8,
			10,
			12,
			14,
			15,
			17,
			19,
			21,
			23,
			25,
			27,
			28
		};

		// Token: 0x04002007 RID: 8199
		private static byte[] \u001B = new byte[]
		{
			13,
			16,
			10,
			23,
			0,
			4,
			2,
			27,
			14,
			5,
			20,
			9,
			22,
			18,
			11,
			3,
			25,
			7,
			15,
			6,
			26,
			19,
			12,
			1,
			40,
			51,
			30,
			36,
			46,
			54,
			29,
			39,
			50,
			44,
			32,
			47,
			43,
			48,
			38,
			55,
			33,
			52,
			45,
			41,
			49,
			35,
			28,
			31
		};

		// Token: 0x04002008 RID: 8200
		private static byte[] \u001C = new byte[]
		{
			1,
			35,
			69,
			103,
			137,
			171,
			205,
			239
		};

		// Token: 0x04002009 RID: 8201
		private static uint[] \u001D = new uint[]
		{
			16843776U,
			0U,
			65536U,
			16843780U,
			16842756U,
			66564U,
			4U,
			65536U,
			1024U,
			16843776U,
			16843780U,
			1024U,
			16778244U,
			16842756U,
			16777216U,
			4U,
			1028U,
			16778240U,
			16778240U,
			66560U,
			66560U,
			16842752U,
			16842752U,
			16778244U,
			65540U,
			16777220U,
			16777220U,
			65540U,
			0U,
			1028U,
			66564U,
			16777216U,
			65536U,
			16843780U,
			4U,
			16842752U,
			16843776U,
			16777216U,
			16777216U,
			1024U,
			16842756U,
			65536U,
			66560U,
			16777220U,
			1024U,
			4U,
			16778244U,
			66564U,
			16843780U,
			65540U,
			16842752U,
			16778244U,
			16777220U,
			1028U,
			66564U,
			16843776U,
			1028U,
			16778240U,
			16778240U,
			0U,
			65540U,
			66560U,
			0U,
			16842756U
		};

		// Token: 0x0400200A RID: 8202
		private static uint[] \u001E = new uint[]
		{
			2148565024U,
			2147516416U,
			32768U,
			1081376U,
			1048576U,
			32U,
			2148532256U,
			2147516448U,
			2147483680U,
			2148565024U,
			2148564992U,
			2147483648U,
			2147516416U,
			1048576U,
			32U,
			2148532256U,
			1081344U,
			1048608U,
			2147516448U,
			0U,
			2147483648U,
			32768U,
			1081376U,
			2148532224U,
			1048608U,
			2147483680U,
			0U,
			1081344U,
			32800U,
			2148564992U,
			2148532224U,
			32800U,
			0U,
			1081376U,
			2148532256U,
			1048576U,
			2147516448U,
			2148532224U,
			2148564992U,
			32768U,
			2148532224U,
			2147516416U,
			32U,
			2148565024U,
			1081376U,
			32U,
			32768U,
			2147483648U,
			32800U,
			2148564992U,
			1048576U,
			2147483680U,
			1048608U,
			2147516448U,
			2147483680U,
			1048608U,
			1081344U,
			0U,
			2147516416U,
			32800U,
			2147483648U,
			2148532256U,
			2148565024U,
			1081344U
		};

		// Token: 0x0400200B RID: 8203
		private static uint[] \u001F = new uint[]
		{
			520U,
			134349312U,
			0U,
			134348808U,
			134218240U,
			0U,
			131592U,
			134218240U,
			131080U,
			134217736U,
			134217736U,
			131072U,
			134349320U,
			131080U,
			134348800U,
			520U,
			134217728U,
			8U,
			134349312U,
			512U,
			131584U,
			134348800U,
			134348808U,
			131592U,
			134218248U,
			131584U,
			131072U,
			134218248U,
			8U,
			134349320U,
			512U,
			134217728U,
			134349312U,
			134217728U,
			131080U,
			520U,
			131072U,
			134349312U,
			134218240U,
			0U,
			512U,
			131080U,
			134349320U,
			134218240U,
			134217736U,
			512U,
			0U,
			134348808U,
			134218248U,
			131072U,
			134217728U,
			134349320U,
			8U,
			131592U,
			131584U,
			134217736U,
			134348800U,
			134218248U,
			520U,
			134348800U,
			131592U,
			8U,
			134348808U,
			131584U
		};

		// Token: 0x0400200C RID: 8204
		private static uint[] \u007F = new uint[]
		{
			8396801U,
			8321U,
			8321U,
			128U,
			8396928U,
			8388737U,
			8388609U,
			8193U,
			0U,
			8396800U,
			8396800U,
			8396929U,
			129U,
			0U,
			8388736U,
			8388609U,
			1U,
			8192U,
			8388608U,
			8396801U,
			128U,
			8388608U,
			8193U,
			8320U,
			8388737U,
			1U,
			8320U,
			8388736U,
			8192U,
			8396928U,
			8396929U,
			129U,
			8388736U,
			8388609U,
			8396800U,
			8396929U,
			129U,
			0U,
			0U,
			8396800U,
			8320U,
			8388736U,
			8388737U,
			1U,
			8396801U,
			8321U,
			8321U,
			128U,
			8396929U,
			129U,
			1U,
			8192U,
			8388609U,
			8193U,
			8396928U,
			8388737U,
			8193U,
			8320U,
			8388608U,
			8396801U,
			128U,
			8388608U,
			8192U,
			8396928U
		};

		// Token: 0x0400200D RID: 8205
		private static uint[] \u0080 = new uint[]
		{
			256U,
			34078976U,
			34078720U,
			1107296512U,
			524288U,
			256U,
			1073741824U,
			34078720U,
			1074266368U,
			524288U,
			33554688U,
			1074266368U,
			1107296512U,
			1107820544U,
			524544U,
			1073741824U,
			33554432U,
			1074266112U,
			1074266112U,
			0U,
			1073742080U,
			1107820800U,
			1107820800U,
			33554688U,
			1107820544U,
			1073742080U,
			0U,
			1107296256U,
			34078976U,
			33554432U,
			1107296256U,
			524544U,
			524288U,
			1107296512U,
			256U,
			33554432U,
			1073741824U,
			34078720U,
			1107296512U,
			1074266368U,
			33554688U,
			1073741824U,
			1107820544U,
			34078976U,
			1074266368U,
			256U,
			33554432U,
			1107820544U,
			1107820800U,
			524544U,
			1107296256U,
			1107820800U,
			34078720U,
			0U,
			1074266112U,
			1107296256U,
			524544U,
			33554688U,
			1073742080U,
			524288U,
			0U,
			1074266112U,
			34078976U,
			1073742080U
		};

		// Token: 0x0400200E RID: 8206
		private static uint[] \u0081 = new uint[]
		{
			536870928U,
			541065216U,
			16384U,
			541081616U,
			541065216U,
			16U,
			541081616U,
			4194304U,
			536887296U,
			4210704U,
			4194304U,
			536870928U,
			4194320U,
			536887296U,
			536870912U,
			16400U,
			0U,
			4194320U,
			536887312U,
			16384U,
			4210688U,
			536887312U,
			16U,
			541065232U,
			541065232U,
			0U,
			4210704U,
			541081600U,
			16400U,
			4210688U,
			541081600U,
			536870912U,
			536887296U,
			16U,
			541065232U,
			4210688U,
			541081616U,
			4194304U,
			16400U,
			536870928U,
			4194304U,
			536887296U,
			536870912U,
			16400U,
			536870928U,
			541081616U,
			4210688U,
			541065216U,
			4210704U,
			541081600U,
			0U,
			541065232U,
			16U,
			16384U,
			541065216U,
			4210704U,
			16384U,
			4194320U,
			536887312U,
			0U,
			541081600U,
			536870912U,
			4194320U,
			536887312U
		};

		// Token: 0x0400200F RID: 8207
		private static uint[] \u0082 = new uint[]
		{
			2097152U,
			69206018U,
			67110914U,
			0U,
			2048U,
			67110914U,
			2099202U,
			69208064U,
			69208066U,
			2097152U,
			0U,
			67108866U,
			2U,
			67108864U,
			69206018U,
			2050U,
			67110912U,
			2099202U,
			2097154U,
			67110912U,
			67108866U,
			69206016U,
			69208064U,
			2097154U,
			69206016U,
			2048U,
			2050U,
			69208066U,
			2099200U,
			2U,
			67108864U,
			2099200U,
			67108864U,
			2099200U,
			2097152U,
			67110914U,
			67110914U,
			69206018U,
			69206018U,
			2U,
			2097154U,
			67108864U,
			67110912U,
			2097152U,
			69208064U,
			2050U,
			2099202U,
			69208064U,
			2050U,
			67108866U,
			69208066U,
			69206016U,
			2099200U,
			0U,
			2U,
			69208066U,
			0U,
			2099202U,
			69206016U,
			2048U,
			67108866U,
			67110912U,
			2048U,
			2097154U
		};

		// Token: 0x04002010 RID: 8208
		private static uint[] \u0083 = new uint[]
		{
			268439616U,
			4096U,
			262144U,
			268701760U,
			268435456U,
			268439616U,
			64U,
			268435456U,
			262208U,
			268697600U,
			268701760U,
			266240U,
			268701696U,
			266304U,
			4096U,
			64U,
			268697600U,
			268435520U,
			268439552U,
			4160U,
			266240U,
			262208U,
			268697664U,
			268701696U,
			4160U,
			0U,
			0U,
			268697664U,
			268435520U,
			268439552U,
			266304U,
			262144U,
			266304U,
			262144U,
			268701696U,
			4096U,
			64U,
			268697664U,
			4096U,
			266304U,
			268439552U,
			64U,
			268435520U,
			268697600U,
			268697664U,
			268435456U,
			262144U,
			268439616U,
			0U,
			268701760U,
			262208U,
			268435520U,
			268697600U,
			268439552U,
			268439616U,
			0U,
			268701760U,
			266240U,
			266240U,
			4160U,
			4160U,
			262208U,
			268435456U,
			268701696U
		};
	}
}
