using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	// Token: 0x0200043A RID: 1082
	[Serializable]
	public class AuthenticationException : SystemException
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x000BA2A2 File Offset: 0x000B84A2
		public AuthenticationException()
		{
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000BA2AA File Offset: 0x000B84AA
		protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000BA2B4 File Offset: 0x000B84B4
		public AuthenticationException(string message) : base(message)
		{
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000BA2BD File Offset: 0x000B84BD
		public AuthenticationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
