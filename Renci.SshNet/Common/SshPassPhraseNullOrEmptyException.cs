using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class SshPassPhraseNullOrEmptyException : SshException
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x0002417D File Offset: 0x0002237D
		public SshPassPhraseNullOrEmptyException()
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00024185 File Offset: 0x00022385
		public SshPassPhraseNullOrEmptyException(string message) : base(message)
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002418E File Offset: 0x0002238E
		public SshPassPhraseNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00024198 File Offset: 0x00022398
		protected SshPassPhraseNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
