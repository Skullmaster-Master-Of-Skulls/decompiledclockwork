using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.ClassTestDefinitionViews
{
	// Token: 0x0200052D RID: 1325
	public class ClassTestDefinitionSummary : ClassTest
	{
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x0002AFC5 File Offset: 0x000291C5
		// (set) Token: 0x06002A09 RID: 10761 RVA: 0x0002AFCD File Offset: 0x000291CD
		public IList<IDynamicDataSerializableItem> InstructorFormData { get; set; }
	}
}
