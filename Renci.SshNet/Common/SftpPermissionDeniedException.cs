using System;
using System.Runtime.Serialization;

namespace Renci.SshNet.Common
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class SftpPermissionDeniedException : SshException
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002417D File Offset: 0x0002237D
		public SftpPermissionDeniedException()
		{
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00024185 File Offset: 0x00022385
		public SftpPermissionDeniedException(string message) : base(message)
		{
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002418E File Offset: 0x0002238E
		public SftpPermissionDeniedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00024198 File Offset: 0x00022398
		protected SftpPermissionDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
