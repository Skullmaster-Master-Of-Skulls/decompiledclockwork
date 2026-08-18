using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x0200005E RID: 94
	public interface IDynamicDataForReportsClientManager : IWebService
	{
		// Token: 0x060002CF RID: 719
		DataTable CrossReferenceDataIntoSingleTable(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x060002D0 RID: 720
		DataTable CrossReferenceAccommodationDataTemplateOrCourseSpecific(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x060002D1 RID: 721
		DataTable CrossReferenceAccommodationDataTemplateOnly(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x060002D2 RID: 722
		DataTable CrossReferencePerStudentData(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x060002D3 RID: 723
		DataTable CrossReferencePerAppointmentData(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x060002D4 RID: 724
		DataTable LoadStudentReportInfo(int[] studentPersonIds, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds);

		// Token: 0x060002D5 RID: 725
		Task<DataTable> LoadStudentReportInfoAsync(int[] studentPersonIds, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds);
	}
}
