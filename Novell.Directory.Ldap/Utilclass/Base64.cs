using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000EB RID: 235
	public class Base64
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x0001ADE0 File Offset: 0x00019DE0
		private Base64()
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001ADF8 File Offset: 0x00019DF8
		public static string encode(string inputString)
		{
			string result;
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				byte[] bytes = encoding.GetBytes(inputString);
				sbyte[] inputBytes = SupportClass.ToSByteArray(bytes);
				result = Base64.encode(inputBytes);
			}
			catch (IOException ex)
			{
				throw new SystemException("US-ASCII String encoding not supported by JVM");
			}
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001AE54 File Offset: 0x00019E54
		[CLSCompliant(false)]
		public static string encode(sbyte[] inputBytes)
		{
			bool flag = false;
			bool flag2 = false;
			int num = inputBytes.Length;
			string result;
			if (num == 0)
			{
				result = new StringBuilder("").ToString();
			}
			else
			{
				int num2;
				if (num % 3 == 0)
				{
					num2 = num / 3;
				}
				else
				{
					num2 = num / 3 + 1;
				}
				if (num % 3 == 1)
				{
					flag2 = true;
				}
				else if (num % 3 == 2)
				{
					flag = true;
				}
				char[] array = new char[num2 * 4];
				int i = 0;
				int num3 = 0;
				int num4 = 1;
				while (i < num)
				{
					int num5 = 255 & (int)inputBytes[i];
					array[num3] = Base64.emap[num5 >> 2];
					if (num4 == num2 && flag2)
					{
						array[num3 + 1] = Base64.emap[(num5 & 3) << 4];
						array[num3 + 2] = '=';
						array[num3 + 3] = '=';
						break;
					}
					int num6 = 255 & (int)inputBytes[i + 1];
					array[num3 + 1] = Base64.emap[((num5 & 3) << 4) + ((num6 & 240) >> 4)];
					if (num4 == num2 && flag)
					{
						array[num3 + 2] = Base64.emap[(num6 & 15) << 2];
						array[num3 + 3] = '=';
						break;
					}
					int num7 = 255 & (int)inputBytes[i + 2];
					array[num3 + 2] = Base64.emap[(num6 & 15) << 2 | (num7 & 192) >> 6];
					array[num3 + 3] = Base64.emap[num7 & 63];
					i += 3;
					num3 += 4;
					num4++;
				}
				result = new string(array);
			}
			return result;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001AFBC File Offset: 0x00019FBC
		[CLSCompliant(false)]
		public static sbyte[] decode(string encodedString)
		{
			char[] encodedChars = new char[encodedString.Length];
			SupportClass.GetCharsFromString(encodedString, 0, encodedString.Length, ref encodedChars, 0);
			return Base64.decode(encodedChars);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001AFF0 File Offset: 0x00019FF0
		[CLSCompliant(false)]
		public static sbyte[] decode(char[] encodedChars)
		{
			int num = encodedChars.Length;
			int num2 = num / 4;
			bool flag = false;
			bool flag2 = false;
			sbyte[] result;
			if (encodedChars.Length == 0)
			{
				result = new sbyte[0];
			}
			else
			{
				if (num % 4 != 0)
				{
					throw new SystemException("Novell.Directory.Ldap.ldif_dsml.Base64Decoder: decode: mal-formatted encode value");
				}
				sbyte[] array;
				if (encodedChars[num - 1] == '=' && encodedChars[num - 2] == '=')
				{
					flag2 = true;
					int num3 = num2 * 3 - 2;
					array = new sbyte[num3];
				}
				else if (encodedChars[num - 1] == '=')
				{
					flag = true;
					int num3 = num2 * 3 - 1;
					array = new sbyte[num3];
				}
				else
				{
					int num3 = num2 * 3;
					array = new sbyte[num3];
				}
				int i = 0;
				int num4 = 0;
				int num5 = 1;
				while (i < num)
				{
					array[num4] = (sbyte)((int)Base64.dmap[(int)encodedChars[i]] << 2 | (Base64.dmap[(int)encodedChars[i + 1]] & 48) >> 4);
					if (num5 == num2 && flag2)
					{
						break;
					}
					array[num4 + 1] = (sbyte)((int)(Base64.dmap[(int)encodedChars[i + 1]] & 15) << 4 | (Base64.dmap[(int)encodedChars[i + 2]] & 60) >> 2);
					if (num5 == num2 && flag)
					{
						break;
					}
					array[num4 + 2] = (sbyte)((int)(Base64.dmap[(int)encodedChars[i + 2]] & 3) << 6 | (int)(Base64.dmap[(int)encodedChars[i + 3]] & 63));
					i += 4;
					num4 += 3;
					num5++;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001B130 File Offset: 0x0001A130
		[CLSCompliant(false)]
		public static sbyte[] decode(StringBuilder encodedSBuf, int start, int end)
		{
			int num = end - start;
			int num2 = num / 4;
			bool flag = false;
			bool flag2 = false;
			sbyte[] result;
			if (encodedSBuf.Length == 0)
			{
				result = new sbyte[0];
			}
			else
			{
				if (num % 4 != 0)
				{
					throw new SystemException("Novell.Directory.Ldap.ldif_dsml.Base64Decoder: decode error: mal-formatted encode value");
				}
				sbyte[] array;
				if (encodedSBuf[end - 1] == '=' && encodedSBuf[end - 2] == '=')
				{
					flag2 = true;
					int num3 = num2 * 3 - 2;
					array = new sbyte[num3];
				}
				else if (encodedSBuf[end - 1] == '=')
				{
					flag = true;
					int num3 = num2 * 3 - 1;
					array = new sbyte[num3];
				}
				else
				{
					int num3 = num2 * 3;
					array = new sbyte[num3];
				}
				int i = 0;
				int num4 = 0;
				int num5 = 1;
				while (i < num)
				{
					array[num4] = (sbyte)((int)Base64.dmap[(int)encodedSBuf[start + i]] << 2 | (Base64.dmap[(int)encodedSBuf[start + i + 1]] & 48) >> 4);
					if (num5 == num2 && flag2)
					{
						break;
					}
					array[num4 + 1] = (sbyte)((int)(Base64.dmap[(int)encodedSBuf[start + i + 1]] & 15) << 4 | (Base64.dmap[(int)encodedSBuf[start + i + 2]] & 60) >> 2);
					if (num5 == num2 && flag)
					{
						break;
					}
					array[num4 + 2] = (sbyte)((int)(Base64.dmap[(int)encodedSBuf[start + i + 2]] & 3) << 6 | (int)(Base64.dmap[(int)encodedSBuf[start + i + 3]] & 63));
					i += 4;
					num4 += 3;
					num5++;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001B2A4 File Offset: 0x0001A2A4
		[CLSCompliant(false)]
		public static bool isLDIFSafe(sbyte[] bytes)
		{
			int num = bytes.Length;
			if (num > 0)
			{
				int num2 = (int)bytes[0];
				if (num2 == 0 || num2 == 10 || num2 == 13 || num2 == 32 || num2 == 58 || num2 == 60 || num2 < 0)
				{
					return false;
				}
				if (bytes[num - 1] == 32)
				{
					return false;
				}
				if (num > 1)
				{
					for (int i = 1; i < bytes.Length; i++)
					{
						num2 = (int)bytes[i];
						if (num2 == 0 || num2 == 10 || num2 == 13 || num2 < 0)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001B320 File Offset: 0x0001A320
		public static bool isLDIFSafe(string str)
		{
			bool result;
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				byte[] bytes = encoding.GetBytes(str);
				sbyte[] bytes2 = SupportClass.ToSByteArray(bytes);
				result = Base64.isLDIFSafe(bytes2);
			}
			catch (IOException ex)
			{
				throw new SystemException("UTF-8 String encoding not supported by JVM");
			}
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001B37C File Offset: 0x0001A37C
		private static int getByteCount(sbyte b)
		{
			int result;
			if (b > 0)
			{
				result = 0;
			}
			else if (((int)b & 224) == 192)
			{
				result = 1;
			}
			else if (((int)b & 240) == 224)
			{
				result = 2;
			}
			else if (((int)b & 248) == 240)
			{
				result = 3;
			}
			else if (((int)b & 252) == 248)
			{
				result = 4;
			}
			else if (((int)b & 255) == 252)
			{
				result = 5;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001B3F0 File Offset: 0x0001A3F0
		[CLSCompliant(false)]
		public static bool isValidUTF8(sbyte[] array, bool isUCS2Only)
		{
			for (int i = 0; i < array.Length; i++)
			{
				int byteCount = Base64.getByteCount(array[i]);
				if (byteCount != 0)
				{
					bool result;
					if (byteCount == -1 || i + byteCount >= array.Length || (isUCS2Only && byteCount >= 3))
					{
						result = false;
					}
					else
					{
						if ((Base64.lowerBoundMask[byteCount][0] & array[i]) != 0 || (Base64.lowerBoundMask[byteCount][1] & array[i + 1]) != 0)
						{
							for (int j = 1; j <= byteCount; j++)
							{
								if ((array[i + j] & Base64.continuationMask) != Base64.continuationResult)
								{
									return false;
								}
							}
							i += byteCount + 1;
							continue;
						}
						result = false;
					}
					return result;
				}
			}
			return true;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001B588 File Offset: 0x0001A588
		// Note: this type is marked as 'beforefieldinit'.
		static Base64()
		{
			sbyte[][] array = new sbyte[6][];
			sbyte[][] array2 = array;
			int num = 0;
			sbyte[] array3 = new sbyte[2];
			array2[num] = array3;
			sbyte[][] array4 = array;
			int num2 = 1;
			array3 = new sbyte[2];
			array3[0] = 30;
			array4[num2] = array3;
			array[2] = new sbyte[]
			{
				15,
				32
			};
			array[3] = new sbyte[]
			{
				7,
				48
			};
			array[4] = new sbyte[]
			{
				2,
				56
			};
			array[5] = new sbyte[]
			{
				1,
				60
			};
			Base64.lowerBoundMask = array;
			Base64.continuationMask = (sbyte)SupportClass.Identity(192L);
			Base64.continuationResult = (sbyte)SupportClass.Identity(128L);
		}

		// Token: 0x0400042B RID: 1067
		private static readonly char[] emap = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'+',
			'/'
		};

		// Token: 0x0400042C RID: 1068
		private static readonly sbyte[] dmap = new sbyte[]
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
			62,
			0,
			0,
			0,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			0,
			0,
			0,
			0,
			0,
			0,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400042D RID: 1069
		private static readonly sbyte[][] lowerBoundMask;

		// Token: 0x0400042E RID: 1070
		private static sbyte continuationMask;

		// Token: 0x0400042F RID: 1071
		private static sbyte continuationResult;
	}
}
