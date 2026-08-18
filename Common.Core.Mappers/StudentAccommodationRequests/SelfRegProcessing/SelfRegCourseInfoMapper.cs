using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x02000065 RID: 101
	public static class SelfRegCourseInfoMapper
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		static SelfRegCourseInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<SelfRegCourseInfoDTO, SelfRegCourseInfo>();
			Mapper.CreateMap<SelfRegCourseInfo, SelfRegCourseInfoDTO>();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public static SelfRegCourseInfo ToDomainObject(this SelfRegCourseInfoDTO dto)
		{
			return Mapper.Map<SelfRegCourseInfoDTO, SelfRegCourseInfo>(dto);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public static SelfRegCourseInfoDTO ToDTO(this SelfRegCourseInfo item)
		{
			return Mapper.Map<SelfRegCourseInfo, SelfRegCourseInfoDTO>(item);
		}
	}
}
