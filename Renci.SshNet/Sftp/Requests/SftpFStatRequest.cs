using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000051 RID: 81
	internal class SftpFStatRequest : SftpRequest
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001283D File Offset: 0x00010A3D
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.FStat;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00012840 File Offset: 0x00010A40
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00012848 File Offset: 0x00010A48
		public byte[] Handle { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00012851 File Offset: 0x00010A51
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00012864 File Offset: 0x00010A64
		public SftpFStatRequest(uint protocolVersion, uint requestId, byte[] handle, Action<SftpAttrsResponse> attrsAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			base.SetAction(attrsAction);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001287F File Offset: 0x00010A7F
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00012893 File Offset: 0x00010A93
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
		}
	}
}
