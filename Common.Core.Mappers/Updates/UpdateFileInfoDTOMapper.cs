using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Core.Mappers.Updates
{
	// Token: 0x02000023 RID: 35
	public static class UpdateFileInfoDTOMapper
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00005564 File Offset: 0x00003764
		static UpdateFileInfoDTOMapper()
		{
			eUpdateStatusDTOMapper.CreateMap();
			FileTypeDTOMapper.CreateMap();
			Mapper.CreateMap<UpdateFileInfoDTO, UpdateFileInfo>().ForMember((UpdateFileInfo pb) => pb.Id, delegate(IMemberConfigurationExpression<UpdateFileInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<UpdateFileInfo, UpdateFileInfoDTO>();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000055E0 File Offset: 0x000037E0
		public static UpdateFileInfoDTO ToDTO(this UpdateFileInfo updateFileInfo)
		{
			return Mapper.Map<UpdateFileInfo, UpdateFileInfoDTO>(updateFileInfo);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000055F8 File Offset: 0x000037F8
		public static UpdateFileInfo ToDomainObject(this UpdateFileInfoDTO updateFileInfoDto)
		{
			return Mapper.Map<UpdateFileInfoDTO, UpdateFileInfo>(updateFileInfoDto);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005610 File Offset: 0x00003810
		public static IList<UpdateFileInfoDTO> ToDTO(this IList<UpdateFileInfo> list)
		{
			IList<UpdateFileInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<UpdateFileInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005654 File Offset: 0x00003854
		public static IList<UpdateFileInfo> ToDomainObject(this IList<UpdateFileInfoDTO> list)
		{
			IList<UpdateFileInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<UpdateFileInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
