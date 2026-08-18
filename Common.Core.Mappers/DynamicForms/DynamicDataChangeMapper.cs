using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000114 RID: 276
	public static class DynamicDataChangeMapper
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00016F28 File Offset: 0x00015128
		static DynamicDataChangeMapper()
		{
			PersonBaseMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<DynamicDataChangeDTO, DynamicDataChange>();
			Mapper.CreateMap<DynamicDataChange, DynamicDataChangeDTO>();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00016F44 File Offset: 0x00015144
		public static DynamicDataChange ToDomainObject(this DynamicDataChangeDTO dynamicDataChangeDTO)
		{
			return Mapper.Map<DynamicDataChangeDTO, DynamicDataChange>(dynamicDataChangeDTO);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016F5C File Offset: 0x0001515C
		public static DynamicDataChangeDTO ToDTO(this DynamicDataChange dynamicDataChange)
		{
			return Mapper.Map<DynamicDataChange, DynamicDataChangeDTO>(dynamicDataChange);
		}
	}
}
