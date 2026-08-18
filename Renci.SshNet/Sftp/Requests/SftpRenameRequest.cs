using System;
using System.Text;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200005D RID: 93
	internal class SftpRenameRequest : SftpRequest
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000130A7 File Offset: 0x000112A7
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Rename;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x000130AB File Offset: 0x000112AB
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x000130C7 File Offset: 0x000112C7
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

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000130DB File Offset: 0x000112DB
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000130F7 File Offset: 0x000112F7
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001310B File Offset: 0x0001130B
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00013113 File Offset: 0x00011313
		public Encoding Encoding { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001311C File Offset: 0x0001131C
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._oldPath.Length + 4 + this._newPath.Length;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001313A File Offset: 0x0001133A
		public SftpRenameRequest(uint protocolVersion, uint requestId, string oldPath, string newPath, Encoding encoding, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction)
		{
			this.Encoding = encoding;
			this.OldPath = oldPath;
			this.NewPath = newPath;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001315D File Offset: 0x0001135D
		protected override void LoadData()
		{
			base.LoadData();
			this._oldPath = base.ReadBinary();
			this._newPath = base.ReadBinary();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001317D File Offset: 0x0001137D
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._oldPath);
			base.WriteBinaryString(this._newPath);
		}

		// Token: 0x04000215 RID: 533
		private byte[] _oldPath;

		// Token: 0x04000216 RID: 534
		private byte[] _newPath;
	}
}
