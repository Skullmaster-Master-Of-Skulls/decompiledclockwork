using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000062 RID: 98
	internal class SftpSymLinkRequest : SftpRequest
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00013520 File Offset: 0x00011720
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.SymLink;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00013524 File Offset: 0x00011724
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00013540 File Offset: 0x00011740
		public string NewLinkPath
		{
			get
			{
				return this.Encoding.GetString(this._newLinkPath, 0, this._newLinkPath.Length);
			}
			private set
			{
				this._newLinkPath = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00013554 File Offset: 0x00011754
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00013570 File Offset: 0x00011770
		public string ExistingPath
		{
			get
			{
				return this.Encoding.GetString(this._existingPath, 0, this._existingPath.Length);
			}
			private set
			{
				this._existingPath = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00013584 File Offset: 0x00011784
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001358C File Offset: 0x0001178C
		public Encoding Encoding { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00013595 File Offset: 0x00011795
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._newLinkPath.Length + 4 + this._existingPath.Length;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000135B3 File Offset: 0x000117B3
		public SftpSymLinkRequest(uint protocolVersion, uint requestId, string newLinkPath, string existingPath, Encoding encoding, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.NewLinkPath = newLinkPath;
			this.ExistingPath = existingPath;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000135D6 File Offset: 0x000117D6
		protected override void LoadData()
		{
			base.LoadData();
			this._newLinkPath = base.ReadBinary();
			this._existingPath = base.ReadBinary();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000135F6 File Offset: 0x000117F6
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._newLinkPath);
			base.WriteBinaryString(this._existingPath);
		}

		// Token: 0x04000228 RID: 552
		private byte[] _newLinkPath;

		// Token: 0x04000229 RID: 553
		private byte[] _existingPath;
	}
}
