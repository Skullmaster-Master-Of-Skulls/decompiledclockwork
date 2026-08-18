using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004B3 RID: 1203
	public class BaseBasicAppointment : BusinessBase<int>
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x000277D0 File Offset: 0x000259D0
		public BaseBasicAppointment()
		{
			this.Attendees = new List<Attendee>();
			this.Location = "";
			this.SubTitle = "";
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x00027800 File Offset: 0x00025A00
		// (set) Token: 0x06002456 RID: 9302 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x00027818 File Offset: 0x00025A18
		// (set) Token: 0x06002458 RID: 9304 RVA: 0x00027820 File Offset: 0x00025A20
		public virtual AppType AppType { get; set; }

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x00027829 File Offset: 0x00025A29
		// (set) Token: 0x0600245A RID: 9306 RVA: 0x00027831 File Offset: 0x00025A31
		public virtual AppShowTimeAsType ShowTimeAs { get; set; }

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x0002783A File Offset: 0x00025A3A
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x00027842 File Offset: 0x00025A42
		public virtual DateTime StartDateTime { get; set; }

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x0002784B File Offset: 0x00025A4B
		// (set) Token: 0x0600245E RID: 9310 RVA: 0x00027853 File Offset: 0x00025A53
		public virtual DateTime EndDateTime { get; set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0002785C File Offset: 0x00025A5C
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x00027864 File Offset: 0x00025A64
		public virtual string SubTitle { get; set; }

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x0002786D File Offset: 0x00025A6D
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x00027875 File Offset: 0x00025A75
		public virtual bool IsCancelled { get; set; }

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x0002787E File Offset: 0x00025A7E
		// (set) Token: 0x06002464 RID: 9316 RVA: 0x00027886 File Offset: 0x00025A86
		public virtual bool IsLocked { get; set; }

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x0002788F File Offset: 0x00025A8F
		// (set) Token: 0x06002466 RID: 9318 RVA: 0x00027897 File Offset: 0x00025A97
		public virtual bool IsPrivate { get; set; }

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000278A0 File Offset: 0x00025AA0
		// (set) Token: 0x06002468 RID: 9320 RVA: 0x000278A8 File Offset: 0x00025AA8
		public virtual int GroupCode { get; set; }

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000278B1 File Offset: 0x00025AB1
		// (set) Token: 0x0600246A RID: 9322 RVA: 0x000278B9 File Offset: 0x00025AB9
		public virtual List<Attendee> Attendees { get; set; }

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x000278C2 File Offset: 0x00025AC2
		// (set) Token: 0x0600246C RID: 9324 RVA: 0x000278CA File Offset: 0x00025ACA
		public virtual string Location { get; set; }

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000278D3 File Offset: 0x00025AD3
		public virtual bool IsTentative
		{
			get
			{
				return this.ShowTimeAs != null && this.ShowTimeAs.IsTentative;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x000278EB File Offset: 0x00025AEB
		public virtual bool IsRecurring
		{
			get
			{
				return this.GroupCode > 0;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000278F8 File Offset: 0x00025AF8
		public virtual bool IsPointOfContact
		{
			get
			{
				return this.StartDateTime.Hour == 0 && this.EndDateTime.Hour == 1 && this.StartDateTime.Minute == 0 && this.EndDateTime.Minute == 0;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x0002794C File Offset: 0x00025B4C
		public virtual bool IsAllDay
		{
			get
			{
				return this.StartDateTime.Hour == 0 && this.StartDateTime.Minute == 1 && this.EndDateTime.Hour == 23 && this.EndDateTime.Minute == 59;
			}
		}
	}
}
