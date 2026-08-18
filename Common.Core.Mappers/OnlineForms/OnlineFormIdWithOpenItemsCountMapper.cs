using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.Core.Mappers.OnlineForms
{
	// Token: 0x020000B1 RID: 177
	public static class OnlineFormIdWithOpenItemsCountMapper
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		static OnlineFormIdWithOpenItemsCountMapper()
		{
			Mapper.CreateMap<OnlineFormIdWithOpenItemsCount, OnlineFormIdWithOpenItemsCountDTO>();
			Mapper.CreateMap<OnlineFormIdWithOpenItemsCountDTO, OnlineFormIdWithOpenItemsCount>();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		public static OnlineFormIdWithOpenItemsCount ToDomainObject(this OnlineFormIdWithOpenItemsCountDTO onlineFormDTO)
		{
			return Mapper.Map<OnlineFormIdWithOpenItemsCountDTO, OnlineFormIdWithOpenItemsCount>(onlineFormDTO);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		public static OnlineFormIdWithOpenItemsCountDTO ToDTO(this OnlineFormIdWithOpenItemsCount onlineForm)
		{
			return Mapper.Map<OnlineFormIdWithOpenItemsCount, OnlineFormIdWithOpenItemsCountDTO>(onlineForm);
		}
	}
}
