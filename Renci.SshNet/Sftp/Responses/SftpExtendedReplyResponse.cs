using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000043 RID: 67
	internal class SftpExtendedReplyResponse : SftpResponse
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x000120B8 File Offset: 0x000102B8
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.ExtendedReply;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001204D File Offset: 0x0001024D
		public SftpExtendedReplyResponse(uint protocolVersion) : base(protocolVersion)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000120BF File Offset: 0x000102BF
		public T GetReply<T>() where T : SshData, new()
		{
			return base.OfType<T>();
		}
	}
}
