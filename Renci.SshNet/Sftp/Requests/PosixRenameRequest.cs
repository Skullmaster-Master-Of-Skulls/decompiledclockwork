using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200004E RID: 78
	internal class PosixRenameRequest : SftpExtendedRequest
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00012646 File Offset: 0x00010846
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00012662 File Offset: 0x00010862
		public string OldPath
		{
			get
			{
				return this.Encoding.GetString(this._oldPath, 0, this._oldPath.Length);
			}
			private set
			{
				this._oldPath = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00012676 File Offset: 0x00010876
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00012692 File Offset: 0x00010892
		public string NewPath
		{
			get
			{
				return this.Encoding.GetString(this._newPath, 0, this._newPath.Length);
			}
			private set
			{
				this._newPath = this.Encoding.GetBytes(value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000126A6 File Offset: 0x000108A6
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x000126AE File Offset: 0x000108AE
		public Encoding Encoding { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000126B7 File Offset: 0x000108B7
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._oldPath.Length + 4 + this._newPath.Length;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000126D5 File Offset: 0x000108D5
		public PosixRenameRequest(uint protocolVersion, uint requestId, string oldPath, string newPath, Encoding encoding, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction, "posix-rename@openssh.com")
		{
			this.Encoding = encoding;
			this.OldPath = oldPath;
			this.NewPath = newPath;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000126FD File Offset: 0x000108FD
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._oldPath);
			base.WriteBinaryString(this._newPath);
		}

		// Token: 0x040001F3 RID: 499
		private byte[] _oldPath;

		// Token: 0x040001F4 RID: 500
		private byte[] _newPath;
	}
}
