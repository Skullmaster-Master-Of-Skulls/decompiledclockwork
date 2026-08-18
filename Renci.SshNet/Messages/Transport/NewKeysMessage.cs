using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000DA RID: 218
	[Message("SSH_MSG_NEWKEYS", 21)]
	public class NewKeysMessage : Message, IKeyExchangedAllowed
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void LoadData()
		{
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void SaveData()
		{
		}
	}
}
