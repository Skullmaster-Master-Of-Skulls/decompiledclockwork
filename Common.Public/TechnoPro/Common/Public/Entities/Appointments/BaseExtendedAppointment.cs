using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004B4 RID: 1204
	public class BaseExtendedAppointment : BaseBasicAppointment
	{
		// Token: 0x06002471 RID: 9329 RVA: 0x000279A1 File Offset: 0x00025BA1
		public BaseExtendedAppointment()
		{
			this.CancelInfo = new AppCancelInfo();
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000279B7 File Offset: 0x00025BB7
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x000279BF File Offset: 0x00025BBF
		public virtual string Memo { get; set; }

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000279C8 File Offset: 0x00025BC8
		// (set) Token: 0x06002475 RID: 9333 RVA: 0x000279D0 File Offset: 0x00025BD0
		public virtual PersonBase WhoBooked { get; set; }

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x000279D9 File Offset: 0x00025BD9
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x000279E1 File Offset: 0x00025BE1
		public virtual DateTime DateBooked { get; set; }

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000279EA File Offset: 0x00025BEA
		// (set) Token: 0x06002479 RID: 9337 RVA: 0x000279F2 File Offset: 0x00025BF2
		public virtual int ExtraAttendeesCount { get; set; }

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x000279FB File Offset: 0x00025BFB
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x00027A03 File Offset: 0x00025C03
		public virtual AppointmentRoom Room { get; set; }

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x00027A0C File Offset: 0x00025C0C
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x00027A14 File Offset: 0x00025C14
		public virtual AppCancelInfo CancelInfo { get; set; }

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x00027A1D File Offset: 0x00025C1D
		// (set) Token: 0x0600247F RID: 9343 RVA: 0x00027A25 File Offset: 0x00025C25
		public int? OverrideColour { get; set; }

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x00027A2E File Offset: 0x00025C2E
		// (set) Token: 0x06002481 RID: 9345 RVA: 0x00027A36 File Offset: 0x00025C36
		public DateTime? ActualStartDateTime { get; set; }

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x00027A3F File Offset: 0x00025C3F
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x00027A47 File Offset: 0x00025C47
		public DateTime? ActualEndDateTime { get; set; }
	}
}
