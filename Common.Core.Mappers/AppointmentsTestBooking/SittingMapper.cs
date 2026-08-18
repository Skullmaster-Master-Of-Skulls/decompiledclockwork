using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C7 RID: 455
	public static class SittingMapper
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x00021964 File Offset: 0x0001FB64
		static SittingMapper()
		{
			PersonBaseMapper.CreateMap();
			SittingBaseMapper.CreateMap();
			Mapper.CreateMap<SittingDTO, Sitting>().ForMember((Sitting pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SittingDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Sitting, SittingDTO>();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000219EC File Offset: 0x0001FBEC
		public static Sitting ToDomainObject(this SittingDTO sittingDTO)
		{
			return Mapper.Map<SittingDTO, Sitting>(sittingDTO);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00021A04 File Offset: 0x0001FC04
		public static SittingDTO ToDTO(this Sitting sitting)
		{
			return Mapper.Map<Sitting, SittingDTO>(sitting);
		}
	}
}
