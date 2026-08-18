using System;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000CB RID: 203
	[Message("SSH_MSG_USERAUTH_SUCCESS", 52)]
	public class SuccessMessage : Message
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void LoadData()
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void SaveData()
		{
		}
	}
}
