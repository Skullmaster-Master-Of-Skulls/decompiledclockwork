using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004AC RID: 1196
	public class AppointmentForNotification : BusinessBase<int>
	{
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000274B8 File Offset: 0x000256B8
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
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

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000274D0 File Offset: 0x000256D0
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x000274D8 File Offset: 0x000256D8
		public int[] AttendeePersonIds { get; set; }

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000274E1 File Offset: 0x000256E1
		// (set) Token: 0x06002410 RID: 9232 RVA: 0x000274E9 File Offset: 0x000256E9
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000274F2 File Offset: 0x000256F2
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x000274FA File Offset: 0x000256FA
		public DateTime EndDateTime { get; set; }
	}
}
