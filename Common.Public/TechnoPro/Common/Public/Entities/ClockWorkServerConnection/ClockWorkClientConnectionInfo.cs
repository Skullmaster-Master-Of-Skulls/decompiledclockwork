using System;
using TechnoPro.Common.Public.Entities.Database;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerConnection
{
	// Token: 0x0200044E RID: 1102
	public class ClockWorkClientConnectionInfo : BusinessBase<string>
	{
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x000255F4 File Offset: 0x000237F4
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Version
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0002560C File Offset: 0x0002380C
		// (set) Token: 0x0600215E RID: 8542 RVA: 0x00025614 File Offset: 0x00023814
		public ClockWorkServerPreferredConnectionInfo ServerPreferredConnection { get; set; }

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x0002561D File Offset: 0x0002381D
		// (set) Token: 0x06002160 RID: 8544 RVA: 0x00025625 File Offset: 0x00023825
		public DbConnectionInfo DatabaseConnection { get; set; }
	}
}
