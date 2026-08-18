using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Core.Mappers.Updates
{
	// Token: 0x02000024 RID: 36
	internal static class eUpdateStatusDTOMapper
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00005696 File Offset: 0x00003896
		static eUpdateStatusDTOMapper()
		{
			Mapper.CreateMap<eUpdateStatusDTO, eUpdateStatus>();
			Mapper.CreateMap<eUpdateStatus, eUpdateStatusDTO>();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}
	}
}
