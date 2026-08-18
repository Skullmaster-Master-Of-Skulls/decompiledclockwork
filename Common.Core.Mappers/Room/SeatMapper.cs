using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.Core.Mappers.Room
{
	// Token: 0x02000085 RID: 133
	public static class SeatMapper
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000D040 File Offset: 0x0000B240
		static SeatMapper()
		{
			CampusMapper.CreateMap();
			Mapper.CreateMap<SeatDTO, Seat>().ForMember((Seat pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SeatDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Seat, SeatDTO>();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		public static Seat ToDomainObject(this SeatDTO dto)
		{
			return Mapper.Map<SeatDTO, Seat>(dto);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public static SeatDTO ToDTO(this Seat item)
		{
			return Mapper.Map<Seat, SeatDTO>(item);
		}
	}
}
