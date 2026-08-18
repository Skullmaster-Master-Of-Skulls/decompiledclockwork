using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Mappers.Tasks
{
	// Token: 0x0200004A RID: 74
	public static class TaskNoteMapper
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00008E94 File Offset: 0x00007094
		static TaskNoteMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<TaskNoteDTO, TaskNote>().ForMember((TaskNote pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TaskNoteDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TaskNote, TaskNoteDTO>();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008F18 File Offset: 0x00007118
		public static TaskNote ToDomainObject(this TaskNoteDTO taskNoteDTO)
		{
			return Mapper.Map<TaskNoteDTO, TaskNote>(taskNoteDTO);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008F30 File Offset: 0x00007130
		public static TaskNoteDTO ToDTO(this TaskNote taskNote)
		{
			return Mapper.Map<TaskNote, TaskNoteDTO>(taskNote);
		}
	}
}
