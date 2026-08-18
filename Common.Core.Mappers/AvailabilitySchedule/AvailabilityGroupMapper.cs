using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x0200017F RID: 383
	public static class AvailabilityGroupMapper
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		static AvailabilityGroupMapper()
		{
			Mapper.CreateMap<AvailabilityGroupDTO, AvailabilityGroup>().ForMember((AvailabilityGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AvailabilityGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AvailabilityGroup, AvailabilityGroupDTO>();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001DF4C File Offset: 0x0001C14C
		public static AvailabilityGroup ToDomainObject(this AvailabilityGroupDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityGroupDTO, AvailabilityGroup>(appTypeGroupDTO);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001DF64 File Offset: 0x0001C164
		public static AvailabilityGroupDTO ToDTO(this AvailabilityGroup appTypeGroup)
		{
			return Mapper.Map<AvailabilityGroup, AvailabilityGroupDTO>(appTypeGroup);
		}
	}
}
