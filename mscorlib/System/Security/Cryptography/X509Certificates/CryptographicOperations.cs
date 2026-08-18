using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008CD RID: 2253
	internal static class CryptographicOperations
	{
		// Token: 0x06005246 RID: 21062 RVA: 0x00127483 File Offset: 0x00126483
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ZeroMemory(Span<byte> buffer)
		{
			buffer.Clear();
		}
	}
}
