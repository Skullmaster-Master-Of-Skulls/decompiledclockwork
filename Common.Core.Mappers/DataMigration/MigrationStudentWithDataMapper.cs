using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x0200014F RID: 335
	public static class MigrationStudentWithDataMapper
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x0001A894 File Offset: 0x00018A94
		static MigrationStudentWithDataMapper()
		{
			MigrationStudentMapper.CreateMap();
			MigrationDataItemMapper.CreateMap();
			Mapper.CreateMap<MigrationStudentWithDataDTO, MigrationStudentWithData>().ForMember((MigrationStudentWithData pb) => pb.Student, delegate(IMemberConfigurationExpression<MigrationStudentWithDataDTO> m)
			{
				m.MapFrom<MigrationStudent>((MigrationStudentWithDataDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((MigrationStudentWithData pb) => pb.DataItems, delegate(IMemberConfigurationExpression<MigrationStudentWithDataDTO> m)
			{
				m.MapFrom<List<MigrationDataItem>>((MigrationStudentWithDataDTO pbdto) => (pbdto.DataItems == null) ? null : (from g in pbdto.DataItems
				select g.ToDomainObject()).ToList<MigrationDataItem>());
			});
			Mapper.CreateMap<MigrationStudentWithData, MigrationStudentWithDataDTO>().ForMember((MigrationStudentWithDataDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<MigrationStudentWithData> m)
			{
				m.MapFrom<MigrationStudentDTO>((MigrationStudentWithData pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((MigrationStudentWithDataDTO pb) => pb.DataItems, delegate(IMemberConfigurationExpression<MigrationStudentWithData> m)
			{
				m.MapFrom<List<MigrationDataItemDTO>>((MigrationStudentWithData pbdto) => (pbdto.DataItems == null) ? null : (from g in pbdto.DataItems
				select g.ToDTO()).ToList<MigrationDataItemDTO>());
			});
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		public static MigrationStudentWithData ToDomainObject(this MigrationStudentWithDataDTO dto)
		{
			return Mapper.Map<MigrationStudentWithDataDTO, MigrationStudentWithData>(dto);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001AA0C File Offset: 0x00018C0C
		public static MigrationStudentWithDataDTO ToDTO(this MigrationStudentWithData item)
		{
			return Mapper.Map<MigrationStudentWithData, MigrationStudentWithDataDTO>(item);
		}
	}
}
