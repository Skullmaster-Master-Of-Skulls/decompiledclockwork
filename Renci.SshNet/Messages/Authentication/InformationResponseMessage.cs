using System;
using System.Collections.Generic;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C2 RID: 194
	[Message("SSH_MSG_USERAUTH_INFO_RESPONSE", 61)]
	internal class InformationResponseMessage : Message
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001F2B9 File Offset: 0x0001D4B9
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0001F2C1 File Offset: 0x0001D4C1
		public IList<string> Responses { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001EA45 File Offset: 0x0001CC45
		protected override int BufferCapacity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001F2CA File Offset: 0x0001D4CA
		public InformationResponseMessage()
		{
			this.Responses = new List<string>();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void LoadData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
		protected override void SaveData()
		{
			base.Write((uint)this.Responses.Count);
			foreach (string data in this.Responses)
			{
				base.Write(data);
			}
		}
	}
}
