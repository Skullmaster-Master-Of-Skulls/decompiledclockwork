using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A6 RID: 166
	public static class StaffCommonInfoMapper
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		static StaffCommonInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<StaffCommonInfoDTO, StaffCommonInfo>().ForMember((StaffCommonInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StaffCommonInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StaffCommonInfo, StaffCommonInfoDTO>();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000F05C File Offset: 0x0000D25C
		public static StaffCommonInfo ToDomainObject(this StaffCommonInfoDTO dto)
		{
			return Mapper.Map<StaffCommonInfoDTO, StaffCommonInfo>(dto);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F074 File Offset: 0x0000D274
		public static StaffCommonInfoDTO ToDTO(this StaffCommonInfo item)
		{
			return Mapper.Map<StaffCommonInfo, StaffCommonInfoDTO>(item);
		}
	}
}
