using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000060 RID: 96
	internal class SftpSetStatRequest : SftpRequest
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00013387 File Offset: 0x00011587
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.SetStat;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001338B File Offset: 0x0001158B
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x000133A7 File Offset: 0x000115A7
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000133BB File Offset: 0x000115BB
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x000133C3 File Offset: 0x000115C3
		public Encoding Encoding { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000133CC File Offset: 0x000115CC
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000133D4 File Offset: 0x000115D4
		private SftpFileAttributes Attributes { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000133DD File Offset: 0x000115DD
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000133FE File Offset: 0x000115FE
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length + this.AttributesBytes.Length;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001341A File Offset: 0x0001161A
		public SftpSetStatRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, SftpFileAttributes attributes, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
			this.Attributes = attributes;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001343D File Offset: 0x0001163D
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
			this.Attributes = base.ReadAttributes();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001345D File Offset: 0x0001165D
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
			base.Write(this.AttributesBytes);
		}

		// Token: 0x04000222 RID: 546
		private byte[] _path;

		// Token: 0x04000223 RID: 547
		private byte[] _attributesBytes;
	}
}
