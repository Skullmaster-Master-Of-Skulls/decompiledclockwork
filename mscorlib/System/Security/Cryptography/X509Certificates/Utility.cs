using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008F0 RID: 2288
	internal static class Utility
	{
		// Token: 0x0600530A RID: 21258 RVA: 0x0012B3E9 File Offset: 0x0012A3E9
		public static Span<T> GetSpanForArray<T>(T[] array, int offset)
		{
			return Utility.GetSpanForArray<T>(array, offset, array.Length - offset);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0012B3F7 File Offset: 0x0012A3F7
		public static Span<T> GetSpanForArray<T>(T[] array, int offset, int count)
		{
			return new Span<T>(array, offset, count);
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0012B404 File Offset: 0x0012A404
		public static int EncodingGetByteCount(Encoding encoding, ReadOnlySpan<char> input)
		{
			if (input.IsNull)
			{
				return encoding.GetByteCount(new char[0]);
			}
			ArraySegment<char> arraySegment = input.DangerousGetArraySegment();
			return encoding.GetByteCount(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0012B44C File Offset: 0x0012A44C
		public static int EncodingGetBytes(Encoding encoding, char[] input, Span<byte> destination)
		{
			ArraySegment<byte> arraySegment = destination.DangerousGetArraySegment();
			return encoding.GetBytes(input, 0, input.Length, arraySegment.Array, arraySegment.Offset);
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0012B47C File Offset: 0x0012A47C
		public static int EncodingGetBytes(Encoding encoding, ReadOnlySpan<char> input, Span<byte> destination)
		{
			ArraySegment<byte> arraySegment = destination.DangerousGetArraySegment();
			ArraySegment<char> arraySegment2 = input.DangerousGetArraySegment();
			return encoding.GetBytes(arraySegment2.Array, arraySegment2.Offset, arraySegment2.Count, arraySegment.Array, arraySegment.Offset);
		}
	}
}
