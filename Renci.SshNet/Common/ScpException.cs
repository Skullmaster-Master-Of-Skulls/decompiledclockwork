using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public class ScpException : SshException
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002417D File Offset: 0x0002237D
		public ScpException()
		{
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00024185 File Offset: 0x00022385
		public ScpException(string message) : base(message)
		{
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002418E File Offset: 0x0002238E
		public ScpException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00024198 File Offset: 0x00022398
		protected ScpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
