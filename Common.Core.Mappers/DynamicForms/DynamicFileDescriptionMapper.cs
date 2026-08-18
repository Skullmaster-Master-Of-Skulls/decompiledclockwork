using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x0200011C RID: 284
	public static class DynamicFileDescriptionMapper
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00017790 File Offset: 0x00015990
		static DynamicFileDescriptionMapper()
		{
			Mapper.CreateMap<DynamicFileDescriptionDTO, DynamicFileDescription>().ForMember((DynamicFileDescription pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicFileDescriptionDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicFileDescription, DynamicFileDescriptionDTO>();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001780C File Offset: 0x00015A0C
		public static DynamicFileDescription ToDomainObject(this DynamicFileDescriptionDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicFileDescriptionDTO, DynamicFileDescription>(dynamicDataDTO);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017824 File Offset: 0x00015A24
		public static DynamicFileDescriptionDTO ToDTO(this DynamicFileDescription dynamicData)
		{
			return Mapper.Map<DynamicFileDescription, DynamicFileDescriptionDTO>(dynamicData);
		}
	}
}
