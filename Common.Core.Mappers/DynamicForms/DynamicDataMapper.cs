using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000116 RID: 278
	public static class DynamicDataMapper
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00016FB4 File Offset: 0x000151B4
		static DynamicDataMapper()
		{
			DynamicFieldMapper.CreateMap();
			Mapper.CreateMap<DynamicDataDTO, DynamicData>().ForMember((DynamicData pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicDataDTO> m)
			{
				m.Ignore();
			}).ForMember((DynamicData pb) => pb.Field, delegate(IMemberConfigurationExpression<DynamicDataDTO> m)
			{
				m.MapFrom<DynamicField>((DynamicDataDTO pbdto) => (pbdto.Value == null) ? null : pbdto.Field.ToDomainObject());
			}).ForMember((DynamicData pb) => pb.Value, delegate(IMemberConfigurationExpression<DynamicDataDTO> m)
			{
				m.MapFrom<object>((DynamicDataDTO pbdto) => (pbdto.Value == null) ? null : pbdto.Value);
			});
			Mapper.CreateMap<DynamicData, DynamicDataDTO>().ForMember((DynamicDataDTO pb) => pb.Field, delegate(IMemberConfigurationExpression<DynamicData> m)
			{
				m.MapFrom<DynamicFieldDTO>((DynamicData pbdto) => (pbdto.Value == null) ? null : pbdto.Field.ToDTO());
			}).ForMember((DynamicDataDTO pb) => pb.Value, delegate(IMemberConfigurationExpression<DynamicData> m)
			{
				m.MapFrom<object>((DynamicData pbdto) => (pbdto.Value == null) ? null : pbdto.Value);
			});
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017170 File Offset: 0x00015370
		public static DynamicData ToDomainObject(this DynamicDataDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicDataDTO, DynamicData>(dynamicDataDTO);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00017188 File Offset: 0x00015388
		public static DynamicDataDTO ToDTO(this DynamicData dynamicData)
		{
			return Mapper.Map<DynamicData, DynamicDataDTO>(dynamicData);
		}
	}
}
