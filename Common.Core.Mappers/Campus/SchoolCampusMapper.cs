using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers.Campus
{
	// Token: 0x0200017B RID: 379
	public static class SchoolCampusMapper
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x0001DB68 File Offset: 0x0001BD68
		static SchoolCampusMapper()
		{
			Mapper.CreateMap<SchoolCampusDTO, SchoolCampus>();
			Mapper.CreateMap<SchoolCampus, SchoolCampusDTO>();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001DB78 File Offset: 0x0001BD78
		public static SchoolCampus ToDomainObject(this SchoolCampusDTO dto)
		{
			return Mapper.Map<SchoolCampusDTO, SchoolCampus>(dto);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001DB90 File Offset: 0x0001BD90
		public static SchoolCampusDTO ToDTO(this SchoolCampus item)
		{
			return Mapper.Map<SchoolCampus, SchoolCampusDTO>(item);
		}
	}
}
