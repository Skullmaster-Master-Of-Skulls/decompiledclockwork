using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.Core.Mappers.Notetaking
{
	// Token: 0x020000B5 RID: 181
	public static class DownloadedLectureNoteMapper
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000FD18 File Offset: 0x0000DF18
		static DownloadedLectureNoteMapper()
		{
			LectureNoteMapper.CreateMap();
			LectureNoteDescriptionMapper.CreateMap();
			BinaryFileMapper.CreateMap();
			Mapper.CreateMap<DownloadedLectureNoteDTO, DownloadedLectureNote>().ForMember((DownloadedLectureNote pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DownloadedLectureNoteDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DownloadedLectureNote, DownloadedLectureNoteDTO>();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		public static DownloadedLectureNote ToDomainObject(this DownloadedLectureNoteDTO dto)
		{
			return Mapper.Map<DownloadedLectureNoteDTO, DownloadedLectureNote>(dto);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		public static DownloadedLectureNoteDTO ToDTO(this DownloadedLectureNote item)
		{
			return Mapper.Map<DownloadedLectureNote, DownloadedLectureNoteDTO>(item);
		}
	}
}
