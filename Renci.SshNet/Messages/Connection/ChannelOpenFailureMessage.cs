using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A1 RID: 161
	[Message("SSH_MSG_CHANNEL_OPEN_FAILURE", 92)]
	public class ChannelOpenFailureMessage : ChannelMessage
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001DEE7 File Offset: 0x0001C0E7
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001DEEF File Offset: 0x0001C0EF
		public uint ReasonCode { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001DEF8 File Offset: 0x0001C0F8
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001DF13 File Offset: 0x0001C113
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001DF26 File Offset: 0x0001C126
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0001DF41 File Offset: 0x0001C141
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001DF54 File Offset: 0x0001C154
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + 4 + this._description.Length + 4 + this._language.Length;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelOpenFailureMessage()
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001DF74 File Offset: 0x0001C174
		public ChannelOpenFailureMessage(uint localChannelNumber, string description, uint reasonCode) : this(localChannelNumber, description, reasonCode, "en")
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001DF84 File Offset: 0x0001C184
		public ChannelOpenFailureMessage(uint localChannelNumber, string description, uint reasonCode, string language) : base(localChannelNumber)
		{
			this.Description = description;
			this.ReasonCode = reasonCode;
			this.Language = language;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001DFA3 File Offset: 0x0001C1A3
		protected override void LoadData()
		{
			base.LoadData();
			this.ReasonCode = base.ReadUInt32();
			this._description = base.ReadBinary();
			this._language = base.ReadBinary();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001DFCF File Offset: 0x0001C1CF
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.ReasonCode);
			base.WriteBinaryString(this._description);
			base.WriteBinaryString(this._language);
		}

		// Token: 0x04000307 RID: 775
		internal const uint AdministrativelyProhibited = 1U;

		// Token: 0x04000308 RID: 776
		internal const uint ConnectFailed = 2U;

		// Token: 0x04000309 RID: 777
		internal const uint UnknownChannelType = 3U;

		// Token: 0x0400030A RID: 778
		internal const uint ResourceShortage = 4U;

		// Token: 0x0400030B RID: 779
		private byte[] _description;

		// Token: 0x0400030C RID: 780
		private byte[] _language;
	}
}
