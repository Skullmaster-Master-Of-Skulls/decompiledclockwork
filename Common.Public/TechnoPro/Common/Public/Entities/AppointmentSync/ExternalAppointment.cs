using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004E1 RID: 1249
	public class ExternalAppointment : BusinessBase<string>
	{
		// Token: 0x060025BE RID: 9662 RVA: 0x000285E4 File Offset: 0x000267E4
		public ExternalAppointment()
		{
			this.Id = "";
			this.Memo = "";
			this.Location = "";
			this.Subject = "";
			this.Mapping = new ClockWorkExternalAppMapping
			{
				ClockWorkAppointmentId = 0,
				ExternalApplicationUniqueAppointmentId = "",
				ExternalApplicationUniqueAppointmentId2 = "",
				ExternalApplicationGlobalAppointmentId = "",
				ClockWorkLastUpdatedDate = null,
				ExternalApplicationLastUpdatedDate = null
			};
			this.Attendees = new List<ExternalAttendee>();
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x00028690 File Offset: 0x00026890
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x0000E9FC File Offset: 0x0000CBFC
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

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000286A8 File Offset: 0x000268A8
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x000286B0 File Offset: 0x000268B0
		public virtual string LegacyGlobalAppointmentId { get; set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000286B9 File Offset: 0x000268B9
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x000286C1 File Offset: 0x000268C1
		public virtual string UniqueId2 { get; set; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000286CA File Offset: 0x000268CA
		// (set) Token: 0x060025C6 RID: 9670 RVA: 0x000286D2 File Offset: 0x000268D2
		public DateTime StartDate { get; set; }

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000286DB File Offset: 0x000268DB
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x000286E3 File Offset: 0x000268E3
		public DateTime EndDate { get; set; }

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000286EC File Offset: 0x000268EC
		// (set) Token: 0x060025CA RID: 9674 RVA: 0x000286F4 File Offset: 0x000268F4
		public string Memo { get; set; }

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000286FD File Offset: 0x000268FD
		// (set) Token: 0x060025CC RID: 9676 RVA: 0x00028705 File Offset: 0x00026905
		public IList<ExternalAttendee> Attendees { get; set; }

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0002870E File Offset: 0x0002690E
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x00028716 File Offset: 0x00026916
		public bool IsCancelled { get; set; }

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0002871F File Offset: 0x0002691F
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x00028727 File Offset: 0x00026927
		public bool IsPrivate { get; set; }

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00028730 File Offset: 0x00026930
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x00028738 File Offset: 0x00026938
		public bool IsFromCache { get; set; }

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00028741 File Offset: 0x00026941
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x00028749 File Offset: 0x00026949
		public string Location { get; set; }

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x0002875A File Offset: 0x0002695A
		public string Subject { get; set; }

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x00028763 File Offset: 0x00026963
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x0002876B File Offset: 0x0002696B
		public ClockWorkExternalAppMapping Mapping { get; set; }

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00028774 File Offset: 0x00026974
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x0002877C File Offset: 0x0002697C
		public DateTime LastModifiedTime { get; set; }

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00028785 File Offset: 0x00026985
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x0002878D File Offset: 0x0002698D
		public bool IsAllDayEvent { get; set; }

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x00028796 File Offset: 0x00026996
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x0002879E File Offset: 0x0002699E
		public bool IsRecurring { get; set; }

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000287A7 File Offset: 0x000269A7
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x000287AF File Offset: 0x000269AF
		public ExternalAppointmentType AppointmentType { get; set; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000287B8 File Offset: 0x000269B8
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x00028867 File Offset: 0x00026A67
		public ExternalAttendee Organizer
		{
			get
			{
				bool flag = this._organizer != null;
				ExternalAttendee result;
				if (flag)
				{
					result = this._organizer;
				}
				else
				{
					bool flag2 = this.Attendees != null && this.Attendees.Count > 0;
					if (flag2)
					{
						this._organizer = ((List<ExternalAttendee>)this.Attendees).Find((ExternalAttendee a) => a.AttendeeType == eAttendeeType.EVENT_ORGANIZER);
						bool flag3 = this._organizer != null;
						if (flag3)
						{
							result = this._organizer;
						}
						else
						{
							result = (this._organizer = this.Attendees[0]);
						}
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
			set
			{
				this._organizer = value;
			}
		}

		// Token: 0x04001BFF RID: 7167
		private ExternalAttendee _organizer;
	}
}
