using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Core.Mappers.ServiceProvider;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.Notetaking;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.DataSync.Notetaking
{
	// Token: 0x02000147 RID: 327
	public static class NotetakerWithExternalCoursesMapper
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x0001A288 File Offset: 0x00018488
		static NotetakerWithExternalCoursesMapper()
		{
			SPProviderMapper.CreateMap();
			DataSyncExternalCourseMapper.CreateMap();
			Mapper.CreateMap<NotetakerWithExternalCoursesDTO, NotetakerWithExternalCourses>().ForMember((NotetakerWithExternalCourses pb) => pb.Notetaker, delegate(IMemberConfigurationExpression<NotetakerWithExternalCoursesDTO> m)
			{
				m.MapFrom<SPProvider>((NotetakerWithExternalCoursesDTO pbdto) => (pbdto.Notetaker == null) ? null : pbdto.Notetaker.ToDomainObject());
			}).ForMember((NotetakerWithExternalCourses pb) => pb.ExternalCourses, delegate(IMemberConfigurationExpression<NotetakerWithExternalCoursesDTO> m)
			{
				m.MapFrom<List<DataSyncExternalCourse>>((NotetakerWithExternalCoursesDTO pbdto) => (pbdto.ExternalCourses == null) ? null : (from g in pbdto.ExternalCourses
				select g.ToDomainObject()).ToList<DataSyncExternalCourse>());
			});
			Mapper.CreateMap<NotetakerWithExternalCourses, NotetakerWithExternalCoursesDTO>().ForMember((NotetakerWithExternalCoursesDTO pb) => pb.Notetaker, delegate(IMemberConfigurationExpression<NotetakerWithExternalCourses> m)
			{
				m.MapFrom<SPProviderDTO>((NotetakerWithExternalCourses pbdto) => (pbdto.Notetaker == null) ? null : pbdto.Notetaker.ToDTO());
			}).ForMember((NotetakerWithExternalCoursesDTO pb) => pb.ExternalCourses, delegate(IMemberConfigurationExpression<NotetakerWithExternalCourses> m)
			{
				m.MapFrom<List<DataSyncExternalCourseDTO>>((NotetakerWithExternalCourses pbdto) => (pbdto.ExternalCourses == null) ? null : (from g in pbdto.ExternalCourses
				select g.ToDTO()).ToList<DataSyncExternalCourseDTO>());
			});
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A3E8 File Offset: 0x000185E8
		public static NotetakerWithExternalCourses ToDomainObject(this NotetakerWithExternalCoursesDTO dto)
		{
			return Mapper.Map<NotetakerWithExternalCoursesDTO, NotetakerWithExternalCourses>(dto);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A400 File Offset: 0x00018600
		public static NotetakerWithExternalCoursesDTO ToDTO(this NotetakerWithExternalCourses item)
		{
			return Mapper.Map<NotetakerWithExternalCourses, NotetakerWithExternalCoursesDTO>(item);
		}
	}
}
