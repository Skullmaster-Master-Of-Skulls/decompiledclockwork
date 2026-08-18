using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200005B RID: 91
	internal class SftpRealPathRequest : SftpRequest
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00012F52 File Offset: 0x00011152
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.RealPath;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00012F56 File Offset: 0x00011156
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00012F72 File Offset: 0x00011172
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00012F86 File Offset: 0x00011186
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00012F8E File Offset: 0x0001118E
		public Encoding Encoding { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00012F97 File Offset: 0x00011197
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00012FAC File Offset: 0x000111AC
		public SftpRealPathRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpNameResponse> nameAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			if (nameAction == null)
			{
				throw new ArgumentNullException("nameAction");
			}
			if (statusAction == null)
			{
				throw new ArgumentNullException("statusAction");
			}
			this.Encoding = encoding;
			this.Path = path;
			base.SetAction(nameAction);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00012FF8 File Offset: 0x000111F8
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x04000211 RID: 529
		private byte[] _path;
	}
}
