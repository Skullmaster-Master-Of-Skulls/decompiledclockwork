using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000329 RID: 809
	public class IntakeUserAccount : BusinessBase<int>
	{
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		// (set) Token: 0x0600193E RID: 6462 RVA: 0x0001DCFC File Offset: 0x0001BEFC
		public string FirstName { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x0001DD05 File Offset: 0x0001BF05
		// (set) Token: 0x06001940 RID: 6464 RVA: 0x0001DD0D File Offset: 0x0001BF0D
		public string MiddleName { get; set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x0001DD16 File Offset: 0x0001BF16
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x0001DD1E File Offset: 0x0001BF1E
		public string LastName { get; set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0001DD27 File Offset: 0x0001BF27
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x0001DD2F File Offset: 0x0001BF2F
		public string StudentNumber { get; set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0001DD38 File Offset: 0x0001BF38
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public string Email { get; set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x0001DD49 File Offset: 0x0001BF49
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x0001DD51 File Offset: 0x0001BF51
		public string IpAddress { get; set; }
	}
}
