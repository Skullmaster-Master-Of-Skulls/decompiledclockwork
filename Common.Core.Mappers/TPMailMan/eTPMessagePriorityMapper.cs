using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x02000033 RID: 51
	internal static class eTPMessagePriorityMapper
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00006705 File Offset: 0x00004905
		static eTPMessagePriorityMapper()
		{
			Mapper.CreateMap<eTPMessagePriority, eTPMessagePriorityDTO>();
			Mapper.CreateMap<eTPMessagePriorityDTO, eTPMessagePriority>();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}
	}
}
