using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000054 RID: 84
	internal class SftpLStatRequest : SftpRequest
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00012A0B File Offset: 0x00010C0B
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.LStat;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00012A0E File Offset: 0x00010C0E
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00012A2A File Offset: 0x00010C2A
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00012A3E File Offset: 0x00010C3E
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00012A46 File Offset: 0x00010C46
		public Encoding Encoding { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00012A4F File Offset: 0x00010C4F
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012A62 File Offset: 0x00010C62
		public SftpLStatRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpAttrsResponse> attrsAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
			base.SetAction(attrsAction);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012A85 File Offset: 0x00010C85
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00012A99 File Offset: 0x00010C99
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x040001FF RID: 511
		private byte[] _path;
	}
}
