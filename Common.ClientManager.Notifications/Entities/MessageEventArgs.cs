using System;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.Common.ClientManager.Notifications.Entities
{
	// Token: 0x02000017 RID: 23
	public class MessageEventArgs
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000031C3 File Offset: 0x000013C3
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000031CB File Offset: 0x000013CB
		public InstantMessage InstantMessage { get; set; }

		// Token: 0x060000AB RID: 171 RVA: 0x000028FC File Offset: 0x00000AFC
		public MessageEventArgs()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000031D4 File Offset: 0x000013D4
		public MessageEventArgs(InstantMessage instantMessage)
		{
			this.InstantMessage = instantMessage;
		}
	}
}
