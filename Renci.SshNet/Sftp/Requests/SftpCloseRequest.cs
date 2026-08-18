using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200004F RID: 79
	internal class SftpCloseRequest : SftpRequest
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001271D File Offset: 0x0001091D
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Close;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00012720 File Offset: 0x00010920
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00012728 File Offset: 0x00010928
		public byte[] Handle { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00012731 File Offset: 0x00010931
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00012744 File Offset: 0x00010944
		public SftpCloseRequest(uint protocolVersion, uint requestId, byte[] handle, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00012757 File Offset: 0x00010957
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001276B File Offset: 0x0001096B
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
		}
	}
}
