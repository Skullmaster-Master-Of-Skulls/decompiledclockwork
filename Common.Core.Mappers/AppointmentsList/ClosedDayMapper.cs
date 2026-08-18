using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.Public.Entities.AppointmentsList;

namespace TechnoPro.Common.Core.Mappers.AppointmentsList
{
	// Token: 0x020001FC RID: 508
	public static class ClosedDayMapper
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00024B20 File Offset: 0x00022D20
		static ClosedDayMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ClosedDayDTO, ClosedDay>().ForMember((ClosedDay pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClosedDayDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ClosedDay, ClosedDayDTO>();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00024BA4 File Offset: 0x00022DA4
		public static ClosedDay ToDomainObject(this ClosedDayDTO closedDayDTO)
		{
			return Mapper.Map<ClosedDayDTO, ClosedDay>(closedDayDTO);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00024BBC File Offset: 0x00022DBC
		public static ClosedDayDTO ToDTO(this ClosedDay closedDay)
		{
			return Mapper.Map<ClosedDay, ClosedDayDTO>(closedDay);
		}
	}
}
