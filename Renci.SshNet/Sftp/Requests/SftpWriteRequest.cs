using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000063 RID: 99
	internal class SftpWriteRequest : SftpRequest
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00013616 File Offset: 0x00011816
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Write;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00013619 File Offset: 0x00011819
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00013621 File Offset: 0x00011821
		public byte[] Handle { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001362A File Offset: 0x0001182A
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00013632 File Offset: 0x00011832
		public ulong Offset { get; private set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001363B File Offset: 0x0001183B
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00013643 File Offset: 0x00011843
		public byte[] Data { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001364C File Offset: 0x0001184C
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00013654 File Offset: 0x00011854
		public int Length { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001365D File Offset: 0x0001185D
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length + 8 + 4 + this.Length;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001367B File Offset: 0x0001187B
		public SftpWriteRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, byte[] data, int length, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			this.Offset = offset;
			this.Data = data;
			this.Length = length;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000136A6 File Offset: 0x000118A6
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
			this.Offset = base.ReadUInt64();
			this.Data = base.ReadBinary();
			this.Length = this.Data.Length;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000136E0 File Offset: 0x000118E0
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
			base.Write(this.Offset);
			base.WriteBinary(this.Data, 0, this.Length);
		}
	}
}
