using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	internal class SessionKeyExpiredException : MessageSecurityException
	{
		// Token: 0x06001957 RID: 6487 RVA: 0x0005E40E File Offset: 0x0005C60E
		public SessionKeyExpiredException()
		{
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0005E416 File Offset: 0x0005C616
		public SessionKeyExpiredException(string message) : base(message)
		{
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005E41F File Offset: 0x0005C61F
		public SessionKeyExpiredException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005E429 File Offset: 0x0005C629
		protected SessionKeyExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
