using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Mappers.Tutoring
{
	// Token: 0x0200002B RID: 43
	public static class TutorBaseMapper
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00005CF8 File Offset: 0x00003EF8
		static TutorBaseMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<TutorBaseDTO, TutorBase>().ForMember((TutorBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TutorBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TutorBase, TutorBaseDTO>().ForMember((TutorBaseDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<TutorBase> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005DC8 File Offset: 0x00003FC8
		public static TutorBase ToDomainObject(this TutorBaseDTO dto)
		{
			return Mapper.Map<TutorBaseDTO, TutorBase>(dto);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public static TutorBaseDTO ToDTO(this TutorBase item)
		{
			return Mapper.Map<TutorBase, TutorBaseDTO>(item);
		}
	}
}
