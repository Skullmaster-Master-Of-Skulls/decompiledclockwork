using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public class SshAuthenticationException : SshException
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002417D File Offset: 0x0002237D
		public SshAuthenticationException()
		{
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00024185 File Offset: 0x00022385
		public SshAuthenticationException(string message) : base(message)
		{
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002418E File Offset: 0x0002238E
		public SshAuthenticationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00024198 File Offset: 0x00022398
		protected SshAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
