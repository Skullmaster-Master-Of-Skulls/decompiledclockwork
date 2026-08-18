using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D0 RID: 208
	[Message("SSH_MSG_DISCONNECT", 1)]
	public class DisconnectMessage : Message, IKeyExchangedAllowed
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001FA8E File Offset: 0x0001DC8E
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0001FA96 File Offset: 0x0001DC96
		public DisconnectReason ReasonCode { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001FA9F File Offset: 0x0001DC9F
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0001FABA File Offset: 0x0001DCBA
		public string Description
		{
			get
			{
				return SshData.Utf8.GetString(this._description, 0, this._description.Length);
			}
			private set
			{
				this._description = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001FACD File Offset: 0x0001DCCD
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		public string Language
		{
			get
			{
				return SshData.Utf8.GetString(this._language, 0, this._language.Length);
			}
			private set
			{
				this._language = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001FAFB File Offset: 0x0001DCFB
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + 4 + this._description.Length + 4 + this._language.Length;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		public DisconnectMessage()
		{
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001FB1B File Offset: 0x0001DD1B
		public DisconnectMessage(DisconnectReason reasonCode, string message)
		{
			this.ReasonCode = reasonCode;
			this.Description = message;
			this.Language = "en";
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		protected override void LoadData()
		{
			this.ReasonCode = (DisconnectReason)base.ReadUInt32();
			this._description = base.ReadBinary();
			this._language = base.ReadBinary();
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001FB62 File Offset: 0x0001DD62
		protected override void SaveData()
		{
			base.Write((uint)this.ReasonCode);
			base.WriteBinaryString(this._description);
			base.WriteBinaryString(this._language);
		}

		// Token: 0x04000384 RID: 900
		private byte[] _description;

		// Token: 0x04000385 RID: 901
		private byte[] _language;
	}
}
