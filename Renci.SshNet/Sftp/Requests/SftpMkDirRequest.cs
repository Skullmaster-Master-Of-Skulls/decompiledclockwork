using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000055 RID: 85
	internal class SftpMkDirRequest : SftpRequest
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00012AAD File Offset: 0x00010CAD
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.MkDir;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00012AB1 File Offset: 0x00010CB1
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00012ACD File Offset: 0x00010CCD
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00012AE1 File Offset: 0x00010CE1
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00012AE9 File Offset: 0x00010CE9
		public Encoding Encoding { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00012AF2 File Offset: 0x00010CF2
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00012AFA File Offset: 0x00010CFA
		private SftpFileAttributes Attributes { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00012B03 File Offset: 0x00010D03
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00012B24 File Offset: 0x00010D24
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length + this.AttributesBytes.Length;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00012B40 File Offset: 0x00010D40
		public SftpMkDirRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpStatusResponse> statusAction) : this(protocolVersion, requestId, path, encoding, SftpFileAttributes.Empty, statusAction)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00012B54 File Offset: 0x00010D54
		private SftpMkDirRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, SftpFileAttributes attributes, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
			this.Attributes = attributes;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00012B77 File Offset: 0x00010D77
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
			this.Attributes = base.ReadAttributes();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012B97 File Offset: 0x00010D97
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
			base.Write(this.AttributesBytes);
		}

		// Token: 0x04000201 RID: 513
		private byte[] _path;

		// Token: 0x04000202 RID: 514
		private byte[] _attributesBytes;
	}
}
