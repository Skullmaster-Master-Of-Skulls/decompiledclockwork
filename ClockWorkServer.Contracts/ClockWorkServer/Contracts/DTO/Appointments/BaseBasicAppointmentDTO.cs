using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000935 RID: 2357
	[DataContract(Namespace = "http://tpro.ca")]
	public class BaseBasicAppointmentDTO : ICloneable<BaseBasicAppointmentDTO>, ICloneable
	{
		// Token: 0x06003022 RID: 12322 RVA: 0x00017458 File Offset: 0x00015658
		public BaseBasicAppointmentDTO()
		{
			this.Attendees = new List<AttendeeDTO>();
			this.Location = "";
			this.SubTitle = "";
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x00017488 File Offset: 0x00015688
		public BaseBasicAppointmentDTO(BaseBasicAppointmentDTO item)
		{
			bool flag = item == null;
			if (flag)
			{
				this.Attendees = new List<AttendeeDTO>();
				this.Location = "";
				this.SubTitle = "";
			}
			else
			{
				BaseBasicAppointmentDTO.CopyBaseBasicAppointment(item, this);
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000174D4 File Offset: 0x000156D4
		public static void CopyBaseBasicAppointment(BaseBasicAppointmentDTO from, BaseBasicAppointmentDTO to)
		{
			to.AppointmentId = from.AppointmentId;
			to.AppType = ((from.AppType == null) ? null : from.AppType.Clone());
			to.ShowTimeAs = ((from.ShowTimeAs == null) ? null : from.ShowTimeAs.Clone());
			to.StartDateTime = from.StartDateTime;
			to.EndDateTime = from.EndDateTime;
			to.SubTitle = from.SubTitle;
			to.IsCancelled = from.IsCancelled;
			to.IsLocked = from.IsLocked;
			to.IsPrivate = from.IsPrivate;
			to.GroupCode = from.GroupCode;
			bool flag = from.Attendees == null;
			if (flag)
			{
				to.Attendees = new List<AttendeeDTO>();
			}
			else
			{
				to.Attendees = from.Attendees.ToList<AttendeeDTO>().ConvertAll<AttendeeDTO>((AttendeeDTO g) => (g == null) ? null : g.Clone());
			}
			to.Location = from.Location;
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000175E2 File Offset: 0x000157E2
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x000175EA File Offset: 0x000157EA
		[DataMember]
		public virtual int AppointmentId { get; set; }

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000175F3 File Offset: 0x000157F3
		// (set) Token: 0x06003028 RID: 12328 RVA: 0x000175FB File Offset: 0x000157FB
		[DataMember]
		public virtual AppTypeDTO AppType { get; set; }

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x00017604 File Offset: 0x00015804
		// (set) Token: 0x0600302A RID: 12330 RVA: 0x0001760C File Offset: 0x0001580C
		[DataMember]
		public virtual AppShowTimeAsTypeDTO ShowTimeAs { get; set; }

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x00017615 File Offset: 0x00015815
		// (set) Token: 0x0600302C RID: 12332 RVA: 0x0001761D File Offset: 0x0001581D
		[DataMember]
		public virtual DateTime StartDateTime { get; set; }

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x00017626 File Offset: 0x00015826
		// (set) Token: 0x0600302E RID: 12334 RVA: 0x0001762E File Offset: 0x0001582E
		[DataMember]
		public virtual DateTime EndDateTime { get; set; }

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x00017637 File Offset: 0x00015837
		// (set) Token: 0x06003030 RID: 12336 RVA: 0x0001763F File Offset: 0x0001583F
		[DataMember]
		public virtual string SubTitle { get; set; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x00017648 File Offset: 0x00015848
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x00017650 File Offset: 0x00015850
		[DataMember]
		public virtual bool IsCancelled { get; set; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x00017659 File Offset: 0x00015859
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x00017661 File Offset: 0x00015861
		[DataMember]
		public virtual bool IsLocked { get; set; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x0001766A File Offset: 0x0001586A
		// (set) Token: 0x06003036 RID: 12342 RVA: 0x00017672 File Offset: 0x00015872
		[DataMember]
		public virtual bool IsPrivate { get; set; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x0001767B File Offset: 0x0001587B
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x00017683 File Offset: 0x00015883
		[DataMember]
		public virtual int GroupCode { get; set; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x0001768C File Offset: 0x0001588C
		// (set) Token: 0x0600303A RID: 12346 RVA: 0x00017694 File Offset: 0x00015894
		[DataMember]
		public virtual List<AttendeeDTO> Attendees { get; set; }

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x0001769D File Offset: 0x0001589D
		// (set) Token: 0x0600303C RID: 12348 RVA: 0x000176A5 File Offset: 0x000158A5
		[DataMember]
		public virtual string Location { get; set; }

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000176B0 File Offset: 0x000158B0
		public virtual bool IsTentative
		{
			get
			{
				return this.ShowTimeAs != null && this.ShowTimeAs.IsTentative;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x000176D8 File Offset: 0x000158D8
		public virtual bool IsRecurring
		{
			get
			{
				return this.GroupCode > 0;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000176F4 File Offset: 0x000158F4
		public virtual bool IsPointOfContact
		{
			get
			{
				return this.StartDateTime.Hour == 0 && this.EndDateTime.Hour == 1 && this.StartDateTime.Minute == 0 && this.EndDateTime.Minute == 0;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0001774C File Offset: 0x0001594C
		public virtual bool IsAllDay
		{
			get
			{
				return this.StartDateTime.Hour == 0 && this.StartDateTime.Minute == 1 && this.EndDateTime.Hour == 23 && this.EndDateTime.Minute == 59;
			}
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000177A8 File Offset: 0x000159A8
		public BaseBasicAppointmentDTO Clone()
		{
			return new BaseBasicAppointmentDTO(this);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000177C0 File Offset: 0x000159C0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
