using System;

namespace TechnoPro.Common.Public.Entities.Notifications
{
	// Token: 0x0200027C RID: 636
	public class Notification : BusinessBase<string>
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00019430 File Offset: 0x00017630
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string UniqueId
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

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00019448 File Offset: 0x00017648
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x00019450 File Offset: 0x00017650
		public string Title { get; set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00019459 File Offset: 0x00017659
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x00019461 File Offset: 0x00017661
		public string Description { get; set; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0001946A File Offset: 0x0001766A
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x00019472 File Offset: 0x00017672
		public NotificationContext Context { get; set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0001947B File Offset: 0x0001767B
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x00019483 File Offset: 0x00017683
		public eNotificationType NotificationType { get; set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0001948C File Offset: 0x0001768C
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x00019494 File Offset: 0x00017694
		public eNotificationPriority Priority { get; set; }
	}
}
