using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000212 RID: 530
	public static class BasicMediaContentMapper
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x00026B7E File Offset: 0x00024D7E
		static BasicMediaContentMapper()
		{
			MediaContentIdentifierMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			MediaPublisherMapper.CreateMap();
			Mapper.CreateMap<BasicMediaContent, BasicMediaContentDTO>();
			Mapper.CreateMap<BasicMediaContentDTO, BasicMediaContent>();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00026BA0 File Offset: 0x00024DA0
		public static BasicMediaContent ToDomainObject(this BasicMediaContentDTO dto)
		{
			return Mapper.Map<BasicMediaContentDTO, BasicMediaContent>(dto);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00026BB8 File Offset: 0x00024DB8
		public static IList<BasicMediaContent> ToDomainObject(this IList<BasicMediaContentDTO> dto)
		{
			return Mapper.Map<IList<BasicMediaContentDTO>, IList<BasicMediaContent>>(dto);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00026BD0 File Offset: 0x00024DD0
		public static BasicMediaContentDTO ToDTO(this BasicMediaContent bo)
		{
			return Mapper.Map<BasicMediaContent, BasicMediaContentDTO>(bo);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00026BE8 File Offset: 0x00024DE8
		public static IList<BasicMediaContentDTO> ToDTO(this IList<BasicMediaContent> bo)
		{
			return Mapper.Map<IList<BasicMediaContent>, IList<BasicMediaContentDTO>>(bo);
		}
	}
}
