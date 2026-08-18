using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.Mappers.AppointmentsWorkshops
{
	// Token: 0x0200019D RID: 413
	public static class WorkshopDefinitionMapper
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0001F108 File Offset: 0x0001D308
		static WorkshopDefinitionMapper()
		{
			PersonBaseMapper.CreateMap();
			AppTypeMapper.CreateMap();
			Mapper.CreateMap<WorkshopDefinitionDTO, WorkshopDefinition>().ForMember((WorkshopDefinition pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<WorkshopDefinitionDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<WorkshopDefinition, WorkshopDefinitionDTO>();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001F190 File Offset: 0x0001D390
		public static WorkshopDefinition ToDomainObject(this WorkshopDefinitionDTO workshopDefinitionDTO)
		{
			return Mapper.Map<WorkshopDefinitionDTO, WorkshopDefinition>(workshopDefinitionDTO);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		public static WorkshopDefinitionDTO ToDTO(this WorkshopDefinition workshopDefinition)
		{
			return Mapper.Map<WorkshopDefinition, WorkshopDefinitionDTO>(workshopDefinition);
		}
	}
}
