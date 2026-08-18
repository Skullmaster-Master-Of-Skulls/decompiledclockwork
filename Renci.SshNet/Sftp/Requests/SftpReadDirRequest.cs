using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000058 RID: 88
	internal class SftpReadDirRequest : SftpRequest
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00012D77 File Offset: 0x00010F77
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.ReadDir;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00012D7B File Offset: 0x00010F7B
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00012D83 File Offset: 0x00010F83
		public byte[] Handle { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00012D8C File Offset: 0x00010F8C
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00012D9F File Offset: 0x00010F9F
		public SftpReadDirRequest(uint protocolVersion, uint requestId, byte[] handle, Action<SftpNameResponse> nameAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			base.SetAction(nameAction);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00012DBA File Offset: 0x00010FBA
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00012DCE File Offset: 0x00010FCE
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
		}
	}
}
