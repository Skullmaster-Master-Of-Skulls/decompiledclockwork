using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020002FC RID: 764
	public class DataLengthException : CryptoException
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x000A8648 File Offset: 0x000A7648
		public DataLengthException()
		{
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000A8650 File Offset: 0x000A7650
		public DataLengthException(string message) : base(message)
		{
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000A8659 File Offset: 0x000A7659
		public DataLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
