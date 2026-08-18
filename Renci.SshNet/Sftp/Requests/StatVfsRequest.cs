using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000064 RID: 100
	internal class StatVfsRequest : SftpExtendedRequest
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00013713 File Offset: 0x00011913
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001372F File Offset: 0x0001192F
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

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00013743 File Offset: 0x00011943
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001374B File Offset: 0x0001194B
		public Encoding Encoding { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00013754 File Offset: 0x00011954
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00013767 File Offset: 0x00011967
		public StatVfsRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpExtendedReplyResponse> extendedAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction, "statvfs@openssh.com")
		{
			this.Encoding = encoding;
			this.Path = path;
			base.SetAction(extendedAction);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001378F File Offset: 0x0001198F
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x0400022F RID: 559
		private byte[] _path;
	}
}
