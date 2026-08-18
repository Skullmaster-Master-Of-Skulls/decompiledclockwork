using System;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x0200004A RID: 74
	internal class HardLinkRequest : SftpExtendedRequest
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00012364 File Offset: 0x00010564
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001237F File Offset: 0x0001057F
		public string OldPath
		{
			get
			{
				return SshData.Utf8.GetString(this._oldPath, 0, this._oldPath.Length);
			}
			private set
			{
				this._oldPath = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00012392 File Offset: 0x00010592
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x000123AD File Offset: 0x000105AD
		public string NewPath
		{
			get
			{
				return SshData.Utf8.GetString(this._newPath, 0, this._newPath.Length);
			}
			private set
			{
				this._newPath = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000123C0 File Offset: 0x000105C0
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._oldPath.Length + 4 + this._newPath.Length;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000123DE File Offset: 0x000105DE
		public HardLinkRequest(uint protocolVersion, uint requestId, string oldPath, string newPath, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction, "hardlink@openssh.com")
		{
			this.OldPath = oldPath;
			this.NewPath = newPath;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000123FE File Offset: 0x000105FE
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._oldPath);
			base.WriteBinaryString(this._newPath);
		}

		// Token: 0x040001E8 RID: 488
		private byte[] _oldPath;

		// Token: 0x040001E9 RID: 489
		private byte[] _newPath;
	}
}
