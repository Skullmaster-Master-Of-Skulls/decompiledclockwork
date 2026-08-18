using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount;
using TechnoPro.Common.Public.Entities.UserAccount;

namespace TechnoPro.Common.Core.Mappers.UserAccount
{
	// Token: 0x02000021 RID: 33
	public static class UserInfoPasswordMapper
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00005424 File Offset: 0x00003624
		static UserInfoPasswordMapper()
		{
			Mapper.CreateMap<UserInfoPassword, UserInfoPasswordDTO>();
			Mapper.CreateMap<UserInfoPasswordDTO, UserInfoPassword>().ForMember((UserInfoPassword pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<UserInfoPasswordDTO> m)
			{
				m.Ignore();
			}).ForMember((UserInfoPassword pb) => pb.SecondId, delegate(IMemberConfigurationExpression<UserInfoPasswordDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000054F4 File Offset: 0x000036F4
		public static UserInfoPassword ToDomainObject(this UserInfoPasswordDTO groupDTO)
		{
			return Mapper.Map<UserInfoPasswordDTO, UserInfoPassword>(groupDTO);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000550C File Offset: 0x0000370C
		public static UserInfoPasswordDTO ToDTO(this UserInfoPassword group)
		{
			return Mapper.Map<UserInfoPassword, UserInfoPasswordDTO>(group);
		}
	}
}
