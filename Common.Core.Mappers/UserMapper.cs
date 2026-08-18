using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x0200000E RID: 14
	public static class UserMapper
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000036AC File Offset: 0x000018AC
		static UserMapper()
		{
			Mapper.CreateMap<User, IM_User>().ForMember((IM_User imUser) => imUser.Username, delegate(IMemberConfigurationExpression<User> m)
			{
				m.MapFrom<string>((User u) => u.Name);
			}).ForMember((IM_User imUser) => imUser.Roles, delegate(IMemberConfigurationExpression<User> m)
			{
				m.MapFrom<List<string>>((User u) => new List<string>(from r in u.Roles
				select r.Name));
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000375C File Offset: 0x0000195C
		public static IM_User ToDTO(this User user)
		{
			return Mapper.Map<User, IM_User>(user);
		}
	}
}
