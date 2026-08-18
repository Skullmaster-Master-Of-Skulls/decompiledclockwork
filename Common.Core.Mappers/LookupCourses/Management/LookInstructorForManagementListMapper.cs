using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.Core.Mappers.LookupCourses.Management
{
	// Token: 0x020000E2 RID: 226
	public static class LookInstructorForManagementListMapper
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001216C File Offset: 0x0001036C
		static LookInstructorForManagementListMapper()
		{
			LookupInstructorForManagementMapper.CreateMap();
			Mapper.CreateMap<LookInstructorForManagementListDTO, LookInstructorForManagementList>().ForMember((LookInstructorForManagementList pb) => pb.Instructors, delegate(IMemberConfigurationExpression<LookInstructorForManagementListDTO> m)
			{
				m.MapFrom<List<LookupInstructorForManagement>>((LookInstructorForManagementListDTO pbdto) => (pbdto.Instructors == null) ? null : (from g in pbdto.Instructors
				select g.ToDomainObject()).ToList<LookupInstructorForManagement>());
			});
			Mapper.CreateMap<LookInstructorForManagementList, LookInstructorForManagementListDTO>().ForMember((LookInstructorForManagementListDTO pb) => pb.Instructors, delegate(IMemberConfigurationExpression<LookInstructorForManagementList> m)
			{
				m.MapFrom<List<LookupInstructorForManagementDTO>>((LookInstructorForManagementList pbdto) => (pbdto.Instructors == null) ? null : (from g in pbdto.Instructors
				select g.ToDTO()).ToList<LookupInstructorForManagementDTO>());
			});
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012228 File Offset: 0x00010428
		public static LookInstructorForManagementList ToDomainObject(this LookInstructorForManagementListDTO sessionDTO)
		{
			return Mapper.Map<LookInstructorForManagementListDTO, LookInstructorForManagementList>(sessionDTO);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012240 File Offset: 0x00010440
		public static LookInstructorForManagementListDTO ToDTO(this LookInstructorForManagementList session)
		{
			return Mapper.Map<LookInstructorForManagementList, LookInstructorForManagementListDTO>(session);
		}
	}
}
