using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public class SshOperationTimeoutException : SshException
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x0002417D File Offset: 0x0002237D
		public SshOperationTimeoutException()
		{
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00024185 File Offset: 0x00022385
		public SshOperationTimeoutException(string message) : base(message)
		{
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002418E File Offset: 0x0002238E
		public SshOperationTimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00024198 File Offset: 0x00022398
		protected SshOperationTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
