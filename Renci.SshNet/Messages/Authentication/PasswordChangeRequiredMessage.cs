using System;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C3 RID: 195
	[Message("SSH_MSG_USERAUTH_PASSWD_CHANGEREQ", 60)]
	internal class PasswordChangeRequiredMessage : Message
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001F340 File Offset: 0x0001D540
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0001F348 File Offset: 0x0001D548
		public byte[] Message { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0001F351 File Offset: 0x0001D551
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0001F359 File Offset: 0x0001D559
		public byte[] Language { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0001F362 File Offset: 0x0001D562
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Message.Length + 4 + this.Language.Length;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001F380 File Offset: 0x0001D580
		protected override void LoadData()
		{
			this.Message = base.ReadBinary();
			this.Language = base.ReadBinary();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001F39A File Offset: 0x0001D59A
		protected override void SaveData()
		{
			base.WriteBinaryString(this.Message);
			base.WriteBinaryString(this.Language);
		}
	}
}
