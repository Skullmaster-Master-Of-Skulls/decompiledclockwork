using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class ProxyException : SshException
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x0002417D File Offset: 0x0002237D
		public ProxyException()
		{
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00024185 File Offset: 0x00022385
		public ProxyException(string message) : base(message)
		{
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002418E File Offset: 0x0002238E
		public ProxyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00024198 File Offset: 0x00022398
		protected ProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
