using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000115 RID: 277
	public static class DynamicDataContextMapper
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00016F74 File Offset: 0x00015174
		static DynamicDataContextMapper()
		{
			Mapper.CreateMap<DynamicDataContextDTO, DynamicDataContext>();
			Mapper.CreateMap<DynamicDataContext, DynamicDataContextDTO>();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00016F84 File Offset: 0x00015184
		public static DynamicDataContext ToDomainObject(this DynamicDataContextDTO dynamicDataContextDTO)
		{
			return Mapper.Map<DynamicDataContextDTO, DynamicDataContext>(dynamicDataContextDTO);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00016F9C File Offset: 0x0001519C
		public static DynamicDataContextDTO ToDTO(this DynamicDataContext dynamicDataContext)
		{
			return Mapper.Map<DynamicDataContext, DynamicDataContextDTO>(dynamicDataContext);
		}
	}
}
