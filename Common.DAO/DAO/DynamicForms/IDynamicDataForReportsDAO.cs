using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000081 RID: 129
	public interface IDynamicDataForReportsDAO
	{
		// Token: 0x06000347 RID: 839
		DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns);

		// Token: 0x06000348 RID: 840
		DataTable LoadPerAppointmentDataForMultipleStudentsAsDataTable(IList<DynamicDataContext> Contexts, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns);

		// Token: 0x06000349 RID: 841
		DataTable LoadPerDateDataForMultipleStudentsAsDataTable(IList<DynamicDataContext> Contexts, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns);

		// Token: 0x0600034A RID: 842
		DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<DynamicDataContext> Contexts, IList<int> ControlIds, out IDictionary<eDynamicDataSpecialType, IList<DynamicDataColumn>> specialDataColumns);

		// Token: 0x0600034B RID: 843
		IList<Pair<int, BareBonesAppointment>> LoadAllAppointmentsForStudents_OnlyReturnAppointmentsWithAppTypeIdsMatchedToAForm(IList<int> PersonIds, IList<int> ControlIds);

		// Token: 0x0600034C RID: 844
		IList<Pair<int, BareBonesPerDateEntry>> LoadAllPerDateEntriesForStudents_OnlyReturnAppointmentsWithAppTypeIdsMatchedToAForm(IList<int> PersonIds, IList<int> ControlIds);
	}
}
