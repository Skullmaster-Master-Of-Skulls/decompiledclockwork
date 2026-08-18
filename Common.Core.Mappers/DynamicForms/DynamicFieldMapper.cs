using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x0200011A RID: 282
	public static class DynamicFieldMapper
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00017638 File Offset: 0x00015838
		static DynamicFieldMapper()
		{
			Mapper.CreateMap<DynamicFieldDTO, DynamicField>().ForMember((DynamicField pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicFieldDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicField, DynamicFieldDTO>();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000176B4 File Offset: 0x000158B4
		public static DynamicField ToDomainObject(this DynamicFieldDTO dynamicFieldDTO)
		{
			return Mapper.Map<DynamicFieldDTO, DynamicField>(dynamicFieldDTO);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000176CC File Offset: 0x000158CC
		public static DynamicFieldDTO ToDTO(this DynamicField dynamicField)
		{
			return Mapper.Map<DynamicField, DynamicFieldDTO>(dynamicField);
		}
	}
}
