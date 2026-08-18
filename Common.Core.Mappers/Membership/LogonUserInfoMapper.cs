using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.Core.Mappers.Membership
{
	// Token: 0x020000C0 RID: 192
	public static class LogonUserInfoMapper
	{
		// Token: 0x06000330 RID: 816 RVA: 0x000109C4 File Offset: 0x0000EBC4
		static LogonUserInfoMapper()
		{
			Mapper.CreateMap<LogonUserInfo, LogonUserInfoDTO>();
			Mapper.CreateMap<LogonUserInfoDTO, LogonUserInfo>().ForMember((LogonUserInfo pb) => pb.Id, delegate(IMemberConfigurationExpression<LogonUserInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010A34 File Offset: 0x0000EC34
		public static LogonUserInfoDTO ToDTO(this LogonUserInfo logonUserInfo)
		{
			return Mapper.Map<LogonUserInfo, LogonUserInfoDTO>(logonUserInfo);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public static IList<LogonUserInfoDTO> ToDTO(this IList<LogonUserInfo> list)
		{
			IList<LogonUserInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<LogonUserInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010A90 File Offset: 0x0000EC90
		public static LogonUserInfo ToDomainObject(this LogonUserInfoDTO logonUserInfo)
		{
			return Mapper.Map<LogonUserInfoDTO, LogonUserInfo>(logonUserInfo);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		public static IList<LogonUserInfo> ToDomainObject(this IList<LogonUserInfoDTO> list)
		{
			IList<LogonUserInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<LogonUserInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
