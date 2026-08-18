using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200056F RID: 1391
	public class StudentAppointmentBookingRuleTypeAttribute : Attribute
	{
		// Token: 0x06002CD9 RID: 11481 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public StudentAppointmentBookingRuleTypeAttribute()
		{
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x00031C56 File Offset: 0x0002FE56
		public StudentAppointmentBookingRuleTypeAttribute(eStudentAppointmentBookingRuleAppliesTo appliesTo, string managerClassName)
		{
			this.AppliesTo = appliesTo;
			this.ManagerClassName = managerClassName;
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x00031C70 File Offset: 0x0002FE70
		// (set) Token: 0x06002CDC RID: 11484 RVA: 0x00031C78 File Offset: 0x0002FE78
		public eStudentAppointmentBookingRuleAppliesTo AppliesTo { get; set; }

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x00031C81 File Offset: 0x0002FE81
		// (set) Token: 0x06002CDE RID: 11486 RVA: 0x00031C89 File Offset: 0x0002FE89
		public string ManagerClassName { get; set; }
	}
}
