using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Accommodations;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C3 RID: 451
	public static class ExamRequestMapper
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x00021508 File Offset: 0x0001F708
		static ExamRequestMapper()
		{
			LookupCourseBaseWithPrimaryInstructorMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			AccommodationDataMapper.CreateMap();
			Mapper.CreateMap<ExamRequestDTO, ExamRequest>().ForMember((ExamRequest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ExamRequestDTO> m)
			{
				m.Ignore();
			}).ForMember((ExamRequest pb) => pb.AccommodationsSelected, delegate(IMemberConfigurationExpression<ExamRequestDTO> m)
			{
				m.MapFrom<List<AccommodationData>>((ExamRequestDTO pbdto) => (pbdto.AccommodationsSelected == null) ? null : pbdto.AccommodationsSelected.ToList<AccommodationDataDTO>().ConvertAll<AccommodationData>((AccommodationDataDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<ExamRequest, ExamRequestDTO>().ForMember((ExamRequestDTO pb) => pb.AccommodationsSelected, delegate(IMemberConfigurationExpression<ExamRequest> m)
			{
				m.MapFrom<List<AccommodationDataDTO>>((ExamRequest pbdto) => (pbdto.AccommodationsSelected == null) ? null : pbdto.AccommodationsSelected.ToList<AccommodationData>().ConvertAll<AccommodationDataDTO>((AccommodationData g) => g.ToDTO()));
			});
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00021634 File Offset: 0x0001F834
		public static ExamRequest ToDomainObject(this ExamRequestDTO accommodationForTestDTO)
		{
			return Mapper.Map<ExamRequestDTO, ExamRequest>(accommodationForTestDTO);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002164C File Offset: 0x0001F84C
		public static ExamRequestDTO ToDTO(this ExamRequest accommodationForTest)
		{
			return Mapper.Map<ExamRequest, ExamRequestDTO>(accommodationForTest);
		}
	}
}
