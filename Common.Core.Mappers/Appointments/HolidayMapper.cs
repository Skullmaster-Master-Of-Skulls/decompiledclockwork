using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001B4 RID: 436
	public static class HolidayMapper
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x000206E8 File Offset: 0x0001E8E8
		static HolidayMapper()
		{
			Mapper.CreateMap<HolidayDTO, Holiday>().ForMember((Holiday pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<HolidayDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Holiday, HolidayDTO>();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00020764 File Offset: 0x0001E964
		public static Holiday ToDomainObject(this HolidayDTO appointmentIconDTO)
		{
			return Mapper.Map<HolidayDTO, Holiday>(appointmentIconDTO);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0002077C File Offset: 0x0001E97C
		public static HolidayDTO ToDTO(this Holiday appointmentIcon)
		{
			return Mapper.Map<Holiday, HolidayDTO>(appointmentIcon);
		}
	}
}
