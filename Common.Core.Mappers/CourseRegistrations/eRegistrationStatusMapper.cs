using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x02000163 RID: 355
	public static class eRegistrationStatusMapper
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		static eRegistrationStatusMapper()
		{
			Mapper.CreateMap<eRegistrationStatusDTO, eRegistrationStatus>();
			Mapper.CreateMap<eRegistrationStatus, eRegistrationStatusDTO>();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		public static eRegistrationStatus ToDomainObject(this eRegistrationStatusDTO courseRegistrationDTO)
		{
			return Mapper.Map<eRegistrationStatusDTO, eRegistrationStatus>(courseRegistrationDTO);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		public static eRegistrationStatusDTO ToDTO(this eRegistrationStatus courseRegistration)
		{
			return Mapper.Map<eRegistrationStatus, eRegistrationStatusDTO>(courseRegistration);
		}
	}
}
