using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration.Mapping;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.Common.ICore.DataMigration
{
	// Token: 0x020000AA RID: 170
	public interface IDataMigrationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000509 RID: 1289
		IList<MigrationCreateStudentResult> CreateStudents(bool PreviewOnly, IList<MigrationStudent> MigrationStudents);

		// Token: 0x0600050A RID: 1290
		IList<MigrationDataItemResult> MigrateStudentData(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationStudentWithData> StudentsWithPerStudentData, bool clearExistingDataWhenMigrationDataIsEmpty);

		// Token: 0x0600050B RID: 1291
		IList<MigrationDataItemResult> MigrateStudentPerDateData(bool PreviewOnly, int perDateScreenNum, string titleKeyName, IList<MigrationMapperDataItem> DataMapper, IList<MigrationStudentWithPerDateData> StudentsWithPerDateData, bool clearExistingDataWhenMigrationDataIsEmpty);

		// Token: 0x0600050C RID: 1292
		IList<MigrationExternalCourseResult> MigrateCourses(bool PreviewOnly, IList<MigrationStudentWithCourses> StudentsWithCourses);

		// Token: 0x0600050D RID: 1293
		IList<MigrationAppointmentItemResult> MigrateAppointments(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationAppointment> Appointments, bool clearExistingDataWhenMigrationDataIsEmpty, bool AvoidDuplicatAppointmentsEnabled = true);

		// Token: 0x0600050E RID: 1294
		IList<MigrationDataItemResult> MigrateAccommodations(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationStudentWithData> StudentsWithAccommodationData, bool clearExistingDataWhenMigrationDataIsEmpty);

		// Token: 0x0600050F RID: 1295
		DataTable GetMigrationDataFromTable(DataTable table, out IList<MigrationMapperDataItem> dataMapper, out IList<MigrationStudentWithData> studentsWithPerStudentData, string mappingsExternalNameEqualsCidCommaSeparated, string groupIdsCommaSeparatedColName = null);

		// Token: 0x06000510 RID: 1296
		void ApplyDataMapping(DataTable table, IList<DataTableColumnMapping> dataMapping);

		// Token: 0x06000511 RID: 1297
		IList<MigrationFileItemResult> MigrateFiles(bool PreviewOnly, IList<MigrationMapperDataItem> DataMapper, IList<MigrationFile> migrationFiles);
	}
}
