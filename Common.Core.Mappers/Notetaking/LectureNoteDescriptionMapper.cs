using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.Core.Mappers.Notetaking
{
	// Token: 0x020000B6 RID: 182
	public static class LectureNoteDescriptionMapper
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		static LectureNoteDescriptionMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			NotetakerBaseMapper.CreateMap();
			Mapper.CreateMap<LectureNoteDescriptionDTO, LectureNoteDescription>().ForMember((LectureNoteDescription pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LectureNoteDescriptionDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LectureNoteDescription, LectureNoteDescriptionDTO>();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000FE60 File Offset: 0x0000E060
		public static LectureNoteDescription ToDomainObject(this LectureNoteDescriptionDTO dto)
		{
			return Mapper.Map<LectureNoteDescriptionDTO, LectureNoteDescription>(dto);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000FE78 File Offset: 0x0000E078
		public static LectureNoteDescriptionDTO ToDTO(this LectureNoteDescription item)
		{
			return Mapper.Map<LectureNoteDescription, LectureNoteDescriptionDTO>(item);
		}
	}
}
