using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards;
using TechnoPro.Common.Public.Entities.Cards;

namespace TechnoPro.Common.Core.Mappers.Cards
{
	// Token: 0x0200017A RID: 378
	public static class CardLayoutMapper
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x0001DB29 File Offset: 0x0001BD29
		static CardLayoutMapper()
		{
			Mapper.CreateMap<CardLayoutDTO, CardLayout>();
			Mapper.CreateMap<CardLayout, CardLayoutDTO>();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001DB38 File Offset: 0x0001BD38
		public static CardLayout ToDomainObject(this CardLayoutDTO dto)
		{
			return Mapper.Map<CardLayoutDTO, CardLayout>(dto);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001DB50 File Offset: 0x0001BD50
		public static CardLayoutDTO ToDTO(this CardLayout item)
		{
			return Mapper.Map<CardLayout, CardLayoutDTO>(item);
		}
	}
}
