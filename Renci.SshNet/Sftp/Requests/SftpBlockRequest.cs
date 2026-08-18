using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200004C RID: 76
	internal class SftpBlockRequest : SftpRequest
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000124E7 File Offset: 0x000106E7
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Block;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000124EB File Offset: 0x000106EB
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000124F3 File Offset: 0x000106F3
		public byte[] Handle { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000124FC File Offset: 0x000106FC
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00012504 File Offset: 0x00010704
		public ulong Offset { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001250D File Offset: 0x0001070D
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00012515 File Offset: 0x00010715
		public ulong Length { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001251E File Offset: 0x0001071E
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00012526 File Offset: 0x00010726
		public uint LockMask { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001252F File Offset: 0x0001072F
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length + 8 + 8 + 4;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00012548 File Offset: 0x00010748
		public SftpBlockRequest(uint protocolVersion, uint requestId, byte[] handle, ulong offset, ulong length, uint lockMask, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			this.Offset = offset;
			this.Length = length;
			this.LockMask = lockMask;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012573 File Offset: 0x00010773
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
			this.Offset = base.ReadUInt64();
			this.Length = base.ReadUInt64();
			this.LockMask = base.ReadUInt32();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000125AB File Offset: 0x000107AB
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
			base.Write(this.Offset);
			base.Write(this.Length);
			base.Write(this.LockMask);
		}
	}
}
