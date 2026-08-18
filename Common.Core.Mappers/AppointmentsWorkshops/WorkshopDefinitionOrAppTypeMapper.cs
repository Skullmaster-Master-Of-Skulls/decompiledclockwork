using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.Mappers.AppointmentsWorkshops
{
	// Token: 0x0200019F RID: 415
	public static class WorkshopDefinitionOrAppTypeMapper
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0001F348 File Offset: 0x0001D548
		static WorkshopDefinitionOrAppTypeMapper()
		{
			WorkshopDefinitionMapper.CreateMap();
			AppTypeMapper.CreateMap();
			Mapper.CreateMap<WorkshopDefinitionOrAppTypeDTO, WorkshopDefinitionOrAppType>();
			Mapper.CreateMap<WorkshopDefinitionOrAppType, WorkshopDefinitionOrAppTypeDTO>();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001F364 File Offset: 0x0001D564
		public static WorkshopDefinitionOrAppType ToDomainObject(this WorkshopDefinitionOrAppTypeDTO dto)
		{
			return Mapper.Map<WorkshopDefinitionOrAppTypeDTO, WorkshopDefinitionOrAppType>(dto);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001F37C File Offset: 0x0001D57C
		public static WorkshopDefinitionOrAppTypeDTO ToDTO(this WorkshopDefinitionOrAppType item)
		{
			return Mapper.Map<WorkshopDefinitionOrAppType, WorkshopDefinitionOrAppTypeDTO>(item);
		}
	}
}
