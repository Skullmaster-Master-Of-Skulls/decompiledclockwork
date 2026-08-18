using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000007 RID: 7
	public static class CampusMapper
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002908 File Offset: 0x00000B08
		static CampusMapper()
		{
			Mapper.CreateMap<SchoolCampus, SchoolCampusDTO>();
			Mapper.CreateMap<SchoolCampusDTO, SchoolCampus>().ForMember((SchoolCampus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SchoolCampusDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002984 File Offset: 0x00000B84
		public static SchoolCampus ToDomainObject(this SchoolCampusDTO campusDto)
		{
			return Mapper.Map<SchoolCampusDTO, SchoolCampus>(campusDto);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000299C File Offset: 0x00000B9C
		public static SchoolCampusDTO ToDTO(this SchoolCampus campus)
		{
			return Mapper.Map<SchoolCampus, SchoolCampusDTO>(campus);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029B4 File Offset: 0x00000BB4
		public static IList<SchoolCampus> ToDomainObject(this IList<SchoolCampusDTO> list)
		{
			IList<SchoolCampus> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SchoolCampus>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029F8 File Offset: 0x00000BF8
		public static IList<SchoolCampusDTO> ToDTO(this IList<SchoolCampus> list)
		{
			IList<SchoolCampusDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SchoolCampusDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
