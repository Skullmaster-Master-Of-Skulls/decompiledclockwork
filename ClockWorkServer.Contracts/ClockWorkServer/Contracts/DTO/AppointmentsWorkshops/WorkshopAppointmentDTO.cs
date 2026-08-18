using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F2 RID: 2290
	[DataContract(Namespace = "http://tpro.ca")]
	public class WorkshopAppointmentDTO : BaseExtendedAppointmentDTO, ICloneable<WorkshopAppointmentDTO>, ICloneable
	{
		// Token: 0x06002EA0 RID: 11936 RVA: 0x00016158 File Offset: 0x00014358
		public WorkshopAppointmentDTO()
		{
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00016164 File Offset: 0x00014364
		public WorkshopAppointmentDTO(WorkshopAppointmentDTO item)
		{
			WorkshopAppointmentDTO clone = BaseExtendedAppointmentDTO.GetClone<WorkshopAppointmentDTO>(this, item);
			bool flag = clone == null;
			if (!flag)
			{
				IList<AppointmentIconDTO> icons;
				if (item.Icons != null)
				{
					icons = (from g in item.Icons
					select g.Clone()).ToList<AppointmentIconDTO>();
				}
				else
				{
					icons = null;
				}
				this.Icons = icons;
				this.MaxAttendeeCount = item.MaxAttendeeCount;
				this.WorkshopDefinition = ((item.WorkshopDefinition == null) ? null : item.WorkshopDefinition.Clone());
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000161F6 File Offset: 0x000143F6
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000161FE File Offset: 0x000143FE
		[DataMember]
		public IList<AppointmentIconDTO> Icons { get; set; }

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x00016207 File Offset: 0x00014407
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x0001620F File Offset: 0x0001440F
		[DataMember]
		public virtual int MaxAttendeeCount { get; set; }

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x00016218 File Offset: 0x00014418
		// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x00016220 File Offset: 0x00014420
		[DataMember]
		public WorkshopDefinitionDTO WorkshopDefinition { get; set; }

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0001622C File Offset: 0x0001442C
		public new WorkshopAppointmentDTO Clone()
		{
			return new WorkshopAppointmentDTO(this);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00016244 File Offset: 0x00014444
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
