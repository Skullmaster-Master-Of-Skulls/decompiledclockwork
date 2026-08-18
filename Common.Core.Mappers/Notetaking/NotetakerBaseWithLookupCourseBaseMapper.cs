using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.Core.Mappers.Notetaking
{
	// Token: 0x020000B9 RID: 185
	public static class NotetakerBaseWithLookupCourseBaseMapper
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		static NotetakerBaseWithLookupCourseBaseMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			NotetakerBaseMapper.CreateMap();
			Mapper.CreateMap<NotetakerBaseWithLookupCourseBaseDTO, NotetakerBaseWithLookupCourseBase>().ForMember((NotetakerBaseWithLookupCourseBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<NotetakerBaseWithLookupCourseBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<NotetakerBaseWithLookupCourseBase, NotetakerBaseWithLookupCourseBaseDTO>();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001007C File Offset: 0x0000E27C
		public static NotetakerBaseWithLookupCourseBase ToDomainObject(this NotetakerBaseWithLookupCourseBaseDTO dto)
		{
			return Mapper.Map<NotetakerBaseWithLookupCourseBaseDTO, NotetakerBaseWithLookupCourseBase>(dto);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00010094 File Offset: 0x0000E294
		public static NotetakerBaseWithLookupCourseBaseDTO ToDTO(this NotetakerBaseWithLookupCourseBase item)
		{
			return Mapper.Map<NotetakerBaseWithLookupCourseBase, NotetakerBaseWithLookupCourseBaseDTO>(item);
		}
	}
}
