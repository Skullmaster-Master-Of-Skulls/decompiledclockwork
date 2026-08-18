using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000511 RID: 1297
	public class SittingBase : BusinessBase<int>
	{
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x00029954 File Offset: 0x00027B54
		// (set) Token: 0x06002793 RID: 10131 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SittingId
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

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x0002996C File Offset: 0x00027B6C
		// (set) Token: 0x06002795 RID: 10133 RVA: 0x00029974 File Offset: 0x00027B74
		public AppointmentRoom Room { get; set; }

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x0002997D File Offset: 0x00027B7D
		// (set) Token: 0x06002797 RID: 10135 RVA: 0x00029985 File Offset: 0x00027B85
		public string Location { get; set; }

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x0002998E File Offset: 0x00027B8E
		// (set) Token: 0x06002799 RID: 10137 RVA: 0x00029996 File Offset: 0x00027B96
		public string Title { get; set; }

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x0002999F File Offset: 0x00027B9F
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x000299A7 File Offset: 0x00027BA7
		public DateTime ExamDate { get; set; }

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000299B0 File Offset: 0x00027BB0
		// (set) Token: 0x0600279D RID: 10141 RVA: 0x000299B8 File Offset: 0x00027BB8
		public PersonBase Invigilator { get; set; }

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000299C1 File Offset: 0x00027BC1
		// (set) Token: 0x0600279F RID: 10143 RVA: 0x000299C9 File Offset: 0x00027BC9
		public bool Cancelled { get; set; }

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000299D2 File Offset: 0x00027BD2
		// (set) Token: 0x060027A1 RID: 10145 RVA: 0x000299DA File Offset: 0x00027BDA
		public DateTime? ScheduledStartDateTime { get; set; }

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000299E3 File Offset: 0x00027BE3
		// (set) Token: 0x060027A3 RID: 10147 RVA: 0x000299EB File Offset: 0x00027BEB
		public DateTime? ScheduledEndDateTime { get; set; }

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x000299F4 File Offset: 0x00027BF4
		// (set) Token: 0x060027A5 RID: 10149 RVA: 0x000299FC File Offset: 0x00027BFC
		public bool IsPrivate { get; set; }
	}
}
