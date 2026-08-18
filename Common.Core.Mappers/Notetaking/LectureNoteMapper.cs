using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.Core.Mappers.Notetaking
{
	// Token: 0x020000B7 RID: 183
	public static class LectureNoteMapper
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000FE90 File Offset: 0x0000E090
		static LectureNoteMapper()
		{
			LectureNoteDescriptionMapper.CreateMap();
			BinaryFileMapper.CreateMap();
			Mapper.CreateMap<LectureNoteDTO, LectureNote>().ForMember((LectureNote pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LectureNoteDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LectureNote, LectureNoteDTO>();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000FF18 File Offset: 0x0000E118
		public static LectureNote ToDomainObject(this LectureNoteDTO dto)
		{
			return Mapper.Map<LectureNoteDTO, LectureNote>(dto);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000FF30 File Offset: 0x0000E130
		public static LectureNoteDTO ToDTO(this LectureNote item)
		{
			return Mapper.Map<LectureNote, LectureNoteDTO>(item);
		}
	}
}
