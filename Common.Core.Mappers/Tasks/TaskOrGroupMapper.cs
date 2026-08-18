using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Mappers.Tasks
{
	// Token: 0x0200004C RID: 76
	public static class TaskOrGroupMapper
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000090BD File Offset: 0x000072BD
		static TaskOrGroupMapper()
		{
			TaskMapper.CreateMap();
			TaskGroupMapper.CreateMap();
			Mapper.CreateMap<TaskOrGroupDTO, TaskOrGroup>();
			Mapper.CreateMap<TaskOrGroup, TaskOrGroupDTO>();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000090D8 File Offset: 0x000072D8
		public static TaskOrGroup ToDomainObject(this TaskOrGroupDTO dto)
		{
			return Mapper.Map<TaskOrGroupDTO, TaskOrGroup>(dto);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000090F0 File Offset: 0x000072F0
		public static TaskOrGroupDTO ToDTO(this TaskOrGroup item)
		{
			return Mapper.Map<TaskOrGroup, TaskOrGroupDTO>(item);
		}
	}
}
