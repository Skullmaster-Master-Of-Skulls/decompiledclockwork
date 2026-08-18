using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.DAO.MailMerging
{
	// Token: 0x02000053 RID: 83
	public interface IMailMergingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001CB RID: 459
		List<DynamicData> LoadAllPerStudentData(int PersonId);

		// Token: 0x060001CC RID: 460
		List<DynamicData> LoadAllPerDateData(int PersonId, int PerDateId);

		// Token: 0x060001CD RID: 461
		List<DynamicData> LoadAllAccommodationTemplateData(int PersonId);

		// Token: 0x060001CE RID: 462
		List<DynamicData> LoadAllPerAppointmentData(int PersonId, int AppointmentId);

		// Token: 0x060001CF RID: 463
		MailMergeContext LoadSampleContextFromDatabase(int OptionalPersonId, int OptionalAppointmentId);
	}
}
