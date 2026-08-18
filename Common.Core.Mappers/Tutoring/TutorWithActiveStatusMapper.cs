using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Mappers.Tutoring
{
	// Token: 0x0200002E RID: 46
	public static class TutorWithActiveStatusMapper
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00006064 File Offset: 0x00004264
		static TutorWithActiveStatusMapper()
		{
			TutorBaseMapper.CreateMap();
			TutorMapper.CreateMap();
			Mapper.CreateMap<TutorWithActiveStatusDTO, TutorWithActiveStatus>().ForMember((TutorWithActiveStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TutorWithActiveStatusDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TutorWithActiveStatus, TutorWithActiveStatusDTO>().ForMember((TutorWithActiveStatusDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<TutorWithActiveStatus> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000613C File Offset: 0x0000433C
		public static TutorWithActiveStatus ToDomainObject(this TutorWithActiveStatusDTO dto)
		{
			return Mapper.Map<TutorWithActiveStatusDTO, TutorWithActiveStatus>(dto);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006154 File Offset: 0x00004354
		public static TutorWithActiveStatusDTO ToDTO(this TutorWithActiveStatus item)
		{
			return Mapper.Map<TutorWithActiveStatus, TutorWithActiveStatusDTO>(item);
		}
	}
}
