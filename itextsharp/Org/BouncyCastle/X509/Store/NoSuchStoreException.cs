using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020002E3 RID: 739
	public class NoSuchStoreException : X509StoreException
	{
		// Token: 0x06001B6F RID: 7023 RVA: 0x000A5155 File Offset: 0x000A4155
		public NoSuchStoreException()
		{
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000A515D File Offset: 0x000A415D
		public NoSuchStoreException(string message) : base(message)
		{
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000A5166 File Offset: 0x000A4166
		public NoSuchStoreException(string message, Exception e) : base(message, e)
		{
		}
	}
}
