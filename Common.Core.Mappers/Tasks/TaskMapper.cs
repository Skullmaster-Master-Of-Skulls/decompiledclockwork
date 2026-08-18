using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Mappers.Tasks
{
	// Token: 0x02000049 RID: 73
	public static class TaskMapper
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00008DD0 File Offset: 0x00006FD0
		static TaskMapper()
		{
			TaskClientMapper.CreateMap();
			TaskNoteMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			TaskGroupMapper.CreateMap();
			Mapper.CreateMap<TaskDTO, Task>().ForMember((Task pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TaskDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Task, TaskDTO>();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008E64 File Offset: 0x00007064
		public static Task ToDomainObject(this TaskDTO taskDTO)
		{
			return Mapper.Map<TaskDTO, Task>(taskDTO);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008E7C File Offset: 0x0000707C
		public static TaskDTO ToDTO(this Task task)
		{
			return Mapper.Map<Task, TaskDTO>(task);
		}
	}
}
