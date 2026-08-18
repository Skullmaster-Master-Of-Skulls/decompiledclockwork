using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200021C RID: 540
	public static class MediaJobRunninNoteMapper
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x00029200 File Offset: 0x00027400
		static MediaJobRunninNoteMapper()
		{
			Mapper.CreateMap<MediaJobRunningNote, MediaJobRunningNoteDTO>();
			Mapper.CreateMap<MediaJobRunningNoteDTO, MediaJobRunningNote>().ForMember((MediaJobRunningNote bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaJobRunningNoteDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002927C File Offset: 0x0002747C
		public static MediaJobRunningNote ToDomainObject(this MediaJobRunningNoteDTO mediaJobRunningNoteDTO)
		{
			return Mapper.Map<MediaJobRunningNoteDTO, MediaJobRunningNote>(mediaJobRunningNoteDTO);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00029294 File Offset: 0x00027494
		public static IList<MediaJobRunningNote> ToDomainObject(this IList<MediaJobRunningNoteDTO> list)
		{
			IList<MediaJobRunningNote> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaJobRunningNote>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000292D8 File Offset: 0x000274D8
		public static MediaJobRunningNoteDTO ToDTO(this MediaJobRunningNote mediaJobRunningNote)
		{
			return Mapper.Map<MediaJobRunningNote, MediaJobRunningNoteDTO>(mediaJobRunningNote);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000292F0 File Offset: 0x000274F0
		public static IList<MediaJobRunningNoteDTO> ToDTO(this IList<MediaJobRunningNote> list)
		{
			IList<MediaJobRunningNoteDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaJobRunningNoteDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
