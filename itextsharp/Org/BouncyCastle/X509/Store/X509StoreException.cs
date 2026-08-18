using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020000F7 RID: 247
	public class X509StoreException : Exception
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00032B64 File Offset: 0x00031B64
		public X509StoreException()
		{
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00032B6C File Offset: 0x00031B6C
		public X509StoreException(string message) : base(message)
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00032B75 File Offset: 0x00031B75
		public X509StoreException(string message, Exception e) : base(message, e)
		{
		}
	}
}
