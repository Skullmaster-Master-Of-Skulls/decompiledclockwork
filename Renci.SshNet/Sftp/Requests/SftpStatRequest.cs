using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000061 RID: 97
	internal class SftpStatRequest : SftpRequest
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001347D File Offset: 0x0001167D
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Stat;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00013481 File Offset: 0x00011681
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001349D File Offset: 0x0001169D
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000134B1 File Offset: 0x000116B1
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x000134B9 File Offset: 0x000116B9
		public Encoding Encoding { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000134C2 File Offset: 0x000116C2
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._path.Length;
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000134D5 File Offset: 0x000116D5
		public SftpStatRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpAttrsResponse> attrsAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.Path = path;
			base.SetAction(attrsAction);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000134F8 File Offset: 0x000116F8
		protected override void LoadData()
		{
			base.LoadData();
			this._path = base.ReadBinary();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001350C File Offset: 0x0001170C
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._path);
		}

		// Token: 0x04000226 RID: 550
		private byte[] _path;
	}
}
