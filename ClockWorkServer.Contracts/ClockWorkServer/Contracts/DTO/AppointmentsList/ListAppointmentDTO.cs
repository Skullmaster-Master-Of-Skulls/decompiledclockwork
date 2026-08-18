using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC2 RID: 2754
	[DataContract(Namespace = "http://tpro.ca")]
	public class ListAppointmentDTO : BaseExtendedAppointmentDTO, ICloneable<ListAppointmentDTO>, ICloneable
	{
		// Token: 0x06003A69 RID: 14953 RVA: 0x00016158 File Offset: 0x00014358
		public ListAppointmentDTO()
		{
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x0001C558 File Offset: 0x0001A758
		public ListAppointmentDTO(ListAppointmentDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				BaseExtendedAppointmentDTO.CopyBaseExtendedAppointment(item, this);
				this.IsStudentsFirstApp = item.IsStudentsFirstApp;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x0001C58C File Offset: 0x0001A78C
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x0001C594 File Offset: 0x0001A794
		[DataMember]
		public bool IsStudentsFirstApp { get; set; }

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x0001C5A0 File Offset: 0x0001A7A0
		public int WhoBookedPersonId
		{
			get
			{
				return (this.WhoBooked == null) ? 0 : this.WhoBooked.PersonId;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public bool IsIn
		{
			get
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				return attendeeDTO != null && attendeeDTO.MiscCode == 2;
			}
			set
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				bool flag = attendeeDTO != null;
				if (flag)
				{
					attendeeDTO.MiscCode = (value ? 2 : 0);
				}
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x0001C620 File Offset: 0x0001A820
		// (set) Token: 0x06003A71 RID: 14961 RVA: 0x0001C64C File Offset: 0x0001A84C
		public bool IsConfirmed
		{
			get
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				return attendeeDTO != null && attendeeDTO.MiscCode == 4;
			}
			set
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				bool flag = attendeeDTO != null;
				if (flag)
				{
					bool flag2 = attendeeDTO.MiscCode < 1 || (!value && attendeeDTO.MiscCode == 4);
					if (flag2)
					{
						attendeeDTO.MiscCode = (value ? 4 : 0);
					}
				}
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x0001C698 File Offset: 0x0001A898
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
		public bool IsNoShow
		{
			get
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				bool flag = attendeeDTO == null;
				return !flag && attendeeDTO.IsNoShow;
			}
			set
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				bool flag = attendeeDTO != null;
				if (flag)
				{
					attendeeDTO.IsNoShow = value;
				}
			}
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		private AttendeeDTO FindAttendeeByCoreGroup(eCoreGroupDTO coreGroup)
		{
			bool flag = this.Attendees != null;
			if (flag)
			{
				AttendeeDTO attendeeDTO = this.Attendees.Find((AttendeeDTO f) => f.Person.CoreGroup == coreGroup);
				bool flag2 = attendeeDTO != null;
				if (flag2)
				{
					return attendeeDTO;
				}
			}
			return null;
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x0001C744 File Offset: 0x0001A944
		// (set) Token: 0x06003A76 RID: 14966 RVA: 0x0001C76C File Offset: 0x0001A96C
		public PersonBaseDTO Student
		{
			get
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				return (attendeeDTO == null) ? null : attendeeDTO.Person;
			}
			set
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Students);
				bool flag = attendeeDTO != null;
				AttendeeDTO attendeeDTO2;
				if (flag)
				{
					attendeeDTO2 = attendeeDTO;
				}
				else
				{
					attendeeDTO2 = new AttendeeDTO();
					this.Attendees.Add(attendeeDTO2);
				}
				attendeeDTO2.Person = value;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		// (set) Token: 0x06003A78 RID: 14968 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		public DateTime StartDate
		{
			get
			{
				return this.StartDateTime;
			}
			set
			{
				this.StartDateTime = value;
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		// (set) Token: 0x06003A7A RID: 14970 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public DateTime EndDate
		{
			get
			{
				return this.EndDateTime;
			}
			set
			{
				this.EndDateTime = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		// (set) Token: 0x06003A7C RID: 14972 RVA: 0x0001C81C File Offset: 0x0001AA1C
		public PersonBaseDTO Staff
		{
			get
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Staff);
				return (attendeeDTO == null) ? null : attendeeDTO.Person;
			}
			set
			{
				AttendeeDTO attendeeDTO = this.FindAttendeeByCoreGroup(eCoreGroupDTO.Staff);
				bool flag = attendeeDTO != null;
				AttendeeDTO attendeeDTO2;
				if (flag)
				{
					attendeeDTO2 = attendeeDTO;
				}
				else
				{
					attendeeDTO2 = new AttendeeDTO();
					this.Attendees.Add(attendeeDTO2);
				}
				attendeeDTO2.Person = value;
			}
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0001C85C File Offset: 0x0001AA5C
		public new ListAppointmentDTO Clone()
		{
			return new ListAppointmentDTO(this);
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0001C874 File Offset: 0x0001AA74
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
