using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C6 RID: 454
	public static class SittingBaseMapper
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x000218AC File Offset: 0x0001FAAC
		static SittingBaseMapper()
		{
			PersonBaseMapper.CreateMap();
			AppointmentRoomMapper.CreateMap();
			Mapper.CreateMap<SittingBaseDTO, SittingBase>().ForMember((SittingBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SittingBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<SittingBase, SittingBaseDTO>();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00021934 File Offset: 0x0001FB34
		public static SittingBase ToDomainObject(this SittingBaseDTO sittingDTO)
		{
			return Mapper.Map<SittingBaseDTO, SittingBase>(sittingDTO);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002194C File Offset: 0x0001FB4C
		public static SittingBaseDTO ToDTO(this SittingBase sitting)
		{
			return Mapper.Map<SittingBase, SittingBaseDTO>(sitting);
		}
	}
}
