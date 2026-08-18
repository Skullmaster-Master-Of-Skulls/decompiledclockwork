using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x02000060 RID: 96
	public static class StudentCourseAccommodationRequestHistoryItemMapper
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000A51C File Offset: 0x0000871C
		static StudentCourseAccommodationRequestHistoryItemMapper()
		{
			PersonBaseMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<StudentCourseAccommodationRequestHistoryItemDTO, StudentCourseAccommodationRequestHistoryItem>().ForMember((StudentCourseAccommodationRequestHistoryItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItemDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentCourseAccommodationRequestHistoryItem pb) => (object)pb.SecondId, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItemDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentCourseAccommodationRequestHistoryItem pb) => pb.WhoModified, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItemDTO> m)
			{
				m.MapFrom<PersonBase>((StudentCourseAccommodationRequestHistoryItemDTO pbdto) => (pbdto.WhoModified == null) ? null : pbdto.WhoModified.ToDomainObject());
			}).ForMember((StudentCourseAccommodationRequestHistoryItem pb) => pb.Course, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItemDTO> m)
			{
				m.MapFrom<LookupCourseBase>((StudentCourseAccommodationRequestHistoryItemDTO pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDomainObject());
			});
			Mapper.CreateMap<StudentCourseAccommodationRequestHistoryItem, StudentCourseAccommodationRequestHistoryItemDTO>().ForMember((StudentCourseAccommodationRequestHistoryItemDTO pb) => pb.WhoModified, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItem> m)
			{
				m.MapFrom<PersonBaseDTO>((StudentCourseAccommodationRequestHistoryItem pbdto) => (pbdto.WhoModified == null) ? null : pbdto.WhoModified.ToDTO());
			}).ForMember((StudentCourseAccommodationRequestHistoryItemDTO pb) => pb.Course, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItem> m)
			{
				m.MapFrom<LookupCourseBaseDTO>((StudentCourseAccommodationRequestHistoryItem pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDTO());
			});
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000A740 File Offset: 0x00008940
		public static StudentCourseAccommodationRequestHistoryItem ToDomainObject(this StudentCourseAccommodationRequestHistoryItemDTO dto)
		{
			return Mapper.Map<StudentCourseAccommodationRequestHistoryItemDTO, StudentCourseAccommodationRequestHistoryItem>(dto);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000A758 File Offset: 0x00008958
		public static StudentCourseAccommodationRequestHistoryItemDTO ToDTO(this StudentCourseAccommodationRequestHistoryItem item)
		{
			return Mapper.Map<StudentCourseAccommodationRequestHistoryItem, StudentCourseAccommodationRequestHistoryItemDTO>(item);
		}
	}
}
