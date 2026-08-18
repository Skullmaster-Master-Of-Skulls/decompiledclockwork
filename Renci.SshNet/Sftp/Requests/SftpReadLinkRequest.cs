using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000059 RID: 89
	internal class SftpReadLinkRequest : SftpRequest
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00012DE2 File Offset: 0x00010FE2
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.ReadLink;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00012DE6 File Offset: 0x00010FE6
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00012E02 File Offset: 0x00011002
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

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00012E16 File Offset: 0x00011016
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00012E1E File Offset: 0x0001101E
		public Encoding Encoding { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00012E27 File Offset: 0x00011027
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00012E3A File Offset: 0x0001103A
		public SftpReadLinkRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpNameResponse> nameAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
			base.SetAction(nameAction);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00012E5D File Offset: 0x0001105D
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00012E71 File Offset: 0x00011071
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x0400020C RID: 524
		private byte[] _path;
	}
}
