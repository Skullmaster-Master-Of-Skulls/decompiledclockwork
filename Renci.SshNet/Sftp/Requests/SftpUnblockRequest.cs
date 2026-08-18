using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200004B RID: 75
	internal class SftpUnblockRequest : SftpRequest
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001241E File Offset: 0x0001061E
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Unblock;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00012422 File Offset: 0x00010622
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001242A File Offset: 0x0001062A
		public byte[] Handle { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00012433 File Offset: 0x00010633
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001243B File Offset: 0x0001063B
		public ulong Offset { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00012444 File Offset: 0x00010644
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0001244C File Offset: 0x0001064C
		public ulong Length { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00012455 File Offset: 0x00010655
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length + 8 + 8;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001246C File Offset: 0x0001066C
		public SftpUnblockRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, ulong length, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			this.Offset = offset;
			this.Length = length;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001248F File Offset: 0x0001068F
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
			this.Offset = base.ReadUInt64();
			this.Length = base.ReadUInt64();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000124BB File Offset: 0x000106BB
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
			base.Write(this.Offset);
			base.Write(this.Length);
		}
	}
}
