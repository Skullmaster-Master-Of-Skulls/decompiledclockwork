using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x02000032 RID: 50
	internal static class eTPMessageDeliveryMethodMapper
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000066F6 File Offset: 0x000048F6
		static eTPMessageDeliveryMethodMapper()
		{
			Mapper.CreateMap<eTPMessageDeliveryMethod, eTPMessageDeliveryMethodDTO>();
			Mapper.CreateMap<eTPMessageDeliveryMethodDTO, eTPMessageDeliveryMethod>();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}
	}
}
