using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000005 RID: 5
	public static class DropBoxUserMapper
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000026B0 File Offset: 0x000008B0
		static DropBoxUserMapper()
		{
			Mapper.CreateMap<DropBox_User, IM_User>();
			Mapper.CreateMap<IM_User, DropBox_User>().ForMember((DropBox_User bo) => bo.Id, delegate(IMemberConfigurationExpression<IM_User> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002720 File Offset: 0x00000920
		public static IM_User ToDTO(this DropBox_User bo)
		{
			return Mapper.Map<DropBox_User, IM_User>(bo);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002738 File Offset: 0x00000938
		public static DropBox_User ToDomainObject(this IM_User dto)
		{
			return Mapper.Map<IM_User, DropBox_User>(dto);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002750 File Offset: 0x00000950
		public static IList<IM_User> ToDTO(this IList<DropBox_User> list)
		{
			IList<IM_User> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<IM_User>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002794 File Offset: 0x00000994
		public static IList<DropBox_User> ToDomainObject(this IList<IM_User> list)
		{
			IList<DropBox_User> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<DropBox_User>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
