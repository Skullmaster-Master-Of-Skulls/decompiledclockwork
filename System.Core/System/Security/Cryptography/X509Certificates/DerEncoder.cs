using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000122 RID: 290
	internal static class DerEncoder
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x00021218 File Offset: 0x0001F418
		private static byte[] EncodeLength(int length)
		{
			byte b = (byte)length;
			if (length < 128)
			{
				return new byte[]
				{
					b
				};
			}
			if (length <= 255)
			{
				return new byte[]
				{
					129,
					b
				};
			}
			int num = length >> 8;
			byte b2 = (byte)num;
			if (length <= 65535)
			{
				return new byte[]
				{
					130,
					b2,
					b
				};
			}
			num >>= 8;
			byte b3 = (byte)num;
			if (length <= 16777215)
			{
				return new byte[]
				{
					131,
					b3,
					b2,
					b
				};
			}
			num >>= 8;
			byte b4 = (byte)num;
			return new byte[]
			{
				132,
				b4,
				b3,
				b2,
				b
			};
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000212D0 File Offset: 0x0001F4D0
		internal static byte[][] SegmentedEncodeBoolean(bool value)
		{
			byte[] array = new byte[]
			{
				value ? byte.MaxValue : 0
			};
			return new byte[][]
			{
				new byte[]
				{
					1
				},
				new byte[]
				{
					1
				},
				array
			};
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00021318 File Offset: 0x0001F518
		internal static byte[][] SegmentedEncodeUnsignedInteger(uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			return DerEncoder.SegmentedEncodeUnsignedInteger(bytes);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002133F File Offset: 0x0001F53F
		internal static byte[][] SegmentedEncodeUnsignedInteger(byte[] bigEndianBytes)
		{
			return DerEncoder.SegmentedEncodeUnsignedInteger(bigEndianBytes, 0, bigEndianBytes.Length);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002134C File Offset: 0x0001F54C
		internal static byte[][] SegmentedEncodeUnsignedInteger(byte[] bigEndianBytes, int offset, int count)
		{
			int num = offset;
			int num2 = num + count;
			while (num < num2 && bigEndianBytes[num] == 0)
			{
				num++;
			}
			if (num == num2)
			{
				num--;
			}
			int num3 = num2 - num;
			int dstOffset = 0;
			byte[] array;
			if (bigEndianBytes[num] > 127)
			{
				array = new byte[num3 + 1];
				dstOffset = 1;
			}
			else
			{
				array = new byte[num3];
			}
			Buffer.BlockCopy(bigEndianBytes, num, array, dstOffset, num3);
			return new byte[][]
			{
				new byte[]
				{
					2
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000213C6 File Offset: 0x0001F5C6
		internal static byte[][] SegmentedEncodeBitString(params byte[][][] childSegments)
		{
			return DerEncoder.SegmentedEncodeBitString(DerEncoder.ConcatenateArrays(childSegments));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000213D3 File Offset: 0x0001F5D3
		internal static byte[][] SegmentedEncodeBitString(byte[] data)
		{
			return DerEncoder.SegmentedEncodeBitString(0, data);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000213DC File Offset: 0x0001F5DC
		internal static byte[][] SegmentedEncodeBitString(int unusedBits, byte[] data)
		{
			byte[] array = new byte[data.Length + 1];
			Buffer.BlockCopy(data, 0, array, 1, data.Length);
			array[0] = (byte)unusedBits;
			byte b = (byte)(-1 << unusedBits);
			byte[] array2 = array;
			int num = data.Length;
			array2[num] &= b;
			return new byte[][]
			{
				new byte[]
				{
					3
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00021440 File Offset: 0x0001F640
		internal static byte[][] SegmentedEncodeNamedBitList(byte[] bigEndianBytes, int namedBitsCount)
		{
			int num = -1;
			int val = bigEndianBytes.Length * 8 - 1;
			int num2 = Math.Min(val, namedBitsCount - 1);
			for (int i = num2; i >= 0; i--)
			{
				int num3 = i / 8;
				int num4 = 7 - i % 8;
				int num5 = 1 << num4;
				byte b = bigEndianBytes[num3];
				if (((int)b & num5) == num5)
				{
					num = i;
					break;
				}
			}
			byte[] array;
			if (num >= 0)
			{
				int num6 = num + 1;
				int num7 = (7 + num6) / 8;
				int num8 = 7 - num % 8;
				byte b2 = (byte)(-1 << num8);
				array = new byte[num7 + 1];
				array[0] = (byte)num8;
				Buffer.BlockCopy(bigEndianBytes, 0, array, 1, num7);
				byte[] array2 = array;
				int num9 = num7;
				array2[num9] &= b2;
			}
			else
			{
				array = new byte[1];
			}
			return new byte[][]
			{
				new byte[]
				{
					3
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00021513 File Offset: 0x0001F713
		internal static byte[][] SegmentedEncodeOctetString(byte[] data)
		{
			return new byte[][]
			{
				new byte[]
				{
					4
				},
				DerEncoder.EncodeLength(data.Length),
				data
			};
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00021537 File Offset: 0x0001F737
		internal static byte[][] SegmentedEncodeNull()
		{
			return DerEncoder.s_nullTlv;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002153E File Offset: 0x0001F73E
		internal static byte[] EncodeOid(string oidValue)
		{
			return DerEncoder.ConcatenateArrays(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid(oidValue)
			});
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00021554 File Offset: 0x0001F754
		internal static byte[][] SegmentedEncodeOid(Oid oid)
		{
			string value = oid.Value;
			return DerEncoder.SegmentedEncodeOid(value);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00021570 File Offset: 0x0001F770
		internal static byte[][] SegmentedEncodeOid(string oidValue)
		{
			if (string.IsNullOrEmpty(oidValue))
			{
				throw new CryptographicException(SR.GetString("Argument_InvalidOidValue"));
			}
			if (oidValue.Length < 3)
			{
				throw new CryptographicException(SR.GetString("Argument_InvalidOidValue"));
			}
			if (oidValue[1] != '.')
			{
				throw new CryptographicException(SR.GetString("Argument_InvalidOidValue"));
			}
			int num;
			switch (oidValue[0])
			{
			case '0':
				num = 0;
				break;
			case '1':
				num = 1;
				break;
			case '2':
				num = 2;
				break;
			default:
				throw new CryptographicException(SR.GetString("Argument_InvalidOidValue"));
			}
			int i = 2;
			BigInteger left = DerEncoder.ParseOidRid(oidValue, ref i);
			left += 40 * num;
			List<byte> list = new List<byte>(oidValue.Length / 2);
			DerEncoder.EncodeRid(list, ref left);
			while (i < oidValue.Length)
			{
				left = DerEncoder.ParseOidRid(oidValue, ref i);
				DerEncoder.EncodeRid(list, ref left);
			}
			return new byte[][]
			{
				new byte[]
				{
					6
				},
				DerEncoder.EncodeLength(list.Count),
				list.ToArray()
			};
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002167D File Offset: 0x0001F87D
		internal static byte[][] SegmentedEncodeUtf8String(char[] chars)
		{
			return DerEncoder.SegmentedEncodeUtf8String(chars, 0, chars.Length);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002168C File Offset: 0x0001F88C
		internal static byte[][] SegmentedEncodeUtf8String(char[] chars, int offset, int count)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(chars, offset, count);
			return new byte[][]
			{
				new byte[]
				{
					12
				},
				DerEncoder.EncodeLength(bytes.Length),
				bytes
			};
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000216CA File Offset: 0x0001F8CA
		internal static byte[][] ConstructSegmentedSequence(params byte[][][] items)
		{
			return DerEncoder.ConstructSegmentedSequence(items);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x000216D4 File Offset: 0x0001F8D4
		internal static byte[][] ConstructSegmentedSequence(IEnumerable<byte[][]> items)
		{
			byte[] array = DerEncoder.ConcatenateArrays(items);
			return new byte[][]
			{
				new byte[]
				{
					48
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002170C File Offset: 0x0001F90C
		internal static byte[][] ConstructSegmentedContextSpecificValue(int contextId, params byte[][][] items)
		{
			byte[] array = DerEncoder.ConcatenateArrays(items);
			byte b = (byte)(160 | contextId);
			return new byte[][]
			{
				new byte[]
				{
					b
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002174C File Offset: 0x0001F94C
		internal static byte[][] ConstructSegmentedSet(params byte[][][] items)
		{
			byte[][][] array = (byte[][][])items.Clone();
			Array.Sort<byte[][]>(array, DerEncoder.AsnSetValueComparer.Instance);
			byte[] array2 = DerEncoder.ConcatenateArrays(array);
			return new byte[][]
			{
				new byte[]
				{
					49
				},
				DerEncoder.EncodeLength(array2.Length),
				array2
			};
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002179C File Offset: 0x0001F99C
		internal static byte[][] ConstructSegmentedPresortedSet(params byte[][][] items)
		{
			byte[] array = DerEncoder.ConcatenateArrays(items);
			return new byte[][]
			{
				new byte[]
				{
					49
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000217D3 File Offset: 0x0001F9D3
		internal static bool IsValidPrintableString(char[] chars)
		{
			return DerEncoder.IsValidPrintableString(chars, 0, chars.Length);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000217E0 File Offset: 0x0001F9E0
		internal static bool IsValidPrintableString(char[] chars, int offset, int count)
		{
			int num = count + offset;
			for (int i = offset; i < num; i++)
			{
				if (!DerEncoder.IsPrintableStringCharacter(chars[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002180A File Offset: 0x0001FA0A
		internal static byte[][] SegmentedEncodePrintableString(char[] chars)
		{
			return DerEncoder.SegmentedEncodePrintableString(chars, 0, chars.Length);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00021818 File Offset: 0x0001FA18
		internal static byte[][] SegmentedEncodePrintableString(char[] chars, int offset, int count)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (byte)chars[i + offset];
			}
			return new byte[][]
			{
				new byte[]
				{
					19
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00021864 File Offset: 0x0001FA64
		internal static byte[][] SegmentedEncodeIA5String(char[] chars)
		{
			return DerEncoder.SegmentedEncodeIA5String(chars, 0, chars.Length);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00021870 File Offset: 0x0001FA70
		internal static byte[][] SegmentedEncodeIA5String(char[] chars, int offset, int count)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				char c = chars[i + offset];
				if (c > '\u007f')
				{
					throw new CryptographicException(SR.GetString("Cryptography_Invalid_IA5String"));
				}
				array[i] = (byte)c;
			}
			return new byte[][]
			{
				new byte[]
				{
					22
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000218D4 File Offset: 0x0001FAD4
		internal static byte[][] SegmentedEncodeUtcTime(DateTime utcTime)
		{
			byte[] array = new byte[13];
			int num = utcTime.Year;
			int num2 = utcTime.Month;
			int num3 = utcTime.Day;
			int num4 = utcTime.Hour;
			int num5 = utcTime.Minute;
			int num6 = utcTime.Second;
			array[1] = (byte)(48 + num % 10);
			num /= 10;
			array[0] = (byte)(48 + num % 10);
			array[3] = (byte)(48 + num2 % 10);
			num2 /= 10;
			array[2] = (byte)(48 + num2 % 10);
			array[5] = (byte)(48 + num3 % 10);
			num3 /= 10;
			array[4] = (byte)(48 + num3 % 10);
			array[7] = (byte)(48 + num4 % 10);
			num4 /= 10;
			array[6] = (byte)(48 + num4 % 10);
			array[9] = (byte)(48 + num5 % 10);
			num5 /= 10;
			array[8] = (byte)(48 + num5 % 10);
			array[11] = (byte)(48 + num6 % 10);
			num6 /= 10;
			array[10] = (byte)(48 + num6 % 10);
			array[12] = 90;
			return new byte[][]
			{
				new byte[]
				{
					23
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000219F8 File Offset: 0x0001FBF8
		internal static byte[][] SegmentedEncodeGeneralizedTime(DateTime utcTime)
		{
			byte[] array = new byte[15];
			int num = utcTime.Year;
			int num2 = utcTime.Month;
			int num3 = utcTime.Day;
			int num4 = utcTime.Hour;
			int num5 = utcTime.Minute;
			int num6 = utcTime.Second;
			array[3] = (byte)(48 + num % 10);
			num /= 10;
			array[2] = (byte)(48 + num % 10);
			num /= 10;
			array[1] = (byte)(48 + num % 10);
			num /= 10;
			array[0] = (byte)(48 + num % 10);
			array[5] = (byte)(48 + num2 % 10);
			num2 /= 10;
			array[4] = (byte)(48 + num2 % 10);
			array[7] = (byte)(48 + num3 % 10);
			num3 /= 10;
			array[6] = (byte)(48 + num3 % 10);
			array[9] = (byte)(48 + num4 % 10);
			num4 /= 10;
			array[8] = (byte)(48 + num4 % 10);
			array[11] = (byte)(48 + num5 % 10);
			num5 /= 10;
			array[10] = (byte)(48 + num5 % 10);
			array[13] = (byte)(48 + num6 % 10);
			num6 /= 10;
			array[12] = (byte)(48 + num6 % 10);
			array[14] = 90;
			return new byte[][]
			{
				new byte[]
				{
					24
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00021B3C File Offset: 0x0001FD3C
		internal static byte[] ConstructSequence(params byte[][][] items)
		{
			return DerEncoder.ConstructSequence(items);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00021B44 File Offset: 0x0001FD44
		internal static byte[] ConstructSequence(IEnumerable<byte[][]> items)
		{
			int num = 0;
			foreach (byte[][] array in items)
			{
				foreach (byte[] array3 in array)
				{
					num += array3.Length;
				}
			}
			byte[] array4 = DerEncoder.EncodeLength(num);
			byte[] array5 = new byte[1 + array4.Length + num];
			array5[0] = 48;
			int num2 = 1;
			Buffer.BlockCopy(array4, 0, array5, num2, array4.Length);
			num2 += array4.Length;
			foreach (byte[][] array6 in items)
			{
				foreach (byte[] array8 in array6)
				{
					Buffer.BlockCopy(array8, 0, array5, num2, array8.Length);
					num2 += array8.Length;
				}
			}
			return array5;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00021C4C File Offset: 0x0001FE4C
		private static BigInteger ParseOidRid(string oidValue, ref int startIndex)
		{
			int num = oidValue.IndexOf('.', startIndex);
			if (num == -1)
			{
				num = oidValue.Length;
			}
			BigInteger bigInteger = BigInteger.Zero;
			for (int i = startIndex; i < num; i++)
			{
				bigInteger *= 10;
				bigInteger += DerEncoder.AtoI(oidValue[i]);
			}
			startIndex = num + 1;
			return bigInteger;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00021CAE File Offset: 0x0001FEAE
		private static int AtoI(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			throw new CryptographicException(SR.GetString("Argument_InvalidOidValue"));
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00021CD0 File Offset: 0x0001FED0
		private static void EncodeRid(List<byte> encodedData, ref BigInteger rid)
		{
			BigInteger divisor = new BigInteger(128);
			BigInteger bigInteger = rid;
			Stack<byte> stack = new Stack<byte>();
			byte b = 0;
			do
			{
				BigInteger value;
				bigInteger = BigInteger.DivRem(bigInteger, divisor, out value);
				byte b2 = (byte)value;
				b2 |= b;
				b = 128;
				stack.Push(b2);
			}
			while (bigInteger != BigInteger.Zero);
			encodedData.AddRange(stack);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00021D34 File Offset: 0x0001FF34
		private static bool IsPrintableStringCharacter(char c)
		{
			if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
			{
				return true;
			}
			if (c <= ':')
			{
				switch (c)
				{
				case ' ':
				case '\'':
				case '(':
				case ')':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
					break;
				case '!':
				case '"':
				case '#':
				case '$':
				case '%':
				case '&':
				case '*':
					return false;
				default:
					if (c != ':')
					{
						return false;
					}
					break;
				}
			}
			else if (c != '=' && c != '?')
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00021DC3 File Offset: 0x0001FFC3
		private static byte[] ConcatenateArrays(params byte[][][] segments)
		{
			return DerEncoder.ConcatenateArrays(segments);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00021DCC File Offset: 0x0001FFCC
		private static byte[] ConcatenateArrays(IEnumerable<byte[][]> segments)
		{
			int num = 0;
			foreach (byte[][] array in segments)
			{
				foreach (byte[] array3 in array)
				{
					num += array3.Length;
				}
			}
			byte[] array4 = new byte[num];
			int num2 = 0;
			foreach (byte[][] array5 in segments)
			{
				foreach (byte[] array7 in array5)
				{
					Buffer.BlockCopy(array7, 0, array4, num2, array7.Length);
					num2 += array7.Length;
				}
			}
			return array4;
		}

		// Token: 0x040006FD RID: 1789
		private const byte ConstructedFlag = 32;

		// Token: 0x040006FE RID: 1790
		private const byte ConstructedSequenceTag = 48;

		// Token: 0x040006FF RID: 1791
		private const byte ConstructedSetTag = 49;

		// Token: 0x04000700 RID: 1792
		private static readonly byte[][] s_nullTlv = new byte[][]
		{
			new byte[]
			{
				5
			},
			new byte[1],
			EncodingHelpers.s_emptyArray
		};

		// Token: 0x02000355 RID: 853
		private class AsnSetValueComparer : IComparer<byte[][]>, IComparer
		{
			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000632BF File Offset: 0x000614BF
			public static DerEncoder.AsnSetValueComparer Instance { get; } = new DerEncoder.AsnSetValueComparer();

			// Token: 0x06001B72 RID: 7026 RVA: 0x000632C8 File Offset: 0x000614C8
			public int Compare(byte[][] x, byte[][] y)
			{
				int num = (int)(x[0][0] - y[0][0]);
				if (num != 0)
				{
					return num;
				}
				num = x[2].Length - y[2].Length;
				if (num != 0)
				{
					return num;
				}
				for (int i = 0; i < x[2].Length; i++)
				{
					num = (int)(x[2][i] - y[2][i]);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x06001B73 RID: 7027 RVA: 0x00063319 File Offset: 0x00061519
			public int Compare(object x, object y)
			{
				return this.Compare(x as byte[][], y as byte[][]);
			}
		}
	}
}
