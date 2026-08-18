using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200005C RID: 92
	internal class SftpRemoveRequest : SftpRequest
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001300C File Offset: 0x0001120C
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Remove;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00013010 File Offset: 0x00011210
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001302C File Offset: 0x0001122C
		public string Filename
		{
			get
			{
				return this.Encoding.GetString(this._fileName, 0, this._fileName.Length);
			}
			private set
			{
				this._fileName = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00013040 File Offset: 0x00011240
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00013048 File Offset: 0x00011248
		public Encoding Encoding { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00013051 File Offset: 0x00011251
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._fileName.Length;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00013064 File Offset: 0x00011264
		public SftpRemoveRequest(uint protocolVersion, uint requestId, string filename, Encoding encoding, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Filename = filename;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001307F File Offset: 0x0001127F
		protected override void LoadData()
		{
			base.LoadData();
			this._fileName = base.ReadBinary();
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00013093 File Offset: 0x00011293
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._fileName);
		}

		// Token: 0x04000213 RID: 531
		private byte[] _fileName;
	}
}
