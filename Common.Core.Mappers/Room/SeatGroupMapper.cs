using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.Core.Mappers.Room
{
	// Token: 0x02000084 RID: 132
	public static class SeatGroupMapper
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000CF8C File Offset: 0x0000B18C
		static SeatGroupMapper()
		{
			CampusMapper.CreateMap();
			Mapper.CreateMap<SeatGroupDTO, SeatGroup>().ForMember((SeatGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SeatGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<SeatGroup, SeatGroupDTO>();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D010 File Offset: 0x0000B210
		public static SeatGroup ToDomainObject(this SeatGroupDTO dto)
		{
			return Mapper.Map<SeatGroupDTO, SeatGroup>(dto);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D028 File Offset: 0x0000B228
		public static SeatGroupDTO ToDTO(this SeatGroup item)
		{
			return Mapper.Map<SeatGroup, SeatGroupDTO>(item);
		}
	}
}
