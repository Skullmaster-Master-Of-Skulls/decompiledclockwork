using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x0200014D RID: 333
	public static class MigrationMapperDataItemMapper
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x0001A7B4 File Offset: 0x000189B4
		static MigrationMapperDataItemMapper()
		{
			Mapper.CreateMap<MigrationMapperDataItemDTO, MigrationMapperDataItem>();
			Mapper.CreateMap<MigrationMapperDataItem, MigrationMapperDataItemDTO>();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public static MigrationMapperDataItem ToDomainObject(this MigrationMapperDataItemDTO dto)
		{
			return Mapper.Map<MigrationMapperDataItemDTO, MigrationMapperDataItem>(dto);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001A7DC File Offset: 0x000189DC
		public static MigrationMapperDataItemDTO ToDTO(this MigrationMapperDataItem item)
		{
			return Mapper.Map<MigrationMapperDataItem, MigrationMapperDataItemDTO>(item);
		}
	}
}
