using System;

namespace TechnoPro.Common.Public.Entities.Notetaking
{
	// Token: 0x02000283 RID: 643
	public class NotetakerBase : BusinessBase<int>
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0001977C File Offset: 0x0001797C
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderId
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

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00019794 File Offset: 0x00017994
		// (set) Token: 0x06001380 RID: 4992 RVA: 0x0001979C File Offset: 0x0001799C
		public string FirstName { get; set; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x000197A5 File Offset: 0x000179A5
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x000197AD File Offset: 0x000179AD
		public string MiddleName { get; set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x000197B6 File Offset: 0x000179B6
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x000197BE File Offset: 0x000179BE
		public string Username { get; set; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000197C7 File Offset: 0x000179C7
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x000197CF File Offset: 0x000179CF
		public string Student_no { get; set; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x000197D8 File Offset: 0x000179D8
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x000197E0 File Offset: 0x000179E0
		public string LastName { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x000197E9 File Offset: 0x000179E9
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x000197F1 File Offset: 0x000179F1
		public string Email { get; set; }
	}
}
