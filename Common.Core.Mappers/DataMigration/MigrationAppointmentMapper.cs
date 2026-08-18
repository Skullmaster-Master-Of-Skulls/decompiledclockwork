using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x02000149 RID: 329
	public static class MigrationAppointmentMapper
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0001A5A8 File Offset: 0x000187A8
		static MigrationAppointmentMapper()
		{
			MigrationDataItemMapper.CreateMap();
			Mapper.CreateMap<MigrationAppointmentDTO, MigrationAppointment>().ForMember((MigrationAppointment pb) => pb.DataItems, delegate(IMemberConfigurationExpression<MigrationAppointmentDTO> m)
			{
				m.MapFrom<List<MigrationDataItem>>((MigrationAppointmentDTO pbdto) => (pbdto.DataItems == null) ? null : (from g in pbdto.DataItems
				select g.ToDomainObject()).ToList<MigrationDataItem>());
			});
			Mapper.CreateMap<MigrationAppointment, MigrationAppointmentDTO>().ForMember((MigrationAppointmentDTO pb) => pb.DataItems, delegate(IMemberConfigurationExpression<MigrationAppointment> m)
			{
				m.MapFrom<List<MigrationDataItemDTO>>((MigrationAppointment pbdto) => (pbdto.DataItems == null) ? null : (from g in pbdto.DataItems
				select g.ToDTO()).ToList<MigrationDataItemDTO>());
			});
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001A664 File Offset: 0x00018864
		public static MigrationAppointment ToDomainObject(this MigrationAppointmentDTO dto)
		{
			return Mapper.Map<MigrationAppointmentDTO, MigrationAppointment>(dto);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A67C File Offset: 0x0001887C
		public static MigrationAppointmentDTO ToDTO(this MigrationAppointment item)
		{
			return Mapper.Map<MigrationAppointment, MigrationAppointmentDTO>(item);
		}
	}
}
