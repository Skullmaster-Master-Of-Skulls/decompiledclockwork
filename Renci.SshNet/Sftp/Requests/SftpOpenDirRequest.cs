using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000056 RID: 86
	internal class SftpOpenDirRequest : SftpRequest
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00012BB7 File Offset: 0x00010DB7
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.OpenDir;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00012BBB File Offset: 0x00010DBB
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00012BD7 File Offset: 0x00010DD7
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00012BEB File Offset: 0x00010DEB
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00012BF3 File Offset: 0x00010DF3
		public Encoding Encoding { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00012BFC File Offset: 0x00010DFC
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00012C0F File Offset: 0x00010E0F
		public SftpOpenDirRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpHandleResponse> handleAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
			base.SetAction(handleAction);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00012C32 File Offset: 0x00010E32
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00012C46 File Offset: 0x00010E46
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x04000205 RID: 517
		private byte[] _path;
	}
}
