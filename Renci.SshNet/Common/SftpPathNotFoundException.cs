using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class SftpPathNotFoundException : SshException
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002417D File Offset: 0x0002237D
		public SftpPathNotFoundException()
		{
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00024185 File Offset: 0x00022385
		public SftpPathNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002418E File Offset: 0x0002238E
		public SftpPathNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00024198 File Offset: 0x00022398
		protected SftpPathNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
