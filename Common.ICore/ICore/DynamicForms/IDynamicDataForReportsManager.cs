using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000097 RID: 151
	public interface IDynamicDataForReportsManager
	{
		// Token: 0x06000438 RID: 1080
		DataTable ExpandListViewOrFileList(DataTable table, IList<DynamicDataColumn> cols);

		// Token: 0x06000439 RID: 1081
		DataTable CrossReferenceDataIntoSingleTable(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x0600043A RID: 1082
		DataTable CrossReferenceAccommodationDataTemplateOrCourseSpecific(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x0600043B RID: 1083
		DataTable CrossReferenceAccommodationDataTemplateOnly(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x0600043C RID: 1084
		DataTable CrossReferencePerStudentData(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x0600043D RID: 1085
		DataTable CrossReferencePerAppointmentData(DataTable TableWithContext, IList<int> ControlIds);

		// Token: 0x0600043E RID: 1086
		Task<IList<StudentInfoItemBase>[]> LoadStudentReportInfoAsync(int[] pids, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds);

		// Token: 0x0600043F RID: 1087
		IList<StudentInfoItemBase>[] LoadStudentReportInfo(int[] pids, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds);

		// Token: 0x06000440 RID: 1088
		DataTable CrossReferencePerDateData(DataTable TableWithContext, IList<int> ControlIds);
	}
}
