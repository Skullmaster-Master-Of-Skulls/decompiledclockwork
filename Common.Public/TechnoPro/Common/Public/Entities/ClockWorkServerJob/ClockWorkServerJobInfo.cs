using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000454 RID: 1108
	[Serializable]
	public class ClockWorkServerJobInfo : BusinessBase<int>
	{
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000257FC File Offset: 0x000239FC
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x0000E258 File Offset: 0x0000C458
		public int JobId
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

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x00025814 File Offset: 0x00023A14
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x0002581C File Offset: 0x00023A1C
		public string Title { get; set; }

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x00025825 File Offset: 0x00023A25
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0002582D File Offset: 0x00023A2D
		public string Notes { get; set; }

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x00025836 File Offset: 0x00023A36
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x0002583E File Offset: 0x00023A3E
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x00025847 File Offset: 0x00023A47
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x0002584F File Offset: 0x00023A4F
		public ClockWorkServerJobSchedule JobSchedule { get; set; }

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x00025858 File Offset: 0x00023A58
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x00025860 File Offset: 0x00023A60
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x00025869 File Offset: 0x00023A69
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x00025871 File Offset: 0x00023A71
		public bool IsActive { get; set; }

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0002587A File Offset: 0x00023A7A
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x00025882 File Offset: 0x00023A82
		public Guid JobUniqueId { get; set; }

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0002588B File Offset: 0x00023A8B
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x00025893 File Offset: 0x00023A93
		public DateTime? LastRunStartDatetime { get; set; }

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0002589C File Offset: 0x00023A9C
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x000258A4 File Offset: 0x00023AA4
		public DateTime? LastRunEndDatetime { get; set; }

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000258AD File Offset: 0x00023AAD
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x000258B5 File Offset: 0x00023AB5
		public eClockWorkServerJobResult LastRunStatus { get; set; }

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000258BE File Offset: 0x00023ABE
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x000258C6 File Offset: 0x00023AC6
		public string LastRunMessage { get; set; }

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000258CF File Offset: 0x00023ACF
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x000258D7 File Offset: 0x00023AD7
		public IList<ClockWorkServerJobStep> JobSteps { get; set; }

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000258E0 File Offset: 0x00023AE0
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x000258E8 File Offset: 0x00023AE8
		public ClockWorkServerJobInfo.Credentials Impersonate { get; set; }

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000258F1 File Offset: 0x00023AF1
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x000258F9 File Offset: 0x00023AF9
		public bool IsSystemJob { get; set; }

		// Token: 0x060021B6 RID: 8630 RVA: 0x00025902 File Offset: 0x00023B02
		public ClockWorkServerJobInfo()
		{
			this.Timeout = TimeSpan.FromHours(20.0);
		}

		// Token: 0x0200060E RID: 1550
		[Serializable]
		public class Credentials
		{
			// Token: 0x170013F8 RID: 5112
			// (get) Token: 0x0600314C RID: 12620 RVA: 0x00044F07 File Offset: 0x00043107
			// (set) Token: 0x0600314D RID: 12621 RVA: 0x00044F0F File Offset: 0x0004310F
			public string Domain { get; set; }

			// Token: 0x170013F9 RID: 5113
			// (get) Token: 0x0600314E RID: 12622 RVA: 0x00044F18 File Offset: 0x00043118
			// (set) Token: 0x0600314F RID: 12623 RVA: 0x00044F20 File Offset: 0x00043120
			public string Username { get; set; }

			// Token: 0x170013FA RID: 5114
			// (get) Token: 0x06003150 RID: 12624 RVA: 0x00044F29 File Offset: 0x00043129
			// (set) Token: 0x06003151 RID: 12625 RVA: 0x00044F31 File Offset: 0x00043131
			public string Password { get; set; }
		}
	}
}
