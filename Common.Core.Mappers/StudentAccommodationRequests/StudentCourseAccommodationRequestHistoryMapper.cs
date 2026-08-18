using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x02000061 RID: 97
	public static class StudentCourseAccommodationRequestHistoryMapper
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000A770 File Offset: 0x00008970
		static StudentCourseAccommodationRequestHistoryMapper()
		{
			PersonBaseMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			StudentCourseAccommodationRequestHistoryItemMapper.CreateMap();
			Mapper.CreateMap<StudentCourseAccommodationRequestHistoryDTO, StudentCourseAccommodationRequestHistory>().ForMember((StudentCourseAccommodationRequestHistory pb) => pb.HistoryItems, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryDTO> m)
			{
				m.MapFrom<List<StudentCourseAccommodationRequestHistoryItem>>((StudentCourseAccommodationRequestHistoryDTO pbdto) => (pbdto.HistoryItems == null) ? null : (from g in pbdto.HistoryItems
				select g.ToDomainObject()).ToList<StudentCourseAccommodationRequestHistoryItem>());
			});
			Mapper.CreateMap<StudentCourseAccommodationRequestHistory, StudentCourseAccommodationRequestHistoryDTO>().ForMember((StudentCourseAccommodationRequestHistoryDTO pb) => pb.HistoryItems, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistory> m)
			{
				m.MapFrom<List<StudentCourseAccommodationRequestHistoryItemDTO>>((StudentCourseAccommodationRequestHistory pbdto) => (pbdto.HistoryItems == null) ? null : (from g in pbdto.HistoryItems
				select g.ToDTO()).ToList<StudentCourseAccommodationRequestHistoryItemDTO>());
			});
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000A838 File Offset: 0x00008A38
		public static StudentCourseAccommodationRequestHistory ToDomainObject(this StudentCourseAccommodationRequestHistoryDTO dto)
		{
			return Mapper.Map<StudentCourseAccommodationRequestHistoryDTO, StudentCourseAccommodationRequestHistory>(dto);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000A850 File Offset: 0x00008A50
		public static StudentCourseAccommodationRequestHistoryDTO ToDTO(this StudentCourseAccommodationRequestHistory item)
		{
			return Mapper.Map<StudentCourseAccommodationRequestHistory, StudentCourseAccommodationRequestHistoryDTO>(item);
		}
	}
}
