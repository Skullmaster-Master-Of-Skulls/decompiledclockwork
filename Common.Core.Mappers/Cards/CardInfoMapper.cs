using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.Vets;
using TechnoPro.Common.Public.Entities.Cards;
using TechnoPro.Common.Public.Entities.Cards.CardInfos;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Cards
{
	// Token: 0x02000179 RID: 377
	public static class CardInfoMapper
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x0001D958 File Offset: 0x0001BB58
		static CardInfoMapper()
		{
			CardLayoutMapper.CreateMap();
			VetsStudentCardInfoMapper.CreateMap();
			Mapper.CreateMap<CardInfoDTO, CardInfo>().Include<CardInfoVetsApplicationsStudentDTO, CardInfoVetsApplicationsStudent>();
			Mapper.CreateMap<CardInfo, CardInfoDTO>().Include<CardInfoVetsApplicationsStudent, CardInfoVetsApplicationsStudentDTO>();
			Mapper.CreateMap<CardInfoVetsApplicationsStudentDTO, CardInfoVetsApplicationsStudent>().ForMember((CardInfoVetsApplicationsStudent pb) => pb.CardInfo, delegate(IMemberConfigurationExpression<CardInfoVetsApplicationsStudentDTO> m)
			{
				m.MapFrom<VetsStudentCardInfo>((CardInfoVetsApplicationsStudentDTO pbdto) => (pbdto.CardInfo == null) ? null : pbdto.CardInfo.ToDomainObject());
			}).ForMember((CardInfoVetsApplicationsStudent pb) => pb.Layout, delegate(IMemberConfigurationExpression<CardInfoVetsApplicationsStudentDTO> m)
			{
				m.MapFrom<CardLayout>((CardInfoVetsApplicationsStudentDTO pbdto) => (pbdto.Layout == null) ? null : pbdto.Layout.ToDomainObject());
			});
			Mapper.CreateMap<CardInfoVetsApplicationsStudent, CardInfoVetsApplicationsStudentDTO>().ForMember((CardInfoVetsApplicationsStudentDTO pb) => pb.CardInfo, delegate(IMemberConfigurationExpression<CardInfoVetsApplicationsStudent> m)
			{
				m.MapFrom<VetsStudentCardInfoDTO>((CardInfoVetsApplicationsStudent pbdto) => (pbdto.CardInfo == null) ? null : pbdto.CardInfo.ToDTO());
			}).ForMember((CardInfoVetsApplicationsStudentDTO pb) => pb.Layout, delegate(IMemberConfigurationExpression<CardInfoVetsApplicationsStudent> m)
			{
				m.MapFrom<CardLayoutDTO>((CardInfoVetsApplicationsStudent pbdto) => (pbdto.Layout == null) ? null : pbdto.Layout.ToDTO());
			});
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public static CardInfo ToDomainObject(this CardInfoDTO dto)
		{
			return (CardInfo)Mapper.Map(dto, dto.GetType(), typeof(CardInfo));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public static CardInfoDTO ToDTO(this CardInfo item)
		{
			return (CardInfoDTO)Mapper.Map(item, item.GetType(), typeof(CardInfoDTO));
		}
	}
}
