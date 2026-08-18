using System;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C0 RID: 192
	[Message("SSH_MSG_USERAUTH_FAILURE", 51)]
	public class FailureMessage : Message
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001F185 File Offset: 0x0001D385
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0001F18D File Offset: 0x0001D38D
		public string[] AllowedAuthentications { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001F196 File Offset: 0x0001D396
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001F19E File Offset: 0x0001D39E
		public string Message { get; private set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001F1A7 File Offset: 0x0001D3A7
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001F1AF File Offset: 0x0001D3AF
		public bool PartialSuccess { get; private set; }

		// Token: 0x060008BB RID: 2235 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		protected override void LoadData()
		{
			this.AllowedAuthentications = base.ReadNamesList();
			this.PartialSuccess = base.ReadBoolean();
			if (this.PartialSuccess)
			{
				this.Message = string.Join(",", this.AllowedAuthentications);
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void SaveData()
		{
			throw new NotImplementedException();
		}
	}
}
