using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008F2 RID: 2290
	internal static class CryptoPool
	{
		// Token: 0x06005311 RID: 21265 RVA: 0x0012B500 File Offset: 0x0012A500
		public static byte[] Rent(int size)
		{
			return new byte[size];
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0012B508 File Offset: 0x0012A508
		public static void Return(byte[] array, int clearSize)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(array, 0, clearSize));
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0012B517 File Offset: 0x0012A517
		public static void Return(byte[] array)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(array));
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0012B524 File Offset: 0x0012A524
		public static void Return(ArraySegment<byte> segment, int clearSize)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(segment).Slice(0, clearSize));
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0012B546 File Offset: 0x0012A546
		public static void Return(ArraySegment<byte> segment)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(segment));
		}
	}
}
