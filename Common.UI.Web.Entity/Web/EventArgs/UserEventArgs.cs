using System;

namespace TechnoPro.Common.UI.Web.Entity.Web.EventArgs
{
	// Token: 0x0200001D RID: 29
	public class UserEventArgs : EventArgs
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000275E File Offset: 0x0000095E
		public UserEventArgs()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000027F1 File Offset: 0x000009F1
		public UserEventArgs(int pid)
		{
			this.PersonId = pid;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002803 File Offset: 0x00000A03
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000280B File Offset: 0x00000A0B
		public int PersonId { get; set; }
	}
}
