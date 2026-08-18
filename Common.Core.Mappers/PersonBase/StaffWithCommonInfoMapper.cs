using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A7 RID: 167
	public static class StaffWithCommonInfoMapper
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000F08C File Offset: 0x0000D28C
		static StaffWithCommonInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			StaffCommonInfoMapper.CreateMap();
			Mapper.CreateMap<StaffWithCommonInfoDTO, StaffWithCommonInfo>();
			Mapper.CreateMap<StaffWithCommonInfo, StaffWithCommonInfoDTO>();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public static StaffWithCommonInfo ToDomainObject(this StaffWithCommonInfoDTO dto)
		{
			return Mapper.Map<StaffWithCommonInfoDTO, StaffWithCommonInfo>(dto);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public static StaffWithCommonInfoDTO ToDTO(this StaffWithCommonInfo item)
		{
			return Mapper.Map<StaffWithCommonInfo, StaffWithCommonInfoDTO>(item);
		}
	}
}
