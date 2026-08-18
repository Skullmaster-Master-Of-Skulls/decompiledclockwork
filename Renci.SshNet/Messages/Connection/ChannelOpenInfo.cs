using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A3 RID: 163
	public abstract class ChannelOpenInfo : SshData
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007CA RID: 1994
		public abstract string ChannelType { get; }

		// Token: 0x060007CB RID: 1995 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void LoadData()
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void SaveData()
		{
		}
	}
}
