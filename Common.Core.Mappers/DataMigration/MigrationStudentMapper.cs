using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x0200014E RID: 334
	public static class MigrationStudentMapper
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x0001A7F4 File Offset: 0x000189F4
		static MigrationStudentMapper()
		{
			Mapper.CreateMap<MigrationStudentDTO, MigrationStudent>().ForMember((MigrationStudent pb) => pb.Id, delegate(IMemberConfigurationExpression<MigrationStudentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<MigrationStudent, MigrationStudentDTO>();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001A864 File Offset: 0x00018A64
		public static MigrationStudent ToDomainObject(this MigrationStudentDTO dto)
		{
			return Mapper.Map<MigrationStudentDTO, MigrationStudent>(dto);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001A87C File Offset: 0x00018A7C
		public static MigrationStudentDTO ToDTO(this MigrationStudent item)
		{
			return Mapper.Map<MigrationStudent, MigrationStudentDTO>(item);
		}
	}
}
