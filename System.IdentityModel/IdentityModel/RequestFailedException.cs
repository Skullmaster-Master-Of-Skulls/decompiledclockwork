using System;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class RequestFailedException : RequestException
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000C806 File Offset: 0x0000AA06
		public RequestFailedException() : base(SR.GetString("ID2008"))
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000057AD File Offset: 0x000039AD
		public RequestFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000057B6 File Offset: 0x000039B6
		public RequestFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000057C0 File Offset: 0x000039C0
		protected RequestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
