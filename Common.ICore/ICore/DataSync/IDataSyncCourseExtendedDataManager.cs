using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses.ExtendedDataSyncData;

namespace TechnoPro.Common.ICore.DataSync
{
	// Token: 0x020000A5 RID: 165
	public interface IDataSyncCourseExtendedDataManager : IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004D0 RID: 1232
		CourseExtendedDataSyncDataItems LoadCourseExtendedDataSyncDataByLuCourseId(int lucid);

		// Token: 0x060004D1 RID: 1233
		IDictionary<int, CourseExtendedDataSyncDataItems> LoadCourseExtendedDataSyncDataByLuCourseIds(int[] lucids);

		// Token: 0x060004D2 RID: 1234
		IList<CourseExtendedDataSyncField> LoadCourseExtendedDataSyncFields();

		// Token: 0x060004D3 RID: 1235
		void OverwriteCourseExtendedData(int lucid, CourseExtendedDataSyncDataItems dataItems);

		// Token: 0x060004D4 RID: 1236
		void DeleteCourseExtendedDataSyncField(int ControlId);

		// Token: 0x060004D5 RID: 1237
		void UpdateCourseExtendedDataSyncField(CourseExtendedDataSyncField field);

		// Token: 0x060004D6 RID: 1238
		int AddCourseExtendedDataSyncField(CourseExtendedDataSyncField field);
	}
}
