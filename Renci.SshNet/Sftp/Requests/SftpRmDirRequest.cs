using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200005F RID: 95
	internal class SftpRmDirRequest : SftpRequest
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000132EC File Offset: 0x000114EC
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.RmDir;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000132F0 File Offset: 0x000114F0
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001330C File Offset: 0x0001150C
		public string Path
		{
			get
			{
				return this.Encoding.GetString(this._path, 0, this._path.Length);
			}
			private set
			{
				this._path = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00013320 File Offset: 0x00011520
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00013328 File Offset: 0x00011528
		public Encoding Encoding { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00013331 File Offset: 0x00011531
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00013344 File Offset: 0x00011544
		public SftpRmDirRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001335F File Offset: 0x0001155F
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00013373 File Offset: 0x00011573
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x04000220 RID: 544
		private byte[] _path;
	}
}
