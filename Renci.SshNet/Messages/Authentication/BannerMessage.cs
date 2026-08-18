using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000BF RID: 191
	[Message("SSH_MSG_USERAUTH_BANNER", 53)]
	public class BannerMessage : Message
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001F0FD File Offset: 0x0001D2FD
		public string Message
		{
			get
			{
				return SshData.Utf8.GetString(this._message, 0, this._message.Length);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001F118 File Offset: 0x0001D318
		public string Language
		{
			get
			{
				return SshData.Utf8.GetString(this._language, 0, this._language.Length);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001F133 File Offset: 0x0001D333
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._message.Length + 4 + this._language.Length;
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001F151 File Offset: 0x0001D351
		protected override void LoadData()
		{
			this._message = base.ReadBinary();
			this._language = base.ReadBinary();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001F16B File Offset: 0x0001D36B
		protected override void SaveData()
		{
			base.WriteBinaryString(this._message);
			base.WriteBinaryString(this._language);
		}

		// Token: 0x0400035E RID: 862
		private byte[] _message;

		// Token: 0x0400035F RID: 863
		private byte[] _language;
	}
}
