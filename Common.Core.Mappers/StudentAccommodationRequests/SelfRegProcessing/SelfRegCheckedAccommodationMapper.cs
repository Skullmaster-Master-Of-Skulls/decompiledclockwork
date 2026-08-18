using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x02000064 RID: 100
	public static class SelfRegCheckedAccommodationMapper
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000AA80 File Offset: 0x00008C80
		static SelfRegCheckedAccommodationMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<SelfRegCheckedAccommodationDTO, SelfRegCheckedAccommodation>();
			Mapper.CreateMap<SelfRegCheckedAccommodation, SelfRegCheckedAccommodationDTO>();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000AA98 File Offset: 0x00008C98
		public static SelfRegCheckedAccommodation ToDomainObject(this SelfRegCheckedAccommodationDTO dto)
		{
			return Mapper.Map<SelfRegCheckedAccommodationDTO, SelfRegCheckedAccommodation>(dto);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public static SelfRegCheckedAccommodationDTO ToDTO(this SelfRegCheckedAccommodation item)
		{
			return Mapper.Map<SelfRegCheckedAccommodation, SelfRegCheckedAccommodationDTO>(item);
		}
	}
}
