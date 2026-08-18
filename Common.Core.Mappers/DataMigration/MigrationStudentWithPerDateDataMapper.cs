using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x02000150 RID: 336
	public static class MigrationStudentWithPerDateDataMapper
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AA24 File Offset: 0x00018C24
		static MigrationStudentWithPerDateDataMapper()
		{
			MigrationStudentWithDataMapper.CreateMap();
			Mapper.CreateMap<MigrationStudentWithPerDateDataDTO, MigrationStudentWithPerDateData>();
			Mapper.CreateMap<MigrationStudentWithPerDateData, MigrationStudentWithPerDateDataDTO>();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public static MigrationStudentWithPerDateData ToDomainObject(this MigrationStudentWithPerDateDataDTO dto)
		{
			return Mapper.Map<MigrationStudentWithPerDateDataDTO, MigrationStudentWithPerDateData>(dto);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001AA54 File Offset: 0x00018C54
		public static MigrationStudentWithPerDateDataDTO ToDTO(this MigrationStudentWithPerDateData item)
		{
			return Mapper.Map<MigrationStudentWithPerDateData, MigrationStudentWithPerDateDataDTO>(item);
		}
	}
}
