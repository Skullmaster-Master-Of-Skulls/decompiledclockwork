using System;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000053 RID: 83
	internal class SftpLinkRequest : SftpRequest
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000128F9 File Offset: 0x00010AF9
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Link;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x000128FD File Offset: 0x00010AFD
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00012918 File Offset: 0x00010B18
		public string NewLinkPath
		{
			get
			{
				return SshData.Utf8.GetString(this._newLinkPath, 0, this._newLinkPath.Length);
			}
			private set
			{
				this._newLinkPath = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001292B File Offset: 0x00010B2B
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00012946 File Offset: 0x00010B46
		public string ExistingPath
		{
			get
			{
				return SshData.Utf8.GetString(this._existingPath, 0, this._existingPath.Length);
			}
			private set
			{
				this._existingPath = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00012959 File Offset: 0x00010B59
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00012961 File Offset: 0x00010B61
		public bool IsSymLink { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001296A File Offset: 0x00010B6A
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.NewLinkPath.Length + 4 + this.ExistingPath.Length + 1;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00012990 File Offset: 0x00010B90
		public SftpLinkRequest(uint protocolVersion, uint requestId, string newLinkPath, string existingPath, bool isSymLink, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.NewLinkPath = newLinkPath;
			this.ExistingPath = existingPath;
			this.IsSymLink = isSymLink;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000129B3 File Offset: 0x00010BB3
		protected override void LoadData()
		{
			base.LoadData();
			this._newLinkPath = base.ReadBinary();
			this._existingPath = base.ReadBinary();
			this.IsSymLink = base.ReadBoolean();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000129DF File Offset: 0x00010BDF
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._newLinkPath);
			base.WriteBinaryString(this._existingPath);
			base.Write(this.IsSymLink);
		}

		// Token: 0x040001FC RID: 508
		private byte[] _newLinkPath;

		// Token: 0x040001FD RID: 509
		private byte[] _existingPath;
	}
}
