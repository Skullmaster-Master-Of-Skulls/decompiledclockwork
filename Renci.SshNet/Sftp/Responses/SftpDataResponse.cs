using System;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000042 RID: 66
	internal class SftpDataResponse : SftpResponse
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001206A File Offset: 0x0001026A
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Data;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001206E File Offset: 0x0001026E
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00012076 File Offset: 0x00010276
		public byte[] Data { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001207F File Offset: 0x0001027F
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00012087 File Offset: 0x00010287
		public bool IsEof { get; private set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x0001204D File Offset: 0x0001024D
		public SftpDataResponse(uint protocolVersion) : base(protocolVersion)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00012090 File Offset: 0x00010290
		protected override void LoadData()
		{
			base.LoadData();
			this.Data = base.ReadBinary();
			if (!base.IsEndOfData)
			{
				this.IsEof = base.ReadBoolean();
			}
		}
	}
}
