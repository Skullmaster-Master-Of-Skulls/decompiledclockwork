using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000050 RID: 80
	internal class SftpFSetStatRequest : SftpRequest
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001277F File Offset: 0x0001097F
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.FSetStat;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00012783 File Offset: 0x00010983
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001278B File Offset: 0x0001098B
		public byte[] Handle { get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00012794 File Offset: 0x00010994
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001279C File Offset: 0x0001099C
		private SftpFileAttributes Attributes { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000127A5 File Offset: 0x000109A5
		private byte[] AttributesBytes
		{
			get
			{
				if (this._attributesBytes == null)
				{
					this._attributesBytes = this.Attributes.GetBytes();
				}
				return this._attributesBytes;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000127C6 File Offset: 0x000109C6
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length + this.AttributesBytes.Length;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000127E2 File Offset: 0x000109E2
		public SftpFSetStatRequest(uint protocolVersion, uint requestId, byte[] handle, SftpFileAttributes attributes, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Handle = handle;
			this.Attributes = attributes;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000127FD File Offset: 0x000109FD
		protected override void LoadData()
		{
			base.LoadData();
			this.Handle = base.ReadBinary();
			this.Attributes = base.ReadAttributes();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001281D File Offset: 0x00010A1D
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
			base.Write(this.AttributesBytes);
		}

		// Token: 0x040001F7 RID: 503
		private byte[] _attributesBytes;
	}
}
