using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.Common.Core.Mappers.DataMigration
{
	// Token: 0x02000148 RID: 328
	public static class MigrationAppointmentItemResultMapper
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0001A418 File Offset: 0x00018618
		static MigrationAppointmentItemResultMapper()
		{
			MigrationAppointmentMapper.CreateMap();
			MigrationDataItemResultMapper.CreateMap();
			Mapper.CreateMap<MigrationAppointmentItemResultDTO, MigrationAppointmentItemResult>().ForMember((MigrationAppointmentItemResult pb) => pb.ExternalAppointment, delegate(IMemberConfigurationExpression<MigrationAppointmentItemResultDTO> m)
			{
				m.MapFrom<MigrationAppointment>((MigrationAppointmentItemResultDTO pbdto) => (pbdto.ExternalAppointment == null) ? null : pbdto.ExternalAppointment.ToDomainObject());
			}).ForMember((MigrationAppointmentItemResult pb) => pb.DataItemResults, delegate(IMemberConfigurationExpression<MigrationAppointmentItemResultDTO> m)
			{
				m.MapFrom<List<MigrationDataItemResult>>((MigrationAppointmentItemResultDTO pbdto) => (pbdto.DataItemResults == null) ? null : (from g in pbdto.DataItemResults
				select g.ToDomainObject()).ToList<MigrationDataItemResult>());
			});
			Mapper.CreateMap<MigrationAppointmentItemResult, MigrationAppointmentItemResultDTO>().ForMember((MigrationAppointmentItemResultDTO pb) => pb.ExternalAppointment, delegate(IMemberConfigurationExpression<MigrationAppointmentItemResult> m)
			{
				m.MapFrom<MigrationAppointmentDTO>((MigrationAppointmentItemResult pbdto) => (pbdto.ExternalAppointment == null) ? null : pbdto.ExternalAppointment.ToDTO());
			}).ForMember((MigrationAppointmentItemResultDTO pb) => pb.DataItemResults, delegate(IMemberConfigurationExpression<MigrationAppointmentItemResult> m)
			{
				m.MapFrom<List<MigrationDataItemResultDTO>>((MigrationAppointmentItemResult pbdto) => (pbdto.DataItemResults == null) ? null : (from g in pbdto.DataItemResults
				select g.ToDTO()).ToList<MigrationDataItemResultDTO>());
			});
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001A578 File Offset: 0x00018778
		public static MigrationAppointmentItemResult ToDomainObject(this MigrationAppointmentItemResultDTO dto)
		{
			return Mapper.Map<MigrationAppointmentItemResultDTO, MigrationAppointmentItemResult>(dto);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001A590 File Offset: 0x00018790
		public static MigrationAppointmentItemResultDTO ToDTO(this MigrationAppointmentItemResult item)
		{
			return Mapper.Map<MigrationAppointmentItemResult, MigrationAppointmentItemResultDTO>(item);
		}
	}
}
