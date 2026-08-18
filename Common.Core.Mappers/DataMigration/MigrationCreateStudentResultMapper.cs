using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x0200014A RID: 330
	public static class MigrationCreateStudentResultMapper
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A694 File Offset: 0x00018894
		static MigrationCreateStudentResultMapper()
		{
			Mapper.CreateMap<MigrationCreateStudentResultDTO, MigrationCreateStudentResult>();
			Mapper.CreateMap<MigrationCreateStudentResult, MigrationCreateStudentResultDTO>();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A6A4 File Offset: 0x000188A4
		public static MigrationCreateStudentResult ToDomainObject(this MigrationCreateStudentResultDTO dto)
		{
			return Mapper.Map<MigrationCreateStudentResultDTO, MigrationCreateStudentResult>(dto);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001A6BC File Offset: 0x000188BC
		public static MigrationCreateStudentResultDTO ToDTO(this MigrationCreateStudentResult item)
		{
			return Mapper.Map<MigrationCreateStudentResult, MigrationCreateStudentResultDTO>(item);
		}
	}
}
