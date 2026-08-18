using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000008 RID: 8
	internal static class BlobUtilities
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003FFC File Offset: 0x000021FC
		public unsafe static void WriteBytes(this byte[] buffer, int start, byte value, int count)
		{
			fixed (byte* ptr = &buffer[0])
			{
				byte* ptr2 = ptr + start;
				for (int i = 0; i < count; i++)
				{
					ptr2[i] = value;
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000402C File Offset: 0x0000222C
		public unsafe static void WriteDouble(this byte[] buffer, int start, double value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*(double*)ptr = value;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004048 File Offset: 0x00002248
		public unsafe static void WriteSingle(this byte[] buffer, int start, float value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*(float*)ptr = value;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004064 File Offset: 0x00002264
		public static void WriteByte(this byte[] buffer, int start, byte value)
		{
			buffer[start] = value;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000406C File Offset: 0x0000226C
		public unsafe static void WriteUInt16(this byte[] buffer, int start, ushort value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*(short*)ptr = (short)value;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004088 File Offset: 0x00002288
		public unsafe static void WriteUInt16BE(this byte[] buffer, int start, ushort value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*ptr = (byte)(value >> 8);
				ptr[1] = (byte)value;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000040B0 File Offset: 0x000022B0
		public unsafe static void WriteUInt32BE(this byte[] buffer, int start, uint value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*ptr = (byte)(value >> 24);
				ptr[1] = (byte)(value >> 16);
				ptr[2] = (byte)(value >> 8);
				ptr[3] = (byte)value;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000040EC File Offset: 0x000022EC
		public unsafe static void WriteUInt32(this byte[] buffer, int start, uint value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*(int*)ptr = (int)value;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004108 File Offset: 0x00002308
		public unsafe static void WriteUInt64(this byte[] buffer, int start, ulong value)
		{
			fixed (byte* ptr = &buffer[start])
			{
				*(long*)ptr = (long)value;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004124 File Offset: 0x00002324
		public unsafe static void WriteDecimal(this byte[] buffer, int start, decimal value)
		{
			bool flag;
			byte b;
			uint num;
			uint num2;
			uint num3;
			value.GetBits(out flag, out b, out num, out num2, out num3);
			fixed (byte* ptr = &buffer[start])
			{
				*ptr = (b | (flag ? 128 : 0));
				*(int*)((byte*)ptr + 1) = (int)num;
				*(int*)((byte*)ptr + 5) = (int)num2;
				*(int*)((byte*)ptr + 9) = (int)num3;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004178 File Offset: 0x00002378
		public unsafe static void WriteUTF8(this byte[] buffer, int start, char* charPtr, int charCount, int byteCount, bool allowUnpairedSurrogates)
		{
			char* ptr = charPtr + charCount;
			fixed (byte* ptr2 = &buffer[0])
			{
				byte* ptr3 = ptr2 + start;
				if (byteCount == charCount)
				{
					while (charPtr < ptr)
					{
						*(ptr3++) = (byte)(*(charPtr++));
					}
				}
				else
				{
					while (charPtr < ptr)
					{
						char c = *(charPtr++);
						if (c < '\u0080')
						{
							*(ptr3++) = (byte)c;
						}
						else if (c < 'ࠀ')
						{
							*ptr3 = (byte)((c >> 6 & '\u001f') | 'À');
							ptr3[1] = (byte)((c & '?') | '\u0080');
							ptr3 += 2;
						}
						else
						{
							if (BlobUtilities.IsSurrogateChar((int)c))
							{
								if (BlobUtilities.IsHighSurrogateChar((int)c) && charPtr < ptr && BlobUtilities.IsLowSurrogateChar((int)(*charPtr)))
								{
									int num = (int)c;
									int num2 = (int)(*(charPtr++));
									int num3 = (num - 55296 << 10) + num2 - 56320 + 65536;
									*ptr3 = (byte)((num3 >> 18 & 7) | 240);
									ptr3[1] = (byte)((num3 >> 12 & 63) | 128);
									ptr3[2] = (byte)((num3 >> 6 & 63) | 128);
									ptr3[3] = (byte)((num3 & 63) | 128);
									ptr3 += 4;
									continue;
								}
								if (!allowUnpairedSurrogates)
								{
									c = '�';
								}
							}
							*ptr3 = (byte)((c >> 12 & '\u000f') | 'à');
							ptr3[1] = (byte)((c >> 6 & '?') | '\u0080');
							ptr3[2] = (byte)((c & '?') | '\u0080');
							ptr3 += 3;
						}
					}
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000042E0 File Offset: 0x000024E0
		internal unsafe static int GetUTF8ByteCount(string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return BlobUtilities.GetUTF8ByteCount(ptr, str.Length);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000430C File Offset: 0x0000250C
		internal unsafe static int GetUTF8ByteCount(char* str, int charCount)
		{
			char* ptr;
			return BlobUtilities.GetUTF8ByteCount(str, charCount, int.MaxValue, out ptr);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004328 File Offset: 0x00002528
		internal unsafe static int GetUTF8ByteCount(char* str, int charCount, int byteLimit, out char* remainder)
		{
			char* ptr = str + charCount;
			char* ptr2 = str;
			int num = 0;
			while (ptr2 < ptr)
			{
				char c = *(ptr2++);
				int num2;
				if (c < '\u0080')
				{
					num2 = 1;
				}
				else if (c < 'ࠀ')
				{
					num2 = 2;
				}
				else if (BlobUtilities.IsHighSurrogateChar((int)c) && ptr2 < ptr && BlobUtilities.IsLowSurrogateChar((int)(*ptr2)))
				{
					num2 = 4;
					ptr2++;
				}
				else
				{
					num2 = 3;
				}
				if (num + num2 > byteLimit)
				{
					ptr2 -= ((num2 < 4) ? 1 : 2);
					break;
				}
				num += num2;
			}
			remainder = ptr2;
			return num;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000043A6 File Offset: 0x000025A6
		internal static bool IsSurrogateChar(int c)
		{
			return c - 55296 <= 2047;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000043B9 File Offset: 0x000025B9
		internal static bool IsHighSurrogateChar(int c)
		{
			return c - 55296 <= 1023;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000043CC File Offset: 0x000025CC
		internal static bool IsLowSurrogateChar(int c)
		{
			return c - 56320 <= 1023;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000043DF File Offset: 0x000025DF
		internal static void ValidateRange(int bufferLength, int start, int byteCount)
		{
			if (start < 0 || start > bufferLength)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (byteCount < 0 || byteCount > bufferLength - start)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
		}

		// Token: 0x04000014 RID: 20
		public const int SizeOfSerializedDecimal = 13;
	}
}
