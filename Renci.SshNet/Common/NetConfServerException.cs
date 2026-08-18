using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class NetConfServerException : SshException
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x0002417D File Offset: 0x0002237D
		public NetConfServerException()
		{
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00024185 File Offset: 0x00022385
		public NetConfServerException(string message) : base(message)
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002418E File Offset: 0x0002238E
		public NetConfServerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00024198 File Offset: 0x00022398
		protected NetConfServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
