using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000503 RID: 1283
	public class TestExamSettingTypeAttribute : Attribute
	{
		// Token: 0x06002706 RID: 9990 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public TestExamSettingTypeAttribute()
		{
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00029491 File Offset: 0x00027691
		public TestExamSettingTypeAttribute(eClassTestType classTestType)
		{
			this.ClassTestType = classTestType;
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x000294A3 File Offset: 0x000276A3
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x000294AB File Offset: 0x000276AB
		public eClassTestType ClassTestType { get; set; }
	}
}
