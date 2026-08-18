using System;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000041 RID: 65
	internal class SftpAttrsResponse : SftpResponse
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00012038 File Offset: 0x00010238
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Attrs;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001203C File Offset: 0x0001023C
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00012044 File Offset: 0x00010244
		public SftpFileAttributes Attributes { get; private set; }

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001204D File Offset: 0x0001024D
		public SftpAttrsResponse(uint protocolVersion) : base(protocolVersion)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012056 File Offset: 0x00010256
		protected override void LoadData()
		{
			base.LoadData();
			this.Attributes = base.ReadAttributes();
		}
	}
}
