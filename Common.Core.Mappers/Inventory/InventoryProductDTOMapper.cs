using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000FA RID: 250
	public static class InventoryProductDTOMapper
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x00014C04 File Offset: 0x00012E04
		static InventoryProductDTOMapper()
		{
			PersonBaseMapper.CreateMap();
			InventoryProductStatusDTOMapper.CreateMap();
			InventoryVendorInfoDTOMapper.CreateMap();
			InventoryLocationDTOMapper.CreateMap();
			InventoryGroupDTOMapper.CreateMap();
			ProductBarCodeMapper.CreateMap();
			InventoryProductAccessoryMapper.CreateMap();
			Mapper.CreateMap<InventoryProduct, InventoryProductDTO>().ForMember((InventoryProductDTO dto) => dto.Vendor, delegate(IMemberConfigurationExpression<InventoryProduct> m)
			{
				m.MapFrom<InventoryVendorInfoDTO>((InventoryProduct bo) => bo.Vendor.ToDTO());
			}).ForMember((InventoryProductDTO dto) => dto.Accessories, delegate(IMemberConfigurationExpression<InventoryProduct> m)
			{
				m.MapFrom<IList<InventoryProductAccessoryDTO>>((InventoryProduct bo) => bo.Accessories.ToDTO());
			});
			Mapper.CreateMap<InventoryProductDTO, InventoryProduct>().ForMember((InventoryProduct bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryProductDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryProduct bo) => bo.Vendor, delegate(IMemberConfigurationExpression<InventoryProductDTO> m)
			{
				m.MapFrom<InventoryVendorInfo>((InventoryProductDTO dto) => dto.Vendor.ToDomainObject());
			}).ForMember((InventoryProduct bo) => bo.Accessories, delegate(IMemberConfigurationExpression<InventoryProductDTO> m)
			{
				m.MapFrom<IList<InventoryProductAccessory>>((InventoryProductDTO dto) => dto.Accessories.ToDomainObject());
			});
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014DE4 File Offset: 0x00012FE4
		public static InventoryProduct ToDomainObject(this InventoryProductDTO productDTO)
		{
			return Mapper.Map<InventoryProductDTO, InventoryProduct>(productDTO);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014DFC File Offset: 0x00012FFC
		public static InventoryProductDTO ToDTO(this InventoryProduct product)
		{
			return Mapper.Map<InventoryProduct, InventoryProductDTO>(product);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014E14 File Offset: 0x00013014
		public static IList<InventoryProduct> ToDomainObject(this IList<InventoryProductDTO> list)
		{
			IList<InventoryProduct> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryProduct>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00014E58 File Offset: 0x00013058
		public static IList<InventoryProductDTO> ToDTO(this IList<InventoryProduct> list)
		{
			IList<InventoryProductDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryProductDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
