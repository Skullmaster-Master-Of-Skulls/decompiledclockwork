using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001BF RID: 447
	public static class ClassTestForDisplayMapper
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x000210C0 File Offset: 0x0001F2C0
		static ClassTestForDisplayMapper()
		{
			LookupCourseBaseWithPrimaryInstructorMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<ClassTestForDisplayDTO, ClassTestForDisplay>().ForMember((ClassTestForDisplay pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClassTestForDisplayDTO> m)
			{
				m.Ignore();
			}).ForMember((ClassTestForDisplay pb) => pb.CourseWithPrimaryInstructor, delegate(IMemberConfigurationExpression<ClassTestForDisplayDTO> m)
			{
				m.MapFrom<LookupCourseBaseWithPrimaryInstructor>((ClassTestForDisplayDTO pbdto) => (pbdto.CourseWithPrimaryInstructor == null) ? null : pbdto.CourseWithPrimaryInstructor.ToDomainObject());
			}).ForMember((ClassTestForDisplay pb) => pb.InstructorFormData, delegate(IMemberConfigurationExpression<ClassTestForDisplayDTO> m)
			{
				m.MapFrom<List<DynamicData>>((ClassTestForDisplayDTO pbdto) => (pbdto.InstructorFormData == null) ? null : (from g in pbdto.InstructorFormData
				select g.ToDomainObject()).ToList<DynamicData>());
			});
			Mapper.CreateMap<ClassTestForDisplay, ClassTestForDisplayDTO>().ForMember((ClassTestForDisplayDTO pb) => pb.CourseWithPrimaryInstructor, delegate(IMemberConfigurationExpression<ClassTestForDisplay> m)
			{
				m.MapFrom<LookupCourseBaseWithPrimaryInstructorDTO>((ClassTestForDisplay pbdto) => (pbdto.CourseWithPrimaryInstructor == null) ? null : pbdto.CourseWithPrimaryInstructor.ToDTO());
			}).ForMember((ClassTestForDisplayDTO pb) => pb.InstructorFormData, delegate(IMemberConfigurationExpression<ClassTestForDisplay> m)
			{
				m.MapFrom<List<DynamicDataDTO>>((ClassTestForDisplay pbdto) => (pbdto.InstructorFormData == null) ? null : (from g in pbdto.InstructorFormData
				select g.ToDTO()).ToList<DynamicDataDTO>());
			});
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00021280 File Offset: 0x0001F480
		public static ClassTestForDisplay ToDomainObject(this ClassTestForDisplayDTO dto)
		{
			return Mapper.Map<ClassTestForDisplayDTO, ClassTestForDisplay>(dto);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00021298 File Offset: 0x0001F498
		public static ClassTestForDisplayDTO ToDTO(this ClassTestForDisplay item)
		{
			return Mapper.Map<ClassTestForDisplay, ClassTestForDisplayDTO>(item);
		}
	}
}
