using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A5 RID: 165
	public static class PersonBaseWithExtendedInfoMapper
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000EE1C File Offset: 0x0000D01C
		static PersonBaseWithExtendedInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<PersonBaseWithExtendedInfoDTO, PersonBaseWithExtendedInfo>().ForMember((PersonBaseWithExtendedInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<PersonBaseWithExtendedInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((PersonBaseWithExtendedInfo pb) => (object)pb.CoreGroup, delegate(IMemberConfigurationExpression<PersonBaseWithExtendedInfoDTO> m)
			{
				m.MapFrom<eCoreGroup>((PersonBaseWithExtendedInfoDTO pbdto) => (eCoreGroup)pbdto.CoreGroup);
			});
			Mapper.CreateMap<PersonBaseWithExtendedInfo, PersonBaseWithExtendedInfoDTO>().ForMember((PersonBaseWithExtendedInfoDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<PersonBaseWithExtendedInfo> m)
			{
				m.Ignore();
			}).ForMember((PersonBaseWithExtendedInfoDTO pb) => (object)pb.CoreGroup, delegate(IMemberConfigurationExpression<PersonBaseWithExtendedInfo> m)
			{
				m.MapFrom<eCoreGroupDTO>((PersonBaseWithExtendedInfo pbdto) => (eCoreGroupDTO)pbdto.CoreGroup);
			});
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public static PersonBaseWithExtendedInfo ToDomainObject(this PersonBaseWithExtendedInfoDTO groupDTO)
		{
			return Mapper.Map<PersonBaseWithExtendedInfoDTO, PersonBaseWithExtendedInfo>(groupDTO);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public static PersonBaseWithExtendedInfoDTO ToDTO(this PersonBaseWithExtendedInfo group)
		{
			return Mapper.Map<PersonBaseWithExtendedInfo, PersonBaseWithExtendedInfoDTO>(group);
		}
	}
}
