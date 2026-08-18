using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000626 RID: 1574
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x06003072 RID: 12402 RVA: 0x000D1888 File Offset: 0x000D0888
		internal PingException()
		{
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000D1890 File Offset: 0x000D0890
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000D189A File Offset: 0x000D089A
		public PingException(string message) : base(message)
		{
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000D18A3 File Offset: 0x000D08A3
		public PingException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
