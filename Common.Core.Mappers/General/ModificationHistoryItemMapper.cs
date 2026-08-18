using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.General;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.General;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.General
{
	// Token: 0x02000109 RID: 265
	public static class ModificationHistoryItemMapper
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00016330 File Offset: 0x00014530
		static ModificationHistoryItemMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ModificationHistoryItemDTO, ModificationHistoryItem>().ForMember((ModificationHistoryItem pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<ModificationHistoryItemDTO> m)
			{
				m.MapFrom<PersonBase>((ModificationHistoryItemDTO pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDomainObject());
			}).ForMember((ModificationHistoryItem pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<ModificationHistoryItemDTO> m)
			{
				m.MapFrom<PersonBase>((ModificationHistoryItemDTO pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDomainObject());
			});
			Mapper.CreateMap<ModificationHistoryItem, ModificationHistoryItemDTO>().ForMember((ModificationHistoryItemDTO pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<ModificationHistoryItem> m)
			{
				m.MapFrom<PersonBaseDTO>((ModificationHistoryItem pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDTO());
			}).ForMember((ModificationHistoryItemDTO pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<ModificationHistoryItem> m)
			{
				m.MapFrom<PersonBaseDTO>((ModificationHistoryItem pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDTO());
			});
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00016488 File Offset: 0x00014688
		public static ModificationHistoryItem ToDomainObject(this ModificationHistoryItemDTO dto)
		{
			return Mapper.Map<ModificationHistoryItemDTO, ModificationHistoryItem>(dto);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000164A0 File Offset: 0x000146A0
		public static ModificationHistoryItemDTO ToDTO(this ModificationHistoryItem item)
		{
			return Mapper.Map<ModificationHistoryItem, ModificationHistoryItemDTO>(item);
		}
	}
}
