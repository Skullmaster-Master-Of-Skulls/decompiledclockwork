using System;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000046 RID: 70
	internal abstract class SftpResponse : SftpMessage
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000121BB File Offset: 0x000103BB
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x000121C3 File Offset: 0x000103C3
		public uint ResponseId { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000121CC File Offset: 0x000103CC
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x000121D4 File Offset: 0x000103D4
		public uint ProtocolVersion { get; private set; }

		// Token: 0x060004F3 RID: 1267 RVA: 0x000121DD File Offset: 0x000103DD
		protected SftpResponse(uint protocolVersion)
		{
			this.ProtocolVersion = protocolVersion;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000121EC File Offset: 0x000103EC
		protected override void LoadData()
		{
			base.LoadData();
			this.ResponseId = base.ReadUInt32();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012200 File Offset: 0x00010400
		protected override void SaveData()
		{
			throw new InvalidOperationException("Response cannot be saved.");
		}
	}
}
