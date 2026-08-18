using System;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200004D RID: 77
	internal abstract class SftpExtendedRequest : SftpRequest
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000125E3 File Offset: 0x000107E3
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Extended;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000125EA File Offset: 0x000107EA
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x000125F2 File Offset: 0x000107F2
		public string Name
		{
			get
			{
				return this._name;
			}
			private set
			{
				this._name = value;
				this._nameBytes = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001260C File Offset: 0x0001080C
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._nameBytes.Length;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001261F File Offset: 0x0001081F
		protected SftpExtendedRequest(uint protocolVersion, uint requestId, Action<SftpStatusResponse> statusAction, string name) : base(protocolVersion, requestId, statusAction)
		{
			this.Name = name;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00012632 File Offset: 0x00010832
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._nameBytes);
		}

		// Token: 0x040001F1 RID: 497
		private byte[] _nameBytes;

		// Token: 0x040001F2 RID: 498
		private string _name;
	}
}
