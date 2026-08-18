using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200005A RID: 90
	internal class SftpReadRequest : SftpRequest
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00010706 File Offset: 0x0000E906
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Read;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00012E85 File Offset: 0x00011085
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00012E8D File Offset: 0x0001108D
		public byte[] Handle { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00012E96 File Offset: 0x00011096
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00012E9E File Offset: 0x0001109E
		public ulong Offset { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00012EA7 File Offset: 0x000110A7
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00012EAF File Offset: 0x000110AF
		public uint Length { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00012EB8 File Offset: 0x000110B8
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length + 8 + 4;
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00012ECF File Offset: 0x000110CF
		public SftpReadRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, uint length, Action<SftpDataResponse> dataAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			this.Offset = offset;
			this.Length = length;
			base.SetAction(dataAction);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00012EFA File Offset: 0x000110FA
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
			this.Offset = base.ReadUInt64();
			this.Length = base.ReadUInt32();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00012F26 File Offset: 0x00011126
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
			base.Write(this.Offset);
			base.Write(this.Length);
		}
	}
}
