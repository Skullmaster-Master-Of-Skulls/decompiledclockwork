using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F0 RID: 496
	public static class PotentialTestMapper
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x000241FC File Offset: 0x000223FC
		static PotentialTestMapper()
		{
			PotentialTestMethodFoundNoteMapper.CreateMap();
			TestMapper.CreateMap();
			Mapper.CreateMap<PotentialTestDTO, PotentialTest>().ForMember((PotentialTest pb) => pb.MethodFoundNotes, delegate(IMemberConfigurationExpression<PotentialTestDTO> m)
			{
				m.MapFrom<List<PotentialTestMethodFoundNote>>((PotentialTestDTO pbdto) => (pbdto.MethodFoundNotes == null) ? null : pbdto.MethodFoundNotes.ToList<PotentialTestMethodFoundNoteDTO>().ConvertAll<PotentialTestMethodFoundNote>((PotentialTestMethodFoundNoteDTO g) => g.ToDomainObject()));
			}).ForMember((PotentialTest pb) => pb.Test, delegate(IMemberConfigurationExpression<PotentialTestDTO> m)
			{
				m.MapFrom<Test>((PotentialTestDTO pbdto) => (pbdto.Test == null) ? null : pbdto.Test.ToDomainObject());
			});
			Mapper.CreateMap<PotentialTest, PotentialTestDTO>().ForMember((PotentialTestDTO pb) => pb.MethodFoundNotes, delegate(IMemberConfigurationExpression<PotentialTest> m)
			{
				m.MapFrom<List<PotentialTestMethodFoundNoteDTO>>((PotentialTest pbdto) => (pbdto.MethodFoundNotes == null) ? null : pbdto.MethodFoundNotes.ToList<PotentialTestMethodFoundNote>().ConvertAll<PotentialTestMethodFoundNoteDTO>((PotentialTestMethodFoundNote g) => g.ToDTO()));
			}).ForMember((PotentialTestDTO pb) => pb.Test, delegate(IMemberConfigurationExpression<PotentialTest> m)
			{
				m.MapFrom<TestDTO>((PotentialTest pbdto) => (pbdto.Test == null) ? null : pbdto.Test.ToDTO());
			});
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002435C File Offset: 0x0002255C
		public static PotentialTest ToDomainObject(this PotentialTestDTO accommodationForTestDTO)
		{
			return Mapper.Map<PotentialTestDTO, PotentialTest>(accommodationForTestDTO);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00024374 File Offset: 0x00022574
		public static PotentialTestDTO ToDTO(this PotentialTest accommodationForTest)
		{
			return Mapper.Map<PotentialTest, PotentialTestDTO>(accommodationForTest);
		}
	}
}
