using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x0200011B RID: 283
	public static class DynamicFileDescriptionWithColDataMapper
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x000176E4 File Offset: 0x000158E4
		static DynamicFileDescriptionWithColDataMapper()
		{
			Mapper.CreateMap<DynamicFileDescriptionWithColDataDTO, DynamicFileDescriptionWithColData>().ForMember((DynamicFileDescriptionWithColData pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicFileDescriptionWithColDataDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicFileDescriptionWithColData, DynamicFileDescriptionWithColDataDTO>();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017760 File Offset: 0x00015960
		public static DynamicFileDescriptionWithColData ToDomainObject(this DynamicFileDescriptionWithColDataDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicFileDescriptionWithColDataDTO, DynamicFileDescriptionWithColData>(dynamicDataDTO);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00017778 File Offset: 0x00015978
		public static DynamicFileDescriptionWithColDataDTO ToDTO(this DynamicFileDescriptionWithColData dynamicData)
		{
			return Mapper.Map<DynamicFileDescriptionWithColData, DynamicFileDescriptionWithColDataDTO>(dynamicData);
		}
	}
}
