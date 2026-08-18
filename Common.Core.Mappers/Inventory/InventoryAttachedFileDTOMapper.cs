using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F0 RID: 240
	public static class InventoryAttachedFileDTOMapper
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0001303C File Offset: 0x0001123C
		static InventoryAttachedFileDTOMapper()
		{
			InventoryAttachedFileInfoDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryAttachedFile, InventoryAttachedFileDTO>();
			Mapper.CreateMap<InventoryAttachedFileDTO, InventoryAttachedFile>().ForMember((InventoryAttachedFile pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<InventoryAttachedFileDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000130C0 File Offset: 0x000112C0
		public static InventoryAttachedFileDTO ToDTO(this InventoryAttachedFile attachedFile)
		{
			return Mapper.Map<InventoryAttachedFile, InventoryAttachedFileDTO>(attachedFile);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000130D8 File Offset: 0x000112D8
		public static InventoryAttachedFile ToDomainObject(this InventoryAttachedFileDTO attachedFileDTO)
		{
			return Mapper.Map<InventoryAttachedFileDTO, InventoryAttachedFile>(attachedFileDTO);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000130F0 File Offset: 0x000112F0
		public static IList<InventoryAttachedFileDTO> ToDTO(this IList<InventoryAttachedFile> list)
		{
			IList<InventoryAttachedFileDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryAttachedFileDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013134 File Offset: 0x00011334
		public static IList<InventoryAttachedFile> ToDomainObject(this IList<InventoryAttachedFileDTO> list)
		{
			IList<InventoryAttachedFile> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryAttachedFile>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
