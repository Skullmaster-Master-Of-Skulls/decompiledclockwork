using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Core.DataMigration;
using TechnoPro.Common.Core.Mappers.DataMigration;
using TechnoPro.Common.ICore.DataMigration;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000035 RID: 53
	public class DataMigrationServiceManager : IDataMigration, IService
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000A534 File Offset: 0x00008734
		public CreateStudentsResp CreateStudents(CreateStudentsReq Request)
		{
			IDataMigrationManager dataMigrationManager = new DataMigrationManager(Request.GetOperationContext());
			IDataMigrationManager dataMigrationManager2 = dataMigrationManager;
			bool previewMode = Request.PreviewMode;
			IList<MigrationStudent> migrationStudents;
			if (Request.MigrationStudents != null)
			{
				migrationStudents = (from g in Request.MigrationStudents
				select g.ToDomainObject()).ToList<MigrationStudent>();
			}
			else
			{
				migrationStudents = null;
			}
			IList<MigrationCreateStudentResult> list = dataMigrationManager2.CreateStudents(previewMode, migrationStudents);
			CreateStudentsResp createStudentsResp = new CreateStudentsResp();
			IList<MigrationCreateStudentResultDTO> results;
			if (list != null)
			{
				results = (from g in list
				select g.ToDTO()).ToList<MigrationCreateStudentResultDTO>();
			}
			else
			{
				results = null;
			}
			createStudentsResp.Results = results;
			return createStudentsResp;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public MigrateStudentDataResp MigrateStudentData(MigrateStudentDataReq Request)
		{
			IDataMigrationManager dataMigrationManager = new DataMigrationManager(Request.GetOperationContext());
			IDataMigrationManager dataMigrationManager2 = dataMigrationManager;
			bool previewMode = Request.PreviewMode;
			IList<MigrationMapperDataItem> dataMapper;
			if (Request.DataMappers != null)
			{
				dataMapper = (from g in Request.DataMappers
				select g.ToDomainObject()).ToList<MigrationMapperDataItem>();
			}
			else
			{
				dataMapper = null;
			}
			IList<MigrationStudentWithData> studentsWithPerStudentData;
			if (Request.StudentsWithPerStudentData != null)
			{
				studentsWithPerStudentData = (from h in Request.StudentsWithPerStudentData
				select h.ToDomainObject()).ToList<MigrationStudentWithData>();
			}
			else
			{
				studentsWithPerStudentData = null;
			}
			IList<MigrationDataItemResult> list = dataMigrationManager2.MigrateStudentData(previewMode, dataMapper, studentsWithPerStudentData, Request.ClearExistingDataWhenMigrationDataIsEmpty);
			MigrateStudentDataResp migrateStudentDataResp = new MigrateStudentDataResp();
			IList<MigrationDataItemResultDTO> results;
			if (list != null)
			{
				results = (from g in list
				select g.ToDTO()).ToList<MigrationDataItemResultDTO>();
			}
			else
			{
				results = null;
			}
			migrateStudentDataResp.Results = results;
			return migrateStudentDataResp;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A6BC File Offset: 0x000088BC
		public MigrateStudentPerDateDataResp MigrateStudentPerDateData(MigrateStudentPerDateDataReq Request)
		{
			IDataMigrationManager dataMigrationManager = new DataMigrationManager(Request.GetOperationContext());
			IDataMigrationManager dataMigrationManager2 = dataMigrationManager;
			bool previewMode = Request.PreviewMode;
			int perDateScreenNum = Request.PerDateScreenNum;
			string titleKeyName = Request.TitleKeyName;
			IList<MigrationMapperDataItem> dataMapper;
			if (Request.DataMappers != null)
			{
				dataMapper = (from g in Request.DataMappers
				select g.ToDomainObject()).ToList<MigrationMapperDataItem>();
			}
			else
			{
				dataMapper = null;
			}
			IList<MigrationStudentWithPerDateData> studentsWithPerDateData;
			if (Request.StudentsWithPerDateData != null)
			{
				studentsWithPerDateData = (from h in Request.StudentsWithPerDateData
				select h.ToDomainObject()).ToList<MigrationStudentWithPerDateData>();
			}
			else
			{
				studentsWithPerDateData = null;
			}
			IList<MigrationDataItemResult> list = dataMigrationManager2.MigrateStudentPerDateData(previewMode, perDateScreenNum, titleKeyName, dataMapper, studentsWithPerDateData, Request.ClearExistingDataWhenMigrationDataIsEmpty);
			MigrateStudentPerDateDataResp migrateStudentPerDateDataResp = new MigrateStudentPerDateDataResp();
			IList<MigrationDataItemResultDTO> results;
			if (list != null)
			{
				results = (from g in list
				select g.ToDTO()).ToList<MigrationDataItemResultDTO>();
			}
			else
			{
				results = null;
			}
			migrateStudentPerDateDataResp.Results = results;
			return migrateStudentPerDateDataResp;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A7AC File Offset: 0x000089AC
		public MigrateAppointmentsResp MigrateAppointments(MigrateAppointmentsReq Request)
		{
			IDataMigrationManager dataMigrationManager = new DataMigrationManager(Request.GetOperationContext());
			IDataMigrationManager dataMigrationManager2 = dataMigrationManager;
			bool previewMode = Request.PreviewMode;
			IList<MigrationMapperDataItem> dataMapper;
			if (Request.DataMappers != null)
			{
				dataMapper = (from g in Request.DataMappers
				select g.ToDomainObject()).ToList<MigrationMapperDataItem>();
			}
			else
			{
				dataMapper = null;
			}
			IList<MigrationAppointment> appointments;
			if (Request.AppointmentsWithPerAppData != null)
			{
				appointments = (from h in Request.AppointmentsWithPerAppData
				select h.ToDomainObject()).ToList<MigrationAppointment>();
			}
			else
			{
				appointments = null;
			}
			IList<MigrationAppointmentItemResult> list = dataMigrationManager2.MigrateAppointments(previewMode, dataMapper, appointments, Request.ClearExistingDataWhenMigrationDataIsEmpty, !Request.AllowDuplicateAppointmentsToBeCreated);
			MigrateAppointmentsResp migrateAppointmentsResp = new MigrateAppointmentsResp();
			IList<MigrationAppointmentItemResultDTO> results;
			if (list != null)
			{
				results = (from g in list
				select g.ToDTO()).ToList<MigrationAppointmentItemResultDTO>();
			}
			else
			{
				results = null;
			}
			migrateAppointmentsResp.Results = results;
			return migrateAppointmentsResp;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A898 File Offset: 0x00008A98
		public MigrateAccommodationsResp MigrateAccommodations(MigrateAccommodationsReq Request)
		{
			IDataMigrationManager dataMigrationManager = new DataMigrationManager(Request.GetOperationContext());
			IDataMigrationManager dataMigrationManager2 = dataMigrationManager;
			bool previewMode = Request.PreviewMode;
			IList<MigrationMapperDataItem> dataMapper;
			if (Request.DataMappers != null)
			{
				dataMapper = (from g in Request.DataMappers
				select g.ToDomainObject()).ToList<MigrationMapperDataItem>();
			}
			else
			{
				dataMapper = null;
			}
			IList<MigrationStudentWithData> studentsWithAccommodationData;
			if (Request.StudentsWithAccommodationData != null)
			{
				studentsWithAccommodationData = (from h in Request.StudentsWithAccommodationData
				select h.ToDomainObject()).ToList<MigrationStudentWithData>();
			}
			else
			{
				studentsWithAccommodationData = null;
			}
			IList<MigrationDataItemResult> list = dataMigrationManager2.MigrateAccommodations(previewMode, dataMapper, studentsWithAccommodationData, Request.ClearExistingDataWhenMigrationDataIsEmpty);
			MigrateAccommodationsResp migrateAccommodationsResp = new MigrateAccommodationsResp();
			IList<MigrationDataItemResultDTO> results;
			if (list != null)
			{
				results = (from g in list
				select g.ToDTO()).ToList<MigrationDataItemResultDTO>();
			}
			else
			{
				results = null;
			}
			migrateAccommodationsResp.Results = results;
			return migrateAccommodationsResp;
		}
	}
}
