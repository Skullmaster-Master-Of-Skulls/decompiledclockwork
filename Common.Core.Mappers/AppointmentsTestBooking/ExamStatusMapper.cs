using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C4 RID: 452
	public static class ExamStatusMapper
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00021664 File Offset: 0x0001F864
		static ExamStatusMapper()
		{
			PersonBaseMapper.CreateMap();
			TestMapper.CreateMap();
			Mapper.CreateMap<ExamStatusDTO, ExamStatus>().ForMember((ExamStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ExamStatusDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ExamStatus, ExamStatusDTO>();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000216EC File Offset: 0x0001F8EC
		public static ExamStatus ToDomainObject(this ExamStatusDTO sittingDTO)
		{
			return Mapper.Map<ExamStatusDTO, ExamStatus>(sittingDTO);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00021704 File Offset: 0x0001F904
		public static ExamStatusDTO ToDTO(this ExamStatus sitting)
		{
			return Mapper.Map<ExamStatus, ExamStatusDTO>(sitting);
		}
	}
}
