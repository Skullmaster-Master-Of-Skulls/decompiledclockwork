using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.General;
using TechnoPro.Common.Public.Entities.General;

namespace TechnoPro.Common.Core.Mappers.General
{
	// Token: 0x02000108 RID: 264
	public static class ModificationHistoryItemBaseMapper
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x000162F0 File Offset: 0x000144F0
		static ModificationHistoryItemBaseMapper()
		{
			Mapper.CreateMap<ModificationHistoryItemBaseDTO, ModificationHistoryItemBase>();
			Mapper.CreateMap<ModificationHistoryItemBase, ModificationHistoryItemBaseDTO>();
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016300 File Offset: 0x00014500
		public static ModificationHistoryItemBase ToDomainObject(this ModificationHistoryItemBaseDTO dto)
		{
			return Mapper.Map<ModificationHistoryItemBaseDTO, ModificationHistoryItemBase>(dto);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00016318 File Offset: 0x00014518
		public static ModificationHistoryItemBaseDTO ToDTO(this ModificationHistoryItemBase item)
		{
			return Mapper.Map<ModificationHistoryItemBase, ModificationHistoryItemBaseDTO>(item);
		}
	}
}
