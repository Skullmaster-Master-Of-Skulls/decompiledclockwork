using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000EF RID: 239
	public static class InventoryAttachedFileInfoDTOMapper
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00012F72 File Offset: 0x00011172
		static InventoryAttachedFileInfoDTOMapper()
		{
			Mapper.CreateMap<InventoryAttachedFileInfo, InventoryAttachedFileInfoDTO>();
			Mapper.CreateMap<InventoryAttachedFileInfoDTO, InventoryAttachedFileInfo>();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012F84 File Offset: 0x00011184
		public static InventoryAttachedFileInfoDTO ToDTO(this InventoryAttachedFileInfo attachedFileInfo)
		{
			return Mapper.Map<InventoryAttachedFileInfo, InventoryAttachedFileInfoDTO>(attachedFileInfo);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00012F9C File Offset: 0x0001119C
		public static InventoryAttachedFileInfo ToDomainObject(this InventoryAttachedFileInfoDTO attachedFileInfoDTO)
		{
			return Mapper.Map<InventoryAttachedFileInfoDTO, InventoryAttachedFileInfo>(attachedFileInfoDTO);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00012FB4 File Offset: 0x000111B4
		public static IList<InventoryAttachedFileInfoDTO> ToDTO(this IList<InventoryAttachedFileInfo> list)
		{
			IList<InventoryAttachedFileInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryAttachedFileInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00012FF8 File Offset: 0x000111F8
		public static IList<InventoryAttachedFileInfo> ToDomainObject(this IList<InventoryAttachedFileInfoDTO> list)
		{
			IList<InventoryAttachedFileInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryAttachedFileInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
