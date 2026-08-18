using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Core.Mappers.Updates
{
	// Token: 0x02000025 RID: 37
	public static class UploadUpdateFileResultDTOMapper
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000056A5 File Offset: 0x000038A5
		static UploadUpdateFileResultDTOMapper()
		{
			eUpdateStatusDTOMapper.CreateMap();
			FileTypeDTOMapper.CreateMap();
			Mapper.CreateMap<UploadUpdateFileResultDTO, UploadUpdateFileResult>();
			Mapper.CreateMap<UploadUpdateFileResult, UploadUpdateFileResultDTO>();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000056C0 File Offset: 0x000038C0
		public static UploadUpdateFileResultDTO ToDTO(this UploadUpdateFileResult uploadUpdateFileResult)
		{
			return Mapper.Map<UploadUpdateFileResult, UploadUpdateFileResultDTO>(uploadUpdateFileResult);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000056D8 File Offset: 0x000038D8
		public static UploadUpdateFileResult ToDomainObject(this UploadUpdateFileResultDTO uploadUpdateFileResultDTO)
		{
			return Mapper.Map<UploadUpdateFileResultDTO, UploadUpdateFileResult>(uploadUpdateFileResultDTO);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000056F0 File Offset: 0x000038F0
		public static IList<UploadUpdateFileResultDTO> ToDTO(this IList<UploadUpdateFileResult> list)
		{
			IList<UploadUpdateFileResultDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<UploadUpdateFileResultDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005734 File Offset: 0x00003934
		public static IList<UploadUpdateFileResult> ToDomainObject(this IList<UploadUpdateFileResultDTO> list)
		{
			IList<UploadUpdateFileResult> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<UploadUpdateFileResult>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
