using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000E1 RID: 225
	public static class SessionMapper
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00012124 File Offset: 0x00010324
		static SessionMapper()
		{
			AcademicTermMapper.CreateMap();
			Mapper.CreateMap<SessionDTO, Session>();
			Mapper.CreateMap<Session, SessionDTO>();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001213C File Offset: 0x0001033C
		public static Session ToDomainObject(this SessionDTO sessionDTO)
		{
			return Mapper.Map<SessionDTO, Session>(sessionDTO);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00012154 File Offset: 0x00010354
		public static SessionDTO ToDTO(this Session session)
		{
			return Mapper.Map<Session, SessionDTO>(session);
		}
	}
}
