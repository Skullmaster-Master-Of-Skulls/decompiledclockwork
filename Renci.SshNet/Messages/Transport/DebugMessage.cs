using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000CF RID: 207
	[Message("SSH_MSG_DEBUG", 4)]
	public class DebugMessage : Message
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001F9DB File Offset: 0x0001DBDB
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0001F9E3 File Offset: 0x0001DBE3
		public bool IsAlwaysDisplay { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		public string Message
		{
			get
			{
				return SshData.Utf8.GetString(this._message, 0, this._message.Length);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001FA07 File Offset: 0x0001DC07
		public string Language
		{
			get
			{
				return SshData.Utf8.GetString(this._language, 0, this._language.Length);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001FA22 File Offset: 0x0001DC22
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 1 + 4 + this._message.Length + 4 + this._language.Length;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001FA42 File Offset: 0x0001DC42
		protected override void LoadData()
		{
			this.IsAlwaysDisplay = base.ReadBoolean();
			this._message = base.ReadBinary();
			this._language = base.ReadBinary();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001FA68 File Offset: 0x0001DC68
		protected override void SaveData()
		{
			base.Write(this.IsAlwaysDisplay);
			base.WriteBinaryString(this._message);
			base.WriteBinaryString(this._language);
		}

		// Token: 0x04000381 RID: 897
		private byte[] _message;

		// Token: 0x04000382 RID: 898
		private byte[] _language;
	}
}
