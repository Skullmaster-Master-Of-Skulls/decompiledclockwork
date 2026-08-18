using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Vets
{
	// Token: 0x02000011 RID: 17
	public static class VetsStudentCardInfoMapper
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000390C File Offset: 0x00001B0C
		static VetsStudentCardInfoMapper()
		{
			VetsStudentCardInfoItemMapper.CreateMap();
			Mapper.CreateMap<VetsStudentCardInfo, VetsStudentCardInfoDTO>().ForMember((VetsStudentCardInfoDTO pb) => pb.CurrentAndFutureItems, delegate(IMemberConfigurationExpression<VetsStudentCardInfo> m)
			{
				m.MapFrom<List<VetsStudentCardInfoItemDTO>>((VetsStudentCardInfo pbdto) => (pbdto.CurrentAndFutureItems == null) ? null : (from g in pbdto.CurrentAndFutureItems
				select g.ToDTO()).ToList<VetsStudentCardInfoItemDTO>());
			});
			Mapper.CreateMap<VetsStudentCardInfoDTO, VetsStudentCardInfo>().ForMember((VetsStudentCardInfo pb) => pb.CurrentAndFutureItems, delegate(IMemberConfigurationExpression<VetsStudentCardInfoDTO> m)
			{
				m.MapFrom<List<VetsStudentCardInfoItem>>((VetsStudentCardInfoDTO pbdto) => (pbdto.CurrentAndFutureItems == null) ? null : (from g in pbdto.CurrentAndFutureItems
				select g.ToDomainObject()).ToList<VetsStudentCardInfoItem>());
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000039C8 File Offset: 0x00001BC8
		public static VetsStudentCardInfo ToDomainObject(this VetsStudentCardInfoDTO surveyDTO)
		{
			return Mapper.Map<VetsStudentCardInfoDTO, VetsStudentCardInfo>(surveyDTO);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000039E0 File Offset: 0x00001BE0
		public static VetsStudentCardInfoDTO ToDTO(this VetsStudentCardInfo survey)
		{
			return Mapper.Map<VetsStudentCardInfo, VetsStudentCardInfoDTO>(survey);
		}
	}
}
