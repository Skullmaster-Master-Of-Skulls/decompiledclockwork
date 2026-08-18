using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001B5 RID: 437
	public static class IconInfoMapper
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00020794 File Offset: 0x0001E994
		static IconInfoMapper()
		{
			Mapper.CreateMap<IconInfoDTO, IconInfo>().ForMember((IconInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<IconInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((IconInfo pb) => (object)pb.IconNum, delegate(IMemberConfigurationExpression<IconInfoDTO> m)
			{
				m.MapFrom<int>((IconInfoDTO pbdto) => pbdto.IconNum);
			});
			Mapper.CreateMap<IconInfo, IconInfoDTO>().ForMember((IconInfoDTO pb) => (object)pb.IconNum, delegate(IMemberConfigurationExpression<IconInfo> m)
			{
				m.MapFrom<int>((IconInfo pbdto) => pbdto.IconNum);
			});
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000208CC File Offset: 0x0001EACC
		public static IconInfo ToDomainObject(this IconInfoDTO iconDTO)
		{
			return Mapper.Map<IconInfoDTO, IconInfo>(iconDTO);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000208E4 File Offset: 0x0001EAE4
		public static IconInfoDTO ToDTO(this IconInfo icon)
		{
			return Mapper.Map<IconInfo, IconInfoDTO>(icon);
		}
	}
}
