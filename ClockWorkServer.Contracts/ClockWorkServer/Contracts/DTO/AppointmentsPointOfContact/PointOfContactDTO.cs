using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000918 RID: 2328
	[DataContract(Namespace = "http://tpro.ca")]
	public class PointOfContactDTO : BaseExtendedAppointmentDTO
	{
		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0001665C File Offset: 0x0001485C
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x00016664 File Offset: 0x00014864
		[DataMember]
		public AttendeeDTO Student { get; set; }

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0001666D File Offset: 0x0001486D
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x00016675 File Offset: 0x00014875
		[DataMember]
		public AttendeeDTO Staff { get; set; }

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x0001667E File Offset: 0x0001487E
		// (set) Token: 0x06002F31 RID: 12081 RVA: 0x00016686 File Offset: 0x00014886
		[DataMember]
		public List<DynamicDataDTO> SessionNotesData { get; set; }

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x00016690 File Offset: 0x00014890
		// (set) Token: 0x06002F33 RID: 12083 RVA: 0x000166F0 File Offset: 0x000148F0
		public ePointOfContactContext PocContext
		{
			get
			{
				bool flag = base.OverrideColour == null || !Enum.IsDefined(typeof(ePointOfContactContext), base.OverrideColour.Value);
				ePointOfContactContext result;
				if (flag)
				{
					result = ePointOfContactContext.Normal;
				}
				else
				{
					result = (ePointOfContactContext)base.OverrideColour.Value;
				}
				return result;
			}
			set
			{
				bool flag = value == ePointOfContactContext.Normal;
				if (flag)
				{
					base.OverrideColour = null;
				}
				else
				{
					base.OverrideColour = new int?((int)value);
				}
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x00016728 File Offset: 0x00014928
		// (set) Token: 0x06002F35 RID: 12085 RVA: 0x00016748 File Offset: 0x00014948
		[DataMember]
		public override DateTime StartDateTime
		{
			get
			{
				return base.StartDateTime.Date;
			}
			set
			{
				base.StartDateTime = value.Date;
				base.EndDateTime = value.Date.AddHours(1.0);
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x00016784 File Offset: 0x00014984
		public override DateTime EndDateTime
		{
			get
			{
				return this.StartDateTime.AddHours(1.0);
			}
		}
	}
}
