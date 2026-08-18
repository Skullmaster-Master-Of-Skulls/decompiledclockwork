using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace System
{
	// Token: 0x0200005F RID: 95
	internal static class MarvinHash
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000D528 File Offset: 0x0000B728
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int ComputeHash32(string key, ulong seed)
		{
			int result;
			fixed (string text = key)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				result = MarvinHash.ComputeHash32((byte*)ptr, 2 * key.Length, seed);
			}
			return result;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D558 File Offset: 0x0000B758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int ComputeHash32(char[] key, int start, int len, ulong seed)
		{
			int result;
			fixed (char* ptr = &key[start])
			{
				char* data = ptr;
				result = MarvinHash.ComputeHash32((byte*)data, 2 * len, seed);
			}
			return result;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D580 File Offset: 0x0000B780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int ComputeHash32(byte* data, int count, ulong seed)
		{
			long num = MarvinHash.ComputeHash(data, count, seed);
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		private unsafe static long ComputeHash(byte* data, int count, ulong seed)
		{
			uint num = (uint)count;
			uint num2 = (uint)seed;
			uint num3 = (uint)(seed >> 32);
			int num4 = 0;
			while (num >= 8U)
			{
				num2 += *(uint*)(data + num4);
				MarvinHash.Block(ref num2, ref num3);
				num2 += *(uint*)(data + num4 + 4);
				MarvinHash.Block(ref num2, ref num3);
				num4 += 8;
				num -= 8U;
			}
			switch (num)
			{
			case 0U:
				break;
			case 1U:
				goto IL_96;
			case 2U:
				goto IL_B9;
			case 3U:
				goto IL_DC;
			case 4U:
				num2 += *(uint*)(data + num4);
				MarvinHash.Block(ref num2, ref num3);
				break;
			case 5U:
				num2 += *(uint*)(data + num4);
				num4 += 4;
				MarvinHash.Block(ref num2, ref num3);
				goto IL_96;
			case 6U:
				num2 += *(uint*)(data + num4);
				num4 += 4;
				MarvinHash.Block(ref num2, ref num3);
				goto IL_B9;
			case 7U:
				num2 += *(uint*)(data + num4);
				num4 += 4;
				MarvinHash.Block(ref num2, ref num3);
				goto IL_DC;
			default:
				goto IL_F3;
			}
			num2 += 128U;
			goto IL_F3;
			IL_96:
			num2 += (32768U | (uint)data[num4]);
			goto IL_F3;
			IL_B9:
			num2 += (8388608U | (uint)(*(ushort*)(data + num4)));
			goto IL_F3;
			IL_DC:
			num2 += (uint)(int.MinValue | (int)(data + num4)[2] << 16 | (int)(*(ushort*)(data + num4)));
			IL_F3:
			MarvinHash.Block(ref num2, ref num3);
			MarvinHash.Block(ref num2, ref num3);
			return (long)((ulong)num3 << 32 | (ulong)num2);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Block(ref uint rp0, ref uint rp1)
		{
			uint num = rp0;
			uint num2 = rp1;
			num2 ^= num;
			num = MarvinHash._rotl(num, 20);
			num += num2;
			num2 = MarvinHash._rotl(num2, 9);
			num2 ^= num;
			num = MarvinHash._rotl(num, 27);
			num += num2;
			num2 = MarvinHash._rotl(num2, 19);
			rp0 = num;
			rp1 = num2;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D709 File Offset: 0x0000B909
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint _rotl(uint value, int shift)
		{
			return value << shift | value >> 32 - shift;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D71C File Offset: 0x0000B91C
		public unsafe static ulong GenerateSeed()
		{
			byte[] array = new byte[8];
			ulong result;
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
				try
				{
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					result = (ulong)(*(long*)ptr);
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			return result;
		}

		// Token: 0x04000186 RID: 390
		public static readonly ulong DefaultSeed = MarvinHash.GenerateSeed();
	}
}
