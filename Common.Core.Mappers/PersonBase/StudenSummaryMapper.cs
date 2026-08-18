using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A9 RID: 169
	public static class StudenSummaryMapper
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000F18C File Offset: 0x0000D38C
		static StudenSummaryMapper()
		{
			StudentCommonInfoMapper.CreateMap();
			BaseExtendedAppointmentMapper.CreateMap();
			Mapper.CreateMap<StudentSummaryDTO, StudentSummary>().ForMember((StudentSummary pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentSummaryDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentSummary, StudentSummaryDTO>();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F214 File Offset: 0x0000D414
		public static StudentSummary ToDomainObject(this StudentSummaryDTO dto)
		{
			return Mapper.Map<StudentSummaryDTO, StudentSummary>(dto);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F22C File Offset: 0x0000D42C
		public static StudentSummaryDTO ToDTO(this StudentSummary item)
		{
			return Mapper.Map<StudentSummary, StudentSummaryDTO>(item);
		}
	}
}
