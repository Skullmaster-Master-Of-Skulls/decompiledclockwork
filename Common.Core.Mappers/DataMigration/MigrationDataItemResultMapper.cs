using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x0200014C RID: 332
	public static class MigrationDataItemResultMapper
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x0001A774 File Offset: 0x00018974
		static MigrationDataItemResultMapper()
		{
			Mapper.CreateMap<MigrationDataItemResultDTO, MigrationDataItemResult>();
			Mapper.CreateMap<MigrationDataItemResult, MigrationDataItemResultDTO>();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001A784 File Offset: 0x00018984
		public static MigrationDataItemResult ToDomainObject(this MigrationDataItemResultDTO dto)
		{
			return Mapper.Map<MigrationDataItemResultDTO, MigrationDataItemResult>(dto);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001A79C File Offset: 0x0001899C
		public static MigrationDataItemResultDTO ToDTO(this MigrationDataItemResult item)
		{
			return Mapper.Map<MigrationDataItemResult, MigrationDataItemResultDTO>(item);
		}
	}
}
