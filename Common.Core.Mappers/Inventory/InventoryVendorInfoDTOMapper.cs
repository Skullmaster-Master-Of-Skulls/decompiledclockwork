using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000FD RID: 253
	public static class InventoryVendorInfoDTOMapper
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00015878 File Offset: 0x00013A78
		static InventoryVendorInfoDTOMapper()
		{
			Mapper.CreateMap<InventoryVendorInfo, InventoryVendorInfoDTO>().ForMember((InventoryVendorInfoDTO dto) => (object)dto.PurchaseDate, delegate(IMemberConfigurationExpression<InventoryVendorInfo> m)
			{
				m.MapFrom<DateTime?>((InventoryVendorInfo bo) => bo.PurchaseDate.HasValue ? bo.PurchaseDate : ((DateTime?)null));
			}).ForMember((InventoryVendorInfoDTO dto) => (object)dto.WarrantyExpDate, delegate(IMemberConfigurationExpression<InventoryVendorInfo> m)
			{
				m.MapFrom<DateTime?>((InventoryVendorInfo bo) => bo.WarrantyExpDate.HasValue ? bo.WarrantyExpDate : ((DateTime?)null));
			});
			Mapper.CreateMap<InventoryVendorInfoDTO, InventoryVendorInfo>().ForMember((InventoryVendorInfo bo) => bo.Id, delegate(IMemberConfigurationExpression<InventoryVendorInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryVendorInfo bo) => (object)bo.PurchaseDate, delegate(IMemberConfigurationExpression<InventoryVendorInfoDTO> m)
			{
				m.MapFrom<DateTime?>((InventoryVendorInfoDTO dto) => dto.PurchaseDate.HasValue ? dto.PurchaseDate : ((DateTime?)null));
			}).ForMember((InventoryVendorInfo bo) => (object)bo.WarrantyExpDate, delegate(IMemberConfigurationExpression<InventoryVendorInfoDTO> m)
			{
				m.MapFrom<DateTime?>((InventoryVendorInfoDTO dto) => dto.WarrantyExpDate.HasValue ? dto.WarrantyExpDate : ((DateTime?)null));
			});
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00015A5C File Offset: 0x00013C5C
		public static InventoryVendorInfo ToDomainObject(this InventoryVendorInfoDTO vendorDTO)
		{
			return Mapper.Map<InventoryVendorInfoDTO, InventoryVendorInfo>(vendorDTO);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00015A74 File Offset: 0x00013C74
		public static InventoryVendorInfoDTO ToDTO(this InventoryVendorInfo vendor)
		{
			return Mapper.Map<InventoryVendorInfo, InventoryVendorInfoDTO>(vendor);
		}
	}
}
