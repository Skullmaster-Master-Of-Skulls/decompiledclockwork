using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000DD RID: 221
	[Message("SSH_MSG_UNIMPLEMENTED", 3)]
	public class UnimplementedMessage : Message
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void LoadData()
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void SaveData()
		{
			throw new NotImplementedException();
		}
	}
}
