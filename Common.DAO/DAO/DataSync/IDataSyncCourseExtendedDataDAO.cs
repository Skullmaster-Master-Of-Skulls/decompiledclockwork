using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses.ExtendedDataSyncData;

namespace TechnoPro.Common.DAO.DataSync
{
	// Token: 0x0200008D RID: 141
	public interface IDataSyncCourseExtendedDataDAO : IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003A3 RID: 931
		IList<CourseExtendedDataSyncField> LoadCourseExtendedDataSyncFields();

		// Token: 0x060003A4 RID: 932
		int AddCourseExtendedDataSyncField(CourseExtendedDataSyncField field);

		// Token: 0x060003A5 RID: 933
		void DeleteCourseExtendedDataSyncField(int ControlId);

		// Token: 0x060003A6 RID: 934
		void UpdateCourseExtendedDataSyncField(CourseExtendedDataSyncField field);

		// Token: 0x060003A7 RID: 935
		void OverwriteCourseExtendedData(int lucid, IList<CourseExtendedDataSyncField> fields, CourseExtendedDataSyncDataItems dataItems);
	}
}
