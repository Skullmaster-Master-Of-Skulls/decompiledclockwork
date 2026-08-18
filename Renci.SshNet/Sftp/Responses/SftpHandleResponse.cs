using System;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000044 RID: 68
	internal class SftpHandleResponse : SftpResponse
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000120C7 File Offset: 0x000102C7
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Handle;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000120CB File Offset: 0x000102CB
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x000120D3 File Offset: 0x000102D3
		public byte[] Handle { get; private set; }

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001204D File Offset: 0x0001024D
		public SftpHandleResponse(uint protocolVersion) : base(protocolVersion)
		{
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000120DC File Offset: 0x000102DC
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
		}
	}
}
