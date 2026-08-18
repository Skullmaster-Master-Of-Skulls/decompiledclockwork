using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000047 RID: 71
	internal class SftpStatusResponse : SftpResponse
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001220C File Offset: 0x0001040C
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Status;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001204D File Offset: 0x0001024D
		public SftpStatusResponse(uint protocolVersion) : base(protocolVersion)
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00012210 File Offset: 0x00010410
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00012218 File Offset: 0x00010418
		public StatusCodes StatusCode { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00012221 File Offset: 0x00010421
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00012229 File Offset: 0x00010429
		public string ErrorMessage { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00012232 File Offset: 0x00010432
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0001223A File Offset: 0x0001043A
		public string Language { get; private set; }

		// Token: 0x060004FE RID: 1278 RVA: 0x00012244 File Offset: 0x00010444
		protected override void LoadData()
		{
			base.LoadData();
			this.StatusCode = (StatusCodes)base.ReadUInt32();
			if (base.ProtocolVersion < 3U)
			{
				return;
			}
			if (!base.IsEndOfData)
			{
				this.ErrorMessage = base.ReadString(SshData.Utf8);
				this.Language = base.ReadString(SshData.Ascii);
			}
		}
	}
}
