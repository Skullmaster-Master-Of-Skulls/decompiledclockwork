using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Mappers.Tasks
{
	// Token: 0x02000047 RID: 71
	public static class TaskClientMapper
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00008C68 File Offset: 0x00006E68
		static TaskClientMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<TaskClientDTO, TaskClient>().ForMember((TaskClient pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TaskClientDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TaskClient, TaskClientDTO>();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008CEC File Offset: 0x00006EEC
		public static TaskClient ToDomainObject(this TaskClientDTO taskClientDTO)
		{
			return Mapper.Map<TaskClientDTO, TaskClient>(taskClientDTO);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008D04 File Offset: 0x00006F04
		public static TaskClientDTO ToDTO(this TaskClient taskClient)
		{
			return Mapper.Map<TaskClient, TaskClientDTO>(taskClient);
		}
	}
}
