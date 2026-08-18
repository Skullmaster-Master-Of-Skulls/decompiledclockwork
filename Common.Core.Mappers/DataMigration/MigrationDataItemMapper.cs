using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x0200014B RID: 331
	public static class MigrationDataItemMapper
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x0001A6D4 File Offset: 0x000188D4
		static MigrationDataItemMapper()
		{
			Mapper.CreateMap<MigrationDataItemDTO, MigrationDataItem>().ForMember((MigrationDataItem pb) => pb.Id, delegate(IMemberConfigurationExpression<MigrationDataItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<MigrationDataItem, MigrationDataItemDTO>();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A744 File Offset: 0x00018944
		public static MigrationDataItem ToDomainObject(this MigrationDataItemDTO dto)
		{
			return Mapper.Map<MigrationDataItemDTO, MigrationDataItem>(dto);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A75C File Offset: 0x0001895C
		public static MigrationDataItemDTO ToDTO(this MigrationDataItem item)
		{
			return Mapper.Map<MigrationDataItem, MigrationDataItemDTO>(item);
		}
	}
}
