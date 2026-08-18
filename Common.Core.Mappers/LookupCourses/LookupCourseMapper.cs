using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000DB RID: 219
	public static class LookupCourseMapper
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x00011B94 File Offset: 0x0000FD94
		static LookupCourseMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			AlternateContactMapper.CreateMap();
			LookupInstructorMapper.CreateMap();
			LookupTimetableItemMapper.CreateMap();
			Mapper.CreateMap<LookupCourseDTO, LookupCourse>().ForMember((LookupCourse pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupCourseDTO> m)
			{
				m.Ignore();
			}).ForMember((LookupCourse pb) => pb.Instructors, delegate(IMemberConfigurationExpression<LookupCourseDTO> m)
			{
				m.MapFrom<List<LookupInstructor>>((LookupCourseDTO pbdto) => (pbdto.Instructors == null) ? null : (from g in pbdto.Instructors
				select g.ToDomainObject()).ToList<LookupInstructor>());
			}).ForMember((LookupCourse pb) => pb.AlternateContacts, delegate(IMemberConfigurationExpression<LookupCourseDTO> m)
			{
				m.MapFrom<List<AlternateContact>>((LookupCourseDTO pbdto) => (pbdto.AlternateContacts == null) ? null : (from g in pbdto.AlternateContacts
				select g.ToDomainObject()).ToList<AlternateContact>());
			}).ForMember((LookupCourse pb) => pb.TimetableItems, delegate(IMemberConfigurationExpression<LookupCourseDTO> m)
			{
				m.MapFrom<List<LookupTimetableItem>>((LookupCourseDTO pbdto) => (pbdto.TimetableItems == null) ? null : (from g in pbdto.TimetableItems
				select g.ToDomainObject()).ToList<LookupTimetableItem>());
			});
			Mapper.CreateMap<LookupCourse, LookupCourseDTO>().ForMember((LookupCourseDTO pb) => pb.Instructors, delegate(IMemberConfigurationExpression<LookupCourse> m)
			{
				m.MapFrom<List<LookupInstructorDTO>>((LookupCourse pbdto) => (pbdto.Instructors == null) ? null : (from g in pbdto.Instructors
				select g.ToDTO()).ToList<LookupInstructorDTO>());
			}).ForMember((LookupCourseDTO pb) => pb.AlternateContacts, delegate(IMemberConfigurationExpression<LookupCourse> m)
			{
				m.MapFrom<List<AlternateContactDTO>>((LookupCourse pbdto) => (pbdto.AlternateContacts == null) ? null : (from g in pbdto.AlternateContacts
				select g.ToDTO()).ToList<AlternateContactDTO>());
			}).ForMember((LookupCourseDTO pb) => pb.TimetableItems, delegate(IMemberConfigurationExpression<LookupCourse> m)
			{
				m.MapFrom<List<LookupTimetableItemDTO>>((LookupCourse pbdto) => (pbdto.TimetableItems == null) ? null : (from g in pbdto.TimetableItems
				select g.ToDTO()).ToList<LookupTimetableItemDTO>());
			});
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011DFC File Offset: 0x0000FFFC
		public static LookupCourse ToDomainObject(this LookupCourseDTO lookupCourseDTO)
		{
			return Mapper.Map<LookupCourseDTO, LookupCourse>(lookupCourseDTO);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00011E14 File Offset: 0x00010014
		public static LookupCourseDTO ToDTO(this LookupCourse lookupCourse)
		{
			return Mapper.Map<LookupCourse, LookupCourseDTO>(lookupCourse);
		}
	}
}
