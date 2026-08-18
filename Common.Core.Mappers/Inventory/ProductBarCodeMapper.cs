using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x02000102 RID: 258
	public static class ProductBarCodeMapper
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x00015F0C File Offset: 0x0001410C
		static ProductBarCodeMapper()
		{
			Mapper.CreateMap<ProductBarCode, ProductBarCodeDTO>();
			Mapper.CreateMap<ProductBarCodeDTO, ProductBarCode>().ForMember((ProductBarCode bo) => bo.Id, delegate(IMemberConfigurationExpression<ProductBarCodeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}
	}
}
