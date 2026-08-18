using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000113 RID: 275
	public static class DynamicDataBaseMapper
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x00016E74 File Offset: 0x00015074
		static DynamicDataBaseMapper()
		{
			DynamicFieldMapper.CreateMap();
			Mapper.CreateMap<DynamicDataBaseDTO, DynamicDataBase>().ForMember((DynamicDataBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicDataBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicDataBase, DynamicDataBaseDTO>();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00016EF8 File Offset: 0x000150F8
		public static DynamicDataBase ToDomainObject(this DynamicDataBaseDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicDataBaseDTO, DynamicDataBase>(dynamicDataDTO);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00016F10 File Offset: 0x00015110
		public static DynamicDataBaseDTO ToDTO(this DynamicDataBase dynamicData)
		{
			return Mapper.Map<DynamicDataBase, DynamicDataBaseDTO>(dynamicData);
		}
	}
}
