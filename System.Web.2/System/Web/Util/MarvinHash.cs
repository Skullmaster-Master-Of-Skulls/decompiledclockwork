using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace System.Web.Util
{
	// Token: 0x020001C8 RID: 456
	internal static class MarvinHash
	{
		// Token: 0x0600174F RID: 5967 RVA: 0x00049264 File Offset: 0x00047464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int ComputeHash32(string key, ulong seed)
		{
			long num;
			fixed (string text = key)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				num = MarvinHash.ComputeHash((byte*)ptr, 2 * key.Length, seed);
			}
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0004929C File Offset: 0x0004749C
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

		// Token: 0x06001751 RID: 5969 RVA: 0x000493B8 File Offset: 0x000475B8
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

		// Token: 0x06001752 RID: 5970 RVA: 0x00049405 File Offset: 0x00047605
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint _rotl(uint value, int shift)
		{
			return value << shift | value >> 32 - shift;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00049418 File Offset: 0x00047618
		private unsafe static ulong GenerateSeed()
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

		// Token: 0x04001701 RID: 5889
		public static readonly ulong DefaultSeed = MarvinHash.GenerateSeed();
	}
}
