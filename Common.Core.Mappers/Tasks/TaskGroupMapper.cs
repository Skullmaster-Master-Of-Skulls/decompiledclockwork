using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Mappers.Tasks
{
	// Token: 0x02000048 RID: 72
	public static class TaskGroupMapper
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00008D1C File Offset: 0x00006F1C
		static TaskGroupMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<TaskGroupDTO, TaskGroup>().ForMember((TaskGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TaskGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TaskGroup, TaskGroupDTO>();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008DA0 File Offset: 0x00006FA0
		public static TaskGroup ToDomainObject(this TaskGroupDTO taskGroupDTO)
		{
			return Mapper.Map<TaskGroupDTO, TaskGroup>(taskGroupDTO);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public static TaskGroupDTO ToDTO(this TaskGroup taskGroup)
		{
			return Mapper.Map<TaskGroup, TaskGroupDTO>(taskGroup);
		}
	}
}
