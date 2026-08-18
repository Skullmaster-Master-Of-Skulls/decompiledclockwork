using System;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public abstract class RequestException : Exception
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000C7FE File Offset: 0x0000A9FE
		protected RequestException()
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000544D File Offset: 0x0000364D
		protected RequestException(string message) : base(message)
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00005456 File Offset: 0x00003656
		protected RequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00005473 File Offset: 0x00003673
		protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
